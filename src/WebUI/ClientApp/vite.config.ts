import path from 'path';
import { defineConfig } from 'vite'
import reactRefresh from '@vitejs/plugin-react-refresh'
import ViteFonts from 'vite-plugin-fonts';
import tsconfigPaths from 'vite-tsconfig-paths';

// https://vitejs.dev/config/
export default defineConfig({
  resolve: {
    alias: {
      '@/': `${path.resolve(__dirname, 'src')}/`
    }
  },
  server: {
    proxy: {
      '/api': {
        target: 'https://localhost:5001',
        changeOrigin: false,
        secure: false
      },
      '/graphql': {
        target: 'https://localhost:5001',
        changeOrigin: false,
        secure: false
      }
    }
  },
  build: {
    emptyOutDir: true,
    outDir: '../wwwroot'
  },
  plugins: [
      reactRefresh(),
      tsconfigPaths(),
      ViteFonts({
          google: {
              families: ['JetBrains Mono', 'Roboto']
          }
      })]
})
