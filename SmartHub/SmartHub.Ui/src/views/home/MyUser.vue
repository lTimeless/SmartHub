<template>
  <div class="flex-1 w-full justify-end">
    <h1 class="text-3xl text-gray-500 font-bold mb-6">{{ title }}</h1>
    <!-- Form -->
    <div v-if="user">
      <!-- Username -->
      <div class="flex mr-2 justify-between">
        <div class="w-1/3 mr-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">Username </span>
            <input
              v-model="user.userName"
              class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
              :placeholder="user.userName"
            />
          </label>
        </div>
        <div class="w-1/3 ml-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400"
              >Roles
              <span class="text-gray-500 text-sm text-left mt-10">(After changing, you need to login again)</span>
            </span>
            <select
              v-model="selectedRole"
              class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-select"
            >
              <option v-for="role in roles" :key="role" :value="role">{{ role }}</option>
            </select>
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
            :placeholder="user.personName.firstName === '' ? '-' : user.personName.firstName"
          />
        </label>
        <label class="text-left block text-sm w-full mr-2">
          <span class="text-gray-600 dark:text-gray-400">Middlename</span>
          <input
            v-model="user.personName.middleName"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            :placeholder="user.personName.middleName === '' ? '-' : user.personName.middleName"
          />
        </label>
        <label class="text-left block text-sm w-full mr-2">
          <span class="text-gray-600 dark:text-gray-400">Lastname</span>
          <input
            v-model="user.personName.lastName"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            :placeholder="user.personName.lastName === '' ? '-' : user.personName.lastName"
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
          :placeholder="user.email === '' || user.email === null ? '-' : user.email"
        />
      </label>
      <label class="text-left block text-sm w-full mr-2 mt-4">
        <span class="text-gray-600 dark:text-gray-400">Phonenumber</span>
        <input
          v-model="user.phoneNumber"
          class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
          :placeholder="user.phoneNumber === '' || user.phoneNumber === null ? '-' : user.phoneNumber"
        />
      </label>
      <label class="text-left block text-sm w-full mr-2 mt-4">
        <span class="text-gray-600 dark:text-gray-400">Personinfo</span>
        <input
          v-model="user.personInfo"
          class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
          :placeholder="user.personInfo === '' ? '-' : user.personInfo"
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
      <action-button title="Save" color="indigo" :height="35" :width="150" :callback="onSaveClick" />
    </div>
  </div>
</template>

<script lang="ts">
import { useStore } from 'vuex';
import { defineComponent, ref, computed, reactive } from 'vue';
import { getUserRoles, logout } from '@/services/auth/authService';
import ActionButton from '@/components/widgets/AppButton.vue';
import { Roles } from '@/types/enums';
import { UserUpdateRequest } from '@/types/types';
import { AuthActionTypes } from '@/store/auth/actions';

export default defineComponent({
  name: 'MyUser',
  components: {
    ActionButton
  },
  setup() {
    const store = useStore();
    const title = ref('MyUser');
    const user = computed(() => store.state.authModule.Me);
    const userRoles = computed(() => getUserRoles());
    const selectedRole = ref(userRoles.value);
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
      updateUserRequest.firstName = user.value.personName.firstName === null ? '' : user.value.personName.firstName;
      updateUserRequest.middleName = user.value.personName.middleName === null ? '' : user.value.personName.middleName;
      updateUserRequest.lastName = user.value.personName.lastName === null ? '' : user.value.personName.lastName;
      updateUserRequest.email = user.value.email === null ? '' : user.value.email;
      updateUserRequest.phoneNumber = user.value.phoneNumber === null ? '' : user.value.phoneNumber;
      updateUserRequest.newRole = selectedRole.value;
      await store.dispatch(AuthActionTypes.UPDATE_ME, updateUserRequest);
      if (updateUserRequest.newRole !== prevRole) {
        logout();
      }
    };
    return {
      title,
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
