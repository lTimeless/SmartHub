<template>
  <div ref="sidebar" v-if="this.openSidebar" class="px-4 pt-6 lg:pt-6">
    <div
      v-for="(section, index) in this.sidebarLists.sections"
      :key="section.name"
      class="pb-4 mb-4 border-ui-border"
      :class="{ 'border-b': index < this.sidebarLists.sections.length - 1 }"
    >
      <h3 class="flex pt-0 mt-0 mb-1 font-bold text-sm tracking-tight uppercase border-none">
        {{ section.name }}
      </h3>

      <ul class="max-w-full pl-2 mb-0">
        <li
          v-for="page in section.items"
          :id="page.path"
          :key="page.path"
          :class="this.getClassesForAnchor(page)"
          class="hover:text-ui-primary"
        >
          <router-link :to="page.path" class="flex items-center py-1 ">
            <span
              class="absolute w-2 h-2 -ml-3 rounded-full opacity-0 bg-ui-primary transition transform scale-0 origin-center"
              :class="{
                'opacity-100 scale-100': this.currentPath === page.path
              }"
            ></span>
            {{ page.title }}
          </router-link>
        </li>
      </ul>
    </div>

    <div class="flex justify-center mt-7 mb-8">
      <button
        @click="logout"
        class="flex items-center text-ui-primary font-bold px-10 py-2 border border-ui-border rounded-lg
        hover:bg-ui-primary hover:text-white transition-colors"
      >
        Logout
        <!--        <ArrowRightCircleIcon class="ml-4" size="1x" />-->
      </button>
    </div>
  </div>
</template>

<script>
import { computed, defineComponent, onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';

export default defineComponent({
  name: 'Sidebar',
  components: {},
  props: {
    showSidebar: { type: Boolean, required: true }
  },
  setup(props) {
    const router = useRouter();
    const person = {
      userName: 'MaxTime',
      firstName: 'Max',
      lastName: 'ATestperson'
    };
    const imageBgColor = `#${((Math.random() * 0xffffff) << 0).toString(16).padStart(6, '0')}`;
    const isRole = ['Admin'];

    const sidebarLists = {
      sections: [
        {
          name: 'All',
          roleNeeded: ['Guest, User, Admin'],
          items: [{ title: 'Dashboard', icon: 'mdi-view-dashboard', path: '/' }]
        },
        {
          name: 'User',
          roleNeeded: ['Guest, User'],
          items: [
            { title: 'Plugins', icon: 'mdi-toy-brick', path: '/plugins' },
            { title: 'Routines', icon: 'mdi-update', path: '/routines' },
            { title: 'Statistics', icon: 'mdi-chart-arc', path: '/statistics' },
            { title: 'Settings', icon: 'mdi-cog', path: '/settings' }
          ]
        },
        {
          name: 'Admin',
          roleNeeded: ['Admin'],
          items: [
            { title: 'Activity', icon: 'mdi-calendar-alert', path: '/activity' },
            { title: 'Logs', icon: 'mdi-file-document', path: 'logs' },
            { title: 'System', icon: 'mdi-desktop-classic', path: '/system' },
            { title: 'Health', icon: 'mdi-clipboard-pulse', path: '/health' },
            { title: 'Manager', icon: 'mdi-monitor-edit', path: '/manager' }
          ]
        },
        {
          name: 'Help',
          roleNeeded: ['Guest, User, Admin'],
          items: [{ title: 'About', icon: 'mdi-information', path: '/about' }]
        }
      ]
    };

    const openSidebar = ref(props.showSidebar);

    const getClassesForAnchor = ({ path }) => ({
      'text-ui-primary': router.currentRoute.value.path === path,
      'transition transform hover:translate-x-1 hover:text-ui-primary': !router.currentRoute.value.path === path
    });

    const currentPath = computed(() => router.currentRoute.value.path);

    onMounted(() => {
      // const { isAdmin, isUser, isGuest } = getUserRole();
      // this.isAdmin = isAdmin;
      // this.isGuest = isGuest;
      // this.isUser = isUser;
    });

    const logout = () => {
      console.log('logout');
      // clearStorage();
      router.push('Login');
    };

    return {
      logout,
      currentPath,
      sidebarLists,
      isRole,
      imageBgColor,
      getClassesForAnchor,
      openSidebar
    };
  }
});
</script>

<style scoped></style>
