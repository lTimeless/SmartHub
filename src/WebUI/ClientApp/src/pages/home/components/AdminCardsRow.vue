<template>
  <div class="w-full lg:w-6/12 xl:w-3/12 pr-2">
    <div
      class="relative flex flex-col min-w-0 break-words border bg-white rounded hover:border-red-400 mb-6 xl:mb-0"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-600 uppercase font-bold text-xs">Utilization</h5>
            <span class="font-semibold text-xl text-gray-600"> 43,35% </span>
          </div>
          <div class="relative w-auto pl-4 flex-initial">
            <div
              class="text-white p-3 text-center inline-flex items-center justify-center w-12 h-12 rounded-full bg-red-400"
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
                  d="M13 7h8m0 0v8m0-8l-8 8-4-4-6 6"
                />
              </svg>
            </div>
          </div>
        </div>
        <p class="text-sm text-gray-500 mt-4">
          <span class="text-green-500 mr-2">2%</span>
          <span class="whitespace-no-wrap"> Since yesterday </span>
        </p>
      </div>
    </div>
  </div>
  <div class="w-full lg:w-6/12 xl:w-3/12 pr-2">
    <div
      class="relative flex flex-col min-w-0 break-words border bg-white rounded hover:border-orange-400 mb-6 xl:mb-0"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-600 uppercase font-bold text-xs">Errors</h5>
            <span class="font-semibold text-xl text-gray-600"> 6 </span>
          </div>
          <div class="relative w-auto pl-4 flex-initial">
            <div
              class="text-white p-3 text-center inline-flex items-center justify-center w-12 h-12 shadow-lg rounded-full bg-orange-400"
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
                  d="M13 7h8m0 0v8m0-8l-8 8-4-4-6 6"
                />
              </svg>
            </div>
          </div>
        </div>
        <p class="text-sm text-gray-500 mt-4">
          <span class="text-red-500 mr-2">6 </span>
          <span class="whitespace-no-wrap"> Since last login </span>
        </p>
      </div>
    </div>
  </div>
  <div class="w-full lg:w-6/12 xl:w-3/12 px-2">
    <div
      class="relative flex flex-col min-w-0 break-words border bg-white rounded hover:border-pink-400 mb-6 xl:mb-0"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-600 uppercase font-bold text-xs">Activities</h5>
            <span class="font-semibold text-xl text-gray-600"> 42 </span>
          </div>
          <div class="relative w-auto pl-4 flex-initial">
            <div
              class="text-white p-3 text-center inline-flex items-center justify-center w-12 h-12 shadow-lg rounded-full bg-pink-400"
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
                  d="M13 7h8m0 0v8m0-8l-8 8-4-4-6 6"
                />
              </svg>
            </div>
          </div>
        </div>
        <p class="text-sm text-gray-500 mt-4">
          <span class="text-orange-500 mr-2">0 </span>
          <span class="whitespace-no-wrap"> Since yesterday </span>
        </p>
      </div>
    </div>
  </div>
  <div class="w-full lg:w-6/12 xl:w-3/12 pr-2">
    <div
      class="relative flex flex-col min-w-0 break-words border bg-white rounded hover:border-blue-400 mb-6 xl:mb-0"
    >
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-600 uppercase font-bold text-xs">RAM Usage</h5>
            <span class="font-semibold text-xl text-gray-600"> 60% </span>
          </div>
          <div class="relative w-auto pl-4 flex-initial">
            <div
              class="text-white p-3 text-center inline-flex items-center justify-center w-12 h-12 rounded-full bg-blue-400"
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
                  d="M13 7h8m0 0v8m0-8l-8 8-4-4-6 6"
                />
              </svg>
            </div>
          </div>
        </div>
        <p class="text-sm text-gray-500 mt-4">
          <span class="text-green-500 mr-2">1% </span>
          <span class="whitespace-no-wrap"> since yesterday </span>
        </p>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'AdminCardsRow'
});
</script>

<style scoped></style>
