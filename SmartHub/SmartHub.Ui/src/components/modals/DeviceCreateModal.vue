<template>
  <BaseModal
    title="Create new Device"
    save-btn-title="Create"
    close-btn-title="Cancel"
    :close="close"
    :save="save"
    :save-btn-active="saveBtnActive"
    main-bg-color="bg-yellow-400"
    main-border-color="border-yellow-400"
  >
    <div class="flex justify-between">
      <div class="w-full mr-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Name</span>
          <input
            type="text"
            v-model="deviceCreateRequest.name"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            placeholder="Name"
          />
        </label>
      </div>
      <div class="w-1/3 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">PluginType</span>
          <select
            v-model="deviceCreateRequest.pluginTypes"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
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
            v-model="deviceCreateRequest.description"
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
            v-model="deviceCreateRequest.ipv4"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            placeholder="Ipv4"
          />
        </label>
      </div>
    </div>
    <!-- Groupname -->
    <div class="flex justify-between mt-3">
      <div class="w-full mr-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Groupname</span>
          <input
            type="text"
            v-model="deviceCreateRequest.groupName"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            placeholder="Group name (optional)"
          />
        </label>
      </div>
    </div>
    <!-- Company and pluginName -->
    <div class="flex justify-between mt-3">
      <div class="w-1/2 mr-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Companyname</span>
          <input
            type="text"
            v-model="deviceCreateRequest.companyName"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            placeholder="Company description"
          />
        </label>
      </div>
      <div class="w-1/2 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Pluginname</span>
          <input
            type="text"
            v-model="deviceCreateRequest.pluginName"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
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
            v-model="deviceCreateRequest.primaryConnection"
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
            v-model="deviceCreateRequest.secondaryConnection"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          >
            <option v-for="(item, key) in connectionNames" :key="key" :value="key">{{ item }}</option>
          </select>
        </label>
      </div>
    </div>
  </BaseModal>
</template>

<script lang="ts">
import { defineComponent, reactive, computed } from 'vue';
import BaseModal from '@/components/modals/BaseModal.vue';
import { CreateDeviceInput } from '@/types/types';
import { ConnectionTypes, PluginTypes } from '@/types/enums';
import { useEnumTypes } from '@/hooks/useEnums.ts';
// import { postDevice } from '@/services/apis/device';

export default defineComponent({
  name: 'DeviceCreateModal',
  emits: ['close'],
  components: {
    BaseModal
  },
  setup(props, context) {
    const deviceCreateRequest = reactive<CreateDeviceInput>({
      name: '',
      description: '',
      groupName: '',
      ipv4: '',
      companyName: '',
      pluginName: '',
      pluginTypes: PluginTypes.None,
      primaryConnection: ConnectionTypes.None,
      secondaryConnection: ConnectionTypes.None
    });

    const saveBtnActive = computed(() => deviceCreateRequest.name !== '' && deviceCreateRequest.ipv4 !== '');
    const close = () => {
      context.emit('close', false);
    };
    const save = async () => {
      const { pluginTypesValues } = useEnumTypes();

      deviceCreateRequest.pluginTypes = pluginTypesValues.value[deviceCreateRequest.pluginTypes];
      // await postDevice(deviceCreateRequest)
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
      deviceCreateRequest,
      saveBtnActive,
      close,
      save,
      ...useEnumTypes()
    };
  }
});
</script>

<style scoped></style>
