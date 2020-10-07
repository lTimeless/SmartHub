<template>
  <BaseModal title="Create new Group" saveBtnTitle="Create" closeBtnTitle="Cancel" :close="close" :save="save" :saveBtnActive="saveBtnActive" headerColor="bg-orange-400">
    <label class="text-left block text-sm">
      <span class="text-gray-600 dark:text-gray-400">Name</span>
      <input
        v-model="groupCreateRequest.name"
        class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
            focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
        placeholder="Group name"
      />
    </label>
    <label class="text-left block text-sm mt-3">
      <span class="text-gray-600 dark:text-gray-400">Description</span>
      <input
        v-model="groupCreateRequest.description"
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
import { GroupCreateRequest } from '@/types/types';
import { useStore } from 'vuex';
import { HomeActionTypes } from '@/store/home/actions';

export default defineComponent({
  name: 'GroupCreateModal',
  emits: ['close'],
  components: {
    BaseModal
  },
  setup(props, context) {
    const store = useStore();
    const groupCreateRequest: GroupCreateRequest = reactive({
      name: '',
      description: ''
    });
    const saveBtnActive = computed(() => groupCreateRequest.name !== '' && groupCreateRequest.description !== '');
    const close = () => {
      context.emit('close', false);
    };
    const save = async () => {
      await store.dispatch(HomeActionTypes.CREATE_GROUP, groupCreateRequest).then(() => {
        context.emit('close', false);
      });
    };
    return {
      groupCreateRequest,
      saveBtnActive,
      close,
      save
    };
  }
});
</script>

<style scoped></style>
