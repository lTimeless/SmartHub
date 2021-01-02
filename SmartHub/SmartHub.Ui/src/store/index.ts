import { createLogger, createStore } from 'vuex';
import { appModule } from '@/store/app';

export const store = createStore({
  plugins: process.env.NODE_ENV === 'development' ? [createLogger()] : [],
  modules: {
    appModule
  }
});
