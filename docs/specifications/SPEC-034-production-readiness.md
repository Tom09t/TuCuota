# SPEC-034 — Preparación y habilitación de cobros reales

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir el checklist integral y las aprobaciones exigidas antes de habilitar el piloto con dinero real, sin afirmar que los controles ya fueron implementados o superados.

## Checklist obligatorio

Antes de habilitar cobros reales deben estar verificados:

- Todas las funcionalidades esenciales comprometidas para el MVP.
- Autenticación, autorización y aislamiento entre clubes y disciplinas.
- Split Payment en sus dimensiones técnica, operativa y comercial con la pasarela seleccionada.
- Distribución correcta de fondos y comisiones, incluidas condiciones legales y contractuales aplicables.
- Cobros, idempotencia y prevención de pagos duplicados.
- Pagos inciertos, reintentos y conciliación.
- Migración inicial validada y aprobada.
- Backups y mecanismos de recuperación configurados y probados.
- Monitoreo automático y alertas técnicas operativos.
- Procedimientos documentados y ejecutables para incidentes financieros, devoluciones y contracargos.

## Aprobaciones y evidencia

- El resultado del checklist conserva evidencia y trazabilidad de cada validación y aprobación.
- El responsable técnico aprueba formalmente la versión candidata.
- El superadministrador autoriza la habilitación del piloto.
- Marcar un ítem, desplegar una versión o aprobar documentación no reemplaza la evidencia ni las dos aprobaciones requeridas.

## Bloqueos y excepciones

- Cualquier requisito crítico incumplido bloquea nuevos cobros reales.
- Un pendiente no crítico solo puede aceptarse si está documentado, evaluado y autorizado.
- La aceptación de un pendiente no crítico no permite reclasificar ni omitir un requisito crítico.
- La aprobación inicial no sustituye las validaciones requeridas para ampliar posteriormente el piloto.

## Condición crítica posterior

Si después de habilitar el piloto se detecta una condición crítica:

- Se suspenden las nuevas operaciones afectadas.
- Se preserva la trazabilidad de la decisión y del incidente.
- Los pagos anteriores continúan sujetos a conciliación; no se eliminan, revierten ni repiten por la suspensión.
- Reanudar las operaciones exige resolver y volver a validar la condición aplicable.

## Invariantes

- Ningún club procesa dinero real sin superar el checklist integral.
- La validación de Split Payment incluye distribución, comisiones y condiciones aplicables, no solo conectividad técnica.
- La versión candidata requiere aprobación técnica y autorización del superadministrador.
- Todo requisito crítico fallido bloquea cobros nuevos.
- Un pendiente no crítico nunca se acepta de forma tácita.
- Suspender nuevas operaciones no interrumpe la conciliación de pagos anteriores.

## Pendientes de definición

- Formato ejecutable del checklist y repositorio de evidencias.
- Responsable, evidencia mínima y vigencia de cada control.
- Identidad nominal del responsable técnico y mecanismo formal de firma o aprobación.
- Procedimiento para documentar, evaluar y autorizar pendientes no críticos.
- Protocolo operativo de suspensión, alcance de las operaciones afectadas y criterios de reanudación.
- Frecuencia de revalidación posterior al lanzamiento.

Estos puntos requieren definición posterior. La presente especificación no habilita Producción ni declara cumplido ningún control.

## Estado de cumplimiento

**Pendiente:** no se ha auditado evidencia de implementación ni se ha aprobado una versión candidata. Los cobros reales permanecen no autorizados.

## Trazabilidad

- DEC-170.
- RN-1872–RN-1889.
- `SPEC-028`–`SPEC-033` para alcance, piloto, entornos, backups, monitoreo y migración.
- `docs/product/MVP.md` y `MATERIAL_REVIEW_08.md`.

