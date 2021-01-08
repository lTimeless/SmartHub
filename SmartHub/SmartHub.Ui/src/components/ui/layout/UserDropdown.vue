<template>
  <div class="relative z-30 inline-block text-left cursor-pointer">
    <div
      class="block relative z-30 h-14 w-14 flex justify-center items-center hover:bg-charcoalBlue-200 rounded-l"
      :class="[isCurrentRoute ? 'bg-primaryBlue' : '']"
      @click="setDropDownValue(!showDropdown)"
    >
      <AppIcon
        icon-name="User"
        :icon-color="isCurrentRoute ? 'text-white' : 'text-primaryBlue'"
        height="h-7"
        width="w-7"
      />
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
      class="absolute md:inset-x-0 md:bottom-2 md:left-16 origin-top-right right-0 mt-2 mr-2 md:mr-0 z-30 w-40 rounded border bg-white ring-1 ring-black ring-opacity-5"
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
import { useRoute, useRouter } from 'vue-router';
import { Routes } from '@/types/enums';
import { AppActionTypes } from '@/store/app/actions';
import AppIcon from '@/components/icons/AppIcon.vue';
import { useIdentity } from '@/hooks/useIdentity';

export default defineComponent({
  name: 'UserDropdown',
  components: {
    AppIcon
  },
  props: {},
  setup() {
    const store = useStore();
    const { clearStorage } = useIdentity();
    const router = useRouter();
    const dropdownPopoverShow = ref<boolean>(false);
    const showDropdown = computed(() => store.state.appModule.userDropDownOpen);
    const isCurrentRoute = computed(() => useRoute().fullPath.includes(Routes.Me));
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
      isCurrentRoute,
      escapeDropdown,
      dropDownBtnClick,
      logoutClick,
      setDropDownValue
    };
  }
});
</script>
