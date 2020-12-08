<template>
  <div class="flex items-center min-h-screen p-6 bg-loginBackground dark:bg-gray-900 login">
    <ConfirmationModalAsync
      v-if="doneInit"
      title="SmartHub initialization success"
      button-title="Go to Registration"
      :callback="modalCallback"
    >
      <div class="text-gray-600 mb-8">
        Thank you for using SmartHub.
        <br />If you encounter any problems or have any suggestions, please visit
        <a class="text-primary" href="https://github.com/SmartHub-Io/SmartHub">github</a>
        and create an issue. 🔥👌🚀❤
      </div>
    </ConfirmationModalAsync>
    <AppCard v-if="!doneInit" class="bg-white shadow-md">
      <div class="h-32 md:h-auto md:w-1/2">
        <img
          aria-hidden="true"
          class="object-cover w-full h-full dark:hidden"
          src="../assets/images/undraw_at_home_octe.svg"
          alt="Office"
        />
      </div>
      <div class="flex items-center justify-center p-6 sm:p-12 md:w-1/2">
        <div class="w-full">
          <h2 class="mb-4 text-left text-2xl font-semibold text-gray-700 dark:text-gray-200">
            {{ title }}
          </h2>
          <div class="text-gray-700 font-medium mt-3 mb-4 text-left">
            Please type in a name and/or a description for your smartHub.
          </div>
          <label class="flex flex-col text-sm">
            <span class="text-gray-600 dark:text-gray-400 justify-start text-left">Name</span>
            <input
              required
              class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-primary focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline form-input"
              placeholder="SmartHub (default)"
              type="text"
              v-model="appConfigCreateRequest.name"
            />
          </label>
          <label class="flex flex-col text-sm mt-4">
            <span class="text-gray-600 dark:text-gray-400 justify-start text-left">Description</span>
            <input
              required
              class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-primary focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline form-input"
              placeholder="This is an awesome description (default)"
              type="text"
              v-model="appConfigCreateRequest.description"
            />
          </label>
          <div class="mt-4">
            <div class="md:flex md:items-center mb-6">
              <label class="text-gray-500 flex items-center">
                <input
                  class="form-checkbox text-primary form-checkbox focus:border-purple-400 focus:outline-none focus:shadow-outlineIndigo dark:focus:shadow-outline-gray"
                  type="checkbox"
                  v-model="appConfigCreateRequest.autoDetectAddress"
                />
                <span class="ml-2 text-sm"> Automatically detect your home address. </span>
              </label>
            </div>
            <div class="md:flex md:items-center mb-6">
              <label class="text-gray-500 flex items-center">
                <input class="form-checkbox text-primary" type="checkbox" v-model="acceptWip" />
                <span class="ml-2 text-sm"> This project is still under development. </span>
              </label>
            </div>
          </div>
          <button
            @click="InitHome"
            class="block w-full px-4 py-2 mt-4 text-sm font-medium leading-5 text-center text-white transition-colors duration-150 bg-primary border border-transparent rounded-lg active:bg-ui-primary focus:outline-none focus:shadow-outlineIndigo"
            :class="
              allDeactive ? 'opacity-50 focus:outline-none cursor-not-allowed' : 'hover:bg-primaryHover'
            "
            :disabled="allDeactive"
          >
            Complete
          </button>

          <hr class="my-8" />
          <button
            disabled
            class="flex items-center justify-center w-full px-4 py-2 text-sm font-medium leading-5 text-white text-gray-700 transition-colors duration-150 border border-gray-300 rounded-lg dark:text-gray-400 active:bg-transparent focus:border-gray-500 active:text-gray-500 focus:outline-none focus:shadow-outline-gray"
            :class="true ? 'opacity-50 focus:outline-none cursor-not-allowed' : 'hover:border-gray-500'"
          >
            Additional options....
          </button>
        </div>
      </div>
    </AppCard>
  </div>
</template>

<script lang="ts">
import { defineComponent, defineAsyncComponent, reactive, ref, computed, onMounted } from 'vue';
import { AppConfigInitInput } from '@/types/types';
import { AppActionTypes } from '@/store/app/actions';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';
import AppCard from '@/components/widgets/AppCard.vue';
import { useQuery } from '@vue/apollo-composable';
import { checkApp } from '@/graphql/queries';
import { Routes } from '@/types/enums';

const ConfirmationModalAsync = defineAsyncComponent(
  () => import(/* webpackChunkName: "ConfirmationModal" */ '../components/modals/ConfirmationModal.vue')
);

export default defineComponent({
  name: 'Init',
  components: {
    AppCard,
    ConfirmationModalAsync
  },
  setup() {
    const store = useStore();
    const router = useRouter();
    const title = 'Welcome to SmartHub';
    const acceptWip = ref(false);
    const doneInit = ref(false);
    const appConfigCreateRequest: AppConfigInitInput = reactive({
      name: '',
      description: '',
      autoDetectAddress: false
    });

    const { refetch } = useQuery(checkApp);

    onMounted(() => {
      refetch().then((response) => {
        if (response.data.checkApp) {
          router.push(Routes.Login);
          return Promise.resolve();
        }
      });
    });

    const InitHome = () => {
      if (appConfigCreateRequest.name === '') {
        appConfigCreateRequest.name = 'SmartHub';
      }
      if (appConfigCreateRequest.description === '') {
        appConfigCreateRequest.description = 'This is an awesome description';
      }
      store.dispatch(AppActionTypes.InitIALIZE_APP, appConfigCreateRequest).then(() => {
        doneInit.value = true;
      });
    };

    const modalCallback = () => {
      router.push('/registration');
    };

    const allDeactive = computed(() => !appConfigCreateRequest.autoDetectAddress || !acceptWip.value);
    return {
      title,
      acceptWip,
      doneInit,
      appConfigCreateRequest,
      InitHome,
      allDeactive,
      modalCallback
    };
  }
});
</script>

<style scoped lang="scss">
.registration {
  width: 100%;
  height: 100vh;
  display: flex;
  background-color: var(--color-login-background);
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
