<template>
  <div class="w-full">
    <h1 class="text-3xl text-gray-500 font-bold mb-6">Logs</h1>
    <div class="text-gray-600">Will come soon in a new style.</div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'Logs',
  components: {},
  setup() {
    const headers = [
      { text: 'Timestamp', value: 'timestamp' },
      { text: 'Level', value: 'level' },
      { text: 'Message', value: 'message' },
      { text: 'Exception', value: 'exception' }
    ];
    const searchKeys = ['timestamp', 'level', 'message', 'exception'];

    return {
      headers,
      searchKeys
    };
  }
});
</script>

<style scoped></style>
