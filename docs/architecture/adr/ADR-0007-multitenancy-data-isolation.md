# ADR-0007 — Estrategia multitenant y aislamiento de datos

**Estado:** Aceptada  
**Fecha:** 2026-11-25

## Contexto

El sistema debe aislar clubes, disciplinas, usuarios, jugadores y operaciones financieras, mientras permite administración global, reportes consolidados y expansión progresiva. ADR-0006 define módulos con propiedad exclusiva de datos, y ADR-0001 establece PostgreSQL como base de datos.

## Unidad de tenant

El club es el tenant primario. La disciplina es un ámbito subordinado dentro del tenant, no un tenant independiente.

Las cuentas administrativas pertenecen a un club, los delegados operan sobre el club y los administradores sobre una disciplina. Las disciplinas pueden compartir una cuenta receptora sin compartir operaciones, permisos ni información administrativa.

## Alternativas consideradas

### Base de datos por club

Ofrece máximo aislamiento físico, pero multiplica migraciones, conexiones, backups y operación, y dificulta consultas globales. Su costo no se justifica para el piloto y la expansión inicial.

### Esquema por club

Proporciona separación nominal dentro de una base, pero complica migraciones, consultas globales y uso de EF Core, que no ofrece soporte multitenant nativo para esta estrategia.

### Tablas por club

Produce una cantidad creciente de tablas y exige consultas y migraciones dinámicas. Se descarta.

### Esquema compartido con discriminador

Simplifica onboarding, migraciones y consultas globales, pero un filtro omitido podría exponer información si no existen defensas adicionales.

### Esquema compartido, discriminador y RLS

Conserva simplicidad operativa y agrega aislamiento tanto en la aplicación como en PostgreSQL. Requiere políticas de seguridad, gestión rigurosa del contexto de conexión y pruebas específicas.

## Decisión

Utilizar una base PostgreSQL compartida y esquemas compartidos, con `ClubId` obligatorio en toda fila perteneciente a un tenant y `DisciplineId` adicional en los datos limitados a una disciplina.

El aislamiento se aplica mediante cuatro barreras complementarias:

1. Autorización explícita en cada caso de uso.
2. Filtros globales multitenant de EF Core.
3. Row-Level Security de PostgreSQL.
4. Restricciones, claves e índices que incorporan el ámbito.

Ninguna barrera sustituye a las demás.

## Clasificación de datos

| Clase | Ejemplos | Aislamiento |
|---|---|---|
| Global | Catálogos globales y configuración técnica | Sin `ClubId`; acceso global explícito |
| Raíz de tenant | Club e identidad institucional | Identificador global; modificación global autorizada |
| De club | Cuentas administrativas, contratos y consolidaciones | `ClubId` obligatorio |
| De disciplina | Inscripciones, cuotas, pagos, conciliaciones y devoluciones | `ClubId` y `DisciplineId` obligatorios |
| De responsable | Identidad y sesión del responsable | Identidad protegida; acceso efectivo mediante vínculos de ámbito |
| Proyección | Dashboards, reportes y exportaciones | Conserva las claves de ámbito de los datos proyectados |

Una identidad global de jugador o responsable no concede acceso transversal. El acceso se obtiene mediante fichas y vínculos vigentes dentro de un ámbito.

## Reglas de modelado

- `ClubId` y `DisciplineId` no se infieren exclusivamente mediante navegaciones profundas.
- Las escrituras obtienen el tenant desde un contexto confiable, no desde valores libres enviados por el cliente.
- Una actualización ordinaria no puede cambiar `ClubId`.
- Las claves foráneas de entidades de disciplina incorporan el ámbito necesario para impedir asociaciones cruzadas.
- Las restricciones de unicidad incluyen `ClubId` o `DisciplineId` según corresponda.
- Las excepciones globales, como el slug del club, utilizan restricciones independientes.
- Los índices tenant-owned comienzan normalmente por `ClubId` y, cuando corresponde, `DisciplineId`.
- No se implementa particionamiento físico inicialmente; requerirá evidencia de volumen y un ADR posterior.

## EF Core

Cada `DbContext` de módulo recibe un contexto de tenant inmutable para su unidad de trabajo.

Los filtros globales:

- Aplican `ClubId` y, cuando corresponde, `DisciplineId`.
- Se definen como filtros nombrados separados de la baja lógica en EF Core 10.
- No constituyen la única protección de seguridad.
- No se desactivan desde repositorios ordinarios.
- Solo pueden omitirse mediante caminos privilegiados explícitos, restringidos y auditados.

## PostgreSQL RLS

Las tablas tenant-owned usan `ENABLE ROW LEVEL SECURITY`, `FORCE ROW LEVEL SECURITY`, políticas de lectura y escritura y cláusulas `WITH CHECK` para impedir inserciones o cambios hacia otro tenant.

La ausencia de un contexto válido produce denegación por defecto.

El rol normal de ejecución:

- No es propietario de las tablas.
- No posee `BYPASSRLS`.
- Solo accede al tenant configurado para la transacción actual.
- Recibe `ClubId` mediante un valor de sesión de alcance transaccional para evitar que sobreviva al retorno de la conexión al pool.

## Operaciones globales

Superadministración, reportes globales, migraciones y procesos de plataforma usan caminos explícitos, no un club ficticio:

- Rol de base separado para operaciones globales autorizadas.
- Credenciales distintas de las peticiones ordinarias.
- Contratos de aplicación específicos.
- Revalidación de permisos globales.
- Auditoría de actor, motivo, ámbito y resultado.
- Procesamiento tenant por tenant cuando sea posible.

Las migraciones usan una identidad técnica separada. El rol normal no modifica esquemas ni políticas RLS.

## Resolución del contexto

- Panel administrativo: cuenta autenticada y club de pertenencia.
- Administrador: cuenta, club y disciplina autorizada.
- Responsable: sesión OTP y vínculo vigente.
- Webhook: proveedor, conexión y referencias internas verificadas; nunca un `ClubId` libre.
- Tarea programada: unidad de trabajo creada explícitamente para un tenant.
- Superadministrador: contexto global privilegiado y auditado.

## Consecuencias

- Incorporar un club no requiere una nueva base ni un nuevo esquema.
- Las consultas globales utilizan caminos privilegiados separados.
- Una restauración estándar afecta la base compartida completa; no se promete recuperación selectiva por club.
- RLS agrega complejidad a migraciones, pooling y pruebas, pero reduce el impacto de filtros omitidos.
- Las pruebas deben intentar accesos cruzados en lecturas, escrituras, asociaciones, reportes y archivos.
- La estructura exacta de `DbContext`, migraciones y proveedor EF se decide separadamente.
- Esta decisión no autoriza código productivo ni despliegue.

## Trazabilidad

- Aislamiento entre clubes y disciplinas: `SPEC-001`, `ACCEPTANCE-001`, DEC-001–DEC-012, RN-0001–RN-0070.
- Cuentas por club y correo repetible: `SPEC-001`, RN-0102–RN-0153.
- Identidad global y ficha por club: `SPEC-002`, `ACCEPTANCE-002`.
- Ámbito económico por disciplina: `SPEC-003`–`SPEC-011`, `ACCEPTANCE-003`–`ACCEPTANCE-011`.
- Archivos segregados y autorización: `SPEC-012`, `SPEC-020`, `ACCEPTANCE-012`, `ACCEPTANCE-020`.
- Notificaciones privadas por cuenta: `SPEC-013`–`SPEC-015`, `ACCEPTANCE-013`–`ACCEPTANCE-015`.
- Responsable y vínculo vigente: `SPEC-016`, `ACCEPTANCE-016`.
- Cuenta receptora compartida sin mezcla de operaciones: `SPEC-017`, `ACCEPTANCE-017`.
- Devoluciones y disputas aisladas: `SPEC-021`–`SPEC-027`, `ACCEPTANCE-021`–`ACCEPTANCE-027`.
- Separación de ambientes: `SPEC-030`, `ACCEPTANCE-030`.
- Migración inicial aislada: `SPEC-033`, AC-033-14.
- Módulos y propiedad de datos: ADR-0006.

## Referencias técnicas

- [Multitenancy en EF Core](https://learn.microsoft.com/en-us/ef/core/miscellaneous/multitenancy).
- [Filtros globales de EF Core](https://learn.microsoft.com/en-us/ef/core/querying/filters).
- [Row Security Policies de PostgreSQL](https://www.postgresql.org/docs/current/ddl-rowsecurity.html).
- [`CREATE POLICY` de PostgreSQL](https://www.postgresql.org/docs/current/sql-createpolicy.html).

