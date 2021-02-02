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
    <template v-else-if="group">
      <label class="text-left block text-sm">
        <span class="text-gray-600 dark:text-gray-400">Name</span>
        <input
          type="text"
          v-model="updatedGroup.name"
          class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          :placeholder="group.name"
        />
      </label>
      <label class="text-left block text-sm my-3">
        <span class="text-gray-600 dark:text-gray-400">Description</span>
        <input
          type="text"
          v-model="updatedGroup.description"
          class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          :placeholder="group.description ?? 'Description'"
        />
      </label>
      <!-- Show available devices -->
      <template v-if="group.devices !== undefined && group.devices.length > 0">
        <div class="text-left">
          <div>
            <span class="text-gray-500 text-sm text-left mt-2">Devices</span>
          </div>
          <div v-for="device in group.devices" :key="device.id" class="pl-3">
            {{ device.name }}
          </div>
        </div>
      </template>
      <template v-else>
        <div class="text-left">
          <span class="text-gray-500 text-sm text-left mt-2">No devices available</span>
        </div>
      </template>
      <div class="text-gray-500 text-sm text-left mt-10">Creator: {{ group.createdBy }}</div>
      <!-- Save btn -->
      <div class="flex items-center justify-start mt-4">
        <button
          class="bg-transparent border-indigo-400 border border-solid font-bold uppercase text-sm pl-4 pr-6 py-3 rounded outline-none focus:outline-none"
          type="button"
          @click="save"
          :class="[
            !loadUpdate
              ? `hover:bg-indigo-400 hover:text-white text-gray-600`
              : 'opacity-50 focus:outline-none cursor-not-allowed'
          ]"
          :disabled="loadUpdate"
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

<script lang="ts">
import Loader from '@/components/ui/AppSpinner.vue';
import { UpdateGroupInput } from '@/types/types';
import { defineComponent, reactive } from 'vue';
import { useMutation, useQuery, useResult } from '@vue/apollo-composable';
import { UPDATE_GROUP } from './GroupMutations';
import { GET_GROUP_BY_ID, GET_GROUPS } from './GroupQueries';
import { useRoute } from 'vue-router';

export default defineComponent({
  name: 'GroupDetails',
  components: {
    Loader
  },
  setup() {
    const route = useRoute();
    const updatedGroup: UpdateGroupInput = reactive({
      id: ''
    });
    const { mutate: updateGroup, loading: loadUpdate, error: errUpdate } = useMutation(UPDATE_GROUP);
    const { result, loading, error } = useQuery(GET_GROUP_BY_ID, () => ({ name: route.params.name }), {
      fetchPolicy: 'no-cache'
    });
    const group = useResult(result, null, (data) => data.groups[0]);

    const save = async () => {
      if (group.value) {
        updatedGroup.id = group.value.id;
        await updateGroup({ input: updatedGroup }, { refetchQueries: [{ query: GET_GROUPS }] });
      }
    };

    return {
      loading,
      loadUpdate,
      errUpdate,
      error,
      group,
      save,
      updatedGroup
    };
  }
});
</script>

<style scoped></style>
