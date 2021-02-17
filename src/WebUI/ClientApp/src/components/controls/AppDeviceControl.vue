<template>
  <component :is="typeFullName" :state="state" :device-id="deviceId"></component>
</template>

<script lang="ts">
import { computed, defineComponent } from 'vue';

export default defineComponent({
  name: 'AppDeviceControl',
  props: {
    deviceTypeName: {
      type: String,
      required: true
    },
    state: {
      type: Object,
      required: false,
      default: null
    },
    deviceId: {
      type: String,
      required: true
    }
  },
  setup(props) {
    const typeFullName = computed(() => `${props.deviceTypeName}Control`);
    return {
      typeFullName
    };
  }
});
</script>
