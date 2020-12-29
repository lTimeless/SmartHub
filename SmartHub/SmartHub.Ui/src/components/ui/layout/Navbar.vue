<template>
  <!-- Navbar -->
  <nav
    class="relative w-full bg-transparent md:flex-row md:flex-no-wrap md:justify-start flex items-center mb-4 pr-2"
  >
    <div class="w-full mx-auto items-center flex justify-between md:flex-no-wrap flex-wrap">
      <!-- Route -->
      <a class="text-gray-100 text-lg uppercase lg:inline-block font-semibold">{{ route.name }}</a>
      <!-- Form -->
      <form class="md:flex hidden flex-row flex-wrap items-center lg:ml-auto mr-3">
        <div class="relative flex w-full flex-wrap items-stretch">
          <span
            class="h-full leading-snug font-normal absolute text-center text-gray-400 absolute bg-transparent rounded text-base items-center justify-center w-8 pl-3 py-3"
          >
          </span>
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
      <div class="flex-col md:flex-row list-none items-center hidden md:flex">
        <!-- <NotificationDropdown />-->
        <UserDropdown />
      </div>
    </div>
  </nav>

  <!-- End Navbar -->
</template>
<script lang="ts">
import { defineComponent } from 'vue';
import UserDropdown from '@/components/ui/layout/UserDropdown.vue';
import { useRoute } from 'vue-router';
// TODO add back button icon in einer reihe unter dem routeName
export default defineComponent({
  name: 'Navbar',
  components: {
    UserDropdown
  },
  setup() {
    const route = useRoute();

    return {
      route
    };
  }
});
</script>
