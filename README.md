# TuCuota

Repositorio documental del sistema SaaS multiclub de cuotas deportivas, con piloto inicial previsto para Obras Futsal.

## Estado

Este repositorio contiene una **baseline documental preliminar** importada desde el paquete `SSDD - Etapa 2`. La auditoría funcional fue cerrada el 2026-11-24, pero todavía no se creó ni aprobó `baseline-v1`.

No hay código productivo. El stack base y la pasarela seleccionada están registrados en `docs/architecture/adr/`; hosting, topología y validaciones financieras continúan pendientes.

## Navegación

- `docs/product/`: PRD y definición pendiente del MVP.
- `docs/decisions/`: 170 DEC originales y registro de consolidación CON.
- `docs/requirements/`: 1.889 RN originales, cinco RN complementarias e historial.
- `docs/traceability/`: relaciones DEC–RN y CON–RN.
- `docs/validation/`: controles, límites y resultados de importación.
- `docs/specifications/`, `docs/acceptance/`, `docs/architecture/adr/`, `docs/baselines/` y `docs/roadmap/`: documentos pendientes.

Las fuentes privadas se mantienen localmente en `sources/` y están excluidas del control de versiones.

## Precedencia

Aplicar `CON-011`: CON aprobada, corrección explícita aprobada, excepción específica y regla original vigente. Las reglas compatibles se complementan. Ningún documento derivado puede inventar o cambiar requisitos.

