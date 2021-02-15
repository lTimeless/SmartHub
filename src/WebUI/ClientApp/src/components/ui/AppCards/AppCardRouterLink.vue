<template>
  <router-link :to="route">
    <div
      class="bg-white flex flex-col min-w-0 break-words mb-6 xl:mb-0 rounded border"
      :class="borderHoverColor"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <slot name="title" />
        </div>
        <p class="text-sm text-gray-500 mt-4">
          <slot name="subTitle" />
        </p>
      </div>
    </div>
  </router-link>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'AppCardRouterLink',
  props: {
    borderHoverColor: {
      type: String,
      required: false,
      default: 'hover:border-gray-500'
    },
    route: {
      type: String,
      required: true,
      default: '/'
    }
  },
  setup(props) {
    return {
      ...props
    };
  }
});
</script>

<style scoped></style>
