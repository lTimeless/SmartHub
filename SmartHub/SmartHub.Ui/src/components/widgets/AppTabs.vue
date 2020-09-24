<template>
  <div class="w-full">
    <!-- Tab header -->
    <slot name="header"></slot>
    <!-- Tab content -->
    <div class="mb-6 py-5">
      <slot name="content"></slot>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'AppTabs',
  setup() {
    return {};
  }
});
</script>

<style scoped></style>
