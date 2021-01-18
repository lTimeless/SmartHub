<template>
  <div class="mobileSidebar w-48 h-full">
    <div class="flex flex-col space-y w-full items-center">
      <div v-for="view in sidebarLists" :key="view.label">
        <NavigationItem
          v-if="roleIncluded(view.rolesRequired)"
          :icon-name="view.iconName"
          :label="view.label"
          :route="view.route"
          :only-icon="false"
        />
      </div>
    </div>
    <div class="flex flex-col space-y w-full items-center">
      <a class="block relative h-12 md:flex justify-center items-center w-full">
        <NavigationItem
          icon-name="Inbox"
          label="Inbox"
          route="/inbox"
          class="cursor-not-allowed"
          :only-icon="false"
        />
        <div
          class="absolute top-0 mt-3 bg-red-500 left-0 ml-8 w-4 h-4 text-xs text-white rounded-full text-center"
        >
          5
        </div>
      </a>
      <NavigationItem icon-name="User" label="Me" :route="routes.Me" :only-icon="false" />
      <div
        class="hover:bg-charcoalBlue-200 pl-4 w-full h-12 flex flex-row justify-start items-center"
        @click="logout"
      >
        <AppIcon icon-name="Logout" />
        <div class="tracking-wide text-lg leading-loose pl-2">Logout</div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, ref } from 'vue';
import { useRouter } from 'vue-router';
import { Roles, Routes } from '@/types/enums';
import NavigationItem from '@/components/layout/AppSidebar/NavigationItem.vue';
import { useIdentity } from '@/hooks/useIdentity';
import AppIcon from '@/components/icons/AppIcon.vue';

export default defineComponent({
  name: 'AppMobileSidebar',
  components: {
    NavigationItem,
    AppIcon
  },
  props: {},
  setup() {
    const router = useRouter();
    const { isRole, logout } = useIdentity();
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
    ];
    const currentPath = computed(() => router.currentRoute.value.path);
    const roleIncluded = (rolesNeeded: string[]) => rolesNeeded.includes(role.value);

    return {
      currentPath,
      sidebarLists,
      roleIncluded,
      routes: Routes,
      logout
    };
  }
});
</script>
<style scoped>
.mobileSidebar {
  @apply absolute md:hidden flex flex-col justify-between items-center flex-none bg-white rounded;
}
</style>
