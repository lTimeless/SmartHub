<template>
  <div v-if="state && state.lights">
    <button
      class="rounded-full p-1 focus:outline-none"
      :class="[
        lightStatus
          ? 'ring ring-green-500 md:hover:ring-burntSienna-500'
          : 'ring-2 ring-blueGray-300 md:hover:ring-green-500 hover:ring'
      ]"
      @click="handleClick"
    >
      <AppIcon icon-name="Bulb" :icon-color="'text-primaryBlue'" height="h-10" width="h-10" />
    </button>
  </div>
  <div v-else class="text-gray-500 text-sm">No Light State available</div>
</template>

<script lang="ts">
import { defineComponent, PropType, ref, toRefs, onMounted, reactive } from 'vue';
import { LightState } from '@/types/types';
import AppIcon from '../icons/AppIcon.vue';
import { useSetLightStateQuery } from '@/graphql/queries/setLightState.generated';

export default defineComponent({
  name: 'LightControl',
  components: {
    AppIcon
  },
  props: {
    state: {
      type: Object as PropType<LightState>,
      required: false,
      default: null
    },
    deviceId: {
      type: String,
      required: true
    }
  },
  setup(props) {
    const propsRef = toRefs(props);
    const lightStatus = ref<boolean>();

    const variables = reactive({
      input: {
        deviceId: propsRef.deviceId,
        setLight: false
      }
    });

    onMounted(() => {
      // Set initial value
      if (propsRef.state.value?.lights && propsRef.state.value?.lights.length > 0) {
        lightStatus.value = propsRef.state.value.lights[0].ison;
      }
    });

    const { executeQuery } = useSetLightStateQuery({
      pause: true,
      variables,
      requestPolicy: 'network-only'
    });

    const handleClick = () => {
      variables.input.setLight = !lightStatus.value;
      executeQuery().then((x) => {
        lightStatus.value = x.data.value?.setLightState.lightResponseType?.ison;
      });
    };

    return {
      ...propsRef,
      lightStatus,
      handleClick
    };
  }
});
</script>
