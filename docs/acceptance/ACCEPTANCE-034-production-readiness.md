# ACCEPTANCE-034 — Preparación y habilitación de cobros reales

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-034`

## Escenarios

### AC-034-01 — Funcionalidad esencial incompleta

**Dado** una funcionalidad esencial del MVP no implementada o no verificada  
**Cuando** se evalúa la salida a Producción  
**Entonces** los cobros reales permanecen bloqueados.

### AC-034-02 — Seguridad validada

**Dado** una versión candidata  
**Cuando** se evalúan autenticación, autorización y aislamiento  
**Entonces** cada control debe contar con resultado satisfactorio y evidencia.

### AC-034-03 — Split Payment incompleto

**Dado** una integración técnicamente conectada  
**Cuando** falta validación operativa, comercial, legal o contractual  
**Entonces** Split Payment no se considera aprobado para dinero real.

### AC-034-04 — Distribución y comisiones

**Dado** una prueba financiera de la versión candidata  
**Cuando** se procesan fondos de prueba permitidos  
**Entonces** se verifica la distribución y las comisiones conforme a las condiciones aplicables.

### AC-034-05 — Idempotencia y duplicados

**Dado** escenarios repetidos de cobro  
**Cuando** se ejecutan las pruebas de salida  
**Entonces** la idempotencia y prevención de pagos duplicados producen resultados satisfactorios.

### AC-034-06 — Incertidumbre y conciliación

**Dado** escenarios de pago incierto y reintento  
**Cuando** se prueban  
**Entonces** mantienen el bloqueo seguro y permiten conciliación sin duplicar cobros.

### AC-034-07 — Migración aprobada

**Dado** datos iniciales del piloto  
**Cuando** se evalúa el checklist  
**Entonces** la migración debe estar validada y aprobada con evidencia trazable.

### AC-034-08 — Recuperación probada

**Dado** backups configurados  
**Cuando** se evalúa la preparación  
**Entonces** existe evidencia satisfactoria de restauración y recuperación.

### AC-034-09 — Monitoreo operativo

**Dado** la versión candidata en el entorno correspondiente  
**Cuando** se evalúa el monitoreo  
**Entonces** las comprobaciones automáticas y alertas técnicas están operativas.

### AC-034-10 — Procedimientos ejecutables

**Dado** incidentes financieros, devoluciones y contracargos posibles  
**Cuando** se revisa la salida  
**Entonces** sus procedimientos están documentados y pueden ejecutarse por los actores autorizados.

### AC-034-11 — Aprobación técnica ausente

**Dado** todos los controles técnicos con resultado satisfactorio  
**Cuando** falta la aprobación formal del responsable técnico  
**Entonces** no se habilitan cobros reales.

### AC-034-12 — Autorización administrativa ausente

**Dado** una versión técnicamente aprobada  
**Cuando** el superadministrador aún no autorizó el piloto  
**Entonces** los cobros reales permanecen bloqueados.

### AC-034-13 — Requisito crítico fallido

**Dado** cualquier requisito crítico incumplido  
**Cuando** se intenta cerrar el checklist  
**Entonces** la habilitación se rechaza y conserva evidencia del bloqueo.

### AC-034-14 — Pendiente no crítico

**Dado** un pendiente clasificado justificadamente como no crítico  
**Cuando** se pretende aceptar  
**Entonces** requiere documentación, evaluación y autorización trazables.

### AC-034-15 — Evidencia incompleta

**Dado** un ítem marcado sin evidencia suficiente  
**Cuando** se verifica el checklist  
**Entonces** el ítem no se considera cumplido.

### AC-034-16 — Ampliación posterior

**Dado** el piloto inicialmente autorizado  
**Cuando** se solicita ampliar su alcance  
**Entonces** se ejecutan nuevamente las validaciones exigidas por `SPEC-029`.

### AC-034-17 — Condición crítica posterior

**Dado** el piloto habilitado  
**Cuando** se detecta una condición crítica  
**Entonces** se suspenden nuevas operaciones afectadas y se preserva la conciliación de pagos anteriores.

### AC-034-18 — Reanudación controlada

**Dado** operaciones suspendidas por una condición crítica  
**Cuando** se resuelve la causa  
**Entonces** solo se reanudan después de volver a validar los controles aplicables y registrar la decisión.

## Pendientes no aceptables todavía

No pueden cerrarse formato, evidencias mínimas, vigencias, aprobador técnico nominal, procedimiento de pendientes, protocolo de suspensión ni frecuencia de revalidación sin decisiones aprobadas.

## Advertencia de estado

Estos escenarios definen aceptación futura. Su aprobación documental no demuestra implementación, no supera el checklist y no autoriza cobros reales.

## Trazabilidad

Cada escenario deriva de `SPEC-034`, DEC-170 y RN-1872–RN-1889, con dependencias en `SPEC-028`–`SPEC-033`.

