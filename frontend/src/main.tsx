import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import './styles.css';

const queryClient = new QueryClient();

function App() {
  return <main><h1>Sistema Tu Cuota</h1><p>Fundación técnica de R1 preparada.</p></main>;
}

createRoot(document.getElementById('root')!).render(
  <StrictMode><QueryClientProvider client={queryClient}><App /></QueryClientProvider></StrictMode>,
);
