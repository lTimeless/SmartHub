<template>
  <div class="relative inline-block text-left">
    <!-- Three dots Menu -->
    <div class="relative items-center flex cursor-pointer" @click="toggleDropDownValue">
      <span
        class="w-7 h-7 p-1 text-black text-center hover:opacity-75 hover:bg-gray-300 inline-flex items-center justify-center rounded-full"
      >
        <AppIcon icon-name="DotsVertical" />
      </span>
    </div>
    <!-- Dropdown background invisible close btn  -->
    <button
      v-if="showDropdown"
      :tabindex="-1"
      class="fixed inset-0 h-full w-full opacity-0 cursor-default"
      @click="toggleDropDownValue"
    />
    <!-- Dropdown modal -->
    <div
      v-if="showDropdown"
      class="origin-top-right absolute right-0 mt-1 w-40 rounded-md shadow-lg bg-gray-200 ring-1 ring-black ring-opacity-5"
    >
      <div class="py-1">
        <a
          class="flex justify-start px-4 py-2 text-gray-700 hover:bg-gray-100 hover:text-gray-900 cursor-not-allowed active:bg-gray-100"
          role="menuitem"
        >
          <span class="text-sm">Add Device</span>
        </a>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { defineComponent, ref, reactive, toRefs } from 'vue';
import AppIcon from '@/components/icons/AppIcon.vue';

export default defineComponent({
  name: 'GroupDropdown',
  components: {
    AppIcon
  },
  props: {},
  setup(props) {
    const dropdownPopoverShow = ref<boolean>(false);
    const state = reactive({
      showDropdown: false
    });
    const toggleDropDownValue = () => {
      state.showDropdown = !state.showDropdown;
    };

    return {
      ...toRefs(state),
      ...toRefs(props),
      dropdownPopoverShow,
      toggleDropDownValue
    };
  }
});
</script>
