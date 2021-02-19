<template>
  <div class="flex flex-row justify-center w-full text-left">
    <div class="flex flex-col space-y-10">
      <h3 class="text-left md:text-center text-8xl text-primaryBlue mt-5">
        {{ currentTime }}
      </h3>
      <div>
        <h1 class="text-left text-3xl md:text-6xl text-primaryBlue">Good {{ timeOfDay }}</h1>
        <h2 v-if="fetching" class="text-left text-4xl md:text-6xl text-primarySienna">Loading...</h2>
        <h2
          v-else-if="data && data.me.user && data.me.user.userName"
          class="text-left text-4xl md:text-6xl text-primarySienna"
        >
          {{ capitalize(data.me.user.userName) }}
        </h2>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useDateTime } from '@/hooks/useDateTime';
import { useString } from '@/hooks/useString';
import { useGetMeQuery } from '@/graphql/queries/GetMe.generated';

export default defineComponent({
  name: 'Welcome',
  setup() {
    const { data, fetching } = useGetMeQuery();

    return {
      data,
      fetching,
      ...useString(),
      ...useDateTime()
    };
  }
});
</script>

<style scoped></style>
