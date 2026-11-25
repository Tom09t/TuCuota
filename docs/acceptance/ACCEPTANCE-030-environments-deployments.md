# ACCEPTANCE-030 — Entornos y despliegues controlados

**Estado:** aprobada el 2026-11-25  
**Especificación:** `SPEC-030`

## Escenarios

### AC-030-01 — Entornos independientes

**Dado** el MVP desplegable  
**Cuando** se inspeccionan sus entornos  
**Entonces** Desarrollo y Producción mantienen bases de datos, configuraciones y credenciales independientes.

### AC-030-02 — Pasarela de Desarrollo

**Dado** una prueba financiera en Desarrollo  
**Cuando** se conecta con la pasarela  
**Entonces** utiliza credenciales de prueba o sandbox y no procesa dinero real.

### AC-030-03 — Credencial productiva aislada

**Dado** una credencial financiera real  
**Cuando** se intenta obtener desde Desarrollo  
**Entonces** el acceso es imposible y la credencial permanece únicamente en Producción.

### AC-030-04 — Datos de prueba aislados

**Dado** datos u operaciones de prueba  
**Cuando** se ejecutan validaciones técnicas  
**Entonces** no se mezclan ni modifican registros financieros productivos.

### AC-030-05 — Versión identificable

**Dado** una versión desplegada  
**Cuando** se consulta cada entorno  
**Entonces** puede identificarse inequívocamente la versión presente.

### AC-030-06 — Integración verificada

**Dado** un cambio integrado  
**Cuando** se ejecuta CI  
**Entonces** se compilan y validan los componentes afectados y se ejecutan las pruebas automáticas aplicables.

### AC-030-07 — Reglas críticas probadas

**Dado** un cambio que afecta cuotas, pagos, permisos o aislamiento  
**Cuando** el pipeline lo valida  
**Entonces** ejecuta pruebas automáticas sobre las reglas críticas afectadas.

### AC-030-08 — Verificación fallida

**Dado** una verificación obligatoria fallida  
**Cuando** se intenta publicar  
**Entonces** el pipeline bloquea el despliegue.

### AC-030-09 — Migración validada

**Dado** un despliegue con cambios de base de datos  
**Cuando** se prepara para Producción  
**Entonces** la migración se valida previamente contra pérdida o alteración indebida de registros financieros.

### AC-030-10 — Aprobación explícita

**Dado** un pipeline con verificaciones satisfactorias  
**Cuando** aún no aprobó el responsable técnico autorizado  
**Entonces** no se despliega automáticamente en Producción.

### AC-030-11 — Aprobador no autorizado

**Dado** un usuario sin autoridad técnica  
**Cuando** intenta aprobar la publicación productiva  
**Entonces** el sistema deniega la aprobación.

### AC-030-12 — Registro de despliegue

**Dado** una publicación productiva autorizada  
**Cuando** se ejecuta  
**Entonces** registra versión, cambios, aprobador, fecha y resultado.

### AC-030-13 — Configuración del entorno

**Dado** un despliegue dirigido a Producción  
**Cuando** se prepara su configuración  
**Entonces** utiliza exclusivamente credenciales y parámetros productivos controlados.

### AC-030-14 — Operación ya procesada

**Dado** un pago procesado antes del despliegue  
**Cuando** inicia la nueva versión o se ejecuta una recuperación  
**Entonces** no vuelve a cobrar ni reejecuta la operación.

### AC-030-15 — Pago en curso

**Dado** un pago en curso durante un despliegue  
**Cuando** finaliza la publicación  
**Entonces** conserva su trazabilidad y puede reconciliarse con la pasarela.

### AC-030-16 — Recuperación compatible

**Dado** una falla posterior al despliegue  
**Cuando** se aplica el procedimiento documentado de recuperación  
**Entonces** considera la compatibilidad de las migraciones y mantiene los controles financieros.

### AC-030-17 — Staging no obligatorio

**Dado** el alcance del MVP  
**Cuando** se verifica la topología mínima  
**Entonces** la ausencia de un entorno Staging independiente no incumple DEC-164, siempre que Desarrollo y Producción permanezcan correctamente separados.

## Pendientes no aceptables todavía

No pueden cerrarse proveedor, plataforma CI/CD, ramas, aprobador nominal, matriz exacta de pruebas, estrategia de despliegue, gestión de secretos ni recuperación detallada sin decisiones técnicas aprobadas.

## Trazabilidad

Cada escenario deriva de `SPEC-030`, DEC-164–DEC-165 y RN-1768–RN-1800. La protección de backups se desarrolla separadamente bajo DEC-166–DEC-167.

