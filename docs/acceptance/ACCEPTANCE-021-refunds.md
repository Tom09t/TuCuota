# ACCEPTANCE-021 — Devoluciones de pagos

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-021`

## Escenarios

### AC-021-01 — Solicitud disciplinaria

**Dado** un administrador de disciplina  
**Cuando** solicita devolver un pago online de su ámbito con motivo  
**Entonces** se vincula al pago original y queda pendiente de confirmación.

### AC-021-02 — Fuera de ámbito

**Dado** un administrador  
**Cuando** intenta devolver un pago de otra disciplina  
**Entonces** el backend rechaza la operación.

### AC-021-03 — Delegado en lectura

**Dado** un delegado general  
**Cuando** consulta devoluciones de su club  
**Entonces** puede visualizarlas  
**Pero cuando** intenta solicitarlas o autorizarlas  
**Entonces** el backend rechaza la operación.

### AC-021-04 — Checkout completo

**Dado** un checkout confirmado y reembolsable  
**Cuando** se seleccionan todas sus cuotas  
**Entonces** puede solicitarse la devolución total según la capacidad del proveedor.

### AC-021-05 — Subconjunto de cuotas completas

**Dado** un checkout con varias cuotas  
**Cuando** se seleccionan una o más cuotas completas  
**Entonces** puede solicitarse esa devolución si proveedor y split la soportan.

### AC-021-06 — Fracción arbitraria

**Dado** una cuota pagada  
**Cuando** se intenta devolver solo una fracción monetaria  
**Entonces** el sistema rechaza la solicitud.

### AC-021-07 — Importe no disponible

**Dado** una cuota ya devuelta o con importe reservado  
**Cuando** otra solicitud excede lo disponible  
**Entonces** el sistema evita la devolución duplicada o concurrente.

### AC-021-08 — Solicitud no confirmada

**Dado** una devolución enviada  
**Cuando** la pasarela aún no confirma  
**Entonces** no se considera completada y la cuota no cambia definitivamente.

### AC-021-09 — Componentes financieros

**Dado** una devolución confirmada  
**Cuando** se registra el resultado  
**Entonces** se distinguen cuota, destinatario, comisión y costo disponible sin presumir reversión total del split.

### AC-021-10 — Volver a pendiente

**Dado** una cuota cuya devolución fue confirmada con destino pendiente  
**Cuando** no existen operaciones incompatibles  
**Entonces** vuelve a ser exigible y puede pagarse nuevamente.

### AC-021-11 — Cuota anulada

**Dado** una cuota cuya devolución fue confirmada con destino anulado  
**Cuando** se aplica el resultado  
**Entonces** queda sin deuda y no puede incluirse en otro checkout.

### AC-021-12 — Devolución fallida

**Dado** una solicitud rechazada o fallida  
**Cuando** se recibe el resultado definitivo  
**Entonces** se conserva el estado financiero del pago original y se registra el error.

### AC-021-13 — Período original abierto

**Dado** una cuota totalmente devuelta y su período comercial abierto  
**Cuando** se confirma la devolución  
**Entonces** se descuenta del conteo de ese período.

### AC-021-14 — Período original cerrado

**Dado** una cuota totalmente devuelta y su período comercial cerrado  
**Cuando** se confirma la devolución  
**Entonces** se genera un ajuste en un período abierto sin modificar la liquidación cerrada.

### AC-021-15 — Evento repetido

**Dado** una devolución ya procesada  
**Cuando** llega otra vez la confirmación  
**Entonces** no se repite el cambio de cuota ni el ajuste comercial.

### AC-021-16 — Trazabilidad integral

**Dado** una devolución completada  
**Cuando** se consulta la auditoría  
**Entonces** relaciona pago, checkout, cuotas, solicitud, resultado, decisión de estado y ajuste comercial.

## Pendientes no aceptables todavía

No pueden cerrarse criterios dependientes del proveedor, componentes del split ni período abierto de ajuste hasta contar con validación y definición aprobadas.

## Trazabilidad

Cada escenario deriva de `SPEC-021`, DEC-143–DEC-145, RN-1401–RN-1454, CON-009 y CON-018. CON-020 permanece descartada.
