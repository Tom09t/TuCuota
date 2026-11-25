# ACCEPTANCE-011 — Recuperación, historial y exportación de conciliaciones

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-011`

## Escenarios

### AC-011-01 — Reintento selectivo

**Dado** una ejecución con operaciones exitosas y fallidas  
**Cuando** se ejecuta un reintento automático  
**Entonces** solo se reprocesan las fallidas o pendientes  
**Y** los resultados exitosos permanecen intactos.

### AC-011-02 — Auditoría del intento

**Dado** un reintento  
**Cuando** finaliza  
**Entonces** registra fecha, hora, operación, resultado y error sin duplicar conciliaciones.

### AC-011-03 — Reintentos agotados

**Dado** una operación que agota el máximo configurado  
**Cuando** continúa sin conciliar  
**Entonces** queda pendiente por error  
**Y** se alerta internamente al superadministrador y administradores afectados.

### AC-011-04 — Recuperación manual

**Dado** operaciones pendientes por error  
**Cuando** un usuario autorizado inicia su recuperación dentro de su ámbito  
**Entonces** se respetan permisos y límites de pasarela  
**Y** no se duplican resultados.

### AC-011-05 — Alerta resuelta

**Dado** una alerta por reintentos agotados  
**Cuando** se recuperan las operaciones afectadas  
**Entonces** desaparece de la vista activa y conserva su historial.

### AC-011-06 — Conservación completa

**Dado** conciliaciones antiguas  
**Cuando** transcurre el tiempo  
**Entonces** no se eliminan automáticamente por antigüedad  
**Y** conservan ejecuciones, diferencias, resoluciones y acciones.

### AC-011-07 — Archivo automático

**Dado** una antigüedad global configurada  
**Cuando** un registro resuelto cumple el umbral  
**Entonces** se archiva automáticamente sin perder trazabilidad ni permisos.

### AC-011-08 — Pendiente no archivable

**Dado** una conciliación o diferencia pendiente  
**Cuando** alcanza la antigüedad configurada  
**Entonces** el archivo no interfiere con su resolución.

### AC-011-09 — Consulta unificada

**Dado** registros operativos y archivados  
**Cuando** un usuario autorizado busca por filtros  
**Entonces** obtiene resultados en el mismo módulo sin seleccionar almacenamiento.

### AC-011-10 — Consulta histórica lenta

**Dado** una búsqueda que requiere recuperar archivo histórico  
**Cuando** demanda mayor procesamiento  
**Entonces** se informa al usuario sin modificar los registros.

### AC-011-11 — Ámbito de consulta

**Dado** un administrador, delegado o superadministrador  
**Cuando** consulta el historial  
**Entonces** accede respectivamente a su disciplina, su club en lectura o toda la plataforma.

### AC-011-12 — Exportación filtrada

**Dado** una consulta autorizada con filtros  
**Cuando** se exporta a Excel o PDF  
**Entonces** el archivo respeta filtros y permisos e incluye operaciones, diferencias, resoluciones y auditoría.

### AC-011-13 — Auditoría de exportación

**Dado** una exportación solicitada  
**Cuando** se procesa  
**Entonces** se registran usuario, fecha, hora, formato y alcance.

### AC-011-14 — Exportación histórica extensa

**Dado** una exportación que requiere gran volumen archivado  
**Cuando** comienza su recuperación  
**Entonces** se informa su estado de procesamiento  
**Y** los registros originales no cambian.

### AC-011-15 — Obligación legal

**Dado** una obligación aplicable de eliminación o anonimización  
**Cuando** entra en conflicto con la conservación ordinaria  
**Entonces** prevalece la obligación legal sin perder la trazabilidad permitida por dicha obligación.

## Pendientes no aceptables todavía

No pueden cerrarse criterios sobre rangos de reintento, seguimiento detallado de exportaciones históricas ni retención legal concreta hasta contar con definiciones aprobadas.

## Trazabilidad

Cada escenario deriva de `SPEC-011`, DEC-091–DEC-096 y RN-0673–RN-0739.
