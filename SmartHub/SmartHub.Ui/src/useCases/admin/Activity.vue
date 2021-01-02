<template>
  <div class="w-full">
    <h1 class="text-3xl text-gray-500 font-bold mb-6">Activities</h1>
    <div class="text-gray-600">Will come soon in a new style.</div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';

export default defineComponent({
  name: 'Activity',
  components: {},
  setup() {
    const showSearchTable = ref(false);
    const headers = [
      { text: 'Date', value: 'dateTime' },
      { text: 'Requester', value: 'userName' },
      { text: 'Message', value: 'message' },
      { text: 'SuccessfulRequest', value: 'successfulRequest' },
      { text: 'ExecutionTime', value: 'executionTime' }
    ];
    const searchKeys = ['DateTime', 'Requester', 'SuccessfulRequest', 'ExecutionTime'];

    return {
      headers,
      searchKeys,
      showSearchTable
    };
  }
});
</script>

<style scoped></style>
