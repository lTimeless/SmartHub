<template>
  <BaseModal
    title="Device details"
    save-btn-title="Save"
    close-btn-title="Cancel"
    :close="close"
    :save="save"
    main-bg-color="bg-yellow-400"
    main-border-color="border-yellow-400"
  >
    <template v-if="loading">
      <div class="flex items-center justify-center w-full h-full">
        <Loader />
      </div>
    </template>
    <template v-else-if="error">
      <div class="flex items-center justify-center w-full h-full">
        <p>Error: {{ error.name }} {{ error.message }}</p>
      </div>
    </template>
    <template v-else-if="device">
      <div class="flex justify-between">
        <div class="w-full mr-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">Name</span>
            <input
              type="text"
              v-model="updatedDevice.name"
              class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
              :placeholder="device.name"
            />
          </label>
        </div>
        <div class="w-1/3 ml-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">PluginType</span>
            <select
              disabled
              v-model="selectedPluginType"
              class="mt-1 text-gray-500 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            >
              <option v-for="(item, key) in pluginNames" :key="key" :value="key">{{ item }}</option>
            </select>
          </label>
        </div>
      </div>
      <!-- Description and ipv4 -->
      <div class="flex justify-between mt-3">
        <div class="w-full mr-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">Description</span>
            <input
              type="text"
              v-model="updatedDevice.description"
              class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
              :placeholder="device.description"
            />
          </label>
        </div>
        <div class="w-1/3 ml-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">Ipv4</span>
            <input
              type="text"
              v-model="updatedDevice.ipv4"
              class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
              :placeholder="device.ip.ipv4"
            />
          </label>
        </div>
      </div>
      <!-- Groupname -->
      <!-- <div class="flex justify-between mt-3">
      <div class="w-full mr-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Groupname</span>
          <input
            v-model="deviceDetail.groupName"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
            focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            placeholder="Group name (optional)"
          />
        </label>
      </div>
    </div> -->
      <!-- Company and pluginName -->
      <div class="flex justify-between mt-3">
        <div class="w-1/2 mr-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">Companyname</span>
            <input
              type="text"
              disabled
              v-model="device.company.name"
              class="mt-1 text-gray-500 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
              :placeholder="device.company.name"
            />
          </label>
        </div>
        <div class="w-1/2 ml-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">Pluginname</span>
            <input
              type="text"
              disabled
              v-model="device.pluginName"
              class="mt-1 text-gray-500 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
              placeholder="Plugin name"
            />
          </label>
        </div>
      </div>
      <!-- ConnectionTypes -->
      <div class="flex justify-between mt-3">
        <div class="w-1/2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">Primary connection</span>
            <select
              v-model="selectedPConnType"
              class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            >
              <option v-for="(item, key) in connectionNames" :key="key" :value="key">{{ item }}</option>
            </select>
          </label>
        </div>
        <div class="w-1/2 ml-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">Secondary connection</span>
            <select
              v-model="selectedSConnType"
              class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            >
              <option v-for="(item, key) in connectionNames" :key="key" :value="key">{{ item }}</option>
            </select>
          </label>
        </div>
      </div>
      <div class="text-gray-500 text-sm text-left mt-10">Creator: {{ device.createdBy }}</div>
    </template>
  </BaseModal>
</template>

<script lang="ts">
import { defineComponent, reactive, ref, watch } from 'vue';
import BaseModal from '@/components/modals/BaseModal.vue';
import { Device, UpdateDeviceInput } from '@/types/types';
import { useEnumTypes } from '@/hooks/useEnums';
import { useQuery, useMutation } from '@vue/apollo-composable';
import { GET_DEVICE_BY_ID } from '@/graphql/queries';
import Loader from '@/components/Loader.vue';
import { UPDATE_DEVICE } from '@/graphql/mutations';

export default defineComponent({
  name: 'DeviceDetailsModal',
  emits: ['close'],
  components: {
    BaseModal,
    Loader
  },
  props: {
    deviceId: {
      type: String,
      required: true
    }
  },
  setup(props, context) {
    const device = ref<Device | undefined>();

    const selectedPluginType = ref<number>();
    const selectedPConnType = ref<number>();
    const selectedSConnType = ref<number>();
    const updatedDevice: UpdateDeviceInput = reactive({
      id: ''
    });
    const { mutate: updateDevice, loading: loadUpdate, error: errUpdate } = useMutation(UPDATE_DEVICE);
    const { result, loading, error } = useQuery(GET_DEVICE_BY_ID, () => ({ id: props.deviceId }), {
      fetchPolicy: 'no-cache'
    });

    watch([loading, error], ([newLoad, newError]) => {
      if (!newLoad && !newError) {
        device.value = result.value.devices[0];
        if (device.value !== undefined) {
          // TODO fix -- EnumValue is always -1 so the dropdowns aren't filled correctly
          selectedPConnType.value = useEnumTypes().connectionTypesValues.value.indexOf(
            device.value.primaryConnection
          );
          selectedSConnType.value = useEnumTypes().connectionTypesValues.value.indexOf(
            device.value.secondaryConnection
          );
          selectedPluginType.value = useEnumTypes().pluginTypesValues.value.indexOf(device.value.pluginTypes);
        }
      }
    });

    const close = () => {
      context.emit('close');
    };

    const save = async () => {
      if (device.value) {
        updatedDevice.id = device.value.id;
        await updateDevice({ input: updatedDevice });
      }
      context.emit('close');
    };

    return {
      loadUpdate,
      errUpdate,
      device,
      loading,
      error,
      updatedDevice,
      selectedPluginType,
      selectedPConnType,
      selectedSConnType,
      close,
      save,
      ...useEnumTypes()
    };
  }
});
</script>

<style scoped></style>
