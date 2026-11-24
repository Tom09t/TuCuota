# Alcance del MVP

**Estado:** propuesta derivada pendiente de aprobación final.
**Fuente normativa:** DEC-161–DEC-170 y sus RN vigentes, bajo CON-011.

## Objetivo

Habilitar un piloto controlado de Obras Futsal sobre una plataforma multiclub, sin procesar cobros reales hasta satisfacer íntegramente los controles de DEC-170.

## Capacidades incluidas

- Administración de clubes, disciplinas, categorías, jugadores e inscripciones.
- Usuarios, autenticación, roles, permisos y aislamiento por club y disciplina.
- Configuración y generación automática de cuotas.
- Becas, descuentos, importes individuales y recargos esenciales.
- Portal de responsables con vinculaciones autorizadas, consulta y pago de cuotas.
- Registro auditado de pagos externos.
- Pagos online únicamente con Split Payment validado técnica, operativa, comercial y legalmente.
- Conciliación esencial, idempotencia, prevención de cobros duplicados y tratamiento de pagos inciertos.
- Reportes financieros básicos y trazabilidad de operaciones.
- Procedimientos operativos ejecutables para devoluciones, contracargos e incidentes financieros.
- Migración inicial controlada, validada, idempotente y auditada.
- Separación entre Desarrollo y Producción, CI/CD controlado, backups y monitoreo.

## Capacidades diferidas

- Módulos avanzados de devoluciones y contracargos.
- Reportes y exportaciones avanzadas.
- Automatizaciones y clasificación avanzada de incidentes no exigidas para el piloto.
- Entorno Staging independiente.

El diferimiento no elimina reglas de seguridad, auditoría, conservación, conciliación ni procedimientos operativos obligatorios desde el lanzamiento.

## Lanzamiento progresivo

1. Grupo controlado de jugadores y responsables de Obras Futsal.
2. Resto de Obras Futsal, con autorización del superadministrador.
3. Otras disciplinas de Obras Mendoza.
4. Otros clubes después de validar el piloto.

Cada ampliación requiere validación previa, evidencias y autorización conforme a DEC-163.

## Condiciones de salida

Antes de habilitar cobros reales deben verificarse, como mínimo:

- Funcionalidades esenciales, autenticación, autorización y aislamiento.
- Split Payment, distribución de fondos, comisión y condiciones aplicables.
- Cobros, reintentos, idempotencia, pagos inciertos y conciliación.
- Migración inicial aprobada.
- Backups, restauración, monitoreo y alertas operativos.
- Procedimientos de incidentes, devoluciones y contracargos documentados.
- Aprobación formal del responsable técnico y autorización del superadministrador.

Cualquier requisito crítico incumplido bloquea nuevos cobros reales. Los pendientes no críticos solo pueden aceptarse si quedan documentados, evaluados y autorizados.

## Decisiones técnicas y límites

El stack seleccionado es .NET 10 / ASP.NET Core Web API con C#, React con TypeScript, PostgreSQL y Docker. La API utilizará OpenAPI/Swagger. Mercado Pago Split Payments 1:1 es la pasarela seleccionada, sujeta a validación técnica, comercial y legal.

Continúan pendientes el hosting, la infraestructura y las decisiones técnicas detalladas. Este documento no autoriza despliegues ni dinero real.

**Trazabilidad:** DEC-161–DEC-170, RN-1713–RN-1889.

