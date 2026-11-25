# ACCEPTANCE-032 — Monitoreo de Producción y alertas técnicas

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-032`

## Escenarios

### AC-032-01 — Disponibilidad de API

**Dado** la API o un servicio esencial de Producción  
**Cuando** deja de estar disponible  
**Entonces** el monitoreo detecta el problema y genera el incidente técnico correspondiente.

### AC-032-02 — Estado de base de datos

**Dado** una falla de disponibilidad u operación de la base de datos  
**Cuando** ocurre en Producción  
**Entonces** el monitoreo la detecta y alerta al responsable técnico.

### AC-032-03 — Generación de cuotas

**Dado** una ejecución automática de generación de cuotas  
**Cuando** falla  
**Entonces** la falla queda detectada, alertada y registrada.

### AC-032-04 — Procesamiento de pagos

**Dado** un error crítico durante el procesamiento de pagos  
**Cuando** el monitoreo lo identifica  
**Entonces** crea el incidente y envía una alerta técnica segura.

### AC-032-05 — Conciliación esencial

**Dado** un proceso esencial de conciliación  
**Cuando** su ejecución falla  
**Entonces** el sistema registra la falla y avisa al destinatario técnico configurado.

### AC-032-06 — Backup fallido

**Dado** una ejecución automática de backup  
**Cuando** falla  
**Entonces** el monitoreo genera una alerta operativa técnica y conserva la evidencia del incidente.

### AC-032-07 — Contenido mínimo

**Dado** un problema detectado  
**Cuando** se genera el correo técnico  
**Entonces** identifica servicio, problema y momento de detección.

### AC-032-08 — Información sensible excluida

**Dado** una alerta técnica  
**Cuando** se compone su contenido  
**Entonces** no incluye contraseñas, tokens ni datos financieros sensibles.

### AC-032-09 — Destinatario actualizado

**Dado** un responsable técnico configurado y vigente  
**Cuando** se genera una alerta  
**Entonces** el correo se dirige a la dirección técnica mantenida para ese fin.

### AC-032-10 — Incidente persistente

**Dado** una misma falla que continúa activa  
**Cuando** se repite la misma detección  
**Entonces** se evita el envío repetitivo de alertas idénticas sin perder el registro del incidente.

### AC-032-11 — Falla nueva

**Dado** un incidente activo con alertas deduplicadas  
**Cuando** aparece una falla distinta o nueva  
**Entonces** se genera su alerta correspondiente.

### AC-032-12 — Recuperación registrada

**Dado** un incidente técnico detectado  
**Cuando** el servicio se recupera o el incidente se resuelve  
**Entonces** se conserva el registro de recuperación o resolución.

### AC-032-13 — Notificación operativa independiente

**Dado** un incidente que también exige notificar a administradores  
**Cuando** se envía el correo al responsable técnico  
**Entonces** continúan aplicándose separadamente las notificaciones operativas aprobadas.

### AC-032-14 — Alcance mínimo del MVP

**Dado** el monitoreo automático y correo técnico implementados  
**Cuando** no existen canales adicionales ni clasificación avanzada  
**Entonces** esa ausencia no incumple DEC-168 durante el MVP.

## Pendientes no aceptables todavía

No pueden cerrarse proveedor, destinatario nominal, servicios concretos, frecuencias, umbrales, deduplicación, retención, escalamiento ni canales adicionales sin decisiones técnicas aprobadas.

## Trazabilidad

Cada escenario deriva de `SPEC-032`, DEC-168 y RN-1835–RN-1851. Las alertas técnicas no sustituyen las notificaciones operativas vigentes.

