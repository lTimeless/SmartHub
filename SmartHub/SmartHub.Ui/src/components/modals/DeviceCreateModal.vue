<template>
  <BaseModal
    title="Create new Device"
    saveBtnTitle="Create"
    closeBtnTitle="Cancel"
    :close="close"
    :save="save"
    :saveBtnActive="saveBtnActive"
    headerColor="bg-orange-400"
  >
    <label class="text-left block text-sm">
      <span class="text-gray-600 dark:text-gray-400">Name</span>
      <input
        v-model="deviceCreateRequest.name"
        class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
            focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
        placeholder="Group name"
      />
    </label>
    <label class="text-left block text-sm mt-3">
      <span class="text-gray-600 dark:text-gray-400">Description</span>
      <input
        v-model="deviceCreateRequest.description"
        class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
            focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
        placeholder="Group description"
      />
    </label>
  </BaseModal>
</template>

<script lang="ts">
import { defineComponent, reactive, computed } from 'vue';
import BaseModal from '@/components/modals/BaseModal.vue';
import { DeviceCreateRequest } from '@/types/types';
import { useStore } from 'vuex';
import { ConnectionTypes, PluginTypes } from '@/types/enums';

export default defineComponent({
  name: 'DeviceCreateModal',
  emits: ['close-modal'],
  components: {
    BaseModal
  },
  setup(props, context) {
    const store = useStore();
    const deviceCreateRequest: DeviceCreateRequest = reactive({
      name: '',
      description: '',
      groupId: '',
      ipv4: '',
      companyName: '',
      pluginName: '',
      pluginTypes: PluginTypes.None,
      primaryConnection: ConnectionTypes.None,
      secondaryConnection: ConnectionTypes.None
    });
    const saveBtnActive = computed(() => deviceCreateRequest.name !== '' && deviceCreateRequest.description !== '');
    const close = () => {
      context.emit('close-modal', false);
    };
    const save = async () => {
      console.log('click save', deviceCreateRequest);
      //         await store.dispatch(HomeActionTypes.CREATE_GROUP, groupCreateRequest).then(() => {
      //     context.emit('close-modal', false);
      //   });
      context.emit('close-modal', false);
    };
    return {
      deviceCreateRequest,
      saveBtnActive,
      close,
      save
    };
  }
});
</script>

<style scoped></style>
