<template>
  <div class="w-full registration">
    <div
      class="container fully-centered px-8 pt-6 pb-8
              md:8/12 lg:w-8/12 xl:w-8/12"
      :class="doneRegistration ? '' : 'bg-white shadow-md rounded'"
    >
      <template v-if="doneRegistration">
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
            <h2 class="text-2xl mb-4 text-gray-800 text-center font-bold">Registration Success</h2>
            <div class="text-gray-600 mb-8">
              Thank you. We have sent you an email to demo@demo.test. Please click the link in the message to activate your account.
            </div>
            <!--  TODO: link to -->
            <button
              class="w-40 block mx-auto focus:outline-none py-2 px-5 rounded-lg shadow-sm text-center
                      text-gray-600 bg-white hover:bg-gray-100 font-medium border"
            >
              Back to home
            </button>
          </div>
        </div>
      </template>
      <!-- Top Navigation Info -->
      <template v-if="!doneRegistration">
        <div class="flex flex-col border-b-2">
          <div class="progress flex justify-center">
            <div class="progress-step ml-5" :class="{ active: index === activeStep }" v-for="(step, index) in steps" :key="step.step + index"></div>
          </div>
          <div v-for="(step, index) in steps" :key="step.step + index" class="mt-1">
            <div v-if="index === activeStep" class="pl-4 mt-0 text-xl font-bold text-gray-500 leading-tight">
              {{ step.title }}
            </div>
          </div>
        </div>

        <!-- Content and Actions-->
        <div class="h-full flex flex-col justify-between">
          <!-- Content-->
          <div class="h-full flex flex-col justify-start mt-20">
            <template v-if="activeStep === 0">
              <!-- Image Upload-->
              <div class="mb-5 text-center">
                <div class="mx-auto w-32 h-32 mb-2 border rounded-full relative bg-gray-100 mb-4 shadow-inset">
                  <!-- <img id="image" class="object-cover w-full h-32 rounded-full" :src="image" />-->
                  <div class="mt-8">Image Upload coming soon</div>
                </div>

                <button
                  :disabled="true"
                  class="cursor-pointer inline-flex justify-between items-center focus:outline-none border py-2 px-4
                  rounded-lg shadow-sm text-left text-gray-600 bg-white font-medium
                  focus:outline-none cursor-not-allowed"
                >
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    class="inline-flex flex-shrink-0 w-6 h-6 -mt-1 mr-1"
                    viewBox="0 0 24 24"
                    stroke-width="2"
                    stroke="currentColor"
                    fill="none"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                  >
                    <rect x="0" y="0" width="24" height="24" stroke="none"></rect>
                    <path
                      d="M5 7h1a2 2 0 0 0 2 -2a1 1 0 0 1 1 -1h6a1 1 0 0 1 1 1a2 2 0 0 0 2 2h1a2 2 0 0 1 2 2v9a2 2 0 0 1
                      -2 2h-14a2 2 0 0 1 -2 -2v-9a2 2 0 0 1 2 -2"
                    />
                    <circle cx="12" cy="13" r="3" />
                  </svg>
                  Browse Photo
                </button>
                <div class="mx-auto w-48 text-gray-500 text-xs text-center mt-1">Click to add profile picture</div>
              </div>
              <!-- Name input-->
              <form class="px-5">
                <div class="mb-4">
                  <label class="block text-gray-600 md:text-left mb-1 md:mb-0 pr-4" for="username">
                    Username
                  </label>
                  <input
                    required
                    v-model="registrationRequest.username"
                    class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight
                            focus:outline-none focus:shadow-outline bg-ui-background"
                    id="username"
                    type="text"
                    placeholder="JonDoe"
                  />
                  <div class="-mx-3 md:flex mt-4">
                    <div class="md:w-1/2 px-3 mb-6 md:mb-0">
                      <label class="block text-gray-600 md:text-left mb-1 md:mb-0 pr-4" for="grid-first-name">
                        First Name
                      </label>
                      <input
                        class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight
                            focus:outline-none focus:shadow-outline bg-ui-background"
                        id="grid-first-name"
                        type="text"
                        v-model="registrationRequest.firstname"
                        placeholder="Jon"
                      />
                    </div>
                    <div class="md:w-1/2 px-3">
                      <label class="block text-gray-600 md:text-left mb-1 md:mb-0 pr-4" for="grid-last-name">
                        Last Name
                      </label>
                      <input
                        class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight
                            focus:outline-none focus:shadow-outline bg-ui-background"
                        id="grid-last-name"
                        type="text"
                        v-model="registrationRequest.lastname"
                        placeholder="Doe"
                      />
                    </div>
                  </div>
                </div>
              </form>
            </template>
            <template v-if="activeStep === 1">
              <div class="text-left">
                <label for="password" class="font-bold mb-1 text-xl text-gray-700">Set up password</label>
                <div class="text-gray-600 mt-2 mb-4">
                  Please create a secure password including the following criteria below.
                  <ul class="list-disc text-sm ml-4 mt-2 text-left">
                    <li>lowercase letters</li>
                    <li>numbers</li>
                    <li>capital letters</li>
                    <li>spacial characters</li>
                  </ul>
                </div>

                <div class="relative">
                  <input
                    id="password"
                    :type="togglePassword ? 'text' : 'password'"
                    @keydown="checkPasswordStrength"
                    @blur="checkPasswordStrength"
                    v-model="registrationRequest.password"
                    class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight
                            focus:outline-none focus:shadow-outline bg-ui-background"
                    placeholder="Your strong password..."
                  />

                  <div class="absolute right-0 bottom-0 top-0 px-3 py-2 cursor-pointer" @click="togglePassword = !togglePassword">
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
                      <path d="M12,9c-1.642,0-3,1.359-3,3c0,1.642,1.358,3,3,3c1.641,0,3-1.358,3-3C15,10.359,13.641,9,12,9z" />
                      <path
                        d="M12,5c-7.633,0-9.927,6.617-9.948,6.684L1.946,12l0.105,0.316C2.073,12.383,4.367,19,12,
                        19s9.927-6.617,9.948-6.684 L22.054,12l-0.105-0.316C21.927,11.617,19.633,5,12,5z M12,17c-5.351,
                        0-7.424-3.846-7.926-5C4.578,10.842,6.652,7,12,7 c5.351,0,7.424,3.846,7.926,5C19.422,13.158,17.348,17,12,17z"
                      />
                    </svg>
                  </div>
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
                        'bg-orange-400': passwordStrengthText === 'Could be stronger' || passwordStrengthText === 'Strong password'
                      }"
                      class="h-2 rounded-full mr-1 w-1/3 bg-gray-300"
                    ></div>
                    <div :class="{ 'bg-green-400': passwordStrengthText === 'Strong password' }" class="h-2 rounded-full w-1/3 bg-gray-300"></div>
                  </div>
                  <div class="text-gray-500 font-medium text-sm ml-3 leading-none">{{ passwordStrengthText }}</div>
                </div>
              </div>
            </template>
            <template v-if="activeStep === 2">3</template>
          </div>
          <!-- Actions -->
          <div class="grid grid-cols-2 gap-6 py-10">
            <div v-if="activeStep - 1 >= 0" class="col-start-1 flex justify-center">
              <button
                @click="onBackStep(activeStep - 1)"
                class="flex justify-center text-ui-primary font-bold w-5/12 py-2 border border-ui-border rounded-lg
                  transition-colors active:bg-indigo-800 hover:bg-ui-primary hover:text-white"
              >
                Back
              </button>
            </div>
            <div v-if="activeStep + 1 <= 2" class="col-start-2 flex justify-center">
              <button
                @click="onNextStep"
                class="flex justify-center text-ui-primary font-bold w-5/12 py-2 border border-ui-border rounded-lg
                      active:bg-indigo-800 hover:bg-ui-primary hover:text-white transition-colors"
              >
                Next
              </button>
            </div>
            <div v-else class="col-start-2 flex justify-center">
              <button
                :disabled="!checkInputs"
                @click="onRegistrationClick"
                class="flex justify-center col-start-2 text-ui-primary font-bold w-5/12 py-2 border border-ui-border rounded-lg
                      active:bg-indigo-800 transition-colors"
                :class="!checkInputs ? 'opacity-50 focus:outline-none cursor-not-allowed' : 'hover:bg-ui-primary hover:text-white'"
              >
                Complete
              </button>
            </div>
          </div>
        </div>
      </template>
      <p v-if="!doneRegistration">
        Already registered?
        <router-link to="/login">
          Click
        </router-link>
        here.
      </p>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, reactive, computed } from 'vue';
import { AuthResponse, HomeCreateRequest, HomeUpdateRequest, RegistrationRequest, ServerResponse } from '@/types/types';
import { clearStorage, storeAuthResponse, storeToken } from '@/services/auth/authService';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';
import { AUTH_USER } from '@/store/auth/mutations';
import { REGISTRATION } from '@/store/auth/actions';
import { AxiosResponse } from 'axios';
import { CREATE_HOME, FETCH_HOME, UPDATE_HOME } from '@/store/home/actions';

export default defineComponent({
  name: 'Registration',
  components: {},
  setup() {
    const store = useStore();
    const router = useRouter();
    const doneRegistration = ref(false);
    const messageClass = ref('successMessage');
    const message = ref('');
    const activeStep = ref(0);
    const steps = [
      { title: 'Your Profile', step: 0 },
      { title: 'Your Password', step: 1 },
      { title: 'Your SmartHome', step: 2 }
    ];
    const passwordStrengthText = ref('');
    const togglePassword = ref(false);
    let homeCreateRequest: HomeCreateRequest = reactive({
      name: '',
      description: ''
    });

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

    const checkFinalStepHome = computed(() => (store.state.homeModule.home === null ? homeCreateRequest.name : store.state.homeState.home.name));

    const checkInputs = computed(() => registrationRequest.username !== '' && registrationRequest.password !== '' && checkFinalStepHome.value);
    const onBackStep = (nextStep: number) => {
      activeStep.value = nextStep;
    };

    const onNextStep = async (): Promise<void> => {
      let shouldBeNextStep = activeStep.value;
      shouldBeNextStep += 1;
      // if (shouldBeNextStep === 2) {
      //   await store.dispatch(FETCH_HOME);
      // }
      activeStep.value = shouldBeNextStep;
    };

    const existingHome = () => {
      if (store.state.homeModule.home?.name === null) {
        message.value = 'No home created yet';
        messageClass.value = 'secondary--text';
        return '-';
      }
      message.value = 'Already a home created - this will be selected automatically';
      messageClass.value = 'primary--text';
      return !store.state.homeModule.home ? '-' : store.state.homeModule.home.name;
    };

    const onClearForm = (): void => {
      homeCreateRequest = { name: '', description: '' };
    };

    const onRegistrationClick = async () => {
      // await store
      //   .dispatch(REGISTRATION, registrationRequest)
      //   .then(async (response: AxiosResponse<ServerResponse<AuthResponse>>) => {
      //     if (response.data.success) {
      //       const auth = response.data.data as AuthResponse;
      //       storeToken(auth.token);
      //       storeAuthResponse(auth);
      //       await store.commit(AUTH_USER, auth);
      //       if (store.state.homeModule.home === null) {
      //         await store.dispatch(CREATE_HOME, homeCreateRequest);
      //       } else {
      //         const updateHomeRequest: HomeUpdateRequest = {
      //           name: null,
      //           description: null,
      //           settingName: null,
      //           userName: auth.userName
      //         };
      //         await store.dispatch(UPDATE_HOME, updateHomeRequest);
      //       }
      //       await router.push('/');
      //     }
      //   })
      //   .catch((error) => {
      //     console.log(error);
      //   });
      doneRegistration.value = true;
      console.log(doneRegistration.value);
    };

    return {
      activeStep,
      steps,
      registrationRequest,
      togglePassword,
      onBackStep,
      onNextStep,
      doneRegistration,
      checkPasswordStrength,
      passwordStrengthText,
      checkInputs,
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
    margin: 0;
    a {
      text-decoration: underline;
      /*color: var(--color-ui-background);*/
    }
  }
}
</style>
