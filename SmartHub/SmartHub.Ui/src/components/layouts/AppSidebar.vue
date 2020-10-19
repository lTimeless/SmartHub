<template>
  <div ref="sidebar" v-if="this.openSidebar" class="lg:px-3 pt-6">
    <!-- User -->
    <div v-if="user !== null && user !== undefined" class="pb-4 mb-1">
      <router-link :to="userPath">
        <div class="lg:flex">
          <div
            class="text-white text-center pt-2 text-xl lg:h-12 lg:w-12 md:h-10 md:w-full rounded-full lg:mx-0 lg:mr-1 md:mr-0 xl:mr-1 shadow-md"
            :style="{ 'background-color': imageBgColor }"
          >
            {{ user.userName.charAt(0).toUpperCase() }}{{ user.userName.charAt(1).toUpperCase() }}
          </div>
          <div class="text-center md:text-center lg:text-left">
            <h2 class="text-lg">{{ user.userName }}</h2>
            <div class="text-gray-500 text-sm">Logged in</div>
          </div>
        </div>
      </router-link>
    </div>
    <div v-else class="pb-4 mb-1">
      <div class="lg:flex">
        <div class="text-center md:text-center lg:text-left">
          <h2 class="text-lg">Loading ...</h2>
        </div>
      </div>
    </div>
    <!-- Pages -->
    <div v-for="page in sidebarLists" :key="page.name" class="mb-2">
      <template v-if="roleIncluded(page.roleNeeded)">
        <div
          v-if="page.name === 'Events' || page.name === 'Dashboard'"
          class="border-ui-border border-t mb-2"
        ></div>
        <router-link :to="page.path">
          <h2
            class="flex items-center font-bold text-sm uppercase mt-4"
            :class="this.getClassesForAnchor(page.path)"
          >
            <span
              class="mr-2 w-2 h-2 rounded-full opacity-0 bg-ui-primary transition transform scale-1"
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
            class="hover:text-ui-primary"
          >
            <template v-if="roleIncluded(child.roleNeeded)">
              <router-link :to="child.path" class="flex items-center">
                <span
                  class="flex mr-2 w-2 h-2 rounded-full opacity-0 bg-ui-primary transition transform scale-0 origin-center"
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
    <!-- Logout button -->
    <div class="flex justify-center mt-12">
      <AppButton
        class="text-ui-primary shadow-sm"
        color="indigo"
        :height="35"
        :width="150"
        title="Logout"
        :callback="clickLogout"
      />
    </div>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, onMounted, ref, onBeforeMount } from 'vue';
import { useRouter } from 'vue-router';
import { getUserRoles, logout } from '@/services/auth/authService';
import AppButton from '@/components/widgets/AppButton.vue';
import { useStore } from 'vuex';
import { AuthActionTypes } from '@/store/auth/actions';
import { Roles } from '@/types/enums';

export default defineComponent({
  name: 'AppSidebar',
  components: {
    AppButton
  },
  props: {
    showSidebar: { type: Boolean, required: true }
  },
  setup(props) {
    const router = useRouter();
    const store = useStore();
    const userPath = '/user';
    const imageBgColor = `#${((Math.random() * 0xffffff) << 0).toString(16).padStart(6, '0')}`;
    const isRole = ref('');
    const openSidebar = ref(props.showSidebar);

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
      'text-ui-primary': router.currentRoute.value.path === path,
      'transition transform hover:translate-x-1 hover:text-ui-primary':
        router.currentRoute.value.path !== path
    });

    const currentPath = computed(() => router.currentRoute.value.path);
    const roleIncluded = (rolesNeeded: string[]) => rolesNeeded.includes(isRole.value);

    const user = computed(() => store.state.authModule.Me);

    onBeforeMount(async () => {
      await store.dispatch(AuthActionTypes.ME);
    });

    onMounted(() => {
      isRole.value = getUserRoles();
    });

    const clickLogout = () => {
      logout();
    };

    return {
      clickLogout,
      currentPath,
      sidebarLists,
      roleIncluded,
      imageBgColor,
      getClassesForAnchor,
      openSidebar,
      user,
      userPath
    };
  }
});
</script>

<style scoped></style>
