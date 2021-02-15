<template>
  <div class="flex-1 mx-auto overflow-hidden rounded p-2">
    <div class="flex flex-col overflow-y-auto md:flex-row h-full">
      <slot />
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'AppCard'
});
</script>

<style scoped></style>
