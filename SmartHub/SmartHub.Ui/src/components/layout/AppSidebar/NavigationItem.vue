<template>
  <router-link :to="route" class="flex items-center" :class="getClassesForAnchor(route)">
    <div v-show="isCurrentRoute" class="fill-current text-gray-500">
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="24"
        height="24"
        viewBox="0 0 24 24"
        fill="none"
        stroke="currentColor"
        stroke-width="2"
        stroke-linecap="round"
        stroke-linejoin="round"
        class="feather feather-chevron-right"
      >
        <polyline points="9 18 15 12 9 6"></polyline>
      </svg>
    </div>
    <div v-show="!isCurrentRoute" class="bg-red-500 w-6" />
    <div class="h-5 w-5 text-gray-500 mr-3">
      <img :src="getIcon()" :alt="`${label}`" />
    </div>
    <div class="tracking-wide text-lg leading-loose">
      {{ label }}
    </div>
  </router-link>
</template>

<script lang="ts">
import { computed, defineComponent } from 'vue';
import { useRoute, useRouter } from 'vue-router';

export default defineComponent({
  name: 'NavigationItem',
  props: {
    route: {
      type: String,
      required: true
    },
    icon: {
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
    const isCurrentRoute = computed(() => useRoute().path === props.route);
    const getIcon = () => require(`../../../assets/icons/${props.icon}.svg`);
    const getClassesForAnchor = (path: string) => ({
      'text-primary': router.currentRoute.value.path === path,
      'transition transform hover:translate-x-1 hover:text-primary': router.currentRoute.value.path !== path
    });
    return {
      isCurrentRoute,
      getIcon,
      getClassesForAnchor,
      ...props
    };
  }
});
</script>

<style scoped></style>
