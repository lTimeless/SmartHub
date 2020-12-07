<template>
  <main>
    <aside>
      <Sidebar />
    </aside>
    <div class="relative md:ml-64 bg-gray-200 min-h-screen">
      <div class="absolute mx-auto w-full pt-0 z-0">
        <TopDoubleWaves />
      </div>
      <div class="md:pt-28 pt-12 px-4 md:px-10 mx-auto w-full z-10">
        <Navbar />
        <router-view v-slot="{ Component }">
          <transition name="route" mode="out-in">
            <component :is="Component" />
          </transition>
        </router-view>
      </div>
    </div>
  </main>
</template>

<script lang="ts">
import { defineComponent, watch } from 'vue';
import { useSignalRHub } from '@/hooks/useSignalR.ts';
import { useStore } from 'vuex';
import { Group, Device, AppConfig } from '@/types/types';
import Sidebar from '@/components/layout/Sidebar.vue';
import Navbar from '@/components/layout/Navbar.vue';
import TopDoubleWaves from '@/components/svgs/TopDoubleWaves.vue';
import { AppActionTypes } from '@/store/app/actions';

export default defineComponent({
  name: 'Home',
  components: {
    Navbar,
    Sidebar,
    TopDoubleWaves
  },
  setup() {
    const store = useStore();
    store.dispatch(AppActionTypes.GET_APP);
    store.dispatch(AppActionTypes.GET_GROUPS);
    store.dispatch(AppActionTypes.GET_DEVICES);
    // TODO hier dann gql subscriptions machen !!
    // const { data: appConfigData } = useSignalRHub<AppConfig>('home', 'SendAppConfig');
    // const { data: groupsData } = useSignalRHub<Group[]>('home', 'SendGroups');
    // const { data: devicesData } = useSignalRHub<Device[]>('home', 'SendDevices');
    // watch(appConfigData, (newAppConfigData) => {
    //   if (newAppConfigData) {
    //     // store.dispatch(AppActionTypes.UPDATE_APP, newAppConfigData);
    //   }
    // });
    // watch(groupsData, (newGroupsData) => {
    //   if (newGroupsData) {
    //     // store.dispatch(AppActionTypes.UPDATE_GROUPS, newGroupsData);
    //   }
    // });
    // watch(devicesData, (newDevicesData) => {
    //   if (newDevicesData) {
    //     // store.dispatch(AppActionTypes.SET_DEVICES, newDevicesData);
    //   }
    // });

    return {};
  }
});
</script>
