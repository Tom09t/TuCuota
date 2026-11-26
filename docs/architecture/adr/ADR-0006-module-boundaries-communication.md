# ADR-0006 — Límites y comunicación entre módulos

**Estado:** Aceptada  
**Fecha:** 2026-11-25

## Contexto

ADR-0004 adopta un monolito modular y ADR-0005 define un proyecto .NET por módulo funcional. Es necesario establecer la propiedad de capacidades y datos, así como las formas permitidas de interacción, sin convertir cada especificación funcional en un módulo independiente.

## Alternativas consideradas

### Un módulo por especificación

Ofrece trazabilidad directa, pero produciría 34 módulos artificiales. Varias especificaciones representan etapas o aspectos de una misma capacidad funcional.

### Módulos técnicos

Separar por datos, servicios, archivos o integraciones resulta familiar, pero distribuye cada caso de uso entre varios módulos y debilita la propiedad funcional.

### Módulos por entidades principales

Es sencillo inicialmente, pero genera dependencias excesivas entre entidades relacionadas y no refleja invariantes completas.

### Módulos por capacidad y propiedad de datos

Alinea reglas, casos de uso y persistencia, y permite localizar invariantes y responsables. Requiere definir explícitamente las operaciones que atraviesan capacidades.

## Decisión

Definir módulos por capacidad funcional y propiedad exclusiva de datos mutables.

| Módulo | Propiedad funcional | Fuentes principales |
|---|---|---|
| `Access` | Clubes, disciplinas, identidad visual, cuentas administrativas, roles, permisos, identidad del responsable, OTP y sesiones | SPEC-001, SPEC-016 |
| `Sports` | Jugadores, fichas por club, inscripciones, categorías y vínculos responsable–jugador | SPEC-002 y parte de SPEC-016 |
| `Billing` | Cuotas, conceptos, beneficios, becas, mora, vencimientos y estado exigible de cada cuota | SPEC-003 |
| `Payments` | Checkout, selección de cuotas, pagos online y externos, asignaciones, cuentas receptoras, conexión con pasarelas, estados inciertos y evidencia financiera | SPEC-004, SPEC-017–020 |
| `Commercial` | Condiciones comerciales, mínimos, contratos, liquidaciones, penalizaciones, gracia, suspensión y regularización | SPEC-005–007 |
| `Reconciliation` | Ejecuciones, diferencias, resolución, historial y consulta operativa de conciliaciones | SPEC-009–011 |
| `Notifications` | Catálogo, preferencias, destinatarios, centro personal, lectura, entrega y sincronización | SPEC-013–015 |
| `Refunds` | Solicitudes, devoluciones, contracargos, disputas, componentes, costos excepcionales, políticas y expedientes | SPEC-021–027 |
| `Reporting` | Dashboards, proyecciones, reportes, solicitudes de exportación, archivos temporales y descargas | SPEC-008, SPEC-012 |

SPEC-028–SPEC-034 no originan un módulo `Operations`: establecen condiciones transversales de entrega, despliegue, recuperación, monitoreo, migración y habilitación.

## Propiedad de datos

- Cada dato mutable tiene un único módulo propietario.
- Solo el propietario crea o modifica su estado.
- Ningún módulo utiliza tablas, repositorios, entidades o `DbContext` de otro como contrato.
- Las claves entre módulos son identificadores opacos y no habilitan navegación ORM entre agregados de módulos diferentes.
- La infraestructura de archivos puede compartirse, pero cada módulo conserva la propiedad de metadatos, autorización, retención y relación funcional.
- `Reporting` no se convierte en propietario de los datos fuente por consolidarlos o proyectarlos.

## Comunicación

Adoptar comunicación híbrida mediante contratos síncronos y eventos internos.

### Comandos

Todo comando se dirige al módulo propietario. Un handler externo no modifica estado ajeno.

### Consultas síncronas

Se utilizan contratos públicos cuando una decisión requiere información vigente inmediatamente. Por ejemplo, `Payments` consulta contratos de `Access` y `Sports` para validar ámbito y vínculos.

### Coordinación financiera

Cuando el resultado de un pago, devolución o contracargo deba modificar cuotas de forma inseparable, la coordinación será síncrona y transaccional. La mecánica exacta se define en la decisión sobre transacciones y consistencia financiera.

### Eventos

- Los eventos de dominio permanecen dentro de su módulo.
- Los eventos de integración comunican hechos ya ocurridos entre módulos.
- Los eventos públicos son inmutables, tienen semántica documentada y no exponen entidades de persistencia.
- Notificaciones, proyecciones y exportaciones consumen eventos sin bloquear la operación financiera principal.
- Los eventos con consecuencias financieras u operativas no dependerán únicamente de memoria; su durabilidad se definirá junto con idempotencia y procesamiento asíncrono.

Ejemplos de eventos de integración son `PaymentConfirmed`, `PaymentBecameUncertain`, `RefundConfirmed`, `DisputeOpened`, `ReconciliationDifferenceDetected`, `ExportCompleted` y `AccessRevoked`.

### Topología inicial

El despacho inicial ocurre dentro del proceso del monolito. No se incorpora un broker externo hasta que exista una necesidad operativa comprobada y se apruebe un ADR específico.

## Dependencias y responsabilidades críticas

- Se prohíben referencias circulares y se verifican mediante pruebas arquitectónicas.
- `Access` decide identidad y ámbito; cada módulo vuelve a autorizar sobre su propio recurso.
- `Sports` decide la vigencia del vínculo responsable–jugador.
- `Billing` decide el estado exigible de una cuota.
- `Payments` decide el estado de checkout y pago, y solicita a `Billing` la transición correspondiente de cuotas.
- `Commercial` consume hechos financieros confirmados y no interpreta webhooks.
- `Reconciliation` detecta diferencias y nunca reescribe silenciosamente el pago fuente.
- `Refunds` conserva su flujo, mientras el pago original continúa perteneciendo a `Payments`.
- `Notifications` informa hechos y nunca los convierte en exitosos o resueltos.
- `Reporting` genera vistas y archivos, pero no es fuente de verdad financiera.

## Consecuencias

- La estructura se compone de nueve módulos funcionales; se elimina el módulo provisional `Operations`.
- `Access` posee identidades y sesiones, mientras `Sports` posee los vínculos con jugadores.
- La comunicación híbrida evita tanto llamadas síncronas para todo como consistencia eventual indiscriminada.
- Algunas operaciones financieras coordinan `Billing` y `Payments` mediante una transacción explícita.
- No se selecciona todavía una librería de mediación ni un broker.
- Esta decisión no genera código ni modifica requisitos funcionales.

## Trazabilidad

- Aislamiento, identidad y autorización: `SPEC-001`, `ACCEPTANCE-001`, RN-0001–RN-0171.
- Jugadores y vínculos: `SPEC-002`, `SPEC-016`, `ACCEPTANCE-002`, `ACCEPTANCE-016`, RN-1000–RN-1061.
- Cuotas y beneficios: `SPEC-003`, `ACCEPTANCE-003`.
- Pagos y límite de disciplina: `SPEC-004`, `SPEC-017`, `ACCEPTANCE-004`, `ACCEPTANCE-017`.
- Pasarelas, estados inciertos y evidencia: `SPEC-018`–`SPEC-020`, `ACCEPTANCE-018`–`ACCEPTANCE-020`.
- Gestión comercial: `SPEC-005`–`SPEC-007`, `ACCEPTANCE-005`–`ACCEPTANCE-007`.
- Conciliación: `SPEC-009`–`SPEC-011`, `ACCEPTANCE-009`–`ACCEPTANCE-011`.
- Notificaciones: `SPEC-013`–`SPEC-015`, `ACCEPTANCE-013`–`ACCEPTANCE-015`.
- Devoluciones y disputas: `SPEC-021`–`SPEC-027`, `ACCEPTANCE-021`–`ACCEPTANCE-027`.
- Reportes y exportaciones: `SPEC-008`, `SPEC-012`, `ACCEPTANCE-008`, `ACCEPTANCE-012`.
- Controles transversales y entrega: `SPEC-028`–`SPEC-034`, `ACCEPTANCE-028`–`ACCEPTANCE-034`.
- Arquitectura y estructura: ADR-0004 y ADR-0005.

