# SPEC-020 — Evidencia financiera

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir conservación, acceso seguro y versionado inmutable de archivos usados como evidencia financiera.

## Conservación

- El superadministrador configura globalmente el período en años; clubes y disciplinas no lo modifican.
- Cada evidencia conserva fecha de incorporación y operación financiera asociada.
- Al cumplir el plazo puede eliminarse automáticamente solo si no existe obligación legal, contractual o de preservación.
- Evidencias de disputas, investigaciones o revisiones abiertas permanecen protegidas y se reevalúan al finalizar la causa.
- Eliminar el archivo no elimina la operación, la resolución excepcional ni sus auditorías; se registra su existencia y eliminación.
- La política respeta los mínimos legales aplicables y audita cambios de configuración.

## Almacenamiento y acceso

- Los archivos permanecen cifrados en repositorio privado, con aislamiento lógico por club y disciplina.
- Toda consulta o descarga valida identidad y permisos vigentes sobre club, disciplina y operación.
- No existen enlaces públicos permanentes. Los accesos temporales vencen y una nueva solicitud revalida autorización.
- Consultas y descargas se auditan con usuario, fecha, hora, archivo y operación.
- Los documentos mantienen su relación con el pago incierto y su resolución.
- La eliminación conserva auditoría y los accesos usan comunicaciones seguras.

## Inmutabilidad y versiones

- Un archivo cargado es inmutable y no puede sobrescribirse.
- Un usuario autorizado puede incorporar una versión corregida con motivo obligatorio.
- Cada versión tiene identificador, actor, fecha y hora y se vincula con la original.
- La anterior queda marcada como reemplazada, pero sigue consultable mientras deba conservarse.
- Retención, preservación y controles de acceso se aplican a todas las versiones.
- Una corrección no modifica automáticamente una resolución financiera. Si contradice su evidencia, se señala para revisión.

## Invariantes

- Preservación abierta prevalece sobre vencimiento de retención.
- El contenido de una evidencia nunca se sobrescribe.
- Eliminar un archivo no elimina la historia financiera ni su auditoría.
- Un enlace temporal no sustituye autenticación y autorización.
- Corregir evidencia no reescribe silenciosamente una resolución.

## Pendientes de definición

- Período global de conservación y obligaciones legales concretas.
- Formatos, tamaño máximo y controles de seguridad de archivos admitidos.
- Duración de los accesos temporales.

Estos puntos requieren aprobación legal, funcional o técnica.

## Trazabilidad

- DEC-140–DEC-142.
- RN-1351–RN-1400.
- `MATERIAL_REVIEW_07.md`.
