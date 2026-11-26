# Plan integral de validación de Mercado Pago

**Estado:** iniciado mediante R0-D25  
**Fecha:** 2026-11-26  
**Alcance:** preparación y evidencia; no autoriza implementación, contratación, despliegue ni dinero real

## Objetivo

Validar de forma técnica, comercial, contractual, legal, operativa y de seguridad que Mercado Pago Marketplace Split Payments 1:1 puede satisfacer ADR-0002 y el comportamiento aprobado sin alterar silenciosamente el modelo de negocio.

Este plan inicia PAR-020 y PAR-021 durante R0. Su ejecución y cierre pertenecen a los incrementos y gates indicados en el roadmap, con cierre obligatorio antes de habilitar dinero real en R8 y satisfacer DEC-170.

## Decisión que no se toma todavía

Mercado Pago permanece seleccionado por ADR-0002, pero este plan no elige todavía Checkout Pro, Checkout API ni Bricks. La modalidad se decidirá después de obtener evidencia comparativa en sandbox. Si la selección cambia límites arquitectónicos o responsabilidades de seguridad, se registrará mediante un ADR; si solo concreta una opción ya prevista por ADR-0002, se documentará como decisión técnica trazable.

## Responsables

| Función | Responsable | Límite |
|---|---|---|
| Técnica, funcional, seguridad y coordinación comercial | Tomás | Puede preparar y aprobar evidencia interna, pero no sustituye dictámenes externos obligatorios |
| Revisión contractual y legal | Asesoría legal a designar | Requerida para cerrar PAR-021 |
| Autorización de dinero real | Superadministrador a designar | Separada de R0-D25 y sujeta a SPEC/ACCEPTANCE-034 y DEC-170 |

La indisponibilidad del responsable único activa la pausa segura definida en R0-D17. No se presume suplente.

## Registro de evidencia

Cada comprobación utilizará un identificador `MPV-###` y registrará:

- dimensión y pregunta verificable;
- fuente normativa y referencias SPEC, AC, ADR, DEC, RN o CON aplicables;
- método, ambiente y datos utilizados;
- evidencia obtenida, fecha, responsable, hash y ubicación controlada;
- resultado, restricciones, bloqueos y vigencia o fecha de revalidación.

Estados permitidos: `Pendiente`, `En análisis`, `Evidencia obtenida`, `Aprobado`, `Rechazado` y `Bloqueado`. Ningún resultado se considera aprobado solo por existir documentación del proveedor.

## Carril 1 — Elegibilidad comercial y contractual

| ID | Comprobación | Evidencia mínima | Trazabilidad |
|---|---|---|---|
| MPV-001 | Disponibilidad de Marketplace Split Payments 1:1 para Argentina, la plataforma y las cuentas receptoras previstas | Confirmación contractual o comercial vigente y condiciones de elegibilidad | ADR-0002; AC-017-16 |
| MPV-002 | Requisitos de alta, KYC, titularidad y vinculación para clubes o disciplinas | Catálogo de documentos, roles, rechazos y revalidaciones | SPEC-017; AC-017-07–08 |
| MPV-003 | Estructura y orden de costos, comisión de plataforma del 5%, costos del proveedor, liquidación y reservas | Ejemplos contractuales y resultados sandbox conciliables | ADR-0002; AC-017-14 |
| MPV-004 | Límites, cuotas, suspensión, soporte, cambios contractuales y salida del servicio | Términos vigentes, canales, plazos y responsables | SPEC-018–SPEC-019 |
| MPV-005 | Condiciones de reembolsos, contracargos y saldo insuficiente del receptor | Reglas contractuales y escenarios aceptados | ADR-0002; SPEC-019 |

## Carril 2 — Validación técnica en sandbox

| ID | Comprobación | Evidencia mínima | Trazabilidad |
|---|---|---|---|
| MPV-010 | Comparación Checkout Pro, Checkout API y Bricks | Matriz de capacidades, seguridad, UX, operación y restricciones con prueba reproducible | ADR-0002; SPEC-017 |
| MPV-011 | OAuth por vendedor: autorización, scopes, renovación, expiración y revocación | Secuencias y resultados sin exponer tokens | AC-017-12–15 |
| MPV-012 | Aislamiento lógico por disciplina y cuenta receptora compartida | Pruebas de separación de operaciones, permisos y conciliación | AC-017-01–06, AC-017-13 |
| MPV-013 | Split Payment y comisión del 5% sin custodia de TuCuota | Operaciones sandbox con desglose y referencias conciliables | AC-017-14 |
| MPV-014 | Idempotencia, duplicados, reintentos y concurrencia | Casos repetidos y concurrentes sin doble imputación | SPEC-017–SPEC-019 |
| MPV-015 | Webhooks: autenticidad, duplicados, desorden, demora y recuperación por consulta | Evidencia de verificación y convergencia idempotente | AC-018-05; AC-019-06–10, AC-019-16 |
| MPV-016 | Vigencia, cancelación y estados inciertos de checkout | Tabla de estados reales, consultas y protección de cuotas | SPEC-019; AC-019-04–11 |
| MPV-017 | Reembolsos totales y por cuotas completas | Resultados sandbox, incluida falta de saldo cuando pueda simularse | ADR-0002 |
| MPV-018 | Contracargos, discrepancias y conciliación | Identificadores, eventos y tratamiento reproducible | ADR-0002; AC-019-16–17 |
| MPV-019 | Disponibilidad, límites de API y monitoreo sin cobros de prueba | Clasificación de errores y señales permitidas | SPEC-018; AC-018-05–10 |

Las pruebas usarán únicamente credenciales y fondos de prueba autorizados. Quedan prohibidos cargos reales, datos financieros reales y cualquier mecanismo que pueda liquidar dinero.

## Carril 3 — Revisión legal y de cumplimiento

| ID | Comprobación | Evidencia mínima | Trazabilidad |
|---|---|---|---|
| MPV-020 | Flujo directo de fondos y ausencia de custodia por TuCuota | Dictamen contractual y diagrama aprobado | ADR-0002; SPEC-017; AC-017-14 |
| MPV-021 | Roles, protección de datos, DPA, residencia, subencargados y transferencias | Dictamen y documentos contractuales vigentes | SPEC-030–SPEC-031; ADR-0016–ADR-0017 |
| MPV-022 | KYC/AML, consumidor, reembolsos, contracargos, responsabilidad y suspensión | Dictamen con obligaciones y responsables | ADR-0002; SPEC-017–SPEC-019 |
| MPV-023 | Alcance PCI y tratamiento de datos de pago para cada modalidad de checkout | Evaluación por modalidad y controles compensatorios | ADR-0002; SPEC-030; SPEC-034 |
| MPV-024 | Conservación de registros, evidencia y obligaciones fiscales/comerciales | Política y dictamen aprobados | SPEC-020; SPEC-026; SPEC-034 |

## Carril 4 — Operación y seguridad

| ID | Comprobación | Evidencia mínima | Trazabilidad |
|---|---|---|---|
| MPV-030 | Almacenamiento, cifrado, acceso y rotación de secretos y tokens | Diseño y prueba sin secretos en repositorio, frontend, logs o evidencia | ADR-0017; AC-017-12, AC-018-05 |
| MPV-031 | Bloqueo y recuperación ante revocación, configuración inválida o indisponibilidad | Runbook y pruebas por disciplina y globales | SPEC/ACCEPTANCE-018 |
| MPV-032 | Auditoría y correlación sin datos sensibles | Muestras sanitizadas y trazabilidad extremo a extremo | SPEC-017–SPEC-020 |
| MPV-033 | Incidentes, conciliación y resolución excepcional | Runbook con permisos, evidencia y escalamiento | SPEC/ACCEPTANCE-019 |
| MPV-034 | Datos de sandbox, cuentas de prueba y limpieza | Inventario, accesos, retención y eliminación | ADR-0017; SPEC-030 |

## Criterios de comparación de checkout

La matriz MPV-010 ponderará, como mínimo:

1. compatibilidad demostrada con Split Payments 1:1 y OAuth por vendedor;
2. capacidad de aplicar y conciliar la comisión de plataforma aprobada;
3. superficie PCI, exposición de datos y gestión de secretos;
4. aislamiento por disciplina y protección contra doble cobro;
5. estados, webhooks, consultas, reembolsos y contracargos disponibles;
6. experiencia accesible, control de UX y complejidad operativa;
7. límites, observabilidad, soporte, costos y dependencia del proveedor.

Una modalidad queda descartada si falla una condición normativa obligatoria, aunque resulte más simple o económica.

## Gates y momentos de cierre

| Momento | Exigencia |
|---|---|
| Cierre de R0 | Plan aprobado, responsables y preguntas registrados; PAR-020/PAR-021 iniciados, no necesariamente cerrados |
| Antes de implementar el adaptador de pagos | Evidencia suficiente para elegir modalidad; decisión trazable y ADR si corresponde |
| Cierre del incremento técnico de pagos | Escenarios sandbox aplicables de SPEC/ACCEPTANCE-017–019 aprobados, sin dinero real |
| Cierre de R8 / DEC-170 | PAR-020 y PAR-021 cerrados; dictámenes y evidencia vigentes; superadministrador designado; todos los gates de SPEC/ACCEPTANCE-034 satisfechos |

La superación de un gate anterior no implica la del siguiente.

## Resultados posibles

- **Compatible:** satisface todas las condiciones obligatorias con evidencia vigente.
- **Compatible con restricciones:** requiere límites explícitos, controles y aceptación formal de riesgos.
- **Incompatible:** bloquea dinero real y exige revisar proveedor, modalidad o modelo mediante una nueva decisión aprobada.

No se modificará la comisión, el flujo de fondos, la semántica de cuotas ni la gestión de estados inciertos para acomodar una limitación del proveedor sin aprobación normativa y arquitectónica explícita.

## Riesgos y dependencias

- Cambios en productos, términos, APIs o disponibilidad regional del proveedor.
- Acceso tardío a cuentas sandbox o comerciales elegibles.
- Diferencias entre sandbox y comportamiento contractual o productivo.
- Falta de asesoría legal designada para cerrar PAR-021.
- Evidencia insuficiente para saldos, contracargos o eventos tardíos.
- Dependencia de PAR-018 para parámetros funcionales aún abiertos y de PAR-022 para la autorización final.

Cada evidencia dependiente del proveedor tendrá fecha de revalidación. Una modificación material reabre el ítem afectado.

## Fuentes y trazabilidad

- ADR-0002.
- SPEC-017–SPEC-019 y ACCEPTANCE-017–ACCEPTANCE-019.
- SPEC-030, SPEC-031 y SPEC-034; ACCEPTANCE-030, ACCEPTANCE-031 y ACCEPTANCE-034.
- DEC-170 y sus RN vigentes, bajo la precedencia de CON-011.
- R0-D12, R0-D13, R0-D17 y R0-D25.
- Documentación oficial enlazada por ADR-0002, que deberá revalidarse al ejecutar cada prueba.
