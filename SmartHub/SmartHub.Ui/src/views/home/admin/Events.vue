<template>
  <div class="w-full">
    <h1 class="text-3xl py-4 border-b mb-4">Events</h1>
    <div class="flex justify-between items-center">
      <Search :data="eventsArray" :search-keys="searchKeys" @search-result="getSearchResult" @toggle-table="toggleTable" />
      <div class="w-1/3 flex justify-end">
        <span
          v-if="connectionEstablished"
          class="w-1/3 text-xs font-semibold inline-block py-2 px-3 uppercase rounded text-indigo-600 bg-indigo-200 uppercase ml-3
                  mb-3"
        >
          Connected
        </span>
        <span
          v-if="!connectionEstablished"
          class="w-1/3 text-xs font-semibold inline-block py-2 px-3 uppercase rounded text-red-600 bg-red-200 uppercase ml-3
                     mb-3"
        >
          Not connected
        </span>
      </div>
    </div>

    <template v-if="!showSearchTable">
      <div class="tableContainer">
        <table class="border-collapse table-auto w-full whitespace-no-wrap bg-white table-striped relative">
          <thead>
            <tr class="text-left">
              <th
                v-for="heading in headers"
                :key="heading.value"
                class="sticky top-0 border-b py-2"
                v-text="heading.text"
                :class="{ [heading.value]: true }"
              ></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="event in eventsArray" :key="event.id" class="hover:bg-indigo-200">
              <td>
                <span class="text-gray-700 flex items-center" v-text="event.eventType"></span>
              </td>
              <td>
                <span class="text-gray-700 flex items-center" v-text="event.data"></span>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </template>
    <template v-else>
      <div class="tableContainer">
        <table class="border-collapse table-auto w-full whitespace-no-wrap bg-white table-striped relative">
          <thead>
            <tr class="text-left">
              <th
                v-for="heading in headers"
                :key="heading.value"
                class="sticky top-0 border-b py-2"
                v-text="heading.text"
                :class="{ [heading.value]: true }"
              ></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in searchResultArray" :key="item.id" class="hover:bg-indigo-200">
              <td>
                <span class="text-gray-700 flex items-center">{{ item.item.eventType }}</span>
              </td>
              <td>
                <span class="text-gray-700 flex items-center">{{ item.item.data }}</span>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </template>
  </div>
</template>

<script lang="ts">
import { defineComponent, onUnmounted, ref, reactive } from 'vue';
import { HubConnectionBuilder, LogLevel, HubConnection } from '@microsoft/signalr';
import { ModifiedEvent, ServerEvent } from '@/types/types';
import Search from '@/components/Search.vue';

export default defineComponent({
  name: 'Events',
  components: {
    Search
  },
  setup() {
    const connection: HubConnection = new HubConnectionBuilder()
      .withUrl('/api/hub/events')
      .configureLogging(LogLevel.Information)
      .build();
    const showSearchTable = ref(false);
    const eventsArray = reactive<ModifiedEvent[]>([]);
    const searchResultArray = ref<ModifiedEvent[]>([]);
    const connectionEstablished = ref(false);
    const headers = [
      { text: 'EventType', value: 'eventType' },
      { text: 'Data', value: 'data' }
    ];
    const searchKeys = ['id', 'eventType', 'data'];

    connection.onclose(() => {
      connectionEstablished.value = false;
    });

    connection
      .start()
      .then(() => {
        connectionEstablished.value = true;
        connection.on('SendEvent', (data: ServerEvent) => {
          const dataArray = Object.entries(data);
          let id = '';
          let eventType = '';
          let dataString = '';
          dataArray.forEach((obj) => {
            if (obj[0] === 'eventType') {
              const [, second] = obj;
              eventType = second;
            } else if (obj[0] === 'id') {
              [, id] = obj;
            } else {
              dataString += `${obj[0]}: ${obj[1]}; `;
            }
          });
          const modifiedEvents = { id, eventType, data: dataString } as ModifiedEvent;
          eventsArray.push(modifiedEvents);
        });
      })
      .catch((err) => console.error(err));

    onUnmounted(() => {
      connection.stop();
    });

    const getSearchResult = (result: ModifiedEvent[]) => {
      searchResultArray.value = [];
      searchResultArray.value.push(...result);
    };

    const toggleTable = (value: boolean) => {
      showSearchTable.value = value;
      searchResultArray.value = [];
    };

    return {
      eventsArray,
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
