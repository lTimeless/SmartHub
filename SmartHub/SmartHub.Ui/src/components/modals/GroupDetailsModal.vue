<template>
  <BaseModal
    title="Group details"
    save-btn-title="Save"
    close-btn-title="Cancel"
    :close="close"
    :save="save"
    header-color="bg-orange-400"
  >
    <label class="text-left block text-sm">
      <span class="text-gray-600 dark:text-gray-400">Name</span>
      <input
        v-model="groupDetail.name"
        class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400 focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
        placeholder="Group name"
      />
    </label>
    <label class="text-left block text-sm mt-3">
      <span class="text-gray-600 dark:text-gray-400">Description</span>
      <input
        v-model="groupDetail.description"
        class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400 focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
        placeholder="Group description"
      />
    </label>
    <div class="border-ui-border border-t my-2"></div>
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
    <div class="text-gray-500 text-sm text-left mt-10">Creator: {{ groupDetail.createdBy }}</div>
  </BaseModal>
</template>

<script lang="ts">
import { defineComponent, PropType, ref } from 'vue';
import BaseModal from '@/components/modals/BaseModal.vue';
import { Group, GroupUpdateRequest } from '@/types/types';
import { HomeActionTypes } from '@/store/home/actions';
import { useStore } from 'vuex';

export default defineComponent({
  name: 'GroupDetailsModal',
  emits: ['close'],
  components: {
    BaseModal
  },
  props: {
    group: {
      type: Object as PropType<Group>,
      required: true
    }
  },
  setup(props, context) {
    const store = useStore();
    const groupDetail = ref(props.group);
    const close = () => {
      context.emit('close', false);
    };

    const save = async () => {
      const updatedGroup: GroupUpdateRequest = {
        id: groupDetail.value.id,
        name: groupDetail.value.name,
        description: groupDetail.value.description,
        devices: groupDetail.value.devices
      };
      await store.dispatch(HomeActionTypes.UPDATE_GROUP, updatedGroup);
      context.emit('close', false);
    };

    return {
      groupDetail,
      close,
      save
    };
  }
});
</script>

<style scoped></style>
