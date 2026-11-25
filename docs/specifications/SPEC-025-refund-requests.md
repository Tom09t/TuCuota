# SPEC-025 — Solicitudes de devolución del responsable

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir creación, evaluación, cancelación y plazo de solicitudes de devolución iniciadas desde el portal.

## Creación

- Un responsable autenticado solicita devoluciones solo para jugadores con vínculo vigente.
- Puede seleccionar una o varias cuotas completas del mismo checkout; no importes arbitrarios ni fracciones de cuota.
- La solicitud exige motivo y se vincula con pago, cuotas y disciplina.
- Crear la solicitud no ejecuta la devolución; los administradores de la disciplina la evalúan.
- No pueden coexistir solicitudes activas duplicadas sobre las mismas cuotas.
- El responsable consulta su estado y recibe aviso al aprobarse, rechazarse o completarse la devolución.

## Decisión

- El administrador autorizado aprueba o rechaza dentro de su disciplina, registrando decisión y justificación.
- La aprobación respeta capacidad de pasarela, Split Payment, reversión de comisión del 5% y asignación de costos.
- Si el costo corresponde a una falla de TuCuota, interviene el superadministrador para autorizarlo.
- Estado de solicitud y estado de devolución efectiva permanecen diferenciados.

## Cancelación

- El responsable cancela su propia solicitud pendiente de revisión o aprobada solo mientras no comenzó la ejecución ante la pasarela.
- En proceso, confirmada o incierta no puede cancelarse desde el portal.
- El sistema revalida el estado y controla atómicamente cancelación e inicio de ejecución.
- Una solicitud cancelada nunca inicia después una devolución y no cambia el estado financiero de las cuotas.
- La cancelación se audita, notifica a administradores y permanece en el historial.

## Plazo ordinario

- Cada disciplina configura sus días desde la confirmación del pago.
- Dentro del plazo se admiten solicitudes elegibles; fuera de él se bloquea el canal ordinario y se informa la posibilidad de revisión excepcional.
- La revisión excepcional no garantiza aprobación y respeta pasarela, autorizaciones, costos y responsabilidad.
- Las obligaciones legales prevalecen sobre límites internos.
- Una solicitud válida conserva su historia aunque luego cambie el plazo.

## Invariantes

- Solicitud, aprobación y ejecución son etapas distintas.
- Solo se solicitan cuotas completas de un mismo checkout.
- Cancelar no cambia cuotas ni pagos.
- Una carrera entre cancelación y ejecución no puede producir ambos resultados.
- Estar dentro del plazo no garantiza capacidad de devolución.

## Pendientes de definición

- Plazo inicial por disciplina.
- Canal, actores y criterios de la revisión excepcional fuera de plazo.
- Estados completos de solicitud y sus transiciones.

Estos puntos requieren aprobación funcional.

## Trazabilidad

- DEC-152–DEC-154.
- RN-1565–RN-1611.
- DEC-149, DEC-151 y CON-020 descartada como reglas relacionadas.
- `MATERIAL_REVIEW_08.md`.
