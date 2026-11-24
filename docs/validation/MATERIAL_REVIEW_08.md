# Revisión material 08 — DEC-151 a DEC-170

**Estado:** aprobada por el propietario el 2026-11-24.  
**Cobertura:** RN-1539–RN-1889, considerando CON-009, CON-011 y CON-020.

## Resultado por bloque

| DEC | RN | Resultado propuesto |
|---|---|---|
| DEC-151 | RN-1539–1564 | Vigentes y complementarias según `MATERIAL_REVIEW_01.md`. Costos por disciplina requieren autorización de sus administradores; costos de plataforma, del superadministrador; delegado solo lectura. |
| DEC-152–160 | RN-1565–1712 | Vigentes. Solicitudes, cancelación, políticas, motivos, evidencias, historial y bandeja respetan disciplina, vínculo autorizado, capacidad real de pasarela y separación entre aprobación y ejecución. |
| DEC-161 | RN-1713–1731 | Vigentes. Define alcance inicial y permite postergar módulos avanzados, pero mantiene procedimientos controlados para devoluciones y contracargos y prohíbe cobros reales sin validación financiera. |
| DEC-162–163 | RN-1732–1767 | Vigentes. El piloto comienza con Obras Futsal y se amplía únicamente mediante controles y autorización del superadministrador. |
| DEC-164–165 | RN-1768–1800 | Vigentes. Desarrollo y Producción permanecen separados; despliegues, credenciales y migraciones requieren verificación, aprobación y trazabilidad. No fijan todavía proveedor de hosting ni stack definitivo. |
| DEC-166–167 | RN-1801–1834 | Vigentes sin modificaciones: backups automáticos cifrados, PITR objetivo de 30 días, copias mensuales por 12 meses, aislamiento, pruebas de restauración y conciliación posterior. |
| DEC-168 | RN-1835–1851 | Vigentes. Monitoreo y alertas técnicas por correo al responsable técnico son distintas de las notificaciones operativas internas y no las reemplazan. |
| DEC-169 | RN-1852–1871 | Vigentes. Migración inicial validada, idempotente, auditada y sin transacciones ficticias ni comisiones retroactivas. |
| DEC-170 | RN-1872–1889 | Vigentes. El checklist, las pruebas financieras, los backups, monitoreo y aprobaciones bloquean cobros reales ante cualquier requisito crítico incumplido. |

## Hallazgos materiales

- RN-1568 y RN-1423 prohíben fracciones arbitrarias de cuotas. CON-020 permanece descartada y no agrega ni modifica reglas.
- La postergación de módulos avanzados en RN-1729 no elimina los procedimientos operativos mínimos exigidos por RN-1728 y RN-1882.
- Los dos entornos aprobados para el MVP no constituyen una selección definitiva de infraestructura, hosting o stack tecnológico.
- Las reglas de DEC-167 se preservan literalmente y no admiten reducción por decisiones arquitectónicas futuras.
- No se habilitan despliegues productivos ni cobros reales por el solo cierre documental de la baseline.

## Resolución aprobada

Estas clasificaciones quedan aprobadas como capa de vigencia para DEC-151–170, sin desplazar RN adicionales.
