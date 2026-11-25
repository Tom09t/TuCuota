# SPEC-019 — Cambio de pasarela y pagos inciertos

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir la transición entre proveedores, la protección de cuotas durante checkouts pendientes y la resolución excepcional de pagos inciertos.

## Cambio de pasarela

- El administrador solicita el cambio dentro de su disciplina hacia un proveedor habilitado, con OTP, autorización, validación y compatibilidad Split Payment.
- La disciplina conserva una única pasarela principal para nuevos cobros y la activa solo después de validar la nueva conexión.
- Si la validación falla, se mantiene la anterior si sigue operativa.
- Pagos y checkouts previos conservan proveedor, referencias y conciliación originales; no se reescriben comisiones, liquidaciones ni historia.
- La transición no duplica pagos ni afecta otras disciplinas y audita actor, fecha, hora y proveedores.

## Checkouts pendientes

- Un checkout previo puede completarse mientras siga vigente y el proveedor lo permita.
- Conserva proveedor, modalidad, vencimiento real, jugadores y cuotas.
- Una cuota con operación pendiente válida no puede incorporarse a otro checkout incompatible, incluso con otro proveedor.
- Cerrar la pantalla o superar un tiempo interno de monitoreo no cancela el pago.
- El estado real se obtiene mediante eventos, consultas y conciliación idempotentes.
- La cuota se libera solo tras confirmar vencimiento, cancelación o fallo definitivo y ausencia de pago confirmado.
- Los nuevos intentos usan la pasarela principal vigente y conservan todo el historial anterior.

## Estado incierto

- Cuando no puede confirmarse el resultado, la operación se marca incierta y sus cuotas permanecen protegidas.
- La consulta y conciliación continúan. Superado el plazo global configurado por proveedor, se alerta a administradores de la disciplina y superadministrador sin cambiar el estado financiero ni liberar cuotas.
- Los administradores pueden consultar, observar y aportar evidencia, pero no liberar arbitrariamente.
- El plazo y sus cambios son globales por proveedor, prospectivos y auditados.

## Resolución excepcional

- Se prioriza la confirmación oficial de la pasarela.
- Sin respuesta definitiva puede considerarse evidencia externa verificable que identifique la operación y sustente que el cobro no se completó.
- La declaración de un administrador no basta. Solo el superadministrador, desde sesión vigente, autoriza la resolución con justificación y evidencia obligatorias.
- No requiere OTP adicional, contraseña ni segunda aprobación, pero revalida permisos.
- Nunca puede resolverse como impago una operación con confirmación oficial exitosa.
- La liberación preserva el intento original, no crea checkout y registra proveedor, disciplina, cuotas, actor, fecha, motivo y evidencia.
- Eventos tardíos siguen procesándose; cualquier contradicción crea una discrepancia para conciliación sin segunda imputación automática.

## Invariantes

- Estado incierto implica protección contra doble cobro.
- Ningún tiempo interno equivale a cancelación del proveedor.
- Cambiar pasarela no cambia operaciones iniciadas.
- Solo evidencia suficiente y superadministrador permiten liberación excepcional.
- Un resultado tardío nunca duplica automáticamente una imputación.

## Pendientes de definición

- Plazo de alerta para cada proveedor.
- Catálogo de evidencia externa aceptable y su validación.
- Reglas reales de vigencia y cancelación de checkout por proveedor.

Estos puntos requieren aprobación funcional y validación con la pasarela.

## Trazabilidad

- DEC-133–DEC-139.
- RN-1238–RN-1350.
- ADR-0002 y `MATERIAL_REVIEW_07.md`.
