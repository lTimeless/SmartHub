<template>
  <!-- Navbar -->
  <div class="flex flex-row justify-between items-center">
    <!-- Route -->
    <div class="flex flex-row w-1/2">
      <a class="text-gray-600 text-xl p-2 text-blueGray-100 uppercase">{{ route.name }}</a>
    </div>
    <!-- back btn -->
    <div class="flex-row hidden md:flex md:justify-end w-1/2">
      <button
        v-if="route.path !== routes.Home"
        class="rounded p-2 hover:bg-indigo-200"
        type="button"
        @click="goBack"
      >
        <AppIcon icon-name="ArrowLeft" />
      </button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import AppIcon from '@/components/icons/AppIcon.vue';
import { Routes } from '@/types/enums';

export default defineComponent({
  name: 'Navbar',
  components: {
    AppIcon
  },
  setup() {
    const route = useRoute();
    const router = useRouter();
    const goBack = () => {
      router.back();
    };
    return {
      route,
      goBack,
      routes: Routes
    };
  }
});
</script>
