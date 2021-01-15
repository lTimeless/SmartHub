<template>
  <div class="relative z-30 inline-block text-left cursor-pointer">
    <div
      class="flex flex-row items-center"
      :class="[onlyIcon ? 'w-12 justify-center' : ' w-48 justify-start']"
    >
      <div
        class="relative z-30 h-12 flex items-center hover:bg-charcoalBlue-200 rounded-l"
        :class="[
          isRoute ? `bg-primaryBlue ${onlyIcon ? 'bg-charcoalBlue-500' : ''}` : '',
          onlyIcon ? 'w-12 justify-center' : 'w-2/3 pl-4'
        ]"
        @click="onlyIcon ? setDropDownValue(!showDropdown) : handleRouteClick(routes.Me)"
      >
        <AppIcon icon-name="User" :icon-color="isRoute ? 'text-white' : 'text-primaryBlue'" />
        <div v-if="!onlyIcon">
          <div
            class="tracking-wide text-lg leading-loose"
            :class="[isRoute ? 'text-white' : 'text-primaryBlue', onlyIcon ? ' ' : ' pl-2']"
          >
            {{ onlyIcon ? ' ' : 'Me' }}
          </div>
        </div>
      </div>
      <div
        v-if="!onlyIcon"
        class="hover:bg-charcoalBlue-200 w-1/3 h-12 flex flex-row justify-center items-center"
        @click="logoutClick"
      >
        <AppIcon icon-name="Logout" />
      </div>
    </div>

    <!-- Button to close open modal by clicking outside -->
    <template v-if="onlyIcon">
      <button
        v-if="showDropdown"
        @click="setDropDownValue(false)"
        tabindex="-1"
        @keyup.esc="escapeDropdown"
        class="fixed inset-0 h-full w-full bg-black opacity-20 cursor-default"
      ></button>
      <!-- Modal -->
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
            @click="handleRouteClick(item.path)"
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
    }
  },
  setup(props) {
    const store = useStore();
    const { clearStorage } = useIdentity();
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
      store.dispatch(AppActionTypes.SET_USER_DROPDOWN, value);
    };

    const escapeDropdown = () => {
      setDropDownValue(false);
    };

    const handleRouteClick = async (path: string) => {
      await router.push(path).then(() => {
        setDropDownValue(false);
      });
    };

    const logoutClick = async () => {
      await clearStorage();
      await router.push(Routes.Login);
      await store.dispatch(AppActionTypes.SET_USER_DROPDOWN, false);
      await store.dispatch(AppActionTypes.RESET_SIDEBAR);
    };

    return {
      dropdownPopoverShow,
      showDropdown,
      dropDownList,
      ...useCurrentRoute(Routes.Me),
      escapeDropdown,
      handleRouteClick,
      logoutClick,
      setDropDownValue,
      ...toRefs(props),
      routes: Routes
    };
  }
});
</script>
