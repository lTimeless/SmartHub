<template>
  <div>
    <DeviceCreateModal v-if="showCreateModal" @close="toggleCreateModal" />

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
    <template v-if="data">
      <div v-if="data.devices">
        <div class="grid xl:grid-cols-3 md:grid-cols-2 sm:grid-cols-1 gap-4 rounded">
          <AppCard class="hover:shadow-md bg-white w-full" v-for="device in data.devices" :key="device.id">
            <div v-if="device" class="p-3 w-full">
              <h1
                class="text-xl text-left text-gray-600 font-bold cursor-pointer"
                @click="goToDetail(device.name)"
              >
                {{ device.name }}
              </h1>

              <div class="border-border border-t my-2"></div>
              <div class="flex justify-center">
                <AppDeviceControl device-type-name="Light" :light-state="device.status"></AppDeviceControl>
              </div>
            </div>
            <div v-else>Error loading device ...</div>
          </AppCard>
        </div>
      </div>
      <div v-else>No devices available</div>
    </template>
  </div>
</template>

<script lang="ts">
import { reactive, toRefs, defineComponent } from 'vue';
import AppCard from '@/components/ui/cards/AppCard.vue';
import DeviceCreateModal from './modals/DeviceCreateModal.vue';
import { useQuery } from '@urql/vue';
import Loader from '@/components/ui/AppSpinner.vue';
import { GET_DEVICES } from './DeviceQueries';
import { useRouter } from 'vue-router';
import { DevicesPayload } from '../../types/graphql/payloads';
import AppDeviceControl from '../../components/ui/controls/AppDeviceControl.vue';

export default defineComponent({
  name: 'Devices',
  components: {
    AppCard,
    DeviceCreateModal,
    Loader,
    AppDeviceControl
  },
  setup() {
    const router = useRouter();
    const { data, fetching: loading, error } = useQuery<DevicesPayload>({ query: GET_DEVICES });
    const state = reactive({
      showCreateModal: false
    });

    const toggleCreateModal = () => {
      state.showCreateModal = !state.showCreateModal;
    };

    const goToDetail = (name: string) => {
      router.push({ name: 'DeviceDetails', params: { name: name } });
    };

    return {
      ...toRefs(state),
      loading,
      error,
      data,
      toggleCreateModal,
      goToDetail
    };
  }
});
</script>

<style lang="scss" scoped></style>
