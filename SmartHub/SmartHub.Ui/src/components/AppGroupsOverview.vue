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
    <div class="flex justify-start items-center mb-4 xl:w-1/3">
      <div class="w-full md:w-1/3 xl:w-3/6">
        <button
          @click="toggleModal(true, null, null)"
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
  <div v-if="groupsWithSubGroups && groupsWithSubGroups.length > 0">
    <!-- All Groups -->
    <template v-if="showSubGroupsIcon">
      <div>
        <div class="grid xl:grid-cols-3 md:grid-cols-2 sm:grid-cols-1 gap-4">
          <AppCard class="bg-white shadow-md w-full" v-for="group in groupsWithSubGroups" :key="group.id">
            <div class="p-3 w-full">
              <div class="flex items-start justify-between">
                <h1
                  class="text-xl text-left text-gray-600 font-bold cursor-pointer"
                  @click="openDetailModal(true, group.id)"
                >
                  {{ group.name }}
                </h1>
                <GroupDropdown
                  v-if="!group.isSubGroup"
                  :add-sub-group="toggleModal.bind(this, true, group.id, group.name)"
                  :close-drop-down="closeDropdown"
                />
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
      </div>
    </template>
    <!-- Only Parent Groups  -->
    <template v-if="!showSubGroupsIcon">
      <div v-if="onlyParentGroups && onlyParentGroups.length > 0">
        <div class="grid xl:grid-cols-3 md:grid-cols-2 sm:grid-cols-1 gap-4">
          <AppCard class="bg-white shadow-md w-full" v-for="group in onlyParentGroups" :key="group.id">
            <div class="p-3 w-full">
              <div class="flex items-start justify-between">
                <h1
                  class="text-xl text-left text-gray-600 font-bold cursor-pointer"
                  @click="openDetailModal(true, group.id)"
                >
                  {{ group.name }}
                </h1>
                <GroupDropdown
                  v-if="!group.isSubGroup"
                  :add-sub-group="toggleModal.bind(this, true, group.id, group.name)"
                  :close-drop-down="closeDropdown"
                />
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
      </div>
    </template>
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
// import { getByIdGroup } from '@/services/apis/group';
import GroupDropdown from '@/components/GroupDropdown.vue';

export default defineComponent({
  name: 'AppGroupsOverview',
  components: {
    AppCard,
    GroupCreateModal,
    GroupDetailsModal,
    GroupDropdown
  },
  setup() {
    const store = useStore();
    const groupsWithSubGroups = computed(() => store.state.appModule.groups);
    const onlyParentGroups = computed(() => store.getters.getOnlyParentGroups);
    const state = reactive({
      showAddModal: false,
      showDetailModal: false,
      showSubGroupsIcon: false,
      selectedGroupId: '',
      group: {} as Group | null | undefined,
      showLoader: false,
      parentGroupName: '',
      parentGroupId: '',
      closeDropdown: false
    });
    const toggleModal = (value: boolean, parentGroupId: string | null, parentGroupName: string | null) => {
      state.parentGroupId = parentGroupId ?? '';
      state.parentGroupName = parentGroupName ?? '';
      state.showAddModal = value;
      state.closeDropdown = value;
    };
    const closeDetailsModal = (value: boolean) => {
      state.showDetailModal = value;
    };

    const showSubGroups = () => {
      state.showSubGroupsIcon = !state.showSubGroupsIcon;
    };

    const openDetailModal = async (value: boolean, groupId: string) => {
      state.showLoader = true;
      if (value) {
        // state.group = await getByIdGroup(groupId)
        //   .then((response) => {
        //     if (!response.success) {
        //       return Promise.reject(response.message);
        //     }
        //     return Promise.resolve(response.data as Group);
        //   })
        //   .catch((error) => Promise.reject(error));
      }
      state.showLoader = false;
      state.selectedGroupId = groupId;
      state.showDetailModal = value;
    };

    return {
      ...toRefs(state),
      toggleModal,
      openDetailModal,
      closeDetailsModal,
      showSubGroups,
      onlyParentGroups,
      groupsWithSubGroups
    };
  }
});
</script>

<style lang="scss" scoped></style>
