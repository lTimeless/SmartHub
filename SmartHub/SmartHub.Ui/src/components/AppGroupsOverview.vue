<template>
  <div class="w-full">
    <!-- <AppButton title="Add Group" color="orange" :callback="createGroup" class="xl:w-1/2" /> -->
    <div class="flex justify-between items-center mb-4">
      <div class="flex justify-start sm:w-full md:w-1/3 xl:w-1/5">
        <button
          @click="createGroup"
          class="flex justify-center items-center font-bold border border-ui-border rounded-lg bg-gray-400
        hover:text-white transition-colors hover:bg-orange-400 h-10 w-full"
        >
          Add Group
        </button>
      </div>
    </div>
  </div>
  <div class="grid xl:grid-cols-3 md:grid-cols-2 sm:grid-cols-1 gap-4">
    <AppCard class="bg-white shadow-md w-full" v-for="group in groups" :key="group.id">
      <div class="p-3 w-full">
        <router-link to="">
          <h1 class="text-xl text-left text-gray-600 font-bold">
            {{ group.name }}
          </h1>
        </router-link>

        <div class="text-gray-500 text-sm font-normal text-left">
          Creator: <span class="font-bold">{{ group.createdBy }}</span>
        </div>
        <div class="border-ui-border border-t my-2"></div>

        <!-- Show available devices -->
        <template v-if="group.devices !== undefined && group.devices.length > 0">
          <div v-for="device in group.devices" :key="device.id">
            {{ device.name }}
          </div>
        </template>
        <template v-else>
          <div>
            <span class="text-gray-500 text-sm text-left mt-2">No devices available</span>
          </div>
        </template>
      </div>
    </AppCard>
  </div>
</template>

<script lang="ts">
import { reactive, toRefs, defineComponent, computed } from 'vue';
import AppCard from '@/components/widgets/AppCard.vue';
import { useStore } from 'vuex';

export default defineComponent({
  name: 'AppGroupsOverview',
  components: {
    AppCard
  },
  setup() {
    const store = useStore();
    const home = computed(() => store.state.homeModule.home);
    const state = reactive({
      groups: home.value?.groups
    });
    const createGroup = () => {
      console.log('Click Create');
    };
    return {
      ...toRefs(state),
      createGroup
    };
  }
});
</script>

<style lang="scss" scoped></style>
