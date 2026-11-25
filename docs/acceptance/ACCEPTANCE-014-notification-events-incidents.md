# ACCEPTANCE-014 — Eventos e incidentes notificables

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-014`

## Escenarios

### AC-014-01 — Evento implementado

**Dado** un evento existente en la plataforma  
**Cuando** el superadministrador lo asocia con categoría activa y roles  
**Entonces** los eventos futuros generan notificaciones según esa configuración.

### AC-014-02 — Evento inexistente

**Dado** una categoría de notificación  
**Cuando** se intenta asociarla con un evento no implementado  
**Entonces** el sistema rechaza la configuración.

### AC-014-03 — Destinatarios por ámbito

**Dado** un evento de una disciplina  
**Cuando** se determinan destinatarios  
**Entonces** solo reciben aviso los roles configurados con acceso a ese ámbito.

### AC-014-04 — Destinatario crítico

**Dado** un destinatario obligatorio de evento crítico  
**Cuando** se intenta eliminarlo de la configuración  
**Entonces** el sistema rechaza el cambio.

### AC-014-05 — Cambio prospectivo

**Dado** una asociación modificada  
**Cuando** se confirma  
**Entonces** se aplica a eventos futuros  
**Y** no modifica notificaciones históricas.

### AC-014-06 — Primera ocurrencia

**Dado** un incidente nuevo  
**Cuando** ocurre un evento notificable  
**Entonces** se crea como máximo una notificación activa para cada destinatario autorizado.

### AC-014-07 — Reintento del mismo incidente

**Dado** un incidente ya notificado  
**Cuando** se reintenta o procesa concurrentemente la misma ocurrencia  
**Entonces** no se crea una notificación duplicada.

### AC-014-08 — Nueva ocurrencia relacionada

**Dado** un incidente activo  
**Cuando** aparece otra ocurrencia del mismo incidente  
**Entonces** se actualizan su información y estado conservando intentos y errores.

### AC-014-09 — Incidente independiente

**Dado** una categoría con un incidente activo  
**Cuando** ocurre otro incidente independiente  
**Entonces** puede generar su propia notificación.

### AC-014-10 — Resolución automática

**Dado** un incidente con notificaciones activas  
**Cuando** el incidente se resuelve  
**Entonces** sus notificaciones pasan a resueltas y salen de la vista activa  
**Y** se registra fecha y hora sin crear otra notificación.

### AC-014-11 — Lectura independiente

**Dado** una notificación no leída  
**Cuando** su incidente se resuelve  
**Entonces** permanece no leída en el historial hasta que el destinatario la lea o expire.

### AC-014-12 — Preservación operativa

**Dado** una notificación agrupada o resuelta  
**Cuando** cambia su estado  
**Entonces** no se modifican pagos, conciliaciones ni exportaciones originales.

### AC-014-13 — Filtros autorizados

**Dado** un usuario con varios ámbitos  
**Cuando** filtra por lectura, resolución, categoría, clasificación, fecha, club o disciplina  
**Entonces** solo obtiene notificaciones visibles dentro de sus permisos.

### AC-014-14 — Búsqueda y orden

**Dado** el historial visible  
**Cuando** se busca por texto o identificador  
**Entonces** se muestran coincidencias ordenadas desde la más reciente.

## Pendientes no aceptables todavía

No pueden cerrarse criterios sobre identidad de incidentes, autoridad de resolución ni campos de búsqueda hasta contar con definición aprobada.

## Trazabilidad

Cada escenario deriva de `SPEC-014`, DEC-109–DEC-112 y RN-0889–RN-0939.
