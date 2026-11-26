# Estimaciones por escenarios

**Estado:** método y estimación inicial de R1 aprobados  
**Fecha:** 2026-11-26  
**Método aprobado:** R0-D14

## Objetivo

Preparar estimaciones comparables sin inventar capacidad, dedicación, fechas ni costos definitivos. Este documento no constituye compromiso contractual, calendario ni autorización de H1.

## Unidad

La unidad inicial es la **jornada-persona de esfuerzo**. Toda cifra se presenta como rango y no como valor puntual.

Una jornada-persona mide trabajo estimado; no equivale a un día calendario. La duración solo podrá calcularse después de aprobar equipo, disponibilidad, dedicación, calendario y paralelización efectiva en PAR-008 y PAR-009.

## Escenarios

### Favorable

- Requisitos y parámetros necesarios llegan antes de iniciar el trabajo que los consume.
- Versiones, proveedor y herramientas resultan compatibles.
- Datos de prueba y migración están controlados.
- Las revisiones no descubren retrabajo material.
- Las dependencias externas responden dentro de ventanas compatibles con el trabajo.

### Base

- Existen iteraciones normales de integración, revisión, corrección y evidencia.
- Aparecen defectos no críticos y ajustes razonables de contratos o infraestructura.
- Las dependencias externas requieren seguimiento, pero no fuerzan un cambio arquitectónico.
- La automatización inicial necesita estabilización normal.

### Adverso

- Aparecen incompatibilidades de proveedor, paquetes o servicios.
- Parámetros necesarios llegan tarde o cambian antes del gate.
- La calidad de datos o migraciones exige saneamiento importante.
- Las validaciones técnicas, comerciales, contractuales o legales requieren alternativas.
- Se descubre evidencia que obliga a revisar una decisión arquitectónica mediante ADR.

## Estructura de cada estimación

Cada incremento o frente registra:

- Alcance incluido.
- Exclusiones.
- Rango favorable, base y adverso.
- Nivel de confianza: baja, media o alta.
- Supuestos y dependencias.
- Factores que desplazan el resultado entre escenarios.
- Separación entre construcción, pruebas/evidencia, seguridad/operación y coordinación externa.
- Posibilidad real de paralelización sin violar dependencias.

## Tratamiento de esperas externas

Las esperas de proveedores, asesoría legal, aprobaciones, acceso a sandbox o contratación no se convierten en jornadas-persona ficticias. Se registran separadamente como dependencias sin duración hasta contar con información verificable.

El trabajo necesario para preparar, revisar o responder esas gestiones sí puede estimarse como esfuerzo.

## Reglas

1. No convertir esfuerzo a semanas o fechas mientras PAR-008 y PAR-009 estén abiertos.
2. No ocultar pruebas, evidencia, seguridad, operación o documentación dentro de un porcentaje genérico.
3. No reducir alcance, gates ni controles para ajustar una estimación.
4. R1 se descompone primero; R2–R10 permanecen a nivel macro hasta aproximarse su preparación.
5. Cada rango se actualiza con evidencia real al cerrar un incremento y conserva historial.
6. Los costos cloud se estiman separadamente por escenarios de carga.
7. PAR-023 bloquea todo costo operativo presentado como definitivo.
8. El escenario favorable no constituye compromiso.
9. La suma de esfuerzos no implica paralelización ni duración lineal.
10. Toda cifra nueva requiere revisión y aprobación explícita.

## Conversión futura a calendario

Cuando PAR-008 y PAR-009 estén aprobados podrá utilizarse una relación explícita:

```text
duración estimada = esfuerzo / capacidad efectiva ajustada por dependencias
```

La capacidad efectiva deberá descontar coordinación, soporte, ausencias, revisiones y trabajo no dedicado. La fórmula no presume paralelización entre actividades dependientes.

## Estimación inicial de R1

**Decisión:** R0-D15  
**Confianza:** baja  
**Unidad:** jornadas-persona de esfuerzo

| Frente de R1 | Favorable | Base | Adverso |
|---|---:|---:|---:|
| Solución modular, API y Worker | 4–8 | 7–12 | 12–20 |
| SPA shell y cliente OpenAPI | 4–7 | 7–12 | 12–18 |
| PostgreSQL, esquemas y migraciones | 5–8 | 8–14 | 14–22 |
| Contexto tenant, filtros y RLS base | 8–12 | 12–20 | 20–32 |
| Auditoría, outbox/inbox, idempotencia y concurrencia base | 10–16 | 16–26 | 26–40 |
| OpenTelemetry, logs y health checks | 4–7 | 7–12 | 12–18 |
| Contenedores, CI y cadena de suministro | 7–12 | 12–20 | 20–32 |
| Pruebas automatizadas y evidencia de R1 | 10–16 | 16–26 | 26–40 |
| Documentación técnica y operativa | 3–5 | 5–8 | 8–12 |
| **Total** | **55–91** | **90–150** | **150–234** |

### Incluye

Únicamente el alcance de R1 aprobado mediante R0-D01, incluidas sus verificaciones y evidencia.

### Excluye

- Funcionalidad de negocio de R2 en adelante.
- Infraestructura productiva definitiva.
- Validación comercial o legal de Mercado Pago.
- Esperas de proveedores o asesoría legal.
- Biblioteca visual definitiva.
- Despliegue productivo y dinero real.

### Condiciones

- No convertir los rangos en fechas mientras PAR-008 y PAR-009 estén abiertos.
- No presumir paralelización total ni sumar personas de forma lineal.
- Revisar la estimación después del primer checkpoint técnico autorizado.
- Registrar esfuerzo real por frente para recalibrar R2.
- Si aparece una incompatibilidad material o un cambio arquitectónico, revisar escenario y estimación antes de continuar.
- La estimación no autoriza H1.

### Factores principales de incertidumbre

- Integración de RLS, filtros EF y pruebas negativas de aislamiento.
- Alcance reusable de auditoría, outbox/inbox e idempotencia.
- Compatibilidad efectiva del snapshot de paquetes.
- Estabilización de CI, Testcontainers y evidencia reproducible.
- Cantidad real de trabajo operativo necesario para AWS sin aprovisionar Producción.

## Estado de estimaciones

| Alcance | Estado | Confianza | Bloqueos |
|---|---|---|---|
| R1 | Estimación inicial aprobada | Baja | PAR-004 y PAR-005; evidencia ejecutable posterior a H1 |
| R2–R10 | Macro pendiente | Baja | Preparación de cada incremento y parámetros aplicables |
| Calendario | Bloqueado | No aplicable | PAR-008 y PAR-009 |
| Costos definitivos | Bloqueado | No aplicable | PAR-010, PAR-012 y PAR-023 |

## Trazabilidad

- R0-D14 y R0-D15.
- `docs/roadmap/ROADMAP-v1.md`.
- R0-D01 y R0-D10–R0-D13.
- PAR-008, PAR-009, PAR-010, PAR-012 y PAR-023.
