<script lang="ts">
import { computed, defineComponent, toRefs } from 'vue';
import Logo from './Logo.vue';
import { Routes } from '@/types/enums';
import { useQuery } from '@urql/vue';
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
    const { data } = useQuery({ query: GET_APP_CONFIG_NAME });
    const appConfig = computed(() => data.value.appConfig);
    return {
      appConfig,
      routes: Routes,
      ...toRefs(props)
    };
  }
});
</script>

<template>
  <router-link
    :to="routes.Home"
    class="flex rounded-l items-end"
    :class="[onlyIcon ? 'w-12 justify-center' : ' w-52 justify-start']"
    title="Home"
  >
    <Logo :width="32" />
    <span v-if="!onlyIcon" class="ml-2 text-xl text-primaryBlue">
      {{ appConfig.applicationName }}
    </span>
  </router-link>
</template>
