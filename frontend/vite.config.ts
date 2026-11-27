import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/api': 'http://localhost:5254',
      '/health': 'http://localhost:5254',
      '/openapi': 'http://localhost:5254',
    },
  },
});
