# SPEC-018 — Disponibilidad y monitoreo de pasarelas

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir detección, bloqueo y recuperación de conexiones por disciplina, junto con el catálogo y los bloqueos globales de proveedores.

## Interrupción por disciplina

- Cada disciplina mantiene estado independiente. Una conexión no operativa bloquea solo sus nuevos pagos online y notifica a sus administradores y al superadministrador.
- Las cuotas siguen consultables y pueden registrarse pagos externos, que no cuentan como pagos online para mínimos.
- Operaciones anteriores conservan estado y referencia y continúan conciliándose cuando sea posible.
- Al validarse nuevamente la conexión, se habilitan nuevos cobros sin duplicar ni alterar pagos confirmados.
- Interrupción, causa conocida, recuperación e incidente quedan auditados y agrupados sin avisos duplicados.

## Monitoreo y recuperación automática

- Cada conexión se monitorea mediante verificaciones periódicas, eventos oficiales y errores reales, sin cobros de prueba.
- Se distinguen fallas de autorización, configuración y disponibilidad temporal.
- Autorización revocada o configuración inválida confirmada bloquean; un error temporal aislado no produce desconexión permanente.
- Si una falla temporal impide cobrar de forma segura, se suspenden nuevos intentos de esa disciplina.
- Una conexión bloqueada se reactiva automáticamente solo tras comprobar que está operativa y autorizada.
- Recuperar no reintenta cobros con riesgo de duplicación, no modifica pagos anteriores y respeta otros bloqueos comerciales o administrativos.

## Configuración de monitoreo

- Solo el superadministrador configura frecuencias por proveedor; son globales para sus disciplinas y respetan límites de API.
- Cada proveedor puede tener parámetros distintos y límites técnicos impiden configuraciones incompatibles.
- Los administradores consultan el estado de su conexión sin modificar parámetros globales.
- Los cambios rigen verificaciones futuras y conservan valor anterior, nuevo, fecha y responsable.

## Catálogo de proveedores

- Solo el superadministrador habilita proveedores con integración implementada y Split Payment compatible.
- Cada disciplina selecciona un proveedor habilitado y vincula su propia cuenta; habilitarlo no conecta cuentas automáticamente.
- Deshabilitar el catálogo impide nuevas vinculaciones y conserva toda la historia.

## Deshabilitación global

- La modalidad comercial impide nuevas vinculaciones, pero permite continuar a conexiones existentes válidas.
- La modalidad operativa bloquea inmediatamente nuevos pagos de todas las disciplinas del proveedor, sin afectar otras pasarelas.
- Operaciones previas e historial permanecen y se siguen conciliando; las cuotas admiten consulta y pagos externos.
- La recuperación automática de una conexión nunca levanta un bloqueo operativo global.
- Cada cambio registra modalidad, motivo, actor, fecha y hora.

## Reactivación global

- Solo el superadministrador la inicia explícitamente.
- Luego se valida cada conexión por separado: las operativas reanudan cobros; las fallidas siguen bloqueadas y monitoreadas.
- No requiere acción de cada administrador ni genera cobros por sí misma.
- Respeta suspensiones y desactivaciones vigentes y audita la acción y cada validación.

## Invariantes

- Falla particular y bloqueo global son estados distintos.
- Monitorear nunca realiza movimientos financieros reales.
- La recuperación de una conexión no afecta otras ni reintenta cobros inciertos.
- Un bloqueo global solo se levanta por acción explícita del superadministrador.
- Ningún cambio elimina operaciones históricas.

## Pendientes de definición

- Estados nominales completos de conexión y sus transiciones.
- Frecuencias, recurrencia y umbrales de fallas temporales.
- Proveedores iniciales del catálogo y evidencia de compatibilidad Split Payment.

Estos puntos requieren aprobación técnica y funcional.

## Trazabilidad

- DEC-126–DEC-132.
- RN-1134–RN-1237.
- ADR-0002 y `MATERIAL_REVIEW_07.md`.
