# Revisión material 06 — DEC-091 a DEC-120

**Estado:** aprobada por el propietario el 2026-11-24.  
**Cobertura:** RN-0673–RN-1061, considerando CON-005, CON-009 y CON-011.

## Resultado por bloque

| DEC | RN | Resultado propuesto |
|---|---|---|
| DEC-091–096 | RN-0673–0739 | Vigentes. Reintentos, alertas, archivo, consulta y exportación de conciliaciones conservan trazabilidad y permisos por ámbito. |
| DEC-097–102 | RN-0740–0813 | Vigentes. Exportaciones asíncronas, retención, descargas temporales, almacenamiento y recuperación revalidan permisos y aíslan club/disciplina. |
| DEC-103–108 | RN-0814–0888 | Vigentes. Las notificaciones de exportación son internas; categorías, preferencias y retención no habilitan correo ni amplían permisos. |
| DEC-109–116 | RN-0889–0999 | Vigentes. Eventos, incidentes, tiempo real y sincronización respetan destinatario y ámbito; perder permisos revoca acceso inmediato e histórico. |
| DEC-117 | RN-1000–1016 | Vigentes con alcance aclarado: la vinculación inicial debe ser gestionada por administrador de disciplina o superadministrador; el delegado no adquiere edición sobre jugadores por la expresión genérica “personal autorizado”. |
| DEC-118–119 | RN-1017–1046 | Vigentes con el mismo alcance: “personal autorizado” y “administradores autorizados” no incluyen al delegado general para crear, modificar o revocar vinculaciones. |
| DEC-120 | RN-1047–1061 | Vigentes excepto RN-1048, que queda histórica desplazada por CON-005. Administradores de disciplina gestionan su ámbito, superadministrador conserva alcance global y responsables solo consultan. |

## Hallazgos materiales

- RN-1048 concede al delegado gestión de vinculaciones responsable–jugador; CON-005, de mayor precedencia, limita al delegado a solo lectura sobre jugadores. La gestión de una vinculación modifica el acceso a jugadores y no es una mera consulta.
- RN-1014, RN-1025 y RN-1033 se mantienen con alcance aclarado para evitar que expresiones genéricas reintroduzcan la facultad desplazada.
- Las notificaciones internas de DEC-103–116 no contradicen la ausencia de correo aprobada para estos eventos.
- Compartir correo entre cuentas administrativas no comparte notificaciones ni sesiones; DEC-114 conserva separación por cuenta.
- La autenticación OTP del portal es independiente de las cuentas administrativas y no sustituye la autorización por vínculo vigente.

## Resolución aprobada

Estas clasificaciones quedan aprobadas como capa de vigencia para DEC-091–120 y RN-1048 se preserva únicamente como historial.
