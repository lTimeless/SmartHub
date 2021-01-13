<template>
  <!-- Navbar -->
  <div v-if="!isRoute" class="flex flex-row justify-between items-center">
    <!-- Burger Menu btn-->
    <div class="hidden flex flex-row justify-start items-center w-14 w-full rounded">
      <button class="p-2 absolute right-8 sm:right-10 top-2 z-20" type="button">
        <AppIcon icon-name="Menu" icon-color="text-primaryBlueHover" />
      </button>
    </div>
    <!-- Route Name -->
    <div class="flex flex-row w-1/2">
      <button class="rounded p-2 hover:bg-charcoalBlue-600" type="button" @click="goBack">
        <AppIcon icon-name="ArrowLeft" icon-color="text-primaryBlueHover" />
      </button>
    </div>
    <!-- back btn -->
    <div class="flex-row hidden md:flex md:justify-end w-1/2">
      <a class="text-xl p-2 text-primaryBlueHover uppercase">{{ route.name }}</a>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import AppIcon from '@/components/icons/AppIcon.vue';
import { Routes } from '@/types/enums';
import { useCurrentRoute } from '@/hooks/useCurrentRoute';

// TODO hier dann über vuex die sidebar via btn steuern
export default defineComponent({
  name: 'AppNavbar',
  components: {
    AppIcon
  },
  setup() {
    const route = useRoute();
    const router = useRouter();
    const { isRoute } = useCurrentRoute(Routes.Home);
    const goBack = () => {
      router.back();
    };
    return {
      route,
      goBack,
      isRoute,
      routes: Routes
    };
  }
});
</script>
