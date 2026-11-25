# SPEC-004 — Selección y registro de pagos

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir cómo se seleccionan y cancelan cuotas completas mediante la pasarela o mediante pagos externos registrados por un administrador.

## Alcance

Incluye selección de cuotas, operaciones agrupadas, confirmación de cobro, pagos fallidos y pagos externos. No define todavía el checkout concreto de Mercado Pago, distribución de fondos, devoluciones, contracargos ni conciliación.

## Selección de cuotas

- El jugador puede seleccionar una o varias cuotas pendientes sin respetar orden cronológico.
- No necesita cancelar toda su deuda para iniciar un pago.
- Cada cuota seleccionada se incluye por su importe pendiente completo, con el recargo aplicable; no se permiten pagos parciales.
- Antes de iniciar el pago se muestra el detalle de cada cuota y el total de la operación.
- Varias cuotas completas pueden agruparse en una sola operación, manteniendo cada una su estado individual.
- Las cuotas no seleccionadas no cambian de estado.

## Resultado de la pasarela

- Una cuota solo se considera pagada cuando se confirma el cobro completo correspondiente.
- Una operación rechazada o fallida no cancela la deuda ni marca las cuotas como pagadas.
- El tratamiento de estados pendientes, notificaciones duplicadas y confirmación autoritativa deberá respetar las reglas posteriores de pasarela e idempotencia antes de su implementación.

## Pagos externos

- El administrador puede registrar pagos externos únicamente para cuotas de su disciplina.
- Se admiten efectivo, transferencia bancaria y otros medios externos.
- El registro incluye medio, fecha y administrador responsable; el comprobante es opcional.
- Solo un pago externo completo cancela las cuotas seleccionadas. No se admiten pagos parciales.
- El pago externo se identifica separadamente del procesado por la pasarela.
- Se conservan el historial de operaciones y los comprobantes adjuntos.
- El delegado general puede consultar los pagos externos de su club, sin registrarlos ni modificarlos.

## Invariantes

- Ninguna cuota se cancela sin confirmación completa del cobro o registro externo completo autorizado.
- Agrupar cuotas no fusiona sus identidades ni sus estados.
- Un pago nunca modifica cuotas ajenas a su selección.
- Un administrador no registra pagos en otra disciplina.
- Los pagos externos y los pagos de pasarela permanecen diferenciados.

## Condiciones de integración pendientes

ADR-0002 selecciona Mercado Pago Split Payments 1:1 con validaciones obligatorias. Antes de habilitar cobros reales deben definirse y probarse checkout, OAuth, webhooks, idempotencia, estados inciertos, distribución de costos, conciliación, devoluciones y contracargos. Estos aspectos no se completan en esta especificación y continúan sujetos a DEC-170.

## Trazabilidad

- DEC-046–DEC-048.
- RN-0278–RN-0301.
- `MATERIAL_REVIEW_04.md`.
- ADR-0002 y DEC-170 como restricciones de habilitación técnica y operativa.
