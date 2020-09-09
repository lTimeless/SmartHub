<template>
  <div class="mx-auto bg-white rounded-lg p-10 flex items-center shadow justify-between">
    <div>
      <h2 class="text-2xl mb-4 text-gray-800 text-center font-bold">{{ title }}</h2>
      <slot />
      <button
        @click="callback"
        class="w-50 block mx-auto focus:outline-none py-2 px-5 rounded-lg shadow-sm text-center
                          text-gray-600 bg-white hover:bg-gray-100 font-medium border"
      >
        {{ buttonTitle }}
      </button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';

export default defineComponent({
  name: 'NotImplementedModal',
  props: {
    title: {
      type: String,
      required: true
    },
    buttonTitle: {
      type: String,
      required: true
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
