# R1 — Fundación ejecutable y controles transversales

**Estado:** en construcción  
**H1:** autorizado el 2026-11-26

## Primera entrega técnica

La fundación vertical mínima crea la estructura aprobada por ADR-0005, ADR-0006 y ADR-0014 sin implementar funcionalidad financiera ni tratar datos reales.

Incluye:

- solución .NET 10 con API, Worker, BuildingBlocks y nueve módulos;
- proyectos iniciales de pruebas de arquitectura e integración;
- SPA React/TypeScript construida con Vite;
- health check y endpoint técnico sin reglas funcionales;
- restauración reproducible mediante lockfiles;
- CI de compilación, pruebas y build frontend con Actions fijadas por SHA.

## Toolchain revalidada

| Componente | Pin de R1 |
|---|---:|
| .NET SDK | 10.0.404 |
| Runtime .NET / ASP.NET Core | 10.0.15 |
| Node.js LTS | 26.13.1 |
| npm | 11.20.0 |

R0-D07 registró `10.0.15` como pin del SDK, pero ese número corresponde al runtime y a los paquetes Microsoft. La revalidación previa a R1 distinguió el SDK `10.0.404` del runtime `10.0.15` sin cambiar la major aprobada.

Vite `8.3.8` requiere `@vitejs/plugin-react` compatible con Vite 8. Se fijó `6.3.0` después de comprobar que la versión 5.1.1 sólo aceptaba Vite 4–7.

## Verificaciones locales

- Restauración NuGet con lockfiles: satisfactoria.
- Compilación .NET: satisfactoria, cero advertencias y cero errores.
- Pruebas iniciales: dos suites satisfactorias.
- Instalación npm: cero vulnerabilidades reportadas.
- TypeScript y build Vite: satisfactorios.
- Validador documental de R0: satisfactorio.

Estas verificaciones demuestran el scaffolding; no satisfacen por sí solas el gate de salida completo de R1.

## Trazabilidad

- `docs/roadmap/H1_AUTHORIZATION.md`.
- ADR-0001, ADR-0003–ADR-0006 y ADR-0013–ADR-0017.
- SPEC-001, SPEC-030, SPEC-032 y SPEC-034.
- ACCEPTANCE-001, ACCEPTANCE-030, ACCEPTANCE-032 y ACCEPTANCE-034.
