# ADR-0004 — Arquitectura de monolito modular

**Estado:** Aceptada  
**Fecha:** 2026-11-25

## Contexto

El sistema combina aislamiento multiclub, autorización por club, disciplina y rol, gestión deportiva, operaciones comerciales y procesos financieros con requisitos estrictos de consistencia, idempotencia, auditoría y trazabilidad.

La entrega será progresiva y deberá conservar compatibilidad con las capacidades posteriores al MVP. La baseline funcional no exige distribución física ni identifica todavía necesidades de escalado independiente que justifiquen la complejidad operativa y transaccional de una arquitectura de microservicios.

ADR-0001 establece ASP.NET Core 10 para el backend, React con TypeScript para el frontend, PostgreSQL para persistencia y Docker para empaquetado y ejecución.

## Alternativas consideradas

### Monolito tradicional por capas

Simplifica la implementación y las transacciones locales, pero favorece dependencias cruzadas, un dominio anémico y servicios compartidos sin propiedad funcional clara.

### Monolito modular

Mantiene una sola unidad lógica y de versionado, con límites funcionales internos explícitos. Reduce la complejidad operativa inicial, facilita transacciones locales y permite una futura extracción de módulos si aparece evidencia que la justifique. Exige disciplina arquitectónica y validación automática de dependencias.

### Microservicios desde el inicio

Permiten despliegue y escalado independientes, pero introducen consistencia distribuida, mensajería, observabilidad y operación más complejas. Ese costo y riesgo no están justificados para el MVP.

### Monolito distribuido híbrido

Permite separar anticipadamente algunas capacidades, pero incorpora gran parte del costo de un sistema distribuido antes de contar con límites maduros y requisitos de escala comprobados.

## Decisión

Adoptar un **monolito modular** para el backend, acompañado por la aplicación React separada definida en ADR-0001.

Se aplican los siguientes principios:

- El backend constituye un único sistema lógico y una única unidad de versionado.
- Cada módulo representa una frontera funcional explícita y es propietario de su modelo de dominio, casos de uso y persistencia.
- Ningún módulo accede directamente a entidades, `DbSet` o tablas propiedad de otro módulo.
- La interacción entre módulos se realiza mediante contratos internos explícitos y eventos internos. La forma concreta de comunicación se definirá en un ADR posterior.
- Las invariantes que requieren atomicidad permanecen dentro de una única frontera transaccional.
- PostgreSQL puede compartirse físicamente sin convertir el esquema de datos en una API común entre módulos.
- Las dependencias permitidas entre módulos se verifican mediante pruebas arquitectónicas ejecutadas en integración continua.
- API, procesamiento asíncrono y tareas programadas podrán utilizar procesos de ejecución diferentes sin constituir microservicios ni duplicar modelos funcionales. Su topología se decidirá separadamente.
- Una futura extracción a microservicio requiere un nuevo ADR sustentado en evidencia de escala, disponibilidad, seguridad u operación.

## Capacidades preliminares

La arquitectura deberá contemplar, como orientación inicial, las siguientes capacidades:

- Identidad, tenancy y acceso.
- Gestión deportiva.
- Cuotas y beneficios.
- Pagos e integración con pasarelas.
- Gestión comercial.
- Conciliación financiera.
- Notificaciones.
- Devoluciones y disputas.
- Reportes y exportaciones.
- Operación de plataforma.

Esta lista no fija todavía los límites definitivos. Las 34 especificaciones no se traducen mecánicamente en 34 módulos; varias describen capacidades o etapas de un mismo dominio.

## Consecuencias

- Se priorizan simplicidad operativa, consistencia local y evolución progresiva.
- Un fallo del backend puede afectar varias capacidades y no existe escalado independiente por módulo en la arquitectura inicial.
- Los límites internos deberán ser verificables y no depender solamente de convenciones de nombres o carpetas.
- La modularidad no sustituye el aislamiento multitenant ni la autorización obligatoria en el backend.
- La estructura de proyectos .NET, los límites definitivos, la persistencia, la comunicación interna, los procesos asíncronos y el hosting permanecen pendientes de decisiones posteriores.
- Esta decisión no autoriza implementación, despliegue ni cobros reales.

## Trazabilidad

- Aislamiento multiclub y autorización: `SPEC-001`, `ACCEPTANCE-001`, DEC-001–DEC-026, RN-0001–RN-0171.
- Gestión deportiva, cuotas y beneficios: `SPEC-002`–`SPEC-003`, `ACCEPTANCE-002`–`ACCEPTANCE-003`.
- Pagos, pasarelas, evidencia y estados inciertos: `SPEC-004`, `SPEC-017`–`SPEC-020`, `ACCEPTANCE-004`, `ACCEPTANCE-017`–`ACCEPTANCE-020`.
- Gestión comercial: `SPEC-005`–`SPEC-007`, `ACCEPTANCE-005`–`ACCEPTANCE-007`.
- Conciliación, reportes y exportaciones: `SPEC-008`–`SPEC-012`, `ACCEPTANCE-008`–`ACCEPTANCE-012`.
- Notificaciones: `SPEC-013`–`SPEC-015`, `ACCEPTANCE-013`–`ACCEPTANCE-015`.
- Acceso del responsable: `SPEC-016`, `ACCEPTANCE-016`.
- Devoluciones y disputas: `SPEC-021`–`SPEC-027`, `ACCEPTANCE-021`–`ACCEPTANCE-027`.
- MVP progresivo y controles financieros obligatorios: `SPEC-028`, `ACCEPTANCE-028`, DEC-161, RN-1713–RN-1731.
- Piloto, despliegues, recuperación, monitoreo, migración y habilitación financiera: `SPEC-029`–`SPEC-034`, `ACCEPTANCE-029`–`ACCEPTANCE-034`, DEC-162–DEC-170.
- Stack tecnológico base: ADR-0001.

