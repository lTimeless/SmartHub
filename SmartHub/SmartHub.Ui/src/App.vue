<template>
  <v-app>
    <v-main class="main">
      <router-view />
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'SmartHubUi',
  components: {}
});
</script>
<style lang="scss" scoped>
.main {
  /*background-color: #eceff1;*/
  background-color: #eeeeee;
}
</style>
