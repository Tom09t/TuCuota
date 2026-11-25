# ACCEPTANCE-031 — Backups, retención y recuperación financiera

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-031`

## Escenarios

### AC-031-01 — Backup automático

**Dado** la base de datos de Producción  
**Cuando** opera el servicio de copias  
**Entonces** genera backups automáticos y supervisa su ejecución.

### AC-031-02 — Copia protegida

**Dado** cualquier backup productivo  
**Cuando** se almacena  
**Entonces** permanece cifrado, aislado de Producción y restringido a personal técnico autorizado.

### AC-031-03 — Falla alertada

**Dado** un error de generación o conservación  
**Cuando** el monitoreo lo detecta  
**Entonces** produce una alerta operativa y conserva trazabilidad.

### AC-031-04 — Ventana PITR

**Dado** la política de Producción  
**Cuando** se verifica la recuperación reciente  
**Entonces** mantiene una ventana objetivo PITR de 30 días.

### AC-031-05 — Copias mensuales

**Dado** la política de retención  
**Cuando** transcurren los períodos mensuales  
**Entonces** conserva copias mensuales independientes de PITR durante 12 meses.

### AC-031-06 — Proveedor insuficiente

**Dado** un proveedor sin PITR de 30 días  
**Cuando** se evalúa el cumplimiento  
**Entonces** el requisito queda incumplido hasta implementar una solución equivalente.

### AC-031-07 — Integridad periódica

**Dado** copias retenidas  
**Cuando** se ejecuta el control periódico  
**Entonces** se comprueba su integridad y se registra el resultado.

### AC-031-08 — Restauración de punto reciente

**Dado** un punto reciente conservado  
**Cuando** se realiza una prueba de restauración  
**Entonces** se verifica que la información recuperada sea consistente y utilizable.

### AC-031-09 — Restauración mensual

**Dado** una copia mensual retenida  
**Cuando** se incluye en una prueba periódica  
**Entonces** se valida su restauración, consistencia y utilidad.

### AC-031-10 — Confidencialidad en pruebas

**Dado** datos recuperados durante una prueba  
**Cuando** se manipulan  
**Entonces** se mantienen confidencialidad, aislamiento y acceso autorizado por club.

### AC-031-11 — Restauración productiva auditada

**Dado** una restauración de Producción  
**Cuando** se ejecuta conforme al procedimiento  
**Entonces** quedan registrados y auditados la operación y su resultado.

### AC-031-12 — Consistencia posterior

**Dado** una restauración finalizada  
**Cuando** comienza la validación  
**Entonces** se comprueban cuotas, pagos y operaciones financieras antes de reanudar cobros.

### AC-031-13 — Conciliación con pasarela

**Dado** una base restaurada  
**Cuando** se reconstruye el estado financiero  
**Entonces** los pagos se reconcilian con los registros reales de la pasarela.

### AC-031-14 — Pago incierto restaurado

**Dado** un pago con resultado incierto después de restaurar  
**Cuando** se intenta cobrar nuevamente  
**Entonces** se impide el intento hasta verificar su estado y se mantienen los controles de idempotencia.

### AC-031-15 — Reanudación bloqueada

**Dado** inconsistencias financieras sin resolver  
**Cuando** se solicita reanudar operaciones  
**Entonces** los cobros permanecen bloqueados.

### AC-031-16 — Eliminación por vencimiento

**Dado** una copia que superó su retención  
**Cuando** no existe obligación legal ni medida de preservación  
**Entonces** se elimina de forma segura y trazable.

### AC-031-17 — Preservación obligatoria

**Dado** una copia vencida sujeta a obligación legal o medida de preservación  
**Cuando** se procesa la política de eliminación  
**Entonces** la copia no se elimina mientras la obligación permanezca vigente.

### AC-031-18 — Conservación legal independiente

**Dado** el vencimiento de un backup  
**Cuando** existen comprobantes, operaciones o auditorías con conservación legal propia  
**Entonces** su preservación no se considera satisfecha ni extinguida por la política de backups.

## Pendientes no aceptables todavía

No pueden cerrarse proveedor, frecuencias, responsables, RPO/RTO adicionales, claves, eliminación técnica ni formatos de evidencia sin decisiones técnicas aprobadas. Los 30 días de PITR y 12 meses de copias mensuales no son pendientes.

## Trazabilidad

Cada escenario deriva de `SPEC-031`, DEC-166–DEC-167 y RN-1801–RN-1834. La conciliación posterior es obligatoria para cualquier restauración.

