# Cierre de R0 — Preparación de la implementación

**Estado:** cerrado  
**Fecha:** 2026-11-26  
**Baseline funcional:** `baseline-v1`  
**Autorización de H1:** no concedida

## Propósito

Verificar el cumplimiento de los entregables y del gate de salida de R0 sin modificar la baseline, DEC, RN, CON, SPEC, ACCEPTANCE ni ADR aprobados.

El cierre de R0 no autoriza código productivo, despliegues, contratación de infraestructura, tratamiento de datos reales ni operaciones con dinero real.

## Verificación de entregables

| Entregable de `ROADMAP-v1` | Resultado | Evidencia principal |
|---|---|---|
| Alcance de la primera entrega | Cumplido | R0-D01 |
| Responsables técnicos, funcionales y de seguridad | Cumplido | R0-D02, R0-D16 y R0-D17 |
| Repositorio, ramas, revisión y protección | Cumplido | R0-D03–R0-D05, R0-D18–R0-D20, R0-D26, ruleset `main-protection` y PR #1 |
| Plataforma CI/CD y verificaciones obligatorias | Cumplido | R0-D05, R0-D06, R0-D24 y workflow `R0 Documentation CI` |
| Versiones iniciales y paquetes principales | Cumplido | R0-D07, R0-D08 y R0-D21–R0-D22 |
| Revisión inicial de licencias | Cumplido para entrada a implementación | R0-D09, R0-D23 y `LICENSE_REVIEW.md`; PAR-024 corresponde a R1 |
| Proveedores y región propuestos | Cumplido como propuesta previa a contratación | R0-D10 y R0-D11 |
| Convención de trazabilidad | Cumplido | R0-D12 y `TRACEABILITY_CONVENTION.md` |
| Registro de parámetros pendientes | Cumplido | R0-D13 y `PARAMETER_REGISTER.md` |
| Estimaciones por escenarios | Cumplido sin inventar capacidad ni fechas | R0-D14, R0-D15 y `ESTIMATION_SCENARIOS.md` |
| Inicio de validación de Mercado Pago | Cumplido para R0 | R0-D25 y `MERCADO_PAGO_VALIDATION_PLAN.md`; cierre exigible en R8 |

## Verificación de los seis gates previos a implementación

| Gate de `ARCHITECTURE_CLOSURE.md` | Estado | Evidencia |
|---|---|---|
| 1. Roadmap y orden de construcción | Cumplido | `ROADMAP-v1`, H0 cerrado |
| 2. Alcance de primera entrega y criterios de salida | Cumplido | R0-D01 |
| 3. Repositorio, CI básico y política de ramas | Cumplido | R0-D03–R0-D06, R0-D24, R0-D26 y PR #1 |
| 4. Versiones iniciales y revisión de licencias | Cumplido | R0-D07–R0-D09 y R0-D21–R0-D23 |
| 5. Matriz SPEC/AC por incremento | Cumplido | Matriz de cobertura de `ROADMAP-v1` y R0-D12 |
| 6. Responsables técnicos y de seguridad | Cumplido | R0-D02, R0-D16 y R0-D17 |

## Evidencia del gate de repositorio y CI

El repositorio público `Tom09t/TuCuota`, bajo GitHub Free, tiene activo el ruleset `main-protection` sobre `main`. Exige pull request, historial lineal, conversaciones resueltas, rama actualizada, squash y el check `ci-gate`; además bloquea eliminación y force-push.

El PR #1 proporcionó evidencia efectiva:

- la ejecución `45860262871` falló y bloqueó el merge;
- la ejecución `45860513027` pasó después de corregir el código de salida del validador;
- la ejecución final `45877103981`, correspondiente al registro de evidencia, terminó satisfactoriamente;
- GitHub informó todos los checks aprobados y ofreció exclusivamente `Squash and merge`.

PAR-003 queda cerrado para R0. Debe revalidarse antes de Producción o si cambia el plan o la visibilidad del repositorio.

## Parámetros que permanecen abiertos

Los parámetros abiertos de `PARAMETER_REGISTER.md` conservan su propietario y momento límite. No bloquean el cierre de R0 cuando su vencimiento pertenece a R1 o a un gate posterior.

En particular:

- PAR-008 y PAR-009 impiden comprometer fechas hasta conocer capacidad y calendario;
- PAR-020 y PAR-021 continúan en análisis y bloquean dinero real hasta R8;
- PAR-024 exige la revisión transitiva de licencias antes de aceptar R1;
- los parámetros legales, productivos, de recuperación, observabilidad y correo mantienen los gates posteriores documentados.

Ningún parámetro abierto se considera aprobado por presunción ni puede reducir un requisito vigente.

## Resultado

R0 queda cerrado porque todos sus entregables y los seis gates previos a implementación fueron verificados.

H1 permanece bloqueado. Su autorización debe ser explícita, separada y posterior a este cierre. Hasta entonces no se generará código productivo.

También permanecen prohibidos los despliegues productivos y la habilitación de dinero real hasta sus respectivos gates y autorizaciones.

## Trazabilidad

- `baseline-v1`.
- CON-011 y DEC-170 con sus RN vigentes.
- SPEC-001–SPEC-034.
- ACCEPTANCE-001–ACCEPTANCE-034.
- ADR-0001–ADR-0017.
- `docs/architecture/ARCHITECTURE_CLOSURE.md`.
- `docs/roadmap/ROADMAP-v1.md`.
- R0-D01–R0-D26.
