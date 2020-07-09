<template>
  <v-container>
    <v-row>
      <v-col cols="12" md="8">

      </v-col>
    </v-row>
    <div>
      <apexchart width="500" :options="options" :series="series"></apexchart>
    </div>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { ApexOptions } from "apexcharts";

@Component
export default class Dashboard extends Vue {
  options: ApexOptions = {
    chart: {
      id: "vuechart-example",
      type: "bar",
      height: 600,
      width: 600,
      toolbar: {
        show: false
      }
    },
    xaxis: {
      categories: [1991, 1992, 1993, 1994, 1995, 1996, 1997, 1998]
    }
  };
  series = [
    {
      name: "series-1",
      data: [30, 40, 45, 50, 49, 60, 70, 91]
    }
  ];
}
</script>

<style scoped></style>
