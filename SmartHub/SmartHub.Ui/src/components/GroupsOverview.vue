<template>
  <GroupCreateModal v-if="showCreateModal" @close="toggleCreateModal" />
  <GroupDetailsModal v-if="showDetailModal" @close="toggleDetailModal(null)" :group-id="groupId" />

  <!-- Add Group button -->
  <div class="w-full">
    <div class="flex justify-start items-center mb-4 xl:w-1/3">
      <div class="w-full md:w-1/3 xl:w-3/6">
        <button
          @click="toggleCreateModal"
          class="block w-full px-4 py-2 mt-4 text-sm text-gray-500 font-medium leading-5 text-center bg-white hover:text-white hover:bg-indigo-500 border border-transparent rounded-lg active:bg-primary focus:outline-none"
        >
          Add Group
        </button>
      </div>
      <div class="w-full md:w-1/3 xl:w-2/6 ml-2">
        <button
          v-if="showSubGroupsIcon"
          @click="showSubGroups()"
          class="flex justify-center content-center px-2 py-2 mt-4 text-gray-500 bg-transparent border border-transparent rounded-lg focus:outline-none"
        >
          <svg
            class="h-6 w-6"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M13.875 18.825A10.05 10.05 0 0112 19c-4.478 0-8.268-2.943-9.543-7a9.97 9.97 0 011.563-3.029m5.858.908a3 3 0 114.243 4.243M9.878 9.878l4.242 4.242M9.88 9.88l-3.29-3.29m7.532 7.532l3.29 3.29M3 3l3.59 3.59m0 0A9.953 9.953 0 0112 5c4.478 0 8.268 2.943 9.543 7a10.025 10.025 0 01-4.132 5.411m0 0L21 21"
            />
          </svg>
        </button>
        <button
          v-if="!showSubGroupsIcon"
          @click="showSubGroups()"
          class="flex justify-center content-center px-2 py-2 mt-4 text-gray-500 bg-transparent border border-transparent rounded-lg focus:outline-none"
        >
          <svg
            class="h-6 w-6"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"
            />
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z"
            />
          </svg>
        </button>
      </div>
    </div>
  </div>

  <!-- Show Groups -->
  <template v-if="error">
    <p>Error: {{ error.name }} - {{ error.message }}</p>
  </template>
  <template v-else-if="loading">
    <Loader height="h-48" width="w-48" />
  </template>
  <template v-else-if="groups">
    <!-- All Groups -->
    <div v-if="groups.length > 0" class="grid xl:grid-cols-3 md:grid-cols-2 sm:grid-cols-1 gap-4">
      <AppCard class="bg-white shadow-md w-full" v-for="group in groups" :key="group.id">
        <div class="p-3 w-full">
          <div class="flex items-start justify-between">
            <h1
              class="text-xl text-left text-gray-600 font-bold cursor-pointer"
              @click="toggleDetailModal(group.id)"
            >
              {{ group.name }}
            </h1>
            <GroupDropdown v-if="!group.isSubGroup" :group-id="group.id" :group-name="group.name" />
            <span v-else class="text-gray-400 text-xs text-left mt-2">Is Subgroup </span>
          </div>
          <div class="text-gray-500 text-sm font-normal text-left">
            Creator: <span class="font-bold">{{ group.createdBy }}</span>
          </div>
          <div class="border-border border-t my-2"></div>
          <!-- Show available subGroups -->
          <template v-if="!group.isSubGroup">
            <template v-if="group.subGroups !== undefined && group.subGroups.length > 0">
              <div class="text-left">
                <div>
                  <span class="text-gray-500 text-sm text-left mt-2">SubGroups</span>
                </div>
                <!-- Show available subGroup devices-->
                <div v-for="subgroup in group.subGroups" :key="subgroup.id" class="pl-3">
                  {{ subgroup.name }}
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
    <div v-else>No groups available</div>
  </template>
</template>

<script lang="ts">
import { reactive, toRefs, defineComponent, ref, watch } from 'vue';
import AppCard from '@/components/widgets/AppCard.vue';
import GroupCreateModal from '@/components/modals/GroupCreateModal.vue';
import GroupDetailsModal from '@/components/modals/GroupDetailsModal.vue';
import GroupDropdown from '@/components/GroupDropdown.vue';
import { useQuery, useResult } from '@vue/apollo-composable';
import { GET_GROUPS } from '@/graphql/queries';
import { Group } from '@/types/types';
import Loader from '@/components/Loader.vue';

export default defineComponent({
  name: 'GroupsOverview',
  components: {
    AppCard,
    GroupCreateModal,
    GroupDetailsModal,
    GroupDropdown,
    Loader
  },
  setup() {
    const { result, loading, error } = useQuery(GET_GROUPS);
    const groupsResult = useResult(result, null, (data) => data.groups);
    const tempGroups = ref();
    const groups = ref();
    const state = reactive({
      showCreateModal: false,
      showDetailModal: false,
      showSubGroupsIcon: false,
      groupId: '',
      closeDropdown: false
    });

    watch(
      groupsResult,
      (newValue) => {
        tempGroups.value = newValue;
        groups.value = newValue;
      },
      { immediate: true }
    );

    const toggleCreateModal = () => {
      state.showCreateModal = !state.showCreateModal;
    };

    const toggleDetailModal = (groupId: string | null) => {
      state.showDetailModal = !state.showDetailModal;
      if (state.showDetailModal && groupId !== null) {
        state.groupId = groupId;
      } else {
        state.groupId = '';
      }
    };

    const showSubGroups = () => {
      state.showSubGroupsIcon = !state.showSubGroupsIcon;
      if (state.showSubGroupsIcon) {
        groups.value = groups.value?.filter((x: Group) => x.isSubGroup);
      } else {
        groups.value = tempGroups.value;
      }
      console.log(groups.value);
    };

    return {
      loading,
      error,
      ...toRefs(state),
      toggleCreateModal,
      toggleDetailModal,
      showSubGroups,
      groups
    };
  }
});
</script>

<style lang="scss" scoped></style>
