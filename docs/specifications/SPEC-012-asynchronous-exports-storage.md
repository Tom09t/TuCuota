# SPEC-012 — Exportaciones asíncronas y almacenamiento seguro

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir el procesamiento, retención, descarga, almacenamiento y recuperación segura de archivos exportados.

## Procesamiento

- Las exportaciones pequeñas se generan y descargan inmediatamente; las grandes se procesan en segundo plano según su volumen estimado.
- Cada solicitud tiene identificador único y estados *pendiente*, *en proceso*, *completada* o *fallida*.
- El usuario puede seguir usando la plataforma y consultar el historial de sus solicitudes.
- Se registran solicitante, filtros, formato, fechas y resultados, respetando club y disciplina.
- Una solicitud fallida puede reintentarse sin duplicar registros financieros y conservando su trazabilidad.

## Retención

- El superadministrador configura un plazo global en días, contado desde la generación exitosa.
- Al vencer, el archivo se elimina automáticamente y deja de ofrecerse para descarga; la solicitud y su auditoría permanecen.
- La eliminación no afecta datos financieros originales y registra fecha, archivo y resultado.
- El usuario puede solicitar una nueva exportación con permisos y filtros vigentes.

## Autorización de descarga

- Los archivos no son exclusivos de quien los generó, pero solo usuarios autenticados y actualmente autorizados pueden descargarlos.
- Administradores acceden a su disciplina; delegados, a las disciplinas de su club en lectura; superadministrador, globalmente.
- Cada descarga revalida permisos, ámbito y contenido. Perder permisos revoca el acceso aunque el archivo siga vigente.
- Los archivos vencidos no pueden descargarse.
- Se registran descargas autorizadas y rechazadas con usuario, fecha, hora, archivo y resultado.

## Enlaces y almacenamiento

- Cada descarga autorizada usa un enlace temporal protegido, de expiración corta y vinculado con la autorización del solicitante.
- Conocer el enlace no habilita a un tercero; enlaces vencidos o permisos revocados son rechazados.
- Los archivos se almacenan cifrados en repositorios privados con segregación lógica por club y disciplina.
- No existe acceso directo al almacenamiento; el backend autoriza cada entrega.
- Claves y credenciales se gestionan separadas del contenido y nunca se exponen en enlaces o respuestas públicas.

## Recuperación de exportaciones

- El superadministrador configura globalmente máximo e intervalo de reintentos.
- Las tareas grandes con errores recuperables o interrumpidas pueden reanudarse manteniendo el identificador original.
- Los reintentos no generan archivos finales duplicados y registran fecha, hora, resultado y causa.
- Al agotarse, la solicitud queda *fallida*. Un usuario autorizado puede iniciar una nueva ejecución con permisos y filtros revalidados.
- Los archivos incompletos nunca están disponibles para descarga.

## Invariantes

- Generar un archivo no concede acceso permanente a quien pierde permisos.
- Ningún enlace es público ni permite cruzar ámbitos.
- Eliminar el archivo temporal no elimina datos financieros ni auditoría.
- Un reintento conserva la solicitud y no produce archivos finales duplicados.
- Un archivo incompleto, vencido o fuera de ámbito no se descarga.

## Pendientes de definición

- Umbral que separa exportaciones pequeñas de grandes.
- Plazo global de retención y expiración técnica de los enlaces.
- Rangos válidos de máximo e intervalo de reintentos.

Estos valores requieren aprobación funcional o técnica.

## Trazabilidad

- DEC-097–DEC-102.
- RN-0740–RN-0813.
- `MATERIAL_REVIEW_06.md`.
