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
            <v-stepper-step :complete="startStep > 1" step="1">
              User
            </v-stepper-step>
            <v-divider />
            <v-stepper-step :complete="startStep > 2" step="2">
              Home
            </v-stepper-step>
            <v-divider />
            <v-stepper-step step="3">
              Done
            </v-stepper-step>
          </v-stepper-header>

          <v-stepper-items>
            <v-stepper-content step="1">
              <v-card class="mb-12 elevation-0" height="200px">
                <v-form>
                  <v-container class="form-centered">
                    <v-row>
                      <v-col cols="14">
                        <v-text-field
                          v-model="userName"
                          :prepend-inner-icon="'mdi-account'"
                          class="inputField"
                          outlined
                          label="Username"
                          required
                        />
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
                          outlined
                          @click:append="showPwd = !showPwd"
                        />
                        <v-text-field
                          v-model="passwordRetry"
                          :prepend-inner-icon="'mdi-lock'"
                          :rules="[rules.required, rules.retry]"
                          :type="'password'"
                          name="input-10-1"
                          label="Retry password"
                          hint="Needs to be the same"
                          outlined
                        />
                      </v-col>
                      <v-col cols="14">
                        <v-select
                          v-model="selectedRole"
                          :items="roles"
                          :prepend-inner-icon="'mdi-shield-account'"
                          label="Role"
                          outlined
                        />
                      </v-col>
                    </v-row>
                  </v-container>
                </v-form>
              </v-card>

              <v-row>
                <v-col cols="14" class="d-flex justify-end">
                  <v-btn
                    class="reg "
                    color="primary"
                    :disabled="
                      userName.length === 0 ||
                        password.length < 4 ||
                        passwordRetry.length < 4 ||
                        selectedRole.length === 0
                    "
                    @click="startStep = 2"
                  >
                    Continue
                  </v-btn>
                </v-col>
              </v-row>
            </v-stepper-content>

            <v-stepper-content step="2">
              <v-card class="mb-12 elevation-0" height="200px">
                <v-form>
                  <v-container class="form-centered">
                    <v-row>
                      <v-col cols="14">
                        <v-text-field
                          v-model="homeName"
                          :prepend-inner-icon="'mdi-home'"
                          class="inputField"
                          outlined
                          label="Home name"
                          :disabled="selectedHome.length !== 0"
                        />
                      </v-col>
                      <v-divider vertical class="ml-2 mr-4" />
                      <v-col cols="14">
                        <v-row>
                          <v-select
                            v-model="selectedHome"
                            :items="homes"
                            :prepend-inner-icon="'mdi-shield-account'"
                            label="Existing Homes"
                            :disabled="homeName.length > 1"
                            outlined
                          />
                          <v-tooltip bottom>
                            <template v-slot:activator="{ on, attrs }">
                              <v-btn
                                icon
                                color="red"
                                class="mt-2"
                                v-bind="attrs"
                                :disabled="selectedHome.length === 0"
                                v-on="on"
                                @click="onClearSelection"
                              >
                                <v-icon>mdi-close</v-icon>
                              </v-btn>
                            </template>
                            <span>Clear existing home selection</span>
                          </v-tooltip>
                        </v-row>
                      </v-col>
                    </v-row>
                    <v-row>
                      <v-col cols="14">
                        <p>
                          Modify default settings will be coming soon 🔥😉
                        </p>
                        <v-spacer />
                      </v-col>
                    </v-row>
                  </v-container>
                </v-form>
              </v-card>

              <v-row>
                <v-col cols="6" class="d-flex justify-start">
                  <v-btn text @click="startStep = 1">
                    Back
                  </v-btn>
                  <v-tooltip bottom>
                    <template v-slot:activator="{ on, attrs }">
                      <v-btn class="ml-2" text color="red" v-bind="attrs" @click="onClearForm" v-on="on">
                        Clear
                      </v-btn>
                    </template>
                    <span>Clear complete home selection</span>
                  </v-tooltip>
                </v-col>
                <v-col cols="6" class="d-flex justify-end">
                  <v-btn
                    class="reg"
                    color="primary"
                    :disabled="homeName.length === 0 && selectedHome.length === 0"
                    @click="startStep = 3"
                  >
                    Continue
                  </v-btn>
                </v-col>
              </v-row>
            </v-stepper-content>

            <v-stepper-content step="3">
              <v-card class="mb-12 elevation-0" height="200px">
                <v-container class="form-centered">
                  <v-row>
                    <v-col cols="14">
                      <v-text-field
                        v-model="userName"
                        :prepend-inner-icon="'mdi-account'"
                        class="inputField"
                        outlined
                        label="Username"
                        :disabled="doneEdit"
                      />
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
                        outlined
                        :disabled="doneEdit"
                        @click:append="showPwd = !showPwd"
                      />
                    </v-col>
                    <v-col cols="14">
                      <v-text-field
                        v-model="finalHome"
                        :prepend-inner-icon="'mdi-home'"
                        class="inputField"
                        outlined
                        label="Home name"
                        :disabled="doneEdit"
                      />
                    </v-col>
                  </v-row>
                </v-container>
              </v-card>

              <v-row>
                <v-col cols="6" class="d-flex justify-start">
                  <v-btn text @click="startStep = 2">
                    Back
                  </v-btn>
                  <v-tooltip bottom>
                    <template v-slot:activator="{ on, attrs }">
                      <v-btn class="ml-2" text color="red" v-bind="attrs" @click="onEditForm" v-on="on">
                        Edit
                      </v-btn>
                    </template>
                    <span>Edit</span>
                  </v-tooltip>
                </v-col>
                <v-col cols="6" class="d-flex justify-end">
                  <v-btn
                    class="reg"
                    color="primary"
                    :disabled="userName.length === 0 || password.length < 4 || finalHome.length === 0"
                    @click.prevent="onRegistrationClick"
                  >
                    Register
                  </v-btn>
                </v-col>
              </v-row>
            </v-stepper-content>
          </v-stepper-items>
        </v-stepper>

        <v-card-actions class="actions">
          <v-row>
            <v-col cols="14">
              <p class="login">
                Already registered?
                <router-link to="/login">
                  Click
                </router-link>
                here.
              </p>
              <v-spacer />
            </v-col>
          </v-row>
        </v-card-actions>
      </v-card>
    </v-container>
  </div>
</template>

<script lang="ts">
import Component from 'vue-class-component';
import Vue from 'vue';
import { InputMessage } from 'vuetify';

@Component
export default class Registration extends Vue {
  welcomeToSmartHub = 'Welcome to SmartHub';
  startStep = 1;
  showPwd = false;
  password = '';
  passwordRetry = '';
  userName = '';
  selectedRole = '';
  selectedHome = '';
  homeName = '';
  doneEdit = true;
  roles = ['Guest', 'User', 'Admin'];
  homes = ['Test', 'Test_1'];
  rules = {
    required: (value: InputEvent) => !!value || 'Required.',
    min: (v: InputMessage) => v.length >= 4 || 'Min 4 characters',
    retry: (v: InputMessage) => v !== '' || 'Not the same passwords' // TODO: add matching logic with pwd
  };

  get finalHome() {
    return this.homeName.length !== 0 ? this.homeName : this.selectedHome;
  }

  onRegistrationClick(): void {
    console.log('registration', this.homeName);
  }

  onClearSelection(): void {
    this.selectedHome = '';
  }

  onClearForm(): void {
    this.onClearSelection();
    this.homeName = '';
  }

  onEditForm(): void {
    this.doneEdit = !this.doneEdit;
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

  .login {
    margin: 0 0 0 24px;
  }
}
</style>
