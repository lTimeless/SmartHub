<template>
  <!-- Main View -->
  <div class="flex items-center min-h-screen p-6 background">
    <!-- Top Navigation Info -->
    <AppCard class="bg-white border">
      <div class="h-108 md:h-auto md:w-1/2">
        <img
          aria-hidden="true"
          class="object-fill w-full h-full dark:hidden"
          src="../../../assets/images/undraw_smart_home_28oy.svg"
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
              type="text"
              v-model="registrationRequest.userName"
              class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded"
              placeholder="Jane Doe"
            />
          </label>
          <div class="relative">
            <label class="text-left block mt-4 text-sm">
              <span class="text-gray-600 dark:text-gray-400"> Password </span>
              <input
                :type="togglePassword ? 'text' : 'password'"
                @keydown="checkPasswordStrength"
                @blur="checkPasswordStrength"
                v-model="registrationRequest.password"
                class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded"
                placeholder="***************"
              />
              <div
                class="absolute right-0 bottom-0 flex flex-col justify-end top-0 px-3 py-2 cursor-pointer"
                @click="togglePassword = !togglePassword"
              >
                <svg
                  :class="{ hidden: !togglePassword, block: togglePassword }"
                  xmlns="http://www.w3.org/2000/svg"
                  class="w-6 h-6 fill-current text-gray-500"
                  viewBox="0 0 24 24"
                >
                  <path
                    d="M12 19c.946 0 1.81-.103 2.598-.281l-1.757-1.757C12.568 16.983 12.291 17 12 17c-5.351
                                    0-7.424-3.846-7.926-5 .204-.47.674-1.381 1.508-2.297L4.184 8.305c-1.538 1.667-2.121 3.346-2.132
                                    3.379-.069.205-.069.428 0 .633C2.073 12.383 4.367 19 12 19zM12 5c-1.837 0-3.346.396-4.604.981L3.707
                                    2.293 2.293 3.707l18 18 1.414-1.414-3.319-3.319c2.614-1.951 3.547-4.615 3.561-4.657.069-.205.069-.428
                                    0-.633C21.927 11.617 19.633 5 12 5zM16.972 15.558l-2.28-2.28C14.882 12.888 15 12.459 15
                                    12c0-1.641-1.359-3-3-3-.459 0-.888.118-1.277.309L8.915 7.501C9.796 7.193 10.814 7 12 7c5.351
                                    0 7.424 3.846 7.926 5C19.624 12.692 18.76 14.342 16.972 15.558z"
                  />
                </svg>
                <svg
                  :class="{ hidden: togglePassword, block: !togglePassword }"
                  xmlns="http://www.w3.org/2000/svg"
                  class="w-6 h-6 fill-current text-gray-500"
                  viewBox="0 0 24 24"
                >
                  <path
                    d="M12,9c-1.642,0-3,1.359-3,3c0,1.642,1.358,3,3,3c1.641,0,3-1.358,3-3C15,10.359,13.641,9,12,9z"
                  />
                  <path
                    d="M12,5c-7.633,0-9.927,6.617-9.948,6.684L1.946,12l0.105,0.316C2.073,12.383,4.367,19,12,
                                    19s9.927-6.617,9.948-6.684 L22.054,12l-0.105-0.316C21.927,11.617,19.633,5,12,5z M12,17c-5.351,
                                    0-7.424-3.846-7.926-5C4.578,10.842,6.652,7,12,7 c5.351,0,7.424,3.846,7.926,5C19.422,13.158,17.348,17,12,17z"
                  />
                </svg>
              </div>
            </label>
          </div>
          <div class="relative">
            <label class="text-left block mt-4 text-sm">
              <span class="text-gray-600 dark:text-gray-400"> Confirm password </span>
              <input
                :type="togglePassword ? 'text' : 'password'"
                v-model="confirmPwd"
                class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded"
                placeholder="***************"
              />
            </label>
          </div>
          <div class="flex items-center mt-4 h-3">
            <div class="w-2/3 flex justify-between h-2">
              <div
                :class="{
                  'bg-red-400':
                    passwordStrengthText === 'Too weak' ||
                    passwordStrengthText === 'Could be stronger' ||
                    passwordStrengthText === 'Strong password'
                }"
                class="h-2 rounded-full mr-1 w-1/3 bg-gray-300"
              ></div>
              <div
                :class="{
                  'bg-yellow-400':
                    passwordStrengthText === 'Could be stronger' || passwordStrengthText === 'Strong password'
                }"
                class="h-2 rounded-full mr-1 w-1/3 bg-gray-300"
              ></div>
              <div
                :class="{ 'bg-green-400': passwordStrengthText === 'Strong password' }"
                class="h-2 rounded-full w-1/3 bg-gray-300"
              ></div>
            </div>
            <div class="text-gray-500 font-medium text-sm ml-3 leading-none">
              {{ passwordStrengthText }}
            </div>
          </div>

          <div v-show="registrationRequest.password !== confirmPwd" class="flex mt-4 text-sm">
            <span class="text-red-400">Password is not identical </span>
          </div>

          <button
            class="block w-full px-4 py-2 mt-2 text-sm font-medium leading-5 text-center text-white transition-colors duration-150 bg-primary border border-transparent rounded-lg active:bg-primary focus:outline-none focus:shadow-outlineIndigo"
            :class="
              registrationDisabled
                ? 'opacity-50 focus:outline-none cursor-not-allowed'
                : 'hover:bg-primaryHover'
            "
            @click="onRegistrationClick"
            :disabled="registrationDisabled"
          >
            <span class="flex content-center justify-center">
              <Loader v-if="loadCreate" height="h-2" width="w-2" />
              <span class="pl-2">Create account</span>
            </span>
          </button>
          <hr class="my-8" />
          <button
            disabled
            class="flex items-center justify-center w-full px-4 py-2 text-sm font-medium leading-5 text-black transition-colors duration-150 border border-gray-300 rounded-lg dark:text-gray-400 active:bg-transparent focus:border-gray-500 active:text-gray-500 focus:outline-none focus:shadow-outline-gray"
            :class="true ? 'opacity-50 focus:outline-none cursor-not-allowed' : 'hover:border-gray-500'"
          >
            Additional options....
          </button>
          <p class="mt-4 text-left">
            <router-link
              class="text-sm font-medium text-primary dark:text-primary hover:underline"
              to="/login"
            >
              Already have an account? Login
            </router-link>
          </p>
        </div>
      </div>
    </AppCard>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, reactive, computed } from 'vue';
import { RegistrationInput } from '@/types/types';
import { useRouter } from 'vue-router';
import Loader from '@/components/ui/AppSpinner.vue';
import { Routes } from '@/types/enums';
import AppCard from '@/components/ui/AppCard/AppCard.vue';
import { useMutation } from '@vue/apollo-composable';
import { REGISTRATION } from '@/useCases/identity/registration/RegistrationMutation';
import { useIdentity } from '@/hooks/useIdentity';

export default defineComponent({
  name: 'Registration',
  components: {
    AppCard,
    Loader
  },
  props: {},
  setup() {
    const router = useRouter();
    const { token, clearStorage } = useIdentity();
    const title = 'Create account';
    const passwordStrengthText = ref('');
    const togglePassword = ref(false);
    const confirmPwd = ref('');
    const registrationRequest: RegistrationInput = reactive({
      userName: '',
      password: '',
      role: 'User' // default role, can only be changed after registration
    });

    onMounted(() => {
      clearStorage();
    });
    const { mutate: createAccount, loading: loadCreate, error: errCreate } = useMutation(REGISTRATION);

    const passwordStrength = computed(() => passwordStrengthText.value !== 'Too weak');
    const checkPwd = computed(
      () => registrationRequest.password === '' || registrationRequest.password !== confirmPwd.value
    );
    const registrationDisabled = computed(
      () => registrationRequest.userName === '' || checkPwd.value || !passwordStrength.value
    );

    const checkPasswordStrength = () => {
      const strongRegex = new RegExp('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])(?=.{8,})');
      const mediumRegex = new RegExp(
        '^(((?=.*[a-z])(?=.*[A-Z]))|((?=.*[a-z])(?=.*[0-9]))|((?=.*[A-Z])(?=.*[0-9])))(?=.{6,})'
      );
      const value = registrationRequest.password;
      if (strongRegex.test(value)) {
        passwordStrengthText.value = 'Strong password';
      } else if (mediumRegex.test(value)) {
        passwordStrengthText.value = 'Could be stronger';
      } else {
        passwordStrengthText.value = 'Too weak';
      }
    };

    const onRegistrationClick = async () => {
      await createAccount({ input: registrationRequest }).then((res) => {
        if (res.data.registration.token != null) {
          token.value = res.data.registration.token;
          router.push(Routes.Home);
          return Promise.resolve();
        }
      });
    };

    return {
      loadCreate,
      errCreate,
      registrationRequest,
      togglePassword,
      title,
      confirmPwd,
      checkPasswordStrength,
      passwordStrengthText,
      registrationDisabled,
      onRegistrationClick
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

    .progress-step {
      position: relative;
      width: 20px;
      height: 20px;
      border: 2px solid var(--color-ui-primary);
      border-radius: 50%;
      padding: 3px;
      color: white;
      background-color: var(--color-ui-primary);
      font-weight: bold;
      z-index: 2;

      &.active {
        background-color: var(--color-ui-primary);

        ~ .progress-step {
          color: #ccc;
          background-color: #ccc;
          border: 2px solid #ccc;
        }

        ~ .progress-step::before {
          background-color: #ccc;
        }
      }
    }
  }

  input[type='password']::-ms-reveal,
  input[type='password']::-ms-clear {
    display: none;
  }

  p {
    a {
      text-decoration: underline;
      /*color: var(--color-ui-background);*/
    }
  }
}
</style>
