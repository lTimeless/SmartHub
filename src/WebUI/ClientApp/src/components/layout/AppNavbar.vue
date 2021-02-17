<template>
  <!-- Navbar -->
  <div class="flex flex-row justify-between items-center">
    <div class="flex flex-row md:w-1/2 w-full h-12">
      <!-- Burger Menu btn-->
      <div class="md:hidden flex flex-row justify-start items-center md:w-14 w-full rounded">
        <button type="button" class="ml-6 mt-1">
          <AppIcon :icon-name="iconName" icon-color="text-primaryBlue" @click="handleMenuClick" />
        </button>
      </div>
      <!-- Route Name -->
      <a v-if="!isRoute" class="md:text-xl text-lg py-2 text-primaryBlue uppercase">{{ route.name }}</a>
    </div>
    <!-- back btn -->
    <div v-if="!isRoute" class="flex-row hidden md:flex md:justify-end md:w-1/2">
      <button
        class="rounded p-1 focus:outline-none hover:ring-2 hover:ring-primaryBlue hover:ring-opacity-50"
        type="button"
        @click="goBack"
      >
        <AppIcon icon-name="ArrowLeft" icon-color="text-primaryBlue" />
      </button>
    </div>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, ref, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import AppIcon from '@/components/icons/AppIcon.vue';
import { Routes } from '@/types/enums';
import { useCurrentRoute } from '@/hooks/useCurrentRoute';
import { useStore } from 'vuex';
import { AppActionTypes } from '@/store/app/actions';

export default defineComponent({
  name: 'AppNavbar',
  components: {
    AppIcon
  },
  setup() {
    const route = useRoute();
    const router = useRouter();
    const store = useStore();
    const iconName = ref('Menu');
    const mobileSidebarOpen = computed(() => store.state.appModule.mobileSidebarOpen);
    const { isRoute } = useCurrentRoute(Routes.Home);
    const goBack = () => {
      router.back();
    };

    watch(mobileSidebarOpen, (newV) => {
      if (newV) {
        iconName.value = 'Close';
      } else {
        iconName.value = 'Menu';
      }
    });

    const handleMenuClick = () => {
      store.dispatch(AppActionTypes.SET_MOBILE_SIDEBAR, true);
    };

    return {
      route,
      goBack,
      isRoute,
      routes: Routes,
      handleMenuClick,
      iconName
    };
  }
});
</script>
