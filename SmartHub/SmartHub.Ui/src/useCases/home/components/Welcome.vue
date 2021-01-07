<template>
  <div class="flex flex-row justify-center w-full text-left">
    <div class="flex flex-col space-y-10">
      <h3 class="text-center text-7xl text-blueGray-300">
        {{ currentTime }}
      </h3>
      <div>
        <h1 class="text-left text-6xl text-blueGray-300">Good {{ timeOfDay }}</h1>
        <h2 class="text-left text-6xl text-blueGray-100">{{ capitalize(userName) }}</h2>
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
