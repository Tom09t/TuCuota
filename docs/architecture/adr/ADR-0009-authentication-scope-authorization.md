# ADR-0009 — Autenticación y autorización por ámbito

**Estado:** Aceptada  
**Fecha:** 2026-11-25

## Contexto

El sistema distingue cuentas administrativas por club, una identidad global de superadministración, responsables autenticados mediante OTP e identidades técnicas. Toda operación privada debe validar sesión, club, disciplina, rol, vínculo y estado vigente en el backend. ADR-0007 y ADR-0008 agregan aislamiento en persistencia, pero no sustituyen la autorización funcional.

## Alternativas consideradas

### Proveedor de identidad externo obligatorio

Ofrece protocolos y MFA maduros, pero no representa directamente cuentas independientes por club con correos repetibles y agrega dependencia operativa para el MVP.

### Implementación criptográfica propia

Otorga control total, pero introduce riesgo de seguridad injustificado. Se descarta.

### ASP.NET Core Identity con modelo predeterminado

Proporciona contraseñas, tokens, bloqueo y cookies, pero su almacenamiento predeterminado requiere adaptación para la unicidad por club.

### Componentes de Identity con store tenant-aware

Reutiliza primitivas probadas y permite respetar las reglas multitenant. Requiere implementar cuidadosamente búsqueda, unicidad y revocación.

## Decisión

Utilizar componentes de ASP.NET Core Identity para las cuentas administrativas, con almacenamiento y resolución adaptados al módulo `Access`.

Separar tres clases de identidad:

1. Administrativa: correo, contraseña y pertenencia a un club.
2. Responsable: contacto verificado, OTP y sesión temporal, independiente de las cuentas administrativas.
3. Técnica: migraciones, tareas, webhooks y procesos internos, sin simular usuarios humanos.

El superadministrador es una identidad global explícita, no una cuenta asociada a un club ficticio.

## Sesión del navegador

La SPA utiliza cookies de autenticación `Secure`, `HttpOnly`, restringidas al host, con `SameSite=Lax` o más estricto cuando el flujo lo permita, duración limitada y renovación controlada.

- Las operaciones con efecto exigen protección antiforgery.
- Ninguna operación modificadora utiliza `GET`.
- No se guardan access tokens o refresh tokens en `localStorage` o `sessionStorage`.
- La topología preferida publica SPA y API bajo el mismo origen.
- Una topología cross-origin requiere aprobación explícita de CORS, cookies cross-site y protección CSRF.

## Sesiones revocables

La cookie no es la única fuente de vigencia. Cada sesión mantiene en servidor identificador, clase de identidad, creación, expiración, versión de seguridad o permisos, estado de revocación, actividad relevante y metadatos de auditoría limitados.

Cada solicitud privada verifica sesión, identidad activa y versión de seguridad. Cambios de rol, baja de cuenta, revocación de vínculo o inactivación de ámbito invalidan las sesiones afectadas sin esperar el vencimiento de la cookie. Las conexiones en tiempo real también dejan de entregar información.

## Cuentas administrativas

- La clave de búsqueda es conceptualmente `(ClubId, NormalizedEmail)`.
- Un correo repetido en otro club representa una cuenta independiente.
- Las contraseñas utilizan el hasher de ASP.NET Core Identity.
- Contraseñas y hashes no se registran en logs o auditoría.
- Los intentos fallidos producen bloqueo temporal y rate limiting.
- Las respuestas evitan enumeración de cuentas.
- La recuperación utiliza un enlace aleatorio, temporal y de un solo uso.
- Recuperar la cuenta invalida sesiones y tokens anteriores.
- Los parámetros exactos de contraseña, expiración y bloqueo se configuran y validan antes de Producción.

## Responsable y OTP

- El DNI por sí solo no confirma ni concede acceso.
- El OTP es criptográficamente aleatorio, de un solo uso y vigencia limitada.
- Sólo se conserva un hash o valor protegido.
- Se aplican límites por identidad, contacto, red y ventana temporal.
- El reenvío es controlado y no prolonga indefinidamente la validez.
- La comparación es segura y el consumo atómico.
- La sesión temporal obtiene privilegios sólo de vínculos vigentes.
- Revocar el vínculo invalida inmediatamente el acceso relacionado.
- El OTP del responsable no se reutiliza para acciones administrativas reforzadas.

## Autorización

Adoptar autorización basada en políticas y recursos de ASP.NET Core.

Una operación requiere conjuntamente identidad autenticada, sesión vigente, club, disciplina, capacidad o rol, y estado actual del recurso.

- La política fallback requiere autenticación por defecto.
- Los endpoints públicos se declaran explícitamente.
- Un rol no basta para autorizar un recurso concreto.
- Las políticas expresan capacidades funcionales.
- La autorización basada en recurso valida ámbito, propietario, estado y operación.
- Cada módulo vuelve a autorizar sus recursos.
- El frontend sólo orienta la interfaz y nunca constituye la barrera.
- RLS y filtros EF se aplican después de la autorización.

## Ámbitos efectivos

| Identidad | Ámbito |
|---|---|
| Superadministrador | Global, sujeto a políticas específicas |
| Delegado general | Su club y las facultades de lectura aprobadas |
| Administrador | Su club y disciplina, según capacidad |
| Responsable | Jugadores y cuotas alcanzados por vínculos vigentes |
| Tarea técnica | Capacidad y tenant asignados explícitamente |
| Migrador | Esquema y migraciones |
| Webhook | Proveedor y conexión verificados; no es un rol humano |

La vista global no concede por sí sola autoridad financiera adicional.

## Acciones reforzadas

- Configurar o cambiar cuenta receptora exige el OTP administrativo de SPEC-017.
- Resolver excepcionalmente un pago incierto exige justificación y evidencia de SPEC-019.
- Las excepciones administrativas globales registran motivo y auditoría.
- Superadministración exige MFA antes de Producción.
- Podrá ampliarse MFA a otros roles sin sustituir controles funcionales específicos.

## Identidades técnicas y webhooks

- Cada proceso técnico usa identidad y credenciales propias con mínimo privilegio.
- API, migraciones y administración no comparten credenciales.
- Las tareas reciben tenant y capacidad explícitos.
- Los webhooks validan origen, conexión, referencia, antigüedad y replay.
- Un webhook auténtico no convierte automáticamente un evento en resultado financiero definitivo.
- La rotación de secretos no recrea identidades funcionales.

## Rate limiting

Se aplican políticas diferenciadas a inicio de sesión, recuperación, solicitud y validación de OTP, reenvío, descargas, webhooks y endpoints costosos. Las particiones combinan identidad, ámbito, contacto e información de red cuando corresponda.

## Auditoría de seguridad

Se registran, sin secretos, autenticaciones, bloqueos, recuperaciones, emisión y consumo de OTP, sesiones, revocaciones, cambios de rol, rechazos sensibles, uso de caminos globales y acciones reforzadas.

## Consecuencias

- No se exige un proveedor de identidad externo para el MVP.
- Cuentas administrativas y responsables usan esquemas de autenticación separados.
- Las sesiones requieren estado servidor y son revocables.
- La autorización es más precisa que un RBAC basado sólo en roles.
- Las claves de ASP.NET Core Data Protection deben persistirse y compartirse entre réplicas.
- OAuth/OIDC para clientes externos queda fuera del alcance actual.
- Esta decisión no genera código productivo.

## Trazabilidad

- Cuentas, correo y roles: `SPEC-001`, `ACCEPTANCE-001`, DEC-019–DEC-026, RN-0102–RN-0153.
- Límites de delegados y administradores: `SPEC-001`–`SPEC-011`, `ACCEPTANCE-001`–`ACCEPTANCE-011`.
- Responsable, OTP y vínculos: `SPEC-016`, `ACCEPTANCE-016`, DEC-117–DEC-120, RN-1000–RN-1061.
- OTP para cuenta receptora: `SPEC-017`, `ACCEPTANCE-017`.
- Resolución excepcional: `SPEC-019`, `ACCEPTANCE-019`.
- Descargas con revalidación: `SPEC-012`, `SPEC-020`, `SPEC-026`.
- Revocación de notificaciones: `SPEC-015`, `ACCEPTANCE-015`.
- Seguridad desde el MVP: `SPEC-028`, AC-028-08.
- Separación de ambientes: `SPEC-030`, `ACCEPTANCE-030`.
- Preparación productiva: `SPEC-034`, `ACCEPTANCE-034`.
- Tenant y persistencia: ADR-0007 y ADR-0008.

## Referencias técnicas

- [Protección CSRF en ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/anti-request-forgery?view=aspnetcore-10.0).
- [Autorización por políticas](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-10.0).
- [Autorización basada en recursos](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/resource-based?view=aspnetcore-10.0).
- [Rate limiting en ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/performance/rate-limit?view=aspnetcore-10.0).

