# SPEC-030 — Entornos y despliegues controlados

**Estado:** aprobada el 2026-11-25  
**Baseline:** `baseline-v1`

## Objetivo

Definir la separación mínima de entornos del MVP y las condiciones de integración, aprobación, despliegue y recuperación que protegen datos y operaciones financieras.

## Entornos del MVP

- El MVP dispone de dos entornos independientes: Desarrollo y Producción.
- Desarrollo se utiliza para programación, pruebas técnicas e integraciones; Producción, para operaciones reales autorizadas.
- Cada entorno mantiene base de datos, configuración y credenciales propias.
- Desarrollo utiliza credenciales de prueba o sandbox y nunca procesa dinero real.
- Las credenciales financieras reales solo están disponibles en Producción.
- Los datos de prueba no se mezclan con registros financieros productivos y las pruebas no modifican operaciones reales.
- Cada entorno permite identificar la versión desplegada.

Un entorno Staging independiente no es obligatorio para el MVP. Podrá incorporarse posteriormente sin reducir la separación y los controles aprobados.

## Integración y validación

- Cada cambio integrado pasa por verificaciones automáticas.
- El pipeline compila y valida los componentes afectados y ejecuta pruebas antes de habilitar una publicación productiva.
- Las pruebas cubren reglas críticas de cuotas, pagos, permisos y aislamiento.
- Una verificación obligatoria fallida bloquea la publicación.
- Los cambios y las migraciones de base de datos se validan antes de Producción.
- La validación de migraciones debe proteger los registros financieros contra pérdida o alteración indebida.

## Aprobación y despliegue

- Ningún cambio se despliega automáticamente en Producción.
- La publicación requiere aprobación explícita del responsable técnico autorizado.
- Cada despliegue usa configuración y credenciales propias del entorno e identifica versión y cambios incluidos.
- Se registran responsable de aprobación, fecha y resultado.
- El despliegue productivo sigue un procedimiento controlado.

## Continuidad financiera

- Existe un procedimiento documentado de recuperación ante fallas, compatible con las migraciones aplicadas.
- Un despliegue o recuperación no puede duplicar cobros ni volver a ejecutar operaciones ya procesadas.
- Los pagos en curso conservan trazabilidad y deben poder reconciliarse después del despliegue.
- La recuperación técnica no sustituye la conciliación ni las verificaciones financieras posteriores.

## Invariantes

- Desarrollo nunca contiene credenciales financieras reales ni procesa cobros reales.
- Datos de prueba y datos financieros productivos permanecen separados.
- Aprobar una integración no equivale a autorizar su despliegue productivo.
- Producción siempre requiere aprobación humana explícita.
- Una verificación obligatoria fallida impide publicar.
- Los despliegues no reejecutan operaciones financieras históricas.

## Pendientes de definición

- Proveedor de hosting e infraestructura de ejecución.
- Plataforma de CI/CD, estrategia de ramas y eventos que disparan pipelines.
- Responsable técnico nominal, mecanismo de aprobación y segregación operativa.
- Matriz exacta de pruebas automáticas y criterios de éxito.
- Estrategia de despliegue, ventanas de mantenimiento y verificaciones posteriores.
- Procedimiento detallado de recuperación y reversión, sujeto a compatibilidad de migraciones.
- Gestión técnica de secretos y rotación de credenciales.

Estos puntos requieren decisiones técnicas posteriores; esta especificación no selecciona proveedor ni arquitectura definitiva.

## Trazabilidad

- DEC-164–DEC-165.
- RN-1768–RN-1800.
- DEC-166–DEC-167 para backups y recuperación de datos.
- `docs/product/MVP.md` y `MATERIAL_REVIEW_08.md`.

