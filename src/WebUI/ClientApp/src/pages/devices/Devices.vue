<script lang="ts">
import { reactive, toRefs, defineComponent } from 'vue';
import AppCard from '@/components/app/AppCards/AppCard.vue';
import Loader from '@/components/app/AppSpinner.vue';
import { useRouter } from 'vue-router';
import AppIcon from '@/components/icons/AppIcon.vue';
import { useString } from '@/hooks/useString';
import AppDeviceControl from '../../components/controls/AppDeviceControl.vue';
import DeviceCreateModal from './modals/DeviceCreateModal.vue';
import { useGetDevicesQuery } from '@/graphql/queries/devices/getDevices.generated';

export default defineComponent({
  name: 'Devices',
  components: {
    AppIcon,
    AppCard,
    DeviceCreateModal,
    Loader,
    AppDeviceControl
  },
  setup() {
    const router = useRouter();
    const { data, fetching: loading, error } = useGetDevicesQuery();

    const state = reactive({
      showCreateModal: false
    });

    const toggleCreateModal = () => {
      state.showCreateModal = !state.showCreateModal;
    };
    const goToDetail = (name: string) => {
      router.push({ name: 'DeviceDetails', params: { name } });
    };

    return {
      ...toRefs(state),
      ...useString(),
      loading,
      error,
      data,
      toggleCreateModal,
      goToDetail
    };
  }
});
</script>

<template>
  <div>
    <DeviceCreateModal v-if="showCreateModal" @close="toggleCreateModal" />

    <!-- Buttons -->
    <div class="w-full flex justify-between items-center my-4">
      <!-- Add Button -->
      <div class="flex justify-start">
        <button
          class="h-9 w-9 text-gray-600 text-center bg-white hover:text-white hover:bg-primaryBlue rounded-lg active:bg-primary focus:outline-none"
          @click="toggleCreateModal"
        >
          <a class="text-2xl">+</a>
        </button>
        <button
          class="ml-1 flex flex-row justify-between items-center content-center focus:outline-none cursor-not-allowed"
        >
          <AppIcon
            icon-name="Edit"
            :icon-color="false ? 'text-white' : ''"
            class="h-9 w-9 p-2 mr-1 bg-white rounded-lg"
            :class="[false ? 'hover:bg-primaryBlue' : '']"
          />
        </button>
      </div>
    </div>
    <!-- Show devices -->
    <div v-if="error">
      <p>Error: {{ error.name }} - {{ error.message }}</p>
    </div>
    <div v-else-if="loading">
      <Loader height="h-48" width="w-48" />
    </div>
    <div v-if="data">
      <div v-if="data.devices">
        <div class="grid xl:grid-cols-6 md:grid-cols-4 grid-cols-2 gap-4 rounded">
          <AppCard v-for="device in data.devices" :key="device.id" class="bg-white w-full h-36">
            <div v-if="device" class="w-full">
              <!-- Header -->
              <div class="flex flex-row justify-between items-center content-center">
                <h1 class="text-xl text-left text-gray-600 font-bold">
                  {{ device.name }}
                </h1>
                <AppIcon
                  icon-name="Edit"
                  class="cursor-pointer p-1 rounded focus:outline-none hover:ring-2 hover:ring-primaryBlue mr-1"
                  @click="goToDetail(device.name)"
                />
              </div>
              <!-- Divider -->
              <div class="border-border border-t border-blueGray-300 my-2" />
              <!-- Body -->
              <div class="flex justify-center py-4">
                <AppDeviceControl
                  :device-type-name="capitalize(device.pluginTypes.toString())"
                  :state="device.status"
                  :device-id="device.id"
                />
              </div>
            </div>
            <div v-else>Error loading device ...</div>
          </AppCard>
        </div>
      </div>
      <div v-else>No devices available</div>
    </div>
  </div>
</template>
