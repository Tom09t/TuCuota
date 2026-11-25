# ACCEPTANCE-010 — Operación y alertas de conciliación

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-010`

## Escenarios

### AC-010-01 — Plazo global

**Dado** un superadministrador  
**Cuando** modifica el plazo de alerta en días calendario  
**Entonces** el nuevo valor se aplica a todos los clubes y disciplinas  
**Y** se auditan usuario, fecha y valores.

### AC-010-02 — Configuración no autorizada

**Dado** un administrador de disciplina  
**Cuando** intenta cambiar plazo o frecuencia global  
**Entonces** el backend rechaza la operación.

### AC-010-03 — Diferencia fuera de plazo

**Dado** una diferencia pendiente que supera el plazo global  
**Cuando** se evalúan alertas  
**Entonces** aparece una alerta interna para su disciplina y el superadministrador  
**Y** no se envía correo.

### AC-010-04 — Idempotencia de alerta

**Dado** una alerta activa para una diferencia  
**Cuando** vuelve a evaluarse la misma situación  
**Entonces** no se genera otra alerta equivalente.

### AC-010-05 — Diferencia resuelta

**Dado** una alerta activa  
**Cuando** se resuelve su diferencia  
**Entonces** desaparece de la vista activa  
**Y** conserva historial sin modificar el pago original.

### AC-010-06 — Ejecución automática

**Dado** una frecuencia global configurada  
**Cuando** llega la programación  
**Entonces** se ejecuta la conciliación y se registran fecha, hora, resultado y cantidad procesada.

### AC-010-07 — Pasarela no disponible

**Dado** una ejecución automática  
**Cuando** la pasarela no responde  
**Entonces** se registra el error  
**Y** no se alteran incorrectamente estados financieros  
**Y** las operaciones pendientes pueden reconsultarse después.

### AC-010-08 — Ejecución incompatible

**Dado** una conciliación en curso  
**Cuando** se intenta iniciar otra incompatible sobre las mismas operaciones  
**Entonces** el sistema evita el procesamiento simultáneo y los registros duplicados.

### AC-010-09 — Manual por superadministrador

**Dado** un superadministrador  
**Cuando** inicia una conciliación manual para cualquier club o disciplina  
**Entonces** la ejecución respeta el alcance y queda auditada.

### AC-010-10 — Manual por administrador

**Dado** un administrador de disciplina  
**Cuando** ejecuta una conciliación sobre sus operaciones  
**Entonces** puede procesar pendientes, rango de fechas u operación individual  
**Pero** no puede incluir otra disciplina.

### AC-010-11 — Operación fuera de ámbito

**Dado** un identificador de operación ajeno al ámbito autorizado  
**Cuando** se solicita su conciliación  
**Entonces** el backend rechaza la ejecución.

### AC-010-12 — Delegado en consulta

**Dado** un delegado general  
**Cuando** consulta resultados de su club  
**Entonces** puede visualizarlos  
**Pero cuando** intenta iniciar una conciliación  
**Entonces** el backend la rechaza.

### AC-010-13 — Auditoría manual

**Dado** una ejecución manual autorizada  
**Cuando** finaliza  
**Entonces** registra usuario, fecha, hora, alcance, parámetros, operaciones, resultados y errores  
**Y** no cambia la frecuencia automática.

### AC-010-14 — Integración de resultados

**Dado** resultados de una ejecución automática o manual  
**Cuando** se incorporan al historial  
**Entonces** crean o actualizan las diferencias correspondientes sin duplicarlas  
**Y** no modifican silenciosamente los pagos originales.

## Pendientes no aceptables todavía

No pueden cerrarse criterios de rangos válidos, corte temporal ni incompatibilidad concurrente exacta hasta contar con definiciones aprobadas y validación de pasarela.

## Trazabilidad

Cada escenario deriva de `SPEC-010`, DEC-086–DEC-090 y RN-0624–RN-0672.
