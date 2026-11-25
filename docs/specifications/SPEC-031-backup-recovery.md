# SPEC-031 — Backups, retención y recuperación financiera

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir la protección, conservación, restauración y validación financiera de las copias de Producción sin reducir la política aprobada en DEC-167.

## Copias y protección

- La base de datos de Producción cuenta con backups automáticos.
- Se implementa recuperación a un punto en el tiempo (PITR) mediante copias completas y registros de transacciones, o mecanismos equivalentes.
- Todas las copias permanecen cifradas, aisladas del entorno productivo y accesibles solo por personal técnico autorizado.
- La ejecución de backups se supervisa y sus fallas generan alertas operativas.
- Creación, restauración y eliminación conservan trazabilidad operativa.

## Retención obligatoria

- Se mantiene una ventana objetivo de PITR de 30 días.
- Se conserva una copia mensual durante 12 meses, independientemente de la ventana PITR.
- El proveedor debe soportar ambos períodos; si no ofrece PITR de 30 días, se implementa una solución equivalente antes de considerar cumplido el requisito.
- Las copias vencidas se eliminan de forma segura, salvo obligación legal o medida de preservación aplicable.
- Esta retención no reemplaza la conservación legal de comprobantes, operaciones financieras ni auditorías.

## Pruebas y restauración

- Se comprueba periódicamente la integridad de las copias.
- Las pruebas periódicas de restauración incluyen puntos recientes y copias mensuales, y verifican información consistente y utilizable.
- Las pruebas y restauraciones preservan la confidencialidad y el aislamiento de los datos de los clubes.
- Toda restauración productiva se registra y audita conforme a un procedimiento documentado.

## Reanudación financiera

Después de restaurar:

1. Se verifica la consistencia de cuotas, pagos y operaciones financieras.
2. Se reconcilian los pagos con los registros reales de la pasarela.
3. Los pagos inciertos no se vuelven a cobrar sin verificar previamente su estado.
4. Se mantienen idempotencia y prevención de duplicaciones.
5. Las operaciones financieras solo se reanudan tras validar la consistencia.

Restaurar una copia reciente o histórica nunca permite omitir la conciliación posterior.

## Invariantes

- La ventana objetivo PITR de 30 días y las 12 copias mensuales no pueden reducirse por una decisión arquitectónica posterior.
- Las copias mensuales son independientes de PITR.
- Ninguna copia se almacena sin cifrado o junto al entorno productivo.
- Una restauración técnica no autoriza automáticamente reanudar cobros.
- Los registros de la pasarela son referencia obligatoria para la conciliación posterior.
- Las obligaciones legales o medidas de preservación prevalecen sobre la eliminación por vencimiento.

## Pendientes de definición

- Proveedor y mecanismo concreto de backup/PITR.
- Frecuencia de copias completas, registros y comprobaciones de integridad.
- Periodicidad y responsables de las pruebas de restauración.
- Objetivos cuantitativos RPO/RTO distintos de la ventana de conservación aprobada.
- Procedimiento técnico de restauración, custodia de claves y eliminación segura.
- Formato de evidencias, alertas y registros operativos.

Estos puntos requieren decisiones técnicas posteriores y no pueden reducir los períodos ni controles aprobados.

## Trazabilidad

- DEC-166–DEC-167.
- RN-1801–RN-1834.
- DEC-165 y `SPEC-030` para despliegues y recuperación ante fallas.
- `MATERIAL_REVIEW_08.md`, que ordena preservar literalmente DEC-167.

