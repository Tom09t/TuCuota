# SPEC-032 — Monitoreo de Producción y alertas técnicas

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir el monitoreo automático mínimo de Producción, la comunicación segura de fallas técnicas y el registro de su recuperación.

## Cobertura del monitoreo

Producción supervisa automáticamente:

- Disponibilidad de la API y servicios esenciales.
- Errores técnicos que afecten el funcionamiento de la plataforma.
- Disponibilidad y estado operativo de la base de datos.
- Fallas en la generación automática de cuotas.
- Errores críticos de procesamiento de pagos.
- Fallas en procesos esenciales de conciliación.
- Fallas en la ejecución de backups automáticos.

## Alertas técnicas

- Cada problema detectado genera una alerta automática por correo al responsable técnico configurado.
- Las direcciones técnicas se mantienen configuradas y actualizadas.
- El correo identifica como mínimo el servicio, el problema y el momento de detección.
- No expone contraseñas, tokens ni datos financieros sensibles.
- Mientras persista el mismo incidente se evitan alertas idénticas repetitivas, sin suprimir avisos de fallas nuevas.

## Registro y resolución

- Todo incidente técnico detectado queda registrado para diagnóstico.
- Se conserva el registro de su recuperación o resolución.
- La deduplicación de correos no elimina eventos, cambios relevantes ni evidencia del incidente.
- Las alertas técnicas no reemplazan las notificaciones operativas dirigidas a administradores y superadministradores según las especificaciones vigentes.

## Alcance del MVP

El correo electrónico es el canal técnico obligatorio. Durante el MVP no son obligatorios canales adicionales ni una clasificación avanzada de incidentes. Incorporarlos posteriormente no puede debilitar la cobertura, seguridad o trazabilidad aprobadas.

## Invariantes

- Producción cuenta con monitoreo automático desde el MVP.
- Una falla de backup debe ser observable y alertada.
- Las alertas nunca incluyen secretos ni información financiera sensible.
- Deduplicar no puede ocultar un incidente distinto o nuevo.
- La alerta técnica y la notificación operativa son comunicaciones diferentes y ninguna sustituye a la otra.
- La resolución de un incidente conserva trazabilidad.

## Pendientes de definición

- Herramienta o proveedor de monitoreo y correo.
- Responsable técnico nominal y mecanismo para mantener destinatarios actualizados.
- Lista técnica de servicios esenciales, comprobaciones y frecuencia de sondeo.
- Umbrales, ventanas, reglas de deduplicación y criterios de recuperación.
- Retención del historial técnico y procedimiento de diagnóstico y escalamiento.
- Canales adicionales y clasificación avanzada, si se incorporan después del MVP.

Estos puntos requieren decisiones técnicas posteriores y no reducen la cobertura mínima aprobada.

## Trazabilidad

- DEC-168.
- RN-1835–RN-1851.
- DEC-166–DEC-167 y `SPEC-031` para alertas y trazabilidad de backups.
- `SPEC-013`–`SPEC-015` para notificaciones operativas independientes.
- `MATERIAL_REVIEW_08.md`.

