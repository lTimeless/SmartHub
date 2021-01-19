<template>
  <nav
    class="w-full md:left-0 md:top-0 md:bottom-0 md:overflow-y-auto md:flex-row md:flex-no-wrap md:overflow-hidden
    bg-transparent md:bg-white flex flex-wrap items-center justify-between md:w-64 z-30 py-4 px-6"
  >
    <div
      class="md:flex-col md:items-stretch md:min-h-full md:flex-no-wrap px-0 flex flex-wrap items-center justify-between w-full mx-auto"
    >
      <!-- Toggler -->
      <button
        class="cursor-pointer text-black opacity-50 md:hidden px-3 py-1 text-xl leading-none bg-transparent rounded border border-solid border-transparent z-30"
        type="button"
        @click="toggleCollapseShow"
      >
        X
      </button>
      <!-- Brand -->
      <router-link to="/" class="flex items-center text-ui-primary" title="Home">
        <span class="hidden ml-2 text-xl font-black tracking-tighter uppercase sm:block"> SmartHub </span>
      </router-link>
      <!-- Collapse -->
      <button
        tabindex="-1"
        v-if="collapseShowBool"
        @click="toggleCollapseShow"
        class="fixed inset-0 h-full w-full bg-black opacity-20 cursor-default"
      ></button>
      <div
        class="md:flex md:flex-col md:items-stretch md:opacity-100 md:relative md:mt-4 md:shadow-none absolute top-0 left-0 right-0
        z-40 overflow-y-auto overflow-x-hidden items-center flex-1 rounded"
        :class="[collapseShowBool ? 'bg-white m-2 py-3 px-6' : 'hidden']"
      >
        <!-- Collapse header -->
        <div class="md:min-w-full md:hidden block pb-4 mb-4 border-b border-solid border-gray-300">
          <div class="flex flex-wrap">
            <div class="w-4/12 flex">
              <button
                type="button"
                class="cursor-pointer text-black opacity-50 md:hidden px-1 py-1 text-xl leading-none bg-transparent rounded border border-solid border-transparent"
                @click="toggleCollapseShow"
              >
                X
              </button>
            </div>
            <div class="w-8/12">
              <router-link to="/" class="flex items-center text-ui-primary" title="Home">
                <AppBrand only-icon />
                <span class="hidden ml-2 text-xl font-black tracking-tighter uppercase sm:block">
                  SmartHub
                </span>
              </router-link>
            </div>
          </div>
        </div>
        <div v-for="page in sidebarLists" :key="page.label" class="mb-2">
          <template v-if="roleIncluded(page.rolesRequired)">
            <div
              v-if="page.label === 'Events' || page.label === 'Dashboard'"
              class="border-ui-border border-t mb-2"
            ></div>
            <router-link :to="page.route">
              <h2 class="flex items-center font-bold text-sm uppercase mt-4">
                <span
                  class="mr-2 w-2 h-2 rounded-full opacity-0 bg-ui-primary transition transform scale-1"
                  :class="{
                    'opacity-100 scale-100': currentPath === page.route
                  }"
                ></span>
                {{ page.label }}
              </h2>
            </router-link>
          </template>
        </div>
      </div>
    </div>
  </nav>
  <!--  <div class="sidebar" :class="[miniOpen ? 'md:w-12' : 'w-48']">-->
  <!--    <div class="flex flex-col space-y w-full items-center">-->
  <!--      <AppBrand class="my-3" :only-icon="onlyIcon" />-->
  <!--      <div v-for="view in sidebarLists" :key="view.label">-->
  <!--        <NavigationItem-->
  <!--          v-if="roleIncluded(view.rolesRequired)"-->
  <!--          :icon-name="view.iconName"-->
  <!--          :label="view.label"-->
  <!--          :route="view.route"-->
  <!--          :only-icon="onlyIcon"-->
  <!--        />-->
  <!--      </div>-->
  <!--    </div>-->
  <!--    <div class="flex flex-col space-y w-full items-center">-->
  <!--      <a-->
  <!--        class="block relative h-12 md:flex justify-center items-center"-->
  <!--        :class="[onlyIcon ? 'w-12' : ' w-full']"-->
  <!--      >-->
  <!--        <NavigationItem-->
  <!--          icon-name="Inbox"-->
  <!--          label="Inbox"-->
  <!--          route="/inbox"-->
  <!--          class="cursor-not-allowed"-->
  <!--          :only-icon="onlyIcon"-->
  <!--        />-->
  <!--        <div-->
  <!--          class="absolute top-0 mt-3 bg-red-500 w-4 h-4 text-xs text-white rounded-full text-center"-->
  <!--          :class="[onlyIcon ? 'right-0 mr-3 ' : ' left-0 ml-8']"-->
  <!--        >-->
  <!--          5-->
  <!--        </div>-->
  <!--      </a>-->
  <!--      <AppUserDropdown :only-icon="onlyIcon" />-->
  <!--      <MiniToggle :only-icon="onlyIcon" />-->
  <!--    </div>-->
  <!--  </div>-->
</template>

<script lang="ts">
import AppUserDropdown from '../AppUserDropdown.vue';
import { computed, defineComponent, ref, watch } from 'vue';
import { useRouter } from 'vue-router';
import { Roles, Routes } from '@/types/enums';
import NavigationItem from '@/components/layout/AppSidebar/NavigationItem.vue';
import AppBrand from '@/components/layout/AppSidebar/AppBrand.vue';
import { useIdentity } from '@/hooks/useIdentity';
import MiniToggle from './MiniToggle.vue';
import { useStore } from 'vuex';

export default defineComponent({
  name: 'AppSidebar',
  components: {
    // AppUserDropdown,
    // NavigationItem,
    AppBrand
    // MiniToggle
  },
  props: {},
  setup() {
    const router = useRouter();
    const store = useStore();
    const { isRole } = useIdentity();
    const role = ref(isRole());
    const onlyIcon = ref(true);
    const collapseShowBool = ref(false);
    const miniOpen = computed(() => store.state.appModule.miniSidebarOpen);
    const sidebarLists = [
      // Guest views
      {
        label: 'Home',
        rolesRequired: [Roles.Guest, Roles.User, Roles.Admin],
        route: Routes.Home,
        iconName: 'Home',
        children: []
      },
      // User views
      {
        label: 'Groups',
        rolesRequired: [Roles.User, Roles.Admin],
        route: Routes.Groups,
        iconName: 'Folder',
        children: []
      },
      {
        label: 'Devices',
        rolesRequired: [Roles.User, Roles.Admin],
        route: Routes.Devices,
        iconName: 'Server',
        children: []
      },
      {
        label: 'Users',
        rolesRequired: [Roles.User, Roles.Admin],
        route: Routes.Users,
        iconName: 'Users',
        children: []
      },
      {
        label: 'Automations',
        rolesRequired: [Roles.User, Roles.Admin],
        route: Routes.Automations,
        iconName: 'Repeat',
        children: []
      },
      {
        label: 'Statistics',
        rolesRequired: [Roles.User, Roles.Admin],
        route: Routes.Statistics,
        iconName: 'Pie-chart',
        children: []
      },
      {
        label: 'Plugins',
        rolesRequired: [Roles.User, Roles.Admin],
        route: Routes.Plugins,
        iconName: 'Plugin',
        children: []
      },
      {
        label: 'Configuration',
        rolesRequired: [Roles.User, Roles.Admin],
        route: Routes.Configuration,
        iconName: 'Settings',
        children: []
      }
      // Admin views
      // TODO werden zu tabs oder mini-sidebar in der config view von der Admin view
      // {
      //   name: 'System',
      //   roleNeeded: [Roles.Admin],
      //   path: Routes.System,
      //   icon: 'mdi-desktop-classic',
      //   children: []
      // },
      // {
      //   name: 'Health',
      //   roleNeeded: [Roles.Admin],
      //   path: Routes.Health,
      //   icon: 'mdi-clipboard-pulse',
      //   children: []
      // }
      // {
      //   name: 'Activity',
      //   roleNeeded: [Roles.Admin],
      //   path: Routes.Activity,
      //   icon: 'mdi-calendar-alert',
      //   children: []
      // },
      // {
      //   name: 'Logs',
      //   roleNeeded: [Roles.Admin],
      //   path: Routes.Logs,
      //   icon: 'mdi-file-document',
      //   children: []
      // }
      // TODO: move to toolbar or help icon on the bottom of sidebar
      // {
      //   name: 'About',
      //   roleNeeded: ['Guest', 'User', 'Admin'],
      //   path: '/about',
      //   children: [{ title: 'About', icon: 'mdi-information', path: '/about' }]
      // }
    ];
    const currentPath = computed(() => router.currentRoute.value.path);
    const roleIncluded = (rolesNeeded: string[]) => rolesNeeded.includes(role.value);
    watch(
      miniOpen,
      (newV) => {
        onlyIcon.value = newV;
      },
      { immediate: true }
    );

    const toggleCollapseShow = () => {
      collapseShowBool.value = !collapseShowBool.value;
    };

    return {
      currentPath,
      sidebarLists,
      miniOpen,
      onlyIcon,
      roleIncluded,
      routes: Routes,
      collapseShowBool,
      toggleCollapseShow
    };
  }
});
</script>
<style lang="scss" scoped>
.sidebar {
  @apply hidden md:flex flex-col justify-between items-center flex-none bg-white rounded;
  //backdrop-filter: blur(10px);
  //background: rgba(255, 255, 255, 0.25);
}
.transition {
  transition: 0.3s transform cubic-bezier(0, 0.12, 0.14, 1);
}
</style>
