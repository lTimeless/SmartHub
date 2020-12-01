<template>
  <div class="relative inline-block text-left">
    <div v-if="user !== null && user !== undefined">
      <div class="relative z-20 items-center flex cursor-pointer" @click="setDropDownValue(!showDropdown)">
        <span
          class="w-10 h-10 text-sm md:text-white sm:text-black shadow-lg hover:opacity-75 text-center inline-flex items-center justify-center rounded-full"
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
              d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z"
            />
          </svg>
        </span>
      </div>
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
      class="origin-top-right z-30 absolute right-0 mt-2 w-56 rounded-md shadow-lg bg-white ring-1 ring-black ring-opacity-5"
      role="menu"
      aria-orientation="vertical"
      aria-labelledby="options-menu"
    >
      <div class="py-1">
        <a
          v-for="item in dropDownList"
          :key="item.name"
          @click="dropDownBtnClick(item.name)"
          class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 hover:text-gray-900 cursor-pointer active:bg-gray-100"
          role="menuitem"
          >{{ item.name }}</a
        >
      </div>
      <div class="border-t border-solid border-gray-300 mx-4" />
      <div class="py-1">
        <a
          @click="logoutClick"
          class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 hover:text-gray-900 cursor-pointer active:bg-gray-100"
          role="menuitem"
          >Logout</a
        >
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { computed, defineComponent, ref } from 'vue';
import { useStore } from 'vuex';
import { logout } from '@/services/auth/authService';
import { useRouter } from 'vue-router';
import { Routes } from '@/types/enums';
import { AppMutationTypes } from '@/store/app/mutations';
import { AppActionTypes } from '@/store/app/actions';

export default defineComponent({
  name: 'UserDropdown',
  props: {},
  setup() {
    const store = useStore();
    const router = useRouter();
    const userPath = '/user';
    const dropdownPopoverShow = ref<boolean>(false);
    const user = computed(() => store.state.authModule.Me);
    const showDropdown = computed(() => store.state.appModule.userDropDownOpen);

    const dropDownList = [
      {
        name: 'Profile',
        path: Routes.User
      }
    ];

    const setDropDownValue = (value: boolean) => {
      if (value) {
        store.dispatch(AppActionTypes.SET_NOTIFICATION_DROPDOWN, false);
      }
      store.dispatch(AppActionTypes.SET_USER_DROPDOWN, value);
    };

    const escapeDropdown = () => {
      setDropDownValue(false);
    };

    const dropDownBtnClick = async (name: string) => {
      const item = dropDownList.find((x) => x.name === name) ?? { path: Routes.NotFound };
      await router.push(item.path).then(() => {
        // showDropdown.value = false;
        setDropDownValue(false);
      });
    };

    const logoutClick = () => {
      logout();
      store.dispatch(AppActionTypes.SET_NOTIFICATION_DROPDOWN, false);
      store.dispatch(AppActionTypes.SET_USER_DROPDOWN, false);
    };

    return {
      dropdownPopoverShow,
      user,
      userPath,
      showDropdown,
      dropDownList,
      escapeDropdown,
      dropDownBtnClick,
      logoutClick,
      setDropDownValue
    };
  }
});
</script>
