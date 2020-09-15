<template>
  <div class="font-sans antialiased text-ui-typo bg-ui-background">
    <div class="flex flex-col justify-start min-h-screen">
      <header ref="headerRef" class="sticky top-0 z-10 w-full border-b bg-ui-background border-ui-border" @resize="setHeaderHeight">
        <LayoutHeader />
      </header>

      <main class="relative flex flex-wrap justify-start flex-1 w-full bg-ui-background overflow-auto">
        <aside v-if="hasSidebar" class="px-4 sidebar overflow-auto w-56 bg-ui-background" :class="{ open: sidebarOpen }" :style="sidebarStyle">
          <Sidebar :show-sidebar="this.sidebarOpen" />
        </aside>

        <div class="container w-10/12 pb-6 flex justify-around">
          <router-view />
        </div>
      </main>
    </div>

    <!--    <div v-if="hasSidebar" class="fixed bottom-0 right-0 z-50 p-6 lg:hidden">-->
    <!--      <button-->
    <!--        class="p-3 text-white rounded-full shadow-lg bg-ui-primary-->
    <!--      hover:text-white"-->
    <!--        @click="sidebarOpen = !sidebarOpen"-->
    <!--      >-->
    <!--        <XIcon v-if="sidebarOpen" />-->
    <!--        <MenuIcon v-else />-->
    <!--      </button>-->
    <!--    </div>-->
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, nextTick, onMounted, ref } from 'vue';
import LayoutHeader from '@/components/layouts/LayoutHeader.vue';
import Sidebar from '@/components/layouts/Sidebar.vue';
import { useStore } from '@/store';
import { A_WHOAMI } from '@/store/auth/actions';

export default defineComponent({
  name: 'Home',
  components: {
    LayoutHeader,
    Sidebar
  },
  setup() {
    const store = useStore();
    const headerHeight = ref(0);
    const headerRef = ref();
    const sidebarOpen = ref(true);

    const setHeaderHeight = () => {
      nextTick(() => {
        headerHeight.value = headerRef.value.offsetHeight;
      });
    };

    const hasSidebar = computed(() => headerHeight.value > 0);

    const sidebarStyle = computed(() => ({
      top: `${headerHeight.value}px`,
      height: `calc(100vh - ${headerHeight.value}px)`
    }));

    onMounted(() => {
      setHeaderHeight();
    });

    return {
      hasSidebar,
      sidebarOpen,
      headerHeight,
      headerRef,
      setHeaderHeight,
      sidebarStyle
    };
  }
});
</script>
