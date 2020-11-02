<template>
  <DeviceCreateModal v-if="showAddModal" @close="toggleModal" />
  <DeviceDetailsModal v-if="showDetailModal" @close="closeDetailsModal" :device="device" />

  <div class="w-full">
    <div class="flex justify-between items-center mb-4">
      <div class="flex justify-start w-full md:w-1/3 xl:w-1/6">
        <button
          @click="toggleModal(true)"
          class="flex justify-center items-center font-bold border border-ui-border rounded-lg bg-gray-400 hover:text-white transition-colors hover:bg-green-400 h-10 w-full"
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
    const home = computed(() => store.state.homeModule.home);
    const devices = computed(() => {
      if (home.value !== undefined) {
        const subDev = home.value.groups?.flatMap((x) => x.subGroups !== undefined ? x.subGroups.flatMap(c => c.devices) : []) as Device[];
        const dev = home.value.groups?.flatMap((x) => x.devices) as Device[];
        return subDev.concat(dev);
      }
      return [] as Device[];
    });
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
      home,
      devices,
      toggleModal,
      openDetailModal,
      closeDetailsModal
    };
  }
});
</script>

<style lang="scss" scoped></style>
