<template>
  <router-link
    :to="route"
    class="flex items-center"
    :class="[getActiveClass(route), route === routes.Statistics ? 'border-t border-gray-300' : '']"
  >
    <div v-show="isCurrentRoute">
      <AppIcon icon-name="ChevronRight"></AppIcon>
    </div>
    <div v-show="!isCurrentRoute" class="w-6" />
    <div class="mr-5">
      <AppIcon :icon-name="iconName"></AppIcon>
    </div>
    <div class="tracking-wide text-lg leading-loose">
      {{ label }}
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
      required: true
    }
  },
  setup(props) {
    const router = useRouter();
    const routes = Routes;
    const isCurrentRoute = computed(() => useRoute().path === props.route);
    const getActiveClass = (path: string) => ({
      'text-primary': router.currentRoute.value.path === path,
      'text-gray-600': router.currentRoute.value.path !== path,
      'transition transform hover:translate-x-1 hover:text-primary': router.currentRoute.value.path !== path
    });
    return {
      isCurrentRoute,
      routes,
      getActiveClass,
      ...props
    };
  }
});
</script>

<style scoped></style>
