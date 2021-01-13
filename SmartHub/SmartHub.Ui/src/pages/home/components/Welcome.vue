<template>
  <div class="flex flex-row justify-center w-full text-left">
    <div class="flex flex-col space-y-10">
      <h3 class="text-left md:text-center text-8xl text-charcoalBlue-300 mt-5">
        {{ currentTime }}
      </h3>
      <div>
        <h1 class="text-left text-3xl md:text-6xl text-charcoalBlue-300">Good {{ timeOfDay }}</h1>
        <h2 class="text-left text-4xl md:text-6xl text-primaryYellow">{{ capitalize(userName) }}</h2>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useDateTime } from '@/hooks/useDateTime';
import { useQuery, useResult } from '@vue/apollo-composable';
import { useString } from '@/hooks/useString';
import gql from 'graphql-tag';

const GET_USERNAME = gql`
  query GetUserName {
    me {
      userName
    }
  }
`;

export default defineComponent({
  name: `Welcome`,
  setup() {
    const { result } = useQuery(GET_USERNAME);
    const userName = useResult(result, '', (data) => data.me.userName);
    return {
      userName,
      ...useString(),
      ...useDateTime()
    };
  }
});
</script>

<style scoped></style>
