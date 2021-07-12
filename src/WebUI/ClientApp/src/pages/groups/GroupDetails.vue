<script lang="ts">
import Loader from '@/components/app/AppSpinner.vue';
import { defineComponent, reactive } from 'vue';
import { useRoute } from 'vue-router';
import { UpdateGroupInput } from '@/graphql/graphql.types';
import { useUpdateGroupMutation } from '@/graphql/mutations/groups/updateGroup.generated';
import { useGetGroupByNameQuery } from '@/graphql/queries/groups/getGroupByName.generated';

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
    const { executeMutation: updateGroup, fetching: loadUpdate, error: errUpdate } = useUpdateGroupMutation();
    const { data: groupResult, fetching: loading, error } = useGetGroupByNameQuery({
      variables: { name: route.params.name },
      requestPolicy: 'network-only'
    });

    const save = async () => {
      if (groupResult.value) {
        updatedGroup.id = groupResult.value.groups[0].id;
        await updateGroup({ input: updatedGroup });
      }
    };

    return {
      loading,
      loadUpdate,
      errUpdate,
      error,
      groupResult,
      save,
      updatedGroup
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
    <template v-else-if="groupResult && groupResult.groups[0]">
      <label class="text-left block text-sm">
        <span class="text-gray-600 dark:text-gray-400">Name</span>
        <input
          v-model="updatedGroup.name"
          type="text"
          class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          :placeholder="groupResult.groups[0].name"
        />
      </label>
      <label class="text-left block text-sm my-3">
        <span class="text-gray-600 dark:text-gray-400">Description</span>
        <input
          v-model="updatedGroup.description"
          type="text"
          class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          :placeholder="groupResult.groups[0].description ?? 'Description'"
        />
      </label>
      <!-- Show available devices -->
      <template
        v-if="groupResult.groups[0].devices !== undefined && groupResult.groups[0].devices.length > 0"
      >
        <div class="text-left">
          <div>
            <span class="text-gray-500 text-sm text-left mt-2">Devices</span>
          </div>
          <div v-for="device in groupResult.groups[0].devices" :key="device.id" class="pl-3">
            {{ device.name }}
          </div>
        </div>
      </template>
      <template v-else>
        <div class="text-left">
          <span class="text-gray-500 text-sm text-left mt-2">No devices available</span>
        </div>
      </template>
      <div class="text-gray-500 text-sm text-left mt-10">Creator: {{ groupResult.groups[0].createdBy }}</div>
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
