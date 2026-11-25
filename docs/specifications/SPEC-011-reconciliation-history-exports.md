# SPEC-011 — Recuperación, historial y exportación de conciliaciones

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir reintentos de conciliación, recuperación de errores, conservación y archivo del historial, consulta unificada y exportación.

## Reintentos y recuperación

- El superadministrador configura globalmente cantidad máxima e intervalo de reintentos, respetando límites de la pasarela.
- Solo se reprocesan operaciones fallidas o pendientes; los resultados exitosos se conservan.
- Cada intento registra fecha, hora, operación, resultado y error, sin duplicar conciliaciones ni modificar silenciosamente pagos.
- Al agotarse los reintentos, la operación queda *pendiente por error* y se alerta internamente al superadministrador y administradores afectados, sin correo.
- La alerta identifica operaciones, fecha y motivo; desaparece de la vista activa tras la recuperación, conservando historial.
- Los usuarios autorizados pueden recuperar manualmente según su ámbito, evitando ejecuciones incompatibles y resultados duplicados.

## Conservación y archivo

- Se conserva el historial completo de ejecuciones, diferencias, resoluciones, errores, reintentos, recuperaciones y acciones de usuario.
- No se elimina automáticamente por antigüedad. Puede archivarse sin pérdida de registros ni trazabilidad.
- El superadministrador configura globalmente la antigüedad mínima en meses; el archivo se ejecuta automáticamente.
- No se archivan conciliaciones o diferencias todavía pendientes de resolución.
- Los registros archivados mantienen permisos y siguen disponibles para consulta autorizada.
- Las obligaciones legales y de protección de datos prevalecen cuando exijan eliminación o anonimización.

## Consulta unificada

- Un único módulo consulta registros recientes y archivados.
- Permite filtrar por período, club, disciplina, estado e identificador de operación dentro del ámbito autorizado.
- El sistema localiza y recupera automáticamente información operativa o archivada y avisa si la consulta histórica demorará más.
- Administradores consultan su disciplina; delegados, las disciplinas de su club en lectura; superadministrador, toda la plataforma.
- Consultar no modifica registros y conserva operaciones, diferencias, resoluciones y auditoría.

## Exportación

- Los registros recientes y archivados pueden exportarse en `.xlsx` y PDF con los mismos filtros y permisos de consulta.
- Se incluyen operaciones, diferencias e importes, resoluciones, motivos, responsables, ejecuciones, reintentos y modificaciones.
- Cada exportación registra usuario, fecha, hora, formato y alcance; su auditoría permanece disponible.
- Exportar datos archivados no los modifica. Si requiere recuperar gran volumen, se informa el estado de procesamiento.

## Invariantes

- Un reintento nunca reprocesa innecesariamente una operación ya conciliada.
- Archivar no significa eliminar ni relajar permisos.
- Consulta y exportación no modifican estados financieros.
- La recuperación manual respeta club, disciplina y las restricciones de concurrencia.
- Las obligaciones legales prevalecen sobre la conservación ordinaria.

## Pendientes de definición

- Rangos válidos para máximo e intervalo de reintentos.
- Estados detallados y mecanismo de seguimiento de una exportación histórica en procesamiento.
- Política legal concreta de retención, eliminación y anonimización aplicable.

Estos puntos requieren aprobación funcional, legal o técnica según corresponda.

## Trazabilidad

- DEC-091–DEC-096.
- RN-0673–RN-0739.
- `MATERIAL_REVIEW_06.md`.
