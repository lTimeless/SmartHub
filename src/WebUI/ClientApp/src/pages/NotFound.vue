<script lang="ts">
import NotFound from '@/components/svgs/NotFound.vue';
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'NotFound',
  components: {
    NotFound
  }
});
</script>

<template>
  <div>
    <div class="flex flex-col items-center mb-2 text-primary">
      <NotFound :width="400" />
    </div>
    <div class="pt-8 md:pt-16">
      <h1 class="text-center font-bold text-4xl text-primary">Oh no! There is nothing here.</h1>
    </div>
    <div class="mb-10 flex items-center justify-center">
      <router-link to="/" class="font-bold border-b border-primary text-xl">Go back</router-link>
    </div>
  </div>
</template>
