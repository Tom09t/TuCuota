# ACCEPTANCE-015 — Entrega y sincronización de notificaciones

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-015`

## Escenarios

### AC-015-01 — Consulta paginada

**Dado** un historial visible extenso  
**Cuando** el usuario navega sus páginas o aplica filtros  
**Entonces** solo obtiene notificaciones actualmente autorizadas  
**Y** no cambia su lectura ni resolución.

### AC-015-02 — Entrega conectada

**Dado** un usuario autenticado y conectado  
**Cuando** recibe una nueva notificación  
**Entonces** aparece sin recargar la página y actualiza el contador.

### AC-015-03 — Usuario desconectado

**Dado** un destinatario sin conexión  
**Cuando** se genera una notificación  
**Entonces** queda almacenada y aparece al recuperar la sesión.

### AC-015-04 — Respaldo del canal

**Dado** un fallo del canal en tiempo real  
**Cuando** el usuario continúa conectado  
**Entonces** la consulta periódica recupera novedades sin impedir otras funciones.

### AC-015-05 — Reconexión idempotente

**Dado** una sesión temporalmente desconectada  
**Cuando** se reconecta  
**Entonces** sincroniza notificaciones y estados vigentes sin duplicarlos.

### AC-015-06 — Lectura entre sesiones

**Dado** dos sesiones de la misma cuenta  
**Cuando** una marca una notificación o todas como leídas  
**Entonces** la otra refleja estados y contador consistentes.

### AC-015-07 — Preferencias entre sesiones

**Dado** varias sesiones de una cuenta  
**Cuando** una modifica preferencias informativas  
**Entonces** las demás reciben la configuración central vigente.

### AC-015-08 — Correo compartido

**Dado** dos cuentas independientes con el mismo correo  
**Cuando** una recibe o lee notificaciones  
**Entonces** la otra cuenta no comparte esos datos ni estados.

### AC-015-09 — Aviso crítico

**Dado** una notificación crítica nueva  
**Cuando** se entrega al destinatario  
**Entonces** muestra un aviso destacado no bloqueante con identificación del problema.

### AC-015-10 — Notificación informativa

**Dado** una notificación informativa  
**Cuando** se entrega  
**Entonces** actualiza centro y contador sin aviso emergente.

### AC-015-11 — Cierre del aviso

**Dado** un aviso crítico visible  
**Cuando** el usuario lo cierra  
**Entonces** la notificación permanece disponible y no cambia su lectura ni la resolución.

### AC-015-12 — Aviso idempotente

**Dado** un incidente crítico ya avisado  
**Cuando** se reintenta su procesamiento  
**Entonces** no aparece otro aviso equivalente.

### AC-015-13 — Revocación inmediata

**Dado** un usuario con una sesión abierta  
**Cuando** pierde permisos sobre un ámbito  
**Entonces** deja de recibir y consultar sus notificaciones, incluso mediante enlaces anteriores.

### AC-015-14 — Revocación parcial

**Dado** permisos sobre varias disciplinas  
**Cuando** se revoca solo una  
**Entonces** contadores y búsquedas excluyen esa disciplina  
**Y** permanecen los accesos autorizados restantes.

### AC-015-15 — Recuperación de permisos

**Dado** un usuario que recupera un ámbito  
**Cuando** vuelve a consultar  
**Entonces** el acceso se evalúa con rol y permisos actuales  
**Y** no reaparecen notificaciones cuya visibilidad ya venció.

## Pendientes no aceptables todavía

No pueden cerrarse criterios tecnológicos, frecuencia de respaldo, tamaño de página ni presentación visual exacta hasta contar con definiciones aprobadas.

## Trazabilidad

Cada escenario deriva de `SPEC-015`, DEC-112–DEC-116 y RN-0940–RN-0999.
