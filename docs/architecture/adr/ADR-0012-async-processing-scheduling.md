# ADR-0012 — Procesamiento asíncrono y tareas programadas

**Estado:** Aceptada  
**Fecha:** 2026-11-26

## Contexto

Generación de cuotas, conciliaciones, exportaciones, notificaciones, retención y reintentos deben sobrevivir reinicios y despliegues. Ejecutarlos sólo en la API o en memoria no satisface la recuperación ni la idempotencia exigidas por ADR-0010 y ADR-0011.

## Alternativas consideradas

### Ejecutar todo dentro de la API

Reduce componentes, pero mezcla carga web con tareas, y los reinicios o el escalado de la API afectan el procesamiento.

### Tareas y colas sólo en memoria

Son simples, pero pierden trabajo ante reinicios y no sirven para procesos financieros o exportaciones confiables.

### Broker externo desde el inicio

Desacopla y escala, pero agrega infraestructura y operación no justificadas para el MVP.

### Framework integral de jobs

Puede incluir dashboard y reintentos, pero aumenta dependencia y puede exigir un proveedor PostgreSQL adicional.

### Worker separado con PostgreSQL durable y scheduler persistente

Mantiene el monolito, separa carga, sobrevive reinicios y no exige un broker inicial.

## Decisión

Agregar `TuCuota.Worker` como segundo host de la misma solución y versión.

- API y worker comparten módulos, contratos y persistencia.
- Se ejecutan como procesos o contenedores separados.
- Pueden escalar de forma independiente sin constituir microservicios.
- El worker no expone una API funcional pública.
- La lógica de dominio permanece en los módulos.

Utilizar:

1. `BackgroundService` de .NET 10 para consumidores de outbox, inbox y trabajos persistidos.
2. Quartz.NET con ADO Job Store en PostgreSQL para calendarios y disparadores.
3. Tablas funcionales para procesos que conservan estado propio.
4. Ningún broker externo inicialmente.

La versión mayor de Quartz se fija centralmente tras verificar compatibilidad con .NET 10 y Npgsql.

## Procesos asíncronos

| Proceso | Disparador |
|---|---|
| Generación automática de cuotas | Calendario funcional |
| Conciliación automática | Calendario configurable |
| Reconsulta de pagos pendientes | Trabajo persistido |
| Exportaciones grandes | Solicitud funcional |
| Notificaciones y proyecciones | Outbox |
| Alertas financieras y contractuales | Calendario y estado |
| Limpieza por retención | Calendario |
| Reintentos de integración | Política de recuperación |
| Migración o importación | Ejecución autorizada |

Backups, despliegues, rotación de secretos y monitoreo externo pertenecen a la plataforma, no a trabajos funcionales internos.

## Persistencia

Cada trabajo conserva identificador, tipo y versión, ámbito, referencia funcional, estado, prioridad, disponibilidad, intentos, próximo intento, lease, propietario, vencimiento, correlación, causalidad, fechas y resultado o error sanitizado.

No se serializan agregados completos ni secretos. El worker recarga datos, permisos y configuración vigente antes de ejecutar.

## Reclamación

- Selección con `FOR UPDATE SKIP LOCKED` sobre tablas con semántica de cola.
- Lease temporal identificado por worker.
- Heartbeat sólo para trabajos largos.
- Recuperación después del vencimiento del lease.
- Concurrencia limitada por tipo.
- Procesamiento tenant-aware.
- Transacción corta al reclamar y otra al confirmar.

Un lease vencido no demuestra que una operación externa haya fallado. Antes de repetirla se verifica estado e idempotencia.

## Quartz

Quartz administra calendario y creación o activación de trabajos, no reglas financieras.

- ADO Job Store persistente en PostgreSQL.
- Clustering cuando exista más de una instancia.
- Identidad única por instancia.
- Sin datos binarios de dominio en `JobDataMap`.
- Calendarios y zonas explícitos.
- Misfire definido por tarea.
- Jobs delgados que delegan en módulos.
- Cada disparo crea una ejecución funcional idempotente.

## Solapamiento

| Tipo | Política |
|---|---|
| Conciliación global | No solapar ejecuciones incompatibles |
| Conciliación por ámbito | Paralelismo entre ámbitos y exclusión dentro del mismo |
| Generación de cuotas | Una ejecución por período, club y disciplina |
| Exportaciones | Paralelismo limitado |
| Limpieza | Una ejecución global por clase de dato |
| Reconsulta de pagos | Exclusión por operación |
| Proyecciones y notificaciones | Partición por mensaje y consumidor |

Las exclusiones usan claves funcionales y restricciones persistidas, no memoria local.

## Tiempo y calendarios

- Los instantes se almacenan en UTC.
- Los calendarios usan zona IANA explícita.
- Se registra instante programado, real y zona aplicada.
- Los cambios de frecuencia son prospectivos y auditados.
- Horas y zonas funcionales pendientes permanecen sujetas a sus SPEC.
- Misfires, horario de verano y reinicios no producen dos ejecuciones para la misma clave funcional.

## Reintentos

- Errores transitorios: intentos limitados con backoff y jitter.
- Rate limit externo: respetar `Retry-After`.
- Error funcional definitivo: sin reintento automático.
- Resultado externo incierto: proteger y consultar o conciliar.
- Error no clasificado: política conservadora y revisión.
- Agotamiento: estado fallido, alerta y operación manual autorizada.

Los intentos reutilizan la clave idempotente y no crean otro resultado funcional. Los trabajos agotados no se eliminan silenciosamente.

## Apagado y despliegue

- Al cancelar, el worker deja de reclamar trabajos.
- Los handlers respetan `CancellationToken`.
- Los trabajos breves pueden finalizar dentro de la gracia.
- Los largos guardan progreso o permiten recuperar el lease.
- Un cierre abrupto se recupera mediante lease.
- API y worker se despliegan con versiones compatibles.
- Las migraciones preceden a las versiones que las necesitan.
- Un despliegue no presume el resultado de trabajos en curso.

## Observabilidad mínima

Se miden trabajos por estado, antigüedad, retraso, duración, intentos, agotamiento, leases vencidos, backlog de outbox e inbox, diferencias pendientes, exportaciones detenidas y resultados inciertos.

## Evolución

Un broker requiere un ADR posterior y evidencia de contención en PostgreSQL, desacoplamiento de despliegues, volumen, latencia, disponibilidad independiente o integración externa.

## Consecuencias

- Existe un segundo host ejecutable dentro del mismo monolito.
- Los trabajos sobreviven reinicios y despliegues.
- PostgreSQL funciona inicialmente como almacenamiento durable y cola.
- Quartz administra calendarios, no dominio.
- API y worker requieren despliegue y monitoreo coordinados.
- El broker externo queda diferido.
- Esta decisión no genera código productivo.

## Trazabilidad

- Generación de cuotas: `SPEC-003`, AC-003-09, AC-003-10.
- Conciliación: `SPEC-009`–`SPEC-011`, `ACCEPTANCE-009`–`ACCEPTANCE-011`.
- Exportaciones: `SPEC-012`, `ACCEPTANCE-012`.
- Notificaciones: `SPEC-013`–`SPEC-015`, `ACCEPTANCE-013`–`ACCEPTANCE-015`.
- Pasarelas y pagos inciertos: `SPEC-018`–`SPEC-019`, `ACCEPTANCE-018`–`ACCEPTANCE-019`.
- Evidencia y retención: `SPEC-020`, `SPEC-026`.
- Devoluciones y disputas: `SPEC-021`–`SPEC-027`, `ACCEPTANCE-021`–`ACCEPTANCE-027`.
- MVP y automatizaciones: `SPEC-028`, `ACCEPTANCE-028`.
- Despliegues: `SPEC-030`, `ACCEPTANCE-030`.
- Monitoreo: `SPEC-032`, `ACCEPTANCE-032`.
- Importación: `SPEC-033`, `ACCEPTANCE-033`.
- Preparación productiva: `SPEC-034`, `ACCEPTANCE-034`.
- Transacciones e idempotencia: ADR-0010 y ADR-0011.

## Referencias técnicas

- [Worker Services de .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/workers).
- [`BackgroundService`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.backgroundservice).
- [ADO Job Store y clustering de Quartz.NET](https://www.quartz-scheduler.net/documentation/quartz-3.x/configuration/reference.html).

