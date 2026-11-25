# ACCEPTANCE-005 — Política comercial, liquidaciones y suspensión

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-005`

## Escenarios

### AC-005-01 — Alcance comercial

**Dado** un superadministrador  
**Cuando** configura una política para un club o disciplina  
**Entonces** identifica mínimo, modalidad, importe y responsable económico  
**Y** conserva el historial contractual.

### AC-005-02 — Usuario no autorizado

**Dado** un administrador o delegado general  
**Cuando** intenta modificar condiciones comerciales  
**Entonces** el backend rechaza la operación.

### AC-005-03 — Mínimo alcanzado

**Dado** un responsable cuyo conteo alcanza o supera el mínimo  
**Cuando** cierra el período mensual  
**Entonces** su cargo por incumplimiento es cero.

### AC-005-04 — Cargo fijo

**Dado** un responsable bajo el mínimo con modalidad fija  
**Cuando** cierra el período  
**Entonces** se calcula exactamente el importe fijo configurado.

### AC-005-05 — Cargo por faltantes

**Dado** un responsable bajo el mínimo con modalidad por faltante  
**Cuando** cierra el período  
**Entonces** el cargo es la cantidad faltante multiplicada por el importe unitario.

### AC-005-06 — Checkout agrupado

**Dado** una operación confirmada que cancela varias cuotas  
**Cuando** se actualiza el contador comercial  
**Entonces** cada cuota cancelada cuenta como un pago independiente.

### AC-005-07 — Mes de contabilización

**Dado** una cuota de un período anterior  
**Cuando** su pago online se confirma  
**Entonces** cuenta en el mes calendario de confirmación.

### AC-005-08 — Pagos excluidos

**Dado** pagos externos, pendientes, rechazados o cancelados  
**Cuando** se calcula el mínimo mensual  
**Entonces** ninguno incrementa el contador.

### AC-005-09 — Notificación duplicada

**Dado** una cuota ya contabilizada  
**Cuando** se recibe nuevamente la confirmación  
**Entonces** no vuelve a contarse.

### AC-005-10 — Cierre con incumplimiento

**Dado** un responsable bajo el mínimo  
**Cuando** se ejecuta el cierre automático  
**Entonces** se genera una sola liquidación *Pendiente de aprobación*  
**Y** conserva cálculo, configuración y pagos computados.

### AC-005-11 — Aprobación de liquidación

**Dado** una liquidación pendiente de aprobación  
**Cuando** el superadministrador la aprueba  
**Entonces** se registra fecha y responsable y puede habilitarse para cobro  
**Pero** antes de aprobarla no puede cobrarse.

### AC-005-12 — Pago externo de liquidación

**Dado** una liquidación comercial pendiente  
**Cuando** el superadministrador registra su pago externo  
**Entonces** registra medio, fecha y responsable  
**Y** la liquidación pasa a pagada conservando historial.

### AC-005-13 — Deuda comercial vencida

**Dado** una liquidación vencida e impaga  
**Cuando** se detecta el incumplimiento  
**Entonces** se alerta al superadministrador  
**Y** el servicio no se suspende automáticamente.

### AC-005-14 — Suspensión limitada

**Dado** una disciplina como responsable independiente  
**Cuando** el superadministrador la suspende  
**Entonces** se bloquean sus altas, generación y modificaciones  
**Y** las demás disciplinas del club no se ven afectadas.

### AC-005-15 — Operación durante suspensión

**Dado** un responsable suspendido  
**Cuando** administradores o jugadores acceden  
**Entonces** conservan las consultas autorizadas  
**Y** pueden pagarse cuotas anteriores con sus recargos  
**Y** los datos históricos permanecen intactos.

### AC-005-16 — Reactivación sin regularización

**Dado** un responsable suspendido  
**Cuando** el superadministrador lo reactiva sin generar períodos omitidos  
**Entonces** se restablece la operación normal  
**Y** esos períodos quedan sin cuotas nuevas.

### AC-005-17 — Propuesta de regularización

**Dado** una reactivación con regularización habilitada  
**Cuando** se preparan cuotas faltantes  
**Entonces** permanecen en borrador y no son cobrables  
**Y** el administrador puede revisar jugadores, pagos externos y vencimientos.

### AC-005-18 — Confirmación de regularización

**Dado** una propuesta revisada  
**Cuando** el administrador la confirma explícitamente  
**Entonces** se publican cuotas según las condiciones de cada período  
**Y** no se duplican cuotas ni pagos  
**Y** se registra la trazabilidad completa.

### AC-005-19 — Operación actual en paralelo

**Dado** una regularización histórica en curso  
**Cuando** llega el calendario mensual vigente  
**Entonces** la generación normal continúa sin ser bloqueada por los borradores.

## Pendientes no aceptables todavía

No pueden cerrarse criterios sobre vencimiento exacto de liquidaciones ni su flujo específico de pasarela hasta contar con definiciones aprobadas.

## Trazabilidad

Cada escenario deriva de `SPEC-005`, DEC-049–DEC-060, RN-0302–RN-0403 y CON-017, bajo la precedencia de CON-011.
