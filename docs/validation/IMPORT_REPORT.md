# Informe de importación documental

## Alcance

Importación estructural desde `SSDD - Etapa 2`, sin cambios de contenido en PRD, DEC, RN, CON, trazabilidad ni validación. La comparación SHA-256 confirmó que cada documento importado es idéntico a su fuente.

## Inventario verificado

- 170 identificadores DEC únicos (`DEC-001`–`DEC-170`); 171 encabezados porque `DEC-151` conserva una actualización histórica.
- 1.889 identificadores RN originales únicos (`RN-0001`–`RN-1889`).
- Cinco RN complementarias (`RN-C001`–`RN-C005`).
- 21 registros CON: 20 aprobados y `CON-020` descartada.
- Seis entradas `PRE-RN` históricas conservadas.

## Hallazgos y límites

- `RN-0404`–`RN-0411` poseen dos entradas por identificador; se conservan las versiones históricas según CON-004 y la precedencia de CON-011.
- El CSV contiene 1.897 filas RN para 1.889 IDs únicos, cinco RN-C y seis PRE-RN; la diferencia responde a las ocho versiones históricas duplicadas.
- Windows reserva el nombre `CON`; por ello, el contenido original de `CON.md` se versiona sin modificaciones como `docs/decisions/CONSOLIDATION.md`.
- No se detectaron huecos en las secuencias DEC, RN ni CON.
- La auditoría integral y la baseline funcional definitiva siguen pendientes.

