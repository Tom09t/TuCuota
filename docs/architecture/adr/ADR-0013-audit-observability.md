# ADR-0013 — Auditoría y observabilidad

**Estado:** Aceptada  
**Fecha:** 2026-11-26

## Contexto

El sistema requiere evidencia funcional de cambios, diagnóstico técnico, monitoreo de procesos financieros y alertas de Producción. Logs y trazas no ofrecen por sí solos la atomicidad, estructura o retención necesarias para auditoría. A la vez, utilizar la auditoría como sistema de telemetría produciría volumen y acoplamiento inadecuados.

## Decisión

Separar auditoría funcional de telemetría operativa.

| Mecanismo | Propósito | Garantía |
|---|---|---|
| Auditoría | Quién hizo qué, sobre qué ámbito y resultado | Persistente, transaccional e inmutable |
| Logs | Diagnóstico de eventos y errores | Estructurados y sujetos a retención |
| Métricas | Salud, volumen, latencia y backlog | Agregadas y de baja cardinalidad |
| Trazas | Recorrido entre API, módulos, worker y proveedor | Correlacionadas y muestreables |
| Alertas | Condiciones que requieren intervención | Dedupe, estado y recuperación |
| Health checks | Capacidad de vivir o recibir trabajo | Señal técnica mínima |

Usar PostgreSQL para auditoría funcional; `ILogger`, `System.Diagnostics.Metrics` y `ActivitySource` para telemetría; OpenTelemetry y OTLP para exportación; y health checks separados para liveness y readiness.

## Auditoría funcional

Cada módulo es propietario de sus eventos y utiliza un sobre común con identificador, instante UTC, actor, clase de identidad, sesión o identidad técnica, club, disciplina, módulo, acción, recurso, resultado, motivo, valores permitidos anterior y nuevo, correlación, causalidad, canal y versión de esquema.

Los detalles son tipados por módulo; no se vuelca arbitrariamente una entidad completa.

### Atomicidad

- La auditoría financiera se guarda en la misma transacción que el cambio.
- Si una acción crítica exige auditoría y no puede persistirse, la acción falla.
- Outbox puede exportar una copia después del commit.
- La falla de logs, métricas o trazas no revierte la operación.
- Rechazos relevantes para seguridad se auditan separadamente.

### Inmutabilidad y acceso

- Los roles normales insertan y consultan, pero no actualizan auditorías.
- Correcciones crean otro evento relacionado.
- La eliminación por retención usa un proceso privilegiado y auditado.
- RLS protege ámbitos.
- La consulta global requiere autorización global.
- Auditorías financieras se incluyen en backup y restauración.
- WORM o firma criptográfica se evaluarán según requisitos legales concretos.

## Datos prohibidos

No se registran contraseñas, hashes, OTP, cookies, tokens, secretos OAuth, claves de API, datos completos de tarjetas, cadenas de conexión, evidencia completa, archivos, headers de autorización ni webhooks completos sin sanitizar.

DNI, correo y teléfono no se usan como dimensiones de métricas. Se emplean identificadores internos y redacción.

## Logs

Los logs son estructurados, con plantillas y `EventId` estables. Incluyen ambiente, servicio, versión, instancia, módulo, correlación, trazas, operación, resultado, duración, ámbito seguro, tipo de error e identificadores técnicos.

Condiciones funcionales esperadas, como duplicados resueltos o conflictos de versión, no se registran automáticamente como errores.

## Trazas

Se propaga contexto W3C por solicitudes, casos de uso, PostgreSQL, outbox, worker, proveedores, webhooks y conciliación.

- Outbox, inbox y jobs conservan correlación y causalidad.
- Se instrumentan PostgreSQL y `HttpClient`.
- Los spans no incluyen datos sensibles.
- Auditoría nunca se muestrea.
- Trazas exitosas pueden muestrearse; errores y operaciones críticas usan una política más conservadora.
- El muestreo no afecta métricas esenciales.

## Métricas

Se observan HTTP, PostgreSQL, pools, runtime, llamadas externas, API, worker, pagos por estado, pagos inciertos, conciliaciones, generación de cuotas, outbox, inbox, jobs, exportaciones, webhooks y backups.

No se utilizan identificadores no acotados de usuario, pago, club o disciplina como labels. El detalle se investiga mediante logs y consultas autorizadas.

## Health checks

- API y worker exponen liveness.
- Readiness valida configuración esencial, PostgreSQL y compatibilidad de migraciones.
- Worker agrega scheduler y capacidad de reclamar trabajos.
- Almacenamiento privado se comprueba cuando es esencial.
- Disponibilidad de Mercado Pago es señal separada y no provoca fallo de liveness.
- Los endpoints no exponen secretos ni detalles internos y pueden restringirse por red o autorización.

## Alertas

Se separan alertas técnicas de notificaciones operativas.

Las alertas técnicas tienen clave de deduplicación, registran inicio, persistencia, cambio y recuperación, evitan repetición sin cambio, excluyen datos sensibles e identifican ambiente, componente y condición.

Las notificaciones operativas continúan perteneciendo al módulo `Notifications` y no son sustituidas por el monitoreo técnico.

## Señales mínimas de Producción

Antes de cobros reales deben existir paneles y alertas para API, PostgreSQL, pagos, incertidumbre, conciliación, cuotas, backups, outbox, inbox, worker, scheduler, webhooks, pasarela, autenticación anómala y restauraciones.

Umbrales y SLO se aprueban antes de Producción; este ADR no los inventa.

## Retención

Auditoría financiera, auditoría de seguridad, logs, métricas, trazas, evidencias y alertas tienen políticas separadas. Reducir retención técnica nunca elimina auditoría normativa ni evidencia financiera.

## Portabilidad

La aplicación exporta OTLP a un collector o backend configurado. No se selecciona todavía Grafana, Prometheus, Loki, Jaeger, Azure Monitor u otro proveedor. Los módulos de dominio no usan APIs propietarias de APM.

## Consecuencias

- Auditoría no depende del backend de observabilidad.
- Se evita acoplamiento temprano a un proveedor APM.
- Evidencia y diagnóstico tienen almacenamiento y retención separados.
- Se controla cardinalidad y costo de métricas.
- Backend, umbrales, SLO y destinatarios permanecen pendientes.
- Esta decisión no genera código productivo.

## Trazabilidad

- Administración y cambios económicos: `SPEC-001`–`SPEC-007`, `ACCEPTANCE-001`–`ACCEPTANCE-007`.
- Reportes y conciliación: `SPEC-008`–`SPEC-011`, `ACCEPTANCE-008`–`ACCEPTANCE-011`.
- Exportaciones: `SPEC-012`, `ACCEPTANCE-012`.
- Notificaciones e incidentes: `SPEC-013`–`SPEC-015`, `ACCEPTANCE-013`–`ACCEPTANCE-015`.
- Accesos y pasarelas: `SPEC-016`–`SPEC-019`, `ACCEPTANCE-016`–`ACCEPTANCE-019`.
- Evidencia, devoluciones y disputas: `SPEC-020`–`SPEC-027`, `ACCEPTANCE-020`–`ACCEPTANCE-027`.
- Piloto, despliegues y recuperación: `SPEC-029`–`SPEC-031`, `ACCEPTANCE-029`–`ACCEPTANCE-031`.
- Monitoreo productivo: `SPEC-032`, `ACCEPTANCE-032`, DEC-168, RN-1835–RN-1851.
- Migración y readiness: `SPEC-033`–`SPEC-034`, `ACCEPTANCE-033`–`ACCEPTANCE-034`, DEC-170.
- Arquitectura previa: ADR-0010–ADR-0012.

## Referencias técnicas

- [Observabilidad .NET con OpenTelemetry](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/observability-with-otel).
- [OpenTelemetry para .NET](https://opentelemetry.io/docs/languages/dotnet/).
- [Health checks en ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-10.0).

