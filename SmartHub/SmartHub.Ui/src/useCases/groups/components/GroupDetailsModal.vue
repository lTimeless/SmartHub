<template>
  <BaseModal
    title="Group details"
    save-btn-title="Save"
    close-btn-title="Cancel"
    :close="close"
    :save="save"
    main-border-color="border-red-400"
    main-bg-color="bg-red-400"
    :loading="loadUpdate"
  >
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
      <label class="text-left block text-sm mt-3">
        <span class="text-gray-600 dark:text-gray-400">Description</span>
        <input
          type="text"
          v-model="updatedGroup.description"
          class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          :placeholder="group.description ?? 'Description'"
        />
      </label>
      <div class="border-border border-t my-2"></div>
      <!-- Show available subGroups -->
      <template v-if="group.subGroups !== undefined && group.subGroups.length > 0">
        <div class="text-left">
          <div>
            <span class="text-gray-500 text-sm text-left mt-2">SubGroups</span>
          </div>
          <div v-for="subgroup in group.subGroups" :key="subgroup.id" class="pl-3">
            {{ subgroup.name }}
            <!-- Show available subGroup devices-->
            <template v-if="subgroup.devices !== undefined && subgroup.devices.length > 0">
              <div class="text-left pl-3">
                <div>
                  <span class="text-gray-500 text-sm text-left mt-2">Devices</span>
                </div>
                <div v-for="device in subgroup.devices" :key="device.id" class="pl-3">
                  {{ device.name }}
                </div>
              </div>
            </template>
            <template v-else>
              <div class="text-left pl-3">
                <span class="text-gray-500 text-sm text-left mt-2">No devices available</span>
              </div>
            </template>
          </div>
        </div>
      </template>
      <template v-else>
        <div class="text-left">
          <span class="text-gray-500 text-sm text-left mt-2">No subGroups available</span>
        </div>
      </template>
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
    </template>
  </BaseModal>
</template>

<script lang="ts">
import { defineComponent, reactive, ref, watch } from 'vue';
import BaseModal from '@/components/ui/modals/BaseModal.vue';
import { Group, UpdateGroupInput } from '@/types/types';
import { useMutation, useQuery, useResult } from '@vue/apollo-composable';
import { GET_GROUPS, GET_GROUP_BY_ID } from '@/graphql/queries';
import Loader from '@/components/ui/AppSpinner.vue';
import { UPDATE_GROUP } from '@/graphql/mutations';

export default defineComponent({
  name: 'GroupDetailsModal',
  emits: ['close'],
  components: {
    BaseModal,
    Loader
  },
  props: {
    groupId: {
      type: String,
      required: true
    }
  },
  setup(props, context) {
    const close = () => {
      context.emit('close');
    };
    const updatedGroup: UpdateGroupInput = reactive({
      id: ''
    });
    const { mutate: updateGroup, loading: loadUpdate, error: errUpdate } = useMutation(UPDATE_GROUP);
    const { result, loading, error } = useQuery(GET_GROUP_BY_ID, () => ({ id: props.groupId }), {
      fetchPolicy: 'no-cache'
    });
    const group = useResult(result, null, (data) => data.groups[0]);

    const save = async () => {
      if (group.value) {
        updatedGroup.id = group.value.id;
        await updateGroup({ input: updatedGroup }, { refetchQueries: [{ query: GET_GROUPS }] });
      }
      context.emit('close', false);
    };

    return {
      loading,
      loadUpdate,
      errUpdate,
      error,
      group,
      close,
      save,
      updatedGroup
    };
  }
});
</script>

<style scoped></style>
