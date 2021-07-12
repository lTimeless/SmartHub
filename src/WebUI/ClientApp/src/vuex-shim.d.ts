import { CommitOptions, DispatchOptions, Store as VuexStore } from 'vuex';
import { RootState } from '@/store/index.types';
import { AppMutations } from '@/store/app/mutations';
import { HomeActions } from '@/store/app/actions';
import { HomeGetters } from '@/store/app/getters';
declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    $store: MyStore;
  }
}

// Store types
type Mutations = AppMutations;
type Actions = HomeActions;
type Getters = HomeGetters;

// setup store type
export type MyStore = Omit<VuexStore<RootState>, 'commit' | 'getters' | 'dispatch'> & {
  commit<K extends keyof Mutations, P extends Parameters<Mutations[K]>[1]>(
    key: K,
    payload: P,
    options?: CommitOptions
  ): ReturnType<Mutations[K]>;
} & {
  getters: {
    [K in keyof Getters]: ReturnType<Getters[K]>;
  };
} & {
  dispatch<K extends keyof Actions, P extends Parameters<Actions[K]>[1]>(
    key: K,
    payload?: P,
    options?: DispatchOptions
  ): ReturnType<Actions[K]>;
};

declare module 'vuex' {
  export function useStore(key?: string): MyStore;
}
