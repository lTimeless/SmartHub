import { CommitOptions, createLogger, createStore, DispatchOptions, Store as VuexStore } from 'vuex';
import { authModule } from '@/store/auth';
import { homeModule } from '@/store/home';
import { RootState } from '@/store/index.types';
import { AuthMutations } from '@/store/auth/mutations';
import { AuthGetters } from '@/store/auth/getters';
import { AuthActions } from '@/store/auth/actions';
import { HomeMutations } from '@/store/home/mutations';
import { HomeActions } from '@/store/home/actions';
import { HomeGetters } from '@/store/home/getters';

// Store types
type Mutations = AuthMutations & HomeMutations;
type Actions = AuthActions & HomeActions;
type Getters = AuthGetters & HomeGetters;

// setup store type
export type Store = Omit<VuexStore<RootState>, 'commit' | 'getters' | 'dispatch'> & {
  commit<K extends keyof Mutations>(key: K, payload?: Parameters<Mutations[K]>[1], options?: CommitOptions): ReturnType<Mutations[K]>;
} & {
  getters: {
    [K in keyof Getters]: ReturnType<Getters[K]>;
  };
} & {
  dispatch<K extends keyof Actions>(key: K, payload?: Parameters<Actions[K]>[1], options?: DispatchOptions): ReturnType<Actions[K]>;
};

export const store = createStore({
  modules: {
    authModule,
    homeModule
  },
  plugins: [createLogger()]
});

// Use this store to have Types
export function useStore() {
  return store as Store;
}
