<template>
  <GroupCreateModal
    v-if="showAddModal"
    @close="toggleModal"
    :parent-group-id="parentGroupId"
    :parent-group-name="parentGroupName"
  />
  <GroupDetailsModal v-if="showDetailModal" @close="closeDetailsModal" :group="group" />

  <!-- Add Group button -->
  <div class="w-full">
    <div class="flex justify-between items-center mb-4">
      <div class="flex justify-start w-full md:w-1/3 xl:w-1/6">
        <button
          @click="toggleModal(true, null, null)"
          class="flex justify-center items-center font-bold border border-ui-border rounded-lg bg-gray-400 hover:text-white transition-colors hover:bg-orange-400 h-10 w-full"
        >
          Add Group
        </button>
      </div>
    </div>
  </div>
  <!-- All Groups -->
  <div v-if="home && home.groups">
    <div class="grid xl:grid-cols-3 md:grid-cols-2 sm:grid-cols-1 gap-4">
      <AppCard class="bg-white shadow-md w-full" v-for="group in groups" :key="group.id">
        <div class="p-3 w-full">
          <div class="flex items-start justify-between">
            <h1
              class="text-xl text-left text-gray-600 font-bold cursor-pointer"
              @click="openDetailModal(true, group.id)"
            >
              {{ group.name }}
            </h1>
            <button
              class="flex justify-center p-1 ml-auto rounded-full bg-transparent border-0 text-black opacity-5 float-right text-3xl leading-none font-semibold outline-none focus:outline-none hover:bg-gray-300"
              @click="toggleModal(true, group.id, group.name)"
            >
              <span
                class="text-center bg-transparent text-black opacity-5 h-6 w-6 text-2xl block outline-none focus:outline-none transform rotate-90"
              >
                ...
              </span>
            </button>
          </div>

          <div class="text-gray-500 text-sm font-normal text-left">
            Creator: <span class="font-bold">{{ group.createdBy }}</span>
          </div>
          <div class="border-ui-border border-t my-2"></div>

          <!-- Show available devices -->
          <template v-if="group.devices !== undefined && group.devices.length > 0">
            <div v-for="device in group.devices" :key="device.id">
              {{ device.name }}
            </div>
          </template>
          <template v-else>
            <div>
              <span class="text-gray-500 text-sm text-left mt-2">No devices available</span>
            </div>
          </template>
        </div>
      </AppCard>
    </div>
  </div>
  <div v-else>No Groups available</div>
</template>

<script lang="ts">
import { reactive, toRefs, defineComponent, computed } from 'vue';
import AppCard from '@/components/widgets/AppCard.vue';
import { useStore } from 'vuex';
import GroupCreateModal from '@/components/modals/GroupCreateModal.vue';
import GroupDetailsModal from '@/components/modals/GroupDetailsModal.vue';
import { HomeActionTypes } from '@/store/home/actions';
import { Group } from '@/types/types';

export default defineComponent({
  name: 'AppGroupsOverview',
  components: {
    AppCard,
    GroupCreateModal,
    GroupDetailsModal
  },
  setup() {
    const store = useStore();
    const home = computed(() => store.state.homeModule.home);
    const groups = computed(() => home.value?.groups?.filter((x: Group) => !x.isSubGroup));
    const state = reactive({
      showAddModal: false,
      showDetailModal: false,
      selectedGroupId: '',
      group: {} as Group | null | undefined,
      showLoader: false,
      parentGroupName: '',
      parentGroupId: ''
    });
    const toggleModal = (value: boolean, parentGroupId: string | null, parentGroupName: string | null) => {
      state.parentGroupId = parentGroupId ?? '';
      state.parentGroupName = parentGroupName ?? '';
      state.showAddModal = value;
    };
    const closeDetailsModal = (value: boolean) => {
      state.showDetailModal = value;
    };

    const openDetailModal = async (value: boolean, groupId: string) => {
      state.showLoader = true;
      if (value) {
        await store.dispatch(HomeActionTypes.FETCH_BY_GROUP_ID, groupId).then((response: Group | null) => {
          state.group = response;
        });
      }
      state.showLoader = false;
      state.selectedGroupId = groupId;
      state.showDetailModal = value;
    };

    return {
      ...toRefs(state),
      home,
      groups,
      toggleModal,
      openDetailModal,
      closeDetailsModal
    };
  }
});
</script>

<style lang="scss" scoped></style>
