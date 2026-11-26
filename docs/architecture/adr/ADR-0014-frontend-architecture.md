# ADR-0014 — Arquitectura frontend React/TypeScript

**Estado:** Aceptada  
**Fecha:** 2026-11-26

## Contexto

ADR-0001 selecciona React y TypeScript, ADR-0003 define OpenAPI, ADR-0009 establece sesiones cookie y autorización de backend, y SPEC-015 exige entrega y recuperación de notificaciones. Se requiere una arquitectura frontend modular, desplegable y compatible con los límites del backend.

## Alternativas consideradas

### SPA en un árbol técnico único

Es rápida al inicio, pero mezcla pantallas, permisos y llamadas sin límites funcionales.

### Framework SSR o full-stack

Agrega SSR y un servidor frontend, pero introduce otro runtime productivo sin una necesidad normativa de SEO.

### Microfrontends

Permiten despliegue independiente, pero agregan complejidad sin equipos o escalado que la justifiquen.

### SPA modular construida con Vite

Ofrece build estático, separación por capacidades y alineación con la API independiente.

## Decisión

Adoptar una SPA React/TypeScript construida con Vite, sin SSR, runtime Node.js productivo ni microfrontends.

- Build estático con assets versionados por hash.
- API y SPA preferentemente bajo el mismo origen.
- Node LTS, gestor de paquetes y versiones fijados en el repositorio.
- TypeScript estricto.
- Type checking separado y obligatorio en CI, ya que Vite sólo transpila.

## Estructura

```text
frontend/src/
├── app/
│   ├── providers/
│   ├── router/
│   ├── layouts/
│   └── bootstrap/
├── features/
│   ├── access/
│   ├── sports/
│   ├── billing/
│   ├── payments/
│   ├── commercial/
│   ├── reconciliation/
│   ├── notifications/
│   ├── refunds/
│   └── reporting/
└── shared/
    ├── api/
    ├── auth/
    ├── ui/
    ├── forms/
    ├── formatting/
    └── observability/
```

Cada feature encapsula sus rutas, componentes, hooks y adaptadores. No importa internals de otra. `shared` contiene primitivas estables y `app` compone providers y rutas sin lógica funcional. Las dependencias se validan mediante lint y pruebas de imports.

## Routing

Usar routing cliente con carga diferida para portal administrativo, responsable, superadministración, autenticación y errores.

- Las rutas privadas esperan la sesión.
- Los parámetros no conceden autorización.
- Guards sólo mejoran UX; el backend autoriza nuevamente.
- Cambios de permisos invalidan rutas y datos visibles.
- Error boundaries y carga se organizan por página o flujo.

## Contrato API

OpenAPI 3.1 es la fuente de tipos TypeScript.

- El cliente base centraliza URL, cookies, antiforgery, correlación y errores.
- El código generado no contiene dominio.
- Las features envuelven operaciones generadas con lenguaje funcional.
- CI detecta contrato desactualizado o incompatible.
- No se duplican DTO manualmente.
- Los errores usan una representación estructurada estable.

## Estado

| Estado | Ubicación |
|---|---|
| Servidor | TanStack Query |
| Filtros y paginación compartible | URL |
| Formularios | Estado local y biblioteca de formularios |
| Interfaz efímera | Estado local o contexto acotado |

TanStack Query administra queries, mutations, caché e invalidación. No se incorpora inicialmente un store global general.

El caché es en memoria, incluye ámbito en sus claves, se limpia al cambiar sesión o permisos y no persiste respuestas privadas en almacenamiento web. Mutations financieras no se reenvían sin idempotencia.

## Formularios

Formularios complejos usan React Hook Form y esquemas compatibles con Zod. La validación cliente mejora la experiencia, pero el servidor es autoritativo. Conflictos de versión no se fusionan silenciosamente.

## Importes y tiempo

- Dinero viaja con representación decimal exacta y moneda.
- El frontend no es autoridad de cálculos financieros.
- Cálculos previos son informativos y se confirman con la API.
- Instantes viajan en UTC y se muestran en la zona funcional.
- Fechas civiles no se convierten implícitamente.
- `Intl` realiza el formateo.

## Seguridad

- Cookies `Secure` y `HttpOnly` y antiforgery según ADR-0009.
- No se almacenan tokens de sesión en el navegador.
- Secretos y credenciales de pasarela permanecen en backend.
- Logout limpia caché, conexiones y datos visibles.
- La configuración frontend sólo contiene valores públicos.
- No se usa service worker para cachear respuestas privadas o financieras.

## Autorización visual

Las capacidades de sesión controlan navegación y acciones visibles, pero no sustituyen autorización de backend. La interfaz no deriva autoridad únicamente del rol y una vista global no concede facultades financieras adicionales.

## Tiempo real

Adoptar ASP.NET Core SignalR para notificaciones y cambios visibles.

- Autenticación mediante cookie.
- Grupos por cuenta y ámbitos vigentes.
- Revocación inmediata de entrega.
- Reconexión controlada seguida de consulta API.
- Polling como respaldo.
- Mensajes mínimos con detalle obtenido mediante API.
- Duplicados seguros.
- Cerrar aviso no equivale a leer o resolver.

## Operaciones financieras

No se usa optimistic UI para pagos, devoluciones, conciliaciones, liquidaciones o resoluciones inciertas. Se muestran estados pendientes, se conserva la clave idempotente y se distinguen solicitud, aprobación, ejecución y confirmación.

Optimistic UI se limita a acciones reversibles de bajo riesgo con reconciliación posterior.

## Componentes visuales

- Componentes compartidos accesibles sin reglas de dominio.
- Tokens de diseño para colores, tipografía y estados.
- Identidad de club mediante variables controladas.
- Contraste, teclado y estados no dependientes sólo del color.
- Biblioteca visual concreta pendiente, sujeta a estos contratos.

## Versionado y despliegue

- La SPA publica versión y compatibilidad de contrato.
- HTML de entrada no tiene caché prolongada; assets con hash sí.
- API y SPA separadas siguen expandir–migrar–contraer.
- OpenAPI se valida antes de publicar.

## Consecuencias

- Vite queda seleccionado.
- No existe servidor Node.js productivo.
- SignalR proporciona entrega inmediata y polling recuperación.
- TanStack Query maneja estado servidor; Redux no se incorpora inicialmente.
- Los límites frontend son verificables.
- Operaciones financieras evitan optimistic UI.
- Biblioteca visual permanece abierta.
- Esta decisión no genera código productivo.

## Trazabilidad

- Identidad visual y acceso: `SPEC-001`, `ACCEPTANCE-001`.
- Gestión deportiva, cuotas y pagos: `SPEC-002`–`SPEC-004`, `ACCEPTANCE-002`–`ACCEPTANCE-004`.
- Comercial, dashboards y exportaciones: `SPEC-005`–`SPEC-012`, `ACCEPTANCE-005`–`ACCEPTANCE-012`.
- Notificaciones y tiempo real: `SPEC-013`–`SPEC-015`, `ACCEPTANCE-013`–`ACCEPTANCE-015`.
- Portal del responsable y pasarelas: `SPEC-016`–`SPEC-019`, `ACCEPTANCE-016`–`ACCEPTANCE-019`.
- Evidencias, devoluciones y disputas: `SPEC-020`–`SPEC-027`, `ACCEPTANCE-020`–`ACCEPTANCE-027`.
- MVP, ambientes y readiness: `SPEC-028`, `SPEC-030`, `SPEC-034`.
- Contrato, autenticación y observabilidad: ADR-0003, ADR-0009 y ADR-0013.

## Referencias técnicas

- [Vite](https://vite.dev/guide/).
- [TypeScript en Vite](https://vite.dev/guide/features.html).
- [TanStack Query](https://tanstack.com/query/latest/docs/framework/react/overview).
- [SignalR JavaScript client](https://learn.microsoft.com/en-us/aspnet/core/signalr/javascript-client?view=aspnetcore-10.0).

