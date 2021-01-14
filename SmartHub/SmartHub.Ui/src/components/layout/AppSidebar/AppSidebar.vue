<template>
  <div class="sidebar" :class="[miniOpen ? 'md:w-14' : 'w-48']">
    <div class="flex flex-col space-y w-full items-center">
      <AppBrand class="my-3" :only-icon="onlyIcon" />
      <div v-for="view in sidebarLists" :key="view.label">
        <NavigationItem
          v-if="roleIncluded(view.rolesRequired)"
          :icon-name="view.iconName"
          :label="view.label"
          :route="view.route"
          :only-icon="onlyIcon"
        />
      </div>
    </div>
    <div class="flex flex-col space-y w-full items-center">
      <a
        class="block relative h-14 md:flex justify-center items-center"
        :class="[onlyIcon ? 'w-14' : ' w-full']"
      >
        <NavigationItem
          icon-name="Inbox"
          label="Inbox"
          route="/inbox"
          class="cursor-not-allowed"
          :only-icon="onlyIcon"
        />
        <div
          class="absolute top-0 mt-3 bg-red-500 w-4 h-4 text-xs text-white rounded-full text-center"
          :class="[onlyIcon ? 'right-0 mr-3 ' : ' left-0 ml-8']"
        >
          5
        </div>
      </a>
      <AppUserDropdown :only-icon="onlyIcon" />
      <MiniToggle :only-icon="onlyIcon" />
    </div>
  </div>
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
    AppUserDropdown,
    NavigationItem,
    AppBrand,
    MiniToggle
  },
  props: {},
  setup() {
    const router = useRouter();
    const store = useStore();
    const { isRole } = useIdentity();
    const role = ref(isRole());
    const onlyIcon = ref(true);
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
        console.log(onlyIcon.value);
      },
      { immediate: true }
    );

    return {
      currentPath,
      sidebarLists,
      miniOpen,
      onlyIcon,
      roleIncluded,
      routes: Routes
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
</style>
