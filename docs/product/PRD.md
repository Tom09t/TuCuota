# Product Requirements Document (PRD)

**Proyecto:** Plataforma SaaS multiclub de gestión de cuotas deportivas  
**Versión:** 0.1 — Consolidación inicial  
**Estado:** Borrador para revisión  
**Origen:** Relevamiento SDD, decisiones DEC-001 a DEC-170  
**Club piloto:** Obras Mendoza  
**Disciplina piloto:** Obras Futsal

---

## 1. Resumen ejecutivo

Se desarrollará una plataforma SaaS multiclub destinada a administrar jugadores, disciplinas deportivas, categorías, beneficios económicos, cuotas y pagos.

La plataforma permitirá que cada club organice sus disciplinas, manteniendo independencia administrativa y financiera entre ellas.

El sistema incorporará cobros online mediante una pasarela compatible con Split Payment, de modo que el importe nominal de las cuotas se destine a la cuenta correspondiente y la comisión de la plataforma se distribuya según las reglas comerciales aprobadas.

El producto se lanzará mediante un MVP funcional, comenzando con un piloto progresivo en Obras Futsal.

**Trazabilidad:** DEC-001, DEC-016, DEC-121, DEC-123, DEC-161, DEC-162.

## 2. Objetivos del producto

### 2.1 Objetivo principal

Centralizar la administración de cuotas deportivas y facilitar su cobro, manteniendo independencia operativa y financiera por disciplina.

### 2.2 Objetivos funcionales

- Administrar múltiples clubes desde una plataforma compartida.
- Gestionar disciplinas y sus administradores.
- Registrar jugadores e inscripciones deportivas.
- Administrar categorías y valores de cuotas.
- Gestionar becas y beneficios económicos.
- Generar cuotas automáticamente.
- Permitir pagos online y registrar pagos externos.
- Ofrecer un portal a los responsables de pago.
- Proporcionar información financiera y conciliación.
- Mantener auditoría y trazabilidad de las operaciones.

**Trazabilidad:** DEC-001, DEC-013–DEC-016, DEC-027–DEC-048, DEC-080–DEC-096, DEC-117–DEC-121, DEC-161.

## 3. Modelo multiclub

La plataforma deberá soportar múltiples clubes desde su primera versión.

Cada club contará con su propia configuración, disciplinas, administradores y operaciones.

Un jugador podrá participar en más de un club y en distintas disciplinas, conservando una identidad global basada en DNI y registros de participación independientes.

El aislamiento entre disciplinas será especialmente estricto en las operaciones financieras.

**Trazabilidad:** DEC-001, DEC-007, DEC-009, DEC-015–DEC-017, DEC-121, DEC-162.

## 4. Actores y permisos

### 4.1 Superadministrador

Administra la plataforma globalmente, incluyendo clubes, configuraciones globales, integraciones disponibles, supervisión financiera y autorizaciones especiales.

### 4.2 Delegado general de club

Gestiona la configuración y determinados usuarios de su club. Puede consultar información consolidada de las disciplinas bajo su alcance.

Sus facultades operativas estarán limitadas por las excepciones expresamente aprobadas.

### 4.3 Administrador de disciplina

Gestiona jugadores, categorías, cuotas, becas y operaciones autorizadas de su propia disciplina.

No puede administrar libremente otras disciplinas.

### 4.4 Responsable de pago

Accede mediante DNI y código de un solo uso (OTP). Puede consultar las cuotas de los jugadores con quienes tenga una vinculación autorizada y realizar pagos.

**Trazabilidad:** DEC-005–DEC-008, DEC-019–DEC-023, DEC-080, DEC-117–DEC-120.

## 5. Gestión deportiva

### 5.1 Jugadores

Se podrán crear jugadores manualmente o mediante importación de Excel.

La identidad global utilizará DNI, mientras que los datos y estados específicos de cada club se administrarán de manera independiente.

### 5.2 Inscripciones

Cada jugador podrá tener inscripciones independientes en diferentes disciplinas.

### 5.3 Categorías

Las categorías serán administradas por los responsables autorizados de cada disciplina.

Cada jugador tendrá una categoría activa por disciplina, conservando el historial de cambios.

**Trazabilidad:** DEC-013–DEC-018, DEC-027–DEC-029.

## 6. Gestión de cuotas y beneficios

### 6.1 Cuotas

Las cuotas se generarán automáticamente de forma mensual, según el día configurado por cada disciplina.

Los importes base se definirán por categoría.

Cada disciplina podrá configurar vencimientos y reglas de recargos por mora.

### 6.2 Beneficios económicos

Se admitirán:

- Importes personalizados.
- Descuentos porcentuales.
- Becas configuradas por disciplina.

No se acumularán múltiples beneficios económicos sobre una misma cuota cuando las reglas aprobadas establezcan su exclusividad.

### 6.3 Pagos

Los responsables podrán seleccionar cuotas pendientes y pagarlas completas, individualmente o agrupadas según las restricciones de disciplina.

No se admitirán pagos parciales de una cuota.

**Trazabilidad:** DEC-030–DEC-047, DEC-121.

## 7. Modelo de pagos y monetización

### 7.1 Comisión de la plataforma

La plataforma tendrá como fuente principal de ingresos una comisión equivalente al **5% del importe nominal de las cuotas cobradas online**.

Los costos de procesamiento de la pasarela también serán trasladados al pagador conforme a las condiciones técnicas, comerciales y legales aplicables.

### 7.2 Split Payment

Cada disciplina vinculará una cuenta habilitada en la pasarela correspondiente.

El mecanismo de Split Payment deberá distribuir los fondos entre el destinatario de la cuota y la plataforma, sin que la plataforma deba custodiar los fondos del club.

### 7.3 Independencia financiera

Los checkouts no podrán mezclar cuotas de diferentes disciplinas.

Los registros financieros, comisiones y conciliaciones deberán conservar su independencia, incluso cuando distintas disciplinas utilicen una misma cuenta receptora.

### 7.4 Pagos externos

Los administradores autorizados podrán registrar pagos realizados por efectivo, transferencia u otros medios externos.

Estos registros deberán conservar método, fecha, responsable y auditoría.

**Trazabilidad:** DEC-046–DEC-048, DEC-121–DEC-125, DEC-130–DEC-135.

**Dependencia pendiente:** seleccionar y validar una pasarela que soporte efectivamente el esquema requerido, incluyendo las operaciones posteriores de devolución y conciliación.

## 8. Seguridad financiera

El sistema deberá incorporar:

- Idempotencia en operaciones financieras.
- Prevención de cobros duplicados.
- Verificación de pagos inciertos.
- Procesamiento seguro de notificaciones de pasarela.
- Conciliación de estados y movimientos.
- Auditoría de modificaciones y decisiones financieras.
- Aislamiento de información entre clubes y disciplinas.

Una operación cuyo resultado sea incierto no podrá liberarse para un nuevo cobro únicamente por el vencimiento de un temporizador interno.

**Trazabilidad:** DEC-082–DEC-096, DEC-134–DEC-142, DEC-161.

## 9. Devoluciones y contracargos

El diseño funcional completo contempla solicitudes de devolución, devoluciones totales o parciales por cuotas completas, políticas configurables, evidencias, estados, responsabilidades por costos y tratamiento de contracargos.

En el MVP deberán existir procedimientos operativos controlados para gestionar estos casos, aunque los módulos avanzados se implementen posteriormente.

Las devoluciones estarán condicionadas a las capacidades reales de la pasarela y a las reglas financieras aprobadas.

**Trazabilidad:** DEC-143–DEC-160, DEC-161.

## 10. Reportes y conciliación

El MVP incluirá información financiera básica y mecanismos esenciales de conciliación.

Los reportes deberán distinguir importes nominales, pagos online, pagos externos, comisiones de plataforma, costos de pasarela cuando estén disponibles y fondos correspondientes a cada disciplina.

Las funciones avanzadas de exportación, análisis y automatización se organizarán en versiones posteriores según DEC-161.

**Trazabilidad:** DEC-080–DEC-103, DEC-161.

## 11. Alcance del MVP

### Incluido

- Administración de clubes y disciplinas.
- Usuarios, roles y permisos.
- Jugadores, inscripciones y categorías.
- Cuotas automáticas.
- Becas y beneficios esenciales.
- Pagos online con Split Payment validado.
- Registro de pagos externos.
- Portal de responsables.
- Reportes básicos.
- Conciliación esencial.
- Seguridad, auditoría e idempotencia.
- Controles de incidentes y pagos inciertos.
- Procedimientos operativos para devoluciones y contracargos.

### Para versiones posteriores

- Gestión avanzada de devoluciones y contracargos.
- Reportes avanzados.
- Exportaciones avanzadas.
- Automatizaciones complementarias.

El alcance diferido deberá revisarse contra las obligaciones operativas que sí deben estar cubiertas desde el lanzamiento.

**Trazabilidad:** DEC-161.

## 12. Lanzamiento

El despliegue comercial será progresivo:

1. Grupo controlado de Obras Futsal.
2. Resto de Obras Futsal.
3. Otras disciplinas de Obras Mendoza.
4. Otros clubes.

Cada ampliación requerirá validaciones técnicas y financieras y autorización del superadministrador.

**Trazabilidad:** DEC-162, DEC-163.

## 13. Infraestructura y operación

Se utilizarán dos entornos independientes: Desarrollo y Producción.

Las publicaciones se realizarán mediante CI/CD con pruebas automáticas y aprobación explícita antes del despliegue productivo.

La base de datos productiva contará con una política de recuperación a un punto en el tiempo de 30 días y copias mensuales cifradas conservadas durante 12 meses.

Se implementará monitoreo automático con alertas por correo al responsable técnico.

**Trazabilidad:** DEC-164–DEC-168.

## 14. Migración inicial

La incorporación de Obras Futsal contemplará jugadores, categorías, beneficios vigentes, deudas pendientes y pagos externos históricos confiables.

La migración requerirá validación previa, revisión de duplicados y errores, aprobación, idempotencia y auditoría.

Los pagos históricos no generarán comisiones retroactivas ni se representarán como transacciones ficticias de pasarela.

**Trazabilidad:** DEC-169.

## 15. Criterios de aceptación del lanzamiento

El piloto no podrá procesar pagos reales hasta cumplir el checklist integral aprobado:

- Funcionalidades esenciales verificadas.
- Seguridad y aislamiento validados.
- Split Payment validado.
- Pruebas financieras satisfactorias.
- Migración inicial aprobada.
- Backups y restauración verificados.
- Monitoreo operativo.
- Procedimientos de incidentes documentados.
- Aprobación del responsable técnico.
- Habilitación del superadministrador.

El incumplimiento de una condición crítica bloqueará los nuevos cobros reales.

**Trazabilidad:** DEC-170, RN-1872–RN-1889.

## 16. Dependencias y cuestiones pendientes

Las siguientes cuestiones requieren resolución o verificación antes de dar por cerradas las especificaciones de implementación:

1. Selección y validación real de la pasarela de pagos.
2. Confirmación de capacidades de Split Payment, costos, devoluciones y contracargos.
3. Validación de las condiciones comerciales y legales del modelo de cobro.
4. Selección definitiva de infraestructura compatible con los requisitos de backups y recuperación.
5. Desglose verificable de funcionalidades del MVP y de las versiones posteriores.
6. Revisión completa de la correspondencia entre las 170 decisiones y sus reglas de negocio.

Estas cuestiones no constituyen nuevos requisitos aprobados.

## 17. Estado del documento

**PRD v0.1 — Borrador inicial.**

El documento consolida los requisitos principales del relevamiento. La trazabilidad completa de las 1.889 reglas requiere una matriz específica y revisión contra sus registros originales.

Ninguna decisión aprobada se considera modificada por este borrador.

> Borrador original recuperado; requiere cotejo con RN y DEC.

## Adenda aprobada de consolidación

Esta adenda prevalece sobre el borrador anterior dentro de su alcance. El PRD completo aún requiere cotejo.

- **CON-001:** Conservar IDs originales, versiones vigentes e historial; no renumerar. Solo reglas vigentes en especificaciones; contradicciones no resueltas requieren aprobación.
- **CON-002:** Reglas complementarias RN-C independientes, vinculadas a decisiones originales; no introducir funcionalidades sin aprobación.
- **CON-003:** Aprobar RN-C001–005 para DEC-031: tres modalidades de beneficio individual, importe fijo, porcentaje y beca reutilizable.
- **CON-004:** RN-0001–0006 preliminares pasan a PRE-RN-0001–0006; prevalecen las formales. RN-0404–0411: prevalece la corrección DEC-061; conservar versiones previas.
- **CON-005:** El delegado general puede crear, modificar y desactivar administradores de disciplina de su club después de que el superadministrador cree el primero. Administradores de disciplina gestionan los de su disciplina. Delegado solo lectura sobre jugadores, cuotas y pagos.
- **CON-006:** Proteger último administrador activo de disciplina: delegado y administradores no pueden desactivarlo; superadministrador puede excepcionalmente con justificación y auditoría de actor, fecha, disciplina, cuenta y motivo.
- **CON-007:** Solo superadministrador restaura administración cuando no queda administrador activo; delegado recupera permisos tras restauración. No desactivar disciplina automáticamente ni perder datos.
- **CON-008:** Superadministrador y delegado general modifican identidad visual institucional; administradores de disciplina no. Auditar y reflejar en portal público y administrativo. DEC-012.
- **CON-009:** Delegado solo lectura sobre devoluciones, costos y autorizaciones financieras; no aprueba, rechaza ni compromete fondos. Administradores de disciplina conservan facultades aprobadas; costos de plataforma exclusivos del superadministrador. DEC-021/151.
- **CON-010:** Relevamiento formal prevalece sobre PRE-RN-0001–0006; verificar cobertura, sin nuevas RN complementarias por esta corrección.
- **CON-011:** Precedencia por alcance: CON aprobada, corrección explícita aprobada, excepción específica, regla original vigente. Reglas compatibles se complementan. PRD y especificaciones no cambian requisitos; consultar contradicciones materiales no resueltas.
- **CON-012:** Proteger último delegado activo: otros delegados no pueden desactivarlo; superadministrador excepcionalmente con justificación auditada y único habilitado para restaurar. Club y disciplinas continúan; preservar historial. DEC-022.
- **CON-013:** Baja lógica de jugador exclusivamente por disciplina, sin baja simultánea de todas las disciplinas. Administrador en su ámbito, delegado solo lectura. Preservar historial y otras inscripciones. Sustituye RN-0099, DEC-018.
- **CON-014:** Club inactivo puede recibir pagos de cuotas preexistentes; no genera nuevas cuotas. Preservar historial y reactivar sin retroactivos. Sustituye RN-0014.
- **CON-015:** Tres modalidades de beneficio pueden aplicarse al mes actual impago o siguiente, a elección del administrador autorizado. No modificar meses anteriores ni cuotas pagadas mediante esta operación. Historial y un beneficio activo por inscripción; cambios de base de categoría desde mes siguiente. DEC-030/031/034.
- **CON-016:** Recalcular recargos porcentuales sobre importe actualizado por beneficio; conservar política histórica, vencimiento, gracia, mora acumulada y recargos fijos; registrar importes antes y después. DEC-034/043/045.
- **CON-017:** Cada cuota cancelada por pasarela cuenta como un pago para mínimo comercial, incluso en checkout agrupado. Excluir pagos externos y rechazados; mantener comisión y período mensual. DEC-046/047/049/058/059.
- **CON-018:** Devolución total confirmada de cuota descuenta del período original si abierto; si cerrado, ajuste en período abierto sin modificar directamente liquidación cerrada. Trazabilidad pago, cuota, devolución y ajuste.
- **CON-019:** Contracargo pendiente mantiene cómputo; disputa ganada lo conserva; reversión definitiva total descuenta según CON-018. Ajustes auditados e idempotentes.
- **CON-021:** Ajustes comerciales durante período de gracia se registran sin penalización ni traslado posterior para penalizar. Conservar historial.
