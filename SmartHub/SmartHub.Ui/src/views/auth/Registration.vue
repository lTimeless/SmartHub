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
                          v-model="registrationRequest.username"
                          :prepend-inner-icon="'mdi-account'"
                          class="inputField"
                          outlined
                          label="Username"
                          required
                        />
                        <v-text-field
                          v-model="registrationRequest.password"
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
                          v-model="registrationRequest.role"
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
                      registrationRequest.username.length === 0 ||
                        registrationRequest.password.length < 4 ||
                        passwordRetry.length < 4 ||
                        registrationRequest.role.length === 0
                    "
                    @click="onNextStep"
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
                          v-model="homeCreateRequest.name"
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
                          <span>{{ existingHome.name }}</span>
                          <!-- TODO: hier eine checkbox -->
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
                        <p class="ml-0" :class="messageClass">{{ message }}</p>
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
                  <v-tooltip
                    bottom
                    :open-delay="toolTipOptions.tooltipOpenDelay"
                    :transition="toolTipOptions.tooltipTransition"
                  >
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
                    :disabled="homeCreateRequest.name.length === 0 && selectedHome.length === 0"
                    @click="onNextStep"
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
                        v-model="registrationRequest.username"
                        :prepend-inner-icon="'mdi-account'"
                        class="inputField"
                        outlined
                        label="Username"
                        :disabled="doneEdit"
                      />
                      <v-text-field
                        v-model="registrationRequest.password"
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
                    :disabled="
                      registrationRequest.username.length === 0 ||
                        registrationRequest.password.length < 4 ||
                        finalHome.length === 0
                    "
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
import { CREATE_HOME, GET_HOME } from '@/store/home/actions';
import { HomeCreateRequest, RegistrationRequest } from '@/types/types';
import { REGISTRATION } from '@/store/user/actions';
import Home from '@/views/Home.vue';
import router from '@/router';

@Component
export default class Registration extends Vue {
  welcomeToSmartHub = 'Welcome to SmartHub';
  startStep = 1;
  showPwd = false;
  passwordRetry = '';
  selectedHome = '';
  doneEdit = true;
  messageClass = 'successMessage';
  message = '';
  alreadyRegistered = false;
  alreadyHomeCreated = false;
  roles = ['Guest', 'User', 'Admin'];
  homeCreateRequest: HomeCreateRequest = {
    name: '',
    description: ''
  };

  registrationRequest: RegistrationRequest = {
    username: '',
    password: '',
    role: ''
  };

  toolTipOptions = {
    tooltipOpenDelay: 150,
    tooltipTransition: 'slide-y-transition'
  };

  rules = {
    required: (value: InputEvent) => !!value || 'Required.',
    min: (v: InputMessage) => v.length >= 4 || 'Min 4 characters',
    retry: (v: InputMessage) => v !== '' || 'Not the same passwords' // TODO: add matching logic with pwd
  };

  // eslint-disable-next-line class-methods-use-this
  mounted() {
    localStorage.removeItem('authResponse');
  }

  get finalHome() {
    return this.homeCreateRequest.name.length !== 0 ? this.homeCreateRequest.name : this.selectedHome;
  }

  get existingHome() {
    const home = this.$store.getters.getHome as Home | undefined | null;
    if (home === undefined || home === null) {
      this.message = 'No home created yet';
      this.messageClass = 'primary--text';
    }
    return this.$store.getters.getHome ?? ['-'];
  }

  get homeExistsSelect(): boolean {
    return this.homeCreateRequest.name.length > 1 || this.selectedHome === '';
  }

  async onNextStep(): Promise<void> {
    // TODO: erst einen step machen wenn die stores befüllt sind
    // demnach den step erhöhen und zwischen speichern um zu schauen was die nächste zahl ist
    // und wenn stores befüllt /nach dispatch/ dann überprüfen und step nun wirklich erhöhen
    this.startStep += 1;
    if (this.startStep === 2 && !this.alreadyRegistered) {
      await this.$store.dispatch(REGISTRATION, this.registrationRequest);
      await this.$store.dispatch(GET_HOME);
      this.alreadyRegistered = true;
    }
    if (this.startStep === 3 && !this.alreadyHomeCreated) {
      console.log('home');
      // TODO: Create Home
      await this.$store.dispatch(CREATE_HOME, this.homeCreateRequest);
      this.alreadyHomeCreated = true;
    }
  }

  onRegistrationClick(): void {
    console.log('registration', this.registrationRequest);
    router.push('/');
  }

  onClearSelection(): void {
    this.selectedHome = '-';
  }

  onClearForm(): void {
    this.onClearSelection();
    this.homeCreateRequest.name = '';
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
  .successMessage {
    color: black;
  }
}
</style>
