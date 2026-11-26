[CmdletBinding()]
param(
    [string]$RepositoryRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

function Assert-Condition {
    param(
        [Parameter(Mandatory)] [bool]$Condition,
        [Parameter(Mandatory)] [string]$Message
    )

    if (-not $Condition) {
        throw $Message
    }
}

function Get-NumberedDocumentIds {
    param(
        [Parameter(Mandatory)] [string]$Directory,
        [Parameter(Mandatory)] [string]$Prefix,
        [Parameter(Mandatory)] [int]$Digits
    )

    $pattern = '^' + [regex]::Escape($Prefix) + '-(\d{' + $Digits + '})-.*\.md$'
    return @(
        Get-ChildItem -LiteralPath $Directory -File -Filter "$Prefix-*.md" |
            ForEach-Object {
                $match = [regex]::Match($_.Name, $pattern)
                Assert-Condition $match.Success "Nombre inesperado para ${Prefix}: $($_.Name)"
                [int]$match.Groups[1].Value
            } |
            Sort-Object
    )
}

function Assert-SequentialIds {
    param(
        [Parameter(Mandatory)] [int[]]$Actual,
        [Parameter(Mandatory)] [int]$First,
        [Parameter(Mandatory)] [int]$Last,
        [Parameter(Mandatory)] [string]$Label
    )

    $expected = @($First..$Last)
    Assert-Condition ($Actual.Count -eq $expected.Count) "${Label}: se esperaban $($expected.Count) documentos y se encontraron $($Actual.Count)."
    Assert-Condition (($Actual -join ',') -eq ($expected -join ',')) "${Label}: faltan identificadores o existen duplicados."
}

Push-Location $RepositoryRoot
try {
    git rev-parse --verify --quiet refs/tags/baseline-v1 | Out-Null
    Assert-Condition ($LASTEXITCODE -eq 0) 'No existe la etiqueta requerida baseline-v1.'

    $temporaryRoot = Join-Path ([System.IO.Path]::GetTempPath()) ("tucuota-baseline-" + [guid]::NewGuid().ToString('N'))
    $archivePath = Join-Path $temporaryRoot 'baseline-v1.zip'
    $extractPath = Join-Path $temporaryRoot 'content'
    New-Item -ItemType Directory -Path $extractPath -Force | Out-Null

    try {
        git archive --format=zip --output=$archivePath baseline-v1
        Assert-Condition ($LASTEXITCODE -eq 0) 'No se pudo archivar la etiqueta baseline-v1.'
        Expand-Archive -LiteralPath $archivePath -DestinationPath $extractPath

        $manifestPath = Join-Path $extractPath 'docs/baselines/baseline-v1/SHA256SUMS'
        Assert-Condition (Test-Path -LiteralPath $manifestPath -PathType Leaf) 'La etiqueta baseline-v1 no contiene SHA256SUMS.'

        foreach ($line in Get-Content -LiteralPath $manifestPath) {
            if ([string]::IsNullOrWhiteSpace($line)) {
                continue
            }

            $match = [regex]::Match($line, '^([0-9a-f]{64})  (.+)$')
            Assert-Condition $match.Success "Línea inválida en SHA256SUMS: $line"

            $expectedHash = $match.Groups[1].Value
            $relativePath = $match.Groups[2].Value.Replace('/', [System.IO.Path]::DirectorySeparatorChar)
            $filePath = Join-Path $extractPath $relativePath
            Assert-Condition (Test-Path -LiteralPath $filePath -PathType Leaf) "Archivo ausente en baseline-v1: $relativePath"

            $extension = [System.IO.Path]::GetExtension($filePath).ToLowerInvariant()
            Assert-Condition ($extension -in @('.md', '.csv')) "Tipo no soportado por el manifiesto textual: $relativePath"

            $text = [System.IO.File]::ReadAllText($filePath)
            $normalizedText = [regex]::Replace($text, "\r\n|\n|\r", "`n")
            $bytes = [System.Text.UTF8Encoding]::new($false).GetBytes($normalizedText)
            $actualHash = [Convert]::ToHexString([System.Security.Cryptography.SHA256]::HashData($bytes)).ToLowerInvariant()
            Assert-Condition ($actualHash -eq $expectedHash) "Hash inválido en baseline-v1: $relativePath"
        }
    }
    finally {
        if (Test-Path -LiteralPath $temporaryRoot) {
            Remove-Item -LiteralPath $temporaryRoot -Recurse -Force
        }
    }

    $specificationDirectory = Join-Path $RepositoryRoot 'docs/specifications'
    $acceptanceDirectory = Join-Path $RepositoryRoot 'docs/acceptance'
    $adrDirectory = Join-Path $RepositoryRoot 'docs/architecture/adr'

    $specIds = Get-NumberedDocumentIds -Directory $specificationDirectory -Prefix 'SPEC' -Digits 3
    $acceptanceIds = Get-NumberedDocumentIds -Directory $acceptanceDirectory -Prefix 'ACCEPTANCE' -Digits 3
    $adrIds = Get-NumberedDocumentIds -Directory $adrDirectory -Prefix 'ADR' -Digits 4

    Assert-SequentialIds -Actual $specIds -First 1 -Last 34 -Label 'SPEC'
    Assert-SequentialIds -Actual $acceptanceIds -First 1 -Last 34 -Label 'ACCEPTANCE'
    Assert-SequentialIds -Actual $adrIds -First 1 -Last 17 -Label 'ADR'

    foreach ($file in Get-ChildItem -LiteralPath $specificationDirectory -File -Filter 'SPEC-*.md') {
        $content = Get-Content -LiteralPath $file.FullName -Raw
        Assert-Condition ($content -match '(?mi)^\*\*Estado:\*\*\s+aprobada\b') "SPEC sin estado aprobado: $($file.Name)"
    }

    foreach ($file in Get-ChildItem -LiteralPath $acceptanceDirectory -File -Filter 'ACCEPTANCE-*.md') {
        $content = Get-Content -LiteralPath $file.FullName -Raw
        Assert-Condition ($content -match '(?mi)^\*\*Estado:\*\*\s+aprobada\b') "ACCEPTANCE sin estado aprobado: $($file.Name)"
    }

    foreach ($file in Get-ChildItem -LiteralPath $adrDirectory -File -Filter 'ADR-*.md') {
        $content = Get-Content -LiteralPath $file.FullName -Raw
        Assert-Condition ($content -match '(?mi)^\*\*Estado:\*\*\s+Aceptada\b') "ADR sin estado aceptado: $($file.Name)"
    }

    $r0Files = Get-ChildItem -LiteralPath (Join-Path $RepositoryRoot 'docs/r0') -File -Filter '*.md'
    foreach ($file in $r0Files) {
        $content = Get-Content -LiteralPath $file.FullName -Raw

        foreach ($match in [regex]::Matches($content, '\bSPEC-(\d{3})\b')) {
            $id = [int]$match.Groups[1].Value
            Assert-Condition ($specIds -contains $id) "Referencia SPEC inexistente en $($file.Name): $($match.Value)"
        }

        foreach ($match in [regex]::Matches($content, '\bACCEPTANCE-(\d{3})\b')) {
            $id = [int]$match.Groups[1].Value
            Assert-Condition ($acceptanceIds -contains $id) "Referencia ACCEPTANCE inexistente en $($file.Name): $($match.Value)"
        }

        foreach ($match in [regex]::Matches($content, '\bADR-(\d{4})\b')) {
            $id = [int]$match.Groups[1].Value
            Assert-Condition ($adrIds -contains $id) "Referencia ADR inexistente en $($file.Name): $($match.Value)"
        }
    }

    $trackedFiles = @(git ls-files)
    Assert-Condition ($LASTEXITCODE -eq 0) 'No se pudo enumerar el contenido versionado.'
    $forbiddenNames = '(?i)(^|/)(\.env($|\.)|id_rsa($|\.)|id_ed25519($|\.)|.*\.(pfx|p12|pem|key))$'
    $forbidden = @($trackedFiles | Where-Object { $_ -match $forbiddenNames })
    Assert-Condition ($forbidden.Count -eq 0) ("Archivos sensibles versionados: " + ($forbidden -join ', '))

    $secretPatterns = 'AKIA[0-9A-Z]{16}|-----BEGIN (RSA |EC |OPENSSH )?PRIVATE KEY-----'
    git grep -I -n -E $secretPatterns -- . ':!scripts/validate-r0-docs.ps1' | Out-Null
    Assert-Condition ($LASTEXITCODE -eq 1) 'Se detectó un patrón compatible con credenciales o una falla del escaneo.'

    Write-Host 'Validación documental R0 completada correctamente.'
}
finally {
    Pop-Location
}
