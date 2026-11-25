# SPEC-009 — Conciliación financiera

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir la detección, consulta y resolución auditada de diferencias entre pagos aprobados e importes efectivamente liquidados.

## Estados y detección

- El estado del pago y el de su liquidación son independientes.
- Se distinguen pagos aprobados, pendientes de liquidación y liquidados.
- Cuando la pasarela lo permita, se consulta automáticamente la liquidación y se compara el importe esperado con el efectivamente liquidado.
- Se detectan diferencias de importe, operaciones faltantes y liquidaciones pendientes.
- Cada diferencia identifica operación, club, disciplina y destinatario.
- Los dashboards distinguen importes aprobados de importes liquidados.

## Revisión y resolución

- El administrador consulta, investiga y resuelve únicamente diferencias de su disciplina.
- Resolver exige seleccionar un tipo activo e informar un motivo; los comprobantes son opcionales.
- Se registran usuario, fecha, hora, importe esperado, importe liquidado, diferencia, tipo, explicación y documentación.
- La revisión o resolución no modifica automáticamente ni silenciosamente el pago original.
- Se conservan conciliaciones, resultados, estados, intervenciones y observaciones.
- Las diferencias pendientes y resueltas permanecen disponibles en reportes.

## Ámbitos de acceso

- El superadministrador tiene visibilidad y auditoría global.
- El delegado general consulta diferencias y resoluciones de las disciplinas de su club en modo de solo lectura.
- Ningún administrador gestiona diferencias de otra disciplina y ningún usuario accede a otro club.

## Catálogo de resoluciones

- Existe un catálogo global con tipos predeterminados.
- Solo el superadministrador crea, modifica o desactiva tipos; no se eliminan físicamente.
- Los administradores solo seleccionan tipos activos.
- Desactivar un tipo impide nuevas selecciones, pero las conciliaciones históricas conservan el nombre y descripción originales.
- Cada cambio del catálogo registra usuario, fecha y valores anterior y nuevo.

## Invariantes

- Conciliar no equivale a alterar el pago original.
- Pago aprobado no implica necesariamente liquidación completada.
- Toda resolución requiere tipo, motivo y responsable.
- La desactivación de catálogo nunca reescribe resoluciones históricas.
- Toda consulta respeta club y disciplina.

## Pendientes de definición

- Estados y transiciones exactas de una diferencia de conciliación.
- Condiciones que permiten reabrir o corregir una resolución ya registrada.
- Frecuencia de consulta automática a la pasarela, tratada en decisiones posteriores.

Estos puntos requieren aprobación funcional.

## Trazabilidad

- DEC-082–DEC-085.
- RN-0583–RN-0623.
- `MATERIAL_REVIEW_05.md`.
