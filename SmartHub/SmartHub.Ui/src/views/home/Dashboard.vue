<template>
  <div class="container">
    <h1 class="text-3xl text-gray-500 font-bold mb-6">Dashboard</h1>
    <div class="flex justify-between align-middle mx-0 my-4">
      <action-button title="Home"></action-button>
      <action-button title="Group" color="orange"></action-button>
      <action-button title="Device" color="green"></action-button>
      <action-button title="Routine" color="teal"></action-button>
    </div>
    <div class="grid grid-cols-2 gap-6">
      <div class="border-b border-ui-border col-span-2 mb-3">
        <h2 class="flex justify-start pt-0 mt-0 mb-1 font-bold text-sm tracking-tight uppercase text-gray-500">Graphs</h2>
      </div>
      <div class="border-b border-ui-border col-span-2">
        <h2 class="flex justify-start pt-0 mt-0 mb-1 font-bold text-sm tracking-tight uppercase text-gray-500">Tables</h2>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import ActionButton from '@/components/widgets/ActionButton.vue';

export default defineComponent({
  name: 'Dashboard',
  components: {
    ActionButton
  }
});
</script>

<style scoped></style>
