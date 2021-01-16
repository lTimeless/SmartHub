<script lang="ts">
import { computed, defineComponent, ref, watch } from 'vue';
import Navbar from '@/components/layout/AppNavbar.vue';
import AppSidebar from '@/components/layout/AppSidebar/AppSidebar.vue';
import AppMobileSidebar from '@/components/layout/AppSidebar/AppMobileSidebar.vue';
import { useStore } from 'vuex';

export default defineComponent({
  name: 'Layout',
  components: {
    AppSidebar,
    AppMobileSidebar,
    Navbar
  },
  setup() {
    const store = useStore();
    const hideAfterXSec = ref(false);
    const showMobileSidebar = computed(() => store.state.appModule.mobileSidebarOpen);

    watch(showMobileSidebar, (newV) => {
      if (!newV) {
        setTimeout(() => (hideAfterXSec.value = newV), 300);
      } else {
        hideAfterXSec.value = newV;
      }
    });

    return {
      showMobileSidebar,
      hideAfterXSec
    };
  }
});
</script>

<template>
  <div class="md:flex md:flex-row h-full background">
    <AppMobileSidebar v-if="hideAfterXSec" />
    <!-- Sidebar-->
    <AppSidebar v-if="!showMobileSidebar" />
    <!-- Main View-->
    <div
      class="absolute transition md:relative flex flex-auto h-screen bg-gray-200 w-full overflow-x-hidden background"
      :class="[showMobileSidebar ? 'transform translate-x-48' : '']"
    >
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
<style scoped>
.transition {
  transition: 0.3s transform cubic-bezier(0, 0.12, 0.14, 1);
}
</style>
