<template>
  <BaseModal title="Group details" saveBtnTitle="Save" closeBtnTitle="Cancel" :close="close" :save="save" headerColor="bg-orange-400">
    <label class="text-left block text-sm">
      <span class="text-gray-600 dark:text-gray-400">Name</span>
      <input
        v-model="deviceDetail.name"
        class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
            focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
        placeholder="Group name"
      />
    </label>
    <label class="text-left block text-sm mt-3">
      <span class="text-gray-600 dark:text-gray-400">Description</span>
      <input
        v-model="deviceDetail.description"
        class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
            focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
        placeholder="Group description"
      />
    </label>
    <div class="border-ui-border border-t my-2"></div>

    <div class="text-gray-500 text-sm text-left mt-10">Creator: {{ deviceDetail.createdBy }}</div>
  </BaseModal>
</template>

<script lang="ts">
import { defineComponent, PropType, ref } from 'vue';
import BaseModal from '@/components/modals/BaseModal.vue';
import { Device } from '@/types/types';
import { HomeActionTypes } from '@/store/home/actions';
import { useStore } from 'vuex';

export default defineComponent({
  name: 'DeviceDetailsModal',
  emits: ['close-modal'],
  components: {
    BaseModal
  },
  props: {
    group: {
      type: Object as PropType<Device>,
      required: true
    }
  },
  setup(props, context) {
    const store = useStore();
    const deviceDetail = ref(props.group);
    const close = () => {
      context.emit('close-modal', false);
    };

    const save = async () => {
      //   const updatedGroup: GroupUpdateRequest = {
      //     id: groupDetail.value.id,
      //     name: groupDetail.value.name,
      //     description: groupDetail.value.description,
      //     devices: groupDetail.value.devices
      //   };
      console.log('Update device');
      //   await store.dispatch(HomeActionTypes.UPDATE_GROUP, updatedGroup);
      context.emit('close-modal', false);
    };

    return {
      deviceDetail,
      close,
      save
    };
  }
});
</script>

<style scoped></style>
