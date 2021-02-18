<script lang="ts">
import { defineComponent, reactive, computed } from 'vue';
import AppModal from '@/components/app/AppModals/AppModal.vue';
import { CreateDeviceInput } from '@/types/graphql/inputs';
import { ConnectionTypes, PluginTypes } from '@/types/enums';
import { useEnumTypes } from '@/hooks/useEnums.ts';
import { useMutation } from '@urql/vue';
import { CREATE_DEVICE } from '../../../graphql/mutations/DeviceMutations';

export default defineComponent({
  name: 'DeviceCreateModal',
  emits: ['close'],
  components: {
    AppModal
  },
  setup(_, context) {
    const deviceCreateInput = reactive<CreateDeviceInput>({
      name: '',
      groupName: '',
      ipv4: '',
      companyName: '',
      pluginName: '',
      pluginTypes: PluginTypes.None.toString(),
      primaryConnection: ConnectionTypes.None.toString(),
      secondaryConnection: ConnectionTypes.None.toString()
    });
    const { executeMutation: createDevice, fetching: loadCreate, error: errCreate } = useMutation(
      CREATE_DEVICE
    );
    const saveBtnActive = computed(() => deviceCreateInput.name !== '' && deviceCreateInput.ipv4 !== '');
    const close = () => {
      context.emit('close', false);
    };
    const save = async () => {
      await createDevice({ input: deviceCreateInput });
      context.emit('close', false);
    };
    return {
      deviceCreateInput,
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

<template>
  <AppModal
    title="Create new Device"
    save-btn-title="Create"
    close-btn-title="Cancel"
    :close="close"
    :save="save"
    :save-btn-active="saveBtnActive"
    main-bg-color="bg-yellow-400"
    main-border-color="border-yellow-400"
    :loading="loadCreate"
  >
    <div class="flex justify-between">
      <div class="w-full mr-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Name</span>
          <input
            type="text"
            v-model="deviceCreateInput.name"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            placeholder="Name"
          />
        </label>
      </div>
      <div class="w-1/3 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">PluginType</span>
          <select
            v-model="deviceCreateInput.pluginTypes"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          >
            <option v-for="(item, key) in pluginNames" :key="key" :value="item.toUpperCase()">
              {{ item }}
            </option>
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
            v-model="deviceCreateInput.description"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            placeholder="Description(optional)"
          />
        </label>
      </div>
      <div class="w-1/3 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Ipv4</span>
          <input
            type="text"
            v-model="deviceCreateInput.ipv4"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            placeholder="Ipv4"
          />
        </label>
      </div>
    </div>
    <!-- GroupName -->
    <div class="flex justify-between mt-3">
      <div class="w-full mr-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Groupname</span>
          <input
            type="text"
            v-model="deviceCreateInput.groupName"
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
            v-model="deviceCreateInput.companyName"
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
            v-model="deviceCreateInput.pluginName"
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
            v-model="deviceCreateInput.primaryConnection"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          >
            <option v-for="(item, key) in connectionNames" :key="key" :value="item.toUpperCase()">
              {{ item }}
            </option>
          </select>
        </label>
      </div>
      <div class="w-1/2 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Secondary connection</span>
          <select
            v-model="deviceCreateInput.secondaryConnection"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          >
            <option v-for="(item, key) in connectionNames" :key="key" :value="item.toUpperCase()">
              {{ item }}
            </option>
          </select>
        </label>
      </div>
    </div>
    <template v-if="errCreate">
      <div class="flex items-center pt-1 justify-center w-full h-full text-red-500">
        <p>Error: {{ errCreate.name }} {{ errCreate.message }}</p>
      </div>
    </template>
  </AppModal>
</template>
