<template>
  <nav
    class="md:left-0 md:block md:fixed md:top-0 md:bottom-0 md:overflow-y-auto md:flex-row md:flex-no-wrap md:overflow-hidden flex flex-wrap items-center justify-between md:w-64 z-10 py-4 px-6 bg-white"
  >
    <div
      class="md:flex-col md:items-stretch md:min-h-full md:flex-no-wrap px-0 flex flex-wrap items-center justify-between w-full mx-auto"
    >
      <!-- Toggler -->
      <button
        class="cursor-pointer text-black opacity-50 md:hidden px-3 py-1 text-xl leading-none bg-transparent rounded border border-solid border-transparent"
        type="button"
        @click="toggleCollapseShow('bg-white m-2 py-3 px-6')"
      >
        <i class="fas fa-bars"></i>
        X
      </button>
      <!-- Brand -->
      <router-link to="/" class="flex items-center text-primary" title="Home">
        <Logo :width="40" class="text-primary" />
        <span class="hidden ml-2 text-xl font-black tracking-tighter uppercase sm:block"> SmartHub </span>
      </router-link>
      <!-- User -->
      <ul class="md:hidden items-center flex flex-wrap list-none">
        <li class="inline-block relative mr-2">
          <NotificationDropdown />
        </li>
        <li class="inline-block relative">
          <UserDropdown />
        </li>
      </ul>
      <!-- Collapse -->
      <button
        tabindex="-1"
        v-if="collapseShowBool"
        @click="toggleCollapseShow('hidden')"
        class="fixed inset-0 h-full w-full bg-black opacity-20 cursor-default"
      ></button>
      <div
        class="md:flex md:flex-col md:items-stretch md:opacity-100 md:relative md:mt-4 md:shadow-none shadow absolute top-0 left-0 right-0 z-40 overflow-y-auto overflow-x-hidden h-auto items-center flex-1 rounded"
        :class="collapseShow"
      >
        <!-- Collapse header -->
        <div class="md:min-w-full md:hidden block pb-4 mb-4 border-b border-solid border-gray-300">
          <div class="flex flex-wrap">
            <div class="w-4/12 flex">
              <button
                type="button"
                class="cursor-pointer text-black opacity-50 md:hidden px-1 py-1 text-xl leading-none bg-transparent rounded border border-solid border-transparent"
                @click="toggleCollapseShow('hidden')"
              >
                X
              </button>
            </div>
            <div class="w-8/12">
              <router-link to="/" class="flex items-center text-primary" title="Home">
                <Logo :width="40" class="text-primary" />
                <span class="hidden ml-2 text-xl font-black tracking-tighter uppercase sm:block">
                  SmartHub
                </span>
              </router-link>
            </div>
          </div>
        </div>
        <!-- Form Search -->
        <form class="mt-6 mb-4 md:hidden">
          <div class="mb-3 pt-0">
            <label>
              <input
                type="text"
                placeholder="Search"
                class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
              />
            </label>
          </div>
        </form>
        <div v-for="view in sidebarLists" :key="view.label" class="mb-2">
          <NavigationItem
            v-if="roleIncluded(view.rolesRequired)"
            :icon-name="view.iconName"
            :label="view.label"
            :route="view.route"
          />
        </div>
      </div>
    </div>
  </nav>
</template>
<script lang="ts">
import NotificationDropdown from '../NotificationDropdown.vue';
import UserDropdown from '../UserDropdown.vue';
import { computed, defineComponent, onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';
import { getUserRoles } from '@/services/auth/authService';
import { Roles, Routes } from '@/types/enums';
import Logo from '@/components/shared/svgs/Logo.vue';
import NavigationItem from '@/components/layout/AppSidebar/NavigationItem.vue';

export default defineComponent({
  name: 'AppSidebar',
  components: {
    Logo,
    NotificationDropdown,
    UserDropdown,
    NavigationItem
  },
  props: {},
  setup() {
    const router = useRouter();
    const isRole = ref('');
    const collapseShow = ref<string>('hidden');
    const collapseShowBool = ref(false);
    const sidebarLists = [
      // Guest views
      {
        label: 'Dashboard',
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

    const getClassesForAnchor = (path: string) => ({
      'text-primary': router.currentRoute.value.path === path,
      'transition transform hover:translate-x-1 hover:text-primary': router.currentRoute.value.path !== path
    });

    const currentPath = computed(() => router.currentRoute.value.path);
    const roleIncluded = (rolesNeeded: string[]) => rolesNeeded.includes(isRole.value);

    onMounted(() => {
      isRole.value = getUserRoles();
    });
    const toggleCollapseShow = (classes: string) => {
      collapseShow.value = classes;
      collapseShowBool.value = !collapseShowBool.value;
    };

    return {
      currentPath,
      sidebarLists,
      roleIncluded,
      getClassesForAnchor,
      toggleCollapseShow,
      collapseShow,
      collapseShowBool
    };
  }
});
</script>
<style lang="scss" scoped>
.sidebar {
  overflow: auto;
  @apply fixed bg-background px-4 inset-x-0 bottom-0 w-full border-r border-border transition-all z-40;
  transform: translateX(-100%);

  &.open {
    transform: translateX(0);
  }

  @screen md {
    @apply w-1/4 px-0 bg-transparent top-0 bottom-auto inset-x-auto sticky z-0;
    transform: translateX(0);
  }
}
</style>
