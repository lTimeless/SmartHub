<template>
  <div v-if="dontShowThisTab !== 0" class="w-full lg:w-6/12 xl:w-3/12 px-4">
    <div
      @click="callbackZero"
      class="cursor-pointer relative flex flex-col min-w-0 break-words bg-white rounded mb-6 xl:mb-0 shadow-lg"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-500 uppercase font-bold text-xs">Back</h5>
            <span class="font-semibold text-xl text-gray-800"></span>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div v-if="dontShowThisTab !== 1" class="w-full lg:w-6/12 xl:w-3/12 px-4">
    <div
      @click="callbackOne"
      class="cursor-pointer relative flex flex-col min-w-0 break-words bg-white rounded mb-6 xl:mb-0 shadow-lg"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-500 uppercase font-bold text-xs">Groups</h5>
            <span class="font-semibold text-xl text-gray-800"> 2 </span>
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
          <span class="text-green-500 mr-2">1 </span>
          <span class="whitespace-no-wrap"> Since last login </span>
        </p>
      </div>
    </div>
  </div>
  <div v-if="dontShowThisTab !== 2" class="w-full lg:w-6/12 xl:w-3/12 px-4">
    <div
      @click="callbackTwo"
      class="cursor-pointer relative flex flex-col min-w-0 break-words bg-white rounded mb-6 xl:mb-0 shadow-lg"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-500 uppercase font-bold text-xs">Devices</h5>
            <span class="font-semibold text-xl text-gray-800"> 6 </span>
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
          <span class="text-red-500 mr-2">2 </span>
          <span class="whitespace-no-wrap"> Since last login </span>
        </p>
      </div>
    </div>
  </div>
  <div v-if="dontShowThisTab !== 3" class="w-full lg:w-6/12 xl:w-3/12 px-4">
    <div
      @click="callbackThree"
      class="cursor-pointer relative flex flex-col min-w-0 break-words bg-white rounded mb-6 xl:mb-0 shadow-lg"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-500 uppercase font-bold text-xs">Users</h5>
            <span class="font-semibold text-xl text-gray-800"> 5 </span>
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
          <span class="text-orange-500 mr-2">0 </span>
          <span class="whitespace-no-wrap"> Since last month </span>
        </p>
      </div>
    </div>
  </div>
  <div v-if="dontShowThisTab !== 4" class="w-full lg:w-6/12 xl:w-3/12 px-4">
    <div
      @click="callbackFour"
      class="cursor-pointer relative flex flex-col min-w-0 break-words bg-white rounded mb-6 xl:mb-0 shadow-lg"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-500 uppercase font-bold text-xs">Automations</h5>
            <span class="font-semibold text-xl text-gray-800"> 2 </span>
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
          <span class="text-green-500 mr-2">1 </span>
          <span class="whitespace-no-wrap"> Currently active </span>
        </p>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType, computed } from 'vue';

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
      default: (): void => {
        console.log('click zero');
      },
      type: Function as PropType<() => void>,
      required: false
    },
    callbackOne: {
      default: (): void => {
        console.log('click one');
      },
      type: Function as PropType<() => void>,
      required: false
    },
    callbackTwo: {
      default: (): void => {
        console.log('click two');
      },
      type: Function as PropType<() => void>,
      required: false
    },
    callbackThree: {
      default: (): void => {
        console.log('click three');
      },
      type: Function as PropType<() => void>,
      required: false
    },
    callbackFour: {
      default: (): void => {
        console.log('click four');
      },
      type: Function as PropType<() => void>,
      required: false
    }
  },
  setup(props) {
    const dontShowThisTab = computed(() => props.openTab);
    return {
      dontShowThisTab,
      ...props
    };
  }
});
</script>

<style scoped></style>
