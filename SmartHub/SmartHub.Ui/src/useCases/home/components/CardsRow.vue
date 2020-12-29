<template>
  <div v-if="dontShowThisTab !== 0" class="w-full lg:w-6/12 xl:w-3/12 pr-2">
    <div
      @click="callbackZero"
      class="cursor-pointer relative bg-white flex flex-col min-w-0 break-words mb-6 xl:mb-0 rounded border hover:border-gray-400"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex justify-around items-center">
            <AppIcon icon-name="arrowLeft" />
            <h5 class="text-gray-600 uppercase font-bold text-xs">Back</h5>
            <span class="font-semibold text-xl text-gray-800"></span>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div v-if="dontShowThisTab !== 1" class="w-full lg:w-6/12 xl:w-3/12 pr-2">
    <div
      @click="callbackOne"
      class="cursor-pointer relative bg-white flex flex-col min-w-0 break-words mb-6 xl:mb-0 rounded border hover:border-red-400"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-600 uppercase font-bold text-xs">Groups</h5>
            <span class="text-xl text-gray-600">
              {{ groupsCount }}
            </span>
          </div>
          <div class="relative w-auto pl-4 flex-initial">
            <div
              class="text-center inline-flex items-center justify-center w-12 h-12 rounded-full bg-red-400"
            >
              <AppIcon icon-name="folder" />
            </div>
          </div>
        </div>
        <p class="text-sm text-gray-500 mt-4">
          <span>{{ parentGroupsCount }} </span>
          <span class="whitespace-no-wrap mr-3"> Groups</span>
          <span>{{ subGroupsCount }} </span>
          <span class="whitespace-no-wrap"> Subgroups</span>
        </p>
      </div>
    </div>
  </div>
  <div v-if="dontShowThisTab !== 2" class="w-full lg:w-6/12 xl:w-3/12 pr-2">
    <div
      @click="callbackTwo"
      class="cursor-pointer relative bg-white flex flex-col min-w-0 break-words mb-6 xl:mb-0 rounded border hover:border-yellow-400"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
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
        </div>
        <p class="text-sm text-gray-500 mt-4">
          <span>2 </span>
          <span class="whitespace-no-wrap ml-2"> Since last login </span>
        </p>
      </div>
    </div>
  </div>
  <div v-if="dontShowThisTab !== 3" class="w-full lg:w-6/12 xl:w-3/12 pr-2">
    <div
      @click="callbackThree"
      class="cursor-pointer relative bg-white flex flex-col min-w-0 break-words mb-6 xl:mb-0 rounded border hover:border-green-400"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
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
        </div>
        <p class="text-sm text-gray-500 mt-4">
          <span>0 </span>
          <span class="whitespace-no-wrap ml-2">Since last month </span>
        </p>
      </div>
    </div>
  </div>
  <div v-if="dontShowThisTab !== 4" class="w-full lg:w-6/12 xl:w-3/12 pr-2">
    <div
      @click="callbackFour"
      class="cursor-pointer relative bg-white flex flex-col min-w-0 break-words mb-6 xl:mb-0 rounded border hover:border-blue-400"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-600 uppercase font-bold text-xs">Automations</h5>
            <span class="text-xl text-gray-600"> 2 </span>
          </div>
          <div class="relative w-auto pl-4 flex-initial">
            <div
              class="text-center inline-flex items-center justify-center w-12 h-12 rounded-full bg-blue-400"
            >
              <AppIcon icon-name="repeat" />
            </div>
          </div>
        </div>
        <p class="text-sm text-gray-500 mt-4">
          <span>1 </span>
          <span class="whitespace-no-wrap ml-2">Currently active </span>
        </p>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { GET_DEVICES_COUNT, GET_GROUPS_COUNT } from '@/graphql/queries';
import { useQuery, useResult } from '@vue/apollo-composable';
import { defineComponent, PropType, computed, watch, ref } from 'vue';
import AppIcon from '@/components/icons/AppIcon.vue';

export default defineComponent({
  name: 'CardsRow',
  components: {
    AppIcon
  },
  props: {
    openTab: {
      type: Number,
      default: 0,
      required: true
    },
    callbackZero: {
      type: Function as PropType<() => void>,
      required: true
    },
    callbackOne: {
      type: Function as PropType<() => void>,
      required: true
    },
    callbackTwo: {
      type: Function as PropType<() => void>,
      required: true
    },
    callbackThree: {
      type: Function as PropType<() => void>,
      required: true
    },
    callbackFour: {
      type: Function as PropType<() => void>,
      required: true
    }
  },
  setup(props) {
    const dontShowThisTab = computed(() => props.openTab);
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
      dontShowThisTab,
      parentGroupsCount,
      devicesCount,
      subGroupsCount,
      groupsCount,
      ...props
    };
  }
});
</script>

<style scoped></style>
