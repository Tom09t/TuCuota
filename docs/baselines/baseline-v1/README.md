# Baseline funcional v1

**Estado:** aprobada
**Fecha:** 2026-11-24
**Etiqueta Git:** `baseline-v1`

## Alcance congelado

Esta baseline referencia la documentación funcional y técnica aprobada del repositorio:

- 170 DEC originales.
- 1.889 RN originales y cinco RN complementarias.
- 20 CON aprobadas y CON-020 descartada.
- Textos históricos, sustituciones y precedencia de CON-011.
- PRD con trazabilidad explícita 170/170 DEC.
- Alcance del MVP aprobado.
- Revisiones materiales 01–08 e informe de cierre funcional.
- ADR del stack, OpenAPI/Swagger y Mercado Pago Split Payments 1:1.

`SHA256SUMS` identifica el contenido exacto de cada archivo incluido. La fuente normativa continúa siendo DEC/RN/CON; los documentos derivados no pueden modificarla silenciosamente.

## Decisiones técnicas registradas

- .NET 10, ASP.NET Core Web API y C#.
- React con TypeScript.
- PostgreSQL.
- Docker.
- OpenAPI 3.1 y Swagger.
- Mercado Pago Split Payments 1:1, sujeto a validaciones técnicas, comerciales y legales.

## Límites

La baseline no autoriza implementación, despliegue ni cobros reales. No certifica todavía compatibilidad comercial de Mercado Pago, hosting ni infraestructura. DEC-167 y DEC-170 permanecen como gates obligatorios.

Todo cambio funcional posterior deberá conservar trazabilidad, explicar su impacto y producir una nueva baseline; no se reescribe `baseline-v1`.
