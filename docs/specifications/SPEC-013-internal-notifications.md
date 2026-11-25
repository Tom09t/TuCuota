# SPEC-013 — Notificaciones internas y preferencias

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir las notificaciones internas de exportaciones, su lectura y retención, y la administración de categorías críticas e informativas.

## Eventos de exportación

- Una exportación asíncrona notifica al solicitante cuando se completa o falla definitivamente tras agotar reintentos.
- La notificación es interna, identifica exportación y estado final, y permite acceder al centro de exportaciones.
- No se envía correo ni se adjuntan archivos Excel o PDF.
- El acceso revalida permisos; una notificación antigua no permite descargar archivos vencidos o eliminados.
- Los reintentos no duplican la notificación del mismo resultado y todos los eventos conservan trazabilidad.

## Centro personal

- Cada usuario dispone de notificaciones privadas asociadas con su cuenta y la exportación correspondiente.
- Una notificación nace *no leída* y puede marcarse individualmente o en conjunto como *leída*.
- Se muestra un contador de no leídas y se permite consultar historial visible.
- Ningún usuario consulta o modifica notificaciones privadas de otro.
- Leer no cambia la exportación, no elimina ni extiende el archivo y no altera su auditoría.

## Visibilidad y retención

- El superadministrador configura un plazo global en días desde la creación.
- Al vencer, notificaciones leídas o no leídas se retiran del centro y dejan de contar como pendientes.
- La limpieza no afecta exportaciones, archivos, finanzas ni auditorías y queda registrada.
- La visibilidad de notificaciones es independiente de la retención de archivos exportados.

## Clasificación y preferencias

- Las categorías son críticas o informativas.
- Las críticas son obligatorias para destinatarios autorizados y no pueden desactivarse individualmente.
- Cada usuario puede activar o desactivar categorías informativas sin afectar a otros usuarios ni los historiales.
- Solo el superadministrador modifica la clasificación global; los avisos definidos como internos nunca se convierten en correo.
- Reclasificar a crítica tiene efecto inmediato y prevalece sobre preferencias, que se conservan. Si vuelve a informativa, se restauran.
- La reclasificación solo afecta notificaciones futuras, no amplía permisos y queda auditada.

## Catálogo global

- Existe un catálogo con categorías predefinidas asociadas a eventos reales.
- Solo el superadministrador crea, edita o desactiva categorías y registra valores anterior y nuevo.
- No puede desactivarse una categoría si impide una notificación crítica obligatoria.
- Una categoría desactivada deja de generar avisos nuevos, salvo obligación crítica, sin borrar históricos.

## Invariantes

- Notificación, exportación y archivo tienen estados y retenciones independientes.
- Una notificación nunca omite autorización ni concede acceso al archivo.
- Las preferencias informativas son personales; las críticas prevalecen.
- No se envía correo para eventos definidos como exclusivamente internos.
- Cambiar el catálogo no reescribe notificaciones históricas.

## Pendientes de definición

- Plazo global de visibilidad.
- Categorías predefinidas, eventos asociados y clasificación inicial completa.
- Comportamiento de navegación cuando la exportación existe pero el archivo venció.

Estos puntos requieren aprobación funcional.

## Trazabilidad

- DEC-103–DEC-108.
- RN-0814–RN-0888.
- `MATERIAL_REVIEW_06.md`.
