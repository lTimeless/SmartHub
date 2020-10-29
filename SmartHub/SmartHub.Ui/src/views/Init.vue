<template>
  <div class="flex items-center min-h-screen p-6 bg-ui-loginBackground dark:bg-gray-900 login">
    <ConfirmationModalAsync
      v-if="doneInit"
      title="SmartHub initialization success"
      button-title="Go to Registration"
      :callback="modalCallback"
    >
      <div class="text-gray-600 mb-8">
        Thank you for using SmartHub.
        <br />If you encounter any problems or have any suggestions, please visit
        <a class="text-ui-primary" href="https://github.com/SmartHub-Io/SmartHub">github</a>
        and create an issue. 🔥👌🚀❤
      </div>
    </ConfirmationModalAsync>
    <NotImplementedModalAsync
      v-if="showgoToFakeModal"
      title="SmartHub initialization success"
      button-title="Go to Testwebsite"
      :callback="goToFake"
    >
      <div class="text-gray-600 mb-8">
        <h2 class="text-orange-500">This feature is not implemented at the moment</h2>
        You want to test SmartHub with fake data?
        <br />Than click the button and you will be redirected to the official testwebsite. <br />If
        you encounter any problems or have any suggestions, please visit
        <a class="text-ui-primary" href="https://github.com/SmartHub-Io/SmartHub">github</a>
        and create an issue. 🔥👌🚀❤
      </div>
    </NotImplementedModalAsync>
    <AppCard v-if="!doneInit && !showgoToFakeModal" class="bg-white shadow-md">
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
          <div class="md:flex md:items-center mb-6">
            <label class="text-gray-500 flex items-center">
              <input
                class="form-checkbox text-ui-primary form-checkbox focus:border-purple-400 focus:outline-none focus:shadow-outlineIndigo dark:focus:shadow-outline-gray"
                type="checkbox"
                v-model="useFakeDbDisabled"
                @change="triggerFakeDb"
              />
              <span class="ml-2 text-sm"> Use fake data, for testing purposes? </span>
            </label>
          </div>
          <label class="flex flex-col text-sm">
            <span class="text-gray-600 dark:text-gray-400 justify-start text-left">Name</span>
            <input
              required
              class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-ui-primary focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline form-input"
              :class="useFakeDbDisabled ? 'bg-gray-300' : ''"
              placeholder="SmartHub (default)"
              type="text"
              v-model="homeCreateRequest.name"
              :disabled="useFakeDbDisabled"
            />
          </label>
          <label class="flex flex-col text-sm mt-4">
            <span class="text-gray-600 dark:text-gray-400 justify-start text-left"
              >Description</span
            >
            <input
              required
              class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-ui-primary focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline form-input"
              placeholder="This is an awesome description (default)"
              :class="useFakeDbDisabled ? 'bg-gray-300' : ''"
              type="text"
              v-model="homeCreateRequest.description"
              :disabled="useFakeDbDisabled"
            />
          </label>
          <div class="mt-4">
            <div class="md:flex md:items-center mb-6">
              <label class="text-gray-500 flex items-center">
                <input
                  class="form-checkbox text-ui-primary form-checkbox focus:border-purple-400 focus:outline-none focus:shadow-outlineIndigo dark:focus:shadow-outline-gray"
                  :class="useFakeDbDisabled ? 'bg-gray-300' : ''"
                  type="checkbox"
                  v-model="homeCreateRequest.autoDetectAddress"
                  :disabled="useFakeDbDisabled"
                />
                <span class="ml-2 text-sm"> Automatically detect your home address. </span>
              </label>
            </div>
            <div class="md:flex md:items-center mb-6">
              <label class="text-gray-500 flex items-center">
                <input
                  class="form-checkbox text-ui-primary"
                  :class="useFakeDbDisabled ? 'bg-gray-300' : ''"
                  type="checkbox"
                  v-model="acceptWip"
                  :disabled="useFakeDbDisabled"
                />
                <span class="ml-2 text-sm"> This project is still under development. </span>
              </label>
            </div>
          </div>
          <template v-if="!useFakeDbDisabled">
            <button
              @click="InitHome"
              class="block w-full px-4 py-2 mt-4 text-sm font-medium leading-5 text-center text-white transition-colors duration-150 bg-ui-primary border border-transparent rounded-lg active:bg-ui-primary focus:outline-none focus:shadow-outlineIndigo"
              :class="
                allDeactive
                  ? 'opacity-50 focus:outline-none cursor-not-allowed'
                  : 'hover:bg-ui-primaryHover'
              "
              :disabled="allDeactive"
            >
              Complete
            </button>
          </template>
          <template v-else>
            <button
              @click="showgoToFakeModal = !showgoToFakeModal"
              class="block w-full px-4 py-2 mt-4 text-sm font-medium leading-5 text-center text-white transition-colors duration-150 bg-ui-primary border border-transparent rounded-lg active:bg-ui-primary focus:outline-none focus:shadow-outlineIndigo"
            >
              Go to
            </button>
          </template>

          <hr class="my-8" />
          <button
            disabled
            class="flex items-center justify-center w-full px-4 py-2 text-sm font-medium leading-5 text-white text-gray-700 transition-colors duration-150 border border-gray-300 rounded-lg dark:text-gray-400 active:bg-transparent focus:border-gray-500 active:text-gray-500 focus:outline-none focus:shadow-outline-gray"
            :class="
              true ? 'opacity-50 focus:outline-none cursor-not-allowed' : 'hover:border-gray-500'
            "
          >
            Additional options....
          </button>
        </div>
      </div>
    </AppCard>
  </div>
</template>

<script lang="ts">
import { defineComponent, defineAsyncComponent, reactive, ref, computed } from 'vue';
import { HomeCreateRequest } from '@/types/types';
import { HomeActionTypes } from '@/store/home/actions';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';
import AppCard from '@/components/widgets/AppCard.vue';
import { checkHome } from '@/services/apis/init.services.ts';

const ConfirmationModalAsync = defineAsyncComponent(
  () =>
    import(/* webpackChunkName: "ConfirmationModal" */ '../components/modals/ConfirmationModal.vue')
);
const NotImplementedModalAsync = defineAsyncComponent(
  () =>
    import(
      /* webpackChunkName: "NotImplementedModal" */ '../components/modals/NotImplementedModal.vue'
    )
);

export default defineComponent({
  name: 'Init',
  components: {
    AppCard,
    ConfirmationModalAsync,
    NotImplementedModalAsync
  },
  setup() {
    const store = useStore();
    const router = useRouter();
    const title = 'Welcome to SmartHub';
    const showgoToFakeModal = ref(false);
    const acceptWip = ref(false);
    const doneInit = ref(false);
    const useFakeDbDisabled = ref(false);
    const homeCreateRequest: HomeCreateRequest = reactive({
      name: '',
      description: '',
      autoDetectAddress: false
    });

    checkHome()
      .then((response) => {
        if (response.data) {
          router.push('/login');
        }
        return Promise.resolve();
      })
      .catch((err) => {
        console.log(err);
        return Promise.reject(err);
      });

    const InitHome = () => {
      if (homeCreateRequest.name === '') {
        homeCreateRequest.name = 'SmartHub';
      }
      if (homeCreateRequest.description === '') {
        homeCreateRequest.description = 'This is an awesome description';
      }
      store
        .dispatch(HomeActionTypes.CREATE_HOME, homeCreateRequest)
        .then(() => {
          doneInit.value = true;
        })
        .catch((err) => {
          console.log(err);
        });
    };

    const modalCallback = () => {
      router.push('/registration');
    };
    const triggerFakeDb = () => {
      useFakeDbDisabled.value = !useFakeDbDisabled.value;
    };

    const goToFake = () => {
      console.log('This feature is not implemented at the moment.');
      router.push('/login');
    };
    const allDeactive = computed(() => !homeCreateRequest.autoDetectAddress || !acceptWip.value);
    return {
      title,
      acceptWip,
      showgoToFakeModal,
      doneInit,
      homeCreateRequest,
      useFakeDbDisabled,
      InitHome,
      allDeactive,
      modalCallback,
      goToFake,
      triggerFakeDb
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
