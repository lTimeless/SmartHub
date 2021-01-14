<template>
  <router-link
    :to="routes.Home"
    class="flex hover:bg-primaryBlueHover rounded-l items-end"
    :class="[onlyIcon ? 'w-14 justify-center' : ' w-48 justify-start pl-4']"
    title="Home"
  >
    <Logo :width="32" />
    <span v-if="!onlyIcon" class="ml-2 hidden text-xl sm:block text-primaryBlue">
      {{ appConfig.applicationName }}
    </span>
  </router-link>
</template>

<script lang="ts">
import { defineComponent, toRefs } from 'vue';
import Logo from '@/components/common/svgs/Logo.vue';
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
