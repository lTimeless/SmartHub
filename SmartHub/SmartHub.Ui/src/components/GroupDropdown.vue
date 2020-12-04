<template>
  <div class="relative inline-block text-left">
    <div class="relative items-center flex cursor-pointer" @click="setDropDownValue">
      <span
        class="w-7 h-7 p-1 text-black text-center hover:opacity-75 hover:bg-gray-300 inline-flex items-center justify-center rounded-full"
      >
        <svg
          class="h-6 w-6"
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M5 12h.01M12 12h.01M19 12h.01M6 12a1 1 0 11-2 0 1 1 0 012 0zm7 0a1 1 0 11-2 0 1 1 0 012 0zm7 0a1 1 0 11-2 0 1 1 0 012 0z"
          />
        </svg>
      </span>
    </div>
    <button
      v-if="showDropdown"
      @click="setDropDownValue"
      tabindex="-1"
      class="fixed inset-0 h-full w-full opacity-0 cursor-default"
    ></button>
    <div
      v-if="showDropdown"
      class="origin-top-right absolute right-0 mt-1 w-40 rounded-md shadow-lg bg-gray-200 ring-1 ring-black ring-opacity-5"
      role="menu"
      aria-orientation="vertical"
      aria-labelledby="options-menu"
    >
      <div class="py-1">
        <a
          @click="addSubGroup"
          class="flex justify-start px-4 py-2 text-gray-700 hover:bg-gray-100 hover:text-gray-900 cursor-pointer active:bg-gray-100"
          role="menuitem"
        >
          <svg
            class="h-5 w-5 mr-1"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M9 13h6m-3-3v6m-9 1V7a2 2 0 012-2h6l2 2h6a2 2 0 012 2v8a2 2 0 01-2 2H5a2 2 0 01-2-2z"
            />
          </svg>
          <span class="text-sm">Add Subgroup</span>
        </a>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { defineComponent, ref, reactive, toRefs, PropType, watch } from 'vue';

export default defineComponent({
  name: 'GroupDropdown',
  props: {
    addSubGroup: {
      type: Function as PropType<() => void>,
      required: true
    },
    closeDropDown: {
      type: Boolean
    }
  },
  setup(props) {
    const dropdownPopoverShow = ref<boolean>(false);
    const state = reactive({
      showDropdown: false
    });
    const { closeDropDown } = toRefs(props);
    watch(closeDropDown, (newValue) => {
      state.showDropdown = newValue;
    });
    const setDropDownValue = () => {
      state.showDropdown = !state.showDropdown;
    };

    return {
      ...toRefs(state),
      dropdownPopoverShow,
      setDropDownValue
    };
  }
});
</script>
