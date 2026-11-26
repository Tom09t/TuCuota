# Registro de parámetros pendientes

**Estado:** activo  
**Fecha de corte:** 2026-11-26  
**Convención aprobada:** R0-D13

## Estados

- **Abierto:** falta análisis o información suficiente.
- **En análisis:** existe trabajo verificable en curso.
- **Propuesto:** existe una alternativa recomendada pendiente de aprobación.
- **Aprobado:** existe decisión y evidencia de aprobación.
- **Bloqueado:** no puede avanzarse por una dependencia externa explícita.

## Registro

| ID | Parámetro | Estado | Propietario | Momento límite | Impacto si permanece abierto | Dependencias | Fuente | Evidencia de cierre |
|---|---|---|---|---|---|---|---|---|
| PAR-001 | Identidad nominal del responsable técnico, funcional y de seguridad | Aprobado | Tomás, titular del proyecto y usuario solicitante | Cierre de R0 | Cerrado mediante R0-D16 | Designación explícita | R0-D02; SPEC-030; SPEC-032; SPEC-034 | R0-D16 |
| PAR-002 | Suplencia o procedimiento de continuidad del responsable único | Aprobado | Tomás | Cierre de R0 | Cerrado mediante pausa segura; no existe suplente | PAR-001 | R0-D02; R0-D17 | R0-D17 y procedimiento de recuperación de cuentas antes de habilitar servicios externos |
| PAR-003 | Visibilidad y plan de GitHub compatibles con rulesets, ambientes y protecciones | En análisis | Tomás | Cierre de R0; revalidar antes de Producción o de cambiar la visibilidad | Ruleset configurado; R0 sigue abierto hasta probar un PR con `ci-gate` exitoso y fallido; un cambio a privado con Free volvería a bloquear el gate | Ejecutar prueba efectiva del ruleset `main-protection`; decisión productiva futura | R0-D04; R0-D05; R0-D18–R0-D20; R0-D26 | Ruleset activo verificado el 2026-11-26, más evidencia pendiente de PR y bloqueo efectivo |
| PAR-004 | Inventario y versiones exactas de paquetes secundarios | Aprobado | Tomás | Cierre de R0 | Cerrado; revalidación de parches previa a restauración inicial | Compatibilidad con R0-D07 y R0-D08 | ADR-0013–ADR-0015; R0-D08; R0-D21; R0-D22 | Inventario aprobado en R0-D08, R0-D21 y R0-D22 |
| PAR-005 | Revisión inicial de licencias de dependencias directas y herramientas seleccionadas | Aprobado | Tomás; revisión jurídica futura para casos condicionales | Cierre de R0 | Cerrado para entrada a implementación; no cubre el grafo transitivo futuro | PAR-004 | ADR-0017; R0-D09; R0-D23 | `docs/r0/LICENSE_REVIEW.md` |
| PAR-006 | Validación legal de residencia en Brasil y DPA de AWS | Abierto | Asesoría legal designada | Antes de contratación o datos reales | Bloquea infraestructura con datos reales | Propuesta contractual y categorías de datos | R0-D10; ADR-0016; ADR-0017 | Dictamen y DPA aprobados |
| PAR-007 | Región secundaria y residencia de copias fuera de `sa-east-1` | Bloqueado | Responsable único y asesoría legal | Antes de cerrar R7 | Bloquea recuperación ante pérdida regional | PAR-006; PAR-011 | R0-D10; SPEC-031 | Decisión aprobada y prueba de recuperación aplicable |
| PAR-008 | Equipo, disponibilidad y capacidad efectiva | Abierto | Titular del proyecto | Antes de producir estimaciones calendario | Impide fechas y compromisos de duración | Designaciones y disponibilidad real | ROADMAP-v1 | Matriz nominal de capacidad y restricciones |
| PAR-009 | Calendario, feriados, dedicación y ventanas de trabajo | Abierto | Titular del proyecto | Antes de producir estimaciones calendario | Impide fechas confiables | PAR-008 | ROADMAP-v1 | Calendario de capacidad aprobado |
| PAR-010 | Presupuesto cloud, herramientas y soporte | Abierto | Titular del proyecto | Antes de aprovisionar recursos pagos | Bloquea contratación y sizing definitivo | Escenarios de carga y cotizaciones | R0-D10; R0-D11 | Presupuesto y umbrales aprobados |
| PAR-011 | RPO y RTO adicionales | Abierto | Responsable único y titular del proyecto | Antes de cerrar R7 | Bloquea recuperación productiva; no reduce PITR ni copias obligatorias | Impacto de negocio y costos | ADR-0016; SPEC-031 | Objetivos aprobados y restore test correspondiente |
| PAR-012 | Sizing inicial y presupuesto de conexiones PostgreSQL | Abierto | Responsable único | Antes de aprovisionar R1; definitivo antes de R7 | Bloquea configuración reproducible y escalado | PAR-008; PAR-010; volumen esperado | ADR-0016; R0-D11 | Modelo de capacidad, límites y prueba de carga |
| PAR-013 | SLO, umbrales, frecuencias, deduplicación y criterios de recuperación | Abierto | Responsable único | Antes de cerrar R7 | Bloquea ACCEPTANCE-032 | Volumen, criticidad y presupuesto | SPEC-032; ACCEPTANCE-032 | Catálogo aprobado y pruebas de alertas |
| PAR-014 | Destinatarios nominales y escalamiento de alertas técnicas | Abierto | Responsable único | Antes de cerrar R7 | Bloquea ACCEPTANCE-032 | PAR-001; procedimiento de guardia | SPEC-032; ACCEPTANCE-032 | Lista controlada y prueba de entrega |
| PAR-015 | Retención de logs, métricas, trazas, objetos y evidencia | Abierto | Responsable único y asesoría legal | Antes de cerrar R7 | Bloquea configuración productiva y evidencia vigente | PAR-006; obligaciones legales | ADR-0013; SPEC-020; SPEC-026; SPEC-032 | Política aprobada y lifecycle verificado |
| PAR-016 | Conservación legal y catálogo de evidencia aceptable | Abierto | Asesoría legal designada | Antes de cerrar R7 | Bloquea eliminación definitiva y aceptación de evidencia | Categorías documentales y regulatorias | ARCHITECTURE_CLOSURE; SPEC-020; SPEC-026 | Dictamen y catálogo aprobados |
| PAR-017 | Biblioteca visual frontend | Abierto | Responsable único | Antes de cerrar R1 | Bloquea componentes definitivos, no contratos ni shell | Evaluación de accesibilidad, licencia y mantenimiento | ADR-0014; ROADMAP-v1 | Decisión aprobada y licencia revisada |
| PAR-018 | Parámetros funcionales abiertos en las SPEC | Abierto | Responsable único en función funcional | Antes de preparar el incremento que los consume | Bloquea aceptación del comportamiento correspondiente | Mecanismo normativo y evidencia de negocio | SPEC-001–SPEC-034 | Decisión normativa aprobada y trazabilidad actualizada |
| PAR-019 | Dominio, volumen, cuotas y salida del sandbox de SES | Abierto | Responsable único | Antes de cerrar R7 | Bloquea correo técnico productivo | Dominio controlado; PAR-010 | R0-D11; SPEC-032 | Dominio verificado y prueba SPF/DKIM/DMARC/entrega |
| PAR-020 | Validación técnica de Mercado Pago Split Payment 1:1 | En análisis | Tomás | Iniciar en R0; cerrar en R8 | Bloquea dinero real | Acceso sandbox y documentación vigente | ADR-0002; SPEC-017–SPEC-019; SPEC-034; R0-D25 | Plan R0 iniciado; cierre mediante informe técnico y evidencia sandbox aprobados |
| PAR-021 | Validación comercial, contractual y legal de Mercado Pago | En análisis | Tomás y asesoría legal a designar | Iniciar en R0; cerrar en R8 | Bloquea dinero real | Condiciones comerciales, contrato del proveedor y dictamen externo | ADR-0002; DEC-170; SPEC-034; R0-D25 | Plan R0 iniciado; cierre mediante aprobaciones comercial, contractual y legal |
| PAR-022 | Identidad nominal del superadministrador autorizante | Abierto | Titular del proyecto | Antes de cerrar R8 | Bloquea autorización de dinero real | Gobierno operativo del piloto | SPEC-034; ACCEPTANCE-034 | Designación y mecanismo formal de autorización |
| PAR-023 | Valores de carga para estimaciones de costo | Abierto | Titular del proyecto | Antes de estimar costo operativo definitivo | Limita estimaciones a escenarios hipotéticos | Clubes, usuarios, operaciones, storage y correo | R0-D10; R0-D11 | Hipótesis aprobadas o mediciones reales |
| PAR-024 | Revisión transitiva de licencias, imágenes, paquetes de sistema, Actions, SBOM y avisos | Abierto | Tomás; revisión jurídica cuando aplique | Antes de aceptar R1 | Bloquea aceptación y promoción de artefactos de R1 | Manifests, lockfiles, imágenes y workflows autorizados | ADR-0015; ADR-0017; R0-D09; R0-D23 | Informe reproducible, SBOM y avisos sin bloqueos abiertos |

## Historial

| Fecha | Cambio | Evidencia |
|---|---|---|
| 2026-11-26 | Creación del registro inicial PAR-001–PAR-023 | Aprobación de R0-D13 |
| 2026-11-26 | Cierre de PAR-001 con la designación nominal de Tomás | R0-D16 |
| 2026-11-26 | Cierre de PAR-002 mediante pausa segura sin suplente | R0-D17 |
| 2026-11-26 | Cierre de PAR-003: repositorio público y GitHub Free compatibles con protecciones aprobadas | R0-D18 |
| 2026-11-26 | Reapertura de PAR-003 por decisión de convertir el repositorio en privado antes de H1 | R0-D19 |
| 2026-11-26 | PAR-003 pasa a bloqueado por seleccionar GitHub Free con repositorio privado | R0-D20 |
| 2026-11-26 | Corrección de `@microsoft/signalr` y aprobación del segundo snapshot; PAR-004/PAR-005 continúan en análisis | R0-D21 |
| 2026-11-26 | Cierre de PAR-004 con el último grupo de paquetes; PAR-005 continúa en análisis | R0-D22 |
| 2026-11-26 | Licencia Apache-2.0 de dotnet-stryker 5.0.0 verificada en el repositorio oficial | Evidencia complementaria de R0-D22 |
| 2026-11-26 | Cierre de PAR-005 para la revisión inicial y creación de PAR-024 para la etapa transitiva de R1 | R0-D23 |
| 2026-11-26 | Inicio de PAR-020 y PAR-021 mediante el plan integral de validación de Mercado Pago; ambos permanecen abiertos al cierre técnico y legal de R8 | R0-D25 y `MERCADO_PAGO_VALIDATION_PLAN.md` |
| 2026-11-26 | PAR-003 deja de estar bloqueado: se aprueba mantener GitHub Free y el repositorio público durante implementación y preproducción; queda pendiente probar las protecciones | R0-D26 |
| 2026-11-26 | Se configura `main-protection` activo sobre `main`, sin bypass y con PR, squash, historial lineal, conversaciones resueltas y `ci-gate`; falta la prueba de PR | Evidencia de GitHub y R0-D26 |
