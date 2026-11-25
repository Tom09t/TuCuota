# ACCEPTANCE-016 — Acceso del responsable y vinculación con jugadores

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-016`

## Escenarios

### AC-016-01 — DNI sin verificación

**Dado** un DNI ingresado en el portal  
**Cuando** no se completa el OTP  
**Entonces** no se muestran cuotas, deuda ni datos personales  
**Y** la respuesta no revela innecesariamente si está registrado.

### AC-016-02 — OTP válido

**Dado** un código vigente enviado a un contacto verificado  
**Cuando** se utiliza correctamente por primera vez  
**Entonces** se habilita una sesión temporal con el alcance autorizado.

### AC-016-03 — OTP reutilizado o vencido

**Dado** un código usado o fuera de vigencia  
**Cuando** se intenta verificar  
**Entonces** el sistema rechaza la autenticación.

### AC-016-04 — Límites de seguridad

**Dado** intentos incorrectos o solicitudes reiteradas  
**Cuando** alcanzan los límites configurados  
**Entonces** el sistema restringe nuevos intentos o reenvíos y registra el evento.

### AC-016-05 — Contacto no verificado

**Dado** un jugador sin correo o teléfono verificado  
**Cuando** intenta acceder  
**Entonces** requiere el procedimiento seguro de vinculación inicial.

### AC-016-06 — Relación muchos a muchos

**Dado** varios responsables y jugadores  
**Cuando** se crean vínculos autorizados  
**Entonces** un jugador puede tener varios responsables y viceversa.

### AC-016-07 — Activación administrativa

**Dado** un administrador de disciplina autorizado  
**Cuando** confirma un vínculo dentro de su ámbito  
**Entonces** queda activo inmediatamente sin aprobación adicional  
**Y** se registra administrador, fecha y hora.

### AC-016-08 — Vínculo duplicado

**Dado** un vínculo activo entre responsable y jugador en un ámbito  
**Cuando** se intenta crear otro equivalente  
**Entonces** el sistema impide la duplicación.

### AC-016-09 — Vista unificada

**Dado** un responsable con varios vínculos vigentes y una sesión OTP válida  
**Cuando** consulta el portal  
**Entonces** ve únicamente los jugadores vinculados  
**Y** cada cuota conserva su club y disciplina.

### AC-016-10 — Sin presunción de parentesco

**Dado** responsable y jugador con apellido, domicilio o contacto coincidente  
**Cuando** no existe vínculo vigente  
**Entonces** el responsable no obtiene acceso.

### AC-016-11 — Administrador fuera de ámbito

**Dado** un administrador de disciplina  
**Cuando** intenta gestionar el vínculo de otra disciplina  
**Entonces** el backend rechaza la operación.

### AC-016-12 — Delegado en lectura

**Dado** un delegado general  
**Cuando** consulta vínculos de su club  
**Entonces** puede visualizarlos  
**Pero cuando** intenta crearlos, modificarlos o revocarlos  
**Entonces** el backend rechaza la operación.

### AC-016-13 — Responsable sin autogestión

**Dado** un responsable autenticado  
**Cuando** intenta agregar o revocar un jugador  
**Entonces** el sistema no permite la modificación.

### AC-016-14 — Revocación inmediata

**Dado** un vínculo vigente y una sesión activa  
**Cuando** un administrador autorizado lo revoca  
**Entonces** cesan inmediatamente nuevas consultas y pagos de ese jugador por ese responsable.

### AC-016-15 — Historia preservada

**Dado** un vínculo revocado  
**Cuando** se consultan registros previos  
**Entonces** pagos y comprobantes permanecen intactos  
**Y** el vínculo conserva su auditoría.

### AC-016-16 — Doble condición de acceso

**Dado** un responsable  
**Cuando** falta una sesión OTP válida o un vínculo vigente  
**Entonces** no puede consultar ni pagar cuotas del jugador.

## Pendientes no aceptables todavía

No pueden cerrarse criterios cuantitativos de OTP, sesión y límites ni el procedimiento de primer contacto hasta contar con definiciones aprobadas.

## Trazabilidad

Cada escenario deriva de `SPEC-016`, DEC-117–DEC-120, RN-1000–RN-1061 y CON-005, bajo la precedencia de CON-011. RN-1048 permanece histórica.
