<template>
  <div class="flex-1 w-full justify-end">
    <h1 class="text-3xl text-gray-500 font-bold mb-6">{{ title }}</h1>
    <!-- Form -->
    <div v-if="user !== null">
      <!-- Username -->
      <div class="flex mr-2 justify-between">
        <div class="w-1/3 mr-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">Username</span>
            <input
              v-model="user.userName"
              class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            />
          </label>
        </div>
        <div class="w-1/3 ml-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">Roles</span>
            <input
              v-model="userRoles"
              class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            />
          </label>
        </div>
      </div>
      <!-- Personname -->
      <div class="flex justify-between mt-4">
        <label class="text-left block text-sm w-full  mr-2">
          <span class="text-gray-600 dark:text-gray-400">Firstname</span>
          <input
            v-model="user.personName.firstName"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            placeholder="Jane Doe"
          />
        </label>
        <label class="text-left block text-sm w-full mr-2">
          <span class="text-gray-600 dark:text-gray-400">Middlename</span>
          <input
            v-model="user.personName.middleName"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            placeholder="Jane Doe"
          />
        </label>
        <label class="text-left block text-sm w-full mr-2">
          <span class="text-gray-600 dark:text-gray-400">Lastname</span>
          <input
            v-model="user.personName.lastName"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            placeholder="Jane Doe"
          />
        </label>
      </div>
      <!-- Other -->
      <label class="text-left block text-sm w-full mr-2 mt-4">
        <span class="text-gray-600 dark:text-gray-400">Email</span>
        <input
          v-model="user.email"
          class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
          placeholder="test@test.com"
        />
      </label>
      <label class="text-left block text-sm w-full mr-2 mt-4">
        <span class="text-gray-600 dark:text-gray-400">Phonenumber</span>
        <input
          v-model="user.phoneNumber"
          class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
          placeholder="0000 0011223344"
        />
      </label>
      <div class="text-gray-500 text-sm text-left mt-10">Last modified by: {{ user.lastModifiedBy }}</div>
      <div class="text-gray-500 text-sm text-left">Last modified at: {{ user.lastModifiedAt }}</div>
    </div>
    <div v-else>
      Something went wrong loading your accound data...
    </div>
    <!-- Save button -->
    <div class="md:w-2/12 mt-3">
      <!-- <button
        class="block w-full px-4 py-2 mt-4 text-sm font-medium leading-5 text-center text-white
                  transition-colors duration-150 bg-ui-primary border border-transparent rounded-lg active:bg-ui-primary
                  focus:outline-none focus:shadow-outlineIndigo"
        :class="true ? 'opacity-50 focus:outline-none cursor-not-allowed' : 'hover:bg-ui-primaryHover'"
      >
        Save
      </button> -->
      <action-button title="Save" color="indigo" :height="35" :width="150" :callback="onClick" />
    </div>
  </div>
</template>

<script>
import { useStore } from '@/store';
import { defineComponent, ref, computed } from 'vue';
import { getUserRoles } from '@/services/auth/authService';
import ActionButton from '@/components/widgets/ActionButton.vue';

export default defineComponent({
  name: 'MyUser',
  components: {
    ActionButton
  },
  setup() {
    const store = useStore();
    const title = ref('MyUser');
    const user = computed(() => store.state.authModule.whoAmI);
    const userRoles = computed(() => getUserRoles());
    const onClick = () => {
      console.log('Click button');
    };
    return {
      title,
      user,
      onClick,
      userRoles
    };
  }
});
</script>

<style scoped></style>
