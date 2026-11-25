# ACCEPTANCE-012 — Exportaciones asíncronas y almacenamiento seguro

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-012`

## Escenarios

### AC-012-01 — Exportación pequeña

**Dado** una solicitud de volumen pequeño  
**Cuando** se procesa  
**Entonces** se genera y descarga inmediatamente dentro del ámbito autorizado.

### AC-012-02 — Exportación grande

**Dado** una solicitud de gran volumen  
**Cuando** se acepta  
**Entonces** recibe un identificador y se procesa en segundo plano  
**Y** el usuario puede continuar usando la plataforma.

### AC-012-03 — Ciclo de estados

**Dado** una exportación asíncrona  
**Cuando** progresa o finaliza  
**Entonces** su estado corresponde a pendiente, en proceso, completada o fallida.

### AC-012-04 — Archivo vigente

**Dado** un archivo completado  
**Cuando** no venció su retención y el usuario mantiene permisos  
**Entonces** puede descargarlo mediante un enlace temporal protegido.

### AC-012-05 — Retención vencida

**Dado** un archivo cuyo plazo terminó  
**Cuando** se ejecuta la limpieza  
**Entonces** se elimina el archivo y se registra el resultado  
**Y** permanecen la solicitud, auditoría y datos financieros originales.

### AC-012-06 — Nueva exportación

**Dado** un archivo vencido  
**Cuando** un usuario todavía autorizado solicita otro  
**Entonces** se crea una nueva exportación con filtros y permisos vigentes.

### AC-012-07 — Descarga compartida autorizada

**Dado** un archivo generado por otro usuario del mismo ámbito  
**Cuando** un usuario autenticado tiene permisos actuales sobre su contenido  
**Entonces** puede descargarlo.

### AC-012-08 — Permiso revocado

**Dado** un archivo todavía vigente  
**Cuando** el usuario perdió el permiso correspondiente  
**Entonces** la descarga es rechazada y auditada.

### AC-012-09 — Enlace conocido por tercero

**Dado** un enlace temporal asociado a un usuario autorizado  
**Cuando** un tercero intenta utilizarlo  
**Entonces** no obtiene el archivo.

### AC-012-10 — Enlace vencido

**Dado** un enlace temporal expirado  
**Cuando** se intenta descargar  
**Entonces** la solicitud es rechazada.

### AC-012-11 — Aislamiento de almacenamiento

**Dado** un archivo de otro club o disciplina  
**Cuando** se intenta acceder por identificador o enlace manipulado  
**Entonces** el backend impide la entrega y registra el intento.

### AC-012-12 — Almacenamiento privado

**Dado** un archivo generado  
**Cuando** permanece almacenado  
**Entonces** está cifrado, segregado por ámbito y no expone claves ni credenciales.

### AC-012-13 — Reintento automático

**Dado** una exportación grande con error recuperable  
**Cuando** se reintenta  
**Entonces** conserva el identificador original, registra el intento y no duplica el archivo final.

### AC-012-14 — Reintentos agotados

**Dado** una solicitud que agota sus intentos  
**Cuando** continúa sin completarse  
**Entonces** queda fallida y ningún archivo incompleto puede descargarse.

### AC-012-15 — Recuperación manual

**Dado** una exportación fallida  
**Cuando** un usuario autorizado inicia una nueva ejecución  
**Entonces** se revalidan permisos, filtros y seguridad  
**Y** se conserva la trazabilidad de intentos y recuperación.

## Pendientes no aceptables todavía

No pueden cerrarse criterios cuantitativos sobre umbral, retención, expiración ni reintentos hasta contar con valores aprobados.

## Trazabilidad

Cada escenario deriva de `SPEC-012`, DEC-097–DEC-102 y RN-0740–RN-0813.
