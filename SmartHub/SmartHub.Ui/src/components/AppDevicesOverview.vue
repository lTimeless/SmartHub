<template>
  <DeviceCreateModal v-if="showAddModal" @close="toggleModal" />
  <DeviceDetailsModal v-if="showDetailModal" @close="closeDetailsModal" :device="device" />

  <div class="w-full">
    <div class="flex justify-between items-center mb-4">
      <div class="flex justify-start w-full md:w-1/3 xl:w-1/6">
        <button
          @click="toggleModal(true)"
          class="block w-full px-4 py-2 mt-4 text-sm text-gray-500 font-medium leading-5 text-center bg-white hover:text-white transition-colors duration-150 hover:bg-yellow-400 border border-transparent rounded-lg active:bg-ui-primary focus:outline-none focus:shadow-outlineIndigo"
        >
          Add Device
        </button>
      </div>
    </div>
  </div>
  <div v-if="devices">
    <div class="grid xl:grid-cols-3 md:grid-cols-2 sm:grid-cols-1 gap-4">
      <AppCard class="bg-white shadow-md w-full" v-for="device in devices" :key="device.id">
        <div v-if="device" class="p-3 w-full">
          <h1
            class="text-xl text-left text-gray-600 font-bold cursor-pointer"
            @click="openDetailModal(true, device.id)"
          >
            {{ device.name }}
          </h1>

          <div class="text-gray-500 text-sm font-normal text-left">
            Creator: <span class="font-bold">{{ device.createdBy }}</span>
          </div>
          <div class="border-ui-border border-t my-2"></div>
          <!-- TODO: Here add the actual controlls or infos for the device -->
        </div>
        <div v-else>Error loading device ...</div>
      </AppCard>
    </div>
  </div>
  <div v-else>No devices available</div>
</template>

<script lang="ts">
import { reactive, toRefs, defineComponent, computed } from 'vue';
import AppCard from '@/components/widgets/AppCard.vue';
import { useStore } from 'vuex';
import DeviceCreateModal from '@/components/modals/DeviceCreateModal.vue';
import DeviceDetailsModal from '@/components/modals/DeviceDetailsModal.vue';
import { Device } from '@/types/types';
import { getByIdDevice } from '@/services/apis/device';

export default defineComponent({
  name: 'AppDevicesOverview',
  components: {
    AppCard,
    DeviceCreateModal,
    DeviceDetailsModal
  },
  setup() {
    const store = useStore();
    const devices = computed(() => store.state.appModule.devices);
    const state = reactive({
      showAddModal: false,
      showDetailModal: false,
      selectedDeviceId: '',
      device: {} as Device | null | undefined,
      showLoader: false
    });

    const toggleModal = (value: boolean) => {
      state.showAddModal = value;
    };
    const closeDetailsModal = async (value: boolean) => {
      state.showDetailModal = value;
    };

    const openDetailModal = async (value: boolean, deviceId: string) => {
      state.showLoader = true;
      if (value) {
        state.device = await getByIdDevice(deviceId)
          .then((response) => {
            if (!response.success) {
              return Promise.reject(response.message);
            }
            return Promise.resolve(response.data as Device);
          })
          .catch((error) => Promise.reject(error));
      }
      state.showLoader = false;
      state.selectedDeviceId = deviceId;
      state.showDetailModal = value;
    };

    return {
      ...toRefs(state),
      devices,
      toggleModal,
      openDetailModal,
      closeDetailsModal
    };
  }
});
</script>

<style lang="scss" scoped></style>
