<template>
  <div ref="sidebar" v-if="this.openSidebar" class="px-4 pt-6 lg:pt-6">
    <div class="pb-4 mb-1">
      <div class="md:flex">
        <div
          class="text-white text-center pt-2 text-xl lg:h-12 lg:w-12 md:h-10 md:w-10 sm:w-8 sm:h-8 rounded-full md:mx-0 md:mr-1
                xl:mr-6"
          :style="{ 'background-color': imageBgColor }"
        >
          {{ person.firstName.charAt(0) }}{{ person.lastName.charAt(0) }}
        </div>
        <div class="text-center md:text-left">
          <h2 class="text-lg">{{ person.userName }}</h2>
          <div class="text-gray-500">Logged in</div>
        </div>
      </div>
    </div>

    <div v-for="section in this.sidebarLists.sections" :key="section.name" class="pb-4 mb-1">
      <template v-if="roleIncluded(section.roleNeeded)">
        <div class="border-ui-border border-t mb-2"></div>
        <h3 class="flex pt-0 mt-0 mb-1 font-bold text-sm tracking-tight uppercase">
          {{ section.name }}
        </h3>

        <ul class="max-w-full pl-2 mb-0">
          <li
            v-for="page in section.items"
            :id="page.path"
            :key="page.path"
            :class="this.getClassesForAnchor(page.path)"
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
      </template>
    </div>

    <div class="flex justify-center mt-7 mb-8">
      <action-button color="indigo" :height="35" :width="150" title="Logout" :callback="logout" />
    </div>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';
import { clearStorage, getUserRole } from '@/services/auth/authService';
import ActionButton from '@/components/widgets/ActionButton.vue';
import { useStore } from '@/store';
import { A_LOGOUT } from '@/store/auth/actions';

export default defineComponent({
  name: 'Sidebar',
  components: {
    ActionButton
  },
  props: {
    showSidebar: { type: Boolean, required: true }
  },
  setup(props) {
    const router = useRouter();
    const store = useStore();
    const person = {
      userName: 'MaxTime',
      firstName: 'Max',
      lastName: 'ATestperson'
    };
    const imageBgColor = `#${((Math.random() * 0xffffff) << 0).toString(16).padStart(6, '0')}`;
    const isRole = ref('');

    const sidebarLists = {
      sections: [
        {
          name: 'All',
          roleNeeded: ['Guest', 'User', 'Admin'],
          items: [{ title: 'Dashboard', icon: 'mdi-view-dashboard', path: '/' }]
        },
        {
          name: 'User',
          roleNeeded: ['User', 'Admin'],
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
            { title: 'Events', icon: 'mdi-calendar-alert', path: '/events' },
            { title: 'Logs', icon: 'mdi-file-document', path: '/logs' },
            { title: 'System', icon: 'mdi-desktop-classic', path: '/system' },
            { title: 'Health', icon: 'mdi-clipboard-pulse', path: '/health' },
            { title: 'Manager', icon: 'mdi-monitor-edit', path: '/manager' }
          ]
        },
        {
          name: 'Help',
          roleNeeded: ['Guest', 'User', 'Admin'],
          items: [{ title: 'About', icon: 'mdi-information', path: '/about' }]
        }
      ]
    };

    const openSidebar = ref(props.showSidebar);

    const getClassesForAnchor = (path: string) => ({
      'text-ui-primary': router.currentRoute.value.path === path,
      'transition transform hover:translate-x-1 hover:text-ui-primary': router.currentRoute.value.path !== path
    });

    const currentPath = computed(() => router.currentRoute.value.path);
    const roleIncluded = (rolesNeeded: string[]) => rolesNeeded.includes(isRole.value);

    onMounted(() => {
      isRole.value = getUserRole();
    });

    const logout = () => {
      store.dispatch(A_LOGOUT);
      clearStorage();
      router.push('/login');
    };

    return {
      logout,
      currentPath,
      sidebarLists,
      roleIncluded,
      imageBgColor,
      getClassesForAnchor,
      openSidebar,
      person
    };
  }
});
</script>

<style scoped></style>
