import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// In dev, proxy /api to the .NET backend (see backend: dotnet run --urls http://localhost:8080).
// In the deployed Docker stack, nginx performs the same proxy to the cuberender-api Service.
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    proxy: {
      '/api': 'http://localhost:8080',
    },
  },
  build: { outDir: 'dist' },
})
