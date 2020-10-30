<template>
  <BaseModal
    title="Create new Device"
    save-btn-title="Create"
    close-btn-title="Cancel"
    :close="close"
    :save="save"
    :save-btn-active="saveBtnActive"
    header-color="bg-orange-400"
  >
    <div class="flex justify-between">
      <div class="w-full mr-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Name</span>
          <input
            v-model="deviceCreateRequest.name"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400 focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            placeholder="Name"
          />
        </label>
      </div>
      <div class="w-1/3 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">PluginType</span>
          <select
            v-model="deviceCreateRequest.pluginTypes"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400 focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-select"
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
            v-model="deviceCreateRequest.description"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400 focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            placeholder="Description (optional)"
          />
        </label>
      </div>
      <div class="w-1/3 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Ipv4</span>
          <input
            v-model="deviceCreateRequest.ipv4"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400 focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
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
            v-model="deviceCreateRequest.groupName"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400 focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
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
            v-model="deviceCreateRequest.companyName"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400 focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            placeholder="Company description"
          />
        </label>
      </div>
      <div class="w-1/2 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Pluginname</span>
          <input
            v-model="deviceCreateRequest.pluginName"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400 focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
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
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400 focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-select"
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
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400 focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-select"
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
import { DeviceCreateRequest } from '@/types/types';
import { useStore } from 'vuex';
import { ConnectionTypes, PluginTypes } from '@/types/enums';
import { useEnumTypes } from '@/hooks/useEnums.ts';
import { HomeActionTypes } from '@/store/home/actions';

export default defineComponent({
  name: 'DeviceCreateModal',
  emits: ['close'],
  components: {
    BaseModal
  },
  setup(props, context) {
    const store = useStore();
    const deviceCreateRequest = reactive<DeviceCreateRequest>({
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
      await store.dispatch(HomeActionTypes.CREATE_DEVICE, deviceCreateRequest);
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
