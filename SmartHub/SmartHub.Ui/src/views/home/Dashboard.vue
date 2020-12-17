<template>
  <div class="relative">
    <AppTabs>
      <template #header>
        <!-- Cards -->
        <div class="flex flex-wrap glassmorphism py-2 rounded">
          <CardsRow
            :callback-zero="toggleTabs.bind(this, 0)"
            :callback-one="toggleTabs.bind(this, 1)"
            :callback-two="toggleTabs.bind(this, 2)"
            :callback-three="toggleTabs.bind(this, 3)"
            :callback-four="toggleTabs.bind(this, 4)"
            :open-tab="openTab"
          />
        </div>
        <!-- Expand Arrow -->
        <div v-if="openTab === 0" class="px-4 my-2 flex justify-end cursor-pointer">
          <div class="cursor-pointer flex" @click="expandAdminRow = !expandAdminRow">
            <span v-if="!expandAdminRow" class="text-sm text-gray-700 pr-1">More</span>
            <svg
              v-if="!expandAdminRow"
              class="-mr-1 h-5 w-5"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7" />
            </svg>
          </div>
          <div class="cursor-pointer flex" @click="expandAdminRow = !expandAdminRow">
            <span v-if="expandAdminRow" class="text-sm text-gray-700 pr-1">Hide</span>
            <svg
              v-if="expandAdminRow"
              class="-mr-1 h-5 w-5"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 15l7-7 7 7" />
            </svg>
          </div>
        </div>
        <!-- Admin Cards -->
        <template v-if="roleIncluded(adminRole) && expandAdminRow">
          <div class="flex flex-wrap mb-6 glassmorphism py-2 rounded">
            <AdminCardsRow />
          </div>
        </template>
      </template>
      <template #content>
        <div v-if="openTab === 0">
          <!-- Graphs -->
          <div class="flex flex-wrap glassmorphism py-2 rounded">
            <LineChart></LineChart>
            <BarChart></BarChart>
          </div>
          <!-- Tables -->
          <!-- Expand Arrow -->
          <div class="my-2 flex justify-end">
            <div class="cursor-pointer flex" @click="expandTables = !expandTables">
              <span v-if="!expandTables" class="text-sm text-gray-700 pr-1"> Show Tables</span>
              <svg
                v-if="!expandTables"
                class="-mr-1 h-5 w-5"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7" />
              </svg>
            </div>
            <div class="cursor-pointer flex" @click="expandTables = !expandTables">
              <span v-if="expandTables" class="text-sm text-gray-700 pr-1"> Hide Tables</span>
              <svg
                v-if="expandTables"
                class="-mr-1 h-5 w-5"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 15l7-7 7 7" />
              </svg>
            </div>
          </div>
          <template v-if="expandTables">
            <div class="flex flex-wrap mt-4">
              <div class="w-full xl:w-8/12 mb-12 xl:mb-0 px-4">
                <div
                  class="relative flex flex-col min-w-0 break-words bg-white w-full mb-6 shadow-lg rounded"
                >
                  <div class="rounded-t mb-0 px-4 py-3 border-0">
                    <div class="flex flex-wrap items-center">
                      <div class="relative w-full px-4 max-w-full flex-grow flex-1">
                        <h3 class="font-semibold text-base text-gray-800">Page visits</h3>
                      </div>
                      <div class="relative w-full px-4 max-w-full flex-grow flex-1 text-right">
                        <button
                          class="bg-indigo-500 text-white active:bg-indigo-600 text-xs font-bold uppercase px-3 py-1 rounded outline-none focus:outline-none mr-1 mb-1"
                          type="button"
                          style="transition: all 0.15s ease"
                        >
                          See all
                        </button>
                      </div>
                    </div>
                  </div>
                  <div class="block w-full overflow-x-auto">
                    <!-- Projects table -->
                    <table class="items-center w-full bg-transparent border-collapse">
                      <thead>
                        <tr>
                          <th
                            class="px-6 bg-gray-100 text-gray-600 align-middle border border-solid border-gray-200 py-3 text-xs uppercase border-l-0 border-r-0 whitespace-no-wrap font-semibold text-left"
                          >
                            Page name
                          </th>
                          <th
                            class="px-6 bg-gray-100 text-gray-600 align-middle border border-solid border-gray-200 py-3 text-xs uppercase border-l-0 border-r-0 whitespace-no-wrap font-semibold text-left"
                          >
                            Visitors
                          </th>
                          <th
                            class="px-6 bg-gray-100 text-gray-600 align-middle border border-solid border-gray-200 py-3 text-xs uppercase border-l-0 border-r-0 whitespace-no-wrap font-semibold text-left"
                          >
                            Unique users
                          </th>
                          <th
                            class="px-6 bg-gray-100 text-gray-600 align-middle border border-solid border-gray-200 py-3 text-xs uppercase border-l-0 border-r-0 whitespace-no-wrap font-semibold text-left"
                          >
                            Bounce rate
                          </th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <th
                            class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4 text-left"
                          >
                            /argon/charts.html
                          </th>
                          <td
                            class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                          >
                            3,513
                          </td>
                          <td
                            class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                          >
                            294
                          </td>
                          <td
                            class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                          >
                            <i class="fas fa-arrow-down text-orange-500 mr-4"></i>
                            36,49%
                          </td>
                        </tr>
                        <tr>
                          <th
                            class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4 text-left"
                          >
                            /argon/tables.html
                          </th>
                          <td
                            class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                          >
                            2,050
                          </td>
                          <td
                            class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                          >
                            147
                          </td>
                          <td
                            class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                          >
                            <i class="fas fa-arrow-up text-green-500 mr-4"></i>
                            50,87%
                          </td>
                        </tr>
                        <tr>
                          <th
                            class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4 text-left"
                          >
                            /argon/profile.html
                          </th>
                          <td
                            class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                          >
                            1,795
                          </td>
                          <td
                            class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                          >
                            190
                          </td>
                          <td
                            class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                          >
                            <i class="fas fa-arrow-down text-red-500 mr-4"></i>
                            46,53%
                          </td>
                        </tr>
                      </tbody>
                    </table>
                  </div>
                </div>
              </div>
              <div class="w-full xl:w-4/12 px-4">
                <div
                  class="relative flex flex-col min-w-0 break-words bg-white w-full mb-6 shadow-lg rounded"
                >
                  <div class="rounded-t mb-0 px-4 py-3 border-0">
                    <div class="flex flex-wrap items-center">
                      <div class="relative w-full px-4 max-w-full flex-grow flex-1">
                        <h3 class="font-semibold text-base text-gray-800">Social traffic</h3>
                      </div>
                      <div class="relative w-full px-4 max-w-full flex-grow flex-1 text-right">
                        <button
                          class="bg-indigo-500 text-white active:bg-indigo-600 text-xs font-bold uppercase px-3 py-1 rounded outline-none focus:outline-none mr-1 mb-1"
                          type="button"
                          style="transition: all 0.15s ease"
                        >
                          See all
                        </button>
                      </div>
                    </div>
                  </div>
                  <div class="block w-full overflow-x-auto">
                    <!-- Projects table -->
                    <table class="items-center w-full bg-transparent border-collapse">
                      <thead class="thead-light">
                        <tr>
                          <th
                            class="px-6 bg-gray-100 text-gray-600 align-middle border border-solid border-gray-200 py-3 text-xs uppercase border-l-0 border-r-0 whitespace-no-wrap font-semibold text-left"
                          >
                            Referral
                          </th>
                          <th
                            class="px-6 bg-gray-100 text-gray-600 align-middle border border-solid border-gray-200 py-3 text-xs uppercase border-l-0 border-r-0 whitespace-no-wrap font-semibold text-left"
                          >
                            Visitors
                          </th>
                          <th
                            class="px-6 bg-gray-100 text-gray-600 align-middle border border-solid border-gray-200 py-3 text-xs uppercase border-l-0 border-r-0 whitespace-no-wrap font-semibold text-left"
                            style="min-width: 140px"
                          ></th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <th
                            class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4 text-left"
                          >
                            Google
                          </th>
                          <td
                            class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                          >
                            4,807
                          </td>
                          <td
                            class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                          >
                            <div class="flex items-center">
                              <span class="mr-2">80%</span>
                              <div class="relative w-full">
                                <div class="overflow-hidden h-2 text-xs flex rounded bg-purple-200">
                                  <div
                                    style="width: 80%"
                                    class="shadow-none flex flex-col text-center whitespace-nowrap text-white justify-center bg-purple-500"
                                  ></div>
                                </div>
                              </div>
                            </div>
                          </td>
                        </tr>
                        <tr>
                          <th
                            class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4 text-left"
                          >
                            Instagram
                          </th>
                          <td
                            class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                          >
                            3,678
                          </td>
                          <td
                            class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                          >
                            <div class="flex items-center">
                              <span class="mr-2">75%</span>
                              <div class="relative w-full">
                                <div class="overflow-hidden h-2 text-xs flex rounded bg-blue-200">
                                  <div
                                    style="width: 75%"
                                    class="shadow-none flex flex-col text-center whitespace-nowrap text-white justify-center bg-blue-500"
                                  ></div>
                                </div>
                              </div>
                            </div>
                          </td>
                        </tr>
                      </tbody>
                    </table>
                  </div>
                </div>
              </div>
            </div>
          </template>
        </div>
        <div v-if="openTab === 1">
          <GroupsOverview />
        </div>
        <div v-if="openTab === 2">
          <DevicesOverview />
        </div>
        <div v-if="openTab === 3">
          Users
        </div>
        <div v-if="openTab === 4">
          <AppAutomation />
        </div>
      </template>
    </AppTabs>
  </div>
</template>

<script lang="ts">
import { defineComponent, computed, ref, onMounted } from 'vue';
import AppTabs from '@/components/widgets/AppTabs.vue';
import AppAutomation from '@/components/AutomationsOverview.vue';
import GroupsOverview from '@/components/GroupsOverview.vue';
import DevicesOverview from '@/components/DevicesOverview.vue';
import { useStore } from 'vuex';
import BarChart from '@/components/charts/BarChart.vue';
import LineChart from '@/components/charts/LineChart.vue';
import { getUserRoles } from '@/services/auth/authService';
import { Roles } from '@/types/enums';
import AdminCardsRow from '@/components/admin/AdminCardsRow.vue';
import CardsRow from '@/components/CardsRow.vue';

export default defineComponent({
  name: 'Dashboard',
  components: {
    AdminCardsRow,
    BarChart,
    LineChart,
    CardsRow,
    AppTabs,
    AppAutomation,
    GroupsOverview,
    DevicesOverview
  },
  setup() {
    const openTab = ref(0);
    const store = useStore();
    const appConfig = computed(() => store.state.appModule.appConfig);
    const groups = computed(() => store.state.appModule.groups);
    const devices = computed(() => store.state.appModule.devices);
    const isRole = ref('');
    const adminRole = [Roles.Admin];
    const expandAdminRow = ref(false);
    const expandTables = ref(false);
    const toggleTabs = (tabNumber: number) => {
      openTab.value = tabNumber;
    };
    const roleIncluded = (rolesNeeded: string[]) => rolesNeeded.includes(isRole.value);

    onMounted(() => {
      isRole.value = getUserRoles();
    });
    return {
      openTab,
      toggleTabs,
      appConfig,
      groups,
      devices,
      roleIncluded,
      adminRole,
      expandAdminRow,
      expandTables
    };
  }
});
</script>

<style scoped></style>
