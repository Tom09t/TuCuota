# ACCEPTANCE-001 — Multiclub, disciplinas y acceso administrativo

**Estado:** aprobada el 2026-11-25
**Especificación:** `SPEC-001`

## Escenarios

### AC-001-01 — Alta de club

**Dado** un superadministrador autenticado
**Cuando** crea un club con identificador y slug no utilizados
**Entonces** el club queda registrado con portal y ámbito propios
**Y** un usuario sin rol de superadministrador no puede ejecutar la misma operación.

### AC-001-02 — Unicidad y aislamiento

**Dado** que existen dos clubes
**Cuando** se intenta reutilizar el slug del primero
**Entonces** el alta es rechazada
**Y** una cuenta del segundo club no puede consultar ni modificar información del primero.

### AC-001-03 — Inactivación de club

**Dado** un club activo con cuotas pendientes e historial
**Cuando** el superadministrador lo inactiva
**Entonces** no se eliminan datos ni se generan cuotas nuevas
**Y** las cuotas preexistentes continúan admitiendo pagos.

### AC-001-04 — Reactivación de club

**Dado** un club que estuvo inactivo durante varios períodos
**Cuando** el superadministrador lo reactiva
**Entonces** se registra la fecha de reactivación
**Y** no se generan cuotas por los períodos inactivos
**Y** la generación futura utiliza la configuración vigente.

### AC-001-05 — Creación y aislamiento de disciplina

**Dado** un club activo
**Cuando** su delegado crea una disciplina
**Entonces** la nueva disciplina obtiene administración, categorías, inscripciones y cuotas independientes
**Y** no modifica datos de otras disciplinas.

### AC-001-06 — Estado de disciplina

**Dado** una disciplina activa con deuda preexistente
**Cuando** el superadministrador la desactiva
**Entonces** no admite nuevas inscripciones ni cuotas nuevas
**Y** permite pagar la deuda preexistente
**Y** las demás disciplinas continúan operando.

### AC-001-07 — Cuenta por club

**Dado** un correo ya registrado en un club
**Cuando** se registra el mismo correo en otro club
**Entonces** se crea una cuenta independiente
**Y** sus credenciales, permisos, estado y notificaciones no se comparten.

### AC-001-08 — Límites del delegado

**Dado** un delegado general autenticado
**Cuando** consulta información consolidada de su club
**Entonces** puede verla
**Pero cuando** intenta modificar jugadores, cuotas, pagos, devoluciones o costos
**Entonces** la operación es rechazada por el backend.

### AC-001-09 — Último administrador de disciplina

**Dado** que una disciplina tiene un único administrador activo
**Cuando** un delegado u otro administrador intenta desactivarlo
**Entonces** la operación es rechazada
**Y cuando** el superadministrador ejecuta la excepción con justificación
**Entonces** se registran actor, fecha, disciplina, cuenta y motivo.

### AC-001-10 — Último delegado

**Dado** que un club tiene un único delegado activo
**Cuando** otro delegado intenta desactivarlo
**Entonces** la operación es rechazada
**Y** solo el superadministrador puede ejecutar y auditar la excepción o restaurar el acceso.

### AC-001-11 — Identidad visual

**Dado** un club activo
**Cuando** su delegado modifica nombre visible, logotipo o colores
**Entonces** el cambio aparece en portal y panel
**Y** queda auditado
**Pero** un administrador de disciplina no puede modificarlo.

### AC-001-12 — Autorización en backend

**Dado** una solicitud manipulada desde el cliente
**Cuando** la cuenta no pertenece al club o disciplina solicitados
**Entonces** el backend rechaza la operación sin exponer información del ámbito ajeno.

## Trazabilidad

Cada escenario deriva de `SPEC-001`, DEC-001–012, DEC-019–026, RN-0001–0070, RN-0102–0153 y CON-004–014.
