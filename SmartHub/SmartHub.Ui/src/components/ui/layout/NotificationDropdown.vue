<template>
  <div class="relative inline-block text-left">
    <div class="relative z-20 items-center flex cursor-pointer" @click="setDropDownValue(!showDropdown)">
      <span
        class="w-10 h-10 text-sm text-gray-600 text-center inline-flex items-center justify-center rounded-full bg-white border hover:border-indigo-500"
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
            d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9"
          />
        </svg>
      </span>
    </div>
    <button
      v-if="showDropdown"
      @click="setDropDownValue(false)"
      tabindex="-1"
      @keyup.esc="escapeDropdown"
      class="fixed inset-0 h-full w-full bg-black opacity-20 cursor-default"
    ></button>
    <div
      v-if="showDropdown"
      class="origin-top-right z-20 absolute right-0 mt-2 w-56 rounded-md shadow-lg bg-white border ring-1 ring-black ring-opacity-5"
      role="menu"
      aria-orientation="vertical"
      aria-labelledby="options-menu"
    >
      <div class="py-1">
        <a
          v-for="item in dropDownList"
          :key="item.name"
          @click="dropDownBtnClick(item.name)"
          class="block px-4 py-2 text-sm text-gray-600 hover:bg-gray-200 cursor-pointer active:bg-gray-100"
          role="menuitem"
          >{{ item.name }}</a
        >
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { computed, defineComponent, ref } from 'vue';
import { useStore } from 'vuex';
import { AppActionTypes } from '@/store/app/actions';

export default defineComponent({
  name: 'NotificationDropdown',
  props: {},
  setup() {
    const store = useStore();
    const dropdownPopoverShow = ref<boolean>(false);
    const showDropdown = computed(() => store.state.appModule.notificationDropdownOpen);

    const dropDownList = [
      {
        name: 'Notification_1'
      },
      {
        name: 'Notification_2'
      }
    ];

    const setDropDownValue = (value: boolean) => {
      if (value) {
        store.dispatch(AppActionTypes.SET_USER_DROPDOWN, false);
      }
      store.dispatch(AppActionTypes.SET_NOTIFICATION_DROPDOWN, value);
    };

    const escapeDropdown = () => {
      setDropDownValue(false);
    };

    const dropDownBtnClick = async (name: string) => {
      console.log(name);
      // const item = dropDownList.find(x => x.name === name) ?? { path: Routes.NotFound }
      // await router.push(item.path).then(() => {
      //   showDropdown.value = false;
      // });
    };

    return {
      dropdownPopoverShow,
      showDropdown,
      dropDownList,
      escapeDropdown,
      dropDownBtnClick,
      setDropDownValue
    };
  }
});
</script>
