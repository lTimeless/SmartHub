<script lang="ts">
import { defineComponent, reactive, ref } from 'vue';
import Loader from '@/components/app/AppSpinner.vue';
import { useEnumTypes } from '@/hooks/useEnums';
import { useRoute } from 'vue-router';
import { useUpdate_DeviceMutation } from '@/graphql/mutations/devices/updateDevice.generated';
import { UpdateDeviceInput } from '@/graphql/graphql.types';
import { useGetDeviceByNameQuery } from '@/graphql/queries/devices/getDeviceByName.generated';

export default defineComponent({
  name: 'DeviceDetails',
  components: {
    Loader
  },
  setup() {
    const route = useRoute();
    const selectedPluginType = ref<number>();
    const selectedPConnType = ref<number>();
    const selectedSConnType = ref<number>();
    const updatedDevice: UpdateDeviceInput = reactive({
      id: ''
    });
    const {
      executeMutation: updateDevice,
      fetching: loadUpdate,
      error: errUpdate
    } = useUpdate_DeviceMutation();

    const { data: deviceResult, fetching: loading, error } = useGetDeviceByNameQuery({
      variables: { name: route.params.name as string }
    });

    const save = async () => {
      if (typeof deviceResult.value?.devices !== 'undefined') {
        updatedDevice.id = deviceResult.value.devices[0].id;
        updatedDevice.primaryConnection = deviceResult.value.devices[0].primaryConnection;
        updatedDevice.secondaryConnection = deviceResult.value?.devices[0].secondaryConnection;
        await updateDevice({ input: updatedDevice });
      }
    };

    return {
      loadUpdate,
      errUpdate,
      deviceResult,
      loading,
      error,
      updatedDevice,
      selectedPluginType,
      selectedPConnType,
      selectedSConnType,
      save,
      ...useEnumTypes()
    };
  }
});
</script>

<template>
  <div class="relative flex-col w-full justify-end bg-white border p-3 rounded">
    <template v-if="loading">
      <div class="flex items-center justify-center w-full h-full">
        <Loader height="h-48" width="w-48" />
      </div>
    </template>
    <template v-else-if="error">
      <div class="flex items-center justify-center w-full h-full">
        <p>Error: {{ error.name }} {{ error.message }}</p>
      </div>
    </template>
    <template v-else-if="deviceResult && deviceResult.devices">
      <div class="flex justify-between">
        <div class="w-full mr-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">Name</span>
            <input
              v-model="updatedDevice.name"
              type="text"
              class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
              :placeholder="deviceResult.devices[0].name"
            />
          </label>
        </div>
        <div class="w-1/3 ml-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">PluginType</span>
            <select
              v-model="deviceResult.devices[0].pluginTypes"
              disabled
              class="mt-1 text-gray-500 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
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
              v-model="updatedDevice.description"
              type="text"
              class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
              :placeholder="deviceResult.devices[0].description ?? 'Description'"
            />
          </label>
        </div>
        <div class="w-1/3 ml-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">Ipv4</span>
            <input
              v-model="updatedDevice.ipv4"
              type="text"
              class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
              :placeholder="deviceResult.devices[0].ip.ipv4"
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
              v-model="deviceResult.devices[0].company.name"
              type="text"
              disabled
              class="mt-1 text-gray-500 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
              :placeholder="deviceResult.devices[0].company.name"
            />
          </label>
        </div>
        <div class="w-1/2 ml-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">Pluginname</span>
            <input
              v-model="deviceResult.devices[0].pluginName"
              type="text"
              disabled
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
              v-model="deviceResult.devices[0].primaryConnection"
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
              v-model="deviceResult.devices[0].secondaryConnection"
              class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            >
              <option v-for="(item, key) in connectionNames" :key="key" :value="item.toUpperCase()">
                {{ item }}
              </option>
            </select>
          </label>
        </div>
      </div>
      <div class="text-gray-500 text-sm text-left mt-10">
        Creator: {{ deviceResult.devices[0].createdBy }}
      </div>
      <!-- Save btn -->
      <div class="flex items-center justify-start mt-4">
        <button
          class="bg-transparent border-indigo-400 border border-solid font-bold uppercase text-sm pl-4 pr-6 py-3 rounded outline-none focus:outline-none"
          type="button"
          :class="[
            !loadUpdate
              ? `hover:bg-indigo-400 hover:text-white text-gray-600`
              : 'opacity-50 focus:outline-none cursor-not-allowed'
          ]"
          :disabled="loadUpdate"
          @click="save"
        >
          <span class="flex">
            <Loader v-if="loadUpdate" height="h-2" width="w-2" />
            <span class="pl-2">Save</span>
          </span>
        </button>
      </div>
    </template>
  </div>
</template>
