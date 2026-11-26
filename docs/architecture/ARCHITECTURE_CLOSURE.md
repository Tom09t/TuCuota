# Cierre de arquitectura técnica

**Estado:** cerrado  
**Fecha:** 2026-11-26  
**Baseline funcional:** `baseline-v1`

## Alcance

Este documento consolida las catorce decisiones técnicas aprobadas antes de elaborar el roadmap de implementación. No modifica DEC, RN, CON, SPEC, ACCEPTANCE ni `baseline-v1`.

El cierre arquitectónico no autoriza por sí mismo código productivo, despliegue ni cobros reales. La planificación de implementación se realizará separadamente.

## Decisiones cerradas

| # | Tema | ADR | Resultado |
|---:|---|---|---|
| 1 | Arquitectura interna | ADR-0004 | Monolito modular |
| 2 | Solución .NET 10 | ADR-0005 | Un proyecto por módulo y hosts como composition roots |
| 3 | Límites y comunicación | ADR-0006 | Nueve módulos, contratos síncronos y eventos internos |
| 4 | Multitenancy | ADR-0007 | Club como tenant, base compartida, filtros EF y RLS |
| 5 | Persistencia | ADR-0008 | EF Core 10, Npgsql, contexto y esquema por módulo |
| 6 | Identidad y autorización | ADR-0009 | Identity tenant-aware, cookies revocables y políticas por recurso |
| 7 | Consistencia financiera | ADR-0010 | ACID local, estados externos explícitos y conciliación |
| 8 | Idempotencia | ADR-0011 | Claves por frontera, inbox/outbox, versiones y locks selectivos |
| 9 | Asincronía | ADR-0012 | Worker separado, PostgreSQL durable y Quartz |
| 10 | Auditoría y observabilidad | ADR-0013 | Auditoría transaccional y OpenTelemetry |
| 11 | Frontend | ADR-0014 | SPA React/TypeScript con Vite, TanStack Query y SignalR |
| 12 | Pruebas | ADR-0015 | Pirámide por riesgo con PostgreSQL real y E2E críticos |
| 13 | Ejecución | ADR-0016 | Contenedores administrados, PostgreSQL gestionado y ambientes aislados |
| 14 | Seguridad | ADR-0017 | ASVS, gestor de secretos, KMS y cadena de suministro segura |

## Vista consolidada

```text
Navegador
  └─ SPA React/TypeScript + Vite
       ├─ Cookie segura + antiforgery
       ├─ OpenAPI client
       └─ SignalR + polling de recuperación
                │
        Reverse proxy / TLS
                │
       ┌────────┴────────┐
       │ TuCuota.Api     │
       │ TuCuota.Worker  │
       └────────┬────────┘
                │
    Monolito modular .NET 10
    Access · Sports · Billing · Payments
    Commercial · Reconciliation · Notifications
    Refunds · Reporting
                │
       PostgreSQL administrado
       ├─ esquema por módulo
       ├─ ClubId / DisciplineId
       ├─ RLS
       ├─ outbox / inbox / jobs
       └─ auditoría transaccional
                │
       Servicios administrados
       ├─ object storage privado
       ├─ secretos + KMS
       ├─ correo
       ├─ OTLP / monitoreo
       └─ backups y PITR
```

## Trazabilidad normativa

- `baseline-v1` fija .NET 10, React/TypeScript, PostgreSQL y Docker.
- `SPEC-001`–`SPEC-027` y `ACCEPTANCE-001`–`ACCEPTANCE-027` determinan capacidades, ámbitos, estados e invariantes.
- `SPEC-028` y `ACCEPTANCE-028` delimitan el MVP y sus controles obligatorios.
- `SPEC-029` y `ACCEPTANCE-029` condicionan piloto y expansión.
- `SPEC-030` y `ACCEPTANCE-030` fijan separación de ambientes y despliegues controlados.
- `SPEC-031` y `ACCEPTANCE-031` fijan PITR de 30 días, doce copias mensuales y conciliación posterior.
- `SPEC-032` y `ACCEPTANCE-032` fijan monitoreo y correo técnico.
- `SPEC-033` y `ACCEPTANCE-033` fijan migración inicial segura e idempotente.
- `SPEC-034`, `ACCEPTANCE-034` y DEC-170 mantienen bloqueados los cobros reales hasta satisfacer todos los gates.
- ADR-0001–ADR-0003 permanecen vigentes para stack, Mercado Pago y OpenAPI.

## Parámetros operativos pendientes

Los siguientes puntos no cambian la arquitectura y deben resolverse en el roadmap o antes de su hito correspondiente:

- Proveedor comercial de contenedores, PostgreSQL, storage, secretos, KMS, correo y observabilidad.
- Región y residencia de datos.
- Dimensionamiento inicial y presupuesto de conexiones.
- RPO/RTO adicionales a la retención obligatoria.
- SLO, umbrales, ventanas y destinatarios nominales.
- Plataforma concreta de CI/CD y responsables de aprobación.
- Versión mayor exacta de PostgreSQL y versiones fijadas de Quartz y paquetes.
- Biblioteca visual frontend.
- Valores funcionales pendientes documentados en las SPEC: frecuencias, plazos, tamaños, zonas y retenciones no fijadas.
- Obligaciones legales de conservación y catálogo de evidencia aceptable.
- Modalidad concreta de checkout y validaciones técnicas, comerciales y legales de ADR-0002.

Ninguno de estos puntos permite reducir requisitos aprobados ni habilitar dinero real por presunción.

## Gates antes de implementación

Antes de iniciar código productivo deben aprobarse al menos:

1. Roadmap y orden de construcción por capacidades.
2. Alcance de la primera entrega y criterios de salida.
3. Repositorio, CI básico y política de ramas.
4. Versiones iniciales fijadas y revisión de licencias.
5. Matriz SPEC/AC por incremento.
6. Responsables de decisiones técnicas y de seguridad.

## Gates antes de Producción

- Proveedor e infraestructura conformes a ADR-0016 y ADR-0017.
- Migraciones probadas y procedimiento de recuperación.
- PITR de 30 días y doce copias mensuales verificables.
- Monitoreo y correo técnico activos.
- Secretos separados, rotación ensayada y MFA global.
- Pruebas de aislamiento, RLS, idempotencia y concurrencia.
- Restauración y conciliación posterior ensayadas.
- Revisión ASVS y penetration test.
- Migración inicial aprobada.
- Procedimientos operativos ejecutables.

## Gate de dinero real

Los cobros reales permanecen bloqueados hasta satisfacer integralmente ADR-0002, DEC-170, `SPEC-034` y `ACCEPTANCE-034`, incluyendo validación de Split Payment, comisiones, idempotencia, pagos inciertos, conciliación, devoluciones, contracargos, seguridad, recuperación, monitoreo y aprobaciones explícitas.

## Próximo paso

El siguiente artefacto será el roadmap de implementación. Deberá ordenar capacidades y dependencias sin reabrir requisitos funcionales ni decisiones arquitectónicas, salvo que aparezca evidencia nueva que exija otro ADR.

