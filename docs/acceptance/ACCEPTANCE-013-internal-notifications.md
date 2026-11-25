# ACCEPTANCE-013 — Notificaciones internas y preferencias

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-013`

## Escenarios

### AC-013-01 — Exportación completada

**Dado** una exportación asíncrona  
**Cuando** se completa correctamente  
**Entonces** el solicitante recibe una notificación interna no leída con su estado final.

### AC-013-02 — Exportación fallida

**Dado** una exportación que agota sus reintentos  
**Cuando** queda fallida  
**Entonces** el solicitante recibe una notificación interna sin archivo adjunto ni correo.

### AC-013-03 — Notificación idempotente

**Dado** un resultado final ya notificado  
**Cuando** se reprocesan eventos técnicos  
**Entonces** no se crea una notificación duplicada.

### AC-013-04 — Acceso desde notificación

**Dado** una notificación de exportación  
**Cuando** el usuario accede al centro de exportaciones  
**Entonces** se revalidan sus permisos antes de mostrar o descargar información.

### AC-013-05 — Archivo vencido

**Dado** una notificación antigua cuyo archivo fue eliminado  
**Cuando** se intenta descargar desde ella  
**Entonces** la descarga es rechazada.

### AC-013-06 — Lectura individual y masiva

**Dado** notificaciones no leídas  
**Cuando** el usuario marca una o todas como leídas  
**Entonces** se actualizan sus estados y el contador personal.

### AC-013-07 — Independencia de lectura

**Dado** una notificación vinculada con una exportación  
**Cuando** se marca como leída  
**Entonces** no cambia la exportación ni la disponibilidad del archivo.

### AC-013-08 — Privacidad personal

**Dado** dos usuarios  
**Cuando** uno intenta consultar o modificar notificaciones privadas del otro  
**Entonces** el backend rechaza la operación.

### AC-013-09 — Retiro por visibilidad

**Dado** una notificación que supera el plazo global  
**Cuando** se ejecuta la limpieza  
**Entonces** se retira del centro y del contador  
**Y** se conservan exportación y auditoría.

### AC-013-10 — Preferencia informativa

**Dado** una categoría informativa  
**Cuando** un usuario la desactiva  
**Entonces** la preferencia solo afecta sus avisos futuros y no modifica historiales.

### AC-013-11 — Categoría crítica

**Dado** una categoría crítica aplicable al usuario  
**Cuando** intenta desactivarla  
**Entonces** el sistema mantiene su obligatoriedad.

### AC-013-12 — Reclasificación a crítica

**Dado** una categoría informativa desactivada por un usuario  
**Cuando** el superadministrador la vuelve crítica  
**Entonces** queda obligatoria inmediatamente  
**Y** se conserva la preferencia previa.

### AC-013-13 — Retorno a informativa

**Dado** una categoría crítica antes informativa  
**Cuando** vuelve a clasificarse como informativa  
**Entonces** se restaura la preferencia individual conservada.

### AC-013-14 — Desactivación protegida

**Dado** una categoría necesaria para avisos críticos  
**Cuando** el superadministrador intenta desactivarla  
**Entonces** el sistema rechaza la acción.

### AC-013-15 — Historia del catálogo

**Dado** una categoría modificada o desactivada  
**Cuando** se consultan notificaciones anteriores  
**Entonces** permanecen intactas y el cambio conserva responsable, fecha, hora y valores.

## Pendientes no aceptables todavía

No pueden cerrarse criterios cuantitativos de visibilidad ni el catálogo inicial completo y su navegación hasta contar con definición aprobada.

## Trazabilidad

Cada escenario deriva de `SPEC-013`, DEC-103–DEC-108 y RN-0814–RN-0888.
