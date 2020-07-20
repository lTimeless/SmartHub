<template>
  <div class="login">
    <v-container class="fully-centered">
      <v-card class="card" max-width="500px">
        <v-toolbar class="d-flex justify-center mb-6" flat>
          <v-toolbar-title>
            <h2>
              {{ welcomeToSmartHub }}
            </h2>
          </v-toolbar-title>
        </v-toolbar>
        <v-img class="mb-8" src="../../assets/images/undraw_smart_home_28oy.svg"></v-img>
        <v-form>
          <v-container class="form-centered">
            <v-row class="d-flex justify-center">
              <v-col cols="12" md="10">
                <v-text-field
                  :prepend-inner-icon="'mdi-account'"
                  v-model="username"
                  class="inputField"
                  outlined
                  label="Username"
                  required
                >
                </v-text-field>
              </v-col>
              <v-col cols="12" sm="10">
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
                  @keyup.enter="onLoginClick"
                ></v-text-field>
              </v-col>
            </v-row>
          </v-container>
        </v-form>
        <v-card-actions class="actions">
          <v-row class="pa-0">
            <v-col cols="12" class="d-flex justify-center">
              <p :class="messageClass">{{ message }}</p>
              <v-btn
                class="log"
                color="primary"
                @click.prevent="onLoginClick"
                :disabled="username.length === 0 || password.length < 4"
                >Login</v-btn
              >
            </v-col>
            <v-col cols="12" class="d-flex justify-center">
              <p>
                You can register
                <router-link to="/registration">here</router-link>.
              </p>
            </v-col>
          </v-row>
        </v-card-actions>
      </v-card>
    </v-container>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import Component from 'vue-class-component';
import { InputMessage } from 'vuetify';
import router from '@/router';
import { LoginRequest } from '@/types/types';
import { LOGIN } from '@/store/auth/actions';
// TODO: vuex in here
@Component
export default class Login extends Vue {
  welcomeToSmartHub = 'Welcome to SmartHub';
  showPwd = false;
  password = '';
  username = '';
  message = '';
  messageClass = 'successMessage';

  rules = {
    required: (value: InputEvent) => !!value || 'Required.',
    min: (v: InputMessage) => v.length >= 4 || 'Min 4 characters'
  };

  private async onLoginClick(): Promise<void> {
    const login: LoginRequest = {
      username: this.username,
      password: this.password
    };
    await this.$store.dispatch(LOGIN, login);
    const { userName, token } = this.$store.getters.getAuthResponse;
    this.message = '';
    if (userName === null || token == null) {
      this.messageClass = 'error--text';
      this.message = 'Error: Something went wrong, try again later';
    }
    await router.push('/');
  }
}
</script>

<style lang="scss" scoped>
.login {
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
  }
  .form-centered {
    align-self: center;
    padding: 0 2em;
  }
  .inputField {
    width: 100%;
  }
  .log {
    width: 50%;
  }
  .successMessage {
    color: black;
  }
  p {
    margin: 0;
    a {
      text-decoration: none;
      color: #3f51b5;
    }
  }
}
</style>
