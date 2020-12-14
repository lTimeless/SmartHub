<template>
  <div
    :class="`loader ease-linear rounded-full border-8 border-t-8 border-gray-200 ${height} ${width}`"
  ></div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'Loader',
  props: {
    height: {
      type: String,
      required: true
    },
    width: {
      type: String,
      required: true
    }
  }
});
</script>

<style scoped lang="scss">
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
