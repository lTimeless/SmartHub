<template>
  <v-navigation-drawer
    :mini-variant="drawer"
    app
    clipped
  >
    <v-list dense>
      <v-list-item two-line class="pl-2">
        <v-list-item-avatar>
          <div class="dot" :style="{ 'background-color': imageBgColor }">
            {{ person.firstName.charAt(0) }}{{ person.lastName.charAt(0) }}
          </div>
        </v-list-item-avatar>
        <v-list-item-content>
          <v-list-item-title>{{ person.userName }}</v-list-item-title>
          <v-list-item-subtitle v-if="isAdmin || isUser"
            >Logged In</v-list-item-subtitle
          >
          <v-list-item-subtitle v-if="isGuest"
            >You are a guest</v-list-item-subtitle
          >
        </v-list-item-content>
      </v-list-item>

      <v-divider></v-divider>
      <v-subheader class="mt-4 grey--text text--darken-1">USER</v-subheader>
      <template v-if="isAdmin || isUser || isGuest">
        <v-list-item
          color="primary"
          v-for="item in allList"
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
      </template>
      <template v-if="isUser || isAdmin">
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
      </template>

      <template v-if="isAdmin">
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
      </template>

      <template v-if="isAdmin || isUser || isGuest">
        <v-subheader class="mt-4 grey--text text--darken-1">HELP</v-subheader>
        <v-list-item
          color="primary"
          v-for="item in helpList"
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
      </template>
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

  isAdmin = true;
  isUser = false;
  isGuest = false;
  person = {
    userName: "MaxTime",
    firstName: "Max",
    lastName: "ATestperson"
  };
  imageBgColor = "";

  allList = [{ title: "Dashboard", icon: "mdi-view-dashboard", route: "/" }];
  userList = [
    { title: "Plugins", icon: "mdi-toy-brick", route: "/plugins" },
    { title: "Routines", icon: "mdi-update", route: "/routines" },
    { title: "Statistics", icon: "mdi-chart-arc", route: "/statistics" },
    { title: "Settings", icon: "mdi-cog", route: "/settings" }
  ];
  adminList = [
    { title: "Activity", icon: "mdi-calendar-alert", route: "/activity" },
    { title: "Logs", icon: "mdi-file-document", route: "logs" },
    { title: "System", icon: "mdi-desktop-classic", route: "/system" },
    { title: "Health", icon: "mdi-clipboard-pulse", route: "/health" },
    { title: "Manager", icon: "mdi-monitor-edit", route: "/manager" }
  ];
  helpList = [{ title: "About", icon: "mdi-information", route: "/about" }];

  created() {
    this.imageBgColor =
      "#" + ((Math.random() * 0xffffff) << 0).toString(16).padStart(6, "0");
  }
}
</script>

<style scoped>
.dot {
  width: 50px;
  height: 50px;
  border-radius: 50%;
  font-size: 19px;
  color: #fff;
  line-height: 50px;
  text-align: center;
}
</style>
