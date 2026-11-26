# Autorización de H1 — Inicio de implementación

**Estado:** autorizado  
**Fecha:** 2026-11-26  
**Incremento habilitado:** R1 — Fundación ejecutable y controles transversales  
**Rama inicial:** `codex/r1-foundation`

## Decisión

El titular del proyecto autorizó explícitamente H1 después del cierre formal de R0 y la integración del PR #1 en `main`.

Se autoriza iniciar código productivo únicamente dentro del alcance de R1 definido en `ROADMAP-v1` y sujeto a la arquitectura aprobada mediante ADR-0001–ADR-0017.

## Alcance autorizado

- Solución .NET 10 con hosts API y Worker.
- Estructura de monolito modular y proyectos de pruebas.
- SPA React/TypeScript con Vite y cliente OpenAPI.
- PostgreSQL, EF Core, esquemas por módulo y migraciones iniciales.
- Controles transversales iniciales de tenant, autorización, auditoría, outbox/inbox, idempotencia y concurrencia.
- Observabilidad, health checks, desarrollo containerizado y pipeline de construcción y pruebas.
- Controles iniciales de secretos, dependencias, imágenes y cadena de suministro.
- Recorrido técnico mínimo API–PostgreSQL–frontend sin funcionalidad financiera real.

## Límites

Esta autorización no habilita:

- despliegue en Producción;
- contratación o aprovisionamiento cloud pago;
- datos personales, financieros u operativos reales;
- credenciales productivas de proveedores;
- cobros, devoluciones, contracargos ni dinero real;
- ampliación del alcance funcional fuera de R1;
- reducción o excepción de SPEC, ACCEPTANCE, ADR, DEC, RN, CON o gates posteriores.

Toda decisión arquitectónica material nueva requiere un ADR. Los acuerdos de ejecución que no alteren arquitectura se registrarán como evidencia de R1.

## Condiciones de avance

- Los cambios ingresarán mediante pull request y deberán superar `ci-gate` y las verificaciones aplicables.
- La trazabilidad seguirá `docs/r0/TRACEABILITY_CONVENTION.md`.
- PAR-024 deberá cerrarse antes de aceptar R1.
- Los criterios de salida de R1 permanecen íntegros según `ROADMAP-v1`.
- Producción y dinero real continúan sujetos a autorizaciones posteriores independientes.

## Trazabilidad

- `docs/r0/R0_CLOSURE.md`.
- `docs/roadmap/ROADMAP-v1.md`.
- R0-D01–R0-D26.
- ADR-0001 y ADR-0003–ADR-0017.
- SPEC-001, SPEC-030, SPEC-032 y SPEC-034.
- ACCEPTANCE-001, ACCEPTANCE-030, ACCEPTANCE-032 y ACCEPTANCE-034.
- DEC-170 y sus RN vigentes bajo la precedencia de CON-011.
