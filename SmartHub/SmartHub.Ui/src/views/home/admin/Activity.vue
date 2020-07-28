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
import { ServerEvent } from '@/types/types';

type ModifiedEvent = { id: string; eventType: string; data: string };

@Component
export default class Activity extends Vue {
  connection: any;
  search = '';
  itemsPerPage = 12;
  eventsDictionary: ModifiedEvent[] = [];
  connectionEstablished = false;
  headers = [
    { text: 'EventType', sortable: true, value: 'eventType' },
    { text: 'Data', sortable: true, value: 'data' }
  ];

  created() {
    this.connection = new HubConnectionBuilder()
      .withUrl('/api/hub')
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
        this.connection.on('SendEvent', (data: ServerEvent) => {
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
          this.eventsDictionary.push(modifiedEvents);
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
