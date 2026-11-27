# R1 — Fundación de persistencia multitenant

**Estado:** en construcción  
**Alcance:** primer corte técnico de persistencia; sin funcionalidad financiera ni datos reales

## Resultado del corte

- `TenantContext` inmutable para una unidad de trabajo de club.
- `AccessDbContext` aislado en el esquema PostgreSQL `access`.
- Filtro EF Core nombrado por `ClubId`.
- Entidad técnica `TenantBoundaryProbe` para probar aislamiento sin anticipar el modelo funcional de R2.
- Primera migración del módulo Access con RLS, `FORCE ROW LEVEL SECURITY` y políticas de lectura/escritura con denegación por defecto.
- Endpoint técnico `/api/system/persistence`, habilitado sólo cuando existe una cadena `ConnectionStrings__PostgreSql` externa.

## Límites

El identificador de club usado por el host es sintético y temporal. No representa resolución de identidad ni autorización funcional. R2 deberá resolver el contexto desde una identidad autenticada y un ámbito autorizado.

No se versionan secretos. La cadena de conexión se recibe por configuración externa; el fallback del factory de diseño es exclusivamente local y usa una credencial marcada `development-only`.

## Trazabilidad

- ADR-0007 y ADR-0008.
- ADR-0015 y ADR-0017.
- SPEC-001 / ACCEPTANCE-001, únicamente como base técnica; no se declara aceptación funcional.
- Gate R1: recorrido API–PostgreSQL–frontend y pruebas de aislamiento base.
