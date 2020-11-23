<template>
  <div class="relative inline-block text-left">
    <div v-if="user !== null && user !== undefined">
      <div class="relative z-10 items-center flex cursor-pointer" @click="showDropdown = !showDropdown">
        <span
          class="w-12 h-12 text-sm text-white text-center text-xl bg-gray-300 inline-flex items-center justify-center rounded-full"
          :style="{ 'background-color': imageBgColor }"
        >
          {{ user.userName.charAt(0).toUpperCase() }}{{ user.userName.charAt(1).toUpperCase() }}
        </span>
      </div>
      <!-- Heroicon name: chevron-down -->
      <!--        <svg-->
      <!--          class="-mr-1 ml-2 h-5 w-5"-->
      <!--          xmlns="http://www.w3.org/2000/svg"-->
      <!--          viewBox="0 0 20 20"-->
      <!--          fill="currentColor"-->
      <!--          aria-hidden="true"-->
      <!--        >-->
      <!--          <path-->
      <!--            fill-rule="evenodd"-->
      <!--            d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z"-->
      <!--            clip-rule="evenodd"-->
      <!--          />-->
      <!--        </svg>-->
      <!--      </button>-->
    </div>
    <button
      v-if="showDropdown"
      @click="showDropdown = false"
      tabindex="-1"
      @keyup.esc="escapeDropdown"
      class="fixed inset-0 h-full w-full bg-black opacity-20 cursor-default"
    ></button>
    <div
      v-if="showDropdown"
      class="origin-top-right z-20 absolute right-0 mt-2 w-56 rounded-md shadow-lg bg-white ring-1 ring-black ring-opacity-5"
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
          @click="logout"
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

export default defineComponent({
  name: 'UserDropdown',
  props: {},
  setup() {
    const store = useStore();
    const router = useRouter();
    const userPath = '/user';
    const dropdownPopoverShow = ref<boolean>(false);
    const imageBgColor = `#${((Math.random() * 0xffffff) << 0).toString(16).padStart(6, '0')}`;
    const user = computed(() => store.state.authModule.Me);
    const showDropdown = ref(false);

    const dropDownList = [
      {
        name: 'Profile',
        path: Routes.User
      }
    ];

    const escapeDropdown = () => {
      console.log('esc');
      showDropdown.value = false;
    };

    const dropDownBtnClick = async (name: string) => {
      const item = dropDownList.find(x => x.name === name) ?? { path: Routes.NotFound }
      await router.push(item.path).then(() => {
        showDropdown.value = false;
      });
    };

    return {
      imageBgColor,
      dropdownPopoverShow,
      user,
      userPath,
      showDropdown,
      dropDownList,
      escapeDropdown,
      dropDownBtnClick,
      logout
    };
  }
});
</script>
