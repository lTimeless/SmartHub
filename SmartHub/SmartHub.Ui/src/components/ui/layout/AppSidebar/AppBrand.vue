<template>
  <router-link :to="routes.Home" class="flex items-end mx-auto pr-3" title="Home">
    <Logo :width="35" />
    <span class="ml-2 hidden text-2xl font-bold sm:block text-primary">
      {{ appConfig.applicationName }}
    </span>
  </router-link>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import Logo from '@/components/ui/svgs/Logo.vue';
import { Routes } from '@/types/enums';
import { useQuery, useResult } from '@vue/apollo-composable';
import gql from 'graphql-tag';

const GET_APP_CONFIG_NAME = gql`
  query getAppConfig {
    appConfig {
      applicationName
    }
  }
`;

export default defineComponent({
  name: 'AppBrand',
  components: {
    Logo
  },
  setup() {
    const { result } = useQuery(GET_APP_CONFIG_NAME);
    const appConfig = useResult(result, 'SmartHub', (data) => data.appConfig);
    return {
      appConfig,
      routes: Routes
    };
  }
});
</script>

<style scoped></style>
