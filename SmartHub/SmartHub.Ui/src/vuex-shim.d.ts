import { Store } from '@/store';
import { RootState } from '@/store/index.types';

declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    $store: Store;
  }
}

declare module 'vuex' {
  export function useStore(key?: string): Store<RootState>;
}
