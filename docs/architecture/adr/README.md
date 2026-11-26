# Architecture Decision Records

Las decisiones de arquitectura se registran sin alterar los requisitos funcionales DEC/RN/CON.

| ADR | Estado | Decisión |
|---|---|---|
| [ADR-0001](ADR-0001-technology-stack.md) | Aceptada | .NET 10, ASP.NET Core Web API, C#, React, TypeScript, PostgreSQL y Docker. |
| [ADR-0002](ADR-0002-mercado-pago-split-payments.md) | Aceptada con gates | Mercado Pago Split Payments 1:1 como pasarela seleccionada. |
| [ADR-0003](ADR-0003-openapi-documentation.md) | Aceptada | OpenAPI 3.1 y documentación interactiva Swagger. |
| [ADR-0004](ADR-0004-modular-monolith.md) | Aceptada | Monolito modular con límites funcionales explícitos y una única unidad lógica y de versionado. |
| [ADR-0005](ADR-0005-dotnet-solution-structure.md) | Aceptada | Un proyecto .NET por módulo funcional, con capas internas y hosts usados como composition roots. |
| [ADR-0006](ADR-0006-module-boundaries-communication.md) | Aceptada | Nueve módulos por capacidad y propiedad de datos, comunicados mediante contratos síncronos y eventos internos. |
| [ADR-0007](ADR-0007-multitenancy-data-isolation.md) | Aceptada | Club como tenant en PostgreSQL compartido, protegido mediante autorización, filtros EF, RLS y restricciones de ámbito. |
| [ADR-0008](ADR-0008-ef-core-postgresql.md) | Aceptada | EF Core 10 y Npgsql, con un DbContext y esquema PostgreSQL por módulo y migraciones controladas. |
| [ADR-0009](ADR-0009-authentication-scope-authorization.md) | Aceptada | Identity tenant-aware, sesiones cookie revocables y autorización por políticas, recursos y ámbito. |
| [ADR-0010](ADR-0010-financial-transactions-consistency.md) | Aceptada | Transacciones ACID locales, estados externos persistidos, historia inmutable y convergencia por conciliación. |
| [ADR-0011](ADR-0011-idempotency-concurrency.md) | Aceptada | Idempotencia por frontera, inbox/outbox, versiones explícitas, restricciones y locks selectivos. |
| [ADR-0012](ADR-0012-async-processing-scheduling.md) | Aceptada | Worker separado, PostgreSQL durable y Quartz para tareas asíncronas y calendarios persistentes. |
| [ADR-0013](ADR-0013-audit-observability.md) | Aceptada | Auditoría funcional transaccional y telemetría portable mediante logs, métricas, trazas y OpenTelemetry. |
| [ADR-0014](ADR-0014-frontend-architecture.md) | Aceptada | SPA React/TypeScript modular construida con Vite, cliente OpenAPI, TanStack Query y SignalR. |
| [ADR-0015](ADR-0015-automated-testing-strategy.md) | Aceptada | Pirámide de pruebas por riesgo, PostgreSQL real, E2E críticos y trazabilidad con los criterios de aceptación. |
| [ADR-0016](ADR-0016-containers-environments.md) | Aceptada | Contenedores administrados, PostgreSQL gestionado, ambientes aislados y backups con PITR de 30 días y doce copias mensuales. |
| [ADR-0017](ADR-0017-security-secrets.md) | Aceptada | ASVS, gestor de secretos y KMS, cifrado por capas, runtime endurecido y cadena de suministro verificable. |

La arquitectura técnica objetivo está cerrada. Permanecen parámetros operativos y selecciones comerciales enumerados en `docs/architecture/ARCHITECTURE_CLOSURE.md`.

