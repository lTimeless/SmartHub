import { App } from '@vue/runtime-core';
import LightControl from '../components/controls/LightControl.vue';

export default {
  install(app: App): void {
    app.component('LightControl', LightControl);
  }
};
