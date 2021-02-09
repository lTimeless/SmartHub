<template>
  <div v-if="lightState && lightState.lights">
    <button

      class="rounded-full p-2 focus:outline-none focus:ring focus:ring-primaryBlue focus:ring-opacity-50"
      :class="[
        lightState.lights[0].ison
          ? 'ring ring-green-500 hover:ring-burntSienna-600'
          : 'ring-2 ring-blueGray-300 hover:ring-green-600 hover:ring'
      ]"
    >
      <AppIcon icon-name="Bulb" :icon-color="'text-primaryBlue'" height="h-10" width="h-10"></AppIcon>
    </button>
  </div>
  <div v-else>No Light State available</div>
</template>

<script lang="ts">
import { defineComponent, PropType, toRefs } from 'vue';
import { LightState } from '../../../types/types';
import AppIcon from '../../icons/AppIcon.vue';

export default defineComponent({
  name: 'LightControl',
  props: {
    lightState: {
      type: Object as PropType<LightState>,
      required: false,
      default: null
    }
  },
  components: {
    AppIcon
  },
  setup(props) {
    return {
      ...toRefs(props)
    };
  }
});
</script>
