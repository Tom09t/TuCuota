# ACCEPTANCE-033 — Migración inicial controlada de datos

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-033`

## Escenarios

### AC-033-01 — Datos deportivos válidos

**Dado** jugadores, categorías e inscripciones válidos de una disciplina  
**Cuando** se valida la migración  
**Entonces** quedan clasificados como candidatos a importación dentro de esa disciplina.

### AC-033-02 — Beneficios vigentes

**Dado** becas o beneficios vigentes con datos válidos  
**Cuando** se aprueba la importación  
**Entonces** se incorporan conservando su relación con el jugador y la disciplina.

### AC-033-03 — Cuota histórica pendiente

**Dado** una cuota anterior que continúa impaga  
**Cuando** supera la validación y se aprueba  
**Entonces** puede incorporarse como saldo pendiente sin crear un pago.

### AC-033-04 — Pago histórico confiable

**Dado** un pago externo anterior al lanzamiento con información suficientemente confiable  
**Cuando** se importa  
**Entonces** se identifica como histórico externo y conserva fecha y medio original cuando estén disponibles.

### AC-033-05 — Pago histórico no confiable

**Dado** un pago histórico que no satisface los criterios de confiabilidad aprobados  
**Cuando** se valida  
**Entonces** no se incorpora como pago confirmado.

### AC-033-06 — Pasarela no simulada

**Dado** un pago externo histórico importable  
**Cuando** se incorpora  
**Entonces** no se crea ninguna transacción ficticia en la pasarela.

### AC-033-07 — Sin comisión retroactiva

**Dado** un pago anterior al lanzamiento  
**Cuando** se incorpora como registro histórico  
**Entonces** no genera la comisión retroactiva del 5 %.

### AC-033-08 — Vista previa clasificada

**Dado** un conjunto de datos cargado  
**Cuando** se ejecuta la validación previa  
**Entonces** el resultado distingue registros válidos, duplicados y con errores.

### AC-033-09 — Revisión autorizada

**Dado** el resultado de una validación  
**Cuando** un administrador de la disciplina lo consulta  
**Entonces** puede revisarlo únicamente dentro de su ámbito autorizado.

### AC-033-10 — Error sin resolver

**Dado** un registro con error  
**Cuando** se intenta confirmar sin corregirlo ni excluirlo  
**Entonces** el sistema impide su incorporación.

### AC-033-11 — Confirmación autorizada

**Dado** registros válidos revisados  
**Cuando** un administrador autorizado aprueba la incorporación definitiva  
**Entonces** se importan y se registra el responsable de la operación.

### AC-033-12 — Confirmación no autorizada

**Dado** un usuario fuera del ámbito correspondiente  
**Cuando** intenta aprobar la migración  
**Entonces** el backend deniega la operación.

### AC-033-13 — Reintento idempotente

**Dado** una importación ya procesada  
**Cuando** se reintenta el mismo proceso  
**Entonces** no se duplican entidades, pagos ni saldos.

### AC-033-14 — Aislamiento

**Dado** datos pertenecientes a una disciplina  
**Cuando** se importan  
**Entonces** no quedan visibles ni asociados a otro club o disciplina.

### AC-033-15 — Consistencia posterior

**Dado** una migración finalizada  
**Cuando** se verifican los resultados  
**Entonces** los saldos pendientes son consistentes y se conserva trazabilidad de datos, origen y responsable.

### AC-033-16 — Operación real protegida

**Dado** una operación financiera real previamente confirmada  
**Cuando** un registro histórico entra en conflicto  
**Entonces** la operación real no se sobrescribe y el conflicto se informa para revisión.

## Pendientes no aceptables todavía

No pueden cerrarse formatos, confiabilidad, coincidencias, duplicados, límites, errores parciales, claves idempotentes, reversión ni tolerancias de conciliación sin definiciones aprobadas.

## Trazabilidad

Cada escenario deriva de `SPEC-033`, DEC-169 y RN-1852–RN-1871. DEC-170 mantiene la migración aprobada como condición previa para habilitar cobros reales.

