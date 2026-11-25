# ACCEPTANCE-023 — Componentes económicos de devoluciones

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-023`

## Escenarios

### AC-023-01 — Comisión por cuota

**Dado** una cuota completamente devuelta  
**Cuando** se calcula la comisión a reintegrar  
**Entonces** corresponde al 5% de su importe nominal.

### AC-023-02 — Subconjunto del checkout

**Dado** un checkout con varias cuotas  
**Cuando** se devuelven solo algunas cuotas completas  
**Entonces** se consideran únicamente sus comisiones.

### AC-023-03 — Checkout completo

**Dado** una devolución total  
**Cuando** se calculan sus componentes  
**Entonces** se consideran todas las comisiones de plataforma del checkout.

### AC-023-04 — Comisión no automática

**Dado** una devolución del importe nominal  
**Cuando** la pasarela no confirma la reversión de comisión  
**Entonces** esta no se presume devuelta.

### AC-023-05 — Comisión duplicada

**Dado** una comisión ya reintegrada  
**Cuando** otro evento o solicitud intenta devolverla  
**Entonces** el sistema impide la duplicación.

### AC-023-06 — Limitación del proveedor

**Dado** una pasarela que no soporta la reversión correcta  
**Cuando** se procesa la solicitud  
**Entonces** se registra la limitación y la devolución permanece incompleta hasta una resolución controlada.

### AC-023-07 — Costo recuperable

**Dado** un costo de procesamiento que el proveedor permite reintegrar  
**Cuando** se confirma la devolución  
**Entonces** se intenta su reintegro conforme al contrato.

### AC-023-08 — Costo no recuperable

**Dado** un costo que el proveedor no devuelve  
**Cuando** se determina el resultado  
**Entonces** TuCuota no lo cubre automáticamente  
**Y** se aplica la política comunicada al pagador.

### AC-023-09 — Desglose previo

**Dado** una devolución lista para confirmar  
**Cuando** se presenta al usuario autorizado  
**Entonces** distingue nominal, comisión del 5% y costos recuperables o no recuperables conocidos.

### AC-023-10 — Importe no confiable

**Dado** componentes que no pueden estimarse confiablemente  
**Cuando** se intenta la confirmación automática  
**Entonces** el sistema la bloquea.

### AC-023-11 — Excepción controlada

**Dado** una devolución bloqueada por incertidumbre  
**Cuando** un usuario con permisos suficientes autoriza una excepción con justificación  
**Entonces** se auditan importes, actor, decisión y resultado.

### AC-023-12 — Diferencia conciliable

**Dado** un importe efectivo diferente del estimado  
**Cuando** llega la confirmación del proveedor  
**Entonces** se registra la diferencia en conciliación y se actualizan los reportes por componente.

## Pendientes no aceptables todavía

No pueden cerrarse criterios dependientes del contrato de Mercado Pago, autorización excepcional ni política de costos hasta contar con definición aprobada.

## Trazabilidad

Cada escenario deriva de `SPEC-023`, DEC-149–DEC-150 y RN-1506–RN-1538.
