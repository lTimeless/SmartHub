<template>
  <div class="pt-8 md:pt-16">
    <h1 class="text-center font-bold text-4xl text-primary">Oh no! You are not authorized</h1>
  </div>
  <div class="mb-10 flex items-center justify-center">
    <router-link to="/" class="font-bold border-b border-primary text-xl">Go back</router-link>.
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'NotAuthorized',
  components: {}
});
</script>

<style scoped></style>
