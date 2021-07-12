<script lang="ts">
import { defineComponent, PropType, toRefs } from 'vue';
import Loader from '@/components/app/AppSpinner.vue';

export default defineComponent({
  name: 'AppModal',
  components: {
    Loader
  },
  props: {
    title: {
      type: String,
      required: true
    },
    saveBtnTitle: {
      type: String,
      required: true
    },
    closeBtnTitle: {
      type: String,
      required: true
    },
    close: {
      type: Function as PropType<() => void>,
      required: true
    },
    save: {
      type: Function as PropType<() => void>,
      required: true
    },
    saveBtnActive: {
      type: Boolean,
      default: true
    },
    mainBgColor: {
      type: String,
      default: 'bg-indigo-400'
    },
    mainBorderColor: {
      type: String,
      default: 'border-indigo-400'
    },
    saveBtnColor: {
      type: String,
      default: 'bg-indigo-400'
    },
    loading: {
      type: Boolean,
      required: true
    }
  },
  setup(props) {
    return {
      ...toRefs(props)
    };
  }
});
</script>

<template>
  <teleport to="body">
    <div>
      <div class="opacity-25 fixed inset-0 z-40 bg-black" />
      <div
        class="overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none justify-center items-center flex"
      >
        <div class="relative md:w-10/12 xl:w-1/2 w-11/12 my-6 mx-auto max-w-3xl">
          <!--content-->
          <div
            class="border-0 rounded-lg shadow-md relative flex flex-col w-full bg-white outline-none focus:outline-none"
          >
            <!--header-->
            <div
              class="flex items-start justify-between p-5 border-b border-solid border-gray-300 rounded-t"
              :class="[`${mainBgColor}`]"
            >
              <h3 class="text-3xl font-semibold text-white">
                {{ title }}
              </h3>
              <button class="p-1 ml-auto outline-none focus:outline-none" @click="close">
                <svg
                  class="h-5 w-5 text-white"
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 24 24"
                  stroke="currentColor"
                >
                  <path
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    stroke-width="2"
                    d="M6 18L18 6M6 6l12 12"
                  />
                </svg>
              </button>
            </div>
            <!--body-->
            <div class="relative p-6 flex-auto">
              <slot />
            </div>
            <!--footer-->
            <div class="flex items-center justify-end p-3 border-t border-solid border-gray-300 rounded-b">
              <button
                class="text-red-400 border solid hover:border-red-400 bg-white font-bold uppercase px-6 py-3 text-sm mr-2 rounded"
                type="button"
                @click="close"
              >
                {{ closeBtnTitle }}
              </button>
              <button
                class="bg-transparent border border-solid font-bold uppercase text-sm pl-4 pr-6 py-3 rounded outline-none focus:outline-none"
                type="button"
                :class="[
                  `${mainBorderColor}`,
                  saveBtnActive && !loading
                    ? `hover:${mainBgColor} hover:text-white text-gray-600`
                    : 'opacity-50 focus:outline-none cursor-not-allowed'
                ]"
                :disabled="!saveBtnActive || loading"
                @click="save"
              >
                <span class="flex">
                  <Loader v-if="loading" height="h-2" width="w-2" />
                  <span class="pl-2">{{ saveBtnTitle }}</span>
                </span>
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </teleport>
</template>
