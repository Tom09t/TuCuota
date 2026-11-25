# SPEC-028 — Alcance funcional inicial del MVP

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Delimitar las capacidades esenciales del MVP y las condiciones que impiden confundir una entrega funcional con autorización para operar dinero real.

## Capacidades esenciales

El MVP incluye:

- Administración de clubes y disciplinas con usuarios, roles, permisos y aislamiento.
- Gestión de jugadores, inscripciones y categorías.
- Configuración y generación automática de cuotas.
- Becas y beneficios económicos esenciales.
- Registro de pagos externos.
- Portal del responsable para consultar y pagar cuotas autorizadas.
- Reportes financieros básicos por disciplina y conciliación esencial de pagos y estados financieros.
- Pagos online solo mediante una integración Split Payment técnicamente validada.

El detalle funcional de estas capacidades permanece definido por las especificaciones aprobadas que las desarrollan; esta especificación no las reemplaza ni reduce.

## Controles obligatorios desde el MVP

- Autenticación, autorización, seguridad y aislamiento se aplican desde la primera entrega.
- Toda operación financiera conserva trazabilidad y auditoría.
- Los comandos de pago incorporan idempotencia y prevención de cobros duplicados.
- Un pago incierto se resuelve o controla de forma segura antes de habilitar un nuevo intento.
- Devoluciones y contracargos cuentan desde el lanzamiento con procedimientos operativos controlados, aunque sus módulos avanzados se implementen después.

## Capacidades diferibles

Pueden entregarse en versiones posteriores:

- Reportes avanzados.
- Automatizaciones.
- Exportaciones.
- Gestión avanzada de devoluciones.

El diferimiento no elimina requisitos aprobados ni permite incompatibilidades futuras. Tampoco posterga los controles mínimos de seguridad financiera ni los procedimientos operativos obligatorios.

## Bloqueo de cobros reales

- La existencia del software, una integración en sandbox o el cierre documental no autorizan cobros reales.
- Antes de habilitarlos deben validarse las capacidades reales de la pasarela, la distribución de fondos y la conciliación.
- Esta condición se complementa con los criterios de lanzamiento y aceptación de DEC-162–DEC-170; hasta verificarlos, el sistema permanece sin dinero real.

## Invariantes

- El producto se desarrolla y lanza por etapas.
- Split Payment validado es condición de los pagos online del MVP.
- Ningún módulo diferido debilita seguridad, trazabilidad, auditoría o conciliación.
- Aprobación documental, despliegue técnico y habilitación financiera son hitos distintos.
- Las capacidades postergadas conservan compatibilidad con todas las decisiones funcionales vigentes.

## Pendientes de definición

- Descomposición técnica y orden de implementación de las capacidades del MVP.
- Criterio operativo para clasificar futuras extensiones como básicas o avanzadas cuando no esté determinado por una DEC/RN vigente.
- Cronograma y compromisos de entrega, que pertenecen al roadmap posterior a la baseline funcional.

Estos pendientes no modifican el alcance funcional aprobado ni autorizan arquitectura, despliegues o cobros reales.

## Trazabilidad

- DEC-161.
- RN-1713–RN-1731.
- `docs/product/MVP.md`.
- DEC-162–DEC-170 para condiciones posteriores de lanzamiento y aceptación.
- `MATERIAL_REVIEW_08.md`.

