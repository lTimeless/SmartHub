<template>
  <div>
    <!-- Background -->
    <div class="bg-gray-200 absolute inset-0" />
    <div class="absolute mx-auto w-full">
      <TopDoubleWaves />
    </div>
    <!-- Main View -->
    <div class="flex items-center min-h-screen p-6">
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
          <div class="h-108 md:h-auto md:w-1/2">
            <img
              aria-hidden="true"
              class="object-fill w-full h-full dark:hidden"
              src="../../assets/images/undraw_smart_home_28oy.svg"
              alt="Office"
            />
          </div>
          <div class="flex items-center justify-center h-108 p-6 sm:p-12 md:w-1/2">
            <div class="w-full">
              <h2 class="mb-4 text-left text-2xl font-semibold text-gray-700 dark:text-gray-200">
                {{ title }}
              </h2>
              <label class="text-left block text-sm">
                <span class="text-gray-600 dark:text-gray-400">Username</span>
                <input
                  required
                  type="text"
                  v-model="userName"
                  class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded"
                  placeholder="Jane Doe"
                />
              </label>
              <label class="text-left block mt-4 text-sm">
                <span class="text-gray-600 dark:text-gray-400">Password</span>
                <input
                  required
                  v-model="password"
                  class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded"
                  placeholder="***************"
                  type="password"
                  @keyup.enter="onLoginClick"
                />
              </label>

              <button
                class="block w-full px-4 py-2 mt-4 text-sm font-medium leading-5 text-center text-white transition-colors duration-150 bg-primary border border-transparent rounded active:bg-primary focus:outline-none focus:shadow-outlineIndigo"
                :class="
                  signInDisabled
                    ? 'opacity-50 focus:outline-none cursor-not-allowed'
                    : 'hover:bg-primaryHover'
                "
                @click="onLoginClick"
                :disabled="signInDisabled"
              >
                Log in
              </button>
              <hr class="my-8" />
              <button
                disabled
                class="flex items-center justify-center w-full px-4 py-2 text-sm font-medium leading-5 text-gray-700 transition-colors duration-150 border border-gray-300 rounded-lg active:bg-transparent focus:border-gray-500 active:text-gray-500 focus:outline-none focus:shadow-outline-gray"
                :class="true ? 'opacity-50 focus:outline-none cursor-not-allowed' : 'hover:border-gray-500'"
              >
                <!-- TODO dynamic class-->
                Additional login options....
              </button>
              <p class="mt-4 text-left">
                <router-link
                  class="text-sm font-medium text-primary dark:text-primary hover:underline"
                  to="/forgotpassword"
                >
                  Forgot your password?
                </router-link>
              </p>
              <p class="mt-1 text-left">
                <router-link
                  class="text-sm font-medium text-primary dark:text-primary hover:underline"
                  to="/registration"
                >
                  Create account
                </router-link>
              </p>
            </div>
          </div>
        </template>
      </AppCard>
    </div>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, ref, watch } from 'vue';
import { LoginInput } from '@/types/types';
import { useRouter } from 'vue-router';
import { useStore } from 'vuex';
import { AuthActionTypes } from '@/store/auth/actions';
import { useQuery } from '@vue/apollo-composable';
import { HOME_AND_USERS_EXIST } from '@/graphql/queries';
import { Routes } from '@/types/enums';
import Loader from '@/components/shared/Loader.vue';
import TopDoubleWaves from '@/components/shared/svgs/TopDoubleWaves.vue';
import AppCard from '@/components/shared/widgets/AppCard.vue';

export default defineComponent({
  name: 'Login',
  components: {
    Loader,
    AppCard,
    TopDoubleWaves
  },
  props: {},
  setup() {
    const store = useStore();
    const router = useRouter();
    const title = 'Login';
    const password = ref('');
    const userName = ref('');
    const isSignInBtnClicked = ref(false);
    const input = ref<LoginInput>();
    const { result, loading, error } = useQuery(HOME_AND_USERS_EXIST);

    watch([loading, error], ([newLoad, newError]) => {
      if (!newLoad && !newError) {
        if (!result.value.applicationIsActive) {
          router.push(Routes.Init);
          return Promise.resolve();
        }
        if (!result.value.usersExist) {
          router.push(Routes.Registration);
          return Promise.resolve();
        }
      }
    });

    const onLoginClick = async () => {
      isSignInBtnClicked.value = true;
      input.value = {
        userName: userName.value,
        password: password.value
      };
      store
        .dispatch(AuthActionTypes.LOGIN, input.value)
        .then(() => {
          isSignInBtnClicked.value = false;
          router.push(Routes.Home);
        })
        .catch(() => {
          isSignInBtnClicked.value = false;
        });
    };

    const signInDisabled = computed(
      () => userName.value.length === 0 || password.value.length < 4 || isSignInBtnClicked.value
    );

    return {
      error,
      loading,
      title,
      onLoginClick,
      password,
      userName,
      signInDisabled
    };
  }
});
</script>

<style lang="scss" scoped></style>
