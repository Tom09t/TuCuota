# ADR-0001 — Stack tecnológico base

**Estado:** Aceptada
**Fecha:** 2026-11-24

## Contexto

El proyecto requiere una API, una aplicación web, persistencia relacional y despliegues reproducibles, sin reducir los requisitos de aislamiento, auditoría, seguridad financiera o recuperación.

## Decisión

- Backend: .NET 10, ASP.NET Core Web API y C#.
- Frontend: React con TypeScript.
- Base de datos: PostgreSQL.
- Empaquetado y ejecución: contenedores Docker.

## Consecuencias

- .NET 10 es la versión objetivo y deberá mantenerse dentro de su ciclo LTS.
- Backend y frontend serán componentes separados con contrato HTTP documentado.
- PostgreSQL deberá satisfacer migraciones, transacciones, auditoría, aislamiento y la política de recuperación de DEC-166/167.
- Docker no decide por sí solo hosting, orquestación ni proveedor de infraestructura.
- ORM, librerías, topología interna, build del frontend y estrategia de despliegue permanecen pendientes.

## Restricciones

Esta decisión no autoriza código productivo ni modifica DEC, RN o CON.

