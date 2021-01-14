<template>
  <router-link
    :to="route"
    class="h-14 flex hover:bg-primaryBlueHover rounded-l items-center"
    :class="[
      onlyIcon ? 'w-14 justify-center' : ' w-48 justify-start pl-4',
      isRoute ? 'bg-primaryBlue' : '',
      route === routes.Statistics ? ' border-t border-primaryBlueHover' : ''
    ]"
  >
    <AppIcon
      :icon-name="iconName"
      :icon-color="isRoute ? 'text-white' : 'text-primaryBlue'"
      height="h-7"
      width="w-7"
    />
    <div v-if="!onlyIcon">
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
import { useRouter } from 'vue-router';
import AppIcon from '@/components/icons/AppIcon.vue';
import { Routes } from '@/types/enums';
import { useCurrentRoute } from '@/hooks/useCurrentRoute';

export default defineComponent({
  name: 'NavigationItem',
  components: {
    AppIcon
  },
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
    }
  },
  setup(props) {
    const router = useRouter();
    const { isRoute } = useCurrentRoute(props.route);
    const getActiveClass = (route: string) => ({
      'text-primary': router.currentRoute.value.path === route,
      'text-gray-600': router.currentRoute.value.path !== route,
      'transition transform hover:translate-x-1 hover:text-primary': router.currentRoute.value.path !== route
    });
    return {
      isRoute,
      routes: Routes,
      getActiveClass,
      ...toRefs(props)
    };
  }
});
</script>

<style scoped></style>
