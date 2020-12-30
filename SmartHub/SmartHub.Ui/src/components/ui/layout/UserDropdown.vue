<template>
  <div class="relative z-30 inline-block text-left">
    <div class="relative z-30 items-center flex cursor-pointer" @click="setDropDownValue(!showDropdown)">
      <span
        class="w-10 h-10 text-sm text-gray-600 text-center inline-flex items-center justify-center rounded-full bg-white border hover:border-indigo-500"
      >
        <AppIcon icon-name="User" />
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
      class="origin-top-right z-30 absolute right-0 mt-2 w-56 rounded-md border bg-white ring-1 ring-black ring-opacity-5"
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
      <div class="border-t border-solid border-gray-400 mx-4" />
      <div class="py-1">
        <a
          @click="logoutClick"
          class="block px-4 py-2 text-sm text-gray-600 hover:bg-gray-200 cursor-pointer active:bg-gray-100"
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
import { clearStorage } from '@/services/auth/authService';
import { useRouter } from 'vue-router';
import { Routes } from '@/types/enums';
import { AppActionTypes } from '@/store/app/actions';
import AppIcon from '@/components/icons/AppIcon.vue';

export default defineComponent({
  name: 'UserDropdown',
  components: {
    AppIcon
  },
  props: {},
  setup() {
    const store = useStore();
    const router = useRouter();
    const dropdownPopoverShow = ref<boolean>(false);
    const showDropdown = computed(() => store.state.appModule.userDropDownOpen);

    const dropDownList = [
      {
        name: 'Profile',
        path: Routes.Me
      }
    ];

    const setDropDownValue = (value: boolean) => {
      if (value) {
        // store.dispatch(AppActionTypes.SET_NOTIFICATION_DROPDOWN, false);
      }
      store.dispatch(AppActionTypes.SET_USER_DROPDOWN, value);
    };

    const escapeDropdown = () => {
      setDropDownValue(false);
    };

    const dropDownBtnClick = async (name: string) => {
      const item = dropDownList.find((x) => x.name === name) ?? { path: Routes.NotFound };
      await router.push(item.path).then(() => {
        setDropDownValue(false);
      });
    };

    const logoutClick = async () => {
      await clearStorage();
      await router.push(Routes.Login);
      await store.dispatch(AppActionTypes.SET_NOTIFICATION_DROPDOWN, false);
      await store.dispatch(AppActionTypes.SET_USER_DROPDOWN, false);
    };

    return {
      dropdownPopoverShow,
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
