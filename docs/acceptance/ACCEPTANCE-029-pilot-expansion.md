# ACCEPTANCE-029 — Piloto progresivo y autorización de ampliaciones

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-029`

## Escenarios

### AC-029-01 — Inicio controlado

**Dado** el lanzamiento inicial  
**Cuando** se habilita el piloto  
**Entonces** participan únicamente el grupo autorizado de jugadores y responsables de Obras Futsal.

### AC-029-02 — Capacidad multiclub

**Dado** un piloto limitado a Obras Futsal  
**Cuando** opera la primera versión  
**Entonces** mantiene capacidad multiclub y aislamiento por club y disciplina sin habilitar comercialmente otros ámbitos.

### AC-029-03 — Verificación de cuotas y accesos

**Dado** una etapa activa del piloto  
**Cuando** se evalúan sus operaciones esenciales  
**Entonces** se verifican la generación de cuotas y los accesos y permisos de administradores y responsables.

### AC-029-04 — Verificación financiera

**Dado** operaciones financieras autorizadas del piloto  
**Cuando** se evalúa la etapa  
**Entonces** se comprueban pagos online, Split Payment, destino de fondos y conciliación con cuotas y pasarela.

### AC-029-05 — Duplicados e incertidumbre

**Dado** pagos procesados durante el piloto  
**Cuando** se revisan los controles financieros  
**Entonces** se evalúan la prevención de duplicados y el tratamiento seguro de pagos inciertos.

### AC-029-06 — Incidente registrado

**Dado** un incidente detectado durante el piloto  
**Cuando** se registra  
**Entonces** queda disponible para análisis y para evaluar la posible ampliación.

### AC-029-07 — Solicitud de ampliación

**Dado** un administrador de disciplina autorizado  
**Cuando** solicita avanzar de etapa  
**Entonces** se registra la solicitud y el usuario solicitante sin producir todavía la ampliación.

### AC-029-08 — Estado de verificaciones

**Dado** una solicitud de ampliación  
**Cuando** se consulta su evaluación  
**Entonces** el sistema presenta el estado de cada verificación obligatoria y su evidencia o referencia.

### AC-029-09 — Criterio incumplido

**Dado** una diferencia crítica, un error bloqueante o cualquier control obligatorio incumplido  
**Cuando** se intenta autorizar la ampliación  
**Entonces** el sistema la mantiene bloqueada.

### AC-029-10 — Omisión prohibida

**Dado** un administrador de disciplina  
**Cuando** intenta omitir una verificación obligatoria  
**Entonces** el backend rechaza la acción.

### AC-029-11 — Autoridad insuficiente

**Dado** un usuario que no es superadministrador  
**Cuando** intenta aprobar el avance entre etapas  
**Entonces** el backend deniega la autorización.

### AC-029-12 — Ampliación aprobada

**Dado** todas las verificaciones obligatorias satisfechas  
**Cuando** el superadministrador aprueba la solicitud  
**Entonces** registra fecha, motivo, resultado y decisor, y habilita el alcance de la etapa autorizada.

### AC-029-13 — Ampliación rechazada

**Dado** una solicitud evaluada  
**Cuando** el superadministrador la rechaza  
**Entonces** se conserva fecha, motivo, resultado, solicitante y decisor para auditoría.

### AC-029-14 — Expansión ordenada

**Dado** una etapa satisfactoriamente validada  
**Cuando** se autoriza continuar  
**Entonces** el orden posible es resto de Obras Futsal, otras disciplinas de Obras Mendoza y luego otros clubes.

### AC-029-15 — Integridad de registros anteriores

**Dado** una ampliación aprobada  
**Cuando** se incorporan nuevos participantes  
**Entonces** no se modifican pagos ni registros financieros anteriores.

### AC-029-16 — Dinero real todavía bloqueado

**Dado** una etapa del piloto autorizada  
**Cuando** no se cumplen integralmente los criterios aplicables de DEC-170  
**Entonces** la autorización de ampliación no habilita cobros reales.

## Pendientes no aceptables todavía

No pueden cerrarse participantes iniciales, formulario, evidencias, umbrales, períodos de observación, severidades ni cronograma sin definiciones aprobadas.

## Trazabilidad

Cada escenario deriva de `SPEC-029`, DEC-162–DEC-163 y RN-1732–RN-1767. La habilitación financiera continúa condicionada por DEC-170.

