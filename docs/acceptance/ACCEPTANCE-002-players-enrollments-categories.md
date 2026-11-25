# ACCEPTANCE-002 — Jugadores, inscripciones y categorías

**Estado:** aprobada el 2026-11-25
**Especificación:** `SPEC-002`

## Escenarios

### AC-002-01 — Alta individual

**Dado** un administrador de disciplina autorizado
**Cuando** registra DNI, nombre, apellido y una categoría de su disciplina
**Entonces** se crea o vincula el jugador global
**Y** se crea la ficha del club y la inscripción correspondiente.

### AC-002-02 — Datos opcionales

**Dado** un alta con DNI, nombre y apellido válidos
**Cuando** faltan fecha de nacimiento, teléfono, correo o tutor
**Entonces** el alta no es rechazada por esos datos ausentes.

### AC-002-03 — Importación dentro del ámbito

**Dado** un archivo Excel de jugadores para una disciplina
**Cuando** su administrador ejecuta la importación
**Entonces** solo se incorporan registros al club y disciplina autorizados
**Y** no puede utilizar categorías pertenecientes a otra disciplina.

### AC-002-04 — DNI existente en otro club

**Dado** un DNI registrado en un club
**Cuando** otro club registra al mismo jugador
**Entonces** se reutiliza la identidad global
**Y** se crea una ficha independiente para el segundo club
**Y** no se exponen datos financieros del primero.

### AC-002-05 — Modificación de ficha

**Dado** un jugador vinculado a dos clubes
**Cuando** un administrador modifica su ficha en uno
**Entonces** la ficha del otro club permanece sin cambios.

### AC-002-06 — Varias disciplinas

**Dado** un jugador vinculado a un club
**Cuando** se lo inscribe en una segunda disciplina
**Entonces** ambas inscripciones mantienen categoría, estado e historial independientes.

### AC-002-07 — Inscripción duplicada

**Dado** un jugador con inscripción activa en una disciplina
**Cuando** se intenta crear otra inscripción equivalente en el mismo ámbito
**Entonces** el sistema impide la duplicación.

### AC-002-08 — Categoría única

**Dado** una inscripción activa
**Cuando** se le asigna una categoría
**Entonces** queda exactamente una categoría activa para esa inscripción.

### AC-002-09 — Cambio de categoría

**Dado** una inscripción con categoría activa
**Cuando** el administrador la cambia
**Entonces** se conserva categoría anterior, nueva y fecha
**Y** las cuotas ya generadas no cambian
**Y** el efecto económico comienza el mes siguiente.

### AC-002-10 — Participación ocasional

**Dado** un jugador que participa ocasionalmente en otra categoría
**Cuando** se registra esa participación fuera de un cambio formal
**Entonces** no se crea automáticamente otra inscripción ni una cuota adicional.

### AC-002-11 — Baja por disciplina

**Dado** un jugador con dos inscripciones activas
**Cuando** el administrador inactiva una de ellas
**Entonces** la otra continúa activa
**Y** se preservan cuotas, pagos e historial de la inscripción inactivada.

### AC-002-12 — Intento de baja global

**Dado** un administrador de disciplina
**Cuando** intenta dar de baja al jugador de todas las disciplinas del club
**Entonces** el backend rechaza la operación
**Y** solo permite actuar sobre su propia disciplina.

### AC-002-13 — Delegado en modo lectura

**Dado** un delegado general
**Cuando** consulta jugadores, inscripciones y categorías de su club
**Entonces** puede visualizarlos
**Pero cuando** intenta crearlos, modificarlos o inactivarlos
**Entonces** el backend rechaza la operación.

## Trazabilidad

Cada escenario deriva de `SPEC-002`, DEC-013–018, DEC-027–029, RN-0071–0101, RN-0154–0171, CON-005 y CON-013.
