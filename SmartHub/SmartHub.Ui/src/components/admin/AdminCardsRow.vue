<template>
  <div class="w-full lg:w-6/12 xl:w-3/12 px-4">
    <div class="relative flex flex-col min-w-0 break-words glassmorphism hover:bg-gray-100 hover:bg-opacity-50 mb-6 xl:mb-0 shadow-lg">
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-600 uppercase font-bold text-xs">CPU Performance</h5>
            <span class="font-semibold text-xl text-gray-600"> 43,35% </span>
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
          <span class="text-green-500 mr-2">2%</span>
          <span class="whitespace-no-wrap"> Since yesterday </span>
        </p>
      </div>
    </div>
  </div>
  <div class="w-full lg:w-6/12 xl:w-3/12 px-4">
    <div class="relative flex flex-col min-w-0 break-words glassmorphism hover:bg-gray-100 hover:bg-opacity-50 mb-6 xl:mb-0 shadow-lg">
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-600 uppercase font-bold text-xs">Errors</h5>
            <span class="font-semibold text-xl text-gray-600"> 6 </span>
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
          <span class="text-red-500 mr-2">6 </span>
          <span class="whitespace-no-wrap"> Since last login </span>
        </p>
      </div>
    </div>
  </div>
  <div class="w-full lg:w-6/12 xl:w-3/12 px-4">
    <div class="relative flex flex-col min-w-0 break-words glassmorphism hover:bg-gray-100 hover:bg-opacity-50 mb-6 xl:mb-0 shadow-lg">
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-600 uppercase font-bold text-xs">Activities</h5>
            <span class="font-semibold text-xl text-gray-600"> 42 </span>
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
          <span class="whitespace-no-wrap"> Since yesterday </span>
        </p>
      </div>
    </div>
  </div>
  <div class="w-full lg:w-6/12 xl:w-3/12 px-4">
    <div class="relative flex flex-col min-w-0 break-words glassmorphism hover:bg-gray-100 hover:bg-opacity-50 mb-6 xl:mb-0 shadow-lg">
      <div class="flex-auto p-4">
        <div class="flex flex-wrap">
          <div class="relative w-full pr-4 max-w-full flex-grow flex-1">
            <h5 class="text-gray-600 uppercase font-bold text-xs">Memory Usage</h5>
            <span class="font-semibold text-xl text-gray-600"> 60% </span>
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
          <span class="text-green-500 mr-2">1% </span>
          <span class="whitespace-no-wrap"> since yesterday </span>
        </p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'AdminCardsRow'
};
</script>

<style scoped></style>
