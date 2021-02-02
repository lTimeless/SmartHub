import { computed, ref, ComputedRef } from 'vue';
import { useRoute } from 'vue-router';

type CurrentRouteState = {
  isRoute: ComputedRef<boolean>;
};

export const useCurrentRoute = (route: string): CurrentRouteState => {
  const currentRoute = ref(route);
  const isRoute = computed(() => useRoute().fullPath.includes(currentRoute.value));
  return {
    isRoute
  };
};
