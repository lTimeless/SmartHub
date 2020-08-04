<template>
  <div class="flex-1 mb-4">
    <div class="relative">
      <label>
        <input
          type="search"
          v-model="searchInput"
          class="w-full pl-10 pr-4 py-2 rounded-lg shadow focus:outline-none focus:shadow-outline text-gray-600 font-medium"
          placeholder="Search... (press enter)"
          @keydown.enter="searchData"
          @focus="toggleTable(true)"
          @blur="toggleTable(false)"
        />
      </label>
      <div class="absolute top-0 left-0 inline-flex items-center p-2">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          class="w-6 h-6 text-gray-400"
          viewBox="0 0 24 24"
          stroke-width="2"
          stroke="currentColor"
          fill="none"
          stroke-linecap="round"
          stroke-linejoin="round"
        >
          <rect x="0" y="0" width="24" height="24" stroke="none"></rect>
          <circle cx="10" cy="10" r="7" />
          <line x1="21" y1="21" x2="15" y2="15" />
        </svg>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, computed, ref } from 'vue';
import Fuse from 'fuse.js';

export default defineComponent({
  name: 'Search',
  props: {
    data: {
      type: Array,
      required: true
    },
    searchKeys: {
      type: Array,
      required: true,
      validator: (prop: any) => prop.every((e: any) => typeof e === 'string')
    }
  },
  setup(props, context) {
    const searchInput = ref('');
    const searchData = () => {
      const fuse = new Fuse(props.data, {
        keys: props.searchKeys as string[],
        threshold: 0.25
      });
      const result = fuse.search(searchInput.value);
      context.emit('search-result', result);
    };

    const toggleTable = (value: boolean) => {
      context.emit('toggle-table', value);
    };

    return {
      searchInput,
      searchData,
      toggleTable
    };
  }
});
</script>

<style scoped></style>
