<template>
  <BaseModal
    title="Create new Device"
    saveBtnTitle="Create"
    closeBtnTitle="Cancel"
    :close="close"
    :save="save"
    :saveBtnActive="saveBtnActive"
    headerColor="bg-orange-400"
  >
    <div class="flex justify-between">
      <div class="w-full mr-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Name</span>
          <input
            v-model="deviceCreateRequest.name"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            :placeholder="deviceCreateRequest.name"
          />
        </label>
      </div>
      <div class="w-1/3 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">PluginTypes</span>
          <select
            v-model="deviceCreateRequest.pluginTypes"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-select"
          >
            <option v-for="(item, key) in returnEnumNames(pluginTypes)" :key="key" :value="key">{{ item }}</option>
          </select>
        </label>
      </div>
    </div>
    <div class="flex justify-between mt-3">
      <div class="w-full mr-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Description</span>
          <input
            v-model="deviceCreateRequest.description"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
            focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            placeholder="Group description"
          />
        </label>
      </div>
      <div class="w-1/3 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Ipv4</span>
          <input
            v-model="deviceCreateRequest.ipv4"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
            focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            placeholder="Group name"
          />
        </label>
      </div>
    </div>
    <!-- Groupname -->
    <div class="flex justify-between mt-3">
      <div class="w-full mr-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Groupname</span>
          <input
            v-model="groupName"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
            focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            placeholder="Groupname"
          />
        </label>
      </div>
    </div>
    <!-- Company and pluginName -->
    <div class="flex justify-between mt-3">
      <div class="w-1/2 mr-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Companyname</span>
          <input
            v-model="deviceCreateRequest.companyName"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
            focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            placeholder="Group description"
          />
        </label>
      </div>
      <div class="w-1/2 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Pluginname</span>
          <input
            v-model="deviceCreateRequest.pluginName"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
            focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-input"
            placeholder="Group name"
          />
        </label>
      </div>
    </div>
    <div class="flex justify-between">
      <div class="w-1/2 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Primary connection</span>
          <select
            v-model="deviceCreateRequest.primaryConnection"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-select"
          >
            <option v-for="(item, key) in returnEnumNames(connTypes)" :key="key" :value="key">{{ item }}</option>
          </select>
        </label>
      </div>
      <div class="w-1/2 ml-2">
        <label class="text-left block text-sm">
          <span class="text-gray-600 dark:text-gray-400">Secondary connection</span>
          <select
            v-model="deviceCreateRequest.secondaryConnection"
            class="block w-full mt-1 text-sm dark:border-gray-600 dark:bg-gray-700 focus:border-purple-400
                focus:outline-none focus:shadow-outlineIndigo dark:text-gray-300 dark:focus:shadow-outline-gray form-select"
          >
            <option v-for="(item, key) in returnEnumNames(connTypes)" :key="key" :value="key">{{ item }}</option>
          </select>
        </label>
      </div>
    </div>
  </BaseModal>
</template>

<script lang="ts">
import { defineComponent, reactive, computed, ref } from 'vue';
import BaseModal from '@/components/modals/BaseModal.vue';
import { DeviceCreateRequest } from '@/types/types';
import { useStore } from 'vuex';
import { ConnectionTypes, PluginTypes } from '@/types/enums';
import { HomeActionTypes } from '@/store/home/actions';

export default defineComponent({
  name: 'DeviceCreateModal',
  emits: ['close-modal'],
  components: {
    BaseModal
  },
  setup(props, context) {
    const store = useStore();
    const deviceCreateRequest = reactive<DeviceCreateRequest>({
      name: '',
      description: '',
      groupId: '',
      ipv4: '',
      companyName: '',
      pluginName: '',
      pluginTypes: PluginTypes.None,
      primaryConnection: ConnectionTypes.None,
      secondaryConnection: ConnectionTypes.None
    });
    
    const groupName = ref('');
    const pluginTypes = PluginTypes;
    const connTypes = ConnectionTypes;
    const saveBtnActive = computed(() => deviceCreateRequest.name !== '' && deviceCreateRequest.ipv4 !== '');
    const returnEnumNames = (value: any) => Object.keys(value).filter(e => isNaN(+e)) as unknown as PluginTypes[];
    const close = () => {
      console.log(deviceCreateRequest);
      context.emit('close-modal', false);
    };
    console.log(returnEnumNames(pluginTypes));
    const save = async () => {
      if (groupName.value !== '') {
        const home = store.state.homeModule.home;
        const groupId = home?.groups?.find(x => x.name === groupName.value);
        if(!!groupId){
          deviceCreateRequest.groupId = groupId.id;
          const tgg = PluginTypes[deviceCreateRequest.pluginTypes as any];
          console.log('click save', deviceCreateRequest, tgg);
          // await store.dispatch(HomeActionTypes.CREATE_DEVICE, deviceCreateRequest).then(() => {
          //   context.emit('close-modal', false);
          // });
        }
        context.emit('close-modal', false);
      }
    };
    return {
      deviceCreateRequest,
      saveBtnActive,
      close,
      save,
      pluginTypes,
      groupName,
      connTypes,
      returnEnumNames
    };
  }
});
</script>

<style scoped></style>
