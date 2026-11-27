# R1 — Recorrido OpenAPI API–PostgreSQL–frontend

**Estado:** en construcción  
**Alcance:** corte técnico transversal; sin identidad funcional, datos reales ni operaciones financieras

## Resultado del corte

- Contrato OpenAPI 3.1 generado por ASP.NET Core durante la compilación.
- Contrato versionado y validado por CI para detectar diferencias no regeneradas.
- Cliente TypeScript determinístico generado desde el contrato, sin DTO duplicados manualmente.
- SPA conectada mediante TanStack Query a los endpoints técnicos de API y persistencia.
- Proxy local de Vite para mantener la frontera HTTP durante desarrollo.
- Health checks separados de liveness y readiness.
- Readiness PostgreSQL registrada únicamente cuando existe configuración externa.
- Prueba de integración API–PostgreSQL real mediante Testcontainers.
- Cadena de conexión recibida por configuración; no se incorporan secretos al contrato ni al frontend.

## Límites

El recorrido informa exclusivamente salud técnica. No resuelve identidad, autorización, clubes o disciplinas funcionales. El `TenantContext` sintético continúa limitado a R1 y deberá reemplazarse por resolución autenticada y autorizada en R2.

Este corte aporta evidencia base para el gate de R1, pero no declara cumplidas `ACCEPTANCE-001`, `ACCEPTANCE-030`, `ACCEPTANCE-032` ni `ACCEPTANCE-034`. Tampoco habilita despliegue productivo, datos reales o dinero real.

## Generación reproducible

1. `dotnet build src/TuCuota.Api/TuCuota.Api.csproj --configuration Release` genera `src/TuCuota.Api/openapi/TuCuota.Api.json`.
2. `npm run generate:client`, desde `frontend/`, genera `frontend/src/api/generated.ts`.
3. CI vuelve a ejecutar ambos pasos y falla si los archivos versionados quedan desactualizados.

## Trazabilidad

- ADR-0003, ADR-0007, ADR-0008, ADR-0013–ADR-0015 y ADR-0017.
- Gate R1: recorrido API–base–frontend observable y PostgreSQL real en integración.
- Base técnica de SPEC-001 / ACCEPTANCE-001, sin aceptación funcional.
- Base técnica de SPEC-030, SPEC-032 y SPEC-034, sin afirmar controles productivos.
- R0-D06 y `docs/r0/TRACEABILITY_CONVENTION.md`.
- DEC-170 y RN vigentes bajo la precedencia de CON-011; dinero real permanece bloqueado.
