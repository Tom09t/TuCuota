# SPEC-016 — Acceso del responsable y vinculación con jugadores

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir la autenticación temporal del portal y la relación autorizada entre responsables de pago y jugadores.

## Identificación y OTP

- El acceso puede iniciarse con el DNI del jugador, pero el DNI por sí solo no revela ni habilita información.
- Se requiere un OTP de uso único y vigencia limitada enviado a un correo o teléfono previamente registrado y verificado.
- Se limitan intentos incorrectos y frecuencia de solicitud o reenvío, sin revelar innecesariamente si el DNI existe.
- La verificación habilita una sesión temporal y segura para consultar y pagar únicamente cuotas autorizadas.
- Jugadores sin contacto verificado requieren una vinculación inicial segura gestionada por un administrador de disciplina autorizado o el superadministrador.
- Se auditan verificaciones, fallos y vínculos sin almacenar OTP en texto plano.
- El portal es independiente de las cuentas administrativas.

## Relación responsable–jugador

- La relación es muchos a muchos: cada parte conserva identidad propia.
- Un responsable autenticado ve una vista unificada de jugadores con vínculo vigente; cada cuota mantiene jugador, club y disciplina originales.
- No se presume vínculo por apellido, domicilio o contacto coincidente.
- Crear un vínculo lo activa al confirmarlo el administrador, sin aprobación o prueba de parentesco adicional; esto no reemplaza el OTP.
- No se permiten vínculos activos duplicados para responsable, jugador y ámbito.

## Gestión y permisos

- El administrador de disciplina crea, modifica y revoca vínculos solo para jugadores de su disciplina.
- El superadministrador gestiona cualquier ámbito.
- El delegado general conserva solo lectura; RN-1048 queda histórica desplazada por CON-005.
- El responsable consulta sus vínculos, pero no agrega jugadores, modifica ni revoca por cuenta propia.
- Toda operación revalida permisos y aislamiento de club y disciplina.

## Revocación

- Revocar tiene efecto inmediato: impide nuevas consultas y pagos basados en ese vínculo y se refleja en sesiones activas.
- No elimina jugador o responsable ni anula pagos, cuotas o comprobantes históricos.
- Altas, modificaciones y revocaciones conservan responsable, fecha, hora y valores anterior y nuevo.
- El acceso efectivo siempre requiere simultáneamente vínculo vigente y sesión OTP autenticada.

## Invariantes

- Conocer un DNI no concede acceso ni confirma públicamente su existencia.
- Vínculo vigente no sustituye autenticación; autenticación no sustituye vínculo.
- El delegado no gestiona vínculos.
- Una revocación no reescribe historia financiera.
- Ningún responsable obtiene acceso por similitud de datos personales.

## Pendientes de definición

- Vigencia del OTP y de la sesión, límites de intentos y reenvíos.
- Procedimiento seguro para registrar o verificar el primer contacto.
- Canales habilitados inicialmente y reglas ante contactos múltiples.

Estos puntos requieren aprobación funcional y de seguridad.

## Trazabilidad

- DEC-117–DEC-120.
- RN-1000–RN-1061, con RN-1048 conservada solo como histórica.
- CON-005 y CON-011.
- `MATERIAL_REVIEW_06.md`.
