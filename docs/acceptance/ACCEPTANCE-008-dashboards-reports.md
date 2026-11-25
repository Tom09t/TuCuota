# ACCEPTANCE-008 — Dashboards y reportes

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-008`

## Escenarios

### AC-008-01 — Dashboard global

**Dado** un superadministrador  
**Cuando** abre el dashboard global  
**Entonces** visualiza métricas consolidadas de plataforma, contratos, pagos, ingresos, penalizaciones y mínimos.

### AC-008-02 — Filtros globales

**Dado** el dashboard global  
**Cuando** se filtra por club, disciplina y período  
**Entonces** todos los indicadores y gráficos reflejan la selección.

### AC-008-03 — Dashboard disciplinario

**Dado** un administrador de disciplina  
**Cuando** abre su dashboard  
**Entonces** visualiza jugadores, cuotas, pagos, deuda y recaudación exclusivamente de su disciplina.

### AC-008-04 — Filtros disciplinarios

**Dado** el dashboard de disciplina  
**Cuando** se filtra por período y categoría  
**Entonces** sus indicadores y evolución se actualizan dentro del mismo ámbito.

### AC-008-05 — Delegado consolidado

**Dado** un delegado general  
**Cuando** consulta su dashboard  
**Entonces** visualiza el consolidado del club y el detalle de sus disciplinas  
**Y** no puede ejecutar operaciones financieras.

### AC-008-06 — Acceso fuera de ámbito

**Dado** un usuario de club o administrador  
**Cuando** intenta consultar otro club o disciplina no autorizada  
**Entonces** el backend rechaza la solicitud.

### AC-008-07 — Métricas reservadas

**Dado** un usuario que no es superadministrador  
**Cuando** intenta consultar costos o indicadores comerciales globales de plataforma  
**Entonces** el backend no expone esa información.

### AC-008-08 — Desglose por origen

**Dado** cobros online y pagos externos  
**Cuando** se consulta el reporte financiero  
**Entonces** aparecen diferenciados junto con cuota nominal, comisión, costo disponible y neto.

### AC-008-09 — Total del jugador

**Dado** una operación con componentes adicionales al importe deportivo  
**Cuando** se consulta su detalle  
**Entonces** el total pagado por el jugador se distingue del importe de cuota.

### AC-008-10 — Operación única

**Dado** una operación incluida en vistas de detalle y consolidado  
**Cuando** se calculan totales  
**Entonces** se contabiliza una sola vez.

### AC-008-11 — Exportación global

**Dado** un superadministrador y filtros seleccionados  
**Cuando** exporta a Excel o PDF  
**Entonces** el reporte respeta filtros, identifica período y fecha e incluye el desglose comercial.

### AC-008-12 — Penalización ajustada

**Dado** una penalización ajustada o condonada  
**Cuando** aparece en una exportación  
**Entonces** se diferencian su importe original y final.

### AC-008-13 — Exportación disciplinaria

**Dado** un administrador de disciplina  
**Cuando** exporta a Excel o PDF  
**Entonces** el archivo contiene únicamente información de su disciplina autorizada.

## Pendientes no aceptables todavía

No pueden cerrarse criterios sobre exportación del delegado, frecuencia de actualización ni representación de costos no disponibles hasta contar con definición aprobada.

## Trazabilidad

Cada escenario deriva de `SPEC-008`, DEC-078–DEC-081, RN-0543–RN-0582, CON-005 y CON-009.
