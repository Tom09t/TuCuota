[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$env:DOTNET_CLI_HOME = Join-Path $root '.dotnet-home'
$env:DOTNET_SKIP_FIRST_TIME_EXPERIENCE = '1'
$env:DOTNET_CLI_TELEMETRY_OPTOUT = '1'
Push-Location $root

try {
    dotnet new sln --name TuCuota --format slnx
    dotnet new webapi --name TuCuota.Api --output src/TuCuota.Api --no-openapi --no-restore
    dotnet new worker --name TuCuota.Worker --output src/TuCuota.Worker --no-restore
    dotnet new classlib --name TuCuota.BuildingBlocks --output src/TuCuota.BuildingBlocks --no-restore

    $modules = @('Access', 'Sports', 'Billing', 'Payments', 'Commercial', 'Reconciliation', 'Notifications', 'Refunds', 'Reporting')
    foreach ($module in $modules) {
        dotnet new classlib --name "TuCuota.Modules.$module" --output "src/Modules/TuCuota.Modules.$module" --no-restore
    }

    dotnet new xunit --name TuCuota.ArchitectureTests --output tests/TuCuota.ArchitectureTests --no-restore
    dotnet new xunit --name TuCuota.IntegrationTests --output tests/TuCuota.IntegrationTests --no-restore

    $projects = Get-ChildItem -Path src,tests -Filter '*.csproj' -Recurse | ForEach-Object FullName
    dotnet sln TuCuota.slnx add $projects

    dotnet add src/TuCuota.Api/TuCuota.Api.csproj reference src/TuCuota.BuildingBlocks/TuCuota.BuildingBlocks.csproj
    dotnet add src/TuCuota.Worker/TuCuota.Worker.csproj reference src/TuCuota.BuildingBlocks/TuCuota.BuildingBlocks.csproj
    dotnet add tests/TuCuota.ArchitectureTests/TuCuota.ArchitectureTests.csproj reference src/TuCuota.BuildingBlocks/TuCuota.BuildingBlocks.csproj
    dotnet add tests/TuCuota.IntegrationTests/TuCuota.IntegrationTests.csproj reference src/TuCuota.Api/TuCuota.Api.csproj
}
finally {
    Pop-Location
}
