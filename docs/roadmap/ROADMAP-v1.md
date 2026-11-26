# Roadmap de implementación v1

**Estado:** aprobado

**Fecha:** 2026-11-26

**Baseline funcional:** `baseline-v1`

**Arquitectura:** cerrada mediante ADR-0001–ADR-0017

**Hito alcanzado:** H0 — Roadmap aprobado

## Propósito

Ordenar la construcción del MVP y del piloto de Obras Futsal por incrementos verificables, haciendo explícitas sus dependencias, entregables, criterios de salida y trazabilidad normativa.

Este documento no altera DEC, RN, CON, SPEC, ACCEPTANCE, ADR ni `baseline-v1`. Tampoco autoriza implementación, despliegue o dinero real. Las duraciones y fechas se estimarán después de aprobar este orden y conocer equipo, capacidad, proveedor y restricciones operativas.

## Principios de ejecución

- Cada incremento entrega una capacidad vertical demostrable: dominio, API, interfaz, persistencia, seguridad, auditoría, observabilidad y pruebas aplicables.
- Aislamiento multitenant, autorización por ámbito, auditoría, idempotencia y observabilidad se incorporan desde el primer incremento que los necesita; no se postergan como endurecimiento final.
- Una SPEC solo se considera implementada cuando pasan sus ACCEPTANCE correspondientes y existe evidencia trazable.
- PostgreSQL real se utiliza en pruebas de integración y aislamiento. Los dobles se reservan para fronteras externas controladas.
- Las migraciones de datos y esquema son versionadas, repetibles y verificadas antes de cualquier despliegue productivo.
- Producción técnica y habilitación de dinero real son hitos independientes.
- Toda evidencia nueva que contradiga una decisión arquitectónica requiere revisar o crear un ADR; el roadmap no resuelve esa contradicción por presunción.

## Caminos críticos

```text
Gobierno y fundación
  ├─ Tenant e identidad ─ Padrón deportivo ─ Cuotas ─ Portal del responsable
  │                                             └────── Pagos externos
  └─ Plataforma operativa ─ Pasarela ─ Estados inciertos ─ Conciliación
                                                       └─ Gate dinero real

Migración inicial ─ validación de saldos ─ candidato de Producción ─ piloto controlado
Backups + restauración + monitoreo + seguridad ────────────────┘
```

## Flujo de estados

Cada incremento atraviesa estos estados:

1. **Preparado:** alcance, SPEC/AC, responsables, dependencias y riesgos identificados.
2. **En construcción:** implementación y evidencia técnica en curso.
3. **Verificado:** pruebas automáticas y revisiones del incremento aprobadas.
4. **Aceptado:** criterios funcionales demostrados y evidencia vinculada.
5. **Liberable:** además cumple los controles operativos acumulados exigibles al ambiente destino.

«Aceptado» no significa «desplegado», y «desplegado» no significa «habilitado para dinero real».

## Incrementos

### R0 — Preparación de la implementación

**Objetivo:** cerrar las condiciones de entrada que no cambian el alcance funcional ni la arquitectura.

**Entregables:**

- Alcance aprobado de la primera entrega y responsables técnicos, funcionales y de seguridad.
- Repositorio de implementación, política de ramas, revisión y protección de cambios.
- Plataforma CI/CD y matriz inicial de verificaciones obligatorias.
- Versiones fijadas de .NET, PostgreSQL, Node, paquetes principales y revisión de licencias.
- Proveedores y región propuestos para ejecución, PostgreSQL, storage, secretos/KMS, correo y observabilidad.
- Registro de parámetros pendientes, con propietario y fecha límite para cada decisión.
- Convención para vincular cambios, pruebas y evidencia con SPEC/AC/ADR.

**Gate de salida R0:** se satisfacen los seis gates «antes de implementación» de `ARCHITECTURE_CLOSURE.md`; cualquier decisión material nueva queda registrada mediante ADR.

### R1 — Fundación ejecutable y controles transversales

**Objetivo:** establecer la solución modular y un recorrido técnico mínimo sin funcionalidad financiera real.

**Entregables:**

- Solución .NET 10 con hosts API y Worker, módulos y pruebas según ADR-0004–ADR-0006.
- SPA React/TypeScript, cliente OpenAPI y shell de navegación según ADR-0014.
- PostgreSQL con esquema por módulo, migraciones y convenciones EF Core según ADR-0007–ADR-0008.
- Contexto de tenant, RLS inicial, auditoría, outbox/inbox, idempotencia y concurrencia como componentes reutilizables.
- OpenTelemetry, logs estructurados, correlación y health checks iniciales.
- Desarrollo containerizado y pipeline de compilación, análisis, pruebas y artefactos reproducibles.
- Secretos fuera del repositorio, escaneo de dependencias e imágenes y baseline de seguridad.

**Trazabilidad principal:** ADR-0001, ADR-0003–ADR-0017; base transversal de SPEC/ACCEPTANCE-001, 030, 032 y 034.

**Gate de salida R1:** recorrido API–base–frontend observable; pruebas de arquitectura y aislamiento base; pipeline bloquea fallas obligatorias; no existen secretos versionados.

### R2 — Tenancy, identidad y autorización

**Objetivo:** habilitar clubes, disciplinas, cuentas y ámbitos de acceso antes de almacenar operaciones de negocio.

**Entregables:**

- Administración de clubes, disciplinas, slugs, identidad visual y estados.
- Autenticación, recuperación, sesión revocable y protección antiforgery.
- Roles de plataforma, club y disciplina; políticas por recurso y protecciones de últimos administradores/delegados.
- Aislamiento EF y RLS con pruebas negativas entre tenants y disciplinas.
- Auditoría de altas, bajas, restauraciones, permisos y cambios sensibles.

**Trazabilidad:** SPEC-001 / ACCEPTANCE-001; CON-005–CON-008, CON-012 y CON-014; ADR-0007, ADR-0009, ADR-0013 y ADR-0017.

**Gate de salida R2:** ACCEPTANCE-001 aprobada; ninguna ruta privada depende solo de ocultamiento de interfaz; pruebas demuestran denegación cruzada en API y PostgreSQL.

### R3 — Padrón deportivo e importación estructural

**Objetivo:** construir el padrón confiable sobre el cual se generan cuotas y se vinculan responsables.

**Entregables:**

- Jugadores globales, pertenencias, inscripciones por disciplina, categorías y bajas lógicas.
- Restricciones de unicidad, historial y separación por club/disciplina.
- Importación preliminar de jugadores, categorías e inscripciones con previsualización, errores, duplicados, aprobación e idempotencia.
- Evidencia de origen y responsable de cada lote.

**Trazabilidad:** SPEC-002 / ACCEPTANCE-002; parte estructural de SPEC-033 / ACCEPTANCE-033; CON-013; ADR-0011.

**Gate de salida R3:** ACCEPTANCE-002 aprobada y criterios estructurales de ACCEPTANCE-033 demostrados sin importar todavía saldos productivos.

### R4 — Cuotas, beneficios y política comercial

**Objetivo:** producir obligaciones financieras determinísticas y auditables antes de incorporar cobros online.

**Entregables:**

- Configuración, generación programada y estados de cuotas.
- Becas, importes fijos, porcentajes, importes individuales, vencimientos, gracia y mora.
- Política comercial, mínimos, comisiones, períodos, ajustes y renovaciones contractuales.
- Jobs durables, reintentos seguros, historial de cálculo y pruebas de reloj/calendario.
- Pruebas de precisión monetaria, límites, invariantes y concurrencia.

**Trazabilidad:** SPEC/ACCEPTANCE-003, 005, 006 y 007; CON-003, CON-015–CON-017 y CON-021; ADR-0010–ADR-0012.

**Gate de salida R4:** las cuatro ACCEPTANCE están aprobadas; una regeneración o reintento no duplica cuotas; cada importe puede reconstruirse desde su evidencia histórica.

### R5 — Portal del responsable, pagos externos y lectura financiera básica

**Objetivo:** completar el circuito operativo esencial sin depender todavía de una pasarela real.

**Entregables:**

- Vinculación autorizada responsable–jugador y acceso acotado a cuotas.
- Portal responsive con consulta de deuda y estados.
- Registro auditado e idempotente de pagos externos, aplicación a cuotas y reversión controlada cuando corresponda.
- Tableros y reportes financieros básicos por ámbito, con definiciones consistentes.
- Notificaciones operativas mínimas necesarias para estos flujos.

**Trazabilidad:** SPEC/ACCEPTANCE-004, 008, 013 y 016; SPEC-028 / ACCEPTANCE-028 para el recorrido esencial del MVP.

**Gate de salida R5:** ACCEPTANCE-004, 008, 013 y 016 aprobadas en su alcance MVP; demostración integral padrón → cuota → consulta → pago externo → reporte, sin dinero real.

### R6 — Pasarela, pagos online y conciliación esencial

**Objetivo:** implementar y validar en sandbox la frontera financiera sin habilitar cobros reales.

**Entregables:**

- Adaptador Mercado Pago detrás de contratos propios y checkout Split Payment 1:1.
- Webhooks autenticados, inbox/outbox, idempotencia y máquina de estados de pago.
- Distribución de fondos y comisión modeladas y verificables.
- Disponibilidad de pasarela, bloqueo seguro, transiciones, pagos inciertos y consulta de estado antes de reintentar.
- Evidencia financiera inmutable y correlacionada.
- Conciliación esencial, diferencias, alertas operativas y resolución trazable.
- Runbooks ejecutables de incidentes financieros, devoluciones y contracargos exigidos desde el lanzamiento.

**Trazabilidad:** SPEC/ACCEPTANCE-009, 010, 014, 017, 018, 019 y 020; alcance obligatorio de SPEC/ACCEPTANCE-028 y 034; ADR-0002, ADR-0010–ADR-0013.

**Gate de salida R6:** todas las ACCEPTANCE indicadas pasan en sandbox y pruebas controladas; se demuestra que timeout, webhook duplicado, reintento y concurrencia no producen doble cobro lógico. Este gate no autoriza dinero real.

### R7 — Plataforma productiva, recuperación y migración completa

**Objetivo:** preparar un candidato desplegable y recuperable con datos iniciales controlados.

**Entregables:**

- Ambientes Desarrollo y Producción separados, despliegue con aprobación humana y migraciones protegidas.
- Infraestructura administrada, secretos/KMS, cifrado, mínimos privilegios, MFA y rotación ensayada.
- PITR de 30 días, doce copias mensuales, alertas de backup y pruebas de restauración.
- Monitoreo productivo, correo técnico, deduplicación y registro de resolución.
- Migración completa de jugadores, inscripciones, beneficios, cuotas pendientes y pagos externos históricos confiables.
- Validación de saldos, restauración seguida de conciliación y procedimiento de reanudación.
- Revisión ASVS, análisis de dependencias/imágenes y penetration test del candidato.

**Trazabilidad:** SPEC/ACCEPTANCE-030, 031, 032 y 033; ADR-0013, ADR-0016 y ADR-0017.

**Gate de salida R7:** las cuatro ACCEPTANCE están aprobadas; restauración y conciliación posterior ensayadas; migración repetible sin duplicados; no se habilitan credenciales financieras reales a la aplicación mientras R8 permanezca abierto.

### R8 — Candidato MVP y gate de dinero real

**Objetivo:** auditar el candidato integral y decidir explícitamente si puede comenzar el piloto con dinero real.

**Entregables:**

- Matriz completa SPEC/AC/implementación/prueba/evidencia para el alcance MVP.
- Validación técnica, operativa, comercial, contractual y legal de Split Payment, distribución, comisión, devoluciones y contracargos.
- Checklist ejecutable de DEC-170 con responsable, evidencia, vigencia y resultado por control.
- Evaluación formal de pendientes; ningún requisito crítico puede exceptuarse.
- Aprobación del responsable técnico y autorización separada del superadministrador.
- Mecanismo probado de suspensión y reanudación de nuevas operaciones.

**Trazabilidad:** SPEC/ACCEPTANCE-028 y 034; DEC-170; ADR-0002; todas las SPEC/AC del MVP de las que dependen.

**Gate de salida R8:** ACCEPTANCE-028 y ACCEPTANCE-034 aprobadas con evidencia vigente y ambas autorizaciones. Si falla un control crítico, el sistema puede desplegarse para validación sin dinero real, pero los cobros continúan bloqueados.

### R9 — Piloto controlado y ampliaciones

**Objetivo:** operar el lanzamiento progresivo sin confundir capacidad multiclub con expansión automática.

**Entregables:**

- Grupo inicial de Obras Futsal definido y habilitado.
- Tablero de verificaciones, incidentes y evidencia del piloto.
- Solicitud, evaluación y autorización auditada de cada ampliación.
- Observación de cuotas, permisos, pagos, fondos, conciliación, duplicados e incertidumbre.
- Retrospectiva y decisión explícita para resto de Obras Futsal, otras disciplinas y otros clubes.

**Trazabilidad:** SPEC-029 / ACCEPTANCE-029; DEC-162, DEC-163 y DEC-170.

**Gate de salida R9:** ACCEPTANCE-029 aprobada para la etapa correspondiente; no hay diferencias críticas ni errores bloqueantes abiertos; cada ampliación conserva evidencia y autorización del superadministrador.

### R10 — Capacidades avanzadas posteriores al MVP

**Objetivo:** completar capacidades diferibles sin erosionar los controles ya operativos.

**Entregables:**

- Historial, exportaciones y almacenamiento asíncrono avanzados.
- Sincronización y entrega avanzada de notificaciones.
- Gestión completa de devoluciones, componentes, costos, solicitudes, políticas, evidencia e historial.
- Gestión de contracargos y disputas integrada a conciliación y política comercial.

**Trazabilidad:** SPEC/ACCEPTANCE-011, 012, 015 y 021–027; CON-009, CON-018–CON-020.

**Gate de salida R10:** ACCEPTANCE asociadas aprobadas; los módulos avanzados respetan evidencia histórica, autorizaciones, idempotencia, ajustes de períodos cerrados y aislamiento.

## Dependencias entre incrementos

| Incremento | Requiere | Puede avanzar en paralelo con |
|---|---|---|
| R0 | Arquitectura cerrada | — |
| R1 | R0 | — |
| R2 | R1 | preparación operativa de R7 |
| R3 | R2 | observabilidad e infraestructura de R7 |
| R4 | R2 y R3 | portal base de R5 |
| R5 | R3 y R4 | validaciones comerciales/técnicas de R6 |
| R6 | R2, R4 y contratos de R5 | infraestructura, backups y monitoreo de R7 |
| R7 | R1 y capacidades a migrar de R3–R6 | cierre de evidencia R6 |
| R8 | R2–R7 | — |
| R9 | R8 aprobado | preparación no productiva de R10 |
| R10 | controles financieros consolidados en R6–R9 | ampliaciones posteriores autorizadas |

La paralelización no permite integrar una capacidad si su dependencia todavía no cumple el gate correspondiente.

## Matriz de cobertura normativa

| SPEC / ACCEPTANCE | Incremento primario | Evidencia de aceptación esperada |
|---|---|---|
| 001 | R2 | aislamiento, roles, sesiones y auditoría por ámbito |
| 002 | R3 | ciclo de jugador, inscripción y categoría |
| 003 | R4 | cálculo, generación, beneficio y mora |
| 004 | R5 | pago externo y aplicación a cuota |
| 005–007 | R4 | política, ajustes, gracia, contratos y renovaciones |
| 008 | R5 | tablero/reporte básico consistente |
| 009–010 | R6 | conciliación, diferencias, operación y alertas |
| 011–012 | R10 | historial, exportación, job y storage |
| 013 | R5 | notificación operativa mínima |
| 014 | R6 | eventos e incidentes financieros |
| 015 | R10 | entrega y sincronización avanzada |
| 016 | R5 | acceso autorizado del responsable |
| 017–020 | R6 | routing, disponibilidad, incertidumbre y evidencia |
| 021–027 | R10 | devoluciones, contracargos, costos, solicitudes y gestión |
| 028 | R5–R8 | recorrido esencial y controles acumulados del MVP |
| 029 | R9 | piloto y ampliación autorizada |
| 030–032 | R7 | despliegue, recuperación y monitoreo productivo |
| 033 | R3 y R7 | importación estructural y migración financiera final |
| 034 | R8 | checklist integral y autorizaciones |

Todas las filas refieren a la SPEC y a la ACCEPTANCE del mismo número. La asignación indica orden de implementación, no modifica alcance ni precedencia.

## Estrategia de verificación acumulativa

| Nivel | Desde | Evidencia mínima |
|---|---|---|
| Dominio | R2 | reglas e invariantes determinísticos |
| Integración | R1 | EF Core y PostgreSQL real, migraciones y RLS |
| Contratos | R1 | OpenAPI y contratos entre módulos/adaptadores |
| Arquitectura | R1 | límites de módulos y dependencias permitidas |
| Seguridad | R1 | autorización negativa, aislamiento, antiforgery y escaneos |
| Financiera | R4 | precisión, idempotencia, concurrencia y estados |
| End-to-end | R5 | recorridos críticos por rol y ámbito |
| Resiliencia | R6 | duplicados, timeout, reintentos, recuperación y conciliación |
| Operativa | R7 | despliegue, restore, rotación, alertas y runbooks |
| Aceptación | cada R | escenarios de ACCEPTANCE y evidencia enlazada |

Los defectos críticos de aislamiento, autorización, integridad financiera, restauración o secretos bloquean el incremento; no se trasladan como deuda aceptable a una etapa posterior.

## Parámetros por cerrar y momento límite

| Parámetro | Momento límite | Tratamiento si permanece abierto |
|---|---|---|
| Equipo, capacidad y calendario | antes de estimar fechas | roadmap permanece sin fechas |
| Proveedores, región y residencia | R0; contratación antes de R7 | bloquea infraestructura productiva |
| Versiones exactas y licencias | R0 | bloquea inicio de construcción |
| Biblioteca visual | antes de cerrar R1 | bloquea componentes definitivos, no contratos |
| CI/CD, ramas y aprobadores | R0 | bloquea integración productiva |
| SLO, umbrales y destinatarios | antes de cerrar R7 | bloquea ACCEPTANCE-032 |
| RPO/RTO adicionales | antes de cerrar R7 | no reduce PITR 30 días ni 12 copias mensuales |
| Reglas de migración pendientes | antes de ejecutar R7 | bloquea importación definitiva |
| Evidencia y umbrales del piloto | antes de R8 | bloquea autorización y ampliaciones |
| Validación Mercado Pago | iniciar en R0; cerrar en R8 | bloquea dinero real |
| Conservación legal | antes de R7 | se conserva de forma precautoria hasta definirla |

## Hitos de gobierno

- **H0 — Roadmap aprobado:** se acepta este orden, sus gates y la cobertura normativa.
- **H1 — Implementación autorizada:** R0 cerrado y autorización explícita separada para comenzar código productivo.
- **H2 — MVP funcional sin dinero real:** R1–R6 aceptados y demostrados en ambiente controlado.
- **H3 — Candidato de Producción:** R7 cerrado y candidato recuperable, monitoreado y seguro.
- **H4 — Dinero real autorizado:** R8 cerrado con evidencia y las dos aprobaciones obligatorias.
- **H5 — Piloto validado:** etapa correspondiente de R9 cerrada.
- **H6 — Capacidades avanzadas:** R10 priorizado y entregado por subconjuntos aceptables.

Cada hito requiere acta o registro equivalente con alcance, versión, evidencia, riesgos aceptados y aprobadores. H0 no implica H1; H3 no implica H4.

## Riesgos principales

| Riesgo | Respuesta del roadmap |
|---|---|
| Split Payment no resulta técnica, comercial o legalmente viable | validación desde R0; adaptador aislado; bloqueo explícito en R8; cualquier cambio de pasarela requiere decisión aprobada |
| Fuga entre clubes o disciplinas | tenant obligatorio, políticas por recurso, RLS y pruebas negativas desde R1–R2 |
| Doble cobro o estado incierto | idempotencia, inbox/outbox, máquina de estados y conciliación antes de habilitar online |
| Cálculos financieros irreproducibles | historial de política e importes, precisión monetaria y evidencia desde R4 |
| Migración altera saldos | preview, aprobación, idempotencia y conciliación en R3/R7 |
| Recuperación reejecuta operaciones | restore ensayado, conciliación obligatoria y reanudación controlada en R7 |
| Infraestructura tardía revela incompatibilidades | selección en R0 y trabajo paralelo desde R2 |
| Capacidades avanzadas retrasan el piloto | se difiere interfaz avanzada, pero runbooks y controles mínimos entran en R6 |

## Exclusiones de esta versión

- Fechas, duración, costo y asignación nominal de personas.
- Selección definitiva de proveedores o contratos comerciales.
- Backlog de historias, tareas técnicas o diseño detallado de pantallas.
- Modificación de requisitos o reclasificación unilateral de controles críticos.
- Autorización para implementar, desplegar o procesar dinero real.

## Aprobación

`ROADMAP-v1` fue aprobado el 2026-11-26. La aprobación confirma únicamente:

1. El orden R0–R10 y sus dependencias.
2. Los gates de salida y los hitos H0–H6.
3. La asignación de SPEC/ACCEPTANCE a incrementos.
4. La separación entre MVP funcional, Producción técnica y dinero real.

H0 queda cerrado. El paso siguiente es ejecutar y cerrar R0 y presentar para aprobación separada el alcance de la primera entrega, la capacidad del equipo y una estimación por escenarios. El inicio de código productivo continúa bloqueado hasta la autorización explícita de H1.
