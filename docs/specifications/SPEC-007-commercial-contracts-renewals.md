# SPEC-007 — Contratos, renovaciones y alertas comerciales

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir la vigencia, renovación y continuidad temporal de los contratos comerciales, junto con sus alertas internas.

## Vencimiento contractual

- Cada contrato puede tener fecha de finalización.
- Al vencer, se notifica al superadministrador sin suspender automáticamente al responsable.
- El superadministrador puede renovar, autorizar continuidad temporal o suspender conforme a las reglas comerciales aprobadas.
- Hasta una decisión, siguen vigentes las condiciones anteriores, el conteo de pagos y la evaluación de mínimos.
- La decisión afecta solo al responsable contractual y conserva fecha, responsable y antecedentes.

## Renovación y versionado

- Cada renovación crea una nueva versión que copia las condiciones anteriores y permanece en borrador hasta su confirmación.
- Antes de confirmar, el superadministrador puede modificar mínimos, penalizaciones y fechas.
- Se configura mes y año de inicio; la versión aplica al mes completo y puede programarse para el futuro.
- No pueden coexistir versiones vigentes ni períodos superpuestos.
- Las versiones y liquidaciones históricas conservan condiciones y referencias originales.
- Una renovación futura confirmada puede editarse hasta su inicio; al comenzar rige su última configuración confirmada.
- Una vez vigente, sus cambios siguen las reglas ordinarias de ajustes comerciales.

## Cancelación programada

- Solo el superadministrador puede cancelar una renovación antes de su inicio, con justificación obligatoria.
- La renovación pasa a *Cancelada*, nunca entra en vigencia y conserva sus condiciones, motivo, fecha, hora y responsable.
- Cancelarla no cambia el contrato vigente; al finalizar este se aplican las reglas ordinarias de vencimiento.

## Continuidad temporal

- Solo el superadministrador autoriza la continuidad de un contrato vencido y define una fecha límite.
- Durante la continuidad se mantienen condiciones, conteo de pagos y evaluación de mínimos.
- Al vencer la autorización se alerta, pero no se suspende automáticamente.
- El superadministrador puede renovar, extender la continuidad o suspender. Mientras decide, la operación continúa con las condiciones existentes.
- Autorizaciones, extensiones y decisiones quedan auditadas.

## Alertas comerciales

- Se generan automáticamente 30, 15 y 7 días calendario antes del vencimiento de contratos y continuidades.
- Los plazos son fijos y el único destinatario es el superadministrador.
- Cada alerta identifica responsable, contrato o motivo y fecha de vencimiento.
- Las alertas se muestran en el centro comercial del panel; no se envían por correo.
- No se duplica una alerta para el mismo vencimiento y plazo. Renovar o extender actualiza la programación pendiente.
- Son informativas: no tienen acciones manuales de lectura o resolución ni modifican contratos.
- Permanecen activas mientras existe la situación; al resolverse desaparecen de la vista, pero conservan registro técnico.

## Invariantes

- Ningún vencimiento contractual o de continuidad provoca suspensión automática.
- Nunca existen dos versiones simultáneamente vigentes para el mismo contrato.
- Una versión histórica o liquidación cerrada no se reescribe por renovar.
- Ocultar una alerta resuelta no elimina su auditoría ni ejecuta acciones comerciales.

## Pendientes de definición

- Comportamiento de alertas cuando un contrato se configura con menos de 30, 15 o 7 días de anticipación.
- Zona horaria y hora exacta para activar versiones, vencimientos y alertas.

Estos puntos requieren aprobación funcional.

## Trazabilidad

- DEC-069–DEC-077.
- RN-0468–RN-0542.
- `MATERIAL_REVIEW_05.md`.
