<script lang="ts">
import { defineComponent, reactive, computed, toRefs } from 'vue';
import AppModal from '@/components/app/AppModals/AppModal.vue';
import { CreateGroupInput } from '@/graphql/graphql.types';
import { useCreateGroupMutation } from '@/graphql/mutations/groups/createGroup.generated';

export default defineComponent({
  name: 'GroupCreateModal',
  components: {
    AppModal
  },
  emits: ['close'],
  setup(_, context) {
    const state = reactive({
      title: 'Create new Group'
    });
    const groupCreateInput = reactive<CreateGroupInput>({
      name: ''
    });
    const { executeMutation: createGroup, fetching: loadCreate, error: errCreate } = useCreateGroupMutation();

    const saveBtnActive = computed(() => groupCreateInput.name !== '');
    const close = () => {
      context.emit('close', false);
    };
    const save = async () => {
      await createGroup({ input: groupCreateInput });
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
        v-model="groupCreateInput.name"
        type="text"
        class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
        placeholder="Group name"
      />
    </label>
    <label class="text-left block text-sm mt-3">
      <span class="text-gray-600 dark:text-gray-400">Description</span>
      <input
        v-model="groupCreateInput.description"
        type="text"
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
