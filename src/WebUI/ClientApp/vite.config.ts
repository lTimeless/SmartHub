import path from 'path';
import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import tsconfigPaths from 'vite-tsconfig-paths';
import ViteFonts from 'vite-plugin-fonts';

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
  optimizeDeps: {
    include: ['@urql/vue', '@urql/core']
  },
  plugins: [
    vue(),
    tsconfigPaths(),
    ViteFonts({
      google: {
        families: ['JetBrains Mono', 'Roboto']
      }
    })
  ]
});
