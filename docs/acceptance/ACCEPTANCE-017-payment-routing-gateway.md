# ACCEPTANCE-017 — Enrutamiento de pagos e integración receptora

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-017`

## Escenarios

### AC-017-01 — Cuotas de una disciplina

**Dado** un responsable con varios jugadores vinculados en una disciplina  
**Cuando** selecciona cuotas completas  
**Entonces** puede agruparlas en una operación conservando su detalle individual.

### AC-017-02 — Disciplinas diferentes

**Dado** cuotas de dos disciplinas  
**Cuando** se intenta agruparlas  
**Entonces** el sistema exige operaciones independientes.

### AC-017-03 — Conteo comercial

**Dado** un checkout confirmado con varias cuotas  
**Cuando** se actualiza el mínimo comercial  
**Entonces** cada cuota cancelada cuenta como un pago independiente.

### AC-017-04 — Falla aislada

**Dado** pagos independientes de dos disciplinas  
**Cuando** falla uno  
**Entonces** no cambia el resultado del otro.

### AC-017-05 — Cuenta compartida

**Dado** dos disciplinas que comparten receptor  
**Cuando** procesan y concilian pagos  
**Entonces** operaciones, costos, liquidaciones y reportes siguen separados por disciplina.

### AC-017-06 — Sin acceso cruzado

**Dado** una cuenta receptora compartida  
**Cuando** un administrador consulta su configuración  
**Entonces** no obtiene acceso administrativo a la otra disciplina.

### AC-017-07 — Alta con OTP

**Dado** un administrador autorizado y una cuenta receptora válida  
**Cuando** verifica el OTP y completa los controles del proveedor  
**Entonces** la configuración puede activarse para su disciplina.

### AC-017-08 — Validación fallida

**Dado** un reemplazo de cuenta  
**Cuando** falla OTP, titularidad o vinculación con la pasarela  
**Entonces** la nueva cuenta no se activa  
**Y** la anterior permanece si sigue operativa.

### AC-017-09 — Configuración no operativa

**Dado** una disciplina sin receptor o conexión operativa  
**Cuando** se intenta iniciar un cobro  
**Entonces** el sistema lo impide.

### AC-017-10 — Pago iniciado antes del cambio

**Dado** un pago ya iniciado  
**Cuando** se reemplaza la cuenta receptora  
**Entonces** conserva la referencia original para conciliación  
**Y** las nuevas operaciones usan la configuración validada vigente.

### AC-017-11 — Auditoría y avisos

**Dado** un cambio de cuenta completado  
**Cuando** queda activo  
**Entonces** se auditan actor, disciplina, fecha y cambio sin secretos  
**Y** se avisa al administrador y superadministrador.

### AC-017-12 — Autorización oficial

**Dado** un proveedor con OAuth disponible  
**Cuando** se vincula la disciplina  
**Entonces** se usa el mecanismo oficial sin solicitar la contraseña personal.

### AC-017-13 — Aislamiento de conexión

**Dado** disciplinas con el mismo proveedor o cuenta autorizada  
**Cuando** operan  
**Entonces** mantienen conexiones y registros lógicamente independientes.

### AC-017-14 — Desglose Split Payment

**Dado** un pago compatible y confirmado  
**Cuando** se registra su distribución  
**Entonces** se identifican cuota, comisión de plataforma del 5% y costo de pasarela  
**Y** los fondos de cuota se dirigen al destinatario sin custodia de TuCuota.

### AC-017-15 — Conexión revocada

**Dado** una conexión que deja de estar autorizada  
**Cuando** el sistema detecta su estado  
**Entonces** bloquea nuevos cobros y preserva pagos y conciliaciones históricas.

### AC-017-16 — Habilitación real condicionada

**Dado** la integración implementada  
**Cuando** aún no se completaron ADR-0002 y DEC-170  
**Entonces** no se habilitan cobros reales.

## Pendientes no aceptables todavía

No pueden cerrarse criterios cuantitativos de OTP, titularidad, OAuth, modalidad de checkout ni validación contractual final hasta contar con evidencia y aprobación.

## Trazabilidad

Cada escenario deriva de `SPEC-017`, DEC-121–DEC-125, RN-1062–RN-1133 y CON-017. ADR-0002 y DEC-170 condicionan la operación real.
