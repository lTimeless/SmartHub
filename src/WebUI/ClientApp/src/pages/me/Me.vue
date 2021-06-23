<script lang="ts">
import { defineComponent, ref, computed, reactive } from 'vue';
import { Roles, Routes } from '@/types/enums';
import Loader from '@/components/app/AppSpinner.vue';
import { useRouter } from 'vue-router';
import { useIdentity } from '@/hooks/useIdentity';
import { UpdateUserInput } from '@/graphql/graphql.types';
import { useUpdateUserMutation } from '@/graphql/mutations/users/updateUser.generated';
import { useGetMeQuery } from '@/graphql/queries/getMe.generated';

export default defineComponent({
  name: 'Me',
  components: {
    Loader
  },
  setup() {
    const router = useRouter();
    const { isRole, clearIdentity } = useIdentity();
    const userRole = computed(() => isRole());
    const selectedRole = ref(userRole.value);
    const prevRole = selectedRole.value;
    const roles = Roles;
    const updateUserInput: UpdateUserInput = reactive({
      userId: ''
    });
    const { executeMutation: updateUser, fetching: loadUpdate, error: errUpdate } = useUpdateUserMutation();
    const { data: resultUser, fetching: loadUser, error: errUser } = useGetMeQuery({requestPolicy: 'network-only'});

    const onSaveClick = async () => {
      const user = resultUser.value?.getMe.user;
      if (user) {
        updateUserInput.userId = user.id!;
        updateUserInput.newRole = selectedRole.value;
        await updateUser({ input: updateUserInput });
        if (typeof updateUserInput.newRole !== 'undefined' && updateUserInput.newRole !== prevRole) {
          clearIdentity();
          await router.push({ path: Routes.Login, replace: true });
        }
      }
    };
    return {
      resultUser,
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

<template>
  <div class="relative flex-col w-full justify-end bg-white border p-3 rounded">
    <!-- Form -->
    <template v-if="loadUser && !resultUser">
      <div class="flex items-center justify-center w-full h-full">
        <Loader height="h-48" width="w-48" />
      </div>
    </template>
    <template v-else-if="errUser">
      <div class="flex items-center justify-center w-full h-full">
        <p>Error: {{ errUser.name }} {{ errUser.message }}</p>
      </div>
    </template>
    <template v-if="resultUser && resultUser.getMe.user">
      <!-- Username -->
      <div class="flex mr-2 justify-between">
        <div class="w-1/3 mr-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">Username</span>
            <input
              v-model="updateUserInput.userName"
              type="text"
              class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
              :placeholder="resultUser.getMe.user.userName ?? '-'"
              disabled
            />
          </label>
        </div>
        <div class="w-1/3 ml-2">
          <label class="text-left block text-sm">
            <span class="text-gray-600 dark:text-gray-400">
              Roles
              <span class="text-gray-600 text-xs text-left">(Change and you need to login again)</span>
            </span>
            <select
              v-model="selectedRole"
              :disabled="selectedRole === roles.Guest"
              class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            >
              <option v-for="role in roles" :key="role" :value="role">{{ role }}</option>
            </select>
            <span v-if="selectedRole === roles.Guest" class="text-red-700 text-xs text-left">
              You need to contact an Admin to change your role
            </span>
          </label>
        </div>
      </div>
      <!-- Personname -->
      <div class="flex justify-between mt-4">
        <label class="text-left block text-sm w-full mr-2">
          <span class="text-gray-600 dark:text-gray-400">Firstname</span>
          <input
            v-model="updateUserInput.firstName"
            type="text"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            :placeholder="
              resultUser.getMe.user.personName.firstName === '' ? '-' : resultUser.getMe.user.personName.firstName
            "
          />
        </label>
        <label class="text-left block text-sm w-full mr-2">
          <span class="text-gray-600 dark:text-gray-400">Middlename</span>
          <input
            v-model="updateUserInput.middleName"
            type="text"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            :placeholder="
              resultUser.getMe.user.personName.middleName === '' ? '-' : resultUser.getMe.user.personName.middleName
            "
          />
        </label>
        <label class="text-left block text-sm w-full mr-2">
          <span class="text-gray-600 dark:text-gray-400">Lastname</span>
          <input
            v-model="updateUserInput.lastName"
            type="text"
            class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
            :placeholder="
              resultUser.getMe.user.personName.lastName === '' ? '-' : resultUser.getMe.user.personName.lastName
            "
          />
        </label>
      </div>
      <!-- Other -->
      <label class="text-left block text-sm w-full mr-2 mt-4">
        <span class="text-gray-600 dark:text-gray-400">Email</span>
        <input
          v-model="updateUserInput.email"
          type="text"
          class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          :placeholder="
            resultUser.getMe.user.email === '' || resultUser.getMe.user.email === null
              ? '-'
              : resultUser.getMe.user.email
          "
        />
      </label>
      <label class="text-left block text-sm w-full mr-2 mt-4">
        <span class="text-gray-600 dark:text-gray-400">Phonenumber</span>
        <input
          v-model="updateUserInput.phoneNumber"
          type="text"
          class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          :placeholder="
            resultUser.getMe.user.phoneNumber === '' || resultUser.getMe.user.phoneNumber === null
              ? '-'
              : resultUser.getMe.user.phoneNumber
          "
        />
      </label>
      <label class="text-left block text-sm w-full mr-2 mt-4">
        <span class="text-gray-600 dark:text-gray-400">Personinfo</span>
        <input
          v-model="updateUserInput.personInfo"
          type="text"
          class="mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full sm:text-sm border-gray-300 rounded-md"
          :placeholder="resultUser.getMe.user.personInfo === '' ? '-' : resultUser.getMe.user.personInfo"
        />
      </label>
      <div class="text-gray-500 text-sm text-left mt-10">
        Last modified by: {{ resultUser.getMe.user.lastModifiedBy }}
      </div>
      <div class="text-gray-500 text-sm text-left">
        Last modified at: {{ resultUser.getMe.user.lastModifiedAt }}
      </div>
    </template>
    <!-- Save button -->
    <div class="md:w-2/12 mt-3">
      <button
        class="block w-full px-4 py-2 mt-4 text-sm text-gray-500 font-medium leading-5 text-center bg-white hover:text-white transition-colors duration-150 hover:bg-indigo-500 border border-transparent rounded-lg active:bg-ui-primary focus:outline-none focus:shadow-outlineIndigo"
        @click="onSaveClick"
      >
        Save
      </button>
    </div>
  </div>
</template>
