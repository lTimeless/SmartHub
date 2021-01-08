<template>
  <router-link
    :to="route"
    class="block relative h-14 w-14 flex justify-center items-center hover:bg-primaryBlueHover rounded-l"
    :class="[
      isCurrentRoute ? 'bg-primaryBlue' : '',
      route === routes.Statistics ? ' border-t border-primaryBlueHover' : ''
    ]"
  >
    <div v-if="onlyIcon">
      <AppIcon
        :icon-name="iconName"
        :icon-color="isCurrentRoute ? 'text-white' : 'text-primaryBlue'"
        height="h-7"
        width="w-7"
      />
    </div>
    <div v-else>
      <div class="tracking-wide text-lg leading-loose">
        {{ label }}
      </div>
    </div>
  </router-link>
</template>

<script lang="ts">
import { computed, defineComponent } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import AppIcon from '@/components/icons/AppIcon.vue';
import { Routes } from '@/types/enums';

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
    const isCurrentRoute = computed(() => useRoute().fullPath.includes(props.route));
    const getActiveClass = (route: string) => ({
      'text-primary': router.currentRoute.value.path === route,
      'text-gray-600': router.currentRoute.value.path !== route,
      'transition transform hover:translate-x-1 hover:text-primary': router.currentRoute.value.path !== route
    });
    return {
      isCurrentRoute,
      routes: Routes,
      getActiveClass,
      ...props
    };
  }
});
</script>

<style scoped></style>
