import { readFile, writeFile } from 'node:fs/promises';
import { fileURLToPath } from 'node:url';
import path from 'node:path';

const scriptDirectory = path.dirname(fileURLToPath(import.meta.url));
const frontendDirectory = path.resolve(scriptDirectory, '..');
const contractPath = path.resolve(frontendDirectory, '../src/TuCuota.Api/openapi/TuCuota.Api.json');
const outputPath = path.resolve(frontendDirectory, 'src/api/generated.ts');
const document = JSON.parse(await readFile(contractPath, 'utf8'));

if (!document.openapi?.startsWith('3.1.')) {
  throw new Error(`Expected OpenAPI 3.1.x, received ${document.openapi ?? 'missing'}.`);
}

const toTypeScriptType = (schema) => {
  if (schema.$ref) return schema.$ref.split('/').at(-1);
  if (Array.isArray(schema.type)) {
    const nonNull = schema.type.filter((type) => type !== 'null');
    const value = toTypeScriptType({ ...schema, type: nonNull[0] });
    return schema.type.includes('null') ? `${value} | null` : value;
  }
  if (schema.type === 'boolean') return 'boolean';
  if (schema.type === 'integer' || schema.type === 'number') return 'number';
  if (schema.type === 'array') return `${toTypeScriptType(schema.items)}[]`;
  return 'string';
};

const lines = [
  '// Generated from src/TuCuota.Api/openapi/TuCuota.Api.json.',
  '// Do not edit manually; run npm run generate:client.',
  '',
];

for (const [name, schema] of Object.entries(document.components?.schemas ?? {})) {
  lines.push(`export interface ${name} {`);
  const required = new Set(schema.required ?? []);
  for (const [property, propertySchema] of Object.entries(schema.properties ?? {})) {
    lines.push(`  ${property}${required.has(property) ? '' : '?'}: ${toTypeScriptType(propertySchema)};`);
  }
  lines.push('}', '');
}

for (const [route, pathItem] of Object.entries(document.paths ?? {})) {
  const operation = pathItem.get;
  if (!operation?.operationId) continue;
  const responseSchema = operation.responses?.['200']?.content?.['application/json']?.schema;
  if (!responseSchema) throw new Error(`${operation.operationId} has no JSON 200 response schema.`);
  const returnType = toTypeScriptType(responseSchema);
  const functionName = `${operation.operationId[0].toLowerCase()}${operation.operationId.slice(1)}`;
  lines.push(`export async function ${functionName}(baseUrl = '', init?: RequestInit): Promise<${returnType}> {`);
  lines.push(`  const response = await fetch(\`${'${baseUrl}'}${route}\`, { ...init, headers: { Accept: 'application/json', ...init?.headers } });`);
  lines.push(`  if (!response.ok) throw new Error(\`${operation.operationId} failed with HTTP ${'${response.status}'}.\`);`);
  lines.push(`  return response.json() as Promise<${returnType}>;`);
  lines.push('}', '');
}

await writeFile(outputPath, `${lines.join('\n')}\n`, 'utf8');
