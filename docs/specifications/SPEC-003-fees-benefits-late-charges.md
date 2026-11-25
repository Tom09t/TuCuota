# SPEC-003 — Cuotas, beneficios y recargos por mora

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir la configuración económica por disciplina, la generación mensual de cuotas y el tratamiento de beneficios, vencimientos, gracia y mora.

## Alcance

Incluye importes base, excepciones económicas, becas, generación automática, vencimiento y recargos. No incluye selección, procesamiento, conciliación ni devolución de pagos.

## Importes y beneficios

- Cada categoría tiene un importe mensual base configurable por el administrador de su disciplina.
- Un cambio de importe base rige desde el mes calendario siguiente; no recalcula cuotas ya generadas, cualquiera sea su estado.
- Se admiten tres beneficios: importe fijo personalizado, descuento porcentual sobre el importe base y beca con reglas reutilizables.
- Cada beneficio pertenece a una inscripción y no afecta otras disciplinas o clubes.
- Solo puede existir un beneficio aplicable por inscripción y período; no se acumulan beneficios.
- La asignación tiene inicio y finalización opcional. Sin finalización, continúa hasta su desactivación; al concluir, vuelve a regir el importe de categoría salvo otra excepción vigente.
- Al asignar o reemplazar un beneficio, el administrador elige aplicarlo a la cuota impaga del mes actual o desde el mes siguiente. No modifica meses anteriores ni cuotas pagadas.
- Aplicarlo al mes actual recalcula el pendiente y registra importe anterior, importe nuevo, beneficio y responsable.
- El reemplazo muestra ambos beneficios, exige confirmación, evita superposición y conserva el historial.

## Becas reutilizables

- Cada tipo de beca pertenece a una sola disciplina y solo puede asignarse a sus jugadores.
- Al modificar sus reglas se elige entre actualizar próximas cuotas de beneficiarios actuales o aplicarlas solo a nuevas asignaciones.
- Se conservan versiones históricas; las cuotas pagadas no cambian.

## Generación y vencimiento

- Cada disciplina configura un único día mensual de generación y otro de vencimiento, compartidos por sus categorías e independientes de otras disciplinas.
- La generación es automática y usa importes y beneficios vigentes para inscripciones habilitadas, respetando los estados del club y la disciplina.
- No existe generación mensual manual y se impide duplicar una cuota para la misma inscripción y período.
- Cada ejecución registra resultado, errores y cuotas generadas.
- Cada cuota conserva su fecha de vencimiento aunque cambie la configuración posterior.

## Mora y período de gracia

- Cada disciplina activa o desactiva la mora y configura cero o más días corridos de gracia.
- Durante la gracia la cuota está vencida sin recargo; el recargo comienza al día siguiente de su último día.
- La modalidad puede ser fija, porcentual o progresiva por tramos de días. El recargo se calcula al consultar el importe y se muestra separado de la cuota.
- Cada cuota conserva la política vigente al generarse: modalidad, valores o porcentajes, tramos y gracia. Los cambios posteriores solo rigen para cuotas nuevas.
- Si un beneficio recalcula la cuota actual impaga, el recargo porcentual se recalcula sobre el nuevo importe. Se conservan vencimiento, gracia, mora acumulada, política histórica y recargos fijos, registrando importes antes y después.
- Los recargos efectivamente cobrados y las versiones de configuración quedan auditados.

## Autorización y consulta

- El administrador modifica únicamente la configuración y los beneficios de su disciplina.
- El delegado general consulta la configuración de las disciplinas de su club sin modificarla.
- El superadministrador conserva alcance global.

## Invariantes

- Una cuota no puede duplicarse para una inscripción y período.
- Una modificación de configuración no reescribe silenciosamente cuotas históricas.
- Un beneficio o recargo de una disciplina no afecta otra disciplina o club.
- La excepción de recalcular el mes actual impago requiere elección explícita y trazabilidad.

## Pendientes de definición

- Regla para un día de generación o vencimiento que no exista en un mes calendario.
- Composición de los tramos progresivos y su comportamiento monetario al recalcular una cuota actual impaga.

Estos puntos no se completan por inferencia y requieren aprobación funcional.

## Trazabilidad

- DEC-030–DEC-045.
- RN-0172–RN-0277 y RN-C001–RN-C005.
- CON-003, CON-015 y CON-016.
- `MATERIAL_REVIEW_04.md`.
