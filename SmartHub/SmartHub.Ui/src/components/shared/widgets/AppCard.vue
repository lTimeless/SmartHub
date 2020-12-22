<template>
  <div class="relative flex-1 h-full max-w-4xl mx-auto overflow-hidden rounded">
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
