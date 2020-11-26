<template>
  <BaseModal
    :title="title"
    save-btn-title="Create"
    close-btn-title="Cancel"
    :close="close"
    :save="save"
    :save-btn-active="saveBtnActive"
    header-color="red-400"
    save-btn-color="red-400"
  >
    <label class="text-left block text-sm">
      <span class="text-gray-600 dark:text-gray-400">Name</span>
      <input
        type="text"
        v-model="groupCreateRequest.name"
        class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
        placeholder="Group name"
      />
    </label>
    <label class="text-left block text-sm mt-3">
      <span class="text-gray-600 dark:text-gray-400">Description</span>
      <input
        type="text"
        v-model="groupCreateRequest.description"
        class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
        placeholder="Group description"
      />
    </label>
  </BaseModal>
</template>

<script lang="ts">
import { defineComponent, reactive, computed, toRefs } from 'vue';
import BaseModal from '@/components/modals/BaseModal.vue';
import { GroupCreateRequest } from '@/types/types';
import { postGroup } from '@/services/apis/group';

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
    const groupCreateRequest: GroupCreateRequest = reactive({
      name: '',
      description: '',
      parentGroupId: '',
      isSubGroup: false
    });

    if (props.parentGroupId !== '') {
      state.title = state.subGroupTitle + props.parentGroupName;
    } else {
      state.title = state.groupTitle;
    }

    const saveBtnActive = computed(
      () => groupCreateRequest.name !== '' && groupCreateRequest.description !== ''
    );
    const close = () => {
      context.emit('close', false);
    };
    const save = async () => {
      if (props.parentGroupId !== '') {
        groupCreateRequest.parentGroupId = props.parentGroupId;
        groupCreateRequest.isSubGroup = true;
      }
      await postGroup(groupCreateRequest)
        .then((response) => {
          if (!response.success) {
            return Promise.reject(response.message);
          }
          context.emit('close', false);
          return Promise.resolve();
        })
        .catch((error) => Promise.reject(error));
    };
    return {
      ...toRefs(state),
      groupCreateRequest,
      saveBtnActive,
      close,
      save
    };
  }
});
</script>

<style scoped></style>
