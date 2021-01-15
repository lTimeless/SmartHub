<template>
  <!-- Navbar -->
  <div v-if="!isRoute" class="flex flex-row justify-between items-center">
    <div class="flex flex-row md:w-1/2 w-full">
      <!-- Burger Menu btn-->
      <div class="md:hidden flex flex-row justify-start items-center md:w-14 w-full rounded">
        <button type="button">
          <AppIcon :icon-name="iconName" icon-color="text-primaryBlueHover" @click="handleMenuClick" />
        </button>
      </div>
      <!-- Route Name -->
      <a class="text-xl p-2 text-primaryBlueHover uppercase">{{ route.name }}</a>
    </div>
    <!-- back btn -->
    <div class="flex-row hidden md:flex md:justify-end md:w-1/2">
      <button class="rounded p-2 hover:bg-charcoalBlue-600" type="button" @click="goBack">
        <AppIcon icon-name="ArrowLeft" icon-color="text-primaryBlueHover" />
      </button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
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
    const { isRoute } = useCurrentRoute(Routes.Home);
    const goBack = () => {
      router.back();
    };

    const handleMenuClick = () => {
      if (iconName.value === 'Close') {
        iconName.value = 'Menu';
        store.dispatch(AppActionTypes.SET_MOBILE_SIDEBAR, true);
      } else if (iconName.value === 'Menu') {
        iconName.value = 'Close';
        store.dispatch(AppActionTypes.SET_MOBILE_SIDEBAR, false);
      }
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
