<template>
  <BaseModal
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
        v-model="groupCreateinput.name"
        class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
        placeholder="Group name"
      />
    </label>
    <label class="text-left block text-sm mt-3">
      <span class="text-gray-600 dark:text-gray-400">Description</span>
      <input
        type="text"
        v-model="groupCreateinput.description"
        class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
        placeholder="Group description"
      />
    </label>
    <template v-if="errCreate">
      <div class="flex items-center justify-center w-full h-full">
        <p>Error: {{ errCreate.name }} {{ errCreate.message }}</p>
      </div>
    </template>
  </BaseModal>
</template>

<script lang="ts">
import { defineComponent, reactive, computed, toRefs } from 'vue';
import BaseModal from '@/components/modals/BaseModal.vue';
import { CreateGroupInput } from '@/types/types';
import { useMutation } from '@vue/apollo-composable';
import { CREATE_GROUP } from '@/graphql/mutations';
import { GET_GROUPS, GET_GROUPS_COUNT } from '@/graphql/queries';
// import { postGroup } from '@/services/apis/group';

export default defineComponent({
  name: 'GroupCreateModal',
  emits: ['close'],
  props: {
    parentGroupId: {
      type: String,
      required: false,
      default: ''
    },
    parentGroupName: {
      type: String,
      required: false,
      default: ''
    }
  },
  components: {
    BaseModal
  },
  setup(props, context) {
    const state = reactive({
      title: '',
      groupTitle: 'Create new Group',
      subGroupTitle: 'Create new SubGroup to '
    });
    const groupCreateinput = reactive<CreateGroupInput>({
      name: '',
      isSubGroup: false
    });
    const { mutate: createGroup, loading: loadCreate, error: errCreate } = useMutation(CREATE_GROUP);

    if (props.parentGroupId !== undefined) {
      state.title = state.subGroupTitle + props.parentGroupName;
    } else {
      state.title = state.groupTitle;
    }

    const saveBtnActive = computed(() => groupCreateinput.name !== '');
    const close = () => {
      context.emit('close', false);
    };
    const save = async () => {
      if (props.parentGroupId !== undefined) {
        groupCreateinput.parentGroupId = props.parentGroupId;
        groupCreateinput.isSubGroup = true;
      }
      await createGroup(
        { input: groupCreateinput },
        { refetchQueries: [{ query: GET_GROUPS }, { query: GET_GROUPS_COUNT }] }
      );
      close();
    };
    return {
      ...toRefs(state),
      groupCreateinput,
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
