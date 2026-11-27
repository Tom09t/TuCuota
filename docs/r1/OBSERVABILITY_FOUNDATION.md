# R1 — Fundación de observabilidad y correlación

**Estado:** en construcción
**Alcance:** telemetría técnica portable; sin monitoreo productivo, auditoría funcional ni alertas

## Resultado del corte

- OpenTelemetry 1.21.0 en API y Worker con paquetes fijados y lockfiles.
- Recursos diferenciados `TuCuota.Api` y `TuCuota.Worker`.
- Trazas de ASP.NET Core, `HttpClient`, Npgsql y actividades propias de TuCuota.
- Métricas de ASP.NET Core, `HttpClient`, runtime y medidores propios de requests y heartbeat.
- Exportación OTLP habilitada únicamente cuando existe `OTEL_EXPORTER_OTLP_ENDPOINT` externo.
- Logs JSON con scopes, timestamps UTC, `TraceId` y `CorrelationId`.
- Propagación de `X-Correlation-ID` seguro; valores ausentes o inválidos se reemplazan por el `TraceId` W3C.
- Contadores de baja cardinalidad: no utilizan usuarios, clubes, disciplinas ni datos financieros como dimensiones.
- Pruebas del contrato de correlación para preservación segura y reemplazo de entradas inválidas.

## Límites

Este corte no implementa auditoría funcional persistente. La auditoría seguirá siendo transaccional, inmutable y separada de logs, métricas y trazas según ADR-0013.

Tampoco selecciona proveedor de observabilidad, define SLO o retención, configura paneles o alertas, ni declara cumplida `ACCEPTANCE-032`. PAR-013–PAR-016 y PAR-024 permanecen abiertos hasta sus gates documentados.

No se registran secretos, tokens, cookies, cadenas de conexión, identificadores personales ni contenido financiero sensible.

## Configuración

- Sin `OTEL_EXPORTER_OTLP_ENDPOINT`, la instrumentación permanece activa dentro del proceso pero no intenta exportar.
- Con un endpoint externo autorizado, OpenTelemetry utiliza el exporter OTLP estándar.
- Producción y su backend de observabilidad requieren una autorización y configuración posteriores.

## Trazabilidad

- ADR-0013, ADR-0015 y ADR-0017.
- Gate R1: recorrido observable, logs estructurados, correlación y health checks iniciales.
- Base técnica de SPEC-032 / ACCEPTANCE-032, sin aceptación productiva.
- SPEC-030 y SPEC-034 únicamente como controles acumulativos futuros.
- DEC-168 y RN-1835–RN-1851 bajo CON-011.
- DEC-170 permanece bloqueante para dinero real.
