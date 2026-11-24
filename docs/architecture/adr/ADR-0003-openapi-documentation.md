# ADR-0003 — Contrato OpenAPI y documentación Swagger

**Estado:** Aceptada
**Fecha:** 2026-11-24

## Decisión

- Generar el contrato de la API con OpenAPI 3.1 mediante el soporte de ASP.NET Core 10.
- Mantener descripciones, esquemas, respuestas y autenticación como parte revisable del contrato.
- Proveer una interfaz Swagger para exploración y pruebas en entornos autorizados.
- No exponer documentación interactiva ni datos sensibles en Producción sin una política explícita de acceso.

## Consecuencias

El documento OpenAPI será verificable en CI y servirá de contrato para React/TypeScript. La interfaz interactiva requiere una herramienta adicional porque ASP.NET Core genera el documento OpenAPI, pero no incluye Swagger UI por defecto.

## Referencias oficiales

- [OpenAPI en ASP.NET Core 10](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/aspnetcore-openapi?view=aspnetcore-10.0)
- [Novedades OpenAPI de ASP.NET Core 10](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-10.0?view=aspnetcore-10.0)

