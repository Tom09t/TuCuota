# ACCEPTANCE-028 — Alcance funcional inicial del MVP

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-028`

## Escenarios

### AC-028-01 — Administración esencial

**Dado** el alcance del MVP  
**Cuando** se verifica su cobertura funcional  
**Entonces** incluye clubes, disciplinas, usuarios, roles, permisos y aislamiento.

### AC-028-02 — Gestión deportiva

**Dado** el alcance del MVP  
**Cuando** se verifican sus entidades operativas  
**Entonces** incluye jugadores, inscripciones y categorías.

### AC-028-03 — Cuotas y beneficios

**Dado** el alcance del MVP  
**Cuando** se verifican sus capacidades económicas esenciales  
**Entonces** incluye configuración y generación automática de cuotas, becas y beneficios esenciales.

### AC-028-04 — Pagos externos

**Dado** un pago recibido fuera de la plataforma  
**Cuando** un usuario autorizado lo registra  
**Entonces** el MVP conserva el registro conforme a las reglas funcionales y de auditoría aprobadas.

### AC-028-05 — Portal del responsable

**Dado** un responsable autorizado  
**Cuando** utiliza el portal del MVP  
**Entonces** puede consultar y pagar únicamente cuotas permitidas por sus vínculos vigentes.

### AC-028-06 — Pago online condicionado

**Dado** una integración de pagos online  
**Cuando** Split Payment no está técnicamente validado  
**Entonces** el MVP no habilita esa capacidad con dinero real.

### AC-028-07 — Información financiera esencial

**Dado** operaciones financieras del MVP  
**Cuando** un administrador consulta su disciplina  
**Entonces** dispone de reportes básicos y conciliación esencial según las especificaciones aprobadas.

### AC-028-08 — Seguridad obligatoria

**Dado** cualquier capacidad incluida en el MVP  
**Cuando** se utiliza  
**Entonces** aplica autenticación, autorización, seguridad y aislamiento desde la primera entrega.

### AC-028-09 — Prevención de duplicados

**Dado** un comando financiero repetido  
**Cuando** el sistema reconoce la misma clave idempotente  
**Entonces** no genera un segundo cobro.

### AC-028-10 — Pago incierto

**Dado** un pago con resultado incierto  
**Cuando** se intenta pagar nuevamente  
**Entonces** se bloquea o controla el reintento hasta tratar de forma segura la operación anterior.

### AC-028-11 — Trazabilidad financiera

**Dado** una operación financiera  
**Cuando** cambia su estado  
**Entonces** conserva trazabilidad y auditoría conforme a las reglas vigentes.

### AC-028-12 — Procedimiento mínimo de devolución

**Dado** que el módulo avanzado de devoluciones se difiere  
**Cuando** el MVP inicia su operación  
**Entonces** existe igualmente un procedimiento operativo controlado para devoluciones y contracargos.

### AC-028-13 — Funcionalidad avanzada diferida

**Dado** un reporte avanzado, automatización, exportación o gestión avanzada de devoluciones  
**Cuando** se posterga para otra versión  
**Entonces** no se elimina su decisión funcional ni se debilitan los controles obligatorios.

### AC-028-14 — Bloqueo financiero

**Dado** el MVP técnicamente desplegado  
**Cuando** aún no están validadas pasarela, distribución de fondos y conciliación  
**Entonces** permanecen bloqueados los cobros reales.

### AC-028-15 — Compatibilidad futura

**Dado** una capacidad postergada  
**Cuando** se diseña o implementa posteriormente  
**Entonces** mantiene compatibilidad con las DEC, RN y especificaciones aprobadas.

## Pendientes no aceptables todavía

No pueden cerrarse el orden técnico, el cronograma ni nuevas clasificaciones entre capacidades básicas y avanzadas sin decisiones posteriores aprobadas.

## Trazabilidad

Cada escenario deriva de `SPEC-028`, DEC-161 y RN-1713–RN-1731. Las condiciones completas de habilitación financiera continúan bajo DEC-162–DEC-170.

