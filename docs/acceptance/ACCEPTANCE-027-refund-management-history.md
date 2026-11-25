# ACCEPTANCE-027 — Gestión, historial y bandeja de devoluciones

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-027`

## Escenarios

### AC-027-01 — Solicitud de información externa

**Dado** un expediente que requiere información adicional  
**Cuando** el administrador de la disciplina contacta al responsable  
**Entonces** utiliza datos y canales autorizados fuera del expediente, sin chat interno.

### AC-027-02 — Evidencia recibida externamente

**Dado** un comprobante recibido por un canal externo  
**Cuando** un administrador autorizado lo incorpora  
**Entonces** queda protegido, aislado y auditado con los controles de `SPEC-026`.

### AC-027-03 — Falta de respuesta

**Dado** un responsable que no responde al pedido de información  
**Cuando** continúa la evaluación  
**Entonces** la solicitud no se rechaza automáticamente y la decisión permanece a cargo del administrador.

### AC-027-04 — Decisión registrada

**Dado** un expediente evaluado  
**Cuando** el administrador adopta la decisión final  
**Entonces** registra decisión y justificación, respetando plazos de pasarela y obligaciones legales.

### AC-027-05 — Historial autorizado

**Dado** un responsable con vínculo vigente  
**Cuando** consulta su historial  
**Entonces** ve únicamente solicitudes y devoluciones para las que conserva acceso autorizado.

### AC-027-06 — Detalle comunicable

**Dado** un registro visible en el historial  
**Cuando** se abre su detalle  
**Entonces** muestra disciplina, cuotas, fecha, motivo, importe solicitado, estado y resolución comunicable.

### AC-027-07 — Aprobación sin confirmación

**Dado** una solicitud administrativamente aprobada  
**Cuando** la pasarela aún no confirmó la devolución  
**Entonces** el historial distingue ambos hechos y no informa un importe efectivamente devuelto.

### AC-027-08 — Devolución confirmada

**Dado** una confirmación válida de la pasarela  
**Cuando** se actualiza el historial  
**Entonces** se muestra el importe devuelto, los costos aplicables al pagador y el comprobante disponible.

### AC-027-09 — Información reservada

**Dado** un expediente con observaciones internas o evidencia reservada  
**Cuando** el responsable consulta su historial  
**Entonces** esos datos y la información financiera privada no se exponen.

### AC-027-10 — Solicitud ajena

**Dado** un responsable autenticado  
**Cuando** intenta acceder a la solicitud de otro responsable sin autorización  
**Entonces** el backend deniega el acceso.

### AC-027-11 — Estados en bandeja

**Dado** solicitudes en distintos estados  
**Cuando** un administrador abre la bandeja  
**Entonces** puede distinguir pendientes, aprobadas, rechazadas, canceladas, en proceso y completadas.

### AC-027-12 — Filtros y búsqueda

**Dado** una bandeja con múltiples solicitudes  
**Cuando** se aplican estado, fechas, motivo, jugador o responsable  
**Entonces** los resultados se filtran dentro del ámbito autorizado.

### AC-027-13 — Ámbito de disciplina

**Dado** un administrador de disciplina  
**Cuando** consulta o gestiona la bandeja  
**Entonces** solo accede a solicitudes de su propia disciplina.

### AC-027-14 — Vista global

**Dado** un superadministrador autorizado  
**Cuando** usa la vista global y filtra por club o disciplina  
**Entonces** consulta los resultados permitidos sin obtener permisos financieros adicionales.

### AC-027-15 — Acción no autorizada

**Dado** una acción incompatible con el estado o los permisos  
**Cuando** el usuario intenta ejecutarla desde la bandeja  
**Entonces** el backend la rechaza y conserva la trazabilidad correspondiente.

### AC-027-16 — Cambio de estado

**Dado** una transición válida de la solicitud  
**Cuando** se confirma el cambio  
**Entonces** la bandeja y el historial lo reflejan sin confundir aprobación con devolución confirmada.

### AC-027-17 — Paginación aislada

**Dado** un volumen que requiere varias páginas  
**Cuando** el usuario navega o busca resultados  
**Entonces** cada página mantiene el aislamiento por club, disciplina y autorización.

## Pendientes no aceptables todavía

No pueden cerrarse canales externos, resumen exacto, prioridad, paginación, ordenamiento, límites de búsqueda ni disponibilidad temporal de comprobantes hasta contar con definiciones aprobadas.

## Trazabilidad

Cada escenario deriva de `SPEC-027`, DEC-158–DEC-160 y RN-1662–RN-1712, con los controles de evidencia de DEC-157 y `SPEC-026`.

