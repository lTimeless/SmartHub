<template>
  <!-- Navbar -->
  <nav
    class="absolute top-0 left-0 w-full z-10 bg-transparent md:flex-row md:flex-no-wrap md:justify-start flex items-center p-4"
  >
    <div class="w-full mx-autp items-center flex justify-between md:flex-no-wrap flex-wrap md:px-10 px-4">
      <!-- Brand -->
      <a class="text-white text-sm uppercase hidden lg:inline-block font-semibold">{{ route.name }}</a>
      <!-- Form -->
      <form class="md:flex hidden flex-row flex-wrap items-center lg:ml-auto mr-3">
        <div class="relative flex w-full flex-wrap items-stretch">
          <span
            class="z-10 h-full leading-snug font-normal absolute text-center text-gray-400 absolute bg-transparent rounded text-base items-center justify-center w-8 pl-3 py-3"
            ><i class="fas fa-search"></i
          ></span>
          <label>
            <input
              type="text"
              placeholder="Search here..."
              class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            />
          </label>
        </div>
      </form>
      <!-- User -->
      <ul class="flex-col md:flex-row list-none items-center hidden md:flex">
        <UserDropdown></UserDropdown>
      </ul>
    </div>
  </nav>

  <!-- End Navbar -->
</template>
<script lang="ts">
// import ToggleDarkMode from '@/components/layouts/AppToggleTheme.vue';
import { defineComponent, ref } from 'vue';
import UserDropdown from '@/components/layout/UserDropdown.vue';
import { useRoute } from 'vue-router';
// import { GithubIcon } from 'vue-feather-icons';

export default defineComponent({
  name: 'Navbar',
  components: {
    UserDropdown
    // GithubIcon
    // ToggleDarkMode,
    // SunIcon,
    // MoonIcon
  },
  setup() {
    const route = useRoute();

    return {
      route
    };
  }
});
</script>
