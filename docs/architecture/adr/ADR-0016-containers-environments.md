# ADR-0016 — Contenedores y ambientes de ejecución

**Estado:** Aceptada  
**Fecha:** 2026-11-26

## Contexto

ADR-0001 selecciona Docker; ADR-0012 define API y worker; SPEC-030 exige Desarrollo y Producción separados; SPEC-031 fija PITR de 30 días y doce copias mensuales. La infraestructura debe conservar portabilidad sin trasladar al equipo la operación innecesaria de base y orquestación.

## Decisión

Usar en Producción un servicio administrado de contenedores Linux, PostgreSQL administrado, almacenamiento privado de objetos, registro privado, terminación TLS, correo transaccional y observabilidad OTLP.

No adoptar Kubernetes, Docker Swarm ni PostgreSQL autogestionado en contenedor para el MVP.

El proveedor se selecciona por región, residencia, backups, costo, soporte y capacidad operativa sin cambiar los límites de aplicación.

## Imágenes

Construir tres imágenes:

- `tucuota-api`.
- `tucuota-worker`.
- `tucuota-web` con assets React estáticos.

PostgreSQL y almacenamiento no forman parte de las imágenes.

Los Dockerfiles son multi-stage, reproducibles y usan bases oficiales fijadas por tag y digest. Las imágenes finales ejecutan como usuario no root, no contienen SDK ni secretos, incluyen ICU y `tzdata`, exponen puertos no privilegiados y usan filesystem de sólo lectura cuando sea compatible.

## Artefactos

Cada release registra versión, commit, digest, OpenAPI, esquema esperado, SBOM, pruebas y aprobación. Producción despliega por digest, nunca `latest`. El artefacto validado se promueve sin reconstrucción.

## Topología

Un reverse proxy con TLS publica un único origen: `/` para SPA, `/api` para API y `/hubs` para SignalR. PostgreSQL y storage no se exponen; worker no tiene API funcional pública.

## Escalado

La API puede comenzar con una réplica. Antes de escalar horizontalmente requiere claves Data Protection compartidas, scale-out de SignalR, rate limiting distribuido y presupuesto de conexiones.

Worker puede comenzar con una instancia; varias usan Quartz clustering y leases. PostgreSQL utiliza pooling coordinado. Réplicas de lectura o particionamiento requieren métricas y otro ADR.

## Ambientes

### Desarrollo

Docker Compose puede ejecutar API, worker, SPA, PostgreSQL, storage compatible, correo local y telemetría. Usa datos sintéticos y credenciales sandbox.

### CI

Ambientes efímeros, Testcontainers, sin secretos productivos y destruidos al finalizar.

### Preproducción

Staging permanente no es obligatorio. Un ambiente temporal puede validar releases de riesgo con artefactos exactos, base separada, sandbox y datos sintéticos o anonimizados.

### Producción

Cuenta o proyecto separado, recursos y secretos exclusivos, acceso mínimo, monitoreo y backups. Credenciales financieras reales sólo están disponibles aquí.

Desarrollo y Producción no comparten bases, storage, credenciales, identidades, claves, OAuth, backups, dominios ni cookies.

## CI/CD

Adoptar ramas cortas y rama principal protegida. El pipeline restaura, analiza, compila, prueba, valida OpenAPI y migraciones, construye imágenes, genera SBOM, analiza vulnerabilidades y publica artefactos.

Desarrollo puede desplegarse automáticamente. Producción requiere aprobación humana autorizada, migración controlada, despliegue por digest, smoke tests y registro de versión, aprobador, fecha y resultado.

La plataforma concreta de CI/CD puede seleccionarse sin cambiar el flujo.

## Despliegue

- Migraciones separadas y expandir–migrar–contraer.
- Rolling deployment de API con dos o más réplicas; reemplazo controlado con una.
- Worker deja de reclamar, termina o libera leases y luego se reemplaza.
- SPA publica assets con hash atómicamente y conserva temporalmente los anteriores.
- Rollback de imagen sólo si el esquema es compatible.
- Un rollback o recuperación no reejecuta pagos.

## Backups

El proveedor debe garantizar:

- PITR de 30 días.
- Doce copias mensuales independientes.
- Cifrado y aislamiento.
- Acceso restringido.
- Alertas ante fallos.
- Eliminación segura y trazable.
- Preservación legal.
- Protección frente a una única falla administrativa.

Se prueban restauraciones recientes y mensuales en ambiente aislado. Antes de reanudar cobros se verifican datos, RLS, auditoría, jobs, idempotencia y conciliación con la pasarela.

RPO y RTO adicionales continúan pendientes y no reducen la retención aprobada.

## Observabilidad

API y worker exportan OTLP. La plataforma monitorea contenedores, base, storage y backups. Las alertas técnicas obligatorias se envían por correo, se deduplican sin ocultar cambios y no incluyen secretos.

## Consecuencias

- Kubernetes y PostgreSQL autogestionado quedan fuera del MVP.
- Desarrollo usa Compose y Producción servicios administrados.
- Se producen tres imágenes inmutables.
- Staging permanente es opcional.
- La política de backup condiciona al proveedor.
- Proveedor, RPO/RTO y tamaño inicial requieren evaluación operativa.
- Gestión de secretos se decide separadamente.
- Esta decisión no genera código productivo.

## Trazabilidad

- Ambientes y despliegues: `SPEC-030`, `ACCEPTANCE-030`, DEC-164–DEC-165.
- Backups y recuperación: `SPEC-031`, `ACCEPTANCE-031`, DEC-166–DEC-167.
- Monitoreo: `SPEC-032`, `ACCEPTANCE-032`, DEC-168.
- Migración y readiness: `SPEC-033`–`SPEC-034`, `ACCEPTANCE-033`–`ACCEPTANCE-034`, DEC-170.
- Stack y hosts: ADR-0001, ADR-0005 y ADR-0012.
- Observabilidad y pruebas: ADR-0013 y ADR-0015.

## Referencias técnicas

- [Imágenes de contenedor .NET](https://learn.microsoft.com/en-us/dotnet/core/docker/container-images).
- [Multi-stage builds](https://docs.docker.com/build/building/multi-stage).

