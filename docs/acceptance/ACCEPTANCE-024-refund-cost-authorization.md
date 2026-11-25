# ACCEPTANCE-024 — Autorización de costos excepcionales

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-024`

## Escenarios

### AC-024-01 — Falla de plataforma

**Dado** un costo no recuperable causado por una falla técnica atribuible a TuCuota  
**Cuando** se determina la responsabilidad  
**Entonces** se asigna a la plataforma y requiere autorización del superadministrador.

### AC-024-02 — Error del pagador aceptado

**Dado** un error del pagador cuya devolución acepta la disciplina  
**Cuando** existe un costo no recuperable  
**Entonces** se asigna a la disciplina y lo autoriza uno de sus administradores autorizados.

### AC-024-03 — Error disciplinario

**Dado** un costo causado por gestión de una disciplina  
**Cuando** se documenta la causa  
**Entonces** la disciplina queda como responsable económico.

### AC-024-04 — Caso ambiguo

**Dado** evidencia insuficiente para atribuir responsabilidad  
**Cuando** se revisa la excepción  
**Entonces** no se asigna automáticamente el costo.

### AC-024-05 — Fondos ajenos

**Dado** un administrador de disciplina  
**Cuando** intenta autorizar un costo de plataforma u otra disciplina  
**Entonces** el backend rechaza la operación.

### AC-024-06 — Delegado en lectura

**Dado** un delegado general  
**Cuando** consulta costos excepcionales de su club  
**Entonces** puede visualizarlos  
**Pero cuando** intenta autorizarlos  
**Entonces** el backend rechaza la operación.

### AC-024-07 — Datos obligatorios

**Dado** una excepción  
**Cuando** faltan causa, motivo, importe, responsable o evidencia requerida  
**Entonces** no puede autorizarse.

### AC-024-08 — Aprobación sin ejecución

**Dado** un costo administrativamente autorizado  
**Cuando** la pasarela no permite el movimiento  
**Entonces** no se marca como ejecutado y se registra la limitación.

### AC-024-09 — Resultado del proveedor

**Dado** una operación compatible y autorizada  
**Cuando** el proveedor informa su resultado  
**Entonces** se registra separadamente de la aprobación y se incorpora a conciliación y reportes.

### AC-024-10 — Idempotencia

**Dado** una excepción ya ejecutada  
**Cuando** se repite la solicitud o confirmación  
**Entonces** no se duplica el movimiento y permanece la auditoría completa.

## Pendientes no aceptables todavía

No pueden cerrarse criterios para casos ambiguos, evidencias mínimas ni mecanismos por proveedor hasta contar con definiciones aprobadas.

## Trazabilidad

Cada escenario deriva de `SPEC-024`, DEC-151, RN-1539–RN-1564 y CON-009, bajo la precedencia de CON-011.
