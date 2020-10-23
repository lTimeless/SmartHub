<template>
  <div class="flex items-center min-h-screen p-6 bg-ui-loginBackground dark:bg-gray-900">
    <AppCard class="bg-white shadow-md">
      <div class="h-32 md:h-auto md:w-1/2">
        <img
          aria-hidden="true"
          class="object-cover w-full h-full dark:hidden"
          src="../../assets/images/undraw_smart_home_28oy.svg"
          alt="Office"
        />
      </div>
      <div class="flex items-center justify-center p-6 sm:p-12 md:w-1/2">
        <div class="w-full">
          <h2 class="mb-4 text-left text-2xl font-semibold text-gray-700 dark:text-gray-200">
            {{ title }}
          </h2>
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">Username</span>
            <input
              required
              v-model="username"
              class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-ui-primary focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline form-input"
              placeholder="Jane Doe"
            />
          </label>
          <label class="text-left block mt-4 text-sm">
            <span class="text-gray-600 dark:text-gray-400">Password</span>
            <input
              required
              v-model="password"
              class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-ui-primary focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline form-input"
              placeholder="***************"
              type="password"
              @keyup.enter="onLoginClick"
            />
          </label>

          <button
            class="block w-full px-4 py-2 mt-4 text-sm font-medium leading-5 text-center text-white transition-colors duration-150 bg-ui-primary border border-transparent rounded-lg active:bg-ui-primary focus:outline-none focus:shadow-outlineIndigo"
            :class="
              signInDisabled ? 'opacity-50 focus:outline-none cursor-not-allowed' : 'hover:bg-ui-primaryHover'
            "
            @click="onLoginClick"
            :disabled="signInDisabled"
          >
            Log in
          </button>
          <hr class="my-8" />
          <button
            disabled
            class="flex items-center justify-center w-full px-4 py-2 text-sm font-medium leading-5 text-gray-700 transition-colors duration-150 border border-gray-300 rounded-lg dark:text-gray-400 active:bg-transparent focus:border-gray-500 active:text-gray-500 focus:outline-none focus:shadow-outline-gray"
            :class="true ? 'opacity-50 focus:outline-none cursor-not-allowed' : 'hover:border-gray-500'"
          >
            Additional login options....
          </button>
          <p class="mt-4 text-left">
            <router-link
              class="text-sm font-medium text-ui-primary dark:text-ui-primary hover:underline"
              to="/forgotpassword"
            >
              Forgot your password?
            </router-link>
          </p>
          <p class="mt-1 text-left">
            <router-link
              class="text-sm font-medium text-ui-primary dark:text-ui-primary hover:underline"
              to="/registration"
            >
              Create account
            </router-link>
          </p>
        </div>
      </div>
    </AppCard>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, ref, watch } from 'vue';
import { LoginRequest } from '@/types/types';
import { useRouter } from 'vue-router';
import { useStore } from 'vuex';
import { AuthActionTypes } from '@/store/auth/actions';
import AppCard from '@/components/widgets/AppCard.vue';
import { checkHome, checkUsers } from '@/services/apis/init';
import { useCheckHome, useCheckUsers } from '@/hooks/api/inits';

export default defineComponent({
  components: {
    AppCard
  },
  setup() {
    const store = useStore();
    const router = useRouter();
    const title = 'Login';
    const password = ref('');
    const username = ref('');
    const isSignInBtnClicked = ref(false);

    const { data } = useCheckHome();
    console.log(data);
    if (!data.value) {
      console.log(data);
      router.push('/init');
    } else {
      const { data } = useCheckUsers();
      if (!data) {
        router.push('/registration');
      }
    }

    const onLoginClick = async () => {
      isSignInBtnClicked.value = true;
      const login: LoginRequest = {
        username: username.value,
        password: password.value
      };
      await store
        .dispatch(AuthActionTypes.LOGIN, login)
        .then(() => {
          isSignInBtnClicked.value = false;
        })
        .catch(() => {
          isSignInBtnClicked.value = false;
        });
      await router.push('/');
    };

    const signInDisabled = computed(
      () => username.value.length === 0 || password.value.length < 4 || isSignInBtnClicked.value
    );

    return {
      title,
      onLoginClick,
      password,
      username,
      signInDisabled
    };
  }
});
</script>

<style lang="scss" scoped></style>
