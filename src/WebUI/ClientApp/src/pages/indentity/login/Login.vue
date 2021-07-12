<script lang="ts">
import {computed, defineComponent, reactive, ref, watch} from 'vue';
import {useRouter} from 'vue-router';
import {Routes} from '@/types/enums';
import Loader from '@/components/app/AppSpinner.vue';
import AppCard from '@/components/app/AppCards/AppCard.vue';
import {useIdentity} from '@/hooks/useIdentity';
import {LoginInput} from '@/graphql/graphql.types';
import {useLoginMutation} from '@/graphql/mutations/identity/login.generated';
import {useHomeAndUsersExistQuery} from '@/graphql/queries/appSmallInfo.generated';

export default defineComponent({
  name: 'Login',
  components: {
    Loader,
    AppCard
  },
  props: {},
  setup() {
    const router = useRouter();
    const { setIdentity } = useIdentity();
    const title = 'Login';
    const password = ref('');
    const userName = ref('');
    const loginInput: LoginInput = reactive({
      userName: '',
      password: ''
    });
    const { executeMutation: login, fetching: loadLogin, error: errLogin } = useLoginMutation();

    const {data: result, fetching: loading, error} = useHomeAndUsersExistQuery({requestPolicy: 'network-only'});
    const data = computed(() => result.value);
    watch(data, (newData) => {
      if (newData && !newData.applicationIsActive) {
        router.push(Routes.Init);
        return Promise.resolve();
      }
      if (newData && !newData.usersExist) {
        router.push(Routes.Registration);
        return Promise.resolve();
      }
    });

    const onLoginClick = async () => {
      loginInput.userName = userName.value;
      loginInput.password = password.value;
      await login({ input: loginInput }).then(({ data }) => {
        if (data && data.login) {
          setIdentity(data.login.user?.roles, data.login.user?.id, data.login.isAuthenticated);
          router.push(Routes.Home);
        } else {
          // TODO
          //   errLogin.value = {
          //     graphQLErrors: [
          //       { name: res.data.login.errors[0].code, message: res.data.login.errors[0].message,   }
          //     ]
          //   };
        }
      });
    };

    const signInDisabled = computed(() => userName.value.length === 0 || password.value.length < 4);

    return {
      loadLogin,
      errLogin,
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

<template>
  <!-- Main View -->
  <div class="flex flex-row justify-center items-center min-h-screen p-6 background">
    <div class="w-full xl:w-1/2">
      <AppCard class="bg-white">
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
              class="w-full h-full"
              src="../../../assets/images/undraw_smart_home_28oy.svg"
              alt="Office"
            />
          </div>
          <div class="flex items-center justify-center h-108 p-6 sm:p-12 md:w-1/2">
            <div class="w-full">
              <h2 class="mb-4 text-left text-2xl font-semibold text-primarySienna">
                {{ title }}
              </h2>
              <label class="text-left block text-sm">
                <span class="text-gray-600 dark:text-gray-400">Username</span>
                <input
                  v-model="userName"
                  required
                  type="text"
                  class="
                    mt-1
                    focus:ring-primary
                    focus:border-primary
                    block
                    w-full
                    sm:text-sm
                    border-gray-300
                    rounded
                  "
                  placeholder="Jane Doe"
                />
              </label>
              <label class="text-left block mt-4 text-sm">
                <span class="text-gray-600 dark:text-gray-400">Password</span>
                <input
                  v-model="password"
                  required
                  class="
                    mt-1
                    focus:ring-primary
                    focus:border-primary
                    block
                    w-full
                    sm:text-sm
                    border-gray-300
                    rounded
                  "
                  placeholder="***************"
                  type="password"
                  @keyup.enter="onLoginClick"
                />
              </label>

              <button
                class="
                  block
                  w-full
                  px-4
                  py-2
                  mt-4
                  text-sm
                  font-medium
                  leading-5
                  text-center text-white
                  bg-primarySienna
                  rounded
                "
                :class="
                  signInDisabled
                    ? 'opacity-50 focus:outline-none cursor-not-allowed'
                    : 'hover:bg-primarySiennaHover'
                "
                :disabled="signInDisabled"
                @click="onLoginClick"
              >
                <span class="flex content-center justify-center">
                  <Loader v-if="loadLogin" height="h-2" width="w-2" />
                  <span class="pl-2">Log in</span>
                </span>
              </button>
              <div v-if="errLogin" class="flex mt-4 text-sm">
                <span class="text-red-400">{{ errLogin.name }}: {{ errLogin.message }}</span>
              </div>
              <hr class="my-8" />
              <button
                disabled
                class="
                  flex
                  items-center
                  justify-center
                  w-full
                  px-4
                  py-2
                  text-sm
                  font-medium
                  leading-5
                  text-gray-700
                  transition-colors
                  duration-150
                  border border-gray-300
                  rounded-lg
                  active:bg-transparent
                  focus:border-gray-500
                  active:text-gray-500
                  focus:outline-none
                  focus:shadow-outline-gray
                "
                :class="true ? 'opacity-50 focus:outline-none cursor-not-allowed' : 'hover:border-gray-500'"
              >
                <!-- TODO dynamic class-->
                Additional login options....
              </button>
              <p class="mt-4 text-left">
                <router-link
                  class="text-sm font-medium text-primarySienna hover:underline"
                  to="/forgotpassword"
                >
                  Forgot your password?
                </router-link>
              </p>
              <p class="mt-1 text-left">
                <router-link
                  class="text-sm font-medium text-primarySienna hover:underline"
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
