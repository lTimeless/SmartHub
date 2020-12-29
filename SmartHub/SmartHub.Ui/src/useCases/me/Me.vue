<template>
  <div class="relative flex-1 w-full justify-end bg-white border p-3 rounded">
    <!-- Form -->
    <template v-if="loadUser">
      <div class="flex items-center justify-center w-full h-full">
        <Loader height="h-48" width="w-48" />
      </div>
    </template>
    <template v-else-if="errUser">
      <div class="flex items-center justify-center w-full h-full">
        <p>Error: {{ errUser.name }} {{ errUser.message }}</p>
      </div>
    </template>
    <template v-if="user">
      <!-- Username -->
      <div class="flex mr-2 justify-between">
        <div class="w-1/3 mr-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">Username</span>
            <input
              type="text"
              v-model="updateUserInput.userName"
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
            v-model="updateUserInput.firstName"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            :placeholder="user.personName.firstName === '' ? '-' : user.personName.firstName"
          />
        </label>
        <label class="text-left block text-sm w-full mr-2">
          <span class="text-gray-600 dark:text-gray-400">Middlename</span>
          <input
            type="text"
            v-model="updateUserInput.middleName"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            :placeholder="user.personName.middleName === '' ? '-' : user.personName.middleName"
          />
        </label>
        <label class="text-left block text-sm w-full mr-2">
          <span class="text-gray-600 dark:text-gray-400">Lastname</span>
          <input
            type="text"
            v-model="updateUserInput.lastName"
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
          v-model="updateUserInput.email"
          class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          :placeholder="user.email === '' || user.email === null ? '-' : user.email"
        />
      </label>
      <label class="text-left block text-sm w-full mr-2 mt-4">
        <span class="text-gray-600 dark:text-gray-400">Phonenumber</span>
        <input
          type="text"
          v-model="updateUserInput.phoneNumber"
          class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          :placeholder="user.phoneNumber === '' || user.phoneNumber === null ? '-' : user.phoneNumber"
        />
      </label>
      <label class="text-left block text-sm w-full mr-2 mt-4">
        <span class="text-gray-600 dark:text-gray-400">Personinfo</span>
        <input
          type="text"
          v-model="updateUserInput.personInfo"
          class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          :placeholder="user.personInfo === '' ? '-' : user.personInfo"
        />
      </label>
      <div class="text-gray-500 text-sm text-left mt-10">Last modified by: {{ user.lastModifiedBy }}</div>
      <div class="text-gray-500 text-sm text-left">Last modified at: {{ user.lastModifiedAt }}</div>
    </template>
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
import { defineComponent, ref, computed, reactive } from 'vue';
import { clearStorage, getUserRoles } from '@/services/auth/authService';
import { Roles, Routes } from '@/types/enums';
import { UpdateUserInput } from '@/types/types';
import { useMutation, useQuery, useResult } from '@vue/apollo-composable';
import Loader from '@/components/shared/Loader.vue';
import { UPDATE_USER } from '@/graphql/mutations';
import { WHO_AM_I } from '@/graphql/queries';
import { useRouter } from 'vue-router';

export default defineComponent({
  name: 'Me',
  components: {
    Loader
  },
  setup() {
    const userRole = computed(() => getUserRoles());
    const selectedRole = ref(userRole.value);
    const prevRole = selectedRole.value;
    const roles = Roles;
    const updateUserInput: UpdateUserInput = reactive({
      userId: ''
    });
    const { mutate: updateUser, loading: loadUpdate, error: errUpdate } = useMutation(UPDATE_USER);
    const { result: resultUser, loading: loadUser, error: errUser } = useQuery(WHO_AM_I, null, {
      fetchPolicy: 'no-cache'
    });
    const user = useResult(resultUser, null, (data) => data.me);

    const onSaveClick = async () => {
      if (typeof user.value === 'undefined') {
        return;
      }
      updateUserInput.userId = user.value.id;
      updateUserInput.newRole = selectedRole.value;
      await updateUser({ input: updateUserInput }, { refetchQueries: [{ query: WHO_AM_I }] });

      if (typeof updateUserInput.newRole !== 'undefined' && updateUserInput.newRole !== prevRole) {
        await clearStorage();
        const router = useRouter();
        await router.replace(Routes.Login);
      }
    };
    return {
      user,
      loadUser,
      errUser,
      loadUpdate,
      errUpdate,
      updateUserInput,
      onSaveClick,
      roles,
      selectedRole
    };
  }
});
</script>

<style scoped></style>
