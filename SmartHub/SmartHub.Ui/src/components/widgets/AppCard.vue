<template>
  <div class="flex-1 h-full max-w-4xl mx-auto overflow-hidden rounded-lg shadow-xs dark:bg-gray-800">
    <div class="flex flex-col overflow-y-auto md:flex-row">
      <slot />
    </div>
  </div>
</template>

<script>
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'AppCard'
});
</script>

<style scoped></style>
