# SPEC-033 — Migración inicial controlada de datos

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir la incorporación inicial de información de la disciplina con validación, aprobación, idempotencia y protección de las operaciones financieras existentes.

## Datos admitidos

La migración inicial puede incorporar:

- Jugadores y sus datos de identificación.
- Categorías e inscripciones de la disciplina.
- Becas y beneficios económicos vigentes.
- Cuotas anteriores que continúen pendientes de pago.
- Pagos externos históricos cuando su información sea suficientemente confiable.

Los pagos históricos conservan fecha y medio original cuando estén disponibles y se identifican como registros históricos externos anteriores al lanzamiento.

## Límites financieros

- Un pago histórico no crea una transacción ficticia en la pasarela.
- Los pagos importados no generan la comisión retroactiva del 5 %.
- Los registros históricos nunca sobrescriben operaciones reales previamente confirmadas.
- Después de incorporar datos se verifica la consistencia de los saldos pendientes.

## Validación y aprobación

1. Antes de confirmar, el sistema valida los datos.
2. El resultado distingue registros válidos, duplicados y con errores.
3. Un administrador autorizado revisa el resultado dentro de su ámbito.
4. Los registros con errores se corrigen o excluyen.
5. La incorporación definitiva requiere aprobación del administrador autorizado.

Revisar o validar no equivale a confirmar la importación.

## Seguridad e idempotencia

- Reintentar el mismo proceso no duplica registros.
- La migración respeta el aislamiento entre clubes y disciplinas.
- Se conserva trazabilidad de los datos importados, su origen y el responsable de la operación.
- La aprobación se limita al ámbito autorizado del administrador.

## Invariantes

- Ningún registro con error se incorpora sin corregirse o excluirse.
- Ningún pago histórico simula actividad de pasarela.
- La migración no crea comisiones retroactivas.
- Un reintento idempotente no duplica entidades ni saldos.
- Los datos de una disciplina no pueden incorporarse en otra.
- Una operación financiera real confirmada prevalece sobre cualquier dato histórico importado.

## Pendientes de definición

- Formatos, plantillas, campos obligatorios y codificación de archivos de entrada.
- Criterios para considerar confiable un pago histórico.
- Reglas exactas de coincidencia y detección de duplicados por tipo de entidad.
- Límites de volumen, procesamiento y estrategia ante errores parciales.
- Mecanismo de idempotencia y plazo de conservación de sus claves.
- Flujo técnico de corrección, exclusión, aprobación y eventual reversión controlada.
- Reporte de conciliación y tolerancias para validar saldos posteriores.

Estos puntos requieren aprobación funcional o técnica posterior y no alteran las prohibiciones financieras vigentes.

## Trazabilidad

- DEC-169.
- RN-1852–RN-1871.
- DEC-170 para aprobación de la migración antes de habilitar cobros reales.
- `docs/product/MVP.md` y `MATERIAL_REVIEW_08.md`.

