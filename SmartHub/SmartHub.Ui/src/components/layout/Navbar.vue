<template>
  <!-- Navbar -->
  <nav
    class="absolute top-0 left-0 w-full z-10 bg-transparent md:flex-row md:flex-no-wrap md:justify-start flex items-center p-4"
  >
    <div class="w-full mx-auto items-center flex justify-between md:flex-no-wrap flex-wrap md:px-10 px-4">
      <!-- Brand -->
      <a class="text-gray-100 text-lg uppercase lg:inline-block font-semibold">{{ route.name }}</a>
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
              class="mt-1 placeholder-gray-500 focus:ring-indigo-500 focus:border-indigo-500 focus:bg-gray-100 focus:bg-opacity-50 block w-full sm:text-sm border-gray-300 rounded-md bg-white"
            />
          </label>
        </div>
      </form>
      <!-- Dropdowns -->
      <ul class="flex-col md:flex-row list-none items-center hidden md:flex">
        <NotificationDropdown class="mr-2" />
        <UserDropdown />
      </ul>
    </div>
  </nav>

  <!-- End Navbar -->
</template>
<script lang="ts">
import { defineComponent } from 'vue';
import UserDropdown from '@/components/layout/UserDropdown.vue';
import { useRoute } from 'vue-router';
import NotificationDropdown from '@/components/layout/NotificationDropdown.vue';

export default defineComponent({
  name: 'Navbar',
  components: {
    UserDropdown,
    NotificationDropdown
  },
  setup() {
    const route = useRoute();

    return {
      route
    };
  }
});
</script>
