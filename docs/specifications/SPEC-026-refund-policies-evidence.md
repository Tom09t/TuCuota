# SPEC-026 — Políticas, motivos y evidencia de devolución

**Estado:** aprobada  
**Baseline:** `baseline-v1`

## Objetivo

Definir el versionado de políticas, la clasificación de motivos y los adjuntos de las solicitudes de devolución.

## Políticas versionadas

- Cada disciplina mantiene versiones independientes con identificador y fecha de vigencia.
- Cada pago referencia la política vigente al confirmarse y toda solicitud usa esa versión.
- Una nueva versión solo rige pagos posteriores y no reduce retroactivamente plazos informados.
- Las versiones usadas no se sobrescriben ni eliminan; permanecen consultables y auditadas.
- Solo administradores autorizados crean versiones de su disciplina, registrando actor, fecha y valores.
- Obligaciones legales prevalecen y la ejecución siempre verifica límites reales de pasarela.

## Motivo y causa

- La solicitud exige un motivo de catálogo; el responsable puede agregar explicación.
- El catálogo incluye error del pagador, error de disciplina, posible error técnico de plataforma y otros.
- Seleccionar *Otro* requiere explicación.
- El motivo declarado no determina responsabilidad económica.
- El administrador revisa antecedentes y registra separadamente causa verificada y justificación.
- Puede incorporar evidencia. Un error técnico de TuCuota requiere intervención del superadministrador; error del pagador aceptado o disciplinario asigna costos según DEC-151.
- Responsabilidad incierta o compartida requiere revisión excepcional.

## Archivos adjuntos

- El responsable puede adjuntar opcionalmente PDF, JPG o PNG y agregar más mientras la solicitud admita información.
- Se validan tipo y contenido, límites de tamaño y cantidad, y controles contra archivos maliciosos.
- Los archivos se almacenan cifrados y privados, vinculados con la solicitud y aislados por club y disciplina.
- Administradores consultan solicitudes bajo su responsabilidad; superadministrador según alcance global; responsable solo sus solicitudes vigentes.
- Las descargas requieren autorización y no usan enlaces públicos permanentes.
- El administrador puede pedir evidencia adicional; adjuntarla no aprueba la solicitud.
- Incorporaciones, consultas y descargas quedan auditadas y sujetas a conservación sin eliminar evidencia necesaria.

## Invariantes

- Un pago conserva la política vigente cuando se realizó.
- Motivo declarado y causa verificada son datos distintos.
- Adjuntar evidencia no decide la solicitud ni la responsabilidad.
- Una versión histórica utilizada nunca se sobrescribe.
- Archivos y solicitudes permanecen aislados por ámbito.

## Pendientes de definición

- Catálogo inicial completo de motivos.
- Límites de tamaño y cantidad de adjuntos.
- Momento a partir del cual una solicitud deja de admitir evidencia complementaria.
- Procedimiento de revisión de responsabilidad incierta o compartida.

Estos puntos requieren aprobación funcional y técnica.

## Trazabilidad

- DEC-155–DEC-157.
- RN-1612–RN-1661.
- DEC-151 y `MATERIAL_REVIEW_08.md`.
