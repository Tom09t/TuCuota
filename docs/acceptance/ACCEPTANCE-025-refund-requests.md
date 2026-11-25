# ACCEPTANCE-025 — Solicitudes de devolución del responsable

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-025`

## Escenarios

### AC-025-01 — Solicitud válida

**Dado** un responsable autenticado con vínculo vigente  
**Cuando** selecciona cuotas completas del mismo checkout e informa motivo dentro del plazo  
**Entonces** se crea una solicitud para evaluación de la disciplina.

### AC-025-02 — Jugador no vinculado

**Dado** un responsable sin vínculo vigente con el jugador  
**Cuando** intenta solicitar una devolución  
**Entonces** el backend rechaza la operación.

### AC-025-03 — Selección inválida

**Dado** cuotas de checkouts distintos o una fracción monetaria  
**Cuando** se intenta crear la solicitud  
**Entonces** el sistema la rechaza.

### AC-025-04 — Solicitud duplicada

**Dado** una solicitud activa sobre una cuota  
**Cuando** se intenta crear otra equivalente  
**Entonces** el sistema impide la duplicación.

### AC-025-05 — Aprobación administrativa

**Dado** una solicitud pendiente de la disciplina  
**Cuando** un administrador autorizado la aprueba con justificación  
**Entonces** cambia el estado de la solicitud  
**Pero** no se considera ejecutada la devolución.

### AC-025-06 — Rechazo

**Dado** una solicitud pendiente  
**Cuando** el administrador la rechaza con justificación  
**Entonces** el responsable puede consultar el resultado y recibe una notificación.

### AC-025-07 — Costo de plataforma

**Dado** una solicitud aprobable con costo atribuible a TuCuota  
**Cuando** se requiere cubrir la diferencia  
**Entonces** interviene el superadministrador antes de comprometer fondos de plataforma.

### AC-025-08 — Cancelación pendiente

**Dado** una solicitud propia todavía pendiente de revisión  
**Cuando** el responsable la cancela  
**Entonces** queda cancelada, se notifica a la disciplina y permanece en historial.

### AC-025-09 — Cancelación aprobada sin ejecutar

**Dado** una solicitud aprobada cuya operación aún no comenzó  
**Cuando** el responsable la cancela  
**Entonces** no puede iniciar posteriormente una devolución.

### AC-025-10 — Cancelación tardía

**Dado** una devolución en proceso, confirmada o incierta  
**Cuando** el responsable intenta cancelarla  
**Entonces** el portal rechaza la operación.

### AC-025-11 — Concurrencia

**Dado** cancelación e inicio de ejecución simultáneos  
**Cuando** el sistema decide atómicamente  
**Entonces** solo una acción puede prevalecer y no quedan estados contradictorios.

### AC-025-12 — Fuera de plazo

**Dado** un pago que supera los días configurados  
**Cuando** el responsable intenta una solicitud ordinaria  
**Entonces** se bloquea y se informa el canal de revisión excepcional.

### AC-025-13 — Plazo modificado

**Dado** una solicitud presentada válidamente  
**Cuando** después cambia el plazo de la disciplina  
**Entonces** su historial y validez original permanecen.

### AC-025-14 — Obligación legal

**Dado** una obligación legal aplicable  
**Cuando** entra en conflicto con el plazo interno  
**Entonces** prevalece la obligación legal.

## Pendientes no aceptables todavía

No pueden cerrarse criterios cuantitativos, estados completos ni revisión excepcional hasta contar con definiciones aprobadas.

## Trazabilidad

Cada escenario deriva de `SPEC-025`, DEC-152–DEC-154 y RN-1565–RN-1611. CON-020 permanece descartada.
