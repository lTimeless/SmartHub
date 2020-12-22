<template>
  <div class="w-full">
    <!-- Tab header -->
    <slot name="header"></slot>
    <!-- Tab content -->
    <div>
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
