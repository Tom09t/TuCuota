# ACCEPTANCE-026 — Políticas, motivos y evidencia de devolución

**Estado:** aprobada  
**Especificación:** `SPEC-026`

## Escenarios

### AC-026-01 — Política asociada al pago

**Dado** un pago confirmado bajo una versión vigente  
**Cuando** posteriormente solicita una devolución  
**Entonces** se evalúa con la versión asociada al pago.

### AC-026-02 — Nueva versión prospectiva

**Dado** una política existente  
**Cuando** el administrador crea una nueva versión  
**Entonces** solo se aplica a pagos posteriores a su vigencia.

### AC-026-03 — Plazo histórico protegido

**Dado** un pago con un plazo informado  
**Cuando** una política nueva lo reduce  
**Entonces** el pago conserva el plazo de su versión original.

### AC-026-04 — Versión inalterable

**Dado** una política usada por pagos  
**Cuando** se intenta sobrescribirla o eliminarla  
**Entonces** el sistema lo impide y exige crear otra versión.

### AC-026-05 — Motivo predefinido

**Dado** una solicitud nueva  
**Cuando** el responsable no selecciona un motivo  
**Entonces** el sistema impide enviarla.

### AC-026-06 — Motivo otro

**Dado** el motivo *Otro*  
**Cuando** no se agrega explicación  
**Entonces** el sistema rechaza la solicitud.

### AC-026-07 — Motivo no asigna responsabilidad

**Dado** un responsable que declara error técnico  
**Cuando** el administrador revisa la solicitud  
**Entonces** el costo no se asigna a la plataforma sin causa verificada y justificada.

### AC-026-08 — Causa verificada

**Dado** antecedentes y evidencia evaluados  
**Cuando** el administrador clasifica la causa  
**Entonces** se conserva separada del motivo declarado, con actor, fecha y justificación.

### AC-026-09 — Archivo opcional

**Dado** una solicitud válida sin adjuntos  
**Cuando** se envía  
**Entonces** la ausencia de archivo no impide crearla.

### AC-026-10 — Formato permitido

**Dado** un archivo PDF, JPG o PNG válido  
**Cuando** cumple controles y límites  
**Entonces** se almacena cifrado y vinculado con la solicitud.

### AC-026-11 — Archivo inseguro o inválido

**Dado** un contenido malicioso, tipo no admitido o límite excedido  
**Cuando** se intenta adjuntar  
**Entonces** el sistema rechaza el archivo.

### AC-026-12 — Acceso a evidencia

**Dado** un usuario autenticado  
**Cuando** consulta o descarga evidencia  
**Entonces** se valida su permiso sobre la solicitud y se audita el acceso.

### AC-026-13 — Evidencia adicional

**Dado** una solicitud que admite información complementaria  
**Cuando** el administrador pide evidencia y el responsable la aporta  
**Entonces** se agrega al expediente sin aprobarlo automáticamente.

### AC-026-14 — Acceso fuera de ámbito

**Dado** un archivo de otra disciplina o solicitud no autorizada  
**Cuando** se intenta acceder  
**Entonces** el backend rechaza la operación.

### AC-026-15 — Preservación necesaria

**Dado** evidencia requerida por una disputa o auditoría  
**Cuando** alcanza el plazo ordinario  
**Entonces** no se elimina mientras exista la obligación de preservación.

## Pendientes no aceptables todavía

No pueden cerrarse criterios del catálogo inicial, límites de archivos, cierre de evidencia ni revisión compartida hasta contar con definiciones aprobadas.

## Trazabilidad

Cada escenario deriva de `SPEC-026`, DEC-155–DEC-157 y RN-1612–RN-1661.
