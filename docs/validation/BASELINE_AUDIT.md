# Auditoría de baseline funcional

## Estado

**En curso.** Este documento registra controles y pendientes; no certifica una baseline funcional definitiva ni autoriza crear `baseline-v1`.

## Criterio de precedencia

Todo cotejo aplica `CON-011`: CON aprobada, corrección explícita aprobada, excepción específica y regla original vigente. Las reglas compatibles se complementan. `CON-020` permanece descartada y no introduce requisitos.

## Resultados estructurales

| Control | Resultado |
|---|---:|
| DEC únicas | 170 |
| RN originales únicas | 1.889 |
| RN complementarias | 5 |
| CON aprobadas | 20 |
| CON descartadas | 1 (`CON-020`) |
| PRE-RN históricas | 6 |
| DEC sin huecos de numeración | Sí |
| RN sin huecos de numeración | Sí |
| CON sin huecos de numeración | Sí |

## Estados que impiden el cierre

- 1.818 RN están marcadas `EXTRAIDA_NO_VALIDADA`; requieren cotejo con DEC y fuente antes de cambiar su estado.
- 55 RN están marcadas `REVISAR_IMPACTO`: DEC-005 (5), DEC-006 (7), DEC-012 (5), DEC-019 (6), DEC-020 (6) y DEC-151 (26).
- 169 DEC conservan estado `Cotejo pendiente` en la matriz de trazabilidad.
- DEC-031 no tenía RN numeradas en el registro original; RN-C001–RN-C005 fueron aprobadas posteriormente por CON-003.
- La trazabilidad PRD→DEC todavía no fue construida ni validada.
- Los ocho candidatos de revisión de `VALIDACION.md` fueron cotejados y resueltos en `MATERIAL_REVIEW_02.md`; la detección original no se considera exhaustiva.

## Cotejo estructural de las RN extraídas

Se ejecutó un control completo sobre las 1.818 filas con estado de origen `EXTRAIDA_NO_VALIDADA`:

| Control | Fallas |
|---|---:|
| Formato y presencia de DEC asociada | 0 |
| Identificador de mensaje ausente en DEC.md | 0 |
| Identificador de mensaje ausente en la transcripción privada | 0 |
| Texto original no encontrado literalmente en DEC.md | 0 |

El resultado confirma integridad de extracción y vinculación básica, pero no valida por sí solo vigencia, compatibilidad semántica ni completitud contextual. El estado de origen se conserva hasta completar el cotejo semántico por bloques.

## Revisiones materiales aprobadas

- `MATERIAL_REVIEW_01.md`: 55 RN sobre roles, permisos y costos excepcionales.
- `MATERIAL_REVIEW_02.md`: ocho mensajes candidatos de recuperación e integridad.
- `MATERIAL_REVIEW_03.md`: revisión semántica de DEC-001–030 y RN-0001–0178.
- `MATERIAL_REVIEW_04.md`: revisión semántica de DEC-031–060, RN-0179–0403 y RN-C001–RN-C005.
- `MATERIAL_REVIEW_05.md`: revisión semántica de DEC-061–090 y RN-0404–0672.
- `MATERIAL_REVIEW_06.md`: revisión semántica de DEC-091–120 y RN-0673–1061.
- `MATERIAL_REVIEW_07.md`: revisión semántica de DEC-121–150 y RN-1062–1538.

Ambas revisiones son capas de auditoría aditivas. No alteran el catálogo importado ni implican el cierre integral.

## Historial y sustituciones preservadas

- PRE-RN-0001–PRE-RN-0006 permanecen históricas y no son requisitos vigentes.
- RN-0404–RN-0411 conservan tanto la versión histórica como la corrección de DEC-061; prevalecen las versiones corregidas según CON-004 y CON-011.
- RN-0014 fue sustituida por el texto aprobado en CON-014.
- RN-0099 fue sustituida por el texto aprobado en CON-013.
- DEC-151 conserva sus dos bloques históricos; no se descarta automáticamente ninguno.

## Resoluciones aprobadas de la primera revisión material

Las siguientes interpretaciones fueron aprobadas por el propietario el 2026-11-24. Se registran como una capa de vigencia sin alterar el texto ni eliminar las entradas históricas del catálogo original:

| Bloque | Posible conflicto | Evidencia posterior | Acción requerida |
|---|---|---|---|
| RN-0022–RN-0026 (DEC-005) | Define administradores de club con permisos homogéneos y amplios. | DEC-019/020/021 y CON-005 distinguen delegado y administradores por disciplina. | Preservar como históricas las reglas de homogeneidad/alcance desplazadas y mantener las compatibles sobre multiplicidad, aislamiento y acceso diferenciado. |
| RN-0027–RN-0033 (DEC-006) | Permite que cualquier administrador gestione administradores del club y protege al último administrador del club. | CON-005–CON-007 distribuyen facultades por rol y protegen al último administrador por disciplina. | Aplicar la sustitución parcial; conservar baja lógica, historial y aislamiento compatibles. |
| RN-0066–RN-0070 (DEC-012) | Autoriza genéricamente a administradores del club a cambiar identidad visual. | CON-008 limita la facultad al superadministrador y delegado general. | Tratar RN-0067 como histórica desplazada y conservar RN-0066/RN-0068–0070 con el alcance corregido. |
| RN-0102–RN-0107 (DEC-019) | Niega el rol de administrador general del club. | DEC-021/022 y CON-005 reconocen al delegado general con facultades limitadas. | Tratar RN-0102 como histórica desplazada; conservar el aislamiento por disciplina con la excepción del delegado. |
| RN-0108–RN-0113 (DEC-020) | No contempla las facultades posteriores del delegado ni las protecciones del último administrador. | CON-005–CON-007. | Mantener reglas compatibles y complementarlas con las facultades y protecciones posteriores. |
| RN-1539–RN-1564 (DEC-151) | Dos bloques de la misma DEC regulan autorización y asignación de costos excepcionales. | CON-009 limita al delegado a solo lectura y mantiene facultades financieras por ámbito. | Considerar ambos bloques complementarios; el delegado no adquiere facultades de aprobación. |

El detalle de vigencia efectiva se conserva en `MATERIAL_REVIEW_01.md`. El estado `REVISAR_IMPACTO` del catálogo importado se mantiene como evidencia de origen y se resuelve mediante ese registro, sin mutar la extracción original.

## Secuencia de trabajo pendiente

1. Cotejar primero las 55 RN `REVISAR_IMPACTO` y los mensajes candidatos de `VALIDACION.md`.
2. Validar sustituciones y correcciones contra sus DEC, CON y fuentes.
3. Cotejar las 1.818 RN restantes por bloques de DEC, sin reescribir su texto.
4. Construir y revisar trazabilidad PRD→DEC→RN→CON.
5. Clasificar contradicciones materiales para decisión expresa del propietario.
6. Preparar un informe de cierre para aprobación; solo después podrá proponerse `baseline-v1`.

## Restricciones

No se modifican requisitos funcionales, identificadores, fuentes privadas ni estados de vigencia sin evidencia y aprobación. No se adoptan decisiones de arquitectura ni se inicia implementación durante esta auditoría.
