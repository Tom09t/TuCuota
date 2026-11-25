# SPEC-015 — Entrega y sincronización de notificaciones

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir paginación, entrega en tiempo real, sincronización entre sesiones, avisos críticos y revocación inmediata del acceso a notificaciones.

## Consulta paginada

- Los resultados se paginan y todos los filtros y búsquedas revalidan permisos vigentes.
- No se exponen notificaciones de otros destinatarios ni las retiradas por retención.
- Filtrar o buscar no cambia lectura ni resolución.

## Entrega y recuperación

- Los usuarios conectados reciben nuevas notificaciones y cambios de estado sin recargar la página.
- El contador se actualiza automáticamente y las notificaciones se almacenan aunque el usuario esté desconectado.
- Una desconexión no pierde eventos; al reconectar se sincronizan datos y estados sin duplicados.
- Si falla el canal en tiempo real, una consulta periódica actúa como respaldo sin bloquear otras funciones.
- Solo conexiones autenticadas y autorizadas reciben actualizaciones; errores de entrega y sincronización quedan registrados.

## Estado por cuenta

- Lectura y preferencias se almacenan centralmente por cuenta, no por dispositivo.
- Todas las sesiones de la misma cuenta comparten lectura, acciones masivas, contador y preferencias.
- Cuentas independientes no comparten notificaciones aunque utilicen el mismo correo.
- Sincronizar no modifica historiales financieros ni auditorías.

## Avisos críticos

- Una notificación crítica genera un aviso visual destacado no bloqueante, con información suficiente y acceso autorizado al incidente.
- Las informativas actualizan centro y contador sin aviso emergente.
- Cerrar el aviso no elimina ni lee la notificación y no resuelve el incidente.
- Reintentos del mismo incidente no duplican avisos; un incidente independiente sí puede generar otro.
- La presentación ocurre dentro de la plataforma y no depende de permisos de notificación del navegador.

## Revocación de acceso

- Perder permisos detiene inmediatamente nuevas entregas y el acceso histórico del ámbito, incluso en sesiones abiertas y enlaces anteriores.
- Cada consulta revalida permisos; contadores, filtros y búsquedas excluyen información revocada.
- La revocación no elimina registros ni auditorías y no afecta otros ámbitos todavía autorizados.
- Si se recuperan permisos, el acceso se reevalúa según rol y ámbito vigentes, sin restaurar notificaciones ya vencidas.
- Los cambios de permisos y revocaciones quedan auditados.

## Invariantes

- La persistencia es la fuente de recuperación; el canal en tiempo real no es la única copia.
- El estado pertenece a la cuenta, no al correo ni al dispositivo.
- Cerrar un aviso visual no equivale a leer o resolver.
- Una sesión abierta no conserva acceso después de una revocación.
- Sincronizar nunca altera operaciones financieras originales.

## Pendientes de definición

- Tecnología y protocolo de entrega en tiempo real.
- Intervalo y estrategia de la consulta periódica de respaldo.
- Tamaño de página y tratamiento visual exacto de avisos críticos.

Estos puntos requieren definición técnica o funcional posterior.

## Trazabilidad

- DEC-112–DEC-116.
- RN-0940–RN-0999.
- `MATERIAL_REVIEW_06.md`.
