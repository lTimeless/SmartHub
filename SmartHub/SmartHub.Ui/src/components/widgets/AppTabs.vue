<template>
  <div class="flex flex-wrap">
    <div class="w-full">
      <!-- Tab header -->
      <slot name="header"></slot>
      <!-- Tab content -->
      <div class="relative flex flex-col min-w-0 break-words w-full mb-6">
        <div class="py-5 flex-auto">
          <div class="tab-content tab-space">
            <slot name="content"></slot>
          </div>
        </div>
      </div>
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
