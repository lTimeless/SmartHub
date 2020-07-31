import { createStore } from 'vuex';
import { authModule } from '@/store/auth';
import { homeModule } from '@/store/home';

export default createStore({
  modules: {
    authModule,
    homeModule
  }
});
