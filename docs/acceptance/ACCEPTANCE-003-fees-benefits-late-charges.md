# ACCEPTANCE-003 — Cuotas, beneficios y recargos por mora

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-003`

## Escenarios

### AC-003-01 — Cambio de importe base

**Dado** un administrador que modifica el importe base de una categoría  
**Cuando** el cambio queda registrado  
**Entonces** rige desde el mes calendario siguiente  
**Y** ninguna cuota ya generada se recalcula.

### AC-003-02 — Modalidades de beneficio

**Dado** una inscripción habilitada  
**Cuando** se asigna un importe fijo, descuento porcentual o beca preconfigurada  
**Entonces** se calcula según la modalidad elegida  
**Y** solo afecta esa inscripción y disciplina.

### AC-003-03 — Beneficio único

**Dado** un beneficio aplicable a una inscripción y período  
**Cuando** se intenta acumular otro  
**Entonces** el sistema impide la superposición y ofrece su reemplazo.

### AC-003-04 — Beneficio en el mes actual

**Dado** una cuota impaga del mes actual  
**Cuando** el administrador elige aplicar el beneficio de inmediato  
**Entonces** se recalcula el importe pendiente  
**Y** se registran importes anterior y nuevo, beneficio y responsable.

### AC-003-05 — Beneficio desde el mes siguiente

**Dado** una cuota ya generada  
**Cuando** el administrador elige aplicar el beneficio desde el mes siguiente  
**Entonces** la cuota existente conserva su importe.

### AC-003-06 — Período anterior o cuota pagada

**Dado** una cuota pagada o de un mes anterior  
**Cuando** se asigna o reemplaza un beneficio  
**Entonces** esa cuota no se modifica.

### AC-003-07 — Reemplazo confirmado

**Dado** una inscripción con beneficio vigente  
**Cuando** el administrador revisa el beneficio anterior y el nuevo y confirma  
**Entonces** el anterior termina cuando comienza el nuevo  
**Y** se conserva el historial sin superposición.

### AC-003-08 — Cambio de tipo de beca

**Dado** un tipo de beca con beneficiarios actuales  
**Cuando** se modifican sus reglas  
**Entonces** el administrador elige entre próximas cuotas de beneficiarios actuales o solo nuevas asignaciones  
**Y** se conserva la versión histórica.

### AC-003-09 — Generación automática

**Dado** el día configurado de una disciplina  
**Cuando** se ejecuta la generación mensual  
**Entonces** se crean cuotas solo para inscripciones habilitadas de clubes y disciplinas activos  
**Y** se aplican importes y beneficios vigentes.

### AC-003-10 — Prevención de duplicados

**Dado** una cuota existente para una inscripción y período  
**Cuando** la generación vuelve a procesar ese caso  
**Entonces** no crea otra cuota  
**Y** registra el resultado de la ejecución.

### AC-003-11 — Generación manual no disponible

**Dado** un administrador de disciplina  
**Cuando** intenta iniciar libremente la generación mensual  
**Entonces** la operación no está disponible.

### AC-003-12 — Vencimiento histórico

**Dado** una cuota generada con una fecha de vencimiento  
**Cuando** cambia el día de vencimiento de la disciplina  
**Entonces** la cuota conserva su fecha original.

### AC-003-13 — Mora desactivada

**Dado** una cuota vencida cuya política tiene la mora desactivada  
**Cuando** se consulta su importe a pagar  
**Entonces** no se agrega penalización económica.

### AC-003-14 — Período de gracia

**Dado** una cuota vencida con días de gracia configurados  
**Cuando** aún transcurre la gracia  
**Entonces** figura vencida sin recargo  
**Y** el recargo comienza al día siguiente del último día de gracia.

### AC-003-15 — Modalidad y desglose

**Dado** una cuota fuera de la gracia con mora activa  
**Cuando** se consulta el importe a pagar  
**Entonces** se calcula la modalidad histórica fija, porcentual o progresiva  
**Y** el recargo se muestra separado del importe de cuota.

### AC-003-16 — Política histórica

**Dado** una cuota impaga ya generada  
**Cuando** cambia la configuración de mora  
**Entonces** continúa usando modalidad, valores, tramos y gracia originales.

### AC-003-17 — Beneficio y recargo porcentual

**Dado** una cuota actual impaga con recargo porcentual  
**Cuando** un beneficio actualiza su importe  
**Entonces** el porcentaje se recalcula sobre el importe actualizado  
**Y** se conservan política, vencimiento, gracia, mora acumulada y recargos fijos  
**Y** se auditan los importes anterior y nuevo.

### AC-003-18 — Ámbito y solo lectura

**Dado** un administrador o delegado general  
**Cuando** intenta modificar una configuración fuera de su autorización  
**Entonces** el backend rechaza la operación  
**Y** el delegado mantiene acceso de consulta dentro de su club.

## Pendientes no aceptables todavía

No pueden cerrarse escenarios sobre días inexistentes del mes ni sobre el cálculo monetario de tramos progresivos tras recalcular una cuota hasta que exista definición funcional aprobada.

## Trazabilidad

Cada escenario deriva de `SPEC-003`, DEC-030–045, RN-0172–0277, RN-C001–RN-C005, CON-003, CON-015 y CON-016.
