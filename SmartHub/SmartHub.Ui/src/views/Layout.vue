<template>
  <div class="md:flex md:flex-row md:h-screen background">
    <!-- Sidebar-->
    <AppSidebar />
    <!-- Main View-->
    <div class="flex flex-auto h-screen bg-gray-200 overflow-x-hidden background">
      <div class="flex flex-col w-full m-5 px-3 z-10">
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

<script lang="ts">
import { defineComponent } from 'vue';
import Navbar from '@/components/ui/layout/Navbar.vue';
import AppSidebar from '@/components/ui/layout/AppSidebar/AppSidebar.vue';

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
<style lang="scss" scoped></style>
