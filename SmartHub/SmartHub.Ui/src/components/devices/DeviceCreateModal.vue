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
            v-model="deviceCreateinput.name"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            placeholder="Name"
          />
        </label>
      </div>
      <div class="w-1/3 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">PluginType</span>
          <select
            v-model="deviceCreateinput.pluginTypes"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          >
            <option v-for="(item, key) in pluginNames" :key="key" :value="item.toUpperCase()">{{ item }}</option>
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
            v-model="deviceCreateinput.description"
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
            v-model="deviceCreateinput.ipv4"
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
            v-model="deviceCreateinput.groupName"
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
            v-model="deviceCreateinput.companyName"
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
            v-model="deviceCreateinput.pluginName"
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
            v-model="deviceCreateinput.primaryConnection"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          >
            <option v-for="(item, key) in connectionNames" :key="key" :value="item.toUpperCase()">{{ item }}</option>
          </select>
        </label>
      </div>
      <div class="w-1/2 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Secondary connection</span>
          <select
            v-model="deviceCreateinput.secondaryConnection"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          >
            <option v-for="(item, key) in connectionNames" :key="key" :value="item.toUpperCase()">{{ item }}</option>
          </select>
        </label>
      </div>
    </div>
    <template v-if="errCreate">
      <div class="flex items-center pt-1 justify-center w-full h-full text-red-500">
        <p>Error: {{ errCreate.name }} {{ errCreate.message }}</p>
      </div>
    </template>
  </BaseModal>
</template>

<script lang="ts">
import { defineComponent, reactive, computed } from 'vue';
import BaseModal from '@/components/shared/modals/BaseModal.vue';
import { CreateDeviceInput } from '@/types/types';
import { ConnectionTypes, PluginTypes } from '@/types/enums';
import { useEnumTypes } from '@/hooks/useEnums.ts';
import { CREATE_DEVICE } from '@/graphql/mutations';
import { useMutation } from '@vue/apollo-composable';
import { GET_DEVICES, GET_DEVICES_COUNT } from '@/graphql/queries';

export default defineComponent({
  name: 'DeviceCreateModal',
  emits: ['close'],
  components: {
    BaseModal
  },
  setup(props, context) {
    const deviceCreateinput = reactive<CreateDeviceInput>({
      name: '',
      groupName: '',
      ipv4: '',
      companyName: '',
      pluginName: '',
      pluginTypes: PluginTypes.None.toString(),
      primaryConnection: ConnectionTypes.None.toString(),
      secondaryConnection: ConnectionTypes.None.toString()
    });
    const { mutate: createDevice, loading: loadCreate, error: errCreate } = useMutation(CREATE_DEVICE);
    const saveBtnActive = computed(() => deviceCreateinput.name !== '' && deviceCreateinput.ipv4 !== '');
    const close = () => {
      context.emit('close', false);
    };
    const save = async () => {
      console.log(deviceCreateinput);

      await createDevice(
        { input: deviceCreateinput },
        { refetchQueries: [{ query: GET_DEVICES }, { query: GET_DEVICES_COUNT }] }
      );
      context.emit('close', false);
    };
    return {
      deviceCreateinput,
      ConnectionTypes,
      saveBtnActive,
      close,
      save,
      loadCreate,
      errCreate,
      ...useEnumTypes()
    };
  }
});
</script>

<style scoped></style>
