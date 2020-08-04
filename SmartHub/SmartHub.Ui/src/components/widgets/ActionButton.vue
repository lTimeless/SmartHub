<template>
  <div class="flex justify-center mt-7 mb-8">
    <button
      @click="onClick"
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
import { defineComponent } from 'vue';

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
    }
  },
  setup(props) {
    const onClick = () => {
      console.log('action click', props.title);
    };
    return {
      ...props,
      onClick
    };
  }
});
</script>

<style scoped></style>
