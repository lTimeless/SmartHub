<template>
  <div class="flex items-center min-h-screen p-6 bg-ui-loginBackground dark:bg-gray-900">
    <template v-if="doneRegistration">
      <ConfirmationModal title="Registration Success" button-title="Back to home" :callback="registrationComplete">
        <div class="text-gray-600 mb-8">
          Thank you. We have sent you an email to ... . Please click the link in the message to activate your account.
        </div>
        <!--  TODO: Email activation -->
      </ConfirmationModal>
    </template>
    <!-- Top Navigation Info -->
    <template v-if="!doneRegistration">
      <Card>
        <div class="h-32 md:h-auto md:w-1/2">
          <img aria-hidden="true" class="object-cover w-full h-full dark:hidden" src="../../assets/images/undraw_smart_home_28oy.svg" alt="Office" />
        </div>
        <div class="flex items-center justify-center p-6 sm:p-12 md:w-1/2">
          <div class="w-full">
            <h2 class="mb-4 text-left text-2xl font-semibold text-gray-700 dark:text-gray-200">
              {{ title }}
            </h2>
            <label class="text-left block text-sm">
              <span class="text-gray-600 dark:text-gray-400">Username</span>
              <input
                v-model="registrationRequest.username"
                class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
                placeholder="Jane Doe"
              />
            </label>
            <label class="text-left block mt-4 text-sm">
              <span class="text-gray-600 dark:text-gray-400">Password</span>
              <input
                v-model="registrationRequest.password"
                class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
                placeholder="***************"
                type="password"
              />
            </label>
            <label class="text-left block mt-4 text-sm">
              <span class="text-gray-600 dark:text-gray-400">
                Confirm password
              </span>
              <input
                v-model="confirmPwd"
                class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
                placeholder="***************"
                type="password"
              />
            </label>

            <div class="flex mt-6 text-sm">
              <label class="flex items-center dark:text-gray-400">
                <input
                  v-model="privacyPolicy"
                  type="checkbox"
                  class="text-purple-600 form-checkbox focus:border-purple-400 focus:outline-none
                  focus:shadow-outlineIndigo dark:focus:shadow-outline-gray"
                />
                <span class="ml-2">
                  I agree to the
                  <span class="underline">privacy policy</span>
                </span>
              </label>
            </div>

            <button
              class="block w-full px-4 py-2 mt-4 text-sm font-medium leading-5 text-center text-white
                  transition-colors duration-150 bg-ui-primary border border-transparent rounded-lg active:bg-ui-primary
                  focus:outline-none focus:shadow-outlineIndigo"
              :class="registrationDisabled ? 'opacity-50 focus:outline-none cursor-not-allowed' : 'hover:bg-ui-primaryHover'"
              @click="onRegistrationClick"
              :disabled="registrationDisabled"
            >
              Create account
            </button>
            <hr class="my-8" />
            <button
              disabled
              class="flex items-center justify-center w-full px-4 py-2 text-sm font-medium leading-5 text-white
                  text-gray-700 transition-colors duration-150 border border-gray-300 rounded-lg dark:text-gray-400
                  active:bg-transparent focus:border-gray-500 active:text-gray-500 focus:outline-none focus:shadow-outline-gray"
              :class="true ? 'opacity-50 focus:outline-none cursor-not-allowed' : 'hover:border-gray-500'"
            >
              Additional options....
            </button>
            <p class="mt-4 text-left">
              <router-link class="text-sm font-medium text-ui-primary dark:text-ui-primary hover:underline" to="/login">
                Already have an account? Login
              </router-link>
            </p>
          </div>
        </div>
      </Card>
      <!--        <div class="flex flex-col border-b-2">-->
      <!--          <div class="progress flex justify-center">-->
      <!--            <div class="progress-step ml-5" :class="{ active: index === activeStep }"
        v-for="(step, index) in steps" :key="step.step + index"></div>-->
      <!--          </div>-->
      <!--          <div class="mt-1">-->
      <!--            <template v-for="(step, index) in steps">-->
      <!--              <p :key="step.step + index" v-if="step.step === activeStep"
        class="pl-4 mt-0 text-xl font-bold text-gray-500 leading-tight">-->
      <!--                {{ step.title }}-->
      <!--              </p>-->
      <!--            </template>-->
      <!--          </div>-->
      <!--        </div>-->

      <!--        &lt;!&ndash; Content and Actions&ndash;&gt;-->
      <!--        <div class="h-full flex flex-col justify-between">-->
      <!--          &lt;!&ndash; Content&ndash;&gt;-->
      <!--          <div class="h-full flex flex-col justify-start mt-20">-->
      <!--            <template v-if="activeStep === 0">-->
      <!--              &lt;!&ndash; Image Upload&ndash;&gt;-->
      <!--              <div class="mb-5 text-center">-->
      <!--                <div class="mx-auto w-32 h-32 mb-2 border rounded-full relative bg-gray-100 mb-4 shadow-inset">-->
      <!--                  &lt;!&ndash; <img id="image" class="object-cover w-full h-32 rounded-full" :src="image" />&ndash;&gt;-->
      <!--                  <div class="mt-8">Image Upload coming soon</div>-->
      <!--                </div>-->

      <!--                <button-->
      <!--                  :disabled="true"-->
      <!--                  class="cursor-pointer inline-flex justify-between items-center focus:outline-none border py-2 px-4-->
      <!--                  rounded-lg shadow-sm text-left text-gray-600 bg-white font-medium-->
      <!--                  focus:outline-none cursor-not-allowed"-->
      <!--                >-->
      <!--                  <svg-->
      <!--                    xmlns="http://www.w3.org/2000/svg"-->
      <!--                    class="inline-flex flex-shrink-0 w-6 h-6 -mt-1 mr-1"-->
      <!--                    viewBox="0 0 24 24"-->
      <!--                    stroke-width="2"-->
      <!--                    stroke="currentColor"-->
      <!--                    fill="none"-->
      <!--                    stroke-linecap="round"-->
      <!--                    stroke-linejoin="round"-->
      <!--                  >-->
      <!--                    <rect x="0" y="0" width="24" height="24" stroke="none"></rect>-->
      <!--                    <path-->
      <!--                      d="M5 7h1a2 2 0 0 0 2 -2a1 1 0 0 1 1 -1h6a1 1 0 0 1 1 1a2 2 0 0 0 2 2h1a2 2 0 0 1 2 2v9a2 2 0 0 1-->
      <!--                      -2 2h-14a2 2 0 0 1 -2 -2v-9a2 2 0 0 1 2 -2"-->
      <!--                    />-->
      <!--                    <circle cx="12" cy="13" r="3" />-->
      <!--                  </svg>-->
      <!--                  Browse Photo-->
      <!--                </button>-->
      <!--                <div class="mx-auto w-48 text-gray-500 text-xs text-center mt-1">Click to add profile picture</div>-->
      <!--              </div>-->
      <!--              &lt;!&ndash; Name input&ndash;&gt;-->
      <!--              <form class="px-5">-->
      <!--                <div class="mb-4">-->
      <!--                  <label class="block text-gray-600 md:text-left mb-1 md:mb-0 pr-4" for="username">-->
      <!--                    Username-->
      <!--                  </label>-->
      <!--                  <input-->
      <!--                    required-->
      <!--                    v-model="registrationRequest.username"-->
      <!--                    class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight-->
      <!--                            focus:outline-none focus:shadow-outline bg-ui-background"-->
      <!--                    id="username"-->
      <!--                    type="text"-->
      <!--                    placeholder="JonDoe"-->
      <!--                  />-->
      <!--                  <div class="-mx-3 md:flex mt-4">-->
      <!--                    <div class="md:w-1/2 px-3 mb-6 md:mb-0">-->
      <!--                      <label class="block text-gray-600 md:text-left mb-1 md:mb-0 pr-4" for="grid-first-name">-->
      <!--                        First Name-->
      <!--                      </label>-->
      <!--                      <input-->
      <!--                        class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight-->
      <!--                            focus:outline-none focus:shadow-outline bg-ui-background"-->
      <!--                        id="grid-first-name"-->
      <!--                        type="text"-->
      <!--                        v-model="registrationRequest.firstname"-->
      <!--                        placeholder="Jon"-->
      <!--                      />-->
      <!--                    </div>-->
      <!--                    <div class="md:w-1/2 px-3">-->
      <!--                      <label class="block text-gray-600 md:text-left mb-1 md:mb-0 pr-4" for="grid-last-name">-->
      <!--                        Last Name-->
      <!--                      </label>-->
      <!--                      <input-->
      <!--                        class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight-->
      <!--                            focus:outline-none focus:shadow-outline bg-ui-background"-->
      <!--                        id="grid-last-name"-->
      <!--                        type="text"-->
      <!--                        v-model="registrationRequest.lastname"-->
      <!--                        placeholder="Doe"-->
      <!--                      />-->
      <!--                    </div>-->
      <!--                  </div>-->
      <!--                </div>-->
      <!--              </form>-->
      <!--            </template>-->

      <!--                <div class="relative">-->
      <!--                  <input-->
      <!--                    id="password"-->
      <!--                    :type="togglePassword ? 'text' : 'password'"-->
      <!--                    @keydown="checkPasswordStrength"-->
      <!--                    @blur="checkPasswordStrength"-->
      <!--                    v-model="registrationRequest.password"-->
      <!--                    class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight-->
      <!--                            focus:outline-none focus:shadow-outline bg-ui-background"-->
      <!--                    placeholder="Your strong password..."-->
      <!--                  />-->

      <!--                  <div class="absolute right-0 bottom-0 top-0 px-3 py-2 cursor-pointer" @click="togglePassword = !togglePassword">-->
      <!--                    <svg-->
      <!--                      :class="{ hidden: !togglePassword, block: togglePassword }"-->
      <!--                      xmlns="http://www.w3.org/2000/svg"-->
      <!--                      class="w-6 h-6 fill-current text-gray-500"-->
      <!--                      viewBox="0 0 24 24"-->
      <!--                    >-->
      <!--                      <path-->
      <!--                        d="M12 19c.946 0 1.81-.103 2.598-.281l-1.757-1.757C12.568 16.983 12.291 17 12 17c-5.351-->
      <!--                        0-7.424-3.846-7.926-5 .204-.47.674-1.381 1.508-2.297L4.184 8.305c-1.538 1.667-2.121 3.346-2.132-->
      <!--                        3.379-.069.205-.069.428 0 .633C2.073 12.383 4.367 19 12 19zM12 5c-1.837 0-3.346.396-4.604.981L3.707-->
      <!--                        2.293 2.293 3.707l18 18 1.414-1.414-3.319-3.319c2.614-1.951 3.547-4.615 3.561-4.657.069-.205.069-.428-->
      <!--                        0-.633C21.927 11.617 19.633 5 12 5zM16.972 15.558l-2.28-2.28C14.882 12.888 15 12.459 15-->
      <!--                        12c0-1.641-1.359-3-3-3-.459 0-.888.118-1.277.309L8.915 7.501C9.796 7.193 10.814 7 12 7c5.351-->
      <!--                        0 7.424 3.846 7.926 5C19.624 12.692 18.76 14.342 16.972 15.558z"-->
      <!--                      />-->
      <!--                    </svg>-->
      <!--                    <svg-->
      <!--                      :class="{ hidden: togglePassword, block: !togglePassword }"-->
      <!--                      xmlns="http://www.w3.org/2000/svg"-->
      <!--                      class="w-6 h-6 fill-current text-gray-500"-->
      <!--                      viewBox="0 0 24 24"-->
      <!--                    >-->
      <!--                      <path d="M12,9c-1.642,0-3,1.359-3,3c0,1.642,1.358,3,3,3c1.641,0,3-1.358,3-3C15,10.359,13.641,9,12,9z" />-->
      <!--                      <path-->
      <!--                        d="M12,5c-7.633,0-9.927,6.617-9.948,6.684L1.946,12l0.105,0.316C2.073,12.383,4.367,19,12,-->
      <!--                        19s9.927-6.617,9.948-6.684 L22.054,12l-0.105-0.316C21.927,11.617,19.633,5,12,5z M12,17c-5.351,-->
      <!--                        0-7.424-3.846-7.926-5C4.578,10.842,6.652,7,12,7 c5.351,0,7.424,3.846,7.926,5C19.422,13.158,17.348,17,12,17z"-->
      <!--                      />-->
      <!--                    </svg>-->
      <!--                  </div>-->
      <!--                </div>-->
      <!--                <div class="flex items-center mt-4 h-3">-->
      <!--                  <div class="w-2/3 flex justify-between h-2">-->
      <!--                    <div-->
      <!--                      :class="{-->
      <!--                        'bg-red-400':-->
      <!--                          passwordStrengthText === 'Too weak' ||-->
      <!--                          passwordStrengthText === 'Could be stronger' ||-->
      <!--                          passwordStrengthText === 'Strong password'-->
      <!--                      }"-->
      <!--                      class="h-2 rounded-full mr-1 w-1/3 bg-gray-300"-->
      <!--                    ></div>-->
      <!--                    <div-->
      <!--                      :class="{-->
      <!--                        'bg-orange-400': passwordStrengthText === 'Could be stronger' || passwordStrengthText === 'Strong password'-->
      <!--                      }"-->
      <!--                      class="h-2 rounded-full mr-1 w-1/3 bg-gray-300"-->
      <!--                    ></div>-->
      <!--                    <div :class="{ 'bg-green-400': passwordStrengthText === 'Strong password' }"
        class="h-2 rounded-full w-1/3 bg-gray-300"></div>-->
      <!--                  </div>-->
      <!--                  <div class="text-gray-500 font-medium text-sm ml-3 leading-none">{{ passwordStrengthText }}</div>-->
    </template>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, reactive, computed } from 'vue';
import { RegistrationRequest } from '@/types/types';
import { clearStorage } from '@/services/auth/authService';
import { useRouter } from 'vue-router';
import { A_REGISTRATION } from '@/store/auth/actions';
import { useStore } from '@/store';
import ConfirmationModal from '@/components/modals/ConfirmationModal.vue';
import Card from '@/components/widgets/Card.vue';

export default defineComponent({
  name: 'Registration',
  components: {
    Card,
    ConfirmationModal
  },
  setup() {
    const store = useStore();
    const router = useRouter();
    const title = 'Create account';
    const doneRegistration = ref(false);
    const passwordStrengthText = ref('');
    const togglePassword = ref(false);
    const privacyPolicy = ref(false);
    const confirmPwd = ref('');
    const registrationRequest: RegistrationRequest = reactive({
      username: '',
      firstname: '',
      lastname: '',
      password: '',
      role: 'Admin' // default role, can only be changed after registration
    });

    onMounted(() => {
      clearStorage();
    });
    const checkPwd = computed(() => registrationRequest.password === '' || registrationRequest.password !== confirmPwd.value);
    const registrationDisabled = computed(() => registrationRequest.username === '' || !privacyPolicy.value || checkPwd.value);

    const checkPasswordStrength = () => {
      const strongRegex = new RegExp('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])(?=.{8,})');
      const mediumRegex = new RegExp('^(((?=.*[a-z])(?=.*[A-Z]))|((?=.*[a-z])(?=.*[0-9]))|((?=.*[A-Z])(?=.*[0-9])))(?=.{6,})');
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
      await store.dispatch(A_REGISTRATION, registrationRequest);
    };

    const registrationComplete = () => {
      router.push('/');
    };

    return {
      registrationRequest,
      togglePassword,
      title,
      doneRegistration,
      privacyPolicy,
      confirmPwd,
      checkPasswordStrength,
      passwordStrengthText,
      registrationDisabled,
      onRegistrationClick,
      registrationComplete
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
