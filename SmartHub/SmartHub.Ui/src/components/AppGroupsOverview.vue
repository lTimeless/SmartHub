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
          class="block w-full px-4 py-2 mt-4 text-sm text-gray-500 font-medium leading-5 text-center bg-white hover:text-white transition-colors duration-150 hover:bg-indigo-500 border border-transparent rounded-lg active:bg-ui-primary focus:outline-none focus:shadow-outlineIndigo"
        >
          Add Group
        </button>
      </div>
    </div>
  </div>
  <!-- All Groups -->
  <div v-if="groups && groups.length > 0">
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
              class="rounded-full bg-transparent border-0 w-6 h-6 outline-none focus:outline-none"
              @click="toggleModal(true, group.id, group.name)"
            >
              <svg
                class="-mr-1 h-5 w-5"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  stroke-width="2"
                  d="M9 13h6m-3-3v6m-9 1V7a2 2 0 012-2h6l2 2h6a2 2 0 012 2v8a2 2 0 01-2 2H5a2 2 0 01-2-2z"
                />
              </svg>
            </button>
          </div>

          <div class="text-gray-500 text-sm font-normal text-left">
            Creator: <span class="font-bold">{{ group.createdBy }}</span>
          </div>
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
import { Group } from '@/types/types';
import { getByIdGroup } from '@/services/apis/group';

export default defineComponent({
  name: 'AppGroupsOverview',
  components: {
    AppCard,
    GroupCreateModal,
    GroupDetailsModal
  },
  setup() {
    const store = useStore();
    const noSubGroups = computed(() => store.state.appModule.groups);
    const groups = computed(() => noSubGroups.value?.filter((x: Group) => !x.isSubGroup));
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
        state.group = await getByIdGroup(groupId)
          .then((response) => {
            if (!response.success) {
              return Promise.reject(response.message);
            }
            return Promise.resolve(response.data as Group);
          })
          .catch((error) => Promise.reject(error));
      }
      state.showLoader = false;
      state.selectedGroupId = groupId;
      state.showDetailModal = value;
    };

    return {
      ...toRefs(state),
      groups,
      toggleModal,
      openDetailModal,
      closeDetailsModal
    };
  }
});
</script>

<style lang="scss" scoped></style>
