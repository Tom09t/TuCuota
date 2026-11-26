# ADR-0005 — Estructura de la solución .NET 10

**Estado:** Aceptada  
**Fecha:** 2026-11-25

## Contexto

ADR-0004 establece un monolito modular con límites funcionales explícitos. La estructura física de la solución debe hacer visibles y verificables esos límites sin multiplicar innecesariamente los proyectos durante el MVP.

ADR-0001 fija .NET 10 y ASP.NET Core Web API para el backend, React con TypeScript para el frontend y Docker para empaquetado y ejecución. ADR-0003 establece que el host HTTP publicará el contrato OpenAPI 3.1.

## Alternativas consideradas

### Un único proyecto ASP.NET Core

Ofrece un inicio simple, pero deja los límites del monolito modular sujetos únicamente a carpetas y convenciones, con alto riesgo de acoplamiento accidental.

### Proyectos globales por capa

Una separación global en Domain, Application, Infrastructure y API es conocida, pero agrupa por criterio técnico capacidades funcionales no relacionadas y facilita dependencias transversales entre dominios.

### Cuatro proyectos por módulo

Proporciona aislamiento físico entre las capas de cada módulo, pero genera decenas de proyectos y referencias con un costo desproporcionado para el MVP.

### Un proyecto por módulo con capas internas

Aísla cada capacidad en su propio assembly y mantiene una cantidad razonable de proyectos. La separación entre capas internas debe complementarse con pruebas arquitectónicas.

## Decisión

Adoptar una solución .NET 10 con un proyecto por módulo funcional y capas internas dentro de cada módulo.

La estructura objetivo es:

```text
SistemaTuCuota/
├── TuCuota.slnx
├── global.json
├── Directory.Build.props
├── Directory.Packages.props
├── .editorconfig
├── src/
│   ├── TuCuota.Api/
│   ├── TuCuota.BuildingBlocks/
│   └── Modules/
│       ├── TuCuota.Modules.Access/
│       ├── TuCuota.Modules.Sports/
│       ├── TuCuota.Modules.Billing/
│       ├── TuCuota.Modules.Payments/
│       ├── TuCuota.Modules.Commercial/
│       ├── TuCuota.Modules.Reconciliation/
│       ├── TuCuota.Modules.Notifications/
│       ├── TuCuota.Modules.Refunds/
│       ├── TuCuota.Modules.Reporting/
│       └── TuCuota.Modules.Operations/
├── tests/
│   ├── TuCuota.ArchitectureTests/
│   ├── TuCuota.IntegrationTests/
│   └── Modules/
│       └── TuCuota.Modules.<Module>.Tests/
├── frontend/
└── docs/
```

Los nombres y agrupamientos de módulos son provisionales hasta la decisión específica sobre límites y comunicación.

## Reglas estructurales

- `TuCuota.Api` es el host HTTP y composition root. No contiene reglas de negocio.
- Cada módulo organiza internamente su código en `Domain`, `Application`, `Infrastructure` y `Presentation`.
- Cada módulo expone su superficie pública mediante un namespace `Contracts`.
- `TuCuota.BuildingBlocks` se limita a abstracciones y primitivas técnicas estables. No contiene entidades, servicios funcionales ni un modelo de dominio común.
- Se prohíben referencias circulares.
- Los módulos no referencian `TuCuota.Api`.
- Las dependencias entre módulos son excepcionales, direccionales y verificadas mediante pruebas arquitectónicas.
- No se crean proyectos globales genéricos `Domain`, `Application` o `Infrastructure`.
- La SPA permanece en `frontend/` y no se compila dentro de los proyectos de dominio.
- Si el procesamiento asíncrono requiere un proceso separado, se agregará `TuCuota.Worker` como segundo composition root sobre los mismos módulos, sin duplicar dominio ni infraestructura.

## Configuración transversal

- `global.json` fija la familia y política de actualización del SDK .NET 10.
- `Directory.Build.props` centraliza propiedades del compilador, análisis estático, nullability y tratamiento de advertencias.
- `Directory.Packages.props` centraliza las versiones de paquetes NuGet.
- `.editorconfig` establece convenciones reproducibles.
- `TuCuota.slnx` es el archivo de solución.

## Consecuencias

- Los límites funcionales son visibles en la solución y verificables en integración continua.
- Se evita tanto un proyecto gigante como la proliferación de cuatro proyectos por cada módulo.
- Un proceso worker podrá reutilizar los mismos módulos sin crear otro sistema lógico.
- La separación entre capas internas depende de reglas y pruebas arquitectónicas.
- La definición final de módulos, sus dependencias, el ORM, autenticación y herramientas de prueba permanece pendiente de decisiones posteriores.
- Esta decisión define estructura, pero no crea ni autoriza código productivo.

## Trazabilidad

- Tenancy, acceso y responsables: `SPEC-001`, `SPEC-016`, `ACCEPTANCE-001`, `ACCEPTANCE-016`.
- Gestión deportiva y cuotas: `SPEC-002`–`SPEC-003`, `ACCEPTANCE-002`–`ACCEPTANCE-003`.
- Pagos y pasarelas: `SPEC-004`, `SPEC-017`–`SPEC-020`, `ACCEPTANCE-004`, `ACCEPTANCE-017`–`ACCEPTANCE-020`.
- Gestión comercial: `SPEC-005`–`SPEC-007`, `ACCEPTANCE-005`–`ACCEPTANCE-007`.
- Conciliación, reportes y exportaciones: `SPEC-008`–`SPEC-012`, `ACCEPTANCE-008`–`ACCEPTANCE-012`.
- Notificaciones: `SPEC-013`–`SPEC-015`, `ACCEPTANCE-013`–`ACCEPTANCE-015`.
- Devoluciones y disputas: `SPEC-021`–`SPEC-027`, `ACCEPTANCE-021`–`ACCEPTANCE-027`.
- Operación, lanzamiento y habilitación financiera: `SPEC-028`–`SPEC-034`, `ACCEPTANCE-028`–`ACCEPTANCE-034`.
- Stack tecnológico y contrato HTTP: ADR-0001 y ADR-0003.
- Arquitectura de monolito modular: ADR-0004.

## Referencias técnicas

- [Administración de soluciones con `dotnet sln`](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-sln).
- [Central Package Management de NuGet](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management).
- [Configuración compartida de MSBuild](https://learn.microsoft.com/en-us/visualstudio/msbuild/customize-by-directory).

