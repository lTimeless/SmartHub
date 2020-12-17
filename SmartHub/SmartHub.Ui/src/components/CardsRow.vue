<template>
  <div v-if="dontShowThisTab !== 0" class="w-full lg:w-6/12 xl:w-3/12 px-4">
    <div
      @click="callbackZero"
      class="cursor-pointer relative flex flex-col min-w-0 break-words mb-6 xl:mb-0 rounded glassmorphism hover:bg-gray-100 hover:bg-opacity-40"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex justify-around items-center">
            <svg
              class="h-5 w-5 text-gray-100"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M10 19l-7-7m0 0l7-7m-7 7h18"
              />
            </svg>
            <h5 class="text-gray-100 uppercase font-bold text-xs">Back</h5>
            <span class="font-semibold text-xl text-gray-800"></span>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div v-if="dontShowThisTab !== 1" class="w-full lg:w-6/12 xl:w-3/12 px-4">
    <div
      @click="callbackOne"
      class="cursor-pointer relative flex flex-col min-w-0 break-words mb-6 xl:mb-0 rounded glassmorphism hover:bg-gray-100 hover:bg-opacity-40"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-100 uppercase font-bold text-xs">Groups</h5>
            <span class="text-xl text-gray-100">
              {{ groupsCount }}
            </span>
          </div>
          <div class="relative w-auto pl-4 flex-initial">
            <div
              class="text-center inline-flex items-center justify-center w-12 h-12 shadow-lg rounded-full bg-red-400"
            >
              <svg
                class="h-5 w-5 text-black"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  stroke-width="2"
                  d="M3 7v10a2 2 0 002 2h14a2 2 0 002-2V9a2 2 0 00-2-2h-6l-2-2H5a2 2 0 00-2 2z"
                />
              </svg>
            </div>
          </div>
        </div>
        <p class="text-sm text-gray-500 mt-4">
          <span class="text-gray-500">{{ parentGroupsCount }} </span>
          <span class="whitespace-no-wrap mr-3"> Groups</span>
          <span class="text-gray-500">{{ subGroupsCount }} </span>
          <span class="whitespace-no-wrap"> Subgroups</span>
        </p>
      </div>
    </div>
  </div>
  <div v-if="dontShowThisTab !== 2" class="w-full lg:w-6/12 xl:w-3/12 px-4">
    <div
      @click="callbackTwo"
      class="cursor-pointer relative flex flex-col min-w-0 break-words mb-6 xl:mb-0 rounded glassmorphism hover:bg-gray-100 hover:bg-opacity-40"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-100 uppercase font-bold text-xs">Devices</h5>
            <span class="text-xl text-gray-100"> {{ devicesCount }} </span>
          </div>
          <div class="relative w-auto pl-4 flex-initial">
            <div
              class="text-center inline-flex items-center justify-center w-12 h-12 shadow-lg rounded-full bg-yellow-400"
            >
              <svg
                class="h-5 w-5 text-black"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  stroke-width="2"
                  d="M12 18h.01M8 21h8a2 2 0 002-2V5a2 2 0 00-2-2H8a2 2 0 00-2 2v14a2 2 0 002 2z"
                />
              </svg>
            </div>
          </div>
        </div>
        <p class="text-sm text-gray-500 mt-4">
          <span class="text-red-500 mr-2">2 </span>
          <span class="whitespace-no-wrap"> Since last login </span>
        </p>
      </div>
    </div>
  </div>
  <div v-if="dontShowThisTab !== 3" class="w-full lg:w-6/12 xl:w-3/12 px-4">
    <div
      @click="callbackThree"
      class="cursor-pointer relative flex flex-col min-w-0 break-words mb-6 xl:mb-0 rounded glassmorphism hover:bg-gray-100 hover:bg-opacity-40"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-100 uppercase font-bold text-xs">Users</h5>
            <span class="text-xl text-gray-100"> 5 </span>
          </div>
          <div class="relative w-auto pl-4 flex-initial">
            <div
              class="text-center inline-flex items-center justify-center w-12 h-12 shadow-lg rounded-full bg-green-400"
            >
              <svg
                class="h-5 w-5 text-black"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  stroke-width="2"
                  d="M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197M13 7a4 4 0 11-8 0 4 4 0 018 0z"
                />
              </svg>
            </div>
          </div>
        </div>
        <p class="text-sm text-gray-500 mt-4">
          <span class="text-orange-500 mr-2">0 </span>
          <span class="whitespace-no-wrap">Since last month </span>
        </p>
      </div>
    </div>
  </div>
  <div v-if="dontShowThisTab !== 4" class="w-full lg:w-6/12 xl:w-3/12 px-4">
    <div
      @click="callbackFour"
      class="cursor-pointer relative flex flex-col min-w-0 break-words mb-6 xl:mb-0 rounded glassmorphism hover:bg-gray-100 hover:bg-opacity-40"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-100 uppercase font-bold text-xs">Automations</h5>
            <span class="text-xl text-gray-100"> 2 </span>
          </div>
          <div class="relative w-auto pl-4 flex-initial">
            <div
              class="text-center inline-flex items-center justify-center w-12 h-12 shadow-lg rounded-full bg-blue-400"
            >
              <svg
                class="h-5 w-5 text-black"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  stroke-width="2"
                  d="M4 4v5h.582m15.356 2A8.001 8.001 0 004.582 9m0 0H9m11 11v-5h-.581m0 0a8.003 8.003 0 01-15.357-2m15.357 2H15"
                />
              </svg>
            </div>
          </div>
        </div>
        <p class="text-sm text-gray-500 mt-4">
          <span class="text-green-500 mr-2">1 </span>
          <span class="whitespace-no-wrap">Currently active </span>
        </p>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { GET_DEVICES_COUNT, GET_GROUPS_COUNT } from '@/graphql/queries';
import { useQuery, useResult } from '@vue/apollo-composable';
import { defineComponent, PropType, computed, watch, ref } from 'vue';

export default defineComponent({
  name: 'CardsRow',
  components: {},
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
