# SPEC-014 — Eventos e incidentes notificables

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir la asociación de eventos reales con categorías y destinatarios, la agrupación idempotente por incidente y su consulta histórica.

## Catálogo de eventos

- Solo eventos implementados en la plataforma pueden configurarse como notificables.
- El superadministrador asocia cada evento con una categoría activa y roles destinatarios.
- Los destinatarios se determinan por rol y ámbito: administrador por disciplina, delegado por club y superadministrador globalmente.
- Los destinatarios obligatorios de eventos críticos no pueden eliminarse.
- Asociar un evento no amplía permisos financieros o personales.
- Los cambios rigen para eventos futuros, respetan clasificación y preferencias, y auditan usuario, fecha, hora y valores.

## Incidentes e idempotencia

- Cada incidente notificable tiene identificador único y vincula sus notificaciones.
- Existe como máximo una notificación activa por incidente y destinatario, incluso con concurrencia o reintentos.
- Nuevas ocurrencias del mismo incidente actualizan información y estado; un incidente independiente puede generar otra notificación de igual categoría.
- Se conservan intentos, errores y registros técnicos; agrupar no modifica pagos, conciliaciones ni exportaciones.
- Toda agrupación respeta destinatario y ámbito.

## Resolución y lectura

- Las notificaciones de incidentes tienen estado de resolución.
- Al resolverse el incidente, sus notificaciones pasan automáticamente a *resuelta*, salen de la vista activa y permanecen visibles en historial hasta su retención.
- La resolución registra fecha y hora y no genera una segunda notificación.
- Lectura y resolución son independientes por destinatario: una notificación puede estar resuelta y no leída.
- Resolver no marca como leída ni altera datos financieros u operaciones originales.

## Consulta

- El usuario consulta únicamente su historial visible, ordenado desde lo más reciente.
- Puede filtrar por lectura, incidente activo o resuelto, categoría, clasificación, rango de fechas y ámbitos autorizados.
- Puede buscar por texto visible o identificador de incidente.
- Los filtros de club y disciplina solo incluyen ámbitos actualmente autorizados.

## Invariantes

- No puede configurarse un evento inexistente en el software.
- Incidente, resolución y lectura son estados independientes.
- La idempotencia se aplica por incidente y destinatario.
- Una asociación de notificación nunca concede acceso al recurso subyacente.
- Agrupar o resolver no modifica operaciones originales.

## Pendientes de definición

- Criterio funcional que determina si dos ocurrencias pertenecen al mismo incidente.
- Fuentes y actores autorizados para declarar resuelto cada tipo de incidente.
- Campos de texto incluidos en la búsqueda.

Estos puntos requieren aprobación funcional.

## Trazabilidad

- DEC-109–DEC-112.
- RN-0889–RN-0939.
- `MATERIAL_REVIEW_06.md`.
