# SPEC-024 — Autorización de costos excepcionales

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir la asignación y autorización de costos adicionales no recuperables asociados con devoluciones excepcionales.

## Identificación de responsabilidad

- Toda excepción identifica el costo adicional, su causa, evidencia, importe y responsable económico.
- Las fallas técnicas imputables a TuCuota asignan los costos no recuperables a la plataforma.
- Los errores del pagador que la disciplina acepta devolver y los errores de gestión disciplinaria se asignan a esa disciplina.
- Los casos ambiguos se revisan antes de asignar responsabilidad; no se presume automáticamente.

## Autorización por origen

- Los administradores autorizan únicamente costos imputados a su disciplina y no comprometen fondos de otras disciplinas o de la plataforma.
- Solo el superadministrador autoriza costos asumidos por la plataforma.
- El delegado general mantiene acceso de solo lectura y no autoriza costos, conforme a CON-009.
- Toda excepción requiere motivo y registra importe, actor y fecha.

## Ejecución y resultado

- Antes de ejecutar se verifica que la operación sea técnicamente compatible con la pasarela.
- No se realizan movimientos entre cuentas sin autorización y compatibilidad.
- Aprobar administrativamente no equivale a ejecutar exitosamente; el resultado real depende del proveedor y se registra por separado.
- Las excepciones ingresan a conciliación y los reportes identifican qué parte asumió cada costo.
- La ejecución es idempotente y todas las autorizaciones y resultados quedan auditados.

## Invariantes

- Autorizar un costo no confirma el movimiento financiero.
- Una disciplina nunca compromete fondos ajenos o de plataforma.
- La responsabilidad ambigua no se asigna por defecto.
- Ninguna operación se ejecuta si la pasarela no la soporta.
- Un costo no se ejecuta dos veces.

## Pendientes de definición

- Procedimiento y autoridad para resolver casos de responsabilidad ambigua.
- Evidencias mínimas para atribuir una causa al SaaS, pagador o disciplina.
- Mecanismo técnico por proveedor para cubrir una diferencia autorizada.

Estos puntos requieren aprobación funcional y validación contractual.

## Trazabilidad

- DEC-151.
- RN-1539–RN-1564, vigentes conforme a `MATERIAL_REVIEW_08.md`.
- CON-009 y CON-011.
