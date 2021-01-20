<template>
  <router-link
    :to="route"
    class="h-12 flex hover:bg-primaryBlueHover rounded items-center"
    :class="[
      mobileSidebarOpen ? 'w-full space-x-3' : '',
      onlyIcon && !mobileSidebarOpen ? 'w-12 justify-center' : ' w-48 justify-start pl-4',
      isRoute ? 'bg-primaryBlue' : '',
      route === routes.Statistics ? ' border-t border-primaryBlueHover' : ''
    ]"
  >
    <AppIcon :icon-name="iconName" :icon-color="isRoute ? 'text-white' : 'text-primaryBlue'" />
    <div v-if="!onlyIcon || mobileSidebarOpen">
      <div
        class="tracking-wide text-lg leading-loose"
        :class="[isRoute ? 'text-white' : 'text-primaryBlue', onlyIcon ? ' ' : ' pl-2']"
      >
        {{ label }}
      </div>
    </div>
  </router-link>
</template>

<script lang="ts">
import { defineComponent, toRefs } from 'vue';
import AppIcon from '@/components/icons/AppIcon.vue';
import { Routes } from '@/types/enums';
import { useCurrentRoute } from '@/hooks/useCurrentRoute';

export default defineComponent({
  name: 'NavigationItem',
  components: {
    AppIcon
  },
  emits: ['update:close-mobile-sidebar'],
  props: {
    route: {
      type: String,
      required: true
    },
    iconName: {
      type: String,
      required: true
    },
    label: {
      type: String,
      required: false,
      default: ''
    },
    onlyIcon: {
      type: Boolean,
      required: false,
      default: true
    },
    mobileSidebarOpen: {
      type: Boolean,
      required: false,
      default: false
    }
  },
  setup(props) {
    const { isRoute } = useCurrentRoute(props.route);

    return {
      isRoute,
      routes: Routes,
      ...toRefs(props)
    };
  }
});
</script>

<style scoped></style>
