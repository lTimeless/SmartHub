<template>
  <v-container>
    <v-subheader class="pa-1 grey--text text--darken-1">ACTIONS</v-subheader>
    <v-col cols="12">
      <v-row justify="space-between" align="center" class="ml-1">
        <QuickActionButton title="Home"></QuickActionButton>
        <QuickActionButton title="Group"></QuickActionButton>
        <QuickActionButton title="Device"></QuickActionButton>
        <QuickActionButton title="Routine"></QuickActionButton>
      </v-row>
    </v-col>
    <v-divider></v-divider>
    <v-subheader class="pa-1 grey--text text--darken-1">GRAPHS</v-subheader>
    <div>
      <apexchart width="500" :options="options" :series="series"></apexchart>
    </div>
    <v-divider></v-divider>
    <v-subheader class="pa-1 grey--text text--darken-1">TABLES</v-subheader>

  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { ApexOptions } from "apexcharts";
import QuickActionButton from "@/components/widgets/QuickActionButton.vue";

@Component({
  components: {
    QuickActionButton
  }
})
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
