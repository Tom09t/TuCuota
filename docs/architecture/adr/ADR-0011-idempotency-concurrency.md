# ADR-0011 — Idempotencia y control de concurrencia

**Estado:** Aceptada  
**Fecha:** 2026-11-25

## Contexto

Navegadores, API, workers, webhooks y pasarelas pueden reenviar solicitudes. No existe una garantía práctica de entrega exactamente una vez entre esos componentes. ADR-0010 exige reintentos seguros, protección de cuotas y convergencia sin duplicar efectos.

## Alternativas consideradas

### Confiar en el frontend

No cubre doble clic, recargas, timeouts ni reintentos automáticos. Se descarta.

### Confiar sólo en la pasarela

Protege algunas llamadas externas, pero no comandos internos, webhooks, pagos externos, eventos, importaciones o tareas.

### Locks para toda operación

Pueden controlar paralelismo inmediato, pero no reentregas posteriores y elevan la contención.

### Concurrencia optimista exclusivamente

Detecta modificaciones simultáneas, pero no impide por sí sola dos creaciones equivalentes o llamadas externas duplicadas.

### Capas complementarias

Combina claves idempotentes, inbox/outbox, restricciones únicas, concurrencia optimista y locks selectivos para cubrir cada frontera.

## Decisión

Adoptar entrega al menos una vez, procesamiento idempotente y efectos financieros como máximo una vez por operación lógica.

## Comandos idempotentes

Los comandos financieros o no reversibles aceptan una clave idempotente cuyo alcance incluye tenant, tipo de operación y actor o canal.

Cada registro conserva clave, tipo, club, disciplina, actor, hash canónico de parámetros, estado, recurso creado, respuesta estable o referencia, fechas, intentos, expiración, correlación y causalidad.

- La primera solicitud reserva la clave mediante unicidad en base.
- Una repetición con el mismo contenido devuelve el resultado o estado existente.
- La misma clave con contenido distinto se rechaza como conflicto.
- Una solicitud en curso no inicia otra ejecución.
- El efecto local y el registro se confirman en la misma transacción cuando sea posible.
- La expiración nunca es menor que la ventana de reintento del canal o proveedor.

## Pasarela

Cada operación externa usa una clave estable generada y persistida por el backend antes de llamar al proveedor. La misma operación reutiliza la clave después de un timeout; una operación nueva requiere una decisión funcional nueva.

La clave recibida desde un cliente no se reenvía directamente al proveedor sin validación y separación de ámbito.

## Webhooks e inbox

Cada evento entrante se registra por proveedor, conexión e identificador estable antes de procesarse. Si el proveedor carece de identificador suficiente, se usa una composición documentada de referencia externa, tipo y versión, con hash como apoyo.

El inbox conserva autenticidad, fechas, hash, estado, intentos, resultado, referencia y correlación. Un duplicado no repite transiciones, asignaciones, auditorías o notificaciones.

Validar autenticidad es previo a considerar el evento como evidencia. Recibir un mensaje no equivale a confirmar un pago.

## Outbox e inbox internos

- Cada mensaje outbox tiene `MessageId` global.
- Cada consumidor impone unicidad por `(Consumer, MessageId)`.
- El efecto del handler y su inbox se confirman en una transacción.
- Un mensaje completado no repite su efecto.
- Un fallo conserva el mensaje para reintento.
- Las notificaciones añaden claves funcionales de evento, incidente y destinatario.
- Las proyecciones mantienen posición o versión y detectan desorden.

## Otros procesos

- Pagos externos usan una referencia estable por ámbito y origen.
- Importaciones identifican lote, fila y clave natural normalizada.
- Conciliaciones identifican proveedor, operación y ejecución.
- Ajustes comerciales identifican hecho causal y período.
- Reanudaciones de exportación conservan la solicitud original.
- Migraciones iniciales no recrean resultados para el mismo lote y fila.

Importe y fecha por sí solos no constituyen una clave confiable.

## Concurrencia optimista

Los agregados mutables críticos usan una columna explícita `Version` como token de concurrencia. Se aplica a configuración, usuarios, roles, cuotas, beneficios, contratos, condiciones, devoluciones, políticas, tareas y expedientes editables.

- Una versión obsoleta produce conflicto.
- Las ediciones ordinarias requieren refrescar o confirmar.
- No se fusionan silenciosamente valores financieros.
- Los comandos internos sólo reintentan si reevaluarán todas las invariantes.
- La API puede exponer la versión mediante `ETag` y exigir `If-Match`.
- No se usa `xmin` como contrato de dominio.

## Concurrencia pesimista

Se usan locks de fila para incorporar cuotas a checkout, confirmar o liberar protecciones, reservar devoluciones, procesar transiciones financieras, resolver pagos inciertos, cerrar liquidaciones y reclamar trabajos persistidos.

- Se utiliza el lock mínimo suficiente.
- Múltiples filas se bloquean en orden estable.
- Las transacciones permanecen cortas.
- `NOWAIT` se usa cuando conviene informar contención.
- `SKIP LOCKED` se reserva para semántica de cola.
- No se mantiene un lock durante red o interacción humana.

## Restricciones

PostgreSQL constituye la última barrera mediante restricciones e índices únicos, incluidos índices parciales para estados activos. Se evita duplicar inscripciones, vínculos, asignaciones, eventos, devoluciones, notificaciones, ejecuciones y referencias externas.

## Conflictos

- Payload distinto con la misma clave: conflicto.
- Versión obsoleta: conflicto de concurrencia.
- Recurso financiero bloqueado: ocupado o pendiente.
- Operación externa sin resultado: incierta.
- Duplicado completado: mismo resultado estable.
- Duplicado en curso: estado y referencia existentes.
- Restricción violada: error de dominio sin exponer SQL.

## Retención

La retención de idempotencia, inbox y outbox considera reintentos del proveedor, webhooks tardíos, disputas, devoluciones, restauraciones, auditoría y replay. No se elimina un registro asociado con una operación abierta, preservación o investigación.

## Consecuencias

- Toda operación financiera define clave y alcance idempotente.
- Los duplicados son parte esperable del procesamiento.
- La edición usa concurrencia optimista por defecto; reservas y transiciones críticas usan locks.
- Idempotencia, inbox y outbox forman parte de backups y monitoreo.
- Los plazos exactos requieren validación con proveedores y operación.
- Esta decisión no genera código productivo.

## Trazabilidad

- Cuotas y pagos: `SPEC-003`–`SPEC-004`, AC-003-10, `ACCEPTANCE-004`.
- Notificación comercial: `SPEC-005`, AC-005-09.
- Conciliación: `SPEC-009`–`SPEC-011`, `ACCEPTANCE-009`–`ACCEPTANCE-011`.
- Exportaciones: `SPEC-012`, `ACCEPTANCE-012`.
- Notificaciones: `SPEC-013`–`SPEC-015`, `ACCEPTANCE-013`–`ACCEPTANCE-015`.
- Pasarelas y pagos inciertos: `SPEC-017`–`SPEC-019`, `ACCEPTANCE-017`–`ACCEPTANCE-019`.
- Evidencia, devoluciones y disputas: `SPEC-020`–`SPEC-027`, `ACCEPTANCE-020`–`ACCEPTANCE-027`.
- MVP: `SPEC-028`, AC-028-09, AC-028-10.
- Despliegue: `SPEC-030`, AC-030-14, AC-030-15.
- Migración idempotente: `SPEC-033`, AC-033-13.
- Gate financiero: `SPEC-034`, AC-034-05.
- Transacciones y consistencia: ADR-0010.

## Referencias técnicas

- [Concurrencia optimista en EF Core](https://learn.microsoft.com/en-us/ef/core/modeling/concurrency).
- [Idempotencia de Mercado Pago](https://www.mercadopago.com.ar/developers/es/news/2023/01/04/Idempotency-key-usage-will-be-mandatory).
- [Unicidad en PostgreSQL](https://www.postgresql.org/docs/current/index-unique-checks.html).

