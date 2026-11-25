# SPEC-029 — Piloto progresivo y autorización de ampliaciones

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir las etapas del piloto, las verificaciones obligatorias y la autoridad necesaria para incorporar nuevos participantes, disciplinas o clubes.

## Etapas del lanzamiento

1. El lanzamiento comienza con un grupo limitado de jugadores y responsables de Obras Futsal.
2. Tras validar las operaciones esenciales, se incorpora progresivamente el resto de Obras Futsal.
3. Posteriormente pueden habilitarse otras disciplinas de Obras Mendoza.
4. La expansión comercial a otros clubes ocurre después de validar el piloto.

La plataforma conserva capacidad multiclub y aislamiento entre clubes y disciplinas desde la primera versión, aunque el acceso se habilite gradualmente.

## Validación del piloto

Durante cada etapa se comprueba:

- Generación correcta de cuotas.
- Accesos y permisos de administradores y responsables.
- Procesamiento de pagos online y funcionamiento de Split Payment.
- Llegada de fondos a las cuentas correspondientes.
- Conciliación entre pagos, cuotas y operaciones de la pasarela.
- Prevención de cobros duplicados y tratamiento de pagos inciertos.
- Ausencia de diferencias críticas de conciliación y errores bloqueantes sin resolver.
- Autenticación, autorización y aislamiento entre clubes y disciplinas.

Los incidentes detectados se registran y analizan. Cada verificación conserva evidencia o referencias a sus resultados.

## Solicitud y autorización

- Un administrador de disciplina puede solicitar una ampliación.
- Solo el superadministrador puede aprobar el avance entre etapas.
- El sistema presenta el estado de todas las verificaciones requeridas.
- Cualquier criterio obligatorio incumplido bloquea la ampliación y no puede ser omitido por un administrador.
- La aprobación o el rechazo registra solicitante, superadministrador decisor, fecha, motivo y resultado.
- Los resultados y decisiones permanecen disponibles para auditoría.

## Integridad histórica

- Incorporar participantes no altera pagos ni registros financieros existentes.
- La ampliación habilita alcance futuro; no recalcula ni reasigna operaciones anteriores.
- Autorizar una etapa no sustituye los controles individuales exigibles a cada operación financiera.
- El piloto solo puede operar dinero real cuando también se hayan satisfecho las condiciones aplicables de DEC-170.

## Invariantes

- Obras Futsal es la primera disciplina habilitada.
- La arquitectura multiclub no implica lanzamiento comercial inmediato a múltiples clubes.
- Ninguna ampliación ocurre sin validación previa y autorización del superadministrador.
- Una validación sin evidencia no se considera cumplida.
- Un criterio obligatorio fallido mantiene bloqueada la ampliación.
- La ampliación nunca modifica registros financieros históricos.

## Pendientes de definición

- Composición y cantidad inicial del grupo controlado.
- Formato operativo de la solicitud de ampliación.
- Evidencias concretas exigibles para cada verificación y responsables de producirlas.
- Definición cuantitativa de umbrales, períodos de observación y severidad de errores, incluida la clasificación de «crítico» y «bloqueante».
- Cronograma de las etapas.

Estos puntos requieren aprobación posterior y no pueden utilizarse para omitir controles vigentes.

## Trazabilidad

- DEC-162–DEC-163.
- RN-1732–RN-1767.
- DEC-161 y `SPEC-028` para alcance del MVP.
- DEC-170 para habilitación de cobros reales.
- `docs/product/MVP.md` y `MATERIAL_REVIEW_08.md`.

