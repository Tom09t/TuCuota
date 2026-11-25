# SPEC-005 — Política comercial, liquidaciones y suspensión

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir el mínimo mensual de pagos de la plataforma, los cargos por incumplimiento, las liquidaciones comerciales y los efectos de suspensión y reactivación.

## Alcance

Incluye configuración comercial por club o disciplina, conteo mensual, liquidación, suspensión y regularización de cuotas omitidas. Los cargos comerciales permanecen separados de las cuotas deportivas.

## Configuración y responsable

- Solo el superadministrador configura condiciones, responsable económico y alcance por club o disciplina.
- La configuración establece un mínimo mensual y un cargo fijo o un cargo unitario por pago faltante.
- El cargo es cero cuando se alcanza el mínimo. Si no se alcanza, la modalidad fija aplica el importe configurado; la modalidad por faltante multiplica déficit por importe unitario.
- Cada cargo se imputa únicamente al responsable contractual. Una disciplina independiente no afecta a las demás.
- Se conservan condiciones, responsables e historial utilizados en cada cálculo.

## Conteo y cierre mensual

- Cada cuota cancelada por pasarela cuenta como un pago, incluso dentro de una operación agrupada.
- Se contabiliza en el mes calendario de confirmación, sin importar el período de la cuota.
- No cuentan pagos externos, pendientes, rechazados ni cancelados.
- Una cuota computable no puede contarse dos veces por notificaciones duplicadas.
- El cierre mensual es automático, evalúa el mínimo del responsable y evita liquidaciones duplicadas para el mismo contrato y período.
- Si existe incumplimiento, genera una liquidación inicialmente *Pendiente de aprobación* con cálculo, configuración y pagos contabilizados.
- Solo el superadministrador puede aprobarla; antes de ello no se habilita su cobro.

## Liquidaciones

- La liquidación pertenece al responsable comercial y puede estar pendiente, pagada o vencida.
- Puede pagarse por pasarela; el superadministrador también puede registrar un pago externo con medio, fecha y responsable.
- Se conserva el historial de liquidaciones, pagos, modificaciones y aprobación.

## Suspensión comercial

- Una deuda vencida genera alerta, pero nunca suspensión automática.
- Solo el superadministrador decide suspender, permitir continuidad o reactivar.
- La suspensión afecta exclusivamente al responsable: bloquea nuevas inscripciones, generación de cuotas y modificaciones administrativas.
- Administradores y jugadores conservan las consultas autorizadas; las cuotas anteriores siguen siendo pagables con sus recargos.
- Datos, deuda e historial permanecen intactos.

## Reactivación y regularización

- Al reactivar, el superadministrador decide si habilita la regularización de meses suspendidos.
- Si no la habilita, esos períodos quedan sin cuotas nuevas. Si la habilita, el sistema prepara una propuesta, todavía no cobrable.
- El administrador revisa jugadores, registra pagos externos del período y ajusta vencimientos antes de confirmar explícitamente.
- La confirmación genera las cuotas omitidas con inscripciones y condiciones económicas de cada período, sin duplicar cuotas ni pagos.
- La regularización es una excepción controlada exclusiva de suspensión comercial; no aplica a una desactivación ordinaria.
- La operación habitual se restablece inmediatamente y continúa en paralelo; los borradores históricos no bloquean las cuotas actuales.

## Invariantes

- RN-0382 y RN-0388 no son aplicables: prevalece CON-017.
- El contador comercial y el registro financiero de cobros son conceptos separados.
- Ninguna penalización comercial se cobra sin aprobación.
- Suspender o reactivar no borra deudas ni registros.
- La regularización nunca duplica cuotas o pagos.

## Pendientes de definición

- Plazo de vencimiento de una liquidación comercial y transición exacta a estado vencido.
- Reglas operativas del pago de liquidaciones mediante la pasarela.

Estos puntos requieren definición aprobada y no se completan por inferencia.

## Trazabilidad

- DEC-049–DEC-060.
- RN-0302–RN-0403, conservando RN-0382 y RN-0388 solo como históricas.
- CON-017 y CON-011.
- `MATERIAL_REVIEW_04.md`.
