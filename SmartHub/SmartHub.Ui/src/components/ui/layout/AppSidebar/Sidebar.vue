<template>
  <nav
    class="relative md:left-0 md:block md:fixed md:top-0 md:bottom-0 md:overflow-y-auto md:flex-row md:flex-no-wrap md:overflow-hidden flex flex-wrap items-center justify-between md:w-64 z-10 py-2 md:py-4 px-6 md:bg-white bg-gray-100"
  >
    <div
      class="md:flex-col md:items-stretch md:min-h-full md:flex-no-wrap flex flex-wrap items-center justify-between w-full mx-auto"
    >
      <!-- Toggle btn -->
      <button
        class="cursor-pointer text-black opacity-50 md:hidden px-3 py-1 text-xl leading-none bg-transparent rounded border border-solid border-transparent"
        type="button"
        @click="toggleCollapseShow('bg-gray-100 m-2 py-3 px-6')"
      >
        <AppIcon width="h-6" height="h-6" icon-name="Menu" icon-color="text-gray-600" />
      </button>
      <!-- Brand -->
      <AppBrand />
      <!-- Dropdowns -->
      <div class="md:hidden items-center flex flex-wrap list-none">
        <!-- <NotificationDropdown />-->
        <UserDropdown />
      </div>
      <!-- Collapse btn, dark bg -->
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
        <div class="md:min-w-full md:hidden block mb-2">
          <div class="flex flex-wrap">
            <div class="flex">
              <button
                type="button"
                class="cursor-pointer text-black opacity-50 md:hidden bg-transparent rounded border border-solid border-transparent"
                @click="toggleCollapseShow('hidden')"
              >
                <AppIcon width="h-6" height="h-6" icon-name="Close" icon-color="text-gray-600" />
              </button>
            </div>
          </div>
        </div>
        <!-- Form Search -->
        <form class="mb-4 md:hidden px-6">
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
        <!-- Sidebar List -->
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
import UserDropdown from '../UserDropdown.vue';
import { computed, defineComponent, onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';
import { getUserRoles } from '@/services/auth/authService';
import { Roles, Routes } from '@/types/enums';
import NavigationItem from '@/components/ui/layout/AppSidebar/NavigationItem.vue';
import AppIcon from '@/components/icons/AppIcon.vue';
import AppBrand from '@/components/ui/layout/AppSidebar/AppBrand.vue';

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
    const isRole = ref('');
    const collapseShow = ref<string>('hidden');
    const collapseShowBool = ref(false);
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
      collapseShowBool,
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
