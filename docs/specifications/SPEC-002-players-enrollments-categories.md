# SPEC-002 — Jugadores, inscripciones y categorías

**Estado:** aprobada el 2026-11-25
**Baseline:** `baseline-v1`

## Objetivo

Definir la identidad del jugador, sus fichas por club, las inscripciones independientes por disciplina, las categorías y sus bajas lógicas.

## Alcance

Incluye alta individual e importación, datos personales, identidad global por DNI, ficha por club, inscripción, categoría, cambio de categoría e inactivación por disciplina. No especifica generación de cuotas, beneficios ni pagos.

## Modelo funcional

### Identidad global y ficha por club

- El DNI identifica un único jugador global.
- Un jugador puede vincularse con varios clubes, sin duplicar su vinculación dentro del mismo club.
- Cada club mantiene una ficha independiente con nombre, apellido, fecha de nacimiento, teléfono, correo y tutor.
- Cambiar una ficha en un club no altera las fichas de otros clubes.
- DNI, nombre y apellido son obligatorios. Fecha de nacimiento, teléfono, correo y tutor son opcionales.

### Alta e importación

- El administrador de disciplina puede registrar jugadores individualmente o importarlos desde Excel dentro de su ámbito.
- Una operación no puede registrar jugadores en otro club o disciplina.
- La ausencia de datos opcionales no impide el alta.
- La categoría se asigna a la inscripción de la disciplina, no a la identidad global.

### Inscripciones

- Un jugador puede tener inscripciones en varias disciplinas del mismo club y en distintos clubes.
- Cada inscripción pertenece a un único club y disciplina y conserva estado, categoría, cuotas e historial independientes.
- Una inscripción no puede quedar duplicada para el mismo jugador, club y disciplina.
- Inactivar una inscripción no afecta las demás inscripciones ni las vinculaciones con otros clubes.

### Categorías

- Cada categoría pertenece a una disciplina específica.
- El administrador de disciplina puede crear, modificar y desactivar categorías únicamente en su ámbito.
- El delegado general puede consultarlas, sin modificarlas; el superadministrador mantiene alcance global.
- Cada inscripción tiene una única categoría activa.
- Un cambio conserva categoría anterior, nueva, fecha e historial.
- La nueva categoría produce efectos económicos desde el mes siguiente y no modifica cuotas ya generadas.
- Una participación ocasional en otra categoría no crea otra inscripción ni una cuota adicional.

### Bajas lógicas

- La baja se realiza exclusivamente por inscripción y disciplina.
- No existe una operación que dé de baja simultáneamente al jugador de todas las disciplinas del club.
- La baja conserva cuotas, pagos e historial y no afecta otras disciplinas o clubes.

## Autorización

- El administrador de disciplina crea y modifica jugadores, inscripciones y categorías solo en su disciplina.
- El delegado general tiene acceso consolidado de solo lectura.
- El superadministrador conserva alcance global.
- El backend valida club, disciplina y permisos en cada operación.

## Invariantes

- DNI global único no implica compartir fichas, cuotas o pagos entre clubes.
- La categoría nunca pertenece a la identidad global del jugador.
- No se elimina físicamente historial deportivo o financiero por una baja.
- Ningún administrador obtiene acceso a otro ámbito por compartir un jugador.

## Trazabilidad

- DEC-013–DEC-018, DEC-027–DEC-029.
- RN-0071–RN-0101, RN-0154–RN-0171.
- CON-005, CON-013.
- `MATERIAL_REVIEW_03.md`.

