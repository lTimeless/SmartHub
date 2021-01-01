<template>
  <router-link
    :to="routes.Home"
    class="flex items-end mx-auto"
    :class="[onlyIcon ? 'pr-3' : '']"
    title="Home"
  >
    <Logo :width="35" />
    <span v-if="onlyIcon" class="ml-2 hidden text-2xl font-bold sm:block text-primary">
      {{ appConfig.applicationName }}
    </span>
  </router-link>
</template>

<script lang="ts">
import { defineComponent, toRefs } from 'vue';
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
  props: {
    onlyIcon: {
      type: Boolean,
      required: false,
      default: false
    }
  },
  setup(props) {
    const { result } = useQuery(GET_APP_CONFIG_NAME);
    const appConfig = useResult(result, 'SmartHub', (data) => data.appConfig);
    return {
      appConfig,
      routes: Routes,
      ...toRefs(props)
    };
  }
});
</script>

<style scoped></style>
