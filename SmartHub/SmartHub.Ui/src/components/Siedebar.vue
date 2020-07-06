<template>
  <v-navigation-drawer v-model="drawer" app clipped>
    <v-list dense>
      <v-subheader class="mt-4 grey--text text--darken-1">USER</v-subheader>
      <v-list-item
        color="primary"
        v-for="item in userList"
        :key="item.title"
        :to="item.route"
        link
      >
        <v-list-item-action>
          <v-icon>{{ item.icon }}</v-icon>
        </v-list-item-action>
        <v-list-item-content>
          <v-list-item-title>
            {{ item.title }}
          </v-list-item-title>
        </v-list-item-content>
      </v-list-item>

      <v-subheader class="mt-4 grey--text text--darken-1">ADMIN</v-subheader>
      <v-list-item
        color="primary"
        v-for="item in adminList"
        :key="item.title"
        :to="item.route"
        link
      >
        <v-list-item-action>
          <v-icon>{{ item.icon }}</v-icon>
        </v-list-item-action>
        <v-list-item-content>
          <v-list-item-title>
            {{ item.title }}
          </v-list-item-title>
        </v-list-item-content>
      </v-list-item>
    </v-list>
  </v-navigation-drawer>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { Prop } from "vue-property-decorator";

@Component
export default class Siedebar extends Vue {
  @Prop({ type: Boolean, required: true })
  private drawer!: boolean;

  userList = [
    { title: "Dashboard", icon: "mdi-view-dashboard", route: "/" },
    { title: "Settings", icon: "mdi-cog", route: "/settings" },
    { title: "About", icon: "mdi-help-box", route: "/about" }
  ];
  adminList = [
    { title: "Browse Channels", icon: "mdi-plus-circle-outline" },
    { title: "Manage Subscriptions", icon: "mdi-cog" }
  ];
}
</script>

<style scoped></style>
