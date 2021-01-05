<template>
  <div class="md:hidden flex flex-row justify-start items-center w-14 bg-gray-200 w-full rounded">
    <button class="p-2 absolute right-8 sm:right-10 top-2 z-20" type="button">
      <AppIcon width="h-6" height="h-6" icon-name="Menu" icon-color="text-gray-600" />
    </button>
  </div>
  <div class="hidden md:flex flex-col justify-between items-center flex-none md:w-14 bg-white rounded">
    <div class="flex flex-col space-y w-full items-center">
      <AppBrand class="my-3" />
      <div v-for="view in sidebarLists" :key="view.label">
        <NavigationItem
          v-if="roleIncluded(view.rolesRequired)"
          :icon-name="view.iconName"
          :label="view.label"
          :route="view.route"
        />
      </div>
    </div>
    <div class="flex flex-col space-y">
      <a class="block relative w-full h-16 md:w-16 md:flex justify-center items-center">
        <NavigationItem icon-name="Inbox" route="/inbox" class="cursor-not-allowed" />
        <div
          class="absolute top-0 right-0 mr-3 mt-3 bg-red-500 w-4 h-4 text-xs text-white rounded-full text-center"
        >
          5
        </div>
      </a>
      <UserDropdown />
    </div>
  </div>
</template>

<script lang="ts">
import UserDropdown from '../UserDropdown.vue';
import { computed, defineComponent, ref } from 'vue';
import { useRouter } from 'vue-router';
import { Roles, Routes } from '@/types/enums';
import NavigationItem from '@/components/ui/layout/AppSidebar/NavigationItem.vue';
import AppIcon from '@/components/icons/AppIcon.vue';
import AppBrand from '@/components/ui/layout/AppSidebar/AppBrand.vue';
import { useIdentity } from '@/hooks/useIdentity';

export default defineComponent({
  name: 'AppSidebar',
  components: {
    UserDropdown,
    NavigationItem,
    AppIcon,
    AppBrand
  },
  props: {},
  setup() {
    const router = useRouter();
    const { isRole } = useIdentity();
    const role = ref(isRole());
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

    return {
      currentPath,
      sidebarLists,
      roleIncluded,
      routes: Routes
    };
  }
});
</script>
<style lang="scss" scoped>
.sidebar {
  overflow: auto;
  @apply bg-background inset-x-0 bottom-0 w-full border-r border-border transition-all;
  transform: translateX(-100%);

  &.open {
    transform: translateX(0);
  }

  @screen md {
    @apply w-1/4 px-0 bg-transparent top-0 bottom-auto inset-x-auto sticky;
    transform: translateX(0);
  }
}
</style>
