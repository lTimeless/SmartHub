<template>
  <div @keydown.down="increment" @keydown.up="decrement" @keydown.enter="go" class="relative">
    <!--    <label class="relative block">-->
    <!--      <span class="sr-only">Search Documentation</span>-->
    <!--      <div class="absolute inset-y-0 left-0 flex items-center justify-center px-3 py-2 opacity-50">-->
    <!--        <SearchIcon size="1.25x" class="text-ui-typo" />-->
    <!--      </div>-->
    <!--      <input-->
    <!--        ref="input"-->
    <!--        type="search"-->
    <!--        :value="query"-->
    <!--        class="block w-full py-2 pl-10 pr-4 border-2 rounded-lg bg-ui-sidebar border-ui-sidebar focus:bg-ui-background"-->
    <!--        :class="{ 'rounded-b-none': showResult }"-->
    <!--        placeholder="Search Documentation..."-->
    <!--        @focus="focused = true"-->
    <!--        @blur="focused = false"-->
    <!--        @input="-->
    <!--          focusIndex = -1;-->
    <!--          query = $event.target.value;-->
    <!--        "-->
    <!--        @change="query = $event.target.value"-->
    <!--      />-->
    <!--    </label>-->
    <!--    <div-->
    <!--      v-if="showResult"-->
    <!--      class="fixed inset-x-0 z-50 overflow-y-auto border-2 border-t-0 rounded-lg
    rounded-t-none shadow-lg results bg-ui-background bottom:0 sm:bottom-auto sm:absolute border-ui-sidebar"-->
    <!--      style="max-height: calc(100vh - 120px)"-->
    <!--    >-->
    <!--      <ul class="px-4 py-2 m-0">-->
    <!--        <li v-if="results.length === 0" class="px-2">-->
    <!--          No results for <span class="font-bold">{{ query }}</span-->
    <!--          >.-->
    <!--        </li>-->

    <!--        <li-->
    <!--          v-else-->
    <!--          v-for="(result, index) in results"-->
    <!--          :key="result.item.path + result.item.anchor"-->
    <!--          @mouseenter="focusIndex = index"-->
    <!--          @mousedown="go"-->
    <!--          class="border-ui-sidebar"-->
    <!--          :class="{-->
    <!--            'border-b': index + 1 !== results.length-->
    <!--          }"-->
    <!--        >-->
    <!--          <g-link-->
    <!--            :to="result.item.path + result.item.anchor"-->
    <!--            class="block p-2 -mx-2 text-base font-bold rounded-lg"-->
    <!--            :class="{-->
    <!--              'bg-ui-sidebar text-ui-primary': focusIndex === index-->
    <!--            }"-->
    <!--          >-->
    <!--            <span v-if="result.item.value === result.item.title">-->
    <!--              {{ result.item.value }}-->
    <!--            </span>-->

    <!--            <span v-else class="flex items-center">-->
    <!--              {{ result.item.title }}-->
    <!--              <ChevronRightIcon size="1x" class="mx-1" />-->
    <!--              <span class="font-normal opacity-75">{{ result.item.value }}</span>-->
    <!--            </span>-->
    <!--          </g-link>-->
    <!--        </li>-->
    <!--      </ul>-->
    <!--    </div>-->
  </div>
</template>

<script>
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'SearchWithDropdown'
});
</script>

<style scoped></style>
