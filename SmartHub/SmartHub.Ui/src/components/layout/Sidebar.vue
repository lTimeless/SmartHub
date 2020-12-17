<template>
  <nav
    class="md:left-0 md:block md:fixed md:top-0 md:bottom-0 md:overflow-y-auto md:flex-row md:flex-no-wrap md:overflow-hidden
    shadow-xl flex flex-wrap items-center justify-between md:w-64 z-10 py-4 px-6 glassmorphism"
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
                <i class="fas fa-times"></i>
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
        <div v-for="page in sidebarLists" :key="page.name" class="mb-2">
          <template v-if="roleIncluded(page.roleNeeded)">
            <router-link :to="page.path">
              <h2
                class="flex items-center font-bold text-sm uppercase mt-4 text-gray-600"
                :class="this.getClassesForAnchor(page.path)"
              >
                <span
                  class="mr-2 w-2 h-2 rounded-full opacity-0 bg-primary transition transform scale-1"
                  :class="{
                    'opacity-100 scale-100': this.currentPath === page.path
                  }"
                ></span>
                {{ page.name }}
              </h2>
            </router-link>

            <ul class="max-w-full pl-4 mb-0">
              <li
                v-for="child in page.children"
                :id="child.path"
                :key="child.path"
                :class="this.getClassesForAnchor(child.path)"
                class="hover:text-primary text-gray-600"
              >
                <template v-if="roleIncluded(child.roleNeeded)">
                  <router-link :to="child.path" class="flex items-center">
                    <span
                      class="flex mr-2 w-2 h-2 rounded-full opacity-0 bg-primary transition transform scale-0 origin-center"
                      :class="{
                        'opacity-100 scale-100': this.currentPath === child.path
                      }"
                    ></span>
                    {{ child.name }}
                  </router-link>
                </template>
              </li>
            </ul>
          </template>
        </div>
      </div>
    </div>
  </nav>
</template>
<script lang="ts">
import NotificationDropdown from './NotificationDropdown.vue';
import UserDropdown from './UserDropdown.vue';
import { computed, defineComponent, onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';
import { getUserRoles } from '@/services/auth/authService';
import { Roles } from '@/types/enums';
import Logo from '@/components/svgs/Logo.vue';

export default defineComponent({
  name: 'AppSidebar',
  components: {
    Logo,
    NotificationDropdown,
    UserDropdown
  },
  props: {},
  setup() {
    const router = useRouter();
    const isRole = ref('');
    const collapseShow = ref<string>('hidden');
    const collapseShowBool = ref(false);

    const sidebarLists = [
      {
        name: 'Dashboard',
        roleNeeded: [Roles.Guest, Roles.User, Roles.Admin],
        path: '/',
        icon: 'mdi-view-dashboard',
        children: []
      },
      {
        name: 'Statistics',
        roleNeeded: [Roles.User, Roles.Admin],
        path: '/statistics',
        icon: 'mdi-chart-arc',
        children: []
      },
      {
        name: 'Plugins',
        roleNeeded: [Roles.User, Roles.Admin],
        path: '/plugins',
        icon: 'mdi-chart-arc',
        children: []
      },
      {
        name: 'Configuration',
        roleNeeded: [Roles.User, Roles.Admin],
        path: '/settings',
        icon: 'mdi-cog',
        children: [
          {
            name: 'System',
            roleNeeded: [Roles.Admin],
            path: '/system',
            icon: 'mdi-desktop-classic',
            children: []
          },
          {
            name: 'Health',
            roleNeeded: [Roles.Admin],
            path: '/health',
            icon: 'mdi-clipboard-pulse',
            children: []
          }
        ]
      },
      {
        name: 'Activity',
        roleNeeded: [Roles.Admin],
        path: '/activity',
        icon: 'mdi-calendar-alert',
        children: []
      },
      {
        name: 'Logs',
        roleNeeded: [Roles.Admin],
        path: '/logs',
        icon: 'mdi-file-document',
        children: []
      },
      {
        name: 'Manager',
        roleNeeded: [Roles.Admin],
        path: '/manager',
        icon: 'mdi-monitor-edit',
        children: []
      }
      // TODO: move to toolbar
      // {
      //   name: 'About',
      //   roleNeeded: ['Guest', 'User', 'Admin'],
      //   path: '/about',
      //   children: [{ title: 'About', icon: 'mdi-information', path: '/about' }]
      // }
    ];

    const getClassesForAnchor = (path: string) => ({
      'text-primary': router.currentRoute.value.path === path,
      'transition transform hover:translate-x-1 hover:text-primary':
        router.currentRoute.value.path !== path
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
