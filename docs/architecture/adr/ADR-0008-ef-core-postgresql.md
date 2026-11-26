# ADR-0008 — Persistencia con Entity Framework Core y PostgreSQL

**Estado:** Aceptada  
**Fecha:** 2026-11-25

## Contexto

ADR-0001 selecciona PostgreSQL. ADR-0006 asigna propiedad exclusiva de datos a nueve módulos y ADR-0007 define una base compartida con discriminadores de tenant, filtros EF y Row-Level Security. Se requiere una estrategia uniforme de acceso a datos, contextos, esquemas, migraciones y tipos financieros.

## Alternativas consideradas

### SQL y Npgsql manual

Ofrecen control total, pero producen más código repetitivo y aumentan el costo de cambios de modelo, seguimiento y pruebas.

### Dapper como herramienta principal

Permite SQL explícito y consultas eficientes, pero no brinda unidad de trabajo, seguimiento ni migraciones integradas para los dominios transaccionales.

### EF Core sin excepciones

Uniforma modelo y migraciones, pero puede forzar consultas inadecuadas para reportes o actualizaciones complejas.

### EF Core como estándar y SQL explícito excepcional

Proporciona productividad, transacciones, migraciones y filtros multitenant, conservando la posibilidad de optimizar casos críticos demostrados.

## Decisión

Adoptar Entity Framework Core 10 como ORM principal y `Npgsql.EntityFrameworkCore.PostgreSQL` 10.x como proveedor.

- PostgreSQL es el único motor soportado.
- SQL parametrizado mediante EF Core o Npgsql se permite sólo cuando exista una necesidad demostrada.
- No se incorpora inicialmente un segundo micro-ORM.

## Contextos y esquemas

Cada módulo tiene su propio `DbContext` y esquema PostgreSQL:

| Módulo | Contexto | Esquema |
|---|---|---|
| Access | `AccessDbContext` | `access` |
| Sports | `SportsDbContext` | `sports` |
| Billing | `BillingDbContext` | `billing` |
| Payments | `PaymentsDbContext` | `payments` |
| Commercial | `CommercialDbContext` | `commercial` |
| Reconciliation | `ReconciliationDbContext` | `reconciliation` |
| Notifications | `NotificationsDbContext` | `notifications` |
| Refunds | `RefundsDbContext` | `refunds` |
| Reporting | `ReportingDbContext` | `reporting` |

Los esquemas separan módulos, no tenants. Todos los clubes comparten los esquemas y se aíslan según ADR-0007.

Reglas:

- Un contexto sólo mapea entidades y tablas de su módulo.
- No existen navegaciones EF entre módulos.
- Un módulo no inyecta el contexto de otro en sus repositorios.
- Los contratos públicos exponen DTO e identificadores, nunca entidades EF.
- Lazy loading permanece deshabilitado.
- Los comandos usan tracking; las consultas de sólo lectura usan `AsNoTracking` por defecto.
- Cada contexto vive dentro de una unidad de trabajo y no se reutiliza entre solicitudes o tareas.

## Migraciones

- Cada contexto mantiene migraciones dentro de su módulo.
- Cada módulo utiliza una tabla de historial en su propio esquema.
- Un manifiesto de despliegue establece el orden total de aplicación.
- Las migraciones se generan, revisan y versionan como artefactos.
- Producción no ejecuta migraciones automáticamente al iniciar la API.
- Una identidad técnica separada aplica las migraciones en un paso controlado.
- Cada migración productiva requiere verificación previa, respaldo aplicable y procedimiento de recuperación.
- Los cambios destructivos siguen una secuencia expandir–migrar–contraer.
- Las migraciones administran también RLS, restricciones e índices.

## Convenciones de datos

| Concepto | Convención |
|---|---|
| Identificadores | `uuid`, generados por la aplicación |
| Importes | `numeric` con precisión y escala explícitas |
| Porcentajes y tasas | `numeric`, nunca coma flotante |
| Instantes | UTC mediante `timestamp with time zone` |
| Fecha civil | `date` |
| Hora civil | `time without time zone` cuando corresponda |
| Zona horaria | Identificador IANA separado |
| Texto variable | `text`, salvo límite funcional justificado |
| Estados | Valor estable del dominio más restricción válida |
| Metadatos flexibles | `jsonb` sólo cuando sean realmente variables |

## Importes financieros

- No se utiliza el tipo PostgreSQL `money`.
- No se usan `float`, `real` o `double` para importes, tasas, comisiones o resultados financieros.
- Los value objects monetarios conservan importe y moneda.
- Precisión y escala se declaran por concepto.
- No se delega una política funcional de redondeo al comportamiento implícito del motor.
- Los cálculos intermedios pueden usar mayor escala que el importe liquidable.
- La política definitiva de redondeo y distribución debe validarse con la pasarela antes de cobros reales.

## Integridad

PostgreSQL refuerza invariantes expresables localmente:

- Campos obligatorios y rangos válidos.
- Unicidades por club o disciplina.
- Estados admitidos.
- Claves compuestas que impidan asociaciones entre tenants.
- Asignaciones activas únicas cuando corresponda.
- Identificadores externos únicos por proveedor y ámbito.
- Versiones de evidencia que nunca sobrescriban anteriores.

Las reglas dependientes de varias operaciones o sistemas externos permanecen en el dominio. Los triggers no serán la ubicación habitual de reglas funcionales.

## Características PostgreSQL

- `jsonb` no almacena el estado financiero central ni sustituye el modelo relacional.
- Stored procedures no son la ubicación habitual de reglas de negocio.
- Los triggers se limitan a necesidades técnicas justificadas.
- Los índices responden a restricciones y consultas reales.
- Los índices tenant-owned comienzan normalmente por `ClubId` y `DisciplineId`.
- Toda extensión PostgreSQL requiere justificación y disponibilidad en todos los ambientes.
- No se adopta particionamiento sin métricas de volumen.

## Transacciones y concurrencia

- EF Core proporciona unidad de trabajo dentro de cada módulo.
- La coordinación de varios contextos comparte conexión y transacción sólo en casos financieros explícitos.
- Los agregados mutables críticos utilizan tokens de concurrencia explícitos.
- Niveles de aislamiento, bloqueos, tokens y reintentos se definen en decisiones posteriores.

## Consecuencias

- Se acepta dependencia directa de EF Core 10 y Npgsql.
- El modelo relacional queda dividido por esquemas funcionales.
- Las migraciones no dependen del arranque de la aplicación.
- Las consultas críticas se observan y optimizan con evidencia.
- La versión mayor exacta de PostgreSQL se fija con Docker y ambientes, sin etiquetas flotantes.
- Esta decisión no crea modelos, migraciones ni código productivo.

## Trazabilidad

- Aislamiento, bajas lógicas e historia: `SPEC-001`–`SPEC-003`, `ACCEPTANCE-001`–`ACCEPTANCE-003`.
- Importes, pagos y duplicados: `SPEC-003`–`SPEC-004`, `ACCEPTANCE-003`–`ACCEPTANCE-004`.
- Gestión comercial y períodos cerrados: `SPEC-005`–`SPEC-007`, `ACCEPTANCE-005`–`ACCEPTANCE-007`.
- Reportes sin doble contabilización: `SPEC-008`, `ACCEPTANCE-008`.
- Conciliación y preservación del pago fuente: `SPEC-009`–`SPEC-011`, `ACCEPTANCE-009`–`ACCEPTANCE-011`.
- Retención de archivos y evidencias: `SPEC-012`, `SPEC-020`, `SPEC-026`.
- Notificaciones por cuenta: `SPEC-013`–`SPEC-015`.
- Checkout y pasarelas por disciplina: `SPEC-017`–`SPEC-019`.
- Devoluciones, reservas y disputas: `SPEC-021`–`SPEC-027`.
- Backups y migración: `SPEC-031`, `SPEC-033`.
- Habilitación financiera: `SPEC-034`, DEC-170.
- Límites y aislamiento: ADR-0006 y ADR-0007.

## Referencias técnicas

- [Proveedor Npgsql para EF Core](https://www.npgsql.org/efcore/).
- [Tipos numéricos de PostgreSQL](https://www.postgresql.org/docs/current/datatype-numeric.html).
- [Fecha y hora en Npgsql](https://www.npgsql.org/doc/types/datetime).

