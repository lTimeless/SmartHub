<template>
  <div class="mx-auto bg-white rounded-lg p-10 flex items-center shadow justify-between">
    <div>
      <svg class="mb-4 h-20 w-20 text-green-500 mx-auto" viewBox="0 0 20 20" fill="currentColor">
        <path
          fill-rule="evenodd"
          d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-9.293a1 1 0 00-1.414-1.414L9 10.586 7.707 9.293a1 1
                    0 00-1.414 1.414l2 2a1 1 0 001.414 0l4-4z"
          clip-rule="evenodd"
        />
      </svg>
      <h2 class="text-2xl mb-4 text-gray-800 text-center font-bold">{{ title }}</h2>
      <slot />
      <button
        class="w-50 block mx-auto focus:outline-none py-2 px-5 rounded-lg shadow-sm text-center text-gray-600 bg-white hover:bg-gray-100 font-medium border"
        @click="callback"
      >
        {{ buttonTitle }}
      </button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';

export default defineComponent({
  name: 'ConfirmationModal',
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
