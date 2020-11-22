<template>
  <div v-if="appConfig">
    <h1 class="text-3xl text-gray-500 font-bold mb-4">Dashboard for {{ appConfig?.applicationName }}</h1>
    <AppTabs>
      <template #header>
        <div class="flex flex-wrap justify-between my-4 space-y-2">
          <AppButton
            title="Home"
            :callback="toggleTabs.bind(this, 1)"
            class="mt-2 shadow-sm bg-white"
            :class="{ 'text-ui-primary': openTab !== 1, 'text-white bg-indigo-400': openTab === 1 }"
          />
          <AppButton
            title="Groups"
            color="orange"
            class="shadow-sm bg-white"
            :callback="toggleTabs.bind(this, 2)"
            :class="{ 'text-orange-400': openTab !== 2, 'text-white bg-orange-400': openTab === 2 }"
          />
          <AppButton
            title="Devices"
            color="green"
            class="shadow-sm bg-white"
            :callback="toggleTabs.bind(this, 3)"
            :class="{ 'text-green-400': openTab !== 3, 'text-white bg-green-400': openTab === 3 }"
          />
          <AppButton
            title="Automations"
            color="teal"
            class="shadow-sm bg-white"
            :callback="toggleTabs.bind(this, 4)"
            :class="{ 'text-teal-400': openTab !== 4, 'text-white bg-teal-400': openTab === 4 }"
          />
        </div>
      </template>
      <template #content>
        <div :class="{ hidden: openTab !== 1, block: openTab === 1 }">
          <div class="grid grid-cols-2 gap-6">
            <div class="border-b border-ui-border col-span-2 mb-3">
              <h2
                class="flex justify-start pt-0 mt-0 mb-1 font-bold text-sm tracking-tight uppercase text-gray-500"
              >
                Graphs
              </h2>
            </div>
            <div class="border-b border-ui-border col-span-2">
              <h2
                class="flex justify-start pt-0 mt-0 mb-1 font-bold text-sm tracking-tight uppercase text-gray-500"
              >
                Tables
              </h2>
            </div>
          </div>
        </div>
        <div :class="{ hidden: openTab !== 2, block: openTab === 2 }">
          <AppGroupsOverview />
        </div>
        <div :class="{ hidden: openTab !== 3, block: openTab === 3 }">
          <AppDevicesOverview />
        </div>
        <div :class="{ hidden: openTab !== 4, block: openTab === 4 }">
          <AppAutomation />
        </div>
      </template>
    </AppTabs>
  </div>
  <div v-else>Loading...</div>
</template>

<script lang="ts">
import { defineComponent, computed, ref } from 'vue';
import AppButton from '@/components/widgets/AppButton.vue';
import AppTabs from '@/components/widgets/AppTabs.vue';
import AppAutomation from '@/components/AppAutomations.vue';
import AppGroupsOverview from '@/components/AppGroupsOverview.vue';
import AppDevicesOverview from '@/components/AppDevicesOverview.vue';
import { useStore } from 'vuex';

export default defineComponent({
  name: 'Dashboard',
  components: {
    AppButton,
    AppTabs,
    AppAutomation,
    AppGroupsOverview,
    AppDevicesOverview
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
