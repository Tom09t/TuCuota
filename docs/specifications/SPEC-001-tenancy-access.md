# SPEC-001 — Multiclub, disciplinas y acceso administrativo

**Estado:** aprobada el 2026-11-25
**Baseline:** `baseline-v1`

## Objetivo

Definir el aislamiento multiclub, el ciclo de vida de clubes y disciplinas, las cuentas administrativas y los límites efectivos de cada rol.

## Alcance

Incluye clubes, slugs, personalización institucional, disciplinas, superadministrador, delegado general y administrador de disciplina. No especifica todavía jugadores, cuotas ni pagos, salvo los límites de autorización y los efectos del estado activo/inactivo.

## Reglas funcionales

### Clubes y aislamiento

- Cada club tiene identificador y slug únicos, portal propio, configuración y datos aislados.
- Solo el superadministrador crea, inactiva y reactiva clubes.
- La baja es lógica y conserva administradores, jugadores, cuotas, pagos e historial.
- Un club inactivo no genera cuotas nuevas, pero admite pagos de cuotas preexistentes.
- La reactivación no genera cuotas por períodos inactivos; registra fecha y retoma la generación prospectiva.

### Identidad visual

- Cada club puede configurar nombre visible, logotipo, colores y portada, con valores predeterminados cuando falten.
- Solo el superadministrador y el delegado general del propio club pueden modificarla.
- Los cambios se reflejan en portal y panel y conservan actor y fecha.

### Disciplinas

- Un club puede incorporar disciplinas dinámicamente sin alterar datos de las existentes.
- Superadministrador y delegado general pueden crear disciplinas; solo el superadministrador puede desactivarlas o reactivarlas.
- La desactivación es lógica, no afecta otras disciplinas y conserva datos históricos.
- Una disciplina inactiva no recibe inscripciones ni cuotas nuevas, pero permite pagar deudas preexistentes.
- La reactivación ordinaria no genera cuotas retroactivas.

### Cuentas y autenticación administrativa

- Cada cuenta administrativa pertenece a un único club; una persona que administra varios clubes utiliza cuentas independientes.
- El correo es único dentro de cada club y puede repetirse en clubes diferentes sin compartir estado, permisos o notificaciones.
- Las cuentas se autentican con correo y contraseña; las contraseñas se almacenan con hash seguro.
- La recuperación usa un enlace temporal de un solo uso y afecta únicamente a la cuenta correspondiente.
- Toda operación privada valida sesión, club, disciplina, rol y estado vigente en el backend.

### Roles efectivos

| Rol | Facultades |
|---|---|
| Superadministrador | Acceso global; crea y administra clubes; crea el primer delegado y primer administrador de disciplina; administra excepciones globales; restaura administración cuando no queda un administrador activo. |
| Delegado general | Administra datos e identidad visual de su club, disciplinas y cuentas administrativas permitidas. Solo consulta jugadores, categorías, cuotas, pagos, devoluciones y costos. |
| Administrador de disciplina | Administra cuentas y operaciones funcionales únicamente dentro de su disciplina. No accede a otras disciplinas ni clubes. |

### Protecciones de continuidad administrativa

- Un club puede tener múltiples delegados y cada disciplina múltiples administradores.
- No se puede desactivar al último administrador activo de una disciplina mediante el flujo ordinario.
- El superadministrador puede hacerlo excepcionalmente con justificación y auditoría, y es el único que restaura administración si no queda ninguno activo.
- Los delegados no pueden desactivar al último delegado activo; el superadministrador puede hacerlo excepcionalmente con justificación y es el único autorizado para restaurarlo.
- Las bajas son lógicas y conservan el historial de acciones.

## Invariantes

- Ninguna autorización del frontend reemplaza la validación del backend.
- Un cambio de rol o baja revoca el acceso correspondiente sin eliminar historial.
- Ningún rol de club obtiene acceso a otro club por compartir correo o persona.
- El delegado no adquiere facultades financieras ni de edición de jugadores por administrar cuentas o disciplinas.

## Trazabilidad

- DEC-001–DEC-012, DEC-019–DEC-026.
- RN-0001–RN-0070, RN-0102–RN-0153.
- CON-004–CON-008, CON-010–CON-014.
- `MATERIAL_REVIEW_01.md`, `MATERIAL_REVIEW_03.md`.

