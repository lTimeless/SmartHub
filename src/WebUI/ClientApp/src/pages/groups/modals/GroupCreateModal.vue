<template>
  <AppModal
    :title="title"
    save-btn-title="Create"
    close-btn-title="Cancel"
    :close="close"
    :save="save"
    :save-btn-active="saveBtnActive"
    main-border-color="border-red-400"
    main-bg-color="bg-red-400"
    :loading="loadCreate"
  >
    <label class="text-left block text-sm">
      <span class="text-gray-600 dark:text-gray-400">Name</span>
      <input
        type="text"
        v-model="groupCreateInput.name"
        class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
        placeholder="Group name"
      />
    </label>
    <label class="text-left block text-sm mt-3">
      <span class="text-gray-600 dark:text-gray-400">Description</span>
      <input
        type="text"
        v-model="groupCreateInput.description"
        class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
        placeholder="Group description"
      />
    </label>
    <template v-if="errCreate">
      <div class="flex items-center justify-center w-full h-full">
        <p>Error: {{ errCreate.name }} {{ errCreate.message }}</p>
      </div>
    </template>
  </AppModal>
</template>

<script lang="ts">
import { defineComponent, reactive, computed, toRefs } from 'vue';
import AppModal from '@/components/ui/modals/AppModal.vue';
import { CreateGroupInput } from '@/types/types';
import { useMutation } from '@urql/vue';
import { CREATE_GROUP } from '../GroupMutations';
import { GET_GROUPS } from '../GroupQueries';
import { GET_GROUPS_COUNT } from '@/pages/home/HomeQueries';

export default defineComponent({
  name: 'GroupCreateModal',
  emits: ['close'],
  components: {
    AppModal
  },
  setup(props, context) {
    const state = reactive({
      title: 'Create new Group'
    });
    const groupCreateInput = reactive<CreateGroupInput>({
      name: ''
    });
    const { executeMutation: createGroup, fetching: loadCreate, error: errCreate } = useMutation(
      CREATE_GROUP
    );

    const saveBtnActive = computed(() => groupCreateInput.name !== '');
    const close = () => {
      context.emit('close', false);
    };
    const save = async () => {
      await createGroup(
        { input: groupCreateInput },
        { refetchQueries: [{ query: GET_GROUPS }, { query: GET_GROUPS_COUNT }] }
      );
      close();
    };
    return {
      ...toRefs(state),
      groupCreateInput,
      saveBtnActive,
      loadCreate,
      errCreate,
      close,
      save
    };
  }
});
</script>

<style scoped></style>
