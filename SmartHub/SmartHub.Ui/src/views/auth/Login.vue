<template>
  <div class="w-full login">
    <div class="container fully-centered bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4">
      <div class="h-full flex flex-col justify-between">
        <h2 class="font-bold text-3xl">{{ welcomeToSmartHub }}</h2>
        <div class="ma-0">
          <img class="img" src="../../assets/images/undraw_smart_home_28oy.svg" alt="Test" width="500" />
          <!--      <Images src="./assets/images/undraw_smart_home_28oy.svg"></Images>-->
        </div>
        <form class="px-5">
          <div class="mb-4">
            <label class="block text-gray-700 text-sm font-bold mb-2" for="username">
              Username
            </label>
            <input
              class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight
          focus:outline-none focus:shadow-outline"
              id="username"
              type="text"
              placeholder="Username"
            />
          </div>
          <div class="mb-6">
            <label class="block text-gray-700 text-sm font-bold mb-2" for="password">
              Password
            </label>
            <input
              class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mb-3
          leading-tight focus:outline-none focus:shadow-outline"
              id="password"
              type="password"
              placeholder="Password"
            />
            <p class="text-red-500 text-xs italic"></p>
          </div>
          <div class="flex flex-col items-center justify-between">
            <button
              class="flex items-center text-white font-bold px-10 py-2 border border-ui-border rounded-lg
                      bg-ui-primary hover:bg-indigo-800 hover:text-white transition-colors"
              type="button"
            >
              Sign In
            </button>

            <p class="pt-2">
              You can register
              <router-link to="/registration">here</router-link>.
            </p>
            <!-- <router-link to="/forgotpassword" class="inline-block align-baseline font-bold text-sm
            hover:text-blue-800" href="#">-->
            <!--  Forgot Password?-->
            <!-- </router-link>-->
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
// import Images from '@/components/svgs/Images.vue';

export default defineComponent({
  name: 'Login',
  components: {
    // Images
  },
  setup() {
    const welcomeToSmartHub = 'Welcome to SmartHub';

    return {
      welcomeToSmartHub
    };
  }
});
</script>

<style lang="scss" scoped>
.login {
  width: 100%;
  height: 100vh;
  display: flex;
  background-color: var(--color-ui-login-background);
  .fully-centered {
    align-self: center;
    width: 40%;
    height: 75%;

    .img {
      max-width: 90%;
      display: flex;
      justify-items: center;
      margin: auto;
    }
  }

  .successMessage {
    color: black;
  }
  p {
    margin: 0;
    a {
      text-decoration: underline;
      color: var(--color-ui-background);
    }
  }
}
</style>
