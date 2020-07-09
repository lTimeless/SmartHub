<template>
  <div class="home">
    <v-app-bar app clipped-left color="primary" dark dense>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title class="mr-12 align-center">
        <span class="title">SmartHub</span>
      </v-toolbar-title>
      <v-row>
        <v-spacer></v-spacer>
        <v-col cols="4" class="mt-2">
          <v-text-field
            :append-icon-cb="() => {}"
            @keydown.enter="onEnterSearch"
            placeholder="Search..."
            outlined
            dense
            append-icon="mdi-magnify"
            color="white"
            hide-details
          ></v-text-field>
        </v-col>
        <v-spacer></v-spacer>
        <v-col cols="1" class="mt-4">
          <v-switch
            input-value="darkMode"
            :append-icon="darkModeIcon"
            inset
            @change="switchDarkMode"
          ></v-switch>
        </v-col>
      </v-row>
    </v-app-bar>
    <Sidebar :drawer="drawer"></Sidebar>
    <v-container class="fill-height" fluid>
      <router-view />
    </v-container>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import Sidebar from "../components/Siedebar.vue";

@Component({
  components: {
    Sidebar
  }
})
export default class Home extends Vue {
  drawer = false;
  darkMode = false;
  get darkModeIcon() {
    return this.darkMode ? "mdi-white-balance-sunny" : "mdi-weather-night";
  }

  switchDarkMode() {
    this.darkMode = !this.darkMode;
    this.$vuetify.theme.dark = !this.$vuetify.theme.dark;
  }

  onEnterSearch(): void {
    console.log("Search");
  }
}
</script>
<style lang="scss" scoped>
.home {
  background-color: #fafafb;
}
</style>
