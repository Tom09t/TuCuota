# SPEC-023 — Componentes económicos de devoluciones

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir el reintegro de la comisión de plataforma y el tratamiento de costos de pasarela en una devolución.

## Comisión de plataforma

- La política comercial es devolver al pagador la comisión asociada con cada cuota efectivamente devuelta.
- La comisión corresponde al 5% del importe nominal de cada cuota.
- Si se devuelve un subconjunto de cuotas completas, solo se revierte su comisión; si se devuelve todo el checkout, se consideran todas sus comisiones.
- Antes de ejecutar se verifica que el proveedor permita revertir correctamente el componente del Split Payment.
- Devolver el nominal no implica que la comisión quede automáticamente revertida.
- Una comisión nunca puede devolverse dos veces.

## Limitaciones del proveedor

- No se ejecuta automáticamente una devolución que produzca importes incorrectos o inconsistencias entre las partes.
- Si el proveedor no soporta la reversión necesaria, se registra la limitación y se deriva a resolución operativa controlada.
- Una devolución no queda completamente resuelta mientras existan componentes obligatorios pendientes.
- Toda devolución y excepción conserva trazabilidad entre checkout, cuotas, importes, responsables y resultados.

## Costos de procesamiento

- Se intenta reintegrar el costo de pasarela cuando el proveedor y su contrato lo permiten.
- No se presume que sea recuperable ni que TuCuota cubra automáticamente un costo no reintegrado.
- Se respeta la política de devolución previamente comunicada al pagador.
- Nominal, comisión de plataforma y costo de pasarela se registran y reportan por separado.

## Desglose y conciliación

- Antes de confirmar se presenta un desglose estimado con nominal, comisión del 5% y costos recuperables o no recuperables disponibles.
- Los importes estimados se distinguen de los confirmados por el proveedor.
- Si no puede determinarse un importe confiable, se bloquea la confirmación automática.
- Una excepción requiere usuario con permisos suficientes y justificación; se auditan importes, actor, decisión y resultado.
- Las diferencias entre estimación y devolución efectiva ingresan a conciliación.

## Invariantes

- Cada componente económico se registra por separado.
- Ninguna comisión se devuelve dos veces.
- Una estimación no se presenta como confirmación del proveedor.
- La plataforma no asume automáticamente costos no recuperables.
- Los componentes permanecen aislados por disciplina.

## Pendientes de definición

- Capacidades contractuales reales de Mercado Pago para revertir comisión y costos.
- Flujo de resolución controlada y roles con permiso suficiente para excepciones.
- Política que se comunicará al pagador sobre costos no recuperables.

Estos puntos requieren validación contractual y aprobación funcional.

## Trazabilidad

- DEC-149–DEC-150.
- RN-1506–RN-1538.
- ADR-0002 y `MATERIAL_REVIEW_07.md`.
