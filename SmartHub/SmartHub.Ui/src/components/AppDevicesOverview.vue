<template>
  <DeviceCreateModal v-if="showAddModal" @close-modal="toggleModal" />
  <DeviceDetailsModal v-if="showDetailModal" @close-modal="closeDetailsModal" :device="device" />

  <div class="w-full">
    <div class="flex justify-between items-center mb-4">
      <div class="flex justify-start w-full md:w-1/3 xl:w-1/6">
        <button
          @click="openCreateDeviceModal"
          class="flex justify-center items-center font-bold border border-ui-border rounded-lg bg-gray-400
        hover:text-white transition-colors hover:bg-orange-400 h-10 w-full"
        >
          Add Device
        </button>
      </div>
    </div>
  </div>
  <div v-if="devices">
    <div class="grid grid-cols-1 gap-4">
      <AppCard class="bg-white shadow-md w-full" v-for="device in devices" :key="device.id">
        <div v-if="device" class="p-3 w-full">
          <h1 class="text-xl text-left text-gray-600 font-bold cursor-pointer" @click="openDetailModal(true, device.id)">
            {{ device.name }}
          </h1>

          <div class="text-gray-500 text-sm font-normal text-left">
            Creator: <span class="font-bold">{{ device.createdBy }}</span>
          </div>
          <div class="border-ui-border border-t my-2"></div>
        </div>
        <div v-else>Error loading device ... {{ device.name }}</div>
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
import { HomeActionTypes } from '@/store/home/actions';
import { Device } from '@/types/types';

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
    const devices = computed(() => (home.value === undefined ? undefined : home.value.groups?.flatMap((x) => x.devices)));
    const state = reactive({
      showAddModal: false,
      showDetailModal: false,
      selectedGroupId: '',
      device: {} as Device | null | undefined,
      showLoader: false
    });
    console.log('devices', devices.value);

    const openCreateDeviceModal = () => {
      state.showAddModal = true;
    };
    const toggleModal = (value: boolean) => {
      state.showAddModal = value;
    };
    const closeDetailsModal = async (value: boolean) => {
      state.showDetailModal = value;
    };

    const openDetailModal = async (value: boolean, groupId: string) => {
      state.showLoader = true;
      if (value) {
        // await store.dispatch(HomeActionTypes.FETCH_BY_GROUP_ID, groupId).then((response: Group | null) => {
        //   state.device = response;
        // });
      }
      state.showLoader = false;
      state.selectedGroupId = groupId;
    };

    return {
      ...toRefs(state),
      home,
      devices,
      openCreateDeviceModal,
      toggleModal,
      openDetailModal,
      closeDetailsModal
    };
  }
});
</script>

<style lang="scss" scoped></style>
