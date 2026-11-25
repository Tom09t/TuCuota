# ACCEPTANCE-018 — Disponibilidad y monitoreo de pasarelas

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-018`

## Escenarios

### AC-018-01 — Conexión interrumpida

**Dado** una conexión de disciplina no operativa  
**Cuando** se intenta iniciar un pago online  
**Entonces** se bloquea esa disciplina y se notifica a sus administradores y al superadministrador.

### AC-018-02 — Aislamiento de la falla

**Dado** una conexión fallida  
**Cuando** otras disciplinas mantienen conexiones operativas  
**Entonces** continúan procesando pagos normalmente.

### AC-018-03 — Operación durante interrupción

**Dado** una disciplina bloqueada técnicamente  
**Cuando** se consulta o registra actividad  
**Entonces** las cuotas siguen visibles y se permiten pagos externos  
**Y** estos no cuentan para el mínimo online.

### AC-018-04 — Pago previo

**Dado** una operación iniciada antes de la interrupción  
**Cuando** se pierde la conexión  
**Entonces** conserva estado y referencia y puede conciliarse posteriormente.

### AC-018-05 — Detección múltiple

**Dado** una conexión monitoreada  
**Cuando** una verificación, evento oficial o error real detecta un problema  
**Entonces** se clasifica sin generar cobros de prueba ni exponer secretos.

### AC-018-06 — Error temporal aislado

**Dado** un único error temporal  
**Cuando** no satisface los criterios de recurrencia o indisponibilidad  
**Entonces** no produce una desconexión permanente automática.

### AC-018-07 — Autorización revocada

**Dado** una autorización inválida confirmada  
**Cuando** se evalúa la conexión  
**Entonces** se bloquean nuevos cobros de esa disciplina.

### AC-018-08 — Recuperación particular

**Dado** una conexión bloqueada  
**Cuando** vuelve a estar operativa y autorizada  
**Entonces** reanuda automáticamente nuevos cobros  
**Y** no reintenta operaciones con riesgo de duplicación.

### AC-018-09 — Otro bloqueo vigente

**Dado** una conexión técnica recuperada y una suspensión comercial activa  
**Cuando** finaliza la verificación  
**Entonces** la suspensión continúa impidiendo cobros.

### AC-018-10 — Parámetros por proveedor

**Dado** un superadministrador  
**Cuando** configura monitoreo para un proveedor  
**Entonces** se aplica a sus disciplinas dentro de los límites técnicos y queda auditado.

### AC-018-11 — Proveedor habilitado

**Dado** una integración implementada y compatible con Split Payment  
**Cuando** el superadministrador habilita el proveedor  
**Entonces** las disciplinas pueden seleccionarlo, pero no quedan conectadas automáticamente.

### AC-018-12 — Deshabilitación comercial

**Dado** un proveedor con conexiones existentes válidas  
**Cuando** se deshabilita comercialmente  
**Entonces** se impiden nuevas vinculaciones y las existentes pueden continuar cobrando.

### AC-018-13 — Deshabilitación operativa

**Dado** un proveedor activo  
**Cuando** el superadministrador aplica un bloqueo operativo global  
**Entonces** se detienen nuevos cobros de todas sus disciplinas  
**Y** otras pasarelas continúan funcionando.

### AC-018-14 — Recuperación no levanta bloqueo global

**Dado** una conexión operativa bajo bloqueo global  
**Cuando** el monitoreo confirma su estado  
**Entonces** no habilita cobros mientras siga el bloqueo.

### AC-018-15 — Reactivación explícita

**Dado** un proveedor bloqueado globalmente  
**Cuando** el superadministrador lo reactiva  
**Entonces** se inicia la validación individual de sus conexiones.

### AC-018-16 — Recuperación parcial global

**Dado** conexiones válidas y fallidas tras reactivar el proveedor  
**Cuando** terminan las validaciones  
**Entonces** solo las válidas reanudan cobros  
**Y** las demás permanecen bloqueadas y monitoreadas.

### AC-018-17 — Sin cobro automático

**Dado** un proveedor o conexión reactivada  
**Cuando** recupera estado operativo  
**Entonces** no se genera ningún cobro por el solo hecho de reactivarse.

## Pendientes no aceptables todavía

No pueden cerrarse criterios cuantitativos ni el catálogo inicial hasta aprobar estados, frecuencias, umbrales y evidencia de compatibilidad.

## Trazabilidad

Cada escenario deriva de `SPEC-018`, DEC-126–DEC-132 y RN-1134–RN-1237. ADR-0002 condiciona la integración definitiva.
