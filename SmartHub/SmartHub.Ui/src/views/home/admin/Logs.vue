<template>
  <v-container>
    <v-text-field v-model="search" append-icon="mdi-magnify" label="Search" single-line hide-details></v-text-field>
    <p class="font-weight-light my-4">* Loading bar indicates that a live connection is established</p>
    <v-progress-linear v-if="connectionEstablished" color="primary" buffer-value="0" stream></v-progress-linear>
    <v-data-table
      :items-per-page="itemsPerPage"
      :headers="headers"
      :items="eventsDictionary"
      :search="search"
    ></v-data-table>
  </v-container>
</template>

<script lang="ts">
import Vue from 'vue';
import Component from 'vue-class-component';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { ServerLog } from '@/types/types';

@Component
export default class Logs extends Vue {
  connection: any;
  eventsDictionary: ServerLog[] = [];
  search = '';
  itemsPerPage = 12;
  connectionEstablished = false;
  headers = [
    { text: 'Timestamp', sortable: true, value: 'dateTime' },
    { text: 'Level', sortable: true, value: 'level' },
    { text: 'Message', sortable: false, value: 'message' },
    { text: 'Exception', sortable: false, value: 'exception' }
  ];

  created() {
    this.connection = new HubConnectionBuilder()
      .withUrl('/api/hub/logs')
      .configureLogging(LogLevel.Information)
      .build();
    this.connection.onclose(() => {
      this.connectionEstablished = false;
      console.log('SignalR connection closed');
    });
  }

  async mounted() {
    this.connection
      .start()
      .then(() => {
        this.connectionEstablished = true;
        this.connection.on('SendLog', (data: ServerLog) => {
          this.eventsDictionary.push(data);
        });
      })
      .catch((err: any) => console.error(err));
  }

  destroyed() {
    this.connection.stop();
    console.log('destroy');
  }
}
</script>

<style scoped></style>
