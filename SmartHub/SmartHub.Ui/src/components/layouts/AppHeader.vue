<template>
  <div class="py-2 border-t-2 border-ui-primary">
    <div class="mx-3 px-3">
      <div class="flex items-center justify-between -mx-2 sm:-mx-4">
        <div class="flex flex-col items-start px-2 sm:px-4 sm:flex-row m-0">
          <router-link to="/" class="flex items-center text-ui-primary" title="Home">
            <Logo :width="40" class="text-ui-primary" />
            <span class="hidden ml-2 text-xl font-black tracking-tighter uppercase sm:block">
              SmartHub
            </span>
          </router-link>
        </div>

        <div class="w-4/12 md:ml-32">
          <Search :search-keys="[]" :data="[]" width="full" height="10" />
        </div>

        <div class="flex items-center justify-end px-2 sm:px-4">
          <a href="www.github.com" class="sm:ml-3" target="_blank" rel="noopener noreferrer" title="Github">
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
// import ToggleDarkMode from '@/components/layouts/AppToggleTheme.vue';
import Logo from '@/components/svgs/Logo.vue';
import { defineComponent } from 'vue';
import Search from '@/components/widgets/AppSearch.vue';
// import { GithubIcon } from 'vue-feather-icons';

export default defineComponent({
  name: 'LayoutHeader',
  components: {
    Logo,
    Search
    // GithubIcon
    // ToggleDarkMode,
    // SunIcon,
    // MoonIcon
  }
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
