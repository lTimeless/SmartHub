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
        <v-stepper v-model="getStepperIndex" class="elevation-0">
          <v-stepper-header class="elevation-0">
            <v-stepper-step :complete="getStepperIndex > 1" step="1">
              User
            </v-stepper-step>
            <v-divider />
            <v-stepper-step :complete="getStepperIndex > 2" step="2">
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
                          :disabled="getHome !== null"
                        />
                      </v-col>
                      <v-divider vertical class="ml-2 mr-4" />
                      <v-col cols="14">
                        <v-row>
                          <v-text-field
                            v-model="existingHome"
                            :prepend-inner-icon="'mdi-home'"
                            class="inputField"
                            outlined
                            label="Home name"
                            disabled
                          />
                        </v-row>
                      </v-col>
                    </v-row>
                    <v-row>
                      <v-col cols="14">
                        <template v-if="getHome === null">
                          <p>
                            Modifying default settings will be coming soon 🔥😉
                          </p>
                        </template>
                        <template v-else>
                          <p class="ml-0" :class="messageClass">{{ message }}.</p>
                          <p class="ml-0" :class="messageClass">Please <b>click</b> continue.</p>
                        </template>
                        <v-spacer />
                      </v-col>
                    </v-row>
                  </v-container>
                </v-form>
              </v-card>

              <v-row>
                <v-col cols="6" class="d-flex justify-start">
                  <v-btn text @click="onBackStep(1)">
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
                    :disabled="homeCreateRequest.name.length === 0 && getHome === null"
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
                  <v-btn text @click="onBackStep(2)">
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
import { CREATE_HOME, FETCH_HOME, UPDATE_HOME } from '@/store/home/actions';
import {
  AuthResponse,
  Home,
  HomeCreateRequest,
  HomeUpdateRequest,
  RegistrationRequest,
  ServerResponse
} from '@/types/types';
import { REGISTRATION } from '@/store/auth/actions';
import router from '@/router';
import { Getter } from 'vuex-class';
import { AUTH_USER, UPDATE_REG_STEP } from '@/store/auth/mutations';
import { AxiosResponse } from 'axios';
import { clearStorage, storeAuthResponse, storeToken } from '@/services/auth/tokenService';

@Component
export default class Registration extends Vue {
  @Getter('getRegStepIndex') getStepperIndex!: number;
  @Getter('getHome') getHome!: Home | null;
  welcomeToSmartHub = 'Welcome to SmartHub';
  showPwd = false;
  passwordRetry = '';
  doneEdit = true;
  messageClass = 'successMessage';
  message = '';
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
    this.$store.commit(UPDATE_REG_STEP, 1);
    clearStorage();
  }

  get finalHome() {
    return this.getHome === null ? this.homeCreateRequest.name : this.getHome.name;
  }

  get existingHome() {
    if (this.getHome === null) {
      this.message = 'No home created yet';
      this.messageClass = 'secondary--text';
      return '-';
    }
    this.message = 'Already a home created - this will be selected automatically';
    this.messageClass = 'primary--text';
    return this.getHome.name;
  }

  onBackStep(nextStep: number) {
    this.$store.commit(UPDATE_REG_STEP, nextStep);
  }

  async onNextStep(): Promise<void> {
    let shouldBeNextStep = this.getStepperIndex;
    shouldBeNextStep += 1;

    if (shouldBeNextStep === 2) {
      await this.$store.dispatch(FETCH_HOME);
    }
    await this.$store.commit(UPDATE_REG_STEP, shouldBeNextStep);
  }

  onRegistrationClick = async () => {
    await this.$store
      .dispatch(REGISTRATION, this.registrationRequest)
      .then(async (response: AxiosResponse<ServerResponse<AuthResponse>>) => {
        if (response.data.success) {
          const auth = response.data.data as AuthResponse;
          storeToken(auth.token);
          storeAuthResponse(auth);
          await this.$store.commit(AUTH_USER, auth);
          if (this.getHome === null) {
            await this.$store.dispatch(CREATE_HOME, this.homeCreateRequest);
          } else {
            const updateHomeRequest: HomeUpdateRequest = {
              name: null,
              description: null,
              settingName: null,
              userName: auth.userName
            };
            await this.$store.dispatch(UPDATE_HOME, updateHomeRequest);
          }
          await router.push('/');
        }
      })
      .catch((error) => {
        console.log(error);
      });
  };

  onClearForm(): void {
    this.homeCreateRequest = { name: '', description: '' };
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
