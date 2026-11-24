# Auditoría de trazabilidad del PRD

**Estado:** corrección aprobada y ejecutada el 2026-11-24.  
**Alcance:** referencias explícitas del PRD hacia DEC-001–DEC-170.

## Resultado cuantitativo

- Líneas de trazabilidad explícita: 15.
- DEC referenciadas al menos una vez: 113.
- DEC sin referencia explícita: 57.
- Referencias DEC fuera de rango: 0.
- La sección 15 también referencia correctamente RN-1872–RN-1889.

## DEC faltantes agrupadas

| Grupo | DEC faltantes | Diagnóstico | Acción propuesta |
|---|---|---|---|
| Ciclo de vida del club | DEC-002–004 | El PRD describe multiclub, pero no enlaza alta, baja lógica y reactivación. | Ampliar trazabilidad de la sección 3, sin cambiar su texto funcional. |
| URLs e identidad visual | DEC-010–012 | El modelo multiclub no enlaza slug, personalización ni permisos visuales. | Ampliar trazabilidad de las secciones 3 y 4. |
| Ciclo de vida de disciplinas | DEC-024–026 | Gestión deportiva no enlaza desactivación, reactivación ni períodos inactivos. | Ampliar trazabilidad de la sección 5. |
| Gestión comercial | DEC-049–079 | Omisión material: mínimos, penalizaciones, contratos, gracia, liquidaciones, suspensiones, alertas y dashboard comercial no están suficientemente descritos. | Incorporar una sección derivada de estas DEC y CON-017/021, sujeta a aprobación. |
| Notificaciones avanzadas | DEC-104–116 | El PRD posterga automatizaciones, pero no enlaza el sistema avanzado de notificaciones. | Referenciar desde reportes/funciones posteriores y aclarar su condición post-MVP donde corresponda. |
| Continuidad de pasarelas | DEC-126–129 | Seguridad financiera no enlaza interrupciones, clasificación de fallas, recuperación y monitoreo. | Ampliar trazabilidad y resumen de la sección 8. |

## Lista completa de faltantes

DEC-002–004, DEC-010–012, DEC-024–026, DEC-049–079, DEC-104–116 y DEC-126–129.

## Hallazgos cualitativos

- La adenda CON está presente, pero no sustituye la necesidad de que el cuerpo del PRD describa el modelo comercial aprobado.
- Agregar solo IDs a la trazabilidad de DEC-049–079 ocultaría la omisión funcional; debe incorporarse un resumen fiel, sin crear requisitos nuevos.
- Las referencias de funciones avanzadas deben distinguir alcance funcional aprobado de alcance del MVP conforme a DEC-161.
- La dependencia de seleccionar una pasarela continúa abierta y no invalida los requisitos de continuidad, seguridad o conciliación.

## Propuesta de corrección

1. Completar referencias simples para ciclo de vida, identidad visual y disciplinas.
2. Agregar al PRD una sección de gestión comercial derivada exclusivamente de DEC-049–079 y CON-017/021.
3. Incorporar continuidad de pasarelas en seguridad financiera.
4. Enlazar notificaciones avanzadas como alcance posterior cuando así lo determina DEC-161.
5. Repetir el control hasta alcanzar cobertura explícita de 170/170 DEC.

La corrección actualizará un documento derivado, no las DEC, RN ni CON originales.

## Resultado posterior a la corrección

- DEC referenciadas al menos una vez: 170/170.
- DEC sin referencia explícita: 0.
- Referencias DEC fuera de rango: 0.
- Se incorporó gestión contractual y continuidad comercial derivada de DEC-049–079 y CON-017/021.
- Se completaron referencias de ciclo de vida, identidad visual, disciplinas, notificaciones y continuidad de pasarelas.
- No se modificaron DEC, RN ni CON originales.
