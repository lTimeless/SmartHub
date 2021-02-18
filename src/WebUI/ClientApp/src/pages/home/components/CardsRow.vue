<template>
  <!-- Groups -->
  <div class="w-full lg:w-6/12 xl:w-3/12 pr-2">
    <AppCardRouterLink border-hover-color="hover:border-red-400" :route="routes.Groups">
      <template #title>
        <div class="w-full pr-4 max-w-full flex-grow flex-1">
          <h5 class="text-gray-600 uppercase font-bold text-xs">Groups</h5>
          <span v-if="!groupsFetch" class="text-xl text-gray-600">
            {{ groupsCountResult && groupsCountResult.groupsCount }}
          </span>
        </div>
        <div class="w-auto pl-4 flex-initial">
          <div class="text-center inline-flex items-center justify-center w-12 h-12 rounded-full bg-red-400">
            <AppIcon icon-name="folder" />
          </div>
        </div>
      </template>
      <template #subTitle>
        <span v-if="!groupsFetch">{{ groupsCountResult && groupsCountResult.groupsCount }} </span>
        <span class="whitespace-no-wrap ml-2"> Since last login </span>
      </template>
    </AppCardRouterLink>
  </div>
  <!-- Devices -->
  <div class="w-full lg:w-6/12 xl:w-3/12 pr-2">
    <AppCardRouterLink border-hover-color="hover:border-yellow-400" :route="routes.Devices">
      <template #title>
        <div class="w-full pr-4 max-w-full flex-grow flex-1">
          <h5 class="text-gray-600 uppercase font-bold text-xs">Devices</h5>
          <span v-if="!devicesFetch" class="text-xl text-gray-600">
            {{ devicesResult && devicesResult.devicesCount }}
          </span>
        </div>
        <div class="w-auto pl-4 flex-initial">
          <div
            class="text-center inline-flex items-center justify-center w-12 h-12 rounded-full bg-yellow-400"
          >
            <AppIcon icon-name="device" />
          </div>
        </div>
      </template>
      <template #subTitle>
        <span v-if="!devicesFetch">{{ devicesResult && devicesResult.devicesCount }} </span>
        <span class="whitespace-no-wrap ml-2"> Since last login </span>
      </template>
    </AppCardRouterLink>
  </div>
  <!-- Users -->
  <div class="w-full lg:w-6/12 xl:w-3/12 pr-2">
    <AppCardRouterLink border-hover-color="hover:border-green-400" :route="routes.Users">
      <template #title>
        <div class="w-full pr-4 max-w-full flex-grow flex-1">
          <h5 class="text-gray-600 uppercase font-bold text-xs">Users</h5>
          <span class="text-xl text-gray-600"> 5 </span>
        </div>
        <div class="w-auto pl-4 flex-initial">
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
        <div class="w-full pr-4 max-w-full flex-grow flex-1">
          <h5 class="text-gray-600 uppercase font-bold text-xs">Automations</h5>
          <span class="text-xl text-gray-600"> 2 </span>
        </div>
        <div class="w-auto pl-4 flex-initial">
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
import { useQuery } from '@urql/vue';
import { defineComponent } from 'vue';
import AppIcon from '@/components/icons/AppIcon.vue';
import AppCardRouterLink from '@/components/app/AppCards/AppCardRouterLink.vue';
import { Routes } from '@/types/enums';
import {
  GetDevicesCountQueryType,
  GetGroupsCountQueryType,
  GET_DEVICES_COUNT,
  GET_GROUPS_COUNT
} from '../../../graphql/queries/HomeQueries';

export default defineComponent({
  name: 'CardsRow',
  components: {
    AppIcon,
    AppCardRouterLink
  },
  props: {},
  setup() {
    const { data: groupsCountResult, fetching: groupsFetch } = useQuery<GetGroupsCountQueryType>({
      query: GET_GROUPS_COUNT
    });
    const { data: devicesResult, fetching: devicesFetch } = useQuery<GetDevicesCountQueryType>({
      query: GET_DEVICES_COUNT
    });

    return {
      groupsCountResult,
      groupsFetch,
      devicesResult,
      devicesFetch,
      routes: Routes
    };
  }
});
</script>

<style scoped></style>
