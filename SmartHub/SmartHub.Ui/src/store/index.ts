import { createLogger, createStore } from 'vuex';
import { authModule } from '@/store/auth';
import { homeModule } from '@/store/home';

export const store = createStore({
  plugins: process.env.NODE_ENV === 'development' ? [createLogger()] : [],
  modules: {
    authModule,
    homeModule
  }
});
