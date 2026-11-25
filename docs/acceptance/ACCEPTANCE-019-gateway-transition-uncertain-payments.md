# ACCEPTANCE-019 — Cambio de pasarela y pagos inciertos

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-019`

## Escenarios

### AC-019-01 — Cambio validado

**Dado** un administrador y un proveedor habilitado  
**Cuando** completa OTP, autorización y validación Split Payment  
**Entonces** la nueva pasarela se vuelve principal para nuevos cobros de su disciplina.

### AC-019-02 — Cambio fallido

**Dado** una nueva conexión inválida  
**Cuando** falla su validación  
**Entonces** no se activa y se conserva la anterior si continúa operativa.

### AC-019-03 — Pago anterior al cambio

**Dado** una operación iniciada con el proveedor anterior  
**Cuando** cambia la pasarela principal  
**Entonces** conserva proveedor, referencia y circuito de conciliación originales.

### AC-019-04 — Checkout previo vigente

**Dado** un checkout creado antes del cambio  
**Cuando** sigue vigente y el proveedor permite completarlo  
**Entonces** puede finalizar mediante el proveedor original.

### AC-019-05 — Cuota protegida

**Dado** una cuota con operación pendiente válida  
**Cuando** se intenta agregar a otro checkout incompatible  
**Entonces** el sistema lo impide aunque use otro proveedor.

### AC-019-06 — Pantalla cerrada

**Dado** un checkout pendiente  
**Cuando** el usuario cierra su pantalla  
**Entonces** no se presume cancelado y la cuota sigue protegida.

### AC-019-07 — Tiempo interno superado

**Dado** una operación aparentemente abandonada  
**Cuando** supera el tiempo interno de monitoreo  
**Entonces** se consulta su estado real y no se cancela automáticamente.

### AC-019-08 — Liberación confirmada

**Dado** una operación confirmada como vencida, cancelada o fallida definitivamente  
**Cuando** no existe otro pago confirmado  
**Entonces** sus cuotas pueden liberarse para un nuevo intento con la pasarela vigente.

### AC-019-09 — Pago confirmado

**Dado** un pago confirmado  
**Cuando** llega un evento repetido o se intenta reutilizar la cuota  
**Entonces** no se imputa ni cobra nuevamente.

### AC-019-10 — Estado incierto

**Dado** una operación sin resultado verificable  
**Cuando** se clasifica como incierta  
**Entonces** se continúa conciliando y sus cuotas permanecen protegidas.

### AC-019-11 — Alerta por plazo

**Dado** un pago incierto que supera el plazo de su proveedor  
**Cuando** se evalúa  
**Entonces** se alerta a la disciplina y al superadministrador  
**Y** no cambia su estado ni se liberan cuotas.

### AC-019-12 — Evidencia insuficiente

**Dado** solo una declaración del administrador  
**Cuando** se solicita liberar una cuota incierta  
**Entonces** el sistema no permite resolverla como impaga.

### AC-019-13 — Resolución excepcional válida

**Dado** evidencia verificable sin confirmación exitosa  
**Cuando** el superadministrador justifica y confirma desde una sesión vigente  
**Entonces** se libera la cuota y se audita evidencia, motivo, operación y actor.

### AC-019-14 — Evidencia obligatoria

**Dado** una resolución excepcional  
**Cuando** falta justificación o evidencia  
**Entonces** el sistema impide confirmarla.

### AC-019-15 — Pago exitoso oficial

**Dado** confirmación oficial de pago exitoso  
**Cuando** se intenta resolver manualmente como impago  
**Entonces** el sistema rechaza la operación.

### AC-019-16 — Evento tardío contradictorio

**Dado** una cuota liberada excepcionalmente  
**Cuando** llega después un resultado contradictorio  
**Entonces** se crea una discrepancia para revisión sin imputación automática duplicada.

### AC-019-17 — Preservación histórica

**Dado** una cuota liberada o reintentada  
**Cuando** se consulta su trazabilidad  
**Entonces** permanecen todos los checkouts, intentos, estados y resoluciones previos.

## Pendientes no aceptables todavía

No pueden cerrarse criterios cuantitativos de alerta, evidencia externa ni vigencia por proveedor hasta contar con definición y validación aprobadas.

## Trazabilidad

Cada escenario deriva de `SPEC-019`, DEC-133–DEC-139 y RN-1238–RN-1350. ADR-0002 condiciona los comportamientos dependientes del proveedor.
