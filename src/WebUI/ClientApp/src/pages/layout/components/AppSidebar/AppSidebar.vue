<script lang="ts">
import { computed, defineComponent, ref, watch } from 'vue';
import { useRouter } from 'vue-router';
import { Routes } from '@/types/enums';
import NavigationItem from '@/pages/layout/components/AppSidebar/NavigationItem.vue';
import AppBrand from '@/components/app/AppBrand/AppBrand.vue';
import { useIdentity } from '@/hooks/useIdentity';
import { useStore } from 'vuex';
import { AppActionTypes } from '@/store/app/actions';
import AppIcon from '@/components/icons/AppIcon.vue';
import AppUserDropdown from '../AppUserDropdown.vue';
import MiniToggle from './MiniToggle.vue';
import { useSidebar } from './usesidebar';

export default defineComponent({
  name: 'AppSidebar',
  components: {
    AppUserDropdown,
    NavigationItem,
    AppBrand,
    AppIcon,
    MiniToggle
  },
  props: {},
  setup() {
    const router = useRouter();
    const store = useStore();
    const { isRole } = useIdentity();
    const { sidebarList } = useSidebar();
    const role = ref(isRole());
    const mobileSidebarOpen = computed(() => store.state.appModule.mobileSidebarOpen);
    const miniSidebarOpen = computed(() => store.state.appModule.miniSidebarOpen);

    const currentPath = computed(() => router.currentRoute.value.path);
    const roleIncluded = (rolesRequired: string[]) => rolesRequired.includes(role.value);

    const closeMobileSidebar = () => {
      if (mobileSidebarOpen.value) {
        store.dispatch(AppActionTypes.SET_MOBILE_SIDEBAR, false);
      }
    };
    watch(router.currentRoute, (oldV, newV) => {
      if (oldV !== newV) {
        closeMobileSidebar();
      }
    });

    return {
      currentPath,
      sidebarList,
      miniSidebarOpen,
      roleIncluded,
      routes: Routes,
      mobileSidebarOpen,
      closeMobileSidebar
    };
  }
});
</script>

<template>
  <!-- Sidebar -->
  <div
    :class="[miniSidebarOpen ? 'md:w-12' : 'w-52']"
    class="md:left-0 md:top-0 md:bottom-0 md:overflow-y-auto md:flex-no-wrap md:overflow-hidden bg-white flex flex-none z-40"
  >
    <!-- Background if MobileSidebar is open -->
    <button
      v-if="mobileSidebarOpen"
      class="absolute inset-0 h-full w-full bg-black opacity-20 cursor-default z-20"
      @click="closeMobileSidebar"
    />
    <div
      class="rounded md:flex flex-col justify-between md:relative absolute top-0 left-0 right-0 z-40 overflow-y-auto overflow-x-hidden"
      :class="[mobileSidebarOpen ? 'bg-white m-2 py-3 px-6' : 'hidden']"
    >
      <!-- Collapse header -->
      <div v-if="mobileSidebarOpen" class="mb-2">
        <div class="flex flex-wrap">
          <div class="w-2/12 flex items-center">
            <AppIcon icon-name="Close" class="cursor-pointer" @click="closeMobileSidebar" />
          </div>
          <div class="w-10/12 flex justify-center pl-2">
            <AppBrand />
          </div>
        </div>
      </div>
      <div class="flex flex-col space-y w-full items-center">
        <AppBrand
          v-if="!mobileSidebarOpen"
          class="my-4"
          :class="[miniSidebarOpen ? '' : 'pl-3']"
          :only-icon="miniSidebarOpen"
        />
        <div v-for="view in sidebarList" :key="view.label" :class="[mobileSidebarOpen ? 'w-full' : '']">
          <NavigationItem
            v-if="roleIncluded(view.rolesRequired) && view.active"
            :icon-name="view.iconName"
            :label="view.label"
            :route="view.route"
            :only-icon="miniSidebarOpen"
            :mobile-sidebar-open="mobileSidebarOpen"
          />
        </div>
      </div>
      <div class="flex flex-col space-y w-full items-center">
        <a
          class="relative h-12 md:flex justify-center items-center"
          :class="[miniSidebarOpen && !mobileSidebarOpen ? 'w-12' : ' w-full']"
        >
          <NavigationItem
            icon-name="Inbox"
            label="Inbox"
            route="/inbox"
            class="cursor-not-allowed"
            :only-icon="miniSidebarOpen"
            :mobile-sidebar-open="mobileSidebarOpen"
          />
        </a>
        <AppUserDropdown :only-icon="miniSidebarOpen" :mobile-sidebar-open="mobileSidebarOpen" />
        <MiniToggle :only-icon="miniSidebarOpen" />
      </div>
    </div>
  </div>
</template>
