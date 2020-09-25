<template>
  <teleport to="body">
    <div>
      <div class="opacity-25 fixed inset-0 z-40 bg-black"></div>
      <div class="overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none justify-center items-center flex">
        <div class="relative md:w-10/12 xl:w-1/2 w-11/12 my-6 mx-auto max-w-3xl">
          <!--content-->
          <div class="border-0 rounded-lg shadow-md relative flex flex-col w-full bg-white outline-none focus:outline-none">
            <!--header-->
            <div class="flex items-start justify-between p-5 border-b border-solid border-gray-300 rounded-t">
              <h3 class="text-3xl font-semibold">
                {{ title }}
              </h3>
              <button
                class="p-1 ml-auto bg-transparent border-0 text-black opacity-5 float-right text-3xl 
                leading-none font-semibold outline-none focus:outline-none"
                v-on:click="close"
              >
                <span class="bg-transparent text-black opacity-5 h-6 w-6 text-2xl block outline-none focus:outline-none">
                  ×
                </span>
              </button>
            </div>
            <!--body-->
            <div class="relative p-6 flex-auto">
              <slot />
            </div>
            <!--footer-->
            <div class="flex items-center justify-end p-6 border-t border-solid border-gray-300 rounded-b">
              <button
                class="text-red-400 background-transparent font-bold uppercase px-6 py-3 text-sm outline-none focus:outline-none mr-1 mb-1"
                type="button"
                style="transition: all .15s ease"
                @click="close"
              >
                {{ closeBtnTitle }}
              </button>
              <button
                class="text-orange-400 bg-transparent border border-solid border-orange-400 
                font-bold uppercase text-sm px-6 py-3 rounded outline-none focus:outline-none mr-1 mb-1"
                type="button"
                style="transition: all .15s ease"
                @click="save"
                :class="saveBtnActive ? 'hover:bg-orange-400 hover:text-white active:bg-orange-400' : 'opacity-50 focus:outline-none cursor-not-allowed'"
                :disabled="!saveBtnActive"
              >
                {{ saveBtnTitle }}
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </teleport>
</template>

<script lang="ts">
import { defineComponent, PropType, toRefs } from 'vue';

export default defineComponent({
  name: 'BaseModal',
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
    }
  },
  components: {},
  setup(props) {
    return {
      ...toRefs(props)
    };
  }
});
</script>

<style scoped></style>
