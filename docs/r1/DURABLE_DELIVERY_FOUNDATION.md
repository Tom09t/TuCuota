# R1 — Fundación de entrega durable e idempotencia

**Estado:** en construcción
**Alcance:** primitivas técnicas reutilizables; sin mensajes funcionales, proveedores externos ni dinero real

## Resultado del corte

- Outbox durable por tenant con `MessageId` global, payload `jsonb`, disponibilidad, intentos, lease, correlación y causalidad.
- Inbox durable con unicidad por consumidor y `MessageId`, hash de payload, resultado técnico, intentos y correlación.
- Registro de idempotencia con unicidad por club, alcance de operación y clave; conserva hash, estado, referencias y expiración.
- RLS habilitada y forzada en las tres tablas, con denegación por defecto cuando no existe contexto de club.
- Reclamo de outbox mediante transacción corta, lease y `FOR UPDATE SKIP LOCKED`.
- Token explícito `Version` para demostrar concurrencia optimista sin usar `xmin` como contrato.
- Pruebas PostgreSQL reales para reclamo concurrente sin duplicados, restricciones de inbox/idempotencia y rechazo de una versión obsoleta.

## Límites

Este corte no publica ni procesa eventos funcionales. Tampoco implementa scheduler, reintentos con backoff, agotamiento, limpieza por retención, handlers, auditoría funcional ni integración con proveedores.

Los plazos de retención y expiración permanecen pendientes de las ventanas funcionales y de proveedores. Ningún registro asociado a una operación abierta podrá eliminarse por una política genérica.

No se habilitan pagos, datos reales, infraestructura productiva ni dinero real.

## Trazabilidad

- ADR-0006 y ADR-0008.
- ADR-0010, ADR-0011, ADR-0012, ADR-0013 y ADR-0015.
- Gate R1: componentes reutilizables de outbox, inbox, idempotencia y concurrencia.
- Base transversal de SPEC-030, SPEC-032 y SPEC-034, sin declarar sus ACCEPTANCE cumplidas.
- DEC y RN vigentes se aplican bajo la precedencia de CON-011.
- DEC-170 permanece bloqueante para dinero real.
