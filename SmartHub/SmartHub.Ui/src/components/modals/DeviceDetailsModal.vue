<template>
  <BaseModal title="Group details" saveBtnTitle="Save" closeBtnTitle="Cancel" :close="close" :save="save" headerColor="bg-orange-400">
<div class="flex justify-between">
      <div class="w-full mr-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Name</span>
          <input
            v-model="deviceDetail.name"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
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
            class="block w-full mt-1 text-gray-500 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-select"
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
            v-model="deviceDetail.description"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
            focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            placeholder="Description (optional)"
          />
        </label>
      </div>
      <div class="w-1/3 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Ipv4</span>
          <input
            v-model="deviceDetail.ip.ipv4"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
            focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
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
            disabled
            v-model="deviceDetail.company.name"
            class="block w-full mt-1 text-sm text-gray-500 dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
            focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            placeholder="Company description"
          />
        </label>
      </div>
      <div class="w-1/2 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Pluginname</span>
          <input
            disabled
            v-model="deviceDetail.pluginName"
            class="block w-full mt-1 text-sm text-gray-500 dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
            focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
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
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-select"
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
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-select"
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
import { Device, DeviceUpdateRequest } from '@/types/types';
import { useStore } from 'vuex';
import { useEnumTypes } from '@/hooks/useEnums';
import { putByIdDevice } from '@/services/apis/device.service';
import { HomeActionTypes } from '@/store/home/actions';

export default defineComponent({
  name: 'DeviceDetailsModal',
  emits: ['close-modal'],
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
    const store = useStore();
    const deviceDetail = ref(props.device);
    const selectedPluginType = ref(useEnumTypes().pluginTypesValues.value.indexOf(deviceDetail.value.pluginTypes));
    const selectedPConnType = ref(useEnumTypes().pluginTypesValues.value.indexOf(deviceDetail.value.primaryConnection));
    const selectedSConnType = ref(useEnumTypes().pluginTypesValues.value.indexOf(deviceDetail.value.secondaryConnection));
    
    const close = () => {
      context.emit('close-modal', false);
    };

    const save = async () => {
        const updatedGroup: DeviceUpdateRequest = {
          id: deviceDetail.value.id,
          name: deviceDetail.value.name,
          description: deviceDetail.value.description,
          primaryConnection: deviceDetail.value.primaryConnection,
          secondaryConnection: deviceDetail.value.secondaryConnection,
          ipv4: deviceDetail.value.ip.ipv4
        };
      
      await putByIdDevice(updatedGroup)
        .then((response) => {
          if (!response.success) {
            return Promise.reject(response.message);
          }
          return Promise.resolve();
        })
        .catch((error) => Promise.reject(error));
      context.emit('close-modal', false);
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
