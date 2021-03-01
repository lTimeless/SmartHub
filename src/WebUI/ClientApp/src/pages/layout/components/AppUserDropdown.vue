<template>
  <!-- Background if MobileSidebar is open -->
  <div v-if="showDropdown" class="fixed inset-0 h-full w-full bg-black opacity-20 cursor-default" />
  <!-- User Button and modal -->
  <div class="relative z-40 text-left cursor-pointer" :class="[mobileSidebarOpen ? 'w-full' : '']">
    <div
      class="flex flex-row items-center"
      :class="[
        onlyIcon && !mobileSidebarOpen
          ? 'w-12 justify-center'
          : mobileSidebarOpen
          ? 'w-full justify-start'
          : ' w-52 justify-start'
      ]"
    >
      <div
        class="relative z-40 h-12 flex items-center hover:bg-charcoalBlue-200"
        :class="[
          isRoute ? `bg-blueGray-300 ${onlyIcon ? 'bg-blueGray-300' : ''}` : '',
          onlyIcon && !mobileSidebarOpen ? 'w-12 justify-center' : 'w-2/3 pl-4'
        ]"
        @click="
          onlyIcon && !mobileSidebarOpen ? setDropDownValue(!showDropdown) : handleRouteClick(routes.Me)
        "
      >
        <AppIcon icon-name="User" :icon-color="'text-primaryBlue'" />
        <div v-if="!onlyIcon || mobileSidebarOpen">
          <div
            class="tracking-wide text-lg leading-loose text-primaryBlue"
            :class="[onlyIcon && !mobileSidebarOpen ? ' ' : ' pl-2']"
          >
            {{ onlyIcon && !mobileSidebarOpen ? ' ' : 'Me' }}
          </div>
        </div>
      </div>
      <div
        v-if="!onlyIcon || mobileSidebarOpen"
        class="hover:bg-charcoalBlue-200 w-1/3 h-12 flex flex-row justify-center items-center"
        @click="logout"
      >
        <AppIcon icon-name="Logout" />
      </div>
    </div>
    <template v-if="onlyIcon && !mobileSidebarOpen">
      <!-- Modal -->
      <div
        v-if="showDropdown"
        ref="modalTarget"
        class="fixed md:inset-x-0 md:bottom-2 md:left-16 origin-top-right right-0 mt-2 mr-2 md:mr-0 z-40 w-40 rounded border bg-white ring-1 ring-black ring-opacity-5"
        role="menu"
        aria-orientation="vertical"
        aria-labelledby="options-menu"
      >
        <div class="py-1">
          <a
            v-for="item in dropDownList"
            :key="item.name"
            class="block px-4 py-2 text-sm text-gray-600 hover:bg-gray-200 cursor-pointer active:bg-gray-100"
            role="menuitem"
            @click="handleRouteClick(item.path)"
          >
            {{ item.name }}
          </a>
        </div>
        <div class="border-t border-solid border-gray-400 mx-4" />
        <div class="py-1">
          <a
            class="block px-4 py-2 text-sm text-gray-600 hover:bg-gray-200 cursor-pointer active:bg-gray-100"
            role="menuitem"
            @click="logout"
          >
            Logout
          </a>
        </div>
      </div>
    </template>
  </div>
</template>
<script lang="ts">
import { computed, defineComponent, ref, toRefs } from 'vue';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';
import { Routes } from '@/types/enums';
import { AppActionTypes } from '@/store/app/actions';
import AppIcon from '@/components/icons/AppIcon.vue';
import { useIdentity } from '@/hooks/useIdentity';
import { useCurrentRoute } from '@/hooks/useCurrentRoute';
import { onClickOutside } from '@vueuse/core';

export default defineComponent({
  name: 'AppUserDropdown',
  components: {
    AppIcon
  },
  props: {
    onlyIcon: {
      type: Boolean,
      required: false,
      default: true
    },
    mobileSidebarOpen: {
      type: Boolean,
      required: false,
      default: false
    }
  },
  setup(props) {
    const store = useStore();
    const { logout } = useIdentity();
    const router = useRouter();
    const modalTarget = ref(null);
    const dropdownPopoverShow = ref<boolean>(false);
    const showDropdown = computed(() => store.state.appModule.userDropDownOpen);
    const dropDownList = [
      {
        name: 'Profile',
        path: Routes.Me
      }
    ];

    const setDropDownValue = (value: boolean) => {
      store.dispatch(AppActionTypes.SET_USER_DROPDOWN, value);
    };

    onClickOutside(modalTarget, () => {
      setDropDownValue(false);
    });

    const handleRouteClick = async (path: string) => {
      await router.push(path).then(() => {
        setDropDownValue(false);
      });
    };

    return {
      dropdownPopoverShow,
      showDropdown,
      dropDownList,
      modalTarget,
      ...useCurrentRoute(Routes.Me),
      handleRouteClick,
      logout,
      setDropDownValue,
      ...toRefs(props),
      routes: Routes
    };
  }
});
</script>
