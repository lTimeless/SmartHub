<template>
  <div class="registration">
    <v-container class="fully-centered">
      <v-card class="card">
        <v-toolbar class="d-flex justify-center mb-6" flat>
          <v-toolbar-title>
            <h2>
              {{ welcomeToSmartHub }}
            </h2>
          </v-toolbar-title>
        </v-toolbar>
        <v-stepper v-model="startStep" class="elevation-0">
          <v-stepper-header class="elevation-0">
            <v-stepper-step :complete="startStep > 1" step="1"
              >User</v-stepper-step
            >
            <v-divider></v-divider>
            <v-stepper-step :complete="startStep > 2" step="2"
              >Home</v-stepper-step
            >
            <v-divider></v-divider>
            <v-stepper-step step="3">Done</v-stepper-step>
          </v-stepper-header>

          <v-stepper-items>
            <v-stepper-content step="1">
              <v-card class="mb-12 elevation-0" height="200px">
                <v-form>
                  <v-container class="form-centered">
                    <v-row>
                      <v-col cols="14">
                        <v-text-field
                          :prepend-inner-icon="'mdi-account'"
                          v-model="username"
                          class="inputField"
                          outlined
                          label="Username"
                          required
                        >
                        </v-text-field>
                        <v-text-field
                          v-model="password"
                          :prepend-inner-icon="'mdi-lock'"
                          :append-icon="showPwd ? 'mdi-eye' : 'mdi-eye-off'"
                          :rules="[rules.required, rules.min]"
                          :type="showPwd ? 'text' : 'password'"
                          name="input-10-1"
                          label="Password"
                          hint="At least 4 characters"
                          counter
                          @click:append="showPwd = !showPwd"
                          outlined
                        ></v-text-field>
                        <v-text-field
                          v-model="passwordRetry"
                          :prepend-inner-icon="'mdi-lock'"
                          :rules="[rules.required, rules.retry]"
                          :type="'password'"
                          name="input-10-1"
                          label="Password wiederholen"
                          hint="Needs to be the same"
                          outlined
                        ></v-text-field>
                      </v-col>
                      <v-col cols="14">
                        <v-select
                          :items="items"
                          :prepend-inner-icon="'mdi-shield-account'"
                          label="Role"
                          outlined
                        ></v-select>
                      </v-col>
                    </v-row>
                  </v-container>
                </v-form>
              </v-card>

              <v-row>
                <v-col cols="14" class="d-flex justify-end">
                  <v-btn class="reg " color="primary" @click="startStep = 2"
                    >Continue</v-btn
                  >
                </v-col>
              </v-row>
            </v-stepper-content>

            <v-stepper-content step="2">
              <v-card
                class="mb-12"
                color="grey lighten-1"
                height="200px"
              ></v-card>

              <v-row>
                <v-col cols="6" class="d-flex justify-start">
                  <v-btn @click="startStep = 1" text>Back</v-btn>
                </v-col>
                <v-col cols="6" class="d-flex justify-end">
                  <v-btn class="reg" color="primary" @click="startStep = 3"
                    >Continue</v-btn
                  >
                </v-col>
              </v-row>
            </v-stepper-content>

            <v-stepper-content step="3">
              <v-card
                class="mb-12"
                color="grey lighten-1"
                height="200px"
              ></v-card>

              <v-row>
                <v-col cols="6" class="d-flex justify-start">
                  <v-btn @click="startStep = 2" text>Back</v-btn>
                </v-col>
                <v-col cols="6" class="d-flex justify-end">
                  <v-btn
                    class="reg"
                    color="primary"
                    @click.prevent="onRegistrationClick"
                    >Register</v-btn
                  >
                </v-col>
              </v-row>
            </v-stepper-content>
          </v-stepper-items>
        </v-stepper>

        <v-card-actions class="actions">
          <v-row class="pa-0">
            <v-col cols="12" class="d-flex justify-center">
              <p>
                Bereits registriert?
                <router-link to="/login">Hier</router-link>
                einloggen.
              </p>
              <v-spacer></v-spacer>
            </v-col>
          </v-row>
        </v-card-actions>
      </v-card>
    </v-container></div
></template>

<script lang="ts">
import Component from "vue-class-component";
import Vue from "vue";
import { InputMessage } from "vuetify";

@Component
export default class Registration extends Vue {
  welcomeToSmartHub = "Welcome to SmartHub";
  startStep = 1;
  showPwd = false;
  password = "";
  passwordRetry = "";
  username = "";
  items = ["Guest", "User", "Admin"];

  rules = {
    required: (value: InputEvent) => !!value || "Required.",
    min: (v: InputMessage) => v.length >= 4 || "Min 4 characters",
    retry: (v: InputMessage) => v !== "" || "Not the same passwords" // TODO: add matching logic with pwd
  };
  onRegistrationClick(): void {
    console.log("registration");
  }
}
</script>

<style scoped lang="scss">
.registration {
  width: 100%;
  height: 100vh;
  display: flex;
  background-color: #fafafb;
  .fully-centered {
    align-self: center;
  }

  .card {
    margin-left: auto;
    margin-right: auto;
    max-width: 100%;
  }
  .form-centered {
    align-self: center;
    padding: 0 2em;
  }
  .inputField {
    width: 100%;
  }
  .reg {
    width: 14em;
  }
  p {
    margin: 0;
  }
}
</style>
