<template>
  <div class="flex justify-center">
    <button
      @click="callback"
      class="flex justify-center items-center text-ui-primary font-bold border border-ui-border rounded-lg
        hover:text-white transition-colors"
      :class="`hover:bg-${color}-${colorSaturation}`"
      :style="{ height: height + 'px', width: width + 'px' }"
    >
      {{ title }}
    </button>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';

export default defineComponent({
  name: 'ActionButton',
  components: {},
  props: {
    title: {
      type: String,
      required: true
    },
    color: {
      type: String,
      required: false,
      default: 'indigo'
    },
    colorSaturation: {
      type: String,
      required: false,
      default: '400'
    },
    width: {
      type: Number,
      required: false,
      default: 270
    },
    height: {
      type: Number,
      required: false,
      default: 50
    },
    callback: {
      type: Function as PropType<() => void>,
      required: true
    }
  },
  setup(props) {
    return {
      ...props
    };
  }
});
</script>

<style scoped></style>
