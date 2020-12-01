<template>
  <div class="relative flex-1 w-full justify-end">
    <!-- Form -->
    <div v-if="user">
      <!-- Username -->
      <div class="flex mr-2 justify-between">
        <div class="w-1/3 mr-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">Username</span>
            <input
              type="text"
              v-model="user.userName"
              class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
              :placeholder="user.userName"
            />
          </label>
        </div>
        <div class="w-1/3 ml-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400"
              >Roles
              <span class="text-gray-600 text-xs text-left">(Change and you need to login again)</span>
            </span>
            <select
              :disabled="selectedRole === roles.Guest"
              v-model="selectedRole"
              class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            >
              <option v-for="role in roles" :key="role" :value="role">{{ role }}</option>
            </select>
            <span v-if="selectedRole === roles.Guest" class="text-red-700 text-xs text-left"
              >You need to contact an Admin to change your role</span
            >
          </label>
        </div>
      </div>
      <!-- Personname -->
      <div class="flex justify-between mt-4">
        <label class="text-left block text-sm w-full mr-2">
          <span class="text-gray-600 dark:text-gray-400">Firstname</span>
          <input
            type="text"
            v-model="user.personName.firstName"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            :placeholder="user.personName.firstName === '' ? '-' : user.personName.firstName"
          />
        </label>
        <label class="text-left block text-sm w-full mr-2">
          <span class="text-gray-600 dark:text-gray-400">Middlename</span>
          <input
            type="text"
            v-model="user.personName.middleName"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            :placeholder="user.personName.middleName === '' ? '-' : user.personName.middleName"
          />
        </label>
        <label class="text-left block text-sm w-full mr-2">
          <span class="text-gray-600 dark:text-gray-400">Lastname</span>
          <input
            type="text"
            v-model="user.personName.lastName"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            :placeholder="user.personName.lastName === '' ? '-' : user.personName.lastName"
          />
        </label>
      </div>
      <!-- Other -->
      <label class="text-left block text-sm w-full mr-2 mt-4">
        <span class="text-gray-600 dark:text-gray-400">Email</span>
        <input
          type="text"
          v-model="user.email"
          class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          :placeholder="user.email === '' || user.email === null ? '-' : user.email"
        />
      </label>
      <label class="text-left block text-sm w-full mr-2 mt-4">
        <span class="text-gray-600 dark:text-gray-400">Phonenumber</span>
        <input
          type="text"
          v-model="user.phoneNumber"
          class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          :placeholder="user.phoneNumber === '' || user.phoneNumber === null ? '-' : user.phoneNumber"
        />
      </label>
      <label class="text-left block text-sm w-full mr-2 mt-4">
        <span class="text-gray-600 dark:text-gray-400">Personinfo</span>
        <input
          type="text"
          v-model="user.personInfo"
          class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          :placeholder="user.personInfo === '' ? '-' : user.personInfo"
        />
      </label>
      <div class="text-gray-500 text-sm text-left mt-10">Last modified by: {{ user.lastModifiedBy }}</div>
      <div class="text-gray-500 text-sm text-left">Last modified at: {{ user.lastModifiedAt }}</div>
    </div>
    <div v-else>Something went wrong loading your account data...</div>
    <!-- Save button -->
    <div class="md:w-2/12 mt-3">
      <button
        @click="onSaveClick"
        class="block w-full px-4 py-2 mt-4 text-sm text-gray-500 font-medium leading-5 text-center bg-white hover:text-white transition-colors duration-150 hover:bg-indigo-500 border border-transparent rounded-lg active:bg-ui-primary focus:outline-none focus:shadow-outlineIndigo"
      >
        Save
      </button>
    </div>
  </div>
</template>

<script lang="ts">
import { useStore } from 'vuex';
import { defineComponent, ref, computed, reactive } from 'vue';
import { getUserRoles, logout } from '@/services/auth/authService';
import { Roles } from '@/types/enums';
import { UserUpdateRequest } from '@/types/types';
import { AuthActionTypes } from '@/store/auth/actions';

export default defineComponent({
  name: 'MyUser',
  components: {},
  setup() {
    const store = useStore();
    const user = computed(() => store.state.authModule.Me);
    const userRole = computed(() => getUserRoles());
    const selectedRole = ref(userRole.value);
    const prevRole = selectedRole.value;
    const roles = Roles;
    const updateUserRequest: UserUpdateRequest = reactive({
      userName: '',
      personInfo: '',
      firstName: '',
      middleName: '',
      lastName: '',
      email: '',
      phoneNumber: '',
      newRole: ''
    });
    const onSaveClick = async () => {
      if (user.value === undefined) {
        return;
      }
      updateUserRequest.userName = user.value.userName;
      updateUserRequest.personInfo = user.value.personInfo === null ? '' : user.value.personInfo;
      updateUserRequest.firstName =
        user.value.personName.firstName === null ? '' : user.value.personName.firstName;
      updateUserRequest.middleName =
        user.value.personName.middleName === null ? '' : user.value.personName.middleName;
      updateUserRequest.lastName =
        user.value.personName.lastName === null ? '' : user.value.personName.lastName;
      updateUserRequest.email = user.value.email === null ? '' : user.value.email;
      updateUserRequest.phoneNumber = user.value.phoneNumber === null ? '' : user.value.phoneNumber;
      updateUserRequest.newRole = selectedRole.value;
      await store.dispatch(AuthActionTypes.UPDATE_ME, updateUserRequest);
      if (updateUserRequest.newRole !== prevRole) {
        logout();
      }
    };
    return {
      user,
      updateUserRequest,
      onSaveClick,
      roles,
      selectedRole
    };
  }
});
</script>

<style scoped></style>
