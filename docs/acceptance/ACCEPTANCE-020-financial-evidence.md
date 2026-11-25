# ACCEPTANCE-020 — Evidencia financiera

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-020`

## Escenarios

### AC-020-01 — Retención global

**Dado** un superadministrador  
**Cuando** modifica el período global en años  
**Entonces** se aplica a todos los ámbitos y se auditan valores anterior y nuevo.

### AC-020-02 — Archivo vencido eliminable

**Dado** una evidencia que alcanzó el plazo sin obligación de preservación  
**Cuando** se ejecuta la limpieza  
**Entonces** se elimina el archivo y se registra la acción  
**Y** permanecen operación, resolución y auditoría.

### AC-020-03 — Preservación activa

**Dado** una evidencia vencida vinculada con una disputa, investigación o revisión abierta  
**Cuando** se evalúa su eliminación  
**Entonces** el archivo permanece protegido.

### AC-020-04 — Fin de preservación

**Dado** una causa de preservación finalizada  
**Cuando** se reevalúa la evidencia  
**Entonces** se elimina solo si cumple retención y no existe otra obligación.

### AC-020-05 — Acceso autorizado

**Dado** un usuario autenticado con permiso sobre la operación  
**Cuando** solicita una evidencia  
**Entonces** recibe un acceso temporal seguro y la consulta queda auditada.

### AC-020-06 — Acceso fuera de ámbito

**Dado** un usuario sin permiso sobre club, disciplina u operación  
**Cuando** intenta consultar o descargar  
**Entonces** el sistema rechaza y audita la solicitud.

### AC-020-07 — Enlace vencido

**Dado** un acceso temporal vencido  
**Cuando** se intenta reutilizar  
**Entonces** no permite descargar el archivo.

### AC-020-08 — Almacenamiento protegido

**Dado** una evidencia almacenada  
**Cuando** permanece en el repositorio  
**Entonces** está cifrada, aislada por ámbito y sin enlace público permanente.

### AC-020-09 — Inmutabilidad

**Dado** una evidencia existente  
**Cuando** se intenta sobrescribir su contenido  
**Entonces** el sistema rechaza la operación.

### AC-020-10 — Versión corregida

**Dado** un usuario autorizado y un motivo  
**Cuando** incorpora una corrección  
**Entonces** se crea una versión con identificador, actor y fecha vinculada con la original.

### AC-020-11 — Corrección sin motivo

**Dado** una evidencia existente  
**Cuando** se intenta agregar una versión sin motivo  
**Entonces** el sistema rechaza la incorporación.

### AC-020-12 — Versión reemplazada

**Dado** una nueva versión aceptada  
**Cuando** se consulta la evidencia  
**Entonces** la anterior figura reemplazada pero sigue disponible mientras corresponda conservarla.

### AC-020-13 — Resolución no reescrita

**Dado** una resolución financiera confirmada  
**Cuando** se incorpora evidencia corregida  
**Entonces** la resolución no cambia automáticamente.

### AC-020-14 — Contradicción posterior

**Dado** una corrección que contradice la evidencia usada al resolver  
**Cuando** se incorpora  
**Entonces** se señala el caso para revisión y se conserva toda la trazabilidad.

## Pendientes no aceptables todavía

No pueden cerrarse criterios cuantitativos o legales sobre retención, formatos, tamaños, controles de archivo ni duración de enlaces hasta contar con definiciones aprobadas.

## Trazabilidad

Cada escenario deriva de `SPEC-020`, DEC-140–DEC-142 y RN-1351–RN-1400.
