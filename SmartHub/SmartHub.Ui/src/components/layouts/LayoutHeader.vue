<template>
  <div class="py-2 border-t-2 border-ui-primary">
    <div class="mx-3 px-3">
      <div class="flex items-center justify-between -mx-2 sm:-mx-4">
        <div class="flex flex-col items-start px-2 mr-auto sm:px-4 sm:flex-row">
          <a href="/" class="flex items-center text-ui-primary" title="Home">
            <Logo :width="40" class="text-ui-primary" />
            <span class="hidden ml-2 text-xl font-black tracking-tighter uppercase sm:block">
              SmartHub
            </span>
          </a>
        </div>

        <div class="w-5/12 px-2 sm:px-4 max-w-screen-xs">
          <SearchWithDropdown />
        </div>

        <div class="flex items-center justify-end px-2 sm:px-4">
          <a href="www.github.com" class="sm:ml-3" target="_blank" rel="noopener noreferrer" title="Github" name="Github">
            <!--            <GithubIcon size="1.5x" />-->
          </a>

          <!--          <ToggleDarkMode class="ml-2 sm:ml-8">-->
          <!--            <template v-slot:default="{ dark }">-->
          <!--              <MoonIcon v-if="dark" size="1.5x" />-->
          <!--              <SunIcon v-else size="1.5x" />-->
          <!--            </template>-->
          <!--          </ToggleDarkMode>-->
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
// import ToggleDarkMode from '@/components/layouts/ToggleColorMode.vue';
import SearchWithDropdown from '@/components/layouts/SearchWithDropdown.vue';
import Logo from '@/components/svgs/Logo.vue';
import { defineComponent } from 'vue';
// import { GithubIcon } from 'vue-feather-icons';

// const Search = () =>
//   import(/* webpackChunkName: "search" */ '@/components/layouts/SearchWithDropdown.vue').catch((error) => console.warn(error));

export default defineComponent({
  name: 'LayoutHeader',
  components: {
    Logo,
    SearchWithDropdown
    // GithubIcon
    // ToggleDarkMode,
    // SunIcon,
    // MoonIcon
  }

  // computed: {
  //   // meta() {
  //   //   return this.$static.metadata;
  //   // },
  //   // settings() {
  //   //   return this.meta.settings;
  //   }
  // }
});
</script>

<style lang="scss">
header {
  svg:not(.feather-search) {
    &:hover {
      @apply text-ui-primary;
    }
  }
}

.nav-link {
  &.active {
    @apply text-ui-primary font-bold border-ui-primary;
  }
}
</style>
