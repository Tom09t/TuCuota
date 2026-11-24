# Repository Guidelines

## Project Structure & Module Organization

This is a specification-first workspace for the Sistema Tu Cuota SaaS. It contains:

- `docs/product/`: PRD and the pending MVP definition.
- `docs/decisions/`: original decision evidence (`DEC-###`) and consolidation records.
- `docs/requirements/`: normative rules (`RN-####`) and their CSV export.
- `docs/traceability/`: DEC/RN and CON/RN mappings.
- `docs/validation/`: unresolved reviews and import controls.
- `docs/specifications/`, `docs/acceptance/`, `docs/architecture/adr/`, `docs/baselines/`, and `docs/roadmap/`: explicitly pending artifacts.
- `sources/`: private source exports; ignored by Git and never published.

There is no application source or automated test tree yet. Do not add production code until the specification phase explicitly authorizes it.

## Build, Test, and Development Commands

No build system or package manager is configured. Current work is document validation. Useful checks from the repository root include:

```powershell
rg "DEC-[0-9]{3}|RN-[0-9]{4}|CON-[0-9]{3}" docs
rg "pendiente|revisión|contradic" docs
```

Open `README.md` first, then read the documents under `docs/product/`, `docs/decisions/`, `docs/requirements/`, and `docs/validation/` before proposing changes.

## Documentation Style & Naming

Use UTF-8 Markdown, ATX headings, short paragraphs, and repository terminology in Spanish. Preserve identifier formats exactly: `DEC-170`, `RN-0001`, and `CON-011`. Cite relevant DEC/RN records in specifications, acceptance criteria, and future tests. Keep CSV headers and column order stable unless a coordinated migration is documented.

## Validation Guidelines

Review changes for internal consistency, traceability, and status precedence. `CONSOLIDATION.md` and approved replacement text in `RN.md` supersede historical wording; examples, rejected options, and `PRE-RN` entries are not requirements. Never resolve a material contradiction by assumption. Do not alter backup requirements from `DEC-167`, deploy, or process real money without satisfying `DEC-170`.

## Commit & Pull Request Guidelines

Git history is not available in this checkout, so no established commit pattern can be inferred. Use concise imperative subjects, for example `docs: clarify RN-0421 precedence`. Pull requests should summarize the change, list affected DEC/RN/CON IDs, explain traceability updates, identify unresolved conflicts, and include screenshots only when rendered-document layout changes.
