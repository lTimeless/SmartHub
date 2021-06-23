<template>
  <div
    v-if="ready"
    :class="`loader ease-linear rounded-full border-8 border-t-8 border-gray-200 ${height} ${width}`"
  />
</template>

<script lang="ts">
import { defineComponent, toRefs } from 'vue';
import { useTimeout } from '@vueuse/core';

export default defineComponent({
  name: 'AppSpinner',
  props: {
    height: {
      type: String,
      required: true
    },
    width: {
      type: String,
      required: true
    }
  },
  setup(props) {
    const { ready } = useTimeout(300, {controls: true});
    return {
      ...toRefs(props),
      ready
    };
  }
});
</script>

<style scoped lang="css">
.loader {
  border-top-color: #3498db;
  -webkit-animation: spinner 1.5s linear infinite;
  animation: spinner 1.5s linear infinite;
}

@-webkit-keyframes spinner {
  0% {
    -webkit-transform: rotate(0deg);
  }
  100% {
    -webkit-transform: rotate(360deg);
  }
}

@keyframes spinner {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}
</style>
