# ACCEPTANCE-007 — Contratos, renovaciones y alertas comerciales

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-007`

## Escenarios

### AC-007-01 — Vencimiento sin suspensión

**Dado** un contrato que alcanza su fecha final  
**Cuando** se procesa el vencimiento  
**Entonces** se alerta al superadministrador  
**Y** el responsable no se suspende automáticamente.

### AC-007-02 — Continuidad hasta decisión

**Dado** un contrato vencido sin decisión  
**Cuando** continúa la operación  
**Entonces** se mantienen sus condiciones, conteo de pagos y evaluación de mínimos.

### AC-007-03 — Borrador de renovación

**Dado** un contrato existente  
**Cuando** el superadministrador inicia su renovación  
**Entonces** se crea una nueva versión en borrador con las condiciones copiadas.

### AC-007-04 — Confirmación futura

**Dado** una renovación en borrador  
**Cuando** se confirma para un mes futuro  
**Entonces** sus condiciones aplican al mes de inicio completo  
**Y** hasta entonces siguen las condiciones vigentes.

### AC-007-05 — Superposición contractual

**Dado** una versión vigente o programada  
**Cuando** otra versión intenta superponer su vigencia  
**Entonces** el sistema rechaza la configuración.

### AC-007-06 — Edición antes del inicio

**Dado** una renovación confirmada todavía futura  
**Cuando** el superadministrador modifica sus condiciones  
**Entonces** conserva valores anterior y nuevo, fecha, hora y responsable  
**Y** el contrato actual no cambia.

### AC-007-07 — Inicio de renovación

**Dado** una renovación futura confirmada  
**Cuando** comienza su mes de vigencia  
**Entonces** entra en vigor la última configuración confirmada  
**Y** no coexiste otra versión vigente.

### AC-007-08 — Preservación histórica

**Dado** una renovación activada  
**Cuando** se consultan versiones y liquidaciones anteriores  
**Entonces** conservan las condiciones y referencias usadas originalmente.

### AC-007-09 — Cancelación programada

**Dado** una renovación que todavía no inició  
**Cuando** el superadministrador la cancela con justificación  
**Entonces** pasa a *Cancelada*, no entra en vigencia y no altera el contrato actual.

### AC-007-10 — Cancelación fuera de plazo

**Dado** una renovación ya vigente  
**Cuando** se intenta cancelarla como renovación programada  
**Entonces** el sistema rechaza la operación.

### AC-007-11 — Continuidad temporal

**Dado** un contrato vencido  
**Cuando** el superadministrador autoriza continuidad hasta una fecha  
**Entonces** se mantienen condiciones, conteo y mínimos  
**Y** se registra la autorización.

### AC-007-12 — Fin de continuidad

**Dado** una continuidad que alcanza su límite  
**Cuando** no existe nueva decisión  
**Entonces** se alerta al superadministrador sin suspensión automática  
**Y** la operación continúa bajo las condiciones existentes.

### AC-007-13 — Alertas previas

**Dado** un contrato o continuidad con vencimiento futuro  
**Cuando** faltan 30, 15 o 7 días calendario  
**Entonces** se genera una alerta interna para el superadministrador con responsable y fecha.

### AC-007-14 — Alerta duplicada

**Dado** una alerta ya generada para vencimiento y plazo  
**Cuando** vuelve a ejecutarse su procesamiento  
**Entonces** no se genera otra equivalente.

### AC-007-15 — Reprogramación

**Dado** alertas pendientes  
**Cuando** se renueva el contrato o se extiende la continuidad  
**Entonces** se actualiza su programación según la nueva fecha.

### AC-007-16 — Resolución informativa

**Dado** una alerta comercial activa  
**Cuando** se resuelve la situación que la originó  
**Entonces** desaparece automáticamente de la vista activa  
**Y** permanece en el registro técnico  
**Y** no modifica por sí misma ningún contrato.

## Pendientes no aceptables todavía

No pueden cerrarse criterios para alertas configuradas con anticipación insuficiente ni para la hora y zona temporal de activación hasta contar con definición aprobada.

## Trazabilidad

Cada escenario deriva de `SPEC-007`, DEC-069–DEC-077 y RN-0468–RN-0542.
