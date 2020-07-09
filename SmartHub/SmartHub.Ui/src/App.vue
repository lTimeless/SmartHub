<template>
  <v-app>
    <v-main class="main">
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  name: "SmartHubUi",
  components: {},

  data: () => ({
    //
  })
});
</script>
<style lang="scss" scoped>
.main {
  background-color: #fafafb;
}
</style>
