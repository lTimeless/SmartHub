<template>
  <div>
    hier sieht man eine einfache live log page des systems später auch mit
    filterung des logs
    <br />
    <div>
      {{ testData }}
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";

@Component
export default class Logs extends Vue {
  testData = "";

  async mounted() {
    await Vue.axios
      .get("https://api.spacexdata.com/v3/launches")
      .then((response: any) => {
        this.testData = response.data[0];
        console.log(response);
      });
  }
}
</script>

<style scoped></style>
