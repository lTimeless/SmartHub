<template>
  <div>
    <!-- Header -->
    <div class="pb-12">
      <!-- Card stats -->
      <div class="flex flex-wrap">
        <div class="w-full lg:w-6/12 xl:w-3/12 px-4">
          <div class="relative flex flex-col min-w-0 break-words bg-white rounded mb-6 xl:mb-0 shadow-lg">
            <div class="flex-auto p-4">
              <div class="flex flex-wrap">
                <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
                  <h5 class="text-gray-500 uppercase font-bold text-xs">Traffic</h5>
                  <span class="font-semibold text-xl text-gray-800"> 350,897 </span>
                </div>
                <div class="relative w-auto pl-4 flex-initial">
                  <div
                    class="text-white p-3 text-center inline-flex items-center justify-center w-12 h-12 shadow-lg rounded-full bg-red-500"
                  >
                    <i class="far fa-chart-bar"></i>
                  </div>
                </div>
              </div>
              <p class="text-sm text-gray-500 mt-4">
                <span class="text-green-500 mr-2"> <i class="fas fa-arrow-up"></i> 3.48% </span>
                <span class="whitespace-no-wrap"> Since last month </span>
              </p>
            </div>
          </div>
        </div>
        <div class="w-full lg:w-6/12 xl:w-3/12 px-4">
          <div class="relative flex flex-col min-w-0 break-words bg-white rounded mb-6 xl:mb-0 shadow-lg">
            <div class="flex-auto p-4">
              <div class="flex flex-wrap">
                <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
                  <h5 class="text-gray-500 uppercase font-bold text-xs">New users</h5>
                  <span class="font-semibold text-xl text-gray-800"> 2,356 </span>
                </div>
                <div class="relative w-auto pl-4 flex-initial">
                  <div
                    class="text-white p-3 text-center inline-flex items-center justify-center w-12 h-12 shadow-lg rounded-full bg-orange-500"
                  >
                    <i class="fas fa-chart-pie"></i>
                  </div>
                </div>
              </div>
              <p class="text-sm text-gray-500 mt-4">
                <span class="text-red-500 mr-2"> <i class="fas fa-arrow-down"></i> 3.48% </span>
                <span class="whitespace-no-wrap"> Since last week </span>
              </p>
            </div>
          </div>
        </div>
        <div class="w-full lg:w-6/12 xl:w-3/12 px-4">
          <div class="relative flex flex-col min-w-0 break-words bg-white rounded mb-6 xl:mb-0 shadow-lg">
            <div class="flex-auto p-4">
              <div class="flex flex-wrap">
                <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
                  <h5 class="text-gray-500 uppercase font-bold text-xs">Sales</h5>
                  <span class="font-semibold text-xl text-gray-800"> 924 </span>
                </div>
                <div class="relative w-auto pl-4 flex-initial">
                  <div
                    class="text-white p-3 text-center inline-flex items-center justify-center w-12 h-12 shadow-lg rounded-full bg-pink-500"
                  >
                    <i class="fas fa-users"></i>
                  </div>
                </div>
              </div>
              <p class="text-sm text-gray-500 mt-4">
                <span class="text-orange-500 mr-2"> <i class="fas fa-arrow-down"></i> 1.10% </span>
                <span class="whitespace-no-wrap"> Since yesterday </span>
              </p>
            </div>
          </div>
        </div>
        <div class="w-full lg:w-6/12 xl:w-3/12 px-4">
          <div class="relative flex flex-col min-w-0 break-words bg-white rounded mb-6 xl:mb-0 shadow-lg">
            <div class="flex-auto p-4">
              <div class="flex flex-wrap">
                <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
                  <h5 class="text-gray-500 uppercase font-bold text-xs">Performance</h5>
                  <span class="font-semibold text-xl text-gray-800"> 49,65% </span>
                </div>
                <div class="relative w-auto pl-4 flex-initial">
                  <div
                    class="text-white p-3 text-center inline-flex items-center justify-center w-12 h-12 shadow-lg rounded-full bg-blue-500"
                  >
                    <i class="fas fa-percent"></i>
                  </div>
                </div>
              </div>
              <p class="text-sm text-gray-500 mt-4">
                <span class="text-green-500 mr-2"> <i class="fas fa-arrow-up"></i> 12% </span>
                <span class="whitespace-no-wrap"> Since last month </span>
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- Main Page -->
    <!--  Graphs -->
    <div class="flex flex-wrap">
      <LineChart></LineChart>
      <BarChart></BarChart>
    </div>
    <!-- Tables -->
    <div class="flex flex-wrap mt-4">
      <div class="w-full xl:w-8/12 mb-12 xl:mb-0 px-4">
        <div class="relative flex flex-col min-w-0 break-words bg-white w-full mb-6 shadow-lg rounded">
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
                    /argon/
                  </th>
                  <td
                    class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                  >
                    4,569
                  </td>
                  <td
                    class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                  >
                    340
                  </td>
                  <td
                    class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                  >
                    <i class="fas fa-arrow-up text-green-500 mr-4"></i>
                    46,53%
                  </td>
                </tr>
                <tr>
                  <th
                    class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4 text-left"
                  >
                    /argon/index.html
                  </th>
                  <td
                    class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                  >
                    3,985
                  </td>
                  <td
                    class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                  >
                    319
                  </td>
                  <td
                    class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                  >
                    <i class="fas fa-arrow-down text-orange-500 mr-4"></i>
                    46,53%
                  </td>
                </tr>
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
        <div class="relative flex flex-col min-w-0 break-words bg-white w-full mb-6 shadow-lg rounded">
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
                    Facebook
                  </th>
                  <td
                    class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                  >
                    1,480
                  </td>
                  <td
                    class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                  >
                    <div class="flex items-center">
                      <span class="mr-2">60%</span>
                      <div class="relative w-full">
                        <div class="overflow-hidden h-2 text-xs flex rounded bg-red-200">
                          <div
                            style="width: 60%"
                            class="shadow-none flex flex-col text-center whitespace-nowrap text-white justify-center bg-red-500"
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
                    Facebook
                  </th>
                  <td
                    class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                  >
                    5,480
                  </td>
                  <td
                    class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                  >
                    <div class="flex items-center">
                      <span class="mr-2">70%</span>
                      <div class="relative w-full">
                        <div class="overflow-hidden h-2 text-xs flex rounded bg-green-200">
                          <div
                            style="width: 70%"
                            class="shadow-none flex flex-col text-center whitespace-nowrap text-white justify-center bg-green-500"
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
                <tr>
                  <th
                    class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4 text-left"
                  >
                    twitter
                  </th>
                  <td
                    class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                  >
                    2,645
                  </td>
                  <td
                    class="border-t-0 px-6 align-middle border-l-0 border-r-0 text-xs whitespace-no-wrap p-4"
                  >
                    <div class="flex items-center">
                      <span class="mr-2">30%</span>
                      <div class="relative w-full">
                        <div class="overflow-hidden h-2 text-xs flex rounded bg-orange-200">
                          <div
                            style="width: 30%"
                            class="shadow-none flex flex-col text-center whitespace-nowrap text-white justify-center bg-green-500"
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
  </div>

  <!--  <div v-if="appConfig">-->
  <!--    <h1 class="text-3xl text-gray-500 font-bold mb-4">Dashboard for {{ appConfig?.applicationName }}</h1>-->
  <!--    <AppTabs>-->
  <!--      <template #header>-->
  <!--        <div class="flex flex-wrap justify-between my-4 space-y-2">-->
  <!--          <AppButton-->
  <!--            title="Home"-->
  <!--            :callback="toggleTabs.bind(this, 1)"-->
  <!--            class="mt-2 shadow-sm bg-white"-->
  <!--            :class="{ 'text-ui-primary': openTab !== 1, 'text-white bg-indigo-400': openTab === 1 }"-->
  <!--          />-->
  <!--          <AppButton-->
  <!--            title="Groups"-->
  <!--            color="orange"-->
  <!--            class="shadow-sm bg-white"-->
  <!--            :callback="toggleTabs.bind(this, 2)"-->
  <!--            :class="{ 'text-orange-400': openTab !== 2, 'text-white bg-orange-400': openTab === 2 }"-->
  <!--          />-->
  <!--          <AppButton-->
  <!--            title="Devices"-->
  <!--            color="green"-->
  <!--            class="shadow-sm bg-white"-->
  <!--            :callback="toggleTabs.bind(this, 3)"-->
  <!--            :class="{ 'text-green-400': openTab !== 3, 'text-white bg-green-400': openTab === 3 }"-->
  <!--          />-->
  <!--          <AppButton-->
  <!--            title="Automations"-->
  <!--            color="teal"-->
  <!--            class="shadow-sm bg-white"-->
  <!--            :callback="toggleTabs.bind(this, 4)"-->
  <!--            :class="{ 'text-teal-400': openTab !== 4, 'text-white bg-teal-400': openTab === 4 }"-->
  <!--          />-->
  <!--        </div>-->
  <!--      </template>-->
  <!--      <template #content>-->
  <!--        <div :class="{ hidden: openTab !== 1, block: openTab === 1 }">-->
  <!--          <div class="grid grid-cols-2 gap-6">-->
  <!--            <div class="border-b border-ui-border col-span-2 mb-3">-->
  <!--              <h2-->
  <!--                class="flex justify-start pt-0 mt-0 mb-1 font-bold text-sm tracking-tight uppercase text-gray-500"-->
  <!--              >-->
  <!--                Graphs-->
  <!--              </h2>-->
  <!--            </div>-->
  <!--            <div class="border-b border-ui-border col-span-2">-->
  <!--              <h2-->
  <!--                class="flex justify-start pt-0 mt-0 mb-1 font-bold text-sm tracking-tight uppercase text-gray-500"-->
  <!--              >-->
  <!--                Tables-->
  <!--              </h2>-->
  <!--            </div>-->
  <!--          </div>-->
  <!--        </div>-->
  <!--        <div :class="{ hidden: openTab !== 2, block: openTab === 2 }">-->
  <!--          <AppGroupsOverview />-->
  <!--        </div>-->
  <!--        <div :class="{ hidden: openTab !== 3, block: openTab === 3 }">-->
  <!--          <AppDevicesOverview />-->
  <!--        </div>-->
  <!--        <div :class="{ hidden: openTab !== 4, block: openTab === 4 }">-->
  <!--          <AppAutomation />-->
  <!--        </div>-->
  <!--      </template>-->
  <!--    </AppTabs>-->
  <!--  </div>-->
  <!--  <div v-else>Loading...</div>-->
</template>

<script lang="ts">
import { defineComponent, computed, ref } from 'vue';
import AppButton from '@/components/widgets/AppButton.vue';
import AppTabs from '@/components/widgets/AppTabs.vue';
import AppAutomation from '@/components/AppAutomations.vue';
import AppGroupsOverview from '@/components/AppGroupsOverview.vue';
import AppDevicesOverview from '@/components/AppDevicesOverview.vue';
import { useStore } from 'vuex';
import BarChart from '@/components/layout/BarChart.vue';
import LineChart from '@/components/layout/LineChart.vue';

export default defineComponent({
  name: 'Dashboard',
  components: {
    BarChart,
    LineChart
    // AppButton,
    // AppTabs,
    // AppAutomation,
    // AppGroupsOverview,
    // AppDevicesOverview
  },
  setup() {
    const openTab = ref(1);
    const store = useStore();
    const appConfig = computed(() => store.state.appModule.appConfig);
    const groups = computed(() => store.state.appModule.groups);
    const devices = computed(() => store.state.appModule.devices);
    const toggleTabs = (tabNumber: number) => {
      openTab.value = tabNumber;
    };

    return {
      openTab,
      toggleTabs,
      appConfig,
      groups,
      devices
    };
  }
});
</script>

<style scoped></style>
