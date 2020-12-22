<template>
  <DeviceCreateModal v-if="showCreateModal" @close="toggleCreateModal" />
  <DeviceDetailsModal v-if="showDetailModal" @close="toggleDetailModal(null)" :device-id="deviceId" />

  <!-- Buttons -->
  <div class="w-full">
    <div class="flex justify-between items-center mb-4">
      <!-- Add Button -->
      <div class="flex justify-start w-full md:w-1/3 xl:w-1/6">
        <button
          @click="toggleCreateModal"
          class="block w-full px-4 py-2 mt-4 text-sm text-gray-600 font-medium leading-5 text-center bg-white hover:text-white hover:bg-indigo-400 border border-transparent rounded-lg active:bg-primary focus:outline-none"
        >
          Add Device
        </button>
      </div>
    </div>
  </div>
  <!-- Show devices -->
  <template v-if="error">
    <p>Error: {{ error.name }} - {{ error.message }}</p>
  </template>
  <template v-else-if="loading">
    <Loader height="h-48" width="w-48" />
  </template>
  <template v-if="devices">
    <div v-if="devices.length > 0">
      <div class="grid xl:grid-cols-3 md:grid-cols-2 sm:grid-cols-1 gap-4 rounded">
        <AppCard class="border hover:border-indigo-400 bg-white w-full" v-for="device in devices" :key="device.id">
          <div v-if="device" class="p-3 w-full">
            <h1
              class="text-xl text-left text-gray-600 font-bold cursor-pointer"
              @click="toggleDetailModal(device.id)"
            >
              {{ device.name }}
            </h1>

            <div class="text-gray-500 text-sm font-normal text-left">
              Creator: <span class="font-bold">{{ device.createdBy }}</span>
            </div>
            <div class="border-border border-t my-2"></div>
            <!-- TODO: Here add the actual controlls or infos for the device -->
          </div>
          <div v-else>Error loading device ...</div>
        </AppCard>
      </div>
    </div>
    <div v-else>No devices available</div>
  </template>
</template>

<script lang="ts">
import { reactive, toRefs, defineComponent } from 'vue';
import AppCard from '@/components/shared/widgets/AppCard.vue';
import DeviceCreateModal from '@/components/devices/DeviceCreateModal.vue';
import DeviceDetailsModal from '@/components/devices/DeviceDetailsModal.vue';
import { GET_DEVICES } from '@/graphql/queries';
import { useQuery, useResult } from '@vue/apollo-composable';
import Loader from '@/components/shared/Loader.vue';

export default defineComponent({
  name: 'Devices',
  components: {
    AppCard,
    DeviceCreateModal,
    DeviceDetailsModal,
    Loader
  },
  setup() {
    const { result, loading, error } = useQuery(GET_DEVICES);
    const devices = useResult(result, null, (data) => data.devices);
    const state = reactive({
      showCreateModal: false,
      showDetailModal: false,
      deviceId: ''
    });

    const toggleCreateModal = () => {
      state.showCreateModal = !state.showCreateModal;
    };

    const toggleDetailModal = (deviceId: string | null) => {
      state.showDetailModal = !state.showDetailModal;
      if (state.showDetailModal && deviceId !== null) {
        state.deviceId = deviceId;
      } else {
        state.deviceId = '';
      }
    };

    return {
      ...toRefs(state),
      loading,
      error,
      devices,
      toggleCreateModal,
      toggleDetailModal
    };
  }
});
</script>

<style lang="scss" scoped></style>
