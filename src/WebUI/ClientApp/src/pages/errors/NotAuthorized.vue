<template>
  <div>
    <div class="pt-8 md:pt-16">
      <h1 class="text-center font-bold text-4xl text-primary">Oh no! You are not authorized</h1>
    </div>
    <div class="mb-10 flex flex-row space-x-6 items-center justify-center">
      <router-link :to="routes.Login" class="font-bold border-b border-primary text-xl">
        Go to Home
      </router-link>
      <router-link :to="routes.Home" class="font-bold border-b border-primary text-xl">
        Go to Login
      </router-link>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { Routes } from '@/types/enums';

export default defineComponent({
  name: 'NotAuthorized',
  components: {},
  setup() {
    return {
      routes: Routes
    };
  }
});
</script>

<style scoped></style>
