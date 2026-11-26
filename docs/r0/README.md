# R0 — Preparación de la implementación

**Estado:** en curso  
**Roadmap:** `docs/roadmap/ROADMAP-v1.md`  
**Hito habilitado:** ninguno; H1 permanece bloqueado

## Propósito

Este directorio registra los acuerdos operativos y de planificación necesarios para cerrar R0. No modifica `baseline-v1`, DEC, RN, CON, SPEC, ACCEPTANCE, ADR ni el roadmap aprobado.

Las decisiones arquitectónicas materiales nuevas se documentarán mediante ADR. Los acuerdos de alcance, gobierno y ejecución propios de R0 se registran aquí para evitar ADR artificiales.

## Estado de los gates previos a implementación

| Gate | Estado |
|---|---|
| Roadmap y orden de construcción por capacidades | Cumplido mediante `ROADMAP-v1` |
| Alcance de la primera entrega y criterios de salida | Cumplido mediante R0-D01 |
| Repositorio, CI básico y política de ramas | Cumplido mediante R0-D04–R0-D06, R0-D24, R0-D26 y PR #1 |
| Versiones iniciales fijadas y revisión de licencias | Cumplido para entrada a implementación mediante R0-D07–R0-D09 y R0-D21–R0-D23; revisión transitiva obligatoria en R1 |
| Matriz SPEC/AC por incremento | Cumplido a nivel de roadmap; convención operativa de evidencia pendiente |
| Responsables de decisiones técnicas y de seguridad | Cumplido mediante R0-D02, R0-D16 y R0-D17 |

R0 no está cerrado. El inicio de código productivo requiere, además de cerrar todos los gates, una autorización explícita y separada de H1.

## Decisiones aprobadas

### R0-D26 — Repositorio público con GitHub Free hasta el gate productivo

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** acuerdo operativo de repositorio; no requiere ADR

Se mantendrá el repositorio público bajo GitHub Free durante la implementación y preproducción para disponer de las protecciones de rama y rulesets exigidas por R0-D04.

#### Condiciones

- Antes de autorizar H1 deben configurarse y probarse PR obligatorio, `ci-gate`, bloqueo de push directo, force-push y eliminación de `main`.
- No se versionarán secretos, datos personales, datos financieros, credenciales, evidencia sensible ni exportaciones privadas.
- La ausencia de una licencia de software no concede permiso de uso; antes de incorporar código productivo se documentará la reserva de derechos aplicable.
- Antes de Producción se decidirá entre mantener el repositorio público o contratar un plan compatible y convertirlo a privado sin perder protecciones.
- Un repositorio privado bajo GitHub Free no será aceptable para Producción mientras no soporte y demuestre las protecciones aprobadas.
- El cambio futuro de visibilidad deberá preservar historia, tags, Actions, evidencia y trazabilidad, y requerirá una prueba posterior de controles.

#### Consecuencias

- R0-D19 y R0-D20 quedan reemplazadas en cuanto exigen privacidad antes de H1.
- PAR-003 deja de estar bloqueado y queda cerrado tras la prueba efectiva de las protecciones en el PR #1.
- Esta decisión no cierra R0 ni autoriza H1, despliegues o dinero real.

#### Evidencia de configuración

El 2026-11-26 se creó en `Tom09t/TuCuota` el ruleset activo `main-protection`, aplicado a la rama predeterminada `main`, sin lista de bypass, con:

- eliminación y force-push bloqueados;
- historial lineal y pull request obligatorio;
- cero aprobaciones independientes, conforme al modelo de responsable único;
- conversaciones resueltas y squash como único método de merge;
- rama actualizada y `ci-gate` satisfactorio antes del merge.

La prueba efectiva se realizó en el PR #1 el 2026-11-26. La ejecución inicial `45860262871` falló y GitHub mantuvo bloqueado el merge; después de corregir el código de salida del validador, la ejecución `45860513027` completó satisfactoriamente el check requerido `ci-gate`. El panel de merge confirmó `All checks have passed`, ausencia de conflictos y `Squash and merge` como único método disponible. Con esta evidencia PAR-003 queda cerrado, sujeto a revalidación antes de Producción o ante un cambio de visibilidad o plan.

#### Riesgos y tratamiento

- **Exposición pública accidental:** controles de secretos, revisión de contenido y exclusión estricta de fuentes privadas y evidencia sensible.
- **Copia del código:** reserva de derechos explícita; la visibilidad pública no equivale a una licencia abierta.
- **Pérdida de controles al cambiar a privado:** verificar el plan y probar las reglas inmediatamente después del cambio.

#### Trazabilidad

- R0-D03–R0-D06 y R0-D18–R0-D20.
- ADR-0016 y ADR-0017.
- SPEC-030 y SPEC-034.
- ACCEPTANCE-030 y ACCEPTANCE-034.
- PAR-003 y DEC-170.

### R0-D25 — Plan integral de validación de Mercado Pago

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** acuerdo operativo de validación; no requiere ADR

Se aprueba `docs/r0/MERCADO_PAGO_VALIDATION_PLAN.md` para iniciar de forma coordinada las validaciones técnica, comercial, contractual, legal, operativa y de seguridad de Mercado Pago Marketplace Split Payments 1:1.

#### Alcance

- PAR-020 y PAR-021 pasan a `En análisis`; no se consideran cerrados.
- La modalidad Checkout Pro, Checkout API o Bricks continúa pendiente de evidencia comparativa en sandbox.
- Las pruebas se limitarán a ambientes y fondos de prueba; no se autorizan cobros reales.
- La revisión legal independiente y la autorización del superadministrador permanecen obligatorias antes de dinero real.
- Una incompatibilidad no permite cambiar silenciosamente comisión, flujo de fondos ni reglas financieras.

#### Riesgos y dependencias

- Elegibilidad comercial y contractual para Argentina y para las cuentas receptoras previstas.
- Acceso a sandbox y diferencias respecto del comportamiento productivo.
- Cambios de API, producto o términos del proveedor.
- Asesoría legal aún no designada y parámetros funcionales pendientes.

#### Trazabilidad

- ADR-0002.
- SPEC-017–SPEC-019 y ACCEPTANCE-017–ACCEPTANCE-019.
- SPEC-030, SPEC-031 y SPEC-034; ACCEPTANCE-030, ACCEPTANCE-031 y ACCEPTANCE-034.
- DEC-170 y sus RN vigentes, bajo la precedencia de CON-011.
- PAR-020–PAR-022.

### R0-D01 — Alcance de la primera entrega

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** acuerdo operativo de alcance; no requiere ADR

La primera entrega posterior a una eventual autorización de H1 será **R1 completo — Fundación ejecutable y controles transversales**, según `ROADMAP-v1`.

Incluye:

- Hosts API y Worker y estructura modular según ADR-0004–ADR-0006.
- Shell SPA React/TypeScript y cliente OpenAPI según ADR-0003 y ADR-0014.
- PostgreSQL, esquemas por módulo, migraciones y convenciones EF según ADR-0007–ADR-0008.
- Infraestructura reutilizable de tenant, RLS, auditoría, outbox/inbox, idempotencia y concurrencia aplicable.
- OpenTelemetry, logs estructurados, correlación y health checks iniciales.
- Desarrollo containerizado y pipeline inicial de compilación, análisis, pruebas y artefactos reproducibles.
- Secretos fuera del repositorio y controles iniciales de cadena de suministro.

Pueden utilizarse checkpoints internos para obtener feedback temprano, pero no constituyen entregas aceptadas ni reducen el gate de R1.

#### Criterios de salida

R1 solo se considera entregado cuando:

1. Existe un recorrido API–PostgreSQL–frontend observable.
2. Pasan las pruebas de arquitectura y aislamiento base aplicables.
3. El pipeline bloquea ante verificaciones obligatorias fallidas.
4. No existen secretos versionados.
5. Existe evidencia trazable para los controles aplicables de SPEC/ACCEPTANCE-001, 030, 032 y 034, identificando cobertura parcial cuando corresponda.
6. La entrega no incluye funcionalidad financiera real, despliegue productivo ni habilitación de dinero real.

#### Riesgos y tratamiento

- **Sobrecarga de R1:** limitar el alcance al recorrido técnico mínimo definido por el roadmap.
- **Abstracciones prematuras:** implementar únicamente capacidades exigidas por los ADR y controles aplicables a R1.
- **Confusión entre base técnica y aceptación funcional completa:** declarar explícitamente cobertura parcial de AC cuando el comportamiento completo pertenezca a incrementos posteriores.

#### Dependencias

Antes de iniciar R1 deben cerrarse los demás entregables y gates de R0 y aprobarse H1 de forma explícita y separada.

#### Trazabilidad

- `docs/roadmap/ROADMAP-v1.md`: R0, R1 y matriz de cobertura normativa.
- ADR-0001, ADR-0003–ADR-0017.
- SPEC-001, SPEC-030, SPEC-032 y SPEC-034.
- ACCEPTANCE-001, ACCEPTANCE-030, ACCEPTANCE-032 y ACCEPTANCE-034.
- DEC-164–DEC-170 y sus RN vigentes, bajo la precedencia de CON-011.

### R0-D02 — Modelo de responsables

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** acuerdo operativo de gobierno; no requiere ADR

Durante R0 y R1 una única persona concentrará las responsabilidades técnica, funcional y de seguridad.

#### Responsabilidades acumuladas

- Aplicación de la arquitectura, calidad técnica, CI/CD, releases y aprobación técnica.
- Interpretación normativa, control de alcance y aceptación funcional de SPEC/ACCEPTANCE.
- Threat modeling, ASVS, secretos, vulnerabilidades, excepciones y evidencia de seguridad.

#### Límites

- La concentración de funciones no reduce verificaciones, evidencia ni revisiones obligatorias.
- Una aprobación documental o técnica no autoriza H1, despliegue productivo ni dinero real.
- La autorización del superadministrador exigida por SPEC-034 y ACCEPTANCE-034 permanece separada y no puede ser absorbida por este rol.
- Las vulnerabilidades críticas, exposiciones de secretos y fallas de controles obligatorios continúan siendo bloqueantes.
- Toda excepción técnica o de seguridad debe registrar riesgo, compensaciones, responsable y vencimiento.

#### Pendientes para completar el gate

- Identidad nominal de la persona responsable.
- Medio formal y trazable de aprobación.
- Suplente o procedimiento de continuidad ante ausencia o indisponibilidad.
- Revisión del modelo si aumenta el equipo, el riesgo o la proximidad a Producción.

Hasta resolver estos puntos, el gate de responsables permanece parcial.

#### Riesgos aceptados

- Conflicto de intereses por autorrevisión técnica, funcional y de seguridad.
- Dependencia de una única persona y ausencia de continuidad automática.
- Menor independencia para detectar interpretaciones o controles incorrectos.
- Posibilidad de revisiones meramente formales por sobrecarga.

#### Trazabilidad

- ADR-0015, ADR-0016 y ADR-0017.
- SPEC-030, SPEC-032 y SPEC-034.
- ACCEPTANCE-030, ACCEPTANCE-032 y ACCEPTANCE-034.
- DEC-165, DEC-168 y DEC-170 y sus RN vigentes, bajo la precedencia de CON-011.

### R0-D03 — Repositorio de implementación

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** acuerdo operativo de repositorio; no requiere ADR

La documentación normativa y la implementación futura residirán en el repositorio actual. Después de cerrar R0 y recibir autorización explícita de H1 podrán incorporarse el código, las pruebas, la infraestructura y la automatización correspondientes.

#### Condiciones

- No se incorporará código productivo antes de la autorización explícita de H1.
- `baseline-v1` permanece inmutable.
- Los cambios de implementación no pueden reescribir silenciosamente DEC, RN, CON, SPEC, ACCEPTANCE, ADR ni el roadmap aprobado.
- Cada cambio deberá citar los identificadores SPEC, AC y ADR aplicables, además de los DEC/RN/CON cuando corresponda una referencia normativa directa.
- Cada artefacto de release identificará commit, baseline normativa aplicable, pruebas, evidencia y aprobación.
- La rama principal no admitirá escritura directa y quedará sujeta a la política de protección que se apruebe en R0.
- La estructura técnica futura respetará ADR-0004–ADR-0006 y se incorporará sin reescribir la historia documental.

#### Riesgos y tratamiento

- **Modificación accidental de documentos aprobados:** protección por rutas, revisión obligatoria y validaciones automatizadas de integridad.
- **Permisos excesivos:** mínimo privilegio y propietarios de revisión para rutas sensibles.
- **Crecimiento del repositorio:** mantener límites claros entre documentación, código, pruebas, infraestructura y evidencia.

#### Dependencias

Esta decisión requiere completar la política de ramas, revisión, propietarios de rutas, protección de cambios y plataforma CI/CD. No autoriza la creación de código productivo.

#### Trazabilidad

- `docs/roadmap/ROADMAP-v1.md`: R0 y R1.
- ADR-0004–ADR-0006 y ADR-0015–ADR-0017.
- SPEC-030 y SPEC-034.
- ACCEPTANCE-030 y ACCEPTANCE-034.

### R0-D18 — Visibilidad y plan de GitHub

**Estado:** confirmado  
**Fecha:** 2026-11-26  
**Tipo:** constatación operativa; no requiere ADR

El repositorio `Tom09t/TuCuota` es público y su titular utiliza GitHub Free. Para un repositorio público, el plan permite aplicar las protecciones de rama, status checks y ambientes requeridos por R0-D04 y R0-D05.

PAR-003 queda cerrado. La configuración efectiva y su prueba continúan siendo evidencia necesaria antes de declarar cumplido el gate de repositorio y CI.

La visibilidad pública exige decidir antes de H1 la licencia o reserva de derechos aplicable al código futuro; la ausencia de una licencia de software no se interpretará como autorización de uso.

**Actualización por R0-D19:** se aprobó convertir el repositorio en privado antes de H1. La constatación de compatibilidad para el estado público deja de ser suficiente y PAR-003 se reabre hasta seleccionar un plan que soporte las protecciones requeridas en repositorios privados.

#### Trazabilidad

- R0-D03–R0-D06.
- ADR-0016 y ADR-0017.
- SPEC-030 y ACCEPTANCE-030.

### R0-D19 — Visibilidad privada antes de H1

**Estado:** reemplazada por R0-D26  
**Fecha:** 2026-11-26  
**Tipo:** acuerdo operativo de repositorio; no requiere ADR

El repositorio deberá convertirse en privado antes de autorizar H1. No se adopta una licencia abierta para el código futuro mediante esta decisión.

#### Condiciones

- El cambio de visibilidad debe preservar historia, tags y trazabilidad.
- Antes del cambio se verificará el impacto sobre rulesets, ambientes, Actions, artefactos, enlaces y accesos.
- Después del cambio se probarán pull request obligatorio, `ci-gate`, bloqueo de push directo, ambientes y permisos mínimos.
- La combinación actual de repositorio privado y GitHub Free no se considera suficiente hasta demostrar que soporta todas las protecciones aprobadas; en caso contrario deberá cambiarse el plan.
- La conversión no autoriza código productivo ni H1.

#### Riesgos y tratamiento

- **Pérdida o desactivación de protecciones:** seleccionar un plan compatible y probar las reglas después del cambio.
- **Enlaces públicos rotos:** inventariar referencias externas antes de convertir.
- **Acceso accidental:** revisar colaboradores, tokens, Actions y artefactos.

#### Dependencias

- Decisión de plan GitHub para repositorio privado.
- Configuración y prueba efectiva de R0-D04–R0-D06.

#### Trazabilidad

- R0-D03–R0-D06 y R0-D18.
- ADR-0016 y ADR-0017.
- SPEC-030 y ACCEPTANCE-030.

### R0-D20 — Mantener GitHub Free con repositorio privado

**Estado:** reemplazada por R0-D26  
**Fecha:** 2026-11-26  
**Tipo:** restricción operativa; no requiere ADR

Se selecciona mantener GitHub Free al convertir el repositorio en privado. Esta combinación no dispone de las protecciones de rama privada requeridas por R0-D04 según las capacidades verificadas al tomar la decisión.

#### Consecuencia

- PAR-003 queda bloqueado.
- El gate de repositorio, CI básico y política de ramas permanece pendiente.
- R0 no puede cerrarse y H1 no puede autorizarse mientras persista la incompatibilidad.
- No se sustituye protección efectiva por una declaración manual ni por confianza en el responsable único.
- El repositorio permanece en su estado público actual hasta ejecutar de forma explícita la conversión; convertirlo a privado no resolvería el gate.

#### Formas futuras de desbloqueo

- Revisar R0-D19 y mantener el repositorio público con GitHub Free.
- Revisar R0-D20 y adoptar un plan compatible con repositorios privados protegidos.
- Aportar evidencia oficial y una prueba efectiva de que GitHub Free incorporó las protecciones requeridas para repositorios privados.

#### Trazabilidad

- R0-D03–R0-D06 y R0-D18–R0-D19.
- ADR-0016 y ADR-0017.
- SPEC-030 y ACCEPTANCE-030.

### R0-D04 — Ramas, revisión y protección de cambios

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** acuerdo operativo de integración; no requiere ADR

Se adopta un flujo trunk-based con `main` como única rama permanente, ramas de trabajo cortas y pull request obligatorio para todo cambio.

#### Reglas de integración

- Una rama corta por cambio o conjunto cohesivo de cambios.
- Integración mediante squash merge y eliminación posterior de la rama de trabajo.
- Checks obligatorios satisfactorios y conversaciones resueltas antes del merge.
- La rama de trabajo debe estar actualizada respecto de `main` antes de integrar.
- No se permite escritura directa, force-push ni eliminación de `main`.
- El bypass administrativo permanece deshabilitado salvo que se apruebe posteriormente un procedimiento de emergencia auditable.

#### Revisión con responsable único

Mientras solo exista la persona responsable definida por R0-D02, podrá revisar y fusionar su propio pull request únicamente después de completar el checklist, obtener todos los checks obligatorios y conservar evidencia. Esta autorrevisión no se presenta como revisión independiente.

Cuando exista otra persona técnicamente habilitada, los cambios sensibles requerirán revisión independiente. R0-D17 aclara que no se prevé otro mantenedor interno: la continuidad operativa se resolverá mediante pausa segura. Permanecen vigentes las revisiones externas exigidas por los gates aplicables, incluido el penetration test independiente previo a dinero real.

#### Protecciones documentales y de seguridad

- CI verificará la integridad de `baseline-v1` contra `SHA256SUMS`.
- Los cambios a SPEC, ACCEPTANCE, ADR aceptados o al roadmap requieren aprobación normativa explícita y no pueden incorporarse silenciosamente con una implementación.
- Secret scanning y detección de credenciales serán bloqueantes.
- Las reglas de protección y checks obligatorios se aplicarán desde la plataforma Git/CI que se seleccione en R0.

#### Riesgos aceptados

- La autorrevisión inicial puede omitir errores o conflictos de interés.
- La continuidad depende de una única persona hasta definir suplencia.
- Una configuración incorrecta de protecciones puede permitir eludir el flujo aprobado.

Los pull requests, checklists, checks bloqueantes y evidencia reducen estos riesgos, pero no sustituyen revisión independiente.

#### Dependencias

- Plataforma Git y CI/CD.
- Matriz exacta de verificaciones obligatorias.
- Identidad nominal del responsable y futura persona revisora habilitada.

#### Trazabilidad

- R0-D02 y R0-D03.
- ADR-0015, ADR-0016 y ADR-0017.
- SPEC-030 y ACCEPTANCE-030.
- `docs/architecture/ARCHITECTURE_CLOSURE.md`.

### R0-D05 — Plataforma CI/CD

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** acuerdo operativo de plataforma; no requiere ADR

Se adopta GitHub Actions como plataforma CI/CD para el repositorio alojado en GitHub.

#### Condiciones de seguridad y operación

- Los workflows se definirán en YAML versionado en el repositorio.
- Los permisos predeterminados serán de solo lectura; cualquier elevación se declarará por job y con mínimo privilegio.
- Las acciones externas se fijarán mediante SHA inmutable y se revisarán antes de incorporarlas.
- Los pull requests no confiables no recibirán secretos ni identidades con privilegios.
- Las futuras conexiones cloud utilizarán OIDC y credenciales de corta duración, no claves cloud persistentes.
- Construcción, Desarrollo y Producción utilizarán identidades separadas.
- Los artefactos validados se promoverán sin reconstrucción y conservarán commit, digest, SBOM, procedencia, pruebas y aprobación.
- Los ambientes y la aprobación productiva utilizarán protecciones nativas cuando la visibilidad y el plan contratado lo permitan.

#### Pendientes para completar el gate

- Confirmar visibilidad y plan del repositorio y verificar que soportan las protecciones aprobadas.
- Aprobar la matriz concreta de verificaciones obligatorias y su distribución por evento.
- Seleccionar proveedor cloud antes de configurar trusts OIDC y despliegues.
- Definir retención de artefactos y evidencia.

#### Riesgos y tratamiento

- **Acción externa comprometida:** fijación por SHA, revisión y allowlist.
- **Permisos excesivos:** permisos mínimos por workflow y job.
- **Exposición desde pull requests:** separación de eventos confiables y ausencia de secretos en código no confiable.
- **Consumo inesperado o límites del plan:** medir duración, concurrencia y almacenamiento antes de fijar presupuesto.
- **Dependencia de plataforma:** workflows y artefactos reproducibles, sin acoplar la aplicación a GitHub.

#### Trazabilidad

- R0-D03 y R0-D04.
- ADR-0015, ADR-0016 y ADR-0017.
- SPEC-030 y SPEC-034.
- ACCEPTANCE-030 y ACCEPTANCE-034.
- DEC-165 y DEC-170 y sus RN vigentes, bajo la precedencia de CON-011.

### R0-D10 — Nube principal y región

**Estado:** aprobada como propuesta primaria  
**Fecha:** 2026-11-26  
**Tipo:** selección operativa de proveedor dentro de ADR-0016 y ADR-0017; no requiere un nuevo ADR

Se propone AWS en la región South America (São Paulo), código `sa-east-1`, como nube principal. Esta aprobación permite continuar el diseño y la evaluación de R0, pero no constituye contratación ni autoriza despliegues.

#### Condiciones

- Datos, PostgreSQL, objetos, secretos, claves, logs y backups primarios residirán en `sa-east-1`.
- La Local Zone de Buenos Aires no se utilizará como ubicación principal porque depende de una región padre y no sustituye los servicios regionales administrados requeridos.
- PostgreSQL 18 utilizará PITR configurado con retención de 30 días.
- Se conservarán doce snapshots mensuales independientes mediante AWS Backup y Vault Lock o un control equivalente verificable.
- Desarrollo y Producción utilizarán cuentas o límites administrativos separados, con recursos, identidades, secretos y datos independientes.
- Las claves KMS se separarán por ambiente y se aplicará mínimo privilegio.
- GitHub Actions accederá mediante OIDC, sin access keys cloud persistentes.
- Toda copia fuera de `sa-east-1` queda bloqueada hasta aprobar residencia legal, RPO/RTO y región secundaria.
- Antes de contratar se verificarán disponibilidad exacta de servicios, PostgreSQL 18, cuotas, costos, soporte, DPA y residencia.
- Si AWS no satisface un control obligatorio, Google Cloud Santiago queda como alternativa a evaluar sin reducir ADR-0016 ni ADR-0017.

#### Riesgos y tratamiento

- **Costo regional:** estimar con escenarios y alertas presupuestarias antes de aprovisionar.
- **Residencia fuera de Argentina:** revisión legal y contractual antes de datos reales.
- **Dependencia de proveedor:** conservar contenedores, PostgreSQL, S3-compatible boundaries cuando proceda y OTLP.
- **Pérdida regional:** no afirmar recuperación regional hasta aprobar ubicación secundaria, residencia y RPO/RTO.

#### Dependencias

- Validación legal de residencia y DPA.
- Presupuesto y capacidad esperada.
- RPO/RTO adicionales.
- Verificación comercial y técnica del catálogo en `sa-east-1`.
- Selección concreta de servicios administrados.

#### Trazabilidad

- R0-D05 y R0-D07.
- ADR-0013, ADR-0016 y ADR-0017.
- SPEC-030–SPEC-032 y SPEC-034.
- ACCEPTANCE-030–ACCEPTANCE-032 y ACCEPTANCE-034.
- DEC-166–DEC-170 y sus RN vigentes, bajo la precedencia de CON-011.

### R0-D12 — Convención de trazabilidad

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** acuerdo operativo de trazabilidad; no requiere ADR

Se adopta un modelo híbrido verificable: las referencias permanecen junto al cambio, la prueba y la evidencia, y una matriz consolidada permite comprobar cobertura y referencias huérfanas.

La convención completa se documenta en `docs/r0/TRACEABILITY_CONVENTION.md`.

#### Trazabilidad

- ADR-0015–ADR-0017.
- SPEC-001–SPEC-034 y ACCEPTANCE-001–ACCEPTANCE-034.
- DEC-170 y sus RN vigentes, bajo la precedencia de CON-011.
- R0-D04–R0-D06.

### R0-D13 — Registro de parámetros pendientes

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** acuerdo operativo de seguimiento; no requiere ADR

Se adopta un registro central versionado por gate. Puede vincularse con issues u otras herramientas futuras, pero esas herramientas no sustituyen la foto documental y auditable del repositorio.

El registro se mantiene en `docs/r0/PARAMETER_REGISTER.md`.

#### Reglas

- Cada parámetro usa un identificador `PAR-###` inmutable.
- El propietario identifica un rol y, cuando el gate lo requiera, una persona nominal; no se admite “equipo”.
- Mientras no exista calendario aprobado, el momento límite se expresa mediante gate o incremento.
- Un parámetro vencido bloquea el gate indicado y no se cierra por presunción.
- El cierre requiere evidencia vinculada y conserva historial.
- El registro no resuelve por sí mismo contradicciones ni parámetros funcionales materiales.

#### Trazabilidad

- `docs/architecture/ARCHITECTURE_CLOSURE.md`.
- `docs/roadmap/ROADMAP-v1.md`.
- SPEC-030–SPEC-034 y ACCEPTANCE-030–ACCEPTANCE-034.
- R0-D02–R0-D12.

### R0-D14 — Método de estimación por escenarios

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** acuerdo operativo de planificación; no requiere ADR

Se adopta la estimación de esfuerzo mediante rangos y tres escenarios, sin convertirlos en duración calendario mientras permanezcan abiertos PAR-008 y PAR-009.

El método completo se documenta en `docs/r0/ESTIMATION_SCENARIOS.md`. La aprobación del método no aprueba automáticamente las cifras que se propongan posteriormente.

#### Trazabilidad

- `docs/roadmap/ROADMAP-v1.md`.
- R0-D01 y R0-D10–R0-D13.
- PAR-008, PAR-009 y PAR-023.
- Gates de `docs/architecture/ARCHITECTURE_CLOSURE.md`.

### R0-D15 — Estimación inicial de R1

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** estimación operativa; no requiere ADR

Se aprueba la estimación inicial de R1 mediante rangos amplios por frente. La confianza inicial es baja y las cifras no se convierten en duración ni fechas.

El detalle se conserva en `docs/r0/ESTIMATION_SCENARIOS.md`.

#### Trazabilidad

- R0-D01, R0-D06–R0-D08 y R0-D10–R0-D14.
- ADR-0004–ADR-0017.
- R1 de `docs/roadmap/ROADMAP-v1.md`.

### R0-D16 — Responsable nominal de R0/R1

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** designación operativa; no requiere ADR

Se designa a **Tomás, titular del proyecto y usuario solicitante**, como responsable único técnico, funcional y de seguridad durante R0 y R1, conforme al modelo aprobado en R0-D02.

La designación no elimina los riesgos de autorrevisión, no sustituye la autorización separada del superadministrador y no autoriza H1, Producción ni dinero real.

PAR-001 queda cerrado. PAR-002 permanece abierto hasta aprobar suplencia o un procedimiento de continuidad.

#### Trazabilidad

- R0-D02.
- ADR-0015–ADR-0017.
- SPEC-030, SPEC-032 y SPEC-034.
- ACCEPTANCE-030, ACCEPTANCE-032 y ACCEPTANCE-034.

### R0-D17 — Continuidad del responsable único

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** acuerdo operativo de continuidad; no requiere ADR

El proyecto será mantenido únicamente por Tomás y no se prevé incorporar otro mantenedor. Se adopta una política de pausa segura sin suplente.

#### Procedimiento ante indisponibilidad

- Se detienen merges, cambios normativos, despliegues, excepciones y aprobaciones.
- No se comparten cuentas ni credenciales para simular continuidad.
- GitHub, AWS y los demás proveedores conservarán mecanismos de recuperación de cuenta protegidos mediante MFA y códigos de recuperación almacenados fuera del repositorio.
- Al reanudar, Tomás verificará accesos, auditoría, cambios, secretos, dependencias y estado de los gates antes de continuar.
- Ninguna automatización podrá convertir la ausencia del responsable en una aprobación implícita.

#### Límites

- No existe continuidad operativa inmediata durante la indisponibilidad.
- La autorrevisión aprobada en R0-D04 continúa identificándose como tal.
- No se exige incorporar otro mantenedor interno antes de Producción.
- Sí permanecen obligatorias las revisiones externas definidas por los gates, incluidas la revisión jurídica cuando corresponda y el penetration test independiente antes de dinero real.
- La autorización separada del superadministrador exigida por SPEC-034 continúa vigente y se resolverá en R8; no se presume en R0.

#### Riesgo aceptado

El proyecto depende totalmente de una persona y puede quedar detenido por un período indefinido ante su indisponibilidad. La pausa segura protege integridad y atribución, pero no proporciona continuidad de servicio humano.

PAR-002 queda cerrado mediante esta decisión.

#### Trazabilidad

- R0-D02, R0-D04 y R0-D16.
- ADR-0016 y ADR-0017.
- SPEC-030 y SPEC-034.
- ACCEPTANCE-030 y ACCEPTANCE-034.

### R0-D11 — Servicios administrados en AWS

**Estado:** aprobada como propuesta de servicios  
**Fecha:** 2026-11-26  
**Tipo:** selección operativa dentro de ADR-0016 y ADR-0017; no requiere un nuevo ADR

Se propone el siguiente stack administrado en `sa-east-1`:

| Capacidad | Servicio propuesto |
|---|---|
| Contenedores | Amazon ECS sobre AWS Fargate |
| Registro privado | Amazon ECR |
| Entrada HTTPS | Application Load Balancer y ACM |
| PostgreSQL | Amazon RDS for PostgreSQL 18 |
| Object storage | Amazon S3 |
| Secretos | AWS Secrets Manager |
| Claves | AWS KMS |
| Correo | Amazon SES |
| Logs y métricas | Amazon CloudWatch |
| Trazas | OpenTelemetry/ADOT hacia AWS X-Ray |
| Backups mensuales | AWS Backup y Vault Lock |

#### Topología propuesta

- API y web se ejecutarán como servicios ECS/Fargate detrás de un Application Load Balancer.
- El worker será un servicio Fargate sin ingreso funcional público.
- ECR será privado y sus imágenes se analizarán antes de promoción.
- Producción utilizará RDS PostgreSQL 18 Multi-AZ; Desarrollo podrá utilizar Single-AZ.
- S3 será privado, cifrado con KMS y separado por ambiente y propósito.
- Se habilitará versionado para objetos recuperables. Object Lock solo se aplicará a evidencia que requiera inmutabilidad después de fijar retención, porque su habilitación en el bucket es irreversible.
- Secrets Manager y KMS se separarán por ambiente y carga.
- SES utilizará dominio verificado, SPF, DKIM, DMARC, listas de supresión y salida controlada del sandbox.
- CloudWatch recibirá logs, métricas, alarmas e incidentes; ADOT exportará trazas a X-Ray sin acoplar la instrumentación al proveedor.
- AWS Backup conservará PITR de 30 días y doce snapshots mensuales en un vault protegido.
- No se habilitará replicación fuera de región hasta aprobar residencia y recuperación regional.

#### Parámetros abiertos

- Tamaños, cantidad mínima de tareas y autoscaling.
- Presupuesto de conexiones, almacenamiento y capacidad inicial.
- Retención de logs, métricas, trazas y objetos.
- RPO/RTO adicionales y región secundaria.
- Dominio, volumen de correo y cuotas de SES.
- Presupuesto y alertas de costos.

#### Riesgos y tratamiento

- **Costos de NAT, ALB y telemetría:** estimación por escenarios, límites y alertas antes de aprovisionar.
- **Límites o reputación de SES:** validación de dominio, cuotas, rebotes y supresión antes de uso operativo.
- **Lock-in:** conservar contratos de aplicación propios, contenedores, PostgreSQL y OTLP.
- **Configuración incorrecta de IAM/KMS:** mínimo privilegio, IaC revisada, separación de identidades y pruebas negativas.

#### Dependencias

- Residencia legal y DPA.
- Presupuesto y sizing.
- Retenciones y RPO/RTO.
- Pruebas de restore, rotación, alarmas y entrega de correo.

#### Trazabilidad

- R0-D10.
- ADR-0012, ADR-0013, ADR-0016 y ADR-0017.
- SPEC-012, SPEC-020, SPEC-026 y SPEC-030–SPEC-034.
- ACCEPTANCE-012, ACCEPTANCE-020, ACCEPTANCE-026 y ACCEPTANCE-030–ACCEPTANCE-034.
- DEC-166–DEC-170 y sus RN vigentes, bajo la precedencia de CON-011.

### R0-D07 — Versiones base

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** acuerdo técnico de versionado dentro de la arquitectura aprobada; no requiere ADR

Se fijan las siguientes líneas base:

| Componente | Línea aprobada | Pin inicial de referencia al aprobar R0-D07 |
|---|---:|---:|
| .NET SDK y runtime | 10 LTS | 10.0.15 |
| ASP.NET Core y EF Core | 10.0.x | 10.0.15 |
| PostgreSQL | 18 | 18.7 |
| Node.js | 26 LTS | 26.13.1 |
| npm | Distribuida con el Node fijado | Se fijará al generar el lockfile de R1 |

#### Política de fijación y actualización

- Los majors quedan fijados en .NET 10, PostgreSQL 18 y Node.js 26.
- SDK, runtimes, herramientas e imágenes se fijarán a versión exacta; las imágenes productivas también por digest.
- Los parches de seguridad dentro de una línea aprobada podrán proponerse mediante pull request automatizado, pero requerirán CI completo y posibilidad de reversión.
- Ningún cambio de major ni actualización incompatible se integra automáticamente.
- Desarrollo, CI y Producción utilizarán la misma major de PostgreSQL.
- La disponibilidad y controles de PostgreSQL 18 se comprobarán en los proveedores candidatos.
- Los pins exactos se revalidarán inmediatamente antes de crear R1, porque H1 todavía no está autorizado.

#### Riesgos y tratamiento

- **Proveedor sin PostgreSQL 18 adecuado:** comprobar disponibilidad, PITR, backup y región antes de seleccionarlo.
- **Cambio regresivo en un parche:** CI completo, artefactos reproducibles y rollback compatible.
- **Fin de soporte antes de completar el producto:** registrar fechas de soporte y planificar la revisión sin cambiar de major por presunción.
- **Diferencias entre ambientes:** pins y major homogénea en Desarrollo, CI y Producción.

#### Dependencias

- Selección de proveedor e imágenes base oficiales.
- Selección y revisión de paquetes principales.
- Creación futura de lockfiles y archivos de fijación durante R1, después de autorizar H1.

#### Trazabilidad

- `baseline-v1`.
- R0-D01 y R0-D06.
- ADR-0001, ADR-0005, ADR-0008 y ADR-0014–ADR-0016.

### R0-D08 — Paquetes principales

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** acuerdo técnico de dependencias dentro de la arquitectura aprobada; no requiere ADR

Se adopta un snapshot inicial compatible y estable, evitando cambios de major incompatibles y versiones publicadas sin madurez suficiente.

| Paquete principal | Versión inicial |
|---|---:|
| EF Core / ASP.NET Core | 10.0.15 |
| `Npgsql.EntityFrameworkCore.PostgreSQL` | 10.0.4 |
| Quartz.NET / Hosting | 3.22.10 |
| React / React DOM | 19.3.1 |
| TypeScript | 7.0.2 |
| Vite | 8.3.8 |
| TanStack Query | 5.108.0 |
| Cliente SignalR | ~~10.0.15~~ 10.0.11, corregido por R0-D21 |

Las demás dependencias exigidas por ADR-0014 y ADR-0015 —incluidas React Hook Form, Zod, OpenTelemetry, xUnit v3, Testcontainers, NSubstitute, ArchUnitNET, FsCheck, Stryker, Vitest, Testing Library, MSW, Playwright y axe-core— solo podrán incorporarse mediante inventario aprobado, versión exacta y licencia revisada. Esta decisión no autoriza todavía su instalación.

#### Reglas

- Las dependencias directas usarán versiones exactas, sin rangos flotantes.
- NuGet utilizará administración centralizada y lockfile; npm utilizará `package-lock.json`.
- Las dependencias transitivas quedarán congeladas mediante los lockfiles aplicables.
- Cada actualización requerirá pull request, revisión de changelog, CI y comprobación de licencia.
- Previews, RC, canary y paquetes sin mantenimiento quedan prohibidos salvo excepción aprobada y vencimiento explícito.
- La revalidación previa a R1 podrá proponer parches compatibles, pero no cambiar majors sin otra decisión.
- Una incompatibilidad bloquea el paquete afectado; no permite degradar controles aprobados.

#### Riesgos y tratamiento

- **Compatibilidad aún no ejecutada:** verificar restauración, compilación y suites aplicables al crear la fundación, después de H1.
- **Vulnerabilidades posteriores:** monitoreo automatizado y actualizaciones controladas.
- **Alta frecuencia de cambios frontend:** lockfile, Renovate o Dependabot agrupado y ventanas de actualización.
- **Dependencia abandonada:** revisar mantenimiento, procedencia y alternativa antes de incorporarla.

#### Dependencias

- Política y revisión completa de licencias.
- Creación futura de archivos de administración central y lockfiles, únicamente después de autorizar H1.

#### Trazabilidad

- R0-D06 y R0-D07.
- ADR-0008, ADR-0012–ADR-0015 y ADR-0017.

### R0-D21 — Corrección de SignalR y segundo snapshot de dependencias

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** corrección e inventario técnico; no requiere ADR

R0-D08 contenía una versión npm inexistente para el cliente SignalR. Se corrige `@microsoft/signalr` de `10.0.15` a `10.0.11`, última versión estable verificada de la línea compatible con .NET 10 al aprobar esta decisión. El valor anterior se conserva tachado en R0-D08 para hacer visible la corrección.

Se aprueba además el siguiente snapshot secundario:

| Paquete | Versión | Licencia declarada | Tratamiento inicial |
|---|---:|---|---|
| `@microsoft/signalr` | 10.0.11 | MIT | Admisión ordinaria |
| React Hook Form | 7.92.0 | MIT | Admisión ordinaria |
| Zod | 4.6.7 | MIT | Admisión ordinaria |
| Testcontainers PostgreSQL | 4.17.0 | MIT | Admisión ordinaria |
| FsCheck | 3.6.0 | BSD-3-Clause | Admisión ordinaria |
| Vitest | 5.1.2 | MIT | Admisión ordinaria |
| React Testing Library | 16.3.3 | MIT | Admisión ordinaria |
| MSW | 3.0.7 | MIT | Admisión ordinaria |
| Playwright Test | 1.65.1 | Apache-2.0 | Admisión ordinaria |
| axe-core | 4.14.1 | MPL-2.0 | Revisión expresa |

axe-core se utilizará como herramienta de prueba y no se incorporará por presunción a artefactos productivos. Su forma de uso, distribución, archivos modificados y obligaciones de aviso deberán documentarse antes de cerrar la revisión de licencias.

#### Pendientes

- OpenTelemetry para .NET.
- xUnit v3.
- NSubstitute.
- ArchUnitNET.
- Stryker.NET.
- Dependencias transitivas, imágenes, herramientas y GitHub Actions.

PAR-004 y PAR-005 permanecen abiertos hasta completar esos controles.

#### Trazabilidad

- R0-D08 y R0-D09.
- ADR-0013–ADR-0015 y ADR-0017.

### R0-D23 — Revisión de licencias en dos etapas

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** acuerdo operativo de cumplimiento; no requiere ADR

La revisión se divide en:

1. **Etapa R0:** dependencias directas y herramientas seleccionadas, licencia, procedencia, uso previsto, clasificación y política de bloqueo.
2. **Etapa R1:** grafo transitivo resuelto desde lockfiles, imágenes, paquetes del sistema, GitHub Actions, SBOM y avisos.

La evidencia de la primera etapa se mantiene en `docs/r0/LICENSE_REVIEW.md`. PAR-005 queda cerrado para el gate previo a implementación. PAR-024 controla la segunda etapa y bloquea la aceptación de R1, no la autorización inicial de H1.

axe-core queda permitido como herramienta de prueba sin modificación ni distribución dentro del producto, sujeto a avisos y revisión jurídica antes de Producción. Ninguna dependencia nueva puede incorporarse fuera de R0-D09.

#### Trazabilidad

- R0-D06, R0-D08–R0-D09 y R0-D21–R0-D22.
- ADR-0015 y ADR-0017.

### R0-D24 — CI documental antes de H1

**Estado:** aprobada e implementada localmente  
**Fecha:** 2026-11-26  
**Tipo:** infraestructura documental; no requiere ADR

Se crea un workflow de GitHub Actions y un validador PowerShell que no contienen código productivo ni realizan despliegues.

#### Archivos

- `.github/workflows/r0-documentation-ci.yml`.
- `scripts/validate-r0-docs.ps1`.

#### Controles

- Verificación de los hashes contenidos en la etiqueta inmutable `baseline-v1`.
- Secuencia exacta de SPEC-001–SPEC-034, ACCEPTANCE-001–ACCEPTANCE-034 y ADR-0001–ADR-0017.
- Estados aprobados o aceptados de esos documentos.
- Referencias SPEC, ACCEPTANCE y ADR de los documentos R0.
- Nombres de archivos sensibles y patrones básicos de credenciales.
- Check único y estable `ci-gate`.

La baseline se valida desde la etiqueta, no comparando los archivos actuales con hashes históricos, porque índices y documentos fuera del alcance congelado pueden evolucionar legítimamente.
Los archivos textuales del tag se normalizan a LF antes del hash para neutralizar la conversión de finales de línea aplicada por `git archive`; el manifiesto fue calculado sobre ese contenido normalizado.

#### Límites

- No crea solución, frontend, migraciones ni código productivo.
- No usa secretos ni permisos de escritura.
- No despliega.
- R0-D26 reemplazó la combinación privada + GitHub Free: PAR-003 quedó cerrado al exigir y probar este check en el repositorio público mediante el PR #1.

#### Trazabilidad

- R0-D04–R0-D06, R0-D09 y R0-D23.
- ADR-0015–ADR-0017.
- SPEC-030 y ACCEPTANCE-030.

### R0-D22 — Último grupo de paquetes técnicos

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** inventario técnico; no requiere ADR

Se aprueba el siguiente snapshot estable compatible:

| Paquete | Versión | Licencia declarada | Tratamiento inicial |
|---|---:|---|---|
| OpenTelemetry.Extensions.Hosting | 1.21.0 | Apache-2.0 | Admisión ordinaria |
| xUnit v3 | 4.0.2 | Apache-2.0 | Admisión ordinaria |
| NSubstitute | 6.2.0 | BSD-3-Clause | Admisión ordinaria |
| TngTech.ArchUnitNET | 0.13.5 | Apache-2.0 | Admisión ordinaria |
| dotnet-stryker | 5.0.0 | Apache-2.0, verificada posteriormente en el repositorio oficial | Admisión ordinaria como herramienta |

#### Condiciones

- Los instrumentadores y exporters concretos de OpenTelemetry se fijarán en el mismo nivel compatible cuando se prepare R1.
- Se utilizará la integración de ArchUnitNET compatible con xUnit v3.
- dotnet-stryker será una herramienta programada y no una dependencia de runtime.
- dotnet-stryker se utilizará mediante tool manifest versionado; no será una dependencia de runtime ni se incorporará a imágenes productivas.
- Las versiones se revalidarán inmediatamente antes de la restauración inicial autorizada, sin cambios de major por presunción.

PAR-004 queda cerrado con el inventario aprobado en R0-D08, R0-D21 y R0-D22. PAR-005 permanece en análisis.

#### Trazabilidad

- R0-D08, R0-D09 y R0-D21.
- ADR-0013–ADR-0015 y ADR-0017.

### R0-D09 — Política y revisión de licencias

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** acuerdo operativo de cumplimiento; no requiere ADR

Se adopta una política de licencias por categorías.

#### Admisión ordinaria

MIT, Apache-2.0, BSD-2-Clause, BSD-3-Clause, ISC, PostgreSQL, Zlib y Unicode, sujetas a sus avisos y obligaciones aplicables.

#### Revisión expresa

MPL-2.0, EPL, LGPL, CDDL, licencias duales, BSL/PolyForm y cualquier licencia source-available.

#### Bloqueo sin aprobación jurídica explícita

AGPL, GPL incorporada o distribuida con el producto, SSPL, Commons Clause, licencias personalizadas, desconocidas o ausentes.

La clasificación es un control técnico inicial y no sustituye asesoramiento jurídico ni la revisión de excepciones, avisos o forma de distribución.

#### Revisión preliminar de paquetes principales

| Componente | Licencia declarada |
|---|---|
| .NET, ASP.NET Core, EF Core y SignalR | MIT |
| Npgsql | PostgreSQL License |
| Quartz.NET | Apache-2.0 |
| React / React DOM | MIT |
| TypeScript | Apache-2.0 |
| Vite | MIT |
| TanStack Query | MIT |

No se considera completa la revisión hasta inventariar dependencias transitivas, imágenes, paquetes de sistema operativo, acciones de GitHub y herramientas restantes.

#### Controles obligatorios

- Inventario de dependencias directas y transitivas de NuGet y npm.
- Inventario separado de imágenes base, paquetes del sistema, herramientas y GitHub Actions.
- Identificador SPDX contrastado con el paquete o repositorio oficial.
- SBOM por artefacto y archivo de atribuciones y avisos.
- Cambio de licencia tratado como cambio bloqueante.
- Dependencia sin licencia verificable bloqueada.
- Excepciones con alcance, motivo, evaluación, responsable y vencimiento.
- Revisión jurídica independiente antes de Producción para toda licencia condicional o bloqueada.
- Escaneo automatizado en pull requests y antes de cada release.

#### Riesgos

- Metadatos incorrectos o incompletos.
- Licencias transitivas desconocidas.
- Cambio de licencia entre versiones.
- Obligaciones adicionales derivadas de imágenes o forma de distribución.

#### Dependencias

- Lockfiles futuros.
- Inventario completo de paquetes secundarios.
- Selección de imágenes y proveedores.

#### Trazabilidad

- R0-D06 y R0-D08.
- ADR-0017.
- SPEC-030 y SPEC-034.
- ACCEPTANCE-030 y ACCEPTANCE-034.
- DEC-165 y DEC-170 y sus RN vigentes, bajo la precedencia de CON-011.

### R0-D06 — Matriz inicial de verificaciones obligatorias

**Estado:** aprobada  
**Fecha:** 2026-11-26  
**Tipo:** acuerdo operativo de verificación; no requiere ADR

Se adopta una matriz escalonada por riesgo. Los controles rápidos y críticos se ejecutarán en pull requests; las suites más costosas se distribuirán entre `main`, ejecuciones programadas, candidatos de release y el gate DEC-170.

| Evento | Verificaciones bloqueantes |
|---|---|
| Todo pull request | Integridad de baseline; formato; lint; tipos; compilación; unitarias; componentes; arquitectura; secret scanning; dependencias vulnerables y licencias prohibidas |
| Pull request con implementación | Integración con PostgreSQL real; API; migraciones aplicables; RLS y aislamiento aplicables; OpenAPI y generación reproducible del cliente |
| `main` | Repetición consolidada de checks; imágenes reproducibles; SBOM; análisis de imágenes; E2E críticos disponibles |
| Programada | Dependencias; mutation testing financiero; pruebas autorizadas contra sandbox; controles de resiliencia disponibles |
| Candidato de release | Migraciones; rendimiento; seguridad; despliegue controlado; recuperación aplicable |
| Gate DEC-170 | Matriz completa de controles y evidencia vigente; no forma parte de R1 ni autoriza dinero real |

#### Reglas

- Un check agregado denominado `ci-gate` será el resultado final requerido por la protección de `main` y fallará si cualquier verificación aplicable falla.
- Los jobs no aplicables finalizarán explícitamente como satisfactorios; no quedarán omitidos por filtros de rutas ni se declararán cumplidos.
- Vulnerabilidades críticas, secretos detectados, alteraciones de `baseline-v1` y fallas de aislamiento serán bloqueantes.
- Las pruebas inestables no se reintentarán silenciosamente: generarán un incidente con propietario y vencimiento.
- Las capacidades todavía no implementadas se marcarán como no aplicables, nunca como verificadas.
- Ninguna suite ejecutará smoke tests con dinero real.

#### Riesgos y tratamiento

- **Duración elevada de pull requests:** paralelización y separación de suites costosas sin retirar controles críticos.
- **Consumo de runners:** medición y presupuesto antes de ampliar ejecuciones programadas.
- **Inestabilidad de Testcontainers o E2E:** fixtures determinísticos, diagnóstico conservado y gestión explícita de flakiness.
- **Falsos positivos de seguridad:** triage documentado; una excepción requiere riesgo, responsable, compensación y vencimiento.

#### Dependencias

- Versiones de herramientas y paquetes.
- Política de licencias.
- Configuración efectiva de GitHub y sus protecciones.
- Docker y PostgreSQL disponibles en los runners aplicables.

#### Trazabilidad

- R0-D01 y R0-D04–R0-D05.
- ADR-0015, ADR-0016 y ADR-0017.
- SPEC-001, SPEC-030 y SPEC-034.
- ACCEPTANCE-001, ACCEPTANCE-030 y ACCEPTANCE-034.
- DEC-165 y DEC-170 y sus RN vigentes, bajo la precedencia de CON-011.

