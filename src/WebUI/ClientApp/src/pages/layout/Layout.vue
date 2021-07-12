<script lang="ts">
import { defineComponent } from 'vue';
import Navbar from '@/pages/layout/components/AppNavbar.vue';
import AppSidebar from '@/pages/layout/components/AppSidebar/AppSidebar.vue';

export default defineComponent({
  name: 'Layout',
  components: {
    AppSidebar,
    Navbar
  },
  setup() {
    return {};
  }
});
</script>

<template>
  <div class="flex h-full overflow-hidden">
    <!-- Sidebar-->
    <AppSidebar />
    <!-- Main View-->
    <div class="background relative flex flex-auto h-screen w-full overflow-auto main">
      <div class="flex flex-col w-full m-2 md:m-5 md:px-3">
        <Navbar />
        <router-view v-slot="{ Component }">
          <transition
            mode="out-in"
            enter-active-class="transition duration-200 ease-out-in"
            leave-active-class="transition duration-200 ease-out-in"
            enter-from-class="opacity-0 transform translate-y-3"
            enter-to-class="opacity-100 translate-y-0"
            leave-from-class="opacity-100 translate-y-0"
            leave-to-class="opacity-0 translate-y-3"
          >
            <component :is="Component" />
          </transition>
        </router-view>
      </div>
    </div>
  </div>
</template>
<style scoped>
.main {
  -ms-overflow-style: none;
}
</style>
