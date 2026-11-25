# ACCEPTANCE-004 — Selección y registro de pagos

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-004`

## Escenarios

### AC-004-01 — Selección no cronológica

**Dado** un jugador con varias cuotas pendientes  
**Cuando** selecciona una cuota sin cancelar las anteriores  
**Entonces** puede continuar con esa selección  
**Y** las demás cuotas permanecen pendientes.

### AC-004-02 — Agrupación de cuotas

**Dado** varias cuotas pendientes seleccionadas  
**Cuando** el jugador prepara el pago  
**Entonces** se agrupan en una operación  
**Y** cada cuota conserva su identidad y estado individual.

### AC-004-03 — Detalle previo

**Dado** una selección de cuotas  
**Cuando** el jugador revisa la operación antes de pagar  
**Entonces** visualiza cada cuota, sus recargos aplicables y el total.

### AC-004-04 — Intento de pago parcial

**Dado** una cuota con importe pendiente  
**Cuando** se intenta pagar un importe menor  
**Entonces** el sistema rechaza el pago parcial.

### AC-004-05 — Cobro online confirmado

**Dado** una operación que contiene una o varias cuotas completas  
**Cuando** la pasarela confirma el cobro completo  
**Entonces** las cuotas incluidas pasan a pagadas  
**Y** las no incluidas no cambian.

### AC-004-06 — Cobro rechazado o fallido

**Dado** una operación iniciada  
**Cuando** la pasarela informa rechazo o fallo  
**Entonces** ninguna cuota de la operación se considera pagada  
**Y** la deuda permanece vigente.

### AC-004-07 — Pago externo completo

**Dado** un administrador de disciplina y cuotas de su ámbito  
**Cuando** registra el pago externo completo con medio y fecha  
**Entonces** las cuotas correspondientes se cancelan  
**Y** se registra al administrador responsable.

### AC-004-08 — Comprobante opcional

**Dado** un pago externo completo  
**Cuando** se registra sin comprobante  
**Entonces** la ausencia del archivo no impide el registro.

### AC-004-09 — Pago externo parcial

**Dado** una cuota pendiente  
**Cuando** el administrador intenta registrar un pago externo incompleto  
**Entonces** el sistema rechaza la operación  
**Y** la cuota permanece pendiente.

### AC-004-10 — Separación por origen

**Dado** pagos externos y pagos procesados por pasarela  
**Cuando** se consulta el historial  
**Entonces** cada operación identifica claramente su origen.

### AC-004-11 — Límite de disciplina

**Dado** un administrador de disciplina  
**Cuando** intenta registrar un pago externo para otra disciplina  
**Entonces** el backend rechaza la operación.

### AC-004-12 — Delegado en modo lectura

**Dado** un delegado general  
**Cuando** consulta pagos externos de su club  
**Entonces** puede visualizarlos  
**Pero cuando** intenta registrarlos o modificarlos  
**Entonces** el backend rechaza la operación.

## Pendientes no aceptables todavía

No pueden cerrarse criterios de checkout, OAuth, webhooks, estados pendientes, distribución Split Payment, devoluciones, contracargos o conciliación hasta completar las validaciones de ADR-0002 y DEC-170.

## Trazabilidad

Cada escenario deriva de `SPEC-004`, DEC-046–DEC-048 y RN-0278–RN-0301. ADR-0002 y DEC-170 condicionan la integración real.
