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
    <div class="flex justify-between">
      <div class="w-full mr-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Name</span>
          <input
            type="text"
            v-model="deviceDetail.name"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            placeholder="Name"
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
            v-model="deviceDetail.description"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            placeholder="Description (optional)"
          />
        </label>
      </div>
      <div class="w-1/3 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Ipv4</span>
          <input
            type="text"
            v-model="deviceDetail.ip.ipv4"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            placeholder="Ipv4"
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
            v-model="deviceDetail.company.name"
            class="mt-1 text-gray-500 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            placeholder="Company description"
          />
        </label>
      </div>
      <div class="w-1/2 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Pluginname</span>
          <input
            type="text"
            disabled
            v-model="deviceDetail.pluginName"
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
            v-model="deviceDetail.primaryConnection"
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
            v-model="deviceDetail.secondaryConnection"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          >
            <option v-for="(item, key) in connectionNames" :key="key" :value="key">{{ item }}</option>
          </select>
        </label>
      </div>
    </div>
    <div class="text-gray-500 text-sm text-left mt-10">Creator: {{ deviceDetail.createdBy }}</div>
  </BaseModal>
</template>

<script lang="ts">
import { defineComponent, PropType, ref } from 'vue';
import BaseModal from '@/components/modals/BaseModal.vue';
import { Device, UpdateDeviceInput } from '@/types/types';
import { useEnumTypes } from '@/hooks/useEnums';
// import { putByIdDevice } from '@/services/apis/device';

export default defineComponent({
  name: 'DeviceDetailsModal',
  emits: ['close'],
  components: {
    BaseModal
  },
  props: {
    device: {
      type: Object as PropType<Device>,
      required: true
    }
  },
  setup(props, context) {
    const deviceDetail = ref(props.device);
    const selectedPluginType = ref(
      useEnumTypes().pluginTypesValues.value.indexOf(deviceDetail.value.pluginTypes)
    );
    const selectedPConnType = ref(
      useEnumTypes().pluginTypesValues.value.indexOf(deviceDetail.value.primaryConnection)
    );
    const selectedSConnType = ref(
      useEnumTypes().pluginTypesValues.value.indexOf(deviceDetail.value.secondaryConnection)
    );

    const close = () => {
      context.emit('close', false);
    };

    const save = async () => {
      const updatedGroup: UpdateDeviceInput = {
        id: deviceDetail.value.id,
        name: deviceDetail.value.name,
        description: deviceDetail.value.description,
        primaryConnection: deviceDetail.value.primaryConnection,
        secondaryConnection: deviceDetail.value.secondaryConnection,
        ipv4: deviceDetail.value.ip.ipv4
      };

      // await putByIdDevice(updatedGroup)
      //   .then((response) => {
      //     if (!response.success) {
      //       return Promise.reject(response.message);
      //     }
      //     return Promise.resolve();
      //   })
      //   .catch((error) => Promise.reject(error));
      context.emit('close', false);
    };

    return {
      deviceDetail,
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
