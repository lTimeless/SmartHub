<template>
  <div class="w-full">
    <h1 class="text-3xl text-gray-500 font-bold mb-6">Events</h1>
    <div class="flex justify-between items-center mb-4">
      <Search :data="activityArray" :search-keys="searchKeys" @search-result="getSearchResult" @toggle-table="toggleTable" />
      <div class="w-1/3 flex justify-end">
        <span v-if="connectionEstablished" class="w-1/3 text-xs font-semibold inline-block py-2 px-2 rounded text-indigo-600 bg-indigo-200 uppercase ml-3">
          Connected
        </span>
        <span v-if="!connectionEstablished" class="w-1/3 text-xs font-semibold inline-block py-2 px-2 rounded text-red-600 bg-red-200 uppercase ml-3">
          Not connected
        </span>
      </div>
      <span class="text-red-600 flex items-center">{{ error }}</span>
    </div>

    <template v-if="!showSearchTable">
      <Table :headers="headers">
        <tr v-for="activity in activityArray" :key="activity.id" class="hover:bg-indigo-200">
          <td>
            <span class="text-gray-700 flex items-center" v-text="activity.dateTime"></span>
          </td>
          <td>
            <span class="text-gray-700 flex items-center" v-text="activity.username"></span>
          </td>
          <td>
            <span class="text-gray-700 flex items-center" v-text="activity.message"></span>
          </td>
          <td>
            <span class="text-gray-700 flex items-center"> {{ activity.successfulRequest ?? '-' }}</span>
          </td>
          <td>
            <span class="text-gray-700 flex items-center">{{ activity.executionTime }} <span class="font-normal">ms</span></span>
          </td>
        </tr>
      </Table>
    </template>
    <template v-else>
      <Table :headers="headers">
        <tr v-for="activity in searchResultArray" :key="activity.id" class="hover:bg-indigo-200">
          <td>
            <span class="text-gray-700 flex items-center" v-text="activity.item.dateTime"></span>
          </td>
          <td>
            <span class="text-gray-700 flex items-center" v-text="activity.item.username"></span>
          </td>
          <td>
            <span class="text-gray-700 flex items-center" v-text="activity.item.message"></span>
          </td>
          <td>
            <span class="text-gray-700 flex items-center" v-text="activity.item.successfulRequest"></span>
          </td>
          <td>
            <span class="text-gray-700 flex items-center" v-text="activity.item.executionTime"></span>
          </td>
        </tr>
      </Table>
    </template>
  </div>
</template>

<script lang="ts">
import { defineComponent, onUnmounted, ref, reactive, watch } from 'vue';
import { ServerActivity } from '@/types/types';
import Search from '@/components/widgets/AppSearch.vue';
import Table from '@/components/widgets/AppTable.vue';
import { useSignalRHub } from '@/hooks/useSignalR';

export default defineComponent({
  name: 'Events',
  components: {
    Search,
    Table
  },
  setup() {
    const {connectionEstablished, data, error, connection} = useSignalRHub<ServerActivity>('activity','SendActivity');

    const showSearchTable = ref(false);
    const activityArray = reactive<ServerActivity[]>([]);
    const searchResultArray = ref<ServerActivity[]>([]);
    const headers = [
      { text: 'Date', value: 'dateTime' },
      { text: 'Requestor', value: 'userName' },
      { text: 'Message', value: 'message' },
      { text: 'SuccessfulRequest', value: 'successfulRequest' },
      { text: 'ExecutionTime', value: 'executionTime' }
    ];
    watch(data, (newData) => {
      if (newData) {
        activityArray.push(newData);
      }
    });
    const searchKeys = ['DateTime', 'Requestor', 'SuccessfulRequest', 'ExecutionTime' ];

    onUnmounted(() => {
      connection.value.stop();
    });

    const getSearchResult = (result: ServerActivity[]) => {
      searchResultArray.value = [];
      searchResultArray.value.push(...result);
    };

    const toggleTable = (value: boolean) => {
      showSearchTable.value = value;
      searchResultArray.value = [];
    };

    return {
      activityArray,
      error,
      connectionEstablished,
      headers,
      searchKeys,
      showSearchTable,
      searchResultArray,
      getSearchResult,
      toggleTable
    };
  }
});
</script>

<style scoped>
.tableContainer {
  height: 600px;
  @apply overflow-x-auto bg-white rounded-lg shadow overflow-y-auto relative;
}
</style>
