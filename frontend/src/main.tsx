import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { useQuery } from '@tanstack/react-query';
import { getPersistenceStatus, getSystemStatus } from './api/generated';
import './styles.css';

const queryClient = new QueryClient();

function App() {
  const system = useQuery({ queryKey: ['system-status'], queryFn: () => getSystemStatus() });
  const persistence = useQuery({ queryKey: ['persistence-status'], queryFn: () => getPersistenceStatus() });

  return (
    <main>
      <header>
        <p className="eyebrow">R1 · Fundación técnica</p>
        <h1>Sistema Tu Cuota</h1>
        <p>Recorrido verificable entre la interfaz, la API y PostgreSQL.</p>
      </header>
      <section aria-labelledby="technical-status">
        <h2 id="technical-status">Estado técnico</h2>
        <dl>
          <div><dt>API</dt><dd>{system.data?.status ?? (system.isError ? 'no disponible' : 'consultando')}</dd></div>
          <div><dt>Servicio</dt><dd>{system.data?.service ?? '—'}</dd></div>
          <div><dt>PostgreSQL</dt><dd>{persistence.data?.configured ? (persistence.data.canConnect ? 'conectado' : 'sin conexión') : 'no configurado'}</dd></div>
          <div><dt>Esquema</dt><dd>{persistence.data?.schema ?? '—'}</dd></div>
        </dl>
        {(system.isError || persistence.isError) && <p role="alert">No fue posible completar el recorrido técnico.</p>}
      </section>
      <p className="scope">Sin datos reales, identidad funcional ni operaciones financieras.</p>
    </main>
  );
}

createRoot(document.getElementById('root')!).render(
  <StrictMode><QueryClientProvider client={queryClient}><App /></QueryClientProvider></StrictMode>,
);
