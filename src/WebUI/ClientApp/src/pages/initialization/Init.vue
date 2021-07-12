<script lang="ts">
import { computed, defineComponent, reactive, ref, watch } from 'vue';
import { useRouter } from 'vue-router';
import AppCard from '@/components/app/AppCards/AppCard.vue';
import { Routes } from '@/types/enums';
import Loader from '@/components/app/AppSpinner.vue';
import { AppConfigInitInput } from '@/graphql/graphql.types';
import { useInitAppMutation } from '@/graphql/mutations/init.generated';
import { useApplicationIsActiveQuery } from '@/graphql/queries/appSmallInfo.generated';

export default defineComponent({
  name: 'Init',
  components: {
    AppCard,
    Loader
  },
  setup() {
    const router = useRouter();
    const title = 'Create your new SmartHub';
    const githubUrl = ref(process.env.GITHUB_SMARTHUB);
    const appConfigCreateRequest: AppConfigInitInput = reactive({
      autoDetectAddress: false
    });
    const { executeMutation: initApp, fetching: loadInit, error: errInit } = useInitAppMutation();
    const { data, fetching: loading, error } = useApplicationIsActiveQuery();

    const applicationIsActive = computed(() => data.value?.applicationIsActive);
    watch(applicationIsActive, (newApplicationIsActive) => {
      if (newApplicationIsActive) {
        router.push(Routes.Login);
        return Promise.resolve();
      }
    });

    const InitHome = async () => {
      await initApp({ input: appConfigCreateRequest }).then(() => {
        router.push(Routes.Registration);
      });
    };

    return {
      loadInit,
      errInit,
      loading,
      error,
      title,
      githubUrl,
      appConfigCreateRequest,
      InitHome
    };
  }
});
</script>

<template>
  <!-- Main View -->
  <div class="flex items-center min-h-screen p-6 background">
    <AppCard class="bg-white border">
      <template v-if="loading">
        <div class="flex items-center justify-center w-full h-108">
          <Loader height="h-48" width="w-48" />
        </div>
      </template>
      <template v-else-if="error">
        <div class="flex items-center justify-center w-full h-108">
          <p>Error: {{ error.name }} {{ error.message }}</p>
        </div>
      </template>
      <template v-else>
        <div class="hidden md:block h-108 md:h-auto md:w-1/2">
          <img
            aria-hidden="true"
            class="object-fill w-full h-full"
            src="../../assets/images/undraw_at_home_octe.svg"
            alt="Office"
          />
        </div>
        <div class="flex items-center justify-center h-108 sm:p-12 md:w-1/2">
          <div class="w-full">
            <h2 class="mb-4 text-left text-2xl font-semibold text-primarySienna">
              {{ title }}
            </h2>
            <div class="text-gray-400 text-sm font-medium mt-3 mb-4 text-left">
              Please type in a name and/or a description for your application.
            </div>
            <label class="flex flex-col text-sm">
              <span class="text-gray-600 dark:text-gray-400 justify-start text-left">Name</span>
              <input
                v-model="appConfigCreateRequest.name"
                required
                class="mt-1 focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded"
                placeholder="SmartHub (default)"
                type="text"
              />
            </label>
            <label class="flex flex-col text-sm mt-4">
              <span class="text-gray-600 dark:text-gray-400 justify-start text-left">Description</span>
              <input
                v-model="appConfigCreateRequest.description"
                required
                class="mt-1 focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded"
                placeholder="This is an awesome description (default)"
                type="text"
              />
            </label>
            <div class="mt-4">
              <div class="md:flex md:items-center mb-6">
                <label class="text-gray-500 flex items-center">
                  <input
                    v-model="appConfigCreateRequest.autoDetectAddress"
                    class="mt-1 focus:ring-primary focus:border-primary sm:text-sm text-primary border-gray-300 rounded"
                    type="checkbox"
                  />
                  <span class="ml-2 text-sm"> Automatically detect your home address. </span>
                </label>
              </div>
            </div>
            <button
              class="block w-full px-4 py-2 mt-4 text-sm font-medium leading-5 text-center text-white bg-primarySienna rounded hover:bg-primarySiennaHover"
              @click="InitHome"
            >
              <span class="flex content-center justify-center">
                <Loader v-if="loadInit" height="h-2" width="w-2" />
                <span class="pl-2">Complete</span>
              </span>
            </button>

            <hr class="my-8" />
            <button
              disabled
              class="flex items-center justify-center w-full px-4 py-2 text-sm font-medium leading-5 text-gray-700 transition-colors duration-150 border border-gray-300 rounded-lg dark:text-gray-400 active:bg-transparent focus:border-gray-500 active:text-gray-500 focus:outline-none focus:shadow-outline-gray"
              :class="true ? 'opacity-50 focus:outline-none cursor-not-allowed' : 'hover:border-gray-500'"
            >
              Additional options....
            </button>
          </div>
        </div>
      </template>
    </AppCard>
  </div>
</template>
