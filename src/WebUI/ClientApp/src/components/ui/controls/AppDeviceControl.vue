<template>
  <component :is="typeFullName" :light-state="lightState" :handle-btn-click="handleBtnClick"></component>
</template>

<script lang="ts">
import { computed, defineComponent, PropType } from 'vue';
import { LightState } from '../../../types/types';

export default defineComponent({
  name: 'AppDeviceControl',
  props: {
    deviceTypeName: {
      type: String,
      required: true
    },
    lightState: {
      type: Object as PropType<LightState> | null,
      required: false,
      default: null
    }
  },
  setup(props) {
    const typeFullName = computed(() => props.deviceTypeName + 'Control');
    return {
      typeFullName
    };
  }
});
</script>
