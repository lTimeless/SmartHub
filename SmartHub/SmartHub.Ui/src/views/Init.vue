<template>
  <div class="w-full registration">
    <div
      class="container fully-centered px-8 pt-6 pb-8
              md:6/12 lg:w-6/12 xl:w-6/12"
      :class="doneInit ? '' : 'bg-white shadow-md rounded'"
    >
      <template v-if="doneInit">
        <div class="bg-white rounded-lg p-10 flex items-center shadow justify-between">
          <div>
            <svg class="mb-4 h-20 w-20 text-green-500 mx-auto" viewBox="0 0 20 20" fill="currentColor">
              <path
                fill-rule="evenodd"
                d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-9.293a1 1 0 00-1.414-1.414L9 10.586 7.707 9.293a1 1
                0 00-1.414 1.414l2 2a1 1 0 001.414 0l4-4z"
                clip-rule="evenodd"
              />
            </svg>
            <h2 class="text-2xl mb-4 text-gray-800 text-center font-bold">SmartHub Initialization Success</h2>
            <div class="text-gray-600 mb-8">
              Thank you for using SmartHub. If you encounter any problems or have any suggestions, please visit
              <a href="https://github.com/SmartHub-Io/SmartHub">github</a> and create an issue. 🔥👌🚀❤
            </div>
            <!--  TODO: Email activation -->
            <button
              @click="goToLogin"
              class="w-40 block mx-auto focus:outline-none py-2 px-5 rounded-lg shadow-sm text-center
                      text-gray-600 bg-white hover:bg-gray-100 font-medium border"
            >
              Go to Login
            </button>
          </div>
        </div>
      </template>
      <template v-if="!doneInit">
        <div class="h-full flex flex-col justify-between">
          <h2 class="font-bold text-3xl">{{ welcomeTitle }}</h2>
          <div class="m-1">
            <img class="img" src="../assets/images/undraw_at_home_octe.svg" alt="Test" width="500" />
          </div>
          <form class="px-5">
            <div class="text-gray-700 font-bold mt-3 mb-4 text-left">
              Please type in a name and/or a description for your smarthome.
            </div>
            <div class="mb-4">
              <label class="block text-gray-600 md:text-left mb-1 md:mb-0 pr-4" for="name">
                SmartHub name
              </label>
              <input
                required
                class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight
                        focus:outline-none focus:shadow-outline"
                type="text"
                id="name"
                placeholder="SmartHub"
                v-model="homeCreateRequest.name"
              />
            </div>
            <div class="mb-5 mt-8">
              <label class="block text-gray-600 md:text-left mb-1 md:mb-0 pr-4" for="description">
                SmartHub description
              </label>
              <input
                id="description"
                class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mb-3
                      leading-tight focus:outline-none focus:shadow-outline"
                placeholder="This is an awesome description"
                v-model="homeCreateRequest.description"
              />
              <p class="text-red-500 text-xs italic"></p>
            </div>
            <div class="md:flex md:w-2/3 md:items-center mb-6">
              <label class="block text-gray-500 flex items-center">
                <input class="form-checkbox text-ui-primary" type="checkbox" v-model="acceptStillWIP" />
                <span class="ml-2 text-sm">
                  This project is still under development.
                </span>
              </label>
            </div>
            <div class="md:flex md:w-2/3 md:items-center mb-6">
              <label class="block text-gray-500 flex items-center">
                <input class="form-checkbox text-ui-primary" type="checkbox" v-model="acceptDetectHomeAddress" />
                <span class="ml-2 text-sm">
                  Automatically detect your home address.
                </span>
              </label>
            </div>
            <div class="flex flex-col items-center justify-between">
              <button
                @click="InitHome"
                :disabled="allDeactive"
                class="flex items-center text-white font-bold px-10 py-2 border border-ui-border rounded-lg
                      bg-ui-primary active:bg-indigo-800 hover:bg-indigo-400 focus:outline-none"
                :class="allDeactive ? 'opacity-50 hover:bg-ui-primary focus:outline-none cursor-not-allowed' : ''"
                type="button"
              >
                Sign In
              </button>
            </div>
          </form>
        </div>
      </template>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, ref, computed } from 'vue';
import { HomeCreateRequest } from '@/types/types';
import { A_CREATE_HOME } from '@/store/home/actions';
import { useStore } from '@/store';
import { useRouter } from 'vue-router';

export default defineComponent({
  name: 'Init',
  components: {},
  setup() {
    const store = useStore();
    const router = useRouter();
    const welcomeTitle = 'Welcome to SmartHub';
    const doneInit = ref(false);
    const acceptStillWIP = ref(false);
    const acceptDetectHomeAddress = ref(false);
    const homeCreateRequest: HomeCreateRequest = reactive({
      name: '',
      description: ''
    });

    const InitHome = () => {
      console.log('init');
      store.dispatch(A_CREATE_HOME, homeCreateRequest).then(() => {
        doneInit.value = true;
      });
    };

    const goToLogin = () => {
      router.push('login');
    };
    const allDeactive = computed(() => !acceptDetectHomeAddress.value || !acceptStillWIP.value || homeCreateRequest.name === '');
    return {
      welcomeTitle,
      doneInit,
      homeCreateRequest,
      acceptStillWIP,
      acceptDetectHomeAddress,
      InitHome,
      allDeactive,
      goToLogin
    };
  }
});
</script>

<style scoped lang="scss">
.registration {
  width: 100%;
  height: 100vh;
  display: flex;
  background-color: var(--color-ui-login-background);
  .fully-centered {
    align-self: center;
    height: 80%;

    .img {
      max-width: 90%;
      display: flex;
      justify-items: center;
      margin: auto;
    }
  }
}
</style>
