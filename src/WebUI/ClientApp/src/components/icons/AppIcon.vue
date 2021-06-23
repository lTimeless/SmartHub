<template>
  <svg
    xmlns="http://www.w3.org/2000/svg"
    :class="`${height} ${width} ${iconColor}`"
    fill="none"
    stroke-width="2"
    stroke-linecap="round"
    stroke-linejoin="round"
    stroke="currentColor"
    viewBox="0 0 24 24"
    role="presentation"
  >
    <component :is="iconFullName" />
  </svg>
</template>

<script lang="ts">
import { computed, defineComponent, toRefs } from 'vue';
// TODO write all Icons as JSX and put them into one file , that would save a lot of files
export default defineComponent({
  name: 'AppIcon',
  props: {
    iconName: {
      type: String,
      required: true
    },
    iconColor: {
      type: String,
      required: false,
      default: 'text-gray-600'
    },
    height: {
      type: String,
      required: false,
      default: 'h-6'
    },
    width: {
      type: String,
      required: false,
      default: 'h-6'
    }
  },
  setup(props) {
    const iconFullName = computed(() => `${props.iconName}Icon`);
    return {
      iconFullName,
      ...toRefs(props)
    };
  }
});
</script>

<style scoped></style>
