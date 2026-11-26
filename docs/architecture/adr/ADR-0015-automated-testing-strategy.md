# ADR-0015 — Estrategia de pruebas automatizadas

**Estado:** Aceptada  
**Fecha:** 2026-11-26

## Contexto

Las 34 especificaciones y sus documentos de aceptación incluyen aislamiento multitenant, reglas financieras, procesamiento asíncrono, integración externa y recuperación. Una estrategia basada sólo en unit tests o en E2E no ofrece simultáneamente velocidad, fidelidad y diagnóstico.

## Decisión

Adoptar una pirámide de pruebas por riesgo con PostgreSQL real para integración.

### Herramientas backend

- xUnit v3.
- `WebApplicationFactory` para API.
- Testcontainers for .NET con PostgreSQL.
- NSubstitute sólo en fronteras externas o técnicas.
- Pruebas arquitectónicas con ArchUnitNET o equivalente.
- FsCheck para propiedades financieras seleccionadas.
- Stryker.NET para mutation testing del núcleo financiero.

### Herramientas frontend

- Vitest.
- React Testing Library.
- MSW para componentes.
- Playwright Test para E2E.
- axe-core para accesibilidad crítica.

Las versiones se fijan centralmente.

## Unit tests

Cubren reglas puras de cuotas, beneficios, mora, elegibilidad, pagos, cálculos comerciales, devoluciones, contracargos, retención, autorización y máquinas de estado.

No usan base, red ni reloj real. Las pruebas citan SPEC y AC. Los cálculos financieros usan valores exactos y casos límite explícitos.

## Property-based testing

Se aplica a invariantes como asignaciones que no exceden el pago, devoluciones limitadas, disciplina única por checkout, duplicados sin efectos adicionales, idempotencia y períodos cerrados inmutables.

## Integración de módulos

Cada módulo se prueba contra PostgreSQL de la misma versión mayor que Producción para validar EF, migraciones, restricciones, RLS, filtros, concurrencia, locks, transacciones, outbox, inbox y consultas.

No se usan EF InMemory, SQLite ni mocks de `DbContext` como sustituto de PostgreSQL.

## Multitenancy

Las suites incluyen al menos dos clubes, varias disciplinas, correos repetidos y distintos roles. Intentan lecturas, escrituras, relaciones, descargas, búsquedas y sesiones cruzadas. Se valida autorización de aplicación y RLS.

## API

`WebApplicationFactory` levanta API con PostgreSQL real, migraciones, cookies, antiforgery, reloj controlado, adaptadores externos simulados, almacenamiento de prueba y worker controlable.

Se prueban contratos, autenticación, OTP, CSRF, revocación, autorización por recurso, idempotencia, ETags, SignalR, descargas y estados inciertos.

## OpenAPI

CI valida OpenAPI, cambios incompatibles, generación reproducible de TypeScript, compilación frontend, ejemplos sanitizados y esquemas de dinero, errores e idempotencia.

## Pasarela

Tres niveles:

1. Unit tests de mapeo con respuestas sanitizadas.
2. Contract tests contra servidor controlado.
3. Smoke tests periódicos contra sandbox oficial.

Se cubren éxito, rechazo, pendiente, incertidumbre, timeout, webhook duplicado o tardío, idempotencia, OAuth revocado, rate limit, devolución, contracargo y conciliación. No se procesa dinero real.

## Worker

Se prueban reclamo concurrente, leases, reinicio, reintento, agotamiento, misfire, exclusión, paralelismo tenant, cambio de frecuencia, cancelación y backlog. Reloj y scheduler son controlados.

## Frontend

Componentes cubren capacidades, carga, error, pendientes, formularios, conflictos, caché, importes, fechas, reconexión y accesibilidad. MSW se limita a estas pruebas.

Playwright cubre recorridos críticos de identidad, aislamiento, jugadores, cuotas, pagos, incertidumbre, conciliación, devoluciones, exportaciones y revocación. Los fallos conservan trazas y capturas.

## Arquitectura

Se verifican límites frontend, ciclos backend, referencias prohibidas, independencia de dominio, contratos sin EF, contextos propios y hosts usados sólo como composition roots.

## Migraciones

CI crea una base vacía, aplica migraciones, valida RLS e índices, ensaya actualización desde la versión anterior y comprueba compatibilidad expandir–migrar–contraer.

No se usan datos privados de `sources/` ni datos productivos sin anonimización aprobada.

## Recuperación

Se ensayan restauración PITR y mensual, arranque de API y worker, conciliación posterior, pagos inciertos, reanudación de mensajes y verificación de auditoría y RLS.

## Rendimiento y resiliencia

Antes de Producción se prueban concurrencia, doble envío, caída de pasarela y base, reinicios, backlog, exportaciones, dashboards y límites de recursos. Objetivos cuantitativos se fijan con los SLO.

## CI

| Etapa | Ejecución |
|---|---|
| Cada cambio | Formato, lint, tipos, unitarias, arquitectura y componentes |
| Pull request | Integración PostgreSQL, API y OpenAPI |
| Rama principal | Suite completa y E2E críticos |
| Programada | Sandbox, mutation testing, resiliencia y dependencias |
| Pre-release | Migraciones, rendimiento, seguridad y restauración |
| Gate DEC-170 | Evidencia de controles críticos |

## Cobertura

Cobertura de líneas no es el único gate. Toda rama financiera crítica tiene escenarios explícitos, cada defecto corregido agrega regresión y mutation testing se aplica al núcleo financiero.

## Trazabilidad

Cada AC se clasifica como automatizada, parcial, manual justificada o bloqueada externamente. Las pruebas citan `SPEC-###` y `AC-###-##`; una AC crítica no se considera cubierta por una prueba que omita PostgreSQL, navegador o pasarela cuando éstos son parte del comportamiento.

## Consecuencias

- Docker es requisito de integration tests.
- Persistencia se prueba sobre PostgreSQL real.
- La suite rápida se separa de sandbox, resiliencia y E2E.
- Se mantienen fixtures multitenant y simuladores externos.
- Aceptación y tests conservan trazabilidad explícita.
- Esta decisión no genera código productivo.

## Fuentes normativas

- `ACCEPTANCE-001`–`ACCEPTANCE-034`.
- `SPEC-001`–`SPEC-034`.
- Gate productivo de `SPEC-034` y DEC-170.
- ADR-0004–ADR-0014.

## Referencias técnicas

- [Integration tests en ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-10.0).
- [Testcontainers PostgreSQL](https://dotnet.testcontainers.org/modules/postgres/).
- [Playwright Trace Viewer](https://playwright.dev/docs/trace-viewer-intro).

