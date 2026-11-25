# SPEC-008 — Dashboards y reportes

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir indicadores, filtros, desgloses y exportaciones según el ámbito autorizado de cada rol.

## Dashboard global

- Es exclusivo del superadministrador y consolida toda la plataforma.
- Muestra cantidades de clubes, disciplinas y contratos; pagos online exitosos; importes procesados; comisiones de plataforma; penalizaciones; liquidaciones y cumplimiento de mínimos.
- Incluye evolución mensual y filtros por club, disciplina y período.
- Los indicadores y gráficos se recalculan según los filtros y respetan la independencia económica de cada responsable.

## Dashboards de club y disciplina

- Cada administrador accede únicamente al dashboard de su disciplina, con jugadores, cuotas, pagos, deuda, recaudación, evolución e indicadores.
- Puede filtrar por período y categoría y exportar su información en Excel y PDF.
- El delegado general consulta un consolidado de su club y el detalle individual de sus disciplinas, siempre en modo de solo lectura.
- Ningún usuario de club accede a otro club y ningún administrador accede a información privada de otra disciplina.
- Los indicadores comerciales globales y costos de plataforma permanecen reservados al superadministrador.

## Desglose financiero

- Se muestran por separado importe nominal de cuotas, pagos online, pagos externos, comisión de plataforma, costo de pasarela cuando esté disponible y neto de cada disciplina.
- Se diferencia el total pagado por el jugador del importe deportivo.
- El desglose puede consultarse por operación y consolidado, sin contabilizar operaciones dos veces.
- Todos los datos respetan filtros y permisos del dashboard correspondiente.

## Exportaciones

- El superadministrador exporta reportes globales en `.xlsx` y PDF con los filtros seleccionados.
- Los reportes incluyen pagos, importes, comisiones, penalizaciones, liquidaciones, mínimos y desglose financiero.
- Identifican período, filtros y fecha de generación.
- Distinguen pagos online de externos e importes original y final de penalizaciones ajustadas o condonadas.
- Los administradores exportan únicamente información de su disciplina.

## Invariantes

- Visualizar o exportar nunca amplía el ámbito autorizado.
- El delegado mantiene acceso financiero de solo lectura y no aprueba, rechaza ni compromete fondos.
- Los costos e ingresos globales de plataforma no se exponen fuera del superadministrador.
- Los pagos online y externos nunca se mezclan en una única cifra sin desglose.
- Una operación no se contabiliza dos veces.

## Pendientes de definición

- Facultad del delegado general para exportar el consolidado de su club.
- Frecuencia de actualización y regla de corte temporal de indicadores.
- Tratamiento visual cuando el costo de pasarela aún no está disponible.

Estos puntos requieren aprobación funcional.

## Trazabilidad

- DEC-078–DEC-081.
- RN-0543–RN-0582.
- CON-005 y CON-009.
- `MATERIAL_REVIEW_05.md`.
