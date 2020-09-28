import { CommitOptions, DispatchOptions, Store as VuexStore } from 'vuex';

import { RootState } from '@/store/index.types';
import { AuthMutations } from '@/store/auth/mutations';
import { AuthGetters } from '@/store/auth/getters';
import { AuthActions } from '@/store/auth/actions';
import { HomeMutations } from '@/store/home/mutations';
import { HomeActions } from '@/store/home/actions';
import { HomeGetters } from '@/store/home/getters';
declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    $store: MyStore;
  }
}

// Store types
type Mutations = AuthMutations & HomeMutations;
type Actions = HomeActions & AuthActions;
type Getters = AuthGetters & HomeGetters;

// setup store type
export type MyStore = Omit<VuexStore<RootState>, 'commit' | 'getters' | 'dispatch'> & {
  commit<K extends keyof Mutations, P extends Parameters<Mutations[K]>[1]>(key: K, payload: P, options?: CommitOptions): ReturnType<Mutations[K]>;
} & {
  getters: {
    [K in keyof Getters]: ReturnType<Getters[K]>;
  };
} & {
  dispatch<K extends keyof Actions, P extends Parameters<Actions[K]>[1]>(key: K, payload?: P, options?: DispatchOptions): ReturnType<Actions[K]>;
};

declare module 'vuex' {
  export function useStore(key?: string): MyStore;
}
