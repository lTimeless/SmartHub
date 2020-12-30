<template>
  <!-- Groups -->
  <div class="w-full lg:w-6/12 xl:w-3/12 pr-2">
    <AppCardRouterLink border-hover-color="hover:border-red-400" :route="routes.Groups">
      <template #title>
        <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
          <h5 class="text-gray-600 uppercase font-bold text-xs">Groups</h5>
          <span class="text-xl text-gray-600">
            {{ groupsCount }}
          </span>
        </div>
        <div class="relative w-auto pl-4 flex-initial">
          <div class="text-center inline-flex items-center justify-center w-12 h-12 rounded-full bg-red-400">
            <AppIcon icon-name="folder" />
          </div>
        </div>
      </template>
      <template #subTitle>
        <span>{{ parentGroupsCount }} </span>
        <span class="whitespace-no-wrap mr-3"> Groups</span>
        <span>{{ subGroupsCount }} </span>
        <span class="whitespace-no-wrap"> Subgroups</span>
      </template>
    </AppCardRouterLink>
  </div>
  <!-- Devices -->
  <div class="w-full lg:w-6/12 xl:w-3/12 pr-2">
    <AppCardRouterLink border-hover-color="hover:border-yellow-400" :route="routes.Devices">
      <template #title>
        <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
          <h5 class="text-gray-600 uppercase font-bold text-xs">Devices</h5>
          <span class="text-xl text-gray-600"> {{ devicesCount }} </span>
        </div>
        <div class="relative w-auto pl-4 flex-initial">
          <div
            class="text-center inline-flex items-center justify-center w-12 h-12 rounded-full bg-yellow-400"
          >
            <AppIcon icon-name="device" />
          </div>
        </div>
      </template>
      <template #subTitle>
        <span>2 </span>
        <span class="whitespace-no-wrap ml-2"> Since last login </span>
      </template>
    </AppCardRouterLink>
  </div>
  <!-- Users -->
  <div class="w-full lg:w-6/12 xl:w-3/12 pr-2">
    <AppCardRouterLink border-hover-color="hover:border-green-400" :route="routes.Users">
      <template #title>
        <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
          <h5 class="text-gray-600 uppercase font-bold text-xs">Users</h5>
          <span class="text-xl text-gray-600"> 5 </span>
        </div>
        <div class="relative w-auto pl-4 flex-initial">
          <div
            class="text-center inline-flex items-center justify-center w-12 h-12 rounded-full bg-green-400"
          >
            <AppIcon icon-name="users" />
          </div>
        </div>
      </template>
      <template #subTitle>
        <span>0 </span>
        <span class="whitespace-no-wrap ml-2">Since last month </span>
      </template>
    </AppCardRouterLink>
  </div>
  <!-- Automations -->
  <div class="w-full lg:w-6/12 xl:w-3/12 pr-2">
    <AppCardRouterLink border-hover-color="hover:border-blue-400" :route="routes.Automations">
      <template #title>
        <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
          <h5 class="text-gray-600 uppercase font-bold text-xs">Automations</h5>
          <span class="text-xl text-gray-600"> 2 </span>
        </div>
        <div class="relative w-auto pl-4 flex-initial">
          <div class="text-center inline-flex items-center justify-center w-12 h-12 rounded-full bg-blue-400">
            <AppIcon icon-name="repeat" />
          </div>
        </div>
      </template>
      <template #subTitle>
        <span>1 </span>
        <span class="whitespace-no-wrap ml-2">Currently active </span>
      </template>
    </AppCardRouterLink>
  </div>
</template>

<script lang="ts">
import { useQuery, useResult } from '@vue/apollo-composable';
import { defineComponent, watch, ref } from 'vue';
import AppIcon from '@/components/icons/AppIcon.vue';
import AppCardRouterLink from '@/components/ui/AppCard/AppCardRouterLink.vue';
import { Routes } from '@/types/enums';
import { GET_DEVICES_COUNT, GET_GROUPS_COUNT } from "@/useCases/home/HomeQueries";

export default defineComponent({
  name: 'CardsRow',
  components: {
    AppIcon,
    AppCardRouterLink
  },
  props: {},
  setup() {
    const { result: groupsCountResult } = useQuery(GET_GROUPS_COUNT);
    const { result: devicesResult } = useQuery(GET_DEVICES_COUNT);

    const groupsCounts = useResult(groupsCountResult);
    const subGroupsCount = ref(0);
    const parentGroupsCount = ref(0);
    const groupsCount = ref(0);
    watch(
      groupsCounts,
      (newResult) => {
        parentGroupsCount.value = newResult?.parentGroupsCount;
        subGroupsCount.value = newResult?.subGroupsCount;
        groupsCount.value = parentGroupsCount.value + subGroupsCount.value;
      },
      { immediate: true }
    );
    const devicesCount = useResult(devicesResult, 0, (data) => data.devicesCount);

    return {
      parentGroupsCount,
      devicesCount,
      subGroupsCount,
      groupsCount,
      routes: Routes
    };
  }
});
</script>

<style scoped></style>
