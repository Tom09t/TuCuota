// Generated from src/TuCuota.Api/openapi/TuCuota.Api.json.
// Do not edit manually; run npm run generate:client.

export interface PersistenceStatusResponse {
  provider: string;
  schema: string;
  configured: boolean;
  canConnect: boolean | null;
}

export interface SystemStatusResponse {
  service: string;
  status: string;
  persistence: string;
}

export async function getSystemStatus(baseUrl = '', init?: RequestInit): Promise<SystemStatusResponse> {
  const response = await fetch(`${baseUrl}/api/system`, { ...init, headers: { Accept: 'application/json', ...init?.headers } });
  if (!response.ok) throw new Error(`GetSystemStatus failed with HTTP ${response.status}.`);
  return response.json() as Promise<SystemStatusResponse>;
}

export async function getPersistenceStatus(baseUrl = '', init?: RequestInit): Promise<PersistenceStatusResponse> {
  const response = await fetch(`${baseUrl}/api/system/persistence`, { ...init, headers: { Accept: 'application/json', ...init?.headers } });
  if (!response.ok) throw new Error(`GetPersistenceStatus failed with HTTP ${response.status}.`);
  return response.json() as Promise<PersistenceStatusResponse>;
}

