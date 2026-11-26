# Convención de trazabilidad de implementación

**Estado:** aprobada mediante R0-D12  
**Fecha:** 2026-11-26  
**Aplicación:** cambios posteriores a la autorización explícita de H1

## Propósito

Vincular de forma bidireccional releases, cambios, requisitos, decisiones, pruebas y evidencia sin modificar las matrices históricas DEC/RN/CON ni insertar referencias innecesarias en cada unidad de código.

## Cadena obligatoria

```text
Release → commit/digest → pull request → SPEC/AC/ADR
                                      → pruebas → evidencia
```

## Identificadores

Se utilizan sin abreviaciones alternativas:

- `SPEC-001`.
- `AC-001-02`.
- `ADR-0007`.
- `DEC-170`.
- `RN-1872`.
- `CON-011`.

CI debe comprobar que cada identificador referenciado existe en la documentación normativa correspondiente.

## Pull requests

Cada pull request declara en campos estructurados:

- SPEC afectadas.
- AC cubiertas, implementadas o verificadas.
- ADR aplicables.
- DEC, RN y CON cuando exista dependencia normativa directa.
- Pruebas agregadas o actualizadas.
- Evidencia producida o esperada.
- Riesgos, dependencias y parámetros pendientes.
- Justificación de `No aplica` para campos realmente ajenos al cambio.

La referencia no sustituye explicar el comportamiento modificado. Una lista de identificadores sin relación concreta se considera trazabilidad insuficiente.

## Pruebas y cobertura

Las pruebas citan las AC mediante metadata o tags estructurados que puedan extraerse automáticamente. Cada AC se clasifica como:

- `automatizada`.
- `parcial`.
- `manual-justificada`.
- `bloqueada-externamente`.

Una cobertura `parcial` identifica qué comportamiento falta, por qué y en qué incremento debe completarse. Una cobertura `manual-justificada` identifica procedimiento, responsable y evidencia. Una cobertura `bloqueada-externamente` identifica la dependencia y el gate que impide cerrarla.

Una prueba no declara cubierta una AC que requiere navegador, PostgreSQL o proveedor externo cuando omite esa frontera relevante. La existencia de un tag no demuestra por sí sola cobertura suficiente.

## Evidencia

El repositorio conserva manifiestos de evidencia. Cada entrada registra como mínimo:

- Identificador de AC o control.
- Release, commit y digest aplicables.
- Prueba o procedimiento que produjo la evidencia.
- Fecha y resultado.
- Responsable.
- Hash del artefacto.
- Ubicación controlada.
- Vigencia o fecha de revalidación cuando corresponda.

Los archivos grandes, sensibles o con datos operativos se almacenan fuera de Git en object storage privado. El manifiesto contiene una referencia controlada y no expone secretos, datos personales ni información financiera sensible.

## Matriz consolidada

Cada release produce una vista consolidada con esta relación mínima:

| Campo | Contenido |
|---|---|
| Release | Versión y digest promovido |
| Cambio | Commit y pull request |
| Norma | SPEC y AC; DEC/RN/CON directas cuando corresponda |
| Arquitectura | ADR aplicables |
| Implementación | Componentes afectados |
| Verificación | Pruebas y clasificación de cobertura |
| Evidencia | Manifiesto, hash, resultado y vigencia |
| Estado | Cumplida, parcial, bloqueada o fallida |

La matriz se genera o valida desde fuentes versionadas; no se mantiene como una declaración manual desconectada de cambios y pruebas.

## Controles de CI

CI debe bloquear:

- Identificadores inexistentes o mal formados.
- Referencias a pruebas o evidencia inexistentes.
- AC declaradas automatizadas sin prueba asociada.
- Cobertura parcial sin faltante e incremento de cierre.
- Evidencia sin release, hash, resultado, responsable o vigencia aplicable.
- Cambios de implementación sin referencia técnica o funcional ni justificación de `No aplica`.
- Releases con controles obligatorios fallidos o evidencia vencida.

## Límites

- No es obligatorio insertar identificadores en cada clase, función o comentario de código.
- La trazabilidad no modifica el significado ni la precedencia de los documentos normativos.
- Una referencia no convierte una prueba insuficiente en evidencia válida.
- Una release trazable no queda autorizada para Producción o dinero real sin superar sus gates separados.

## Trazabilidad de esta convención

- R0-D12.
- ADR-0015–ADR-0017.
- SPEC-001–SPEC-034 y ACCEPTANCE-001–ACCEPTANCE-034.
- DEC-170 y sus RN vigentes, bajo la precedencia de CON-011.
