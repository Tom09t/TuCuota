# ADR-0017 — Seguridad y gestión de secretos

**Estado:** Aceptada  
**Fecha:** 2026-11-26

## Contexto

El sistema procesa identidad, datos personales, evidencia y operaciones financieras. ADR-0009 establece autenticación y autorización; ADR-0010 y ADR-0011 protegen operaciones financieras; ADR-0016 define contenedores y ambientes. Se requiere una estrategia transversal de seguridad, cifrado, secretos y cadena de suministro.

## Decisión

Adoptar OWASP ASVS 5.0 nivel 2 como baseline, complementado con controles seleccionados de nivel 3 para autenticación, autorización multitenant, criptografía, lógica financiera, archivos, administración global, despliegues y recuperación.

Antes de dinero real se exige evidencia automatizada, revisión manual y prueba de penetración independiente.

## Secretos y claves

En Producción usar un gestor administrado de secretos y KMS, identidades de workload, credenciales de corta duración, inyección en runtime y auditoría de acceso y rotación.

Cada secreto registra propietario, propósito, ambiente, consumidores, origen, creación, rotación, revocación y respuesta de emergencia.

- Ningún secreto en Git, documentación, imágenes, frontend, OpenAPI, logs o trazas.
- Ningún secreto en archivos `appsettings` versionados ni Docker `ARG`.
- BuildKit secret mounts sólo cuando un build requiera acceso privado.
- Secretos separados por ambiente y servicio.
- API, worker, migrador, backup y CI usan identidades distintas y mínimo privilegio.

Desarrollo utiliza User Secrets o archivos locales ignorados y sólo credenciales sandbox. CI utiliza federación OIDC cuando esté disponible y no entrega secretos a pull requests no confiables.

## Tokens de vendedores

Los tokens OAuth permanecen en backend, cifrados con envelope encryption y claves maestras protegidas por KMS. Se separan por ambiente, proveedor y conexión, conservan versión de clave y permiten rotación y re-cifrado gradual.

Refresh, lectura y revocación se auditan sin exponer tokens. Un fallo de descifrado bloquea nuevos cobros y genera alerta.

## Data Protection

El key ring de ASP.NET Core se persiste fuera de contenedores, se comparte entre réplicas, se separa por ambiente, se cifra mediante KMS y se incluye en recuperación. Las claves retiradas se conservan mientras deban descifrar información; una clave comprometida puede revocarse.

Data Protection no sustituye KMS para protección de largo plazo.

## Cifrado

- HTTPS obligatorio, TLS 1.2 mínimo y TLS 1.3 cuando esté disponible.
- HSTS en Producción.
- Renovación automática de certificados.
- Validación TLS saliente completa.
- PostgreSQL, storage, correo, pasarela y OTLP cifrados en tránsito.
- PostgreSQL, storage, backups y secretos cifrados en reposo.
- Claves maestras separadas de datos.
- Copias y PITR conservan controles equivalentes.

No se implementa criptografía propia.

## Navegador

Aplicar CSP restrictiva, `frame-ancestors`, `nosniff`, Referrer Policy, Permissions Policy, HSTS, cookies seguras, antiforgery y CORS cerrado por defecto.

No se permiten datos sensibles en URL o analytics, scripts inline no autorizados ni personalización de club con HTML, JavaScript o CSS arbitrario.

## Validación

- DTO separados de entidades.
- Allowlist de campos modificables.
- Validación de formato, tamaño, rango y estado.
- Consultas parametrizadas.
- Codificación de salida.
- Protección contra mass assignment.
- Límites de payload y paginación.
- Errores públicos sin stack, SQL o infraestructura.

## Archivos

Toda carga pasa por autorización, límites, allowlist, magic bytes, nombre generado, cuarentena, antimalware, cifrado, storage privado y hash de integridad.

No se ejecuta ni sirve inline contenido no confiable. Las descargas se reautorizan, usan enlaces temporales y respetan inmutabilidad, retención y preservación.

## Red y runtime

- Base y storage sin exposición pública.
- Ingreso sólo por proxy autorizado.
- Egress limitado cuando sea posible.
- Contenedores no root, filesystem de sólo lectura y capacidades mínimas.
- Límites de recursos.
- Protección de metadata service y SSRF.
- URLs salientes construidas desde proveedores configurados.
- Timeouts y límites para servicios externos.

## Cadena de suministro

- Ramas protegidas y revisión.
- Lockfiles y dependencias centralizadas.
- Secret scanning, SAST, SCA y análisis de imágenes e infraestructura.
- Revisión de licencias.
- SBOM, firma o attestation y procedencia.
- Builds efímeros.
- Artefactos promovidos sin reconstrucción.
- Monitoreo continuo de vulnerabilidades desplegadas.

Vulnerabilidades críticas bloquean releases. Las excepciones documentan riesgo, responsable y vencimiento.

## Desarrollo seguro

Cambios sensibles incluyen threat modeling, casos de abuso, pruebas negativas y revisión de autorización, aislamiento, idempotencia, logs, criptografía y dependencias.

El modelo de amenazas cubre cruce de tenant, toma de cuenta, fraude por reintento, webhooks, tokens OAuth, evidencia, superadministración, CI/CD, restauración y exfiltración por reportes.

## Gate productivo

Antes de dinero real se requiere:

- Revisión ASVS aplicable.
- Pruebas de aislamiento y autorización.
- Escaneo de dependencias e imágenes.
- Secret scanning sin hallazgos críticos abiertos.
- Penetration test independiente.
- Revisión de configuración productiva.
- Rotación de secretos ensayada.
- Restauración de claves y datos cifrados.
- Webhooks y replay probados.
- Simulación de secreto comprometido.
- MFA de superadministración.
- Auditoría sin material sensible.
- Gates de ADR-0002 y DEC-170 completos.

## Incidentes

Ante exposición se revoca y rota, se identifican consumidores, se invalidan sesiones, se revisa auditoría, se re-cifra si corresponde, se comprueban backups y se documenta impacto y prevención. Borrar un secreto de Git no resuelve la exposición histórica.

## Consecuencias

- Gestor de secretos y KMS condicionan la selección del proveedor.
- Tokens OAuth reciben cifrado de aplicación adicional.
- CI/CD usa identidades federadas y procedencia verificable.
- Uploads requieren cuarentena y antimalware.
- ASVS, threat modeling y penetration testing forman parte del gate productivo.
- Esta decisión no genera código productivo.

## Trazabilidad

- Identidad, sesiones y permisos: `SPEC-001`, `SPEC-016`, `ACCEPTANCE-001`, `ACCEPTANCE-016`.
- Seguridad financiera: `SPEC-003`–`SPEC-011`, `SPEC-017`–`SPEC-027`.
- Archivos y evidencias: `SPEC-012`, `SPEC-020`, `SPEC-026`.
- OAuth, webhooks y pasarela: `SPEC-017`–`SPEC-019`.
- Controles MVP: `SPEC-028`, AC-028-08–AC-028-12.
- Ambientes y credenciales: `SPEC-030`, `ACCEPTANCE-030`.
- Backups cifrados: `SPEC-031`, `ACCEPTANCE-031`.
- Alertas seguras: `SPEC-032`, `ACCEPTANCE-032`.
- Migración y gate: `SPEC-033`–`SPEC-034`, `ACCEPTANCE-033`–`ACCEPTANCE-034`, DEC-170.
- Arquitectura aprobada: ADR-0004–ADR-0016.

## Referencias técnicas

- [OWASP ASVS 5.0](https://asvs.owasp.org/).
- [OWASP Secrets Management](https://cheatsheetseries.owasp.org/cheatsheets/Secrets_Management_Cheat_Sheet.html).
- [OWASP Software Supply Chain Security](https://cheatsheetseries.owasp.org/cheatsheets/Software_Supply_Chain_Security_Cheat_Sheet.html).
- [ASP.NET Core Data Protection](https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-10.0).
- [Docker build secrets](https://docs.docker.com/build/building/secrets).

