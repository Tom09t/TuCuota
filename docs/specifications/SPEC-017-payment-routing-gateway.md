# SPEC-017 — Enrutamiento de pagos e integración receptora

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir el agrupamiento por disciplina, las cuentas receptoras y los controles para integrar una pasarela con Split Payment.

## Operación por disciplina

- Un responsable puede agrupar cuotas completas de varios jugadores vinculados solo si pertenecen a una misma disciplina.
- Cada operación tiene una disciplina; cuotas de disciplinas distintas requieren pagos independientes.
- El detalle conserva jugador y cuota, y comisiones, costos, pagos y conciliaciones se identifican por disciplina.
- El portal puede mostrar todas las disciplinas autorizadas sin mezclar sus cobros.
- Una falla de una disciplina no cambia operaciones de otra y se evitan cobros duplicados.
- Conforme a CON-017, cada cuota cancelada cuenta individualmente para el mínimo comercial aunque comparta checkout.

## Cuenta receptora

- Cada disciplina tiene configuración independiente y puede usar cuenta exclusiva o compartir una con otras disciplinas del club.
- Compartir cuenta no mezcla operaciones, liquidaciones, conciliaciones, reportes ni permisos.
- Los cambios conservan historia; los pagos iniciados mantienen la configuración con la que fueron creados y los nuevos usan la vigente validada.
- Sin una configuración receptora operativa exigida por la pasarela no se inician nuevos cobros.

## Administración reforzada

- El administrador registra o reemplaza la cuenta únicamente en su disciplina, sin aprobación adicional del superadministrador.
- La activación valida datos, requisitos técnicos y titularidad del proveedor, además de un OTP enviado al correo verificado del administrador.
- El OTP es de uso único, vence y tiene límites de intentos y reenvíos; no reemplaza controles del proveedor.
- Si la validación falla, la nueva cuenta no se activa y se conserva la anterior si continúa operativa.
- Se auditan administrador, disciplina, fecha, hora y cuenta anterior/nueva sin credenciales sensibles; administrador y superadministrador reciben aviso de seguridad.

## Integración y Split Payment

- Cada disciplina mantiene una conexión lógicamente independiente, priorizando autorización oficial como OAuth.
- No se solicita contraseña personal cuando existe autorización oficial; tokens y credenciales se protegen fuera del frontend.
- Solo se habilitan integraciones compatibles con el Split Payment acordado.
- Se separan importe de cuota, comisión de plataforma del 5% y costo de pasarela.
- Los fondos de cuota se acreditan directamente al destinatario mediante la pasarela, sin custodia de TuCuota.
- Una conexión revocada o inutilizable bloquea nuevos pagos, sin alterar historia.
- La implementación definitiva depende de capacidades técnicas y contractuales y de las validaciones de ADR-0002 y DEC-170.

## Invariantes

- Un checkout nunca mezcla disciplinas.
- Compartir cuenta receptora nunca comparte autorización ni información administrativa.
- Cambiar receptor o conexión no reescribe pagos iniciados o históricos.
- Credenciales sensibles no se exponen ni se auditan en claro.
- Una conexión no operativa no procesa nuevos cobros.

## Pendientes de definición

- Parámetros del OTP administrativo.
- Datos y evidencias de titularidad exigidos para activar una cuenta.
- Flujo concreto de OAuth, renovación y revocación con Mercado Pago.
- Modalidad de checkout y validación contractual final de Split Payment.

Estos puntos requieren aprobación funcional, técnica o contractual antes de cobros reales.

## Trazabilidad

- DEC-121–DEC-125.
- RN-1062–RN-1133.
- CON-017.
- ADR-0002, DEC-170 y `MATERIAL_REVIEW_07.md`.
