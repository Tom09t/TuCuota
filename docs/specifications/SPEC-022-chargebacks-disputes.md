# SPEC-022 — Contracargos y disputas

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir la protección de cuotas, defensa, alertas y efecto comercial de contracargos informados por la pasarela.

## Registro y protección

- Cada contracargo se vincula con su pago original y conserva estados, retenciones, costos y comisiones informados por el proveedor.
- Al abrirse la disputa, las cuotas quedan disputadas y protegidas contra nuevos checkouts; no se habilita otro cobro mientras exista incertidumbre.
- Eventos tardíos o repetidos son idempotentes y el caso se incorpora a conciliación, reportes y auditoría por disciplina.
- Administradores de la disciplina reciben avisos; el superadministrador supervisa globalmente y el delegado conserva solo lectura conforme a CON-009.

## Resultado financiero

- Una disputa ganada mantiene válido el pago y pagada la cuota.
- Una reversión definitiva total devuelve la cuota a pendiente, salvo anulación administrativa justificada.
- Mientras la disputa está pendiente mantiene su cómputo comercial; si se gana, lo conserva.
- Una reversión definitiva total descuenta según CON-018: en el período original si está abierto o mediante ajuste en uno abierto si el original cerró.
- Los ajustes son auditados e idempotentes y preservan pago, disputa y liquidaciones cerradas.

## Defensa

- El administrador consulta y aporta documentos solo para disputas de su disciplina.
- Enviar la defensa desde la plataforma depende de capacidad y permisos de la pasarela.
- El superadministrador interviene operativamente solo con autorización suficiente.
- Las evidencias respetan seguridad, retención e inmutabilidad de SPEC-020.
- Presentar requiere acción autorizada; nunca se envía documentación automáticamente.
- Se registran intentos, respuestas, actores, fechas y documentos. Presentar evidencia no significa ganar.

## Alertas de vencimiento

- Cada disputa conserva fecha y hora límite del proveedor.
- Se genera un aviso interno fijo 24 horas antes para administradores autorizados; el superadministrador mantiene visibilidad global.
- Existe un aviso por disputa, vencimiento y destinatario.
- Si la disputa llega con menos de 24 horas, se avisa inmediatamente.
- Una fecha oficial modificada reprograma sin duplicados.
- No se alerta cuando la defensa ya fue presentada, la disputa cerró o dejó de requerirla.

## Invariantes

- Disputa abierta no habilita un nuevo cobro.
- Presentar evidencia no determina el resultado.
- Solo una reversión definitiva total cambia el cómputo comercial.
- Eventos repetidos no duplican estados, ajustes ni avisos.
- Una liquidación comercial cerrada no se reescribe.

## Pendientes de definición

- Estados y capacidades reales de disputas en Mercado Pago.
- Tratamiento contractual de retenciones, costos y comisión de plataforma.
- Criterios y autoridad para una anulación administrativa tras pérdida definitiva.

Estos puntos requieren validación técnica, contractual y aprobación funcional.

## Trazabilidad

- DEC-146–DEC-148.
- RN-1455–RN-1505.
- CON-009, CON-018 y CON-019.
- ADR-0002 y `MATERIAL_REVIEW_07.md`.
