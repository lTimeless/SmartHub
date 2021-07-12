<script lang="ts">
import { defineComponent, ref } from 'vue';
import { Roles } from '@/types/enums';
import { useIdentity } from '@/hooks/useIdentity';
import AdminCardsRow from './components/AdminCardsRow.vue';
import CardsRow from './components/CardsRow.vue';
import Welcome from './components/Welcome.vue';

export default defineComponent({
  name: 'Home',
  components: {
    AdminCardsRow,
    CardsRow,
    Welcome
  },
  setup() {
    const expandAdminRow = ref(false);
    const { isRole } = useIdentity();
    return {
      isRole,
      adminRole: Roles.Admin,
      expandAdminRow
    };
  }
});
</script>

<template>
  <div class="flex flex-col justify-around h-full mt-4">
    <div class="h-1/2">
      <Welcome />
    </div>
    <div class="h-1/2">
      <!-- Cards -->
      <div class="flex flex-wrap justify-between">
        <CardsRow />
      </div>
      <!-- Expand Arrow -->
      <div v-if="isRole() === adminRole" class="px-4 my-2 flex justify-end">
        <div class="cursor-pointer flex" @click="expandAdminRow = !expandAdminRow">
          <span v-if="!expandAdminRow" class="text-sm text-primaryBlue pr-1">More</span>
          <svg
            v-if="!expandAdminRow"
            class="-mr-1 h-5 w-5"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
          >
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7" />
          </svg>
        </div>
        <div class="cursor-pointer flex" @click="expandAdminRow = !expandAdminRow">
          <span v-if="expandAdminRow" class="text-sm text-primaryBlue pr-1">Hide</span>
          <svg
            v-if="expandAdminRow"
            class="-mr-1 h-5 w-5"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
          >
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 15l7-7 7 7" />
          </svg>
        </div>
      </div>
      <!-- Admin Cards -->
      <template v-if="isRole() === adminRole && expandAdminRow">
        <div class="flex flex-wrap mb-6 rounded">
          <AdminCardsRow />
        </div>
      </template>
    </div>
  </div>
</template>
