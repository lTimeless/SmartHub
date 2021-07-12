<template>
  <component :is="typeFullName" :state="state" :device-id="deviceId" />
</template>

<script lang="ts">
import { Maybe } from '@/graphql/graphql.types';
import { computed, defineComponent, PropType } from 'vue';

export default defineComponent({
  name: 'AppDeviceControl',
  props: {
    deviceTypeName: {
      type: String,
      required: true
    },
    state: {
      type: Object as PropType<
        | {
            __typename?: 'StatusResponseType' | undefined;
            lights?:
              | Maybe<{ __typename?: 'LightResponseType' | undefined; ison: boolean }>[]
              | null
              | undefined;
          }
        | null
        | undefined
        | null
        | undefined
      >,
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
