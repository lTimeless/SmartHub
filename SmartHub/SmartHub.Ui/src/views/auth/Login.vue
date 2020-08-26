<template>
  <div class="flex items-center min-h-screen p-6 bg-ui-loginBackground dark:bg-gray-900 login">
    <div class="flex-1 h-full max-w-4xl mx-auto overflow-hidden bg-white rounded-lg shadow-lg dark:bg-gray-800 ">
      <div class="flex flex-col overflow-y-auto md:flex-row ">
        <div class="h-32 md:h-auto md:w-1/2">
          <img aria-hidden="true" class="object-cover w-full h-full dark:hidden" src="../../assets/images/undraw_smart_home_28oy.svg" alt="Office" />
        </div>
        <div class="flex items-center justify-center p-6 sm:p-12 md:w-1/2">
          <div class="w-full">
            <h2 class="mb-4 text-left text-2xl font-semibold text-gray-700 dark:text-gray-200">
              {{ welcomeToSmartHub }}
            </h2>
            <label class="text-left block text-sm">
              <span class="text-gray-600 dark:text-gray-400">Username</span>
              <input
                required
                v-model="username"
                class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                    focus:outline-none focus:shadow-outline-purple dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
                placeholder="Jane Doe"
              />
            </label>
            <label class="text-left block mt-4 text-sm">
              <span class="text-gray-600 dark:text-gray-400">Password</span>
              <input
                required
                v-model="password"
                class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                    focus:outline-none focus:shadow-outline-purple dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
                placeholder="***************"
                type="password"
                @keyup.enter="onLoginClick"
              />
            </label>

            <!-- You should use a button here, as the anchor is only used for the example  -->
            <button
              class="block w-full px-4 py-2 mt-4 text-sm font-medium leading-5 text-center text-white
                  transition-colors duration-150 bg-purple-600 border border-transparent rounded-lg active:bg-purple-600
                  focus:outline-none focus:shadow-outline-purple"
              :class="signInDisabled ? 'opacity-50 focus:outline-none cursor-not-allowed' : 'hover:bg-ui-primary'"
              @click="onLoginClick"
              :disabled="signInDisabled"
            >
              Log in
            </button>
            <hr class="my-8" />
            <button
              disabled
              class="flex items-center justify-center w-full px-4 py-2 text-sm font-medium leading-5 text-white
                  text-gray-700 transition-colors duration-150 border border-gray-300 rounded-lg dark:text-gray-400
                  active:bg-transparent focus:border-gray-500 active:text-gray-500 focus:outline-none focus:shadow-outline-gray"
              :class="true ? 'opacity-50 focus:outline-none cursor-not-allowed' : 'hover:border-gray-500'"
            >
              Additional login options....
            </button>
            <p class="mt-4 text-left">
              <router-link class="text-sm font-medium text-purple-600 dark:text-purple-400 hover:underline" to="/forgotpassword">
                Forgot your password?
              </router-link>
            </p>
            <p class="mt-1 text-left">
              <router-link class="text-sm font-medium text-purple-600 dark:text-purple-400 hover:underline" to="/registration">
                Create account
              </router-link>
            </p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, ref } from 'vue';
import { LoginRequest } from '@/types/types';
import { useRouter } from 'vue-router';
import { useStore } from '@/store';
import { A_LOGIN } from '@/store/auth/actions';
import { A_FETCH_HOME } from '@/store/home/actions';

export default defineComponent({
  name: 'Login',
  components: {},
  setup() {
    const store = useStore();
    const router = useRouter();
    const welcomeToSmartHub = 'Welcome to SmartHub';
    const password = ref('');
    const username = ref('');
    const isSignInBtnClicked = ref(false);
    const getHomeState = ref(store.state.homeModule);

    // store.dispatch(A_FETCH_HOME);
    // if (getHomeState.value.home === null) {
    //   router.push('/init');
    // }
    const onLoginClick = async () => {
      isSignInBtnClicked.value = true;
      const login: LoginRequest = {
        username: username.value,
        password: password.value
      };
      await store.dispatch(A_LOGIN, login);
      await router.push('/');
    };

    const signInDisabled = computed(() => username.value.length === 0 || password.value.length < 4 || isSignInBtnClicked.value);

    return {
      welcomeToSmartHub,
      onLoginClick,
      password,
      username,
      signInDisabled
    };
  }
});
</script>

<style lang="scss" scoped></style>
