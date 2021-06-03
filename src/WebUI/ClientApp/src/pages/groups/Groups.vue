<script lang="ts">
import { reactive, toRefs, defineComponent } from 'vue';
import AppCard from '@/components/app/AppCards/AppCard.vue';
import Loader from '@/components/app/AppSpinner.vue';
import { useRouter } from 'vue-router';
import GroupCreateModal from './modals/GroupCreateModal.vue';
import { useGetGroupsQuery } from '@/graphql/queries/groups/getGroups.generated';

export default defineComponent({
  name: 'Groups',
  components: {
    AppCard,
    GroupCreateModal,
    // GroupDropdown,
    Loader
  },
  setup() {
    const router = useRouter();
    const { data, fetching: loading, error } = useGetGroupsQuery();
    const state = reactive({
      showCreateModal: false,
      showDetailModal: false,
      groupId: '',
      closeDropdown: false
    });

    const toggleCreateModal = () => {
      state.showCreateModal = !state.showCreateModal;
    };

    const goToDetail = (name: string) => {
      router.push({ name: 'GroupDetail', params: { name } });
    };

    return {
      data,
      loading,
      error,
      ...toRefs(state),
      toggleCreateModal,
      goToDetail
    };
  }
});
</script>

<template>
  <div>
    <GroupCreateModal v-if="showCreateModal" @close="toggleCreateModal()" />
    <!-- Buttons -->
    <div class="w-full">
      <div class="flex justify-start items-center mb-4 xl:w-1/3">
        <!-- Add button -->
        <div class="w-full md:w-1/3 xl:w-3/6">
          <button
            class="block w-full px-4 py-2 mt-4 text-sm text-gray-600 font-medium leading-5 text-center bg-white hover:text-white hover:bg-indigo-400 border border-transparent rounded-lg active:bg-primary focus:outline-none"
            @click="toggleCreateModal"
          >
            Add Group
          </button>
        </div>
      </div>
    </div>

    <!-- Show groups -->
    <template v-if="error">
      <p>Error: {{ error.name }} - {{ error.message }}</p>
    </template>
    <template v-else-if="loading">
      <Loader height="h-48" width="w-48" />
    </template>
    <template v-else-if="data">
      <!-- All groups -->
      <div class="pb-4 h-114 rounded">
        <div v-if="data.groups" class="grid xl:grid-cols-3 md:grid-cols-2 sm:grid-cols-1 gap-4 rounded">
          <AppCard
            v-for="group in data.groups"
            :key="group.id"
            class="bg-white border hover:border-indigo-400 w-full"
          >
            <div class="p-3 w-full">
              <div class="flex items-start justify-between">
                <h1
                  class="text-xl text-left text-gray-600 font-bold cursor-pointer"
                  @click="goToDetail(group.name)"
                >
                  {{ group.name }}
                </h1>
                <!-- <GroupDropdown v-if="!group.isSubGroup" :group-id="group.id" :group-name="group.name" /> -->
                <!-- <span v-else class="text-gray-400 text-xs text-left mt-2">Is Subgroup </span> -->
              </div>
              <div class="text-gray-500 text-sm font-normal text-left">
                Creator: <span class="font-bold">{{ group.createdBy }}</span>
              </div>
              <div class="border-border border-t my-2" />
              <!-- Show available devices -->
              <template v-if="group.devices !== undefined">
                <div class="text-left">
                  <div>
                    <span class="text-gray-500 text-sm text-left mt-2">
                      {{ group.devices.length }} device{{ group.devices.length > 1 ? 's' : '' }}
                    </span>
                  </div>
                </div>
              </template>
            </div>
          </AppCard>
        </div>
        <div v-else>No groups</div>
      </div>
    </template>
  </div>
</template>
