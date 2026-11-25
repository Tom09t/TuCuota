# ACCEPTANCE-006 — Ajustes comerciales y período de gracia

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-006`

## Escenarios

### AC-006-01 — Aprobación sin ajuste

**Dado** una liquidación pendiente de aprobación  
**Cuando** el superadministrador aprueba el importe calculado  
**Entonces** se habilita su cobro y se conserva el importe original.

### AC-006-02 — Ajuste parcial

**Dado** una liquidación pendiente  
**Cuando** el superadministrador informa un importe final distinto y una justificación  
**Entonces** se habilita el cobro del importe final  
**Y** el original no se sobrescribe.

### AC-006-03 — Condonación total

**Dado** una liquidación pendiente  
**Cuando** el superadministrador la condona con justificación  
**Entonces** queda sin saldo exigible  
**Y** se conservan importes, motivo, fecha y responsable.

### AC-006-04 — Justificación obligatoria

**Dado** un ajuste o condonación  
**Cuando** no se informa una justificación  
**Entonces** el sistema rechaza la decisión.

### AC-006-05 — Consulta aislada

**Dado** un responsable comercial  
**Cuando** consulta liquidaciones  
**Entonces** solo accede a las propias con importes original y final, justificación y decisión.

### AC-006-06 — Cambio de mínimo

**Dado** un contrato en un mes abierto  
**Cuando** el superadministrador modifica su mínimo  
**Entonces** se recalcula el cumplimiento de todo el mes con el nuevo mínimo  
**Y** los pagos ya contabilizados siguen siendo válidos.

### AC-006-07 — Cambio de importe

**Dado** un contrato en un mes abierto  
**Cuando** se modifica el importe de penalización  
**Entonces** el cierre del mes completo usa el nuevo importe  
**Y** respeta la modalidad configurada.

### AC-006-08 — Cambio de modalidad

**Dado** un contrato con modalidad vigente  
**Cuando** el superadministrador alterna entre cargo fijo y pago faltante  
**Entonces** el cierre del mes completo usa la modalidad e importe vigentes al calcular.

### AC-006-09 — Período cerrado

**Dado** una liquidación de un período cerrado  
**Cuando** cambia el mínimo, importe o modalidad  
**Entonces** la liquidación no se recalcula automáticamente.

### AC-006-10 — Mes final de gracia

**Dado** una gracia configurada hasta un mes y año  
**Cuando** transcurre ese mes  
**Entonces** permanece exento de penalización  
**Y** la exigencia comienza el primer día del mes siguiente.

### AC-006-11 — Pagos durante la gracia

**Dado** un contrato dentro de la gracia  
**Cuando** se confirman pagos online  
**Entonces** se registran para estadísticas y reportes  
**Y** no generan penalización por mínimo insuficiente.

### AC-006-12 — Extensión al mes actual

**Dado** un mes actual no cubierto  
**Cuando** el superadministrador extiende la gracia para incluirlo  
**Entonces** el mes queda exento de penalización.

### AC-006-13 — Acortamiento durante el mes cubierto

**Dado** un mes actual inicialmente cubierto por gracia  
**Cuando** el superadministrador acorta el período y lo excluye  
**Entonces** no se penaliza ese mes  
**Y** la penalización tampoco se traslada a un período posterior.

### AC-006-14 — Independencia contractual

**Dado** dos contratos comerciales  
**Cuando** cambia la gracia de uno  
**Entonces** el otro permanece sin cambios.

### AC-006-15 — Auditoría de cambios

**Dado** cualquier cambio de mínimo, importe, modalidad o gracia  
**Cuando** se confirma  
**Entonces** quedan registrados valores anterior y nuevo, fecha y superadministrador.

## Pendientes no aceptables todavía

No pueden cerrarse criterios sobre el nombre del estado de una condonación total ni sobre concurrencia entre cambios y cierre hasta contar con definición aprobada.

## Trazabilidad

Cada escenario deriva de `SPEC-006`, DEC-061–DEC-068, RN-0404–RN-0467, CON-004 y CON-021, bajo la precedencia de CON-011.
