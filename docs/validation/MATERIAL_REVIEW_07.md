# Revisión material 07 — DEC-121 a DEC-150

**Estado:** aprobada por el propietario el 2026-11-24.  
**Cobertura:** RN-1062–RN-1538, considerando CON-009 y CON-017–CON-020.

## Resultado por bloque

| DEC | RN | Resultado propuesto |
|---|---|---|
| DEC-121 | RN-1062–1072 | Vigentes. Los pagos pueden agrupar cuotas completas solo dentro de una disciplina; CON-017 computa cada cuota cancelada como pago comercial independiente. |
| DEC-122–125 | RN-1073–1133 | Vigentes. Cuentas receptoras, administración y pasarelas permanecen aisladas por disciplina, con verificación reforzada, Split Payment y auditoría. |
| DEC-126–132 | RN-1134–1237 | Vigentes. Interrupciones, monitoreo, catálogo y reactivación distinguen fallas técnicas de bloqueos globales y preservan pagos externos e historial. |
| DEC-133–139 | RN-1238–1350 | Vigentes. Cambios de pasarela, checkouts y pagos inciertos conservan idempotencia; solo evidencia suficiente y autorización del superadministrador permiten resolución excepcional. |
| DEC-140–142 | RN-1351–1400 | Vigentes. Retención, almacenamiento cifrado y versionado inmutable de evidencias mantienen aislamiento, permisos y auditoría. |
| DEC-143–145 | RN-1401–1454 | Vigentes complementadas por CON-018: una devolución total confirmada de una cuota descuenta el período original si sigue abierto; si está cerrado, genera ajuste en un período abierto sin reescribir la liquidación cerrada. |
| DEC-146–148 | RN-1455–1505 | Vigentes complementadas por CON-019: el contracargo pendiente mantiene el cómputo; una disputa ganada lo conserva; una reversión definitiva total descuenta según CON-018. |
| DEC-149–150 | RN-1506–1538 | Vigentes. Comisión de plataforma y costos de pasarela se tratan por componente, capacidad real del proveedor y política comunicada, evitando duplicaciones. |

## Hallazgos materiales

- CON-017 no altera la posibilidad de un checkout agrupado; únicamente establece que cada cuota cancelada cuenta por separado para el mínimo comercial.
- CON-018 añade la imputación temporal contable de devoluciones sin modificar directamente períodos cerrados.
- CON-019 diferencia disputa pendiente, ganada y reversión definitiva para el cómputo comercial.
- CON-020 permanece descartada y no introduce reglas. RN-1423 continúa prohibiendo devolver fracciones arbitrarias de una cuota.
- “Devolución parcial” significa devolver una o varias cuotas completas dentro de un checkout, sujeta al proveedor; no una fracción monetaria arbitraria de una cuota.
- CON-009 mantiene al delegado general en solo lectura sobre devoluciones, costos y autorizaciones financieras.

## Resolución aprobada

Estas clasificaciones quedan aprobadas como capa de vigencia para DEC-121–150, sin desplazar RN adicionales.
