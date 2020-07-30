import { createStore } from 'vuex';
import { auth } from '@/store/auth';

export default createStore({
  modules: {
    auth
  }
});
