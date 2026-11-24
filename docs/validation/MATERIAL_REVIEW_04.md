# Revisión material 04 — DEC-031 a DEC-060

**Estado:** aprobada por el propietario el 2026-11-24.  
**Cobertura:** RN-0179–RN-0403 y RN-C001–RN-C005, considerando CON-003 y CON-015–CON-017.

## Resultado por bloque

| DEC | RN | Resultado propuesto |
|---|---|---|
| DEC-031 | RN-C001–RN-C005 | Vigentes por aprobación expresa de CON-003. Complementan el registro original, que no contenía RN numeradas. |
| DEC-032–033 | RN-0179–0192 | Vigentes. Becas aisladas por disciplina, reutilizables y con vigencia configurable. |
| DEC-034 | RN-0193–0199 | Vigentes con CON-015: aplicación al mes actual únicamente si está impago, o desde el mes siguiente; nunca modifica meses anteriores ni cuotas pagadas. |
| DEC-035–036 | RN-0200–0212 | Vigentes. Un único beneficio activo por inscripción y reemplazo sin superposición, conservando historial. |
| DEC-037–038 | RN-0213–0225 | Vigentes. Cambios en tipos o importes aplican prospectivamente; cuotas ya generadas conservan sus valores salvo la excepción individual aprobada para el mes actual impago. |
| DEC-039–041 | RN-0226–0247 | Vigentes. Generación mensual automática, calendario y vencimiento independientes por disciplina. DEC-056 mantiene una excepción controlada para regularización. |
| DEC-042–045 | RN-0248–0277 | Vigentes con CON-016: el recargo porcentual se recalcula sobre el importe actualizado por beneficio, preservando política histórica, vencimiento, gracia, mora y recargos fijos. |
| DEC-046–048 | RN-0278–0301 | Vigentes. Selección libre de cuotas completas, agrupación en una operación y pagos externos separados por disciplina. |
| DEC-049–057 | RN-0302–0378 | Vigentes. Política comercial, liquidaciones, suspensión y regularización retroactiva permanecen diferenciadas de cuotas deportivas e inactividad ordinaria. |
| DEC-058 | RN-0379–0385 | Vigentes con sustitución parcial: RN-0382 queda histórica desplazada por CON-017. Cada cuota cancelada por pasarela cuenta como un pago, aunque una operación agrupe varias cuotas. |
| DEC-059 | RN-0386–0393 | Vigentes con sustitución parcial: RN-0388 queda histórica desplazada por CON-017. Cada cuota se imputa al mes calendario de confirmación de la operación que la cancela. |
| DEC-060 | RN-0394–0403 | Vigentes con CON-017: el cierre utiliza cuotas canceladas computables, excluye pagos externos y operaciones rechazadas o pendientes, y conserva idempotencia. |

## Hallazgos materiales

- RN-0382 y RN-0388 expresaban conteo por operación agrupada; CON-017 ordena conteo por cada cuota cancelada. Se preservan como históricas.
- Las reglas de idempotencia siguen vigentes: una misma cuota no puede computarse dos veces por notificaciones duplicadas.
- CON-015 no habilita modificar cuotas pagadas ni meses anteriores.
- CON-016 modifica la base del recargo porcentual, no la política histórica ni los demás componentes de mora.
- La generación retroactiva de DEC-055–057 es exclusiva de suspensión comercial y no contradice la inactividad ordinaria de DEC-024–026.

## Resolución aprobada

Estas clasificaciones quedan aprobadas como capa de vigencia para DEC-031–060, sin reescribir ni eliminar reglas del catálogo original.
