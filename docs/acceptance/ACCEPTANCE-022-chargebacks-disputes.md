# ACCEPTANCE-022 — Contracargos y disputas

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-022`

## Escenarios

### AC-022-01 — Disputa iniciada

**Dado** un contracargo informado por la pasarela  
**Cuando** se vincula con el pago original  
**Entonces** sus cuotas quedan disputadas y protegidas contra otro checkout.

### AC-022-02 — Evento repetido

**Dado** una disputa ya registrada  
**Cuando** llega nuevamente el mismo evento  
**Entonces** no duplica el contracargo, su ajuste ni sus notificaciones.

### AC-022-03 — Disputa pendiente

**Dado** una disputa sin resultado definitivo  
**Cuando** se evalúa el mínimo comercial  
**Entonces** la cuota mantiene su cómputo  
**Y** no se habilita otro cobro.

### AC-022-04 — Disputa ganada

**Dado** una confirmación favorable del proveedor  
**Cuando** el pago permanece válido  
**Entonces** la cuota sigue pagada y conserva su cómputo comercial.

### AC-022-05 — Reversión definitiva con período abierto

**Dado** una pérdida total confirmada y el período original abierto  
**Cuando** se aplica el resultado  
**Entonces** la cuota vuelve a pendiente salvo anulación justificada  
**Y** se descuenta del período original.

### AC-022-06 — Reversión definitiva con período cerrado

**Dado** una pérdida total confirmada y el período original cerrado  
**Cuando** se aplica el resultado  
**Entonces** se genera un ajuste en un período abierto sin reescribir la liquidación cerrada.

### AC-022-07 — Consulta disciplinaria

**Dado** un administrador de disciplina  
**Cuando** consulta contracargos  
**Entonces** solo visualiza y gestiona los de su disciplina.

### AC-022-08 — Delegado en lectura

**Dado** un delegado general  
**Cuando** consulta disputas de su club  
**Entonces** puede visualizarlas  
**Pero** no presenta defensas ni compromete fondos.

### AC-022-09 — Evidencia de defensa

**Dado** una disputa abierta  
**Cuando** un administrador autorizado incorpora evidencia  
**Entonces** se conserva con controles de seguridad y versionado  
**Y** la disputa no se considera ganada por ese solo hecho.

### AC-022-10 — Envío autorizado

**Dado** evidencia preparada y una pasarela compatible  
**Cuando** el usuario autorizado confirma el envío  
**Entonces** se verifican permisos de la cuenta y se registran solicitud y respuesta.

### AC-022-11 — Sin envío automático

**Dado** documentación incorporada  
**Cuando** no existe acción autorizada de presentación  
**Entonces** el sistema no la envía al proveedor.

### AC-022-12 — Aviso a 24 horas

**Dado** una disputa que requiere defensa  
**Cuando** faltan 24 horas para el límite  
**Entonces** se genera un único aviso interno por destinatario.

### AC-022-13 — Disputa informada tarde

**Dado** una disputa recibida con menos de 24 horas disponibles  
**Cuando** se registra  
**Entonces** el aviso se genera inmediatamente.

### AC-022-14 — Vencimiento modificado

**Dado** una nueva fecha límite oficial  
**Cuando** la pasarela la informa  
**Entonces** se reprograma el aviso sin duplicaciones innecesarias.

### AC-022-15 — Defensa ya presentada o cerrada

**Dado** una defensa correctamente presentada o una disputa cerrada  
**Cuando** llega el momento programado del aviso  
**Entonces** no se genera una alerta de presentación pendiente.

## Pendientes no aceptables todavía

No pueden cerrarse criterios dependientes de estados del proveedor, costos, retenciones ni anulación administrativa hasta contar con validación y definición aprobadas.

## Trazabilidad

Cada escenario deriva de `SPEC-022`, DEC-146–DEC-148, RN-1455–RN-1505, CON-009, CON-018 y CON-019.
