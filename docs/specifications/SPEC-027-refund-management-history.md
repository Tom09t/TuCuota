# SPEC-027 — Gestión, historial y bandeja de devoluciones

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir el tratamiento de información complementaria, la consulta histórica del responsable y la bandeja administrativa de solicitudes de devolución.

## Información complementaria

- El administrador de disciplina puede solicitar información por canales externos usando datos de contacto autorizados; el expediente no incorpora chat ni mensajería entre las partes.
- No se exige un estado específico de «Esperando información del responsable».
- El administrador puede registrar observaciones internas e incorporar comprobantes recibidos externamente.
- Los comprobantes quedan sujetos a los controles de acceso, seguridad, conservación, aislamiento y auditoría de `SPEC-026`.
- La falta de respuesta no produce rechazo automático: el administrador conserva la decisión y respeta los plazos de la pasarela y las obligaciones legales.
- La decisión final, su justificación, la evidencia incorporada y toda modificación relevante quedan auditadas.
- El responsable continúa consultando el estado y recibe las notificaciones de resolución ya definidas.

## Historial del responsable

- El responsable consulta solicitudes y devoluciones para las que mantiene acceso autorizado según la vigencia de su vínculo con el jugador.
- Cada registro muestra disciplina, cuotas, fecha, motivo declarado, importe solicitado, estado actual y resolución comunicable.
- Aprobación administrativa y ejecución efectiva se muestran como hechos distintos.
- Solo una confirmación de la pasarela habilita a mostrar importe efectivamente devuelto; una operación pendiente o incierta nunca aparece como confirmada.
- Se informan los costos no reintegrados que correspondan al pagador y se ofrece el comprobante mediante descarga segura cuando esté disponible.
- Se preserva la trazabilidad de solicitudes canceladas, rechazadas y completadas.
- No se exponen observaciones internas, evidencia reservada, información financiera privada ni solicitudes ajenas.

## Bandeja administrativa

- Una bandeja unificada presenta solicitudes pendientes, aprobadas, rechazadas, canceladas, en proceso y completadas.
- Permite filtrar por estado, rango de fechas y motivo, y buscar por jugador o responsable de pago.
- Prioriza las pendientes; cada fila presenta un resumen y acceso al detalle, con paginación para volúmenes elevados.
- El administrador de disciplina solo consulta y gestiona su disciplina.
- El superadministrador dispone de vista global con filtros por club y disciplina, sin adquirir por ello autorización financiera adicional.
- Las acciones disponibles dependen conjuntamente del estado y de los permisos del usuario.
- La bandeja distingue solicitud aprobada de devolución confirmada y refleja cada cambio en el historial.
- Consultas y acciones respetan aislamiento, autorización, trazabilidad y auditoría.

## Invariantes

- No existe chat interno para devoluciones.
- La falta de respuesta no decide automáticamente el expediente.
- La información interna o reservada nunca integra la vista del responsable.
- Aprobación no equivale a ejecución ni confirmación.
- La vista global no amplía permisos financieros.
- Ninguna búsqueda puede eludir el aislamiento por club y disciplina.

## Pendientes de definición

- Canales externos autorizados y procedimiento operativo para solicitar información adicional.
- Campos exactos del resumen de bandeja y criterio de prioridad entre solicitudes pendientes.
- Tamaño de página, ordenamiento inicial y límites de búsqueda.
- Política temporal de disponibilidad de comprobantes, subordinada a las reglas de conservación aprobadas.

Estos puntos requieren aprobación funcional o técnica posterior y no alteran las reglas vigentes.

## Trazabilidad

- DEC-158–DEC-160.
- RN-1662–RN-1712.
- DEC-157 y `SPEC-026` para seguridad de evidencia.
- `MATERIAL_REVIEW_08.md`.

