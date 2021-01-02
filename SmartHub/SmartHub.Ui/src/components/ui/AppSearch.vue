<template>
  <div class="flex-1" :class="`w-${width}`">
    <div class="relative">
      <label>
        <input
          type="search"
          v-model="searchInput"
          class="w-full pl-10 pr-4 py-2 rounded-lg shadow focus:outline-none focus:shadow-outline text-gray-600 font-medium"
          :class="`h-${height}`"
          placeholder="Search... (press enter)"
          @keydown.enter="searchData"
          @focus="toggleTable(true)"
          @blur="toggleTable(false)"
        />
      </label>
      <div class="absolute top-0 left-0 inline-flex items-center" :class="`p-${getIconPadding}`">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          class="text-gray-400"
          :class="`w-${getIconSize} h-${getIconSize}`"
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
import { defineComponent, ref, computed } from 'vue';
import Fuse from 'fuse.js';

export default defineComponent({
  name: 'AppSearch',
  props: {
    data: {
      type: Array,
      required: true
    },
    searchKeys: {
      type: Array,
      required: true,
      validator: (prop: any) => prop.every((e: unknown) => typeof e === 'string')
    },
    withDropdown: {
      type: Boolean,
      required: false
    },
    width: {
      type: String,
      required: false,
      default: 'full'
    },
    height: {
      type: String,
      required: false,
      default: '10'
    },
    iconSize: {
      type: String,
      required: false,
      default: '6'
    }
  },
  emits: ['toggle-table', 'search-result'],
  setup(props, context) {
    const searchInput = ref('');
    const iconPadding = ref(2);
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

    const getIconSize = computed(() => {
      if (typeof props.iconSize === 'string') {
        return props.iconSize;
      }
      return props.iconSize - 2;
    });

    const getIconPadding = computed(() => iconPadding.value);

    return {
      searchInput,
      searchData,
      toggleTable,
      getIconSize,
      getIconPadding
    };
  }
});
</script>

<style scoped></style>
