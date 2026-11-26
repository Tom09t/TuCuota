# ADR-0010 — Transacciones y consistencia financiera

**Estado:** Aceptada  
**Fecha:** 2026-11-25

## Contexto

El sistema debe evitar estados financieros parciales, cobros duplicados y reescritura de historia. PostgreSQL puede garantizar atomicidad local, pero una pasarela externa no participa de la misma transacción. ADR-0006 separa propiedad por módulos y ADR-0008 asigna un `DbContext` por módulo.

## Alternativas consideradas

### Transacción por `SaveChanges`

Es suficiente para comandos locales simples, pero no protege operaciones que coordinan varios módulos o pasos.

### Transacción que incluye llamadas externas

Mantiene locks durante operaciones de red y no puede revertir un cobro que la pasarela ya haya aceptado. Se descarta.

### Transacciones distribuidas

Mercado Pago no participa en ellas y generarían una falsa garantía de atomicidad. Se descartan.

### `Serializable` para todo

Ofrece protección fuerte, pero añade costo y abortos innecesarios a operaciones que pueden resolverse con restricciones o locks puntuales.

### ACID local y máquina de estados externa

Proporciona atomicidad real donde es posible y representa explícitamente incertidumbre, reintentos, conciliación y compensaciones.

## Decisión

Adoptar transacciones ACID cortas por comando, transacciones PostgreSQL compartidas cuando una invariante cruce módulos, ninguna llamada de red dentro de una transacción de base, máquinas de estado persistidas para integraciones externas, historia financiera inmutable, outbox transaccional y conciliación como mecanismo de convergencia.

## Fronteras transaccionales

| Operación | Cambios atómicos |
|---|---|
| Crear checkout | Validar y bloquear cuotas; crear checkout, asignaciones y protección |
| Confirmar pago | Transicionar pago; aplicar asignaciones; marcar cuotas; registrar desglose, auditoría y outbox |
| Registrar pago externo | Crear pago, asignaciones y efecto sobre cuotas |
| Solicitar devolución | Validar saldo; reservar importe y cuotas; crear solicitud pendiente |
| Confirmar devolución | Transicionar devolución; cambiar cuotas; registrar ajuste y evento |
| Abrir disputa | Registrar caso; proteger cuotas; registrar aviso pendiente |
| Cerrar período comercial | Fijar corte, resultados y liquidación cerrada |
| Resolver pago incierto | Validar evidencia y autoridad; registrar resolución; liberar cuotas y auditar |

Un fallo previo al commit no deja cambios parciales en PostgreSQL.

## Coordinación entre módulos

Cuando una operación coordina `Billing`, `Payments`, `Refunds` o `Commercial`:

- Un servicio de aplicación es dueño de la unidad de trabajo.
- Los contextos comparten `DbConnection` y `DbTransaction`.
- Cada módulo modifica solamente sus entidades.
- Existe un único commit posterior a todas las validaciones.
- RLS y tenant se configuran dentro de la transacción.
- Auditoría y outbox se guardan antes del mismo commit.

No se utilizan `TransactionScope`, MSDTC ni transacciones distribuidas para proveedores externos.

## Integración externa

Las llamadas a la pasarela se separan en:

1. Transacción que persiste intención pendiente y clave idempotente.
2. Llamada externa fuera de la transacción.
3. Transacción que persiste respuesta o incertidumbre.
4. Webhooks, consultas y conciliación que convergen sobre la misma operación.

Un timeout nunca se interpreta como rechazo. Mientras un resultado pueda ser exitoso, la operación permanece pendiente o incierta y las cuotas siguen protegidas. Un resultado tardío no crea una segunda imputación.

Las compensaciones son operaciones explícitas y auditadas; no se intenta deshacer localmente una operación que pudo completarse externamente.

## Niveles de aislamiento

| Caso | Estrategia |
|---|---|
| CRUD y comandos ordinarios | `Read Committed` |
| Selección o reserva de cuotas | `Read Committed` más locks de fila ordenados |
| Reserva de devolución | Locks sobre pago, asignaciones y solicitudes incompatibles |
| Fotografía financiera estable | `Repeatable Read` de sólo lectura o transacción específica |
| Invariante basada en rangos o ausencia de filas | `Serializable` selectivo con reintento completo |
| Cola persistida | `FOR UPDATE SKIP LOCKED` exclusivamente con semántica de cola |

No se usa `SKIP LOCKED` para decisiones financieras generales.

## Bloqueos

- Se bloquea la menor cantidad de filas posible.
- Locks múltiples se adquieren en orden estable.
- Ninguna transacción espera llamadas HTTP o interacción humana.
- Se configuran timeouts de comando, lock e inactividad transaccional.
- Un timeout o deadlock no habilita repetir ciegamente una llamada externa.
- Los reintentos abarcan unidades completas idempotentes.

## Historia financiera

- Los pagos confirmados no se reescriben como inexistentes.
- Devoluciones, contracargos, diferencias y ajustes son hechos relacionados.
- Las liquidaciones cerradas son inmutables.
- Los ajustes tardíos se registran en un período abierto sin modificar el cerrado.
- La evidencia corregida crea una versión nueva.
- La conciliación registra diferencias sin cambiar silenciosamente el origen.
- La retención conserva metadatos y auditoría exigidos.

## Fuentes de verdad

| Información | Fuente |
|---|---|
| Cuota y exigibilidad | `Billing` |
| Checkout, pago y asignación | `Payments` |
| Evidencia del proveedor | `Payments` |
| Solicitud y devolución | `Refunds` |
| Diferencia de conciliación | `Reconciliation` |
| Liquidación y ajuste | `Commercial` |
| Dashboard y exportación | Proyección de `Reporting` |

Las decisiones financieras consultan fuentes operativas, no proyecciones eventualmente consistentes.

## Outbox y efectos secundarios

La transacción que confirma un hecho financiero guarda cambio de estado, auditoría, evento outbox e identificador de correlación. Después del commit se procesan notificaciones, proyecciones, exportaciones, alertas e integraciones no críticas.

Una falla posterior no revierte el hecho financiero; el evento permanece reintentable.

## Reintentos

Una unidad completa puede reintentarse ante serialización, deadlock, conexión interrumpida antes de una llamada externa o contención transitoria conocida, siempre que el comando sea idempotente. Los intentos son limitados, usan backoff con jitter, crean una nueva transacción y registran agotamiento.

Nunca se repite ciegamente un cobro cuyo resultado sea desconocido.

## Consistencia de lectura

- Comandos y validaciones financieras leen tablas fuente.
- Dashboards, notificaciones y exportaciones pueden ser eventualmente consistentes.
- La interfaz distingue pendiente, confirmado, rechazado e incierto.
- Una proyección incompleta no se presenta como confirmación financiera.
- Los reportes identifican fecha o corte de actualización cuando corresponda.

## Consecuencias

- Estados pendientes e inciertos son parte normal del modelo externo.
- PostgreSQL brinda atomicidad local y conciliación brinda convergencia externa.
- Casos financieros seleccionados comparten transacción entre contextos.
- Los efectos secundarios no bloquean el commit financiero.
- Cada comando financiero debe diseñarse para reintento seguro.
- Esta decisión no genera código productivo.

## Trazabilidad

- Cuotas y pagos: `SPEC-003`–`SPEC-004`, `ACCEPTANCE-003`–`ACCEPTANCE-004`.
- Liquidaciones y períodos cerrados: `SPEC-005`–`SPEC-007`, `ACCEPTANCE-005`–`ACCEPTANCE-007`.
- Reportes y conciliación: `SPEC-008`–`SPEC-011`, `ACCEPTANCE-008`–`ACCEPTANCE-011`.
- Reintentos asíncronos: `SPEC-012`, `ACCEPTANCE-012`.
- Notificaciones sin efecto financiero: `SPEC-013`–`SPEC-015`.
- Checkout y pasarelas: `SPEC-017`–`SPEC-019`, `ACCEPTANCE-017`–`ACCEPTANCE-019`.
- Evidencia: `SPEC-020`, `ACCEPTANCE-020`.
- Devoluciones y disputas: `SPEC-021`–`SPEC-027`, `ACCEPTANCE-021`–`ACCEPTANCE-027`.
- Controles del MVP: `SPEC-028`, AC-028-09, AC-028-10.
- Operaciones durante despliegue: `SPEC-030`, AC-030-14, AC-030-15.
- Recuperación y conciliación: `SPEC-031`, `ACCEPTANCE-031`.
- Gate financiero: `SPEC-034`, `ACCEPTANCE-034`, DEC-170.
- Arquitectura previa: ADR-0006–ADR-0009.

## Referencias técnicas

- [Transacciones en EF Core](https://learn.microsoft.com/en-us/ef/core/saving/transactions).
- [Aislamiento transaccional de PostgreSQL](https://www.postgresql.org/docs/current/transaction-iso.html).
- [Locking en `SELECT`](https://www.postgresql.org/docs/current/sql-select.html).

