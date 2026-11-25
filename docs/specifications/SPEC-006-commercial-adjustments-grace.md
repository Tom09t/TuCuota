# SPEC-006 — Ajustes comerciales y período de gracia

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir la revisión de liquidaciones, los cambios de condiciones comerciales y los períodos iniciales de gracia de cada contrato.

## Revisión de liquidaciones

- Toda liquidación se genera pendiente de aprobación.
- El superadministrador puede aprobar el importe calculado, condonarlo totalmente o ajustarlo parcialmente.
- Toda condonación o ajuste requiere justificación obligatoria.
- El importe original nunca se sobrescribe; se registra el importe final aprobado.
- Una condonación total deja la liquidación sin saldo exigible. Una aprobación o ajuste habilita el cobro del importe final.
- Se auditan fecha, responsable, motivo y valores original y final.
- El superadministrador consulta todas las liquidaciones. Cada responsable comercial consulta solo las propias, incluida la justificación; una disciplina independiente no accede a las demás.

## Cambios de condiciones

- Solo el superadministrador modifica el mínimo, el importe o la modalidad de penalización de un contrato.
- Las modalidades son cargo fijo mensual e importe por pago faltante.
- El nuevo valor o modalidad se aplica al mes calendario en curso completo, con independencia del día del cambio, y continúa vigente hasta otra modificación.
- Los pagos válidos ya contabilizados conservan su validez y el cumplimiento del mes se recalcula con el nuevo mínimo.
- El cierre usa la modalidad y el importe vigentes al momento del cálculo.
- Las liquidaciones de períodos anteriores cerrados no se recalculan automáticamente.
- Se conserva valor anterior, valor nuevo, fecha y superadministrador responsable.

## Período de gracia comercial

- Cada contrato puede definir independientemente un mes y año final de gracia, incluido dentro del período exento.
- Durante los meses cubiertos no se generan penalizaciones por incumplir el mínimo, aunque los pagos online se registran para estadísticas y reportes.
- La exigencia comienza automáticamente el primer día del mes siguiente al último mes de gracia.
- La gracia solo afecta al responsable del contrato y conserva configuración, vigencia e historial.

## Modificación de la gracia

- El superadministrador puede extenderla o acortarla cambiando su mes y año final.
- Extenderla para incluir el mes actual lo deja exento.
- Si el mes actual estaba cubierto y se acorta la gracia, no se genera penalización para ese mes ni se traslada posteriormente. El nuevo final rige prospectivamente.
- No se recalculan liquidaciones anteriores cerradas ni se afectan otros contratos.
- Se registran final anterior y nuevo, momento y responsable.

## Precedencia e invariantes

- Para RN-0404–RN-0411 se aplican exclusivamente las versiones `VERSION_CORREGIDA_CON-004`; las versiones anteriores son históricas.
- CON-021 desplaza el efecto retroactivo de RN-0464 sobre un mes que ya estaba cubierto.
- Ningún ajuste sobrescribe importes originales ni elimina historial.
- Un cambio contractual nunca modifica automáticamente una liquidación cerrada.

## Pendientes de definición

- Estado nominal de una liquidación totalmente condonada.
- Momento exacto de corte dentro del cierre cuando se modifica una condición el mismo día.

Estos puntos requieren aprobación funcional.

## Trazabilidad

- DEC-061–DEC-068.
- RN-0404–RN-0467, con las sustituciones indicadas.
- CON-004, CON-021 y CON-011.
- `MATERIAL_REVIEW_05.md`.
