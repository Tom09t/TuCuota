# SPEC-010 — Operación y alertas de conciliación

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir la programación automática, ejecución manual y alertas internas asociadas a conciliaciones financieras.

## Alertas por diferencias pendientes

- Existe un único plazo global, expresado en días calendario y configurable solo por el superadministrador.
- El plazo se aplica sin excepciones a todos los clubes y disciplinas.
- Se calcula desde la detección de la diferencia; al superarlo se genera una alerta interna para los administradores de la disciplina y el superadministrador.
- No se envían correos ni se duplican alertas para una misma diferencia pendiente.
- Al resolver la diferencia, la alerta desaparece de la vista activa, pero conserva su historial técnico.
- La alerta no cambia el estado de conciliación ni el pago original.
- Todo cambio de plazo registra usuario, fecha y valores anterior y nuevo.

## Conciliación automática

- Tiene una frecuencia global configurable solo por el superadministrador y limitada por las capacidades de la pasarela.
- Cada ejecución registra fecha, hora, resultado y cantidad de operaciones procesadas.
- No se procesan simultáneamente conciliaciones incompatibles ni se generan registros duplicados.
- Si la pasarela no está disponible, se registra el error sin alterar incorrectamente estados financieros.
- Una ejecución posterior puede reconsultar operaciones aún pendientes.
- Los cambios de frecuencia quedan auditados.

## Conciliación manual

- El superadministrador puede ejecutar cualquier ámbito; el administrador solo sus propias operaciones; el delegado únicamente consulta resultados de su club.
- El usuario autorizado puede procesar todas las pendientes de su ámbito, un rango de fechas o una operación por identificador.
- El sistema valida que toda operación seleccionada pertenezca al ámbito autorizado.
- La ejecución respeta límites y disponibilidad de la pasarela y evita concurrencia incompatible y duplicados.
- Se registran usuario, fecha, hora, alcance, parámetros, operaciones, resultados y errores.
- Una ejecución manual no modifica la frecuencia automática.
- Sus resultados se integran al historial y pueden crear o actualizar diferencias sin modificar silenciosamente pagos originales.

## Invariantes

- El plazo de alerta y la frecuencia automática son globales, no configurables por club o disciplina.
- Una falla externa nunca convierte por sí misma un estado financiero en exitoso o definitivo.
- Reintentar o ejecutar manualmente no debe duplicar conciliaciones.
- El delegado no inicia ejecuciones.
- Ninguna ejecución altera silenciosamente el pago fuente.

## Pendientes de definición

- Valores permitidos para plazo de alerta y frecuencia automática.
- Hora y zona temporal de evaluación del plazo en días calendario.
- Criterio exacto de incompatibilidad entre ejecuciones concurrentes.

Estos puntos requieren aprobación funcional y validación técnica con la pasarela.

## Trazabilidad

- DEC-086–DEC-090.
- RN-0624–RN-0672.
- `MATERIAL_REVIEW_05.md`.
