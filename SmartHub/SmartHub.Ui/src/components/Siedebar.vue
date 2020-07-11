<template>
  <v-navigation-drawer :mini-variant="drawer" app clipped>
    <v-layout
      align-start
      justify-space-between
      column
      fill-height
      class="layout"
    >
      <v-list width="100%" dense class="pa-0">
        <v-list-item two-line class="pl-2 v-list-item">
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

        <v-subheader class="mt-2 grey--text text--darken-1">USER</v-subheader>
        <template v-if="isAdmin || isUser || isGuest">
          <v-list-item
            class="v-list-item"
            color="primary"
            v-for="item in allList"
            :key="item.title"
            :to="item.route"
            link
          >
            <v-tooltip
              right
              nudge-right="10"
              :open-delay="tooltipOptions.tooltipOpenDelay"
              :transition="tooltipOptions.tooltipTransition"
            >
              <template v-slot:activator="{ on, attrs }">
                <v-list-item-action v-bind="attrs" v-on="on">
                  <v-icon>{{ item.icon }}</v-icon>
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    {{ item.title }}
                  </v-list-item-title>
                </v-list-item-content>
              </template>
              <span>{{ item.title }}</span>
            </v-tooltip>
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
            <v-tooltip
              right
              nudge-right="10"
              :open-delay="tooltipOptions.tooltipOpenDelay"
              :transition="tooltipOptions.tooltipTransition"
            >
              <template v-slot:activator="{ on, attrs }">
                <v-list-item-action v-bind="attrs" v-on="on">
                  <v-icon>{{ item.icon }}</v-icon>
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    {{ item.title }}
                  </v-list-item-title>
                </v-list-item-content>
              </template>
              <span>{{ item.title }}</span>
            </v-tooltip>
          </v-list-item>
        </template>

        <template v-if="isAdmin">
          <v-subheader class="mt-2 grey--text text--darken-1"
            >ADMIN</v-subheader
          >
          <v-list-item
            class="v-list-item"
            color="primary"
            v-for="item in adminList"
            :key="item.title"
            :to="item.route"
            link
          >
            <v-tooltip
              right
              nudge-right="10"
              :open-delay="tooltipOptions.tooltipOpenDelay"
              :transition="tooltipOptions.tooltipTransition"
            >
              <template v-slot:activator="{ on, attrs }">
                <v-list-item-action v-bind="attrs" v-on="on">
                  <v-icon>{{ item.icon }}</v-icon>
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    {{ item.title }}
                  </v-list-item-title>
                </v-list-item-content>
              </template>
              <span>{{ item.title }}</span>
            </v-tooltip>
          </v-list-item>
        </template>

        <template v-if="isAdmin || isUser || isGuest">
          <v-subheader class="mt-2 grey--text text--darken-1">HELP</v-subheader>
          <v-list-item
            class="v-list-item"
            color="primary"
            v-for="item in helpList"
            :key="item.title"
            :to="item.route"
            link
          >
            <v-tooltip
              right
              nudge-right="10"
              :open-delay="tooltipOptions.tooltipOpenDelay"
              :transition="tooltipOptions.tooltipTransition"
            >
              <template v-slot:activator="{ on, attrs }">
                <v-list-item-action v-bind="attrs" v-on="on">
                  <v-icon>{{ item.icon }}</v-icon>
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    {{ item.title }}
                  </v-list-item-title>
                </v-list-item-content>
              </template>
              <span>{{ item.title }}</span>
            </v-tooltip>
          </v-list-item>
        </template>
      </v-list>

      <v-list width="100%" dense class="pa-0">
        <v-list-item
          link
          justify-end
          color="primary"
          class="v-list-item"
          @click="logout"
        >
          <v-tooltip
            right
            nudge-right="10"
            :open-delay="tooltipOptions.tooltipOpenDelay"
            :transition="tooltipOptions.tooltipTransition"
          >
            <template v-slot:activator="{ on, attrs }">
              <v-list-item-action v-bind="attrs" v-on="on">
                <v-icon>mdi-logout-variant</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Logout
                </v-list-item-title>
              </v-list-item-content>
            </template>
            <span>Logout</span>
          </v-tooltip>
        </v-list-item>
      </v-list>
    </v-layout>
  </v-navigation-drawer>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { Prop } from "vue-property-decorator";
import { userAuth } from "@/services/auth/user";
import router from "@/router";

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
  tooltipOptions = {
    tooltipOpenDelay: 150,
    tooltipTransition: "slide-x-transition"
  };

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

  mounted() {
    const { isAdmin, isUser, isGuest } = userAuth();
    this.isAdmin = isAdmin;
    this.isGuest = isGuest;
    this.isUser = isUser;
  }

  logout() {
    localStorage.removeItem("loginResponse");
    router.push("Login");
  }
}
</script>

<style scoped>
.layout {
  max-height: 100%;
  overflow: auto;
}

.dot {
  width: 50px;
  height: 50px;
  border-radius: 50%;
  font-size: 19px;
  color: #fff;
  line-height: 50px;
  text-align: center;
}

.v-list-item {
  height: 4%;
}
</style>
