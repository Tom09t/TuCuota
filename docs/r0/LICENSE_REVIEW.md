# Revisión inicial de licencias

**Estado:** etapa R0 aprobada mediante R0-D23  
**Fecha de corte:** 2026-11-26  
**Alcance:** dependencias directas y herramientas seleccionadas

## Resultado

No se identificaron licencias bloqueadas entre las dependencias directas aprobadas. axe-core utiliza MPL-2.0 y recibe tratamiento condicional. La revisión del grafo transitivo, imágenes, paquetes del sistema y GitHub Actions no se declara cumplida y se controla mediante PAR-024.

## Inventario revisado

| Componente | Versión o línea | Licencia declarada | Uso | Resultado |
|---|---:|---|---|---|
| .NET, ASP.NET Core y EF Core | 10.0.15 | MIT | Runtime y framework | Admitido |
| Npgsql.EntityFrameworkCore.PostgreSQL | 10.0.4 | PostgreSQL | Runtime | Admitido |
| Quartz.NET / Hosting | 3.22.10 | Apache-2.0 | Runtime | Admitido |
| OpenTelemetry.Extensions.Hosting | 1.21.0 | Apache-2.0 | Runtime e instrumentación | Admitido |
| React / React DOM | 19.3.1 | MIT | Runtime frontend | Admitido |
| TypeScript | 7.0.2 | Apache-2.0 | Build | Admitido |
| Vite | 8.3.8 | MIT | Build | Admitido |
| TanStack Query | 5.108.0 | MIT | Runtime frontend | Admitido |
| @microsoft/signalr | 10.0.11 | MIT | Runtime frontend | Admitido |
| React Hook Form | 7.92.0 | MIT | Runtime frontend | Admitido |
| Zod | 4.6.7 | MIT | Runtime frontend | Admitido |
| xUnit v3 | 4.0.2 | Apache-2.0 | Pruebas | Admitido |
| Testcontainers PostgreSQL | 4.17.0 | MIT | Pruebas | Admitido |
| NSubstitute | 6.2.0 | BSD-3-Clause | Pruebas | Admitido |
| TngTech.ArchUnitNET | 0.13.5 | Apache-2.0 | Pruebas | Admitido |
| FsCheck | 3.6.0 | BSD-3-Clause | Pruebas | Admitido |
| dotnet-stryker | 5.0.0 | Apache-2.0 | Herramienta programada | Admitido; no runtime |
| Vitest | 5.1.2 | MIT | Pruebas | Admitido |
| React Testing Library | 16.3.3 | MIT | Pruebas | Admitido |
| MSW | 3.0.7 | MIT | Pruebas | Admitido |
| Playwright Test | 1.65.1 | Apache-2.0 | Pruebas E2E | Admitido |
| axe-core | 4.14.1 | MPL-2.0 | Pruebas de accesibilidad | Condicional |
| actions/checkout | 7.0.1 (`3d3c42e5aac5ba805825da76410c181273ba90b1`) | MIT | CI documental | Admitido; fijado por SHA |

## Tratamiento de axe-core

- Uso como herramienta de prueba.
- Sin modificación del paquete.
- Sin incorporación por presunción a imágenes o bundles productivos.
- Conservación de licencia y avisos aplicables.
- Revisión jurídica antes de Producción.

## Controles de la etapa R1

Antes de aceptar R1 deben existir:

- Manifests y lockfiles versionados.
- Inventario directo y transitivo reproducible.
- Inventario de imágenes y paquetes del sistema operativo.
- Inventario de GitHub Actions fijadas por SHA.
- Escaneo de licencias bloqueante.
- SBOM por artefacto.
- Archivo de avisos y atribuciones.
- Evidencia de que herramientas de prueba no ingresan a imágenes productivas.

## Límites

- Esta revisión no es asesoramiento jurídico.
- No cubre dependencias futuras no aprobadas.
- No afirma que el grafo transitivo inexistente haya sido auditado.
- No autoriza H1, Producción ni dinero real.

## Trazabilidad

- R0-D09 y R0-D21–R0-D23.
- ADR-0015 y ADR-0017.
- PAR-005 y PAR-024.
