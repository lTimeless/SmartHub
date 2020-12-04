import { createLogger, createStore } from 'vuex';
import { authModule } from '@/store/auth';
import { appModule } from '@/store/app';

export const store = createStore({
  plugins: process.env.NODE_ENV === 'development' ? [createLogger()] : [],
  modules: {
    authModule,
    appModule
  }
});
