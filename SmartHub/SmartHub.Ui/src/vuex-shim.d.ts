import { ComponentCustomProperties } from 'vue';
import { Store } from '@/store';

declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    $store: Store;
  }
}
