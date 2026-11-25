# ACCEPTANCE-009 — Conciliación financiera

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-009`

## Escenarios

### AC-009-01 — Estados independientes

**Dado** un pago aprobado todavía no liquidado  
**Cuando** se consulta su situación financiera  
**Entonces** conserva el estado de pago aprobado  
**Y** muestra la liquidación como pendiente.

### AC-009-02 — Comparación automática

**Dado** información de liquidación disponible en la pasarela  
**Cuando** se procesa la conciliación  
**Entonces** se comparan importes esperado y liquidado.

### AC-009-03 — Diferencia identificada

**Dado** un importe distinto, operación faltante o liquidación pendiente  
**Cuando** se detecta la diferencia  
**Entonces** queda vinculada con operación, club, disciplina y destinatario.

### AC-009-04 — Dashboard financiero

**Dado** pagos aprobados y liquidados  
**Cuando** se muestran indicadores financieros  
**Entonces** los importes aprobados se distinguen de los efectivamente liquidados.

### AC-009-05 — Investigación disciplinaria

**Dado** un administrador de disciplina  
**Cuando** consulta conciliaciones  
**Entonces** solo accede a las diferencias de su disciplina.

### AC-009-06 — Resolución completa

**Dado** una diferencia pendiente  
**Cuando** el administrador selecciona un tipo activo, informa motivo y confirma  
**Entonces** se registran usuario, fecha, hora, importes, tipo y explicación.

### AC-009-07 — Datos obligatorios

**Dado** una diferencia pendiente  
**Cuando** se intenta resolver sin tipo o sin motivo  
**Entonces** el sistema rechaza la resolución.

### AC-009-08 — Comprobante opcional

**Dado** una resolución válida  
**Cuando** no se adjunta documentación  
**Entonces** la ausencia del archivo no impide registrarla.

### AC-009-09 — Preservación del pago

**Dado** una diferencia resuelta  
**Cuando** finaliza la revisión  
**Entonces** el importe y estado originales del pago no se modifican automáticamente  
**Y** la intervención queda en el historial.

### AC-009-10 — Acceso del delegado

**Dado** un delegado general  
**Cuando** consulta conciliaciones de su club  
**Entonces** visualiza diferencias y resoluciones  
**Pero cuando** intenta resolverlas  
**Entonces** el backend rechaza la operación.

### AC-009-11 — Catálogo global

**Dado** un superadministrador  
**Cuando** crea o modifica un tipo de resolución  
**Entonces** el catálogo global registra usuario, fecha y valores anterior y nuevo.

### AC-009-12 — Tipo desactivado

**Dado** un tipo de resolución desactivado  
**Cuando** un administrador intenta usarlo en una nueva resolución  
**Entonces** no está disponible.

### AC-009-13 — Resolución histórica

**Dado** una conciliación resuelta con un tipo posteriormente modificado o desactivado  
**Cuando** se consulta su historial  
**Entonces** conserva el tipo y la descripción utilizados originalmente.

## Pendientes no aceptables todavía

No pueden cerrarse criterios sobre el ciclo completo de estados, reapertura de resoluciones ni frecuencia automática hasta contar con definición aprobada.

## Trazabilidad

Cada escenario deriva de `SPEC-009`, DEC-082–DEC-085 y RN-0583–RN-0623.
