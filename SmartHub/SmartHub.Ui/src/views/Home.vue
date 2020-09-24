<template>
  <div class="font-sans antialiased text-ui-typo bg-ui-background">
    <div class="flex flex-col justify-start min-h-screen">
      <header ref="headerRef" class="sticky top-0 z-10 w-full border-b bg-ui-background border-ui-border" @resize="setHeaderHeight">
        <AppHeader />
      </header>

      <main class="flex justify-start w-full bg-ui-background overflow-auto">
        <aside v-if="hasSidebar" class="px-4 lg:w-56 sidebar bg-ui-background" :class="{ open: sidebarOpen }" :style="sidebarStyle">
          <AppSidebar :show-sidebar="this.sidebarOpen" />
        </aside>

        <div class="container pb-6 flex justify-around">
          <router-view v-slot="{ Component }">
            <transition name="route" mode="out-in">
              <component :is="Component" />
            </transition>
          </router-view>
        </div>
      </main>
    </div>

    <div v-if="hasSidebar" class="fixed bottom-0 right-0 z-50 p-6 md:hidden">
      <button
        class="p-3 text-white rounded-full shadow-lg bg-ui-primary
         hover:text-white"
        @click="sidebarOpen = !sidebarOpen"
      >
        X
        <!-- <XIcon v-if="sidebarOpen" />
           <MenuIcon v-else /> -->
      </button>
    </div>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, nextTick, onMounted, ref, reactive } from 'vue';
import AppHeader from '@/components/layouts/AppHeader.vue';
import AppSidebar from '@/components/layouts/AppSidebar.vue';

export default defineComponent({
  name: 'Home',
  components: {
    AppHeader,
    AppSidebar
  },
  setup() {
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
      top: `${headerHeight.value}px`
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
