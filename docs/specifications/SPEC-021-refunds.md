# SPEC-021 — Devoluciones de pagos

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir solicitudes, confirmación y efectos de devoluciones online por cuotas completas, respetando la capacidad real del proveedor.

## Autorización y seguimiento

- El administrador solicita devoluciones solo de pagos online de su disciplina. El superadministrador supervisa globalmente e interviene cuando tiene autorización suficiente.
- El delegado general mantiene acceso de solo lectura conforme a CON-009.
- Toda devolución se vincula con pago y checkout originales y registra motivo, importe solicitado, importe devuelto, proveedor, solicitante, fecha y resultado.
- Enviarla no implica completarla: requiere confirmación suficiente de la pasarela.
- Estados, errores y rechazos se conservan; eventos repetidos se procesan idempotentemente.

## Alcance monetario

- Puede devolverse el checkout completo o una o varias cuotas completas que lo integran.
- No se permiten fracciones monetarias arbitrarias de una cuota; CON-020 permanece descartada.
- La devolución de un subconjunto de cuotas depende del soporte real del proveedor y del Split Payment.
- No puede exceder el importe disponible ni volver a devolver una cuota ya completamente devuelta.
- Solicitudes pendientes reservan su importe e impiden devoluciones simultáneas incompatibles.

## Componentes financieros

- Se identifican por separado cuota, importe del destinatario, comisión de plataforma del 5% y costo de pasarela cuando exista información.
- No se presume que devolver el pago revierta automáticamente comisión o todos los componentes del split.
- El tratamiento real depende de capacidades y condiciones contractuales del proveedor.
- Toda devolución se refleja en conciliación y reportes, preservando el historial del pago.

## Estado posterior de la cuota

- Al solicitar, el administrador elige por cada cuota entre volver a *pendiente* o quedar *anulada*, con justificación.
- Mientras la devolución está pendiente no cambia definitivamente la cuota.
- Tras confirmación, una cuota todavía exigible vuelve a pendiente, previa verificación de que no existan pagos o checkouts incompatibles.
- Una cuota anulada queda cerrada sin deuda y no puede entrar en nuevos checkouts.
- Si la devolución falla, se conserva el estado derivado del pago original.

## Efecto comercial

- Una cuota con devolución total confirmada se descuenta del período comercial original si continúa abierto.
- Si el período está cerrado, se registra un ajuste en un período abierto sin reescribir la liquidación cerrada.
- Se conserva trazabilidad entre pago, cuota, devolución y ajuste.

## Invariantes

- Una cuota se devuelve completa o no se devuelve.
- Ningún estado cambia definitivamente antes de confirmación verificable.
- Una devolución no se duplica por concurrencia o eventos repetidos.
- Períodos cerrados no se reescriben.
- Toda operación permanece aislada por disciplina.

## Pendientes de definición

- Capacidades reales de Mercado Pago para devolver subconjuntos de cuotas y componentes del split.
- Estados detallados de devolución y tratamiento de comisión/costos.
- Período abierto concreto donde imputar ajustes de devoluciones tardías.

Estos puntos requieren validación técnica, contractual y aprobación funcional.

## Trazabilidad

- DEC-143–DEC-145.
- RN-1401–RN-1454.
- CON-009 y CON-018; CON-020 descartada.
- ADR-0002 y `MATERIAL_REVIEW_07.md`.
