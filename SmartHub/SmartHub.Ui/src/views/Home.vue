<template>
  <div class="font-sans antialiased text-ui-typo bg-ui-background">
    <div class="flex flex-col justify-start min-h-screen">
      <header
        ref="headerRef"
        class="sticky top-0 z-10 w-full border-b bg-ui-background border-ui-border"
        @resize="setHeaderHeight">
        <AppHeader />
      </header>

      <div class="vueTourStartingMessage">
        <div class="call-button-container">
          <span id="v-step-0"></span>
        </div>
      </div>

      <main class="flex justify-start w-full bg-ui-background overflow-auto">
        <aside
          id="v-step-5"
          v-if="hasSidebar"
          class="px-4 md:w-56 sidebar bg-ui-background"
          :class="{ open: sidebarOpen }"
          :style="sidebarStyle"
        >
          <AppSidebar :show-sidebar="this.sidebarOpen" />
        </aside>

        <div class="container pb-6 justify-around overflow-y-auto">
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
        class="p-3 text-white rounded-full shadow-lg bg-ui-primary hover:text-white"
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
import { computed, defineComponent, nextTick, onMounted, ref, watch, onUnmounted} from 'vue';
import AppHeader from '@/components/layouts/AppHeader.vue';
import AppSidebar from '@/components/layouts/AppSidebar.vue';
import { useSignalRHub } from '@/hooks/useSignalR.ts';
import { useStore } from 'vuex';
import { HomeMutationTypes } from '@/store/home/mutations';
import { Home } from '@/types/types';
import { useVueTour } from 'vue-tour/useApi';

export default defineComponent({
  name: 'Home',
  components: {
    AppHeader,
    AppSidebar
  },
  setup() {
    let vueTour;
    const store = useStore();
    const headerHeight = ref(0);
    const headerRef = ref();
    const sidebarOpen = ref(true);
    const { data } = useSignalRHub<Home>('home', 'SendHome');

    watch(data, (newHomeData) => {
      if (newHomeData) {
        store.commit(HomeMutationTypes.UPDATE_HOME, newHomeData);
      }
    });
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
        vueTour = useVueTour();
        vueTour['myTour'].start();
    });

    onUnmounted( () => {
      if (vueTour != undefined) {
        vueTour['myTour'].stop();
      }
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

<style>
  .vueTourStartingMessage {
    position: fixed;
    bottom: 0;
    left: 50%;
    margin-bottom: 0.5em;
  }
</style>
