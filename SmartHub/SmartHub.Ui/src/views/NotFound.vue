<template>
  <v-col>
    <v-row align="center" justify="center">
      <v-img
        class="ma-0"
        src="../assets/images/undraw_page_not_found_su7k.svg"
        max-width="700"
        max-height="700"
      ></v-img>
    </v-row>
    <v-row align="center" justify="center">
      <p>
        Zurück zu
        <router-link to="/login">login</router-link>
        oder
        <router-link to="/">home</router-link>
      </p>
    </v-row>
  </v-col>
</template>

<script>
import Component from 'vue-class-component';
import Vue from 'vue';

@Component
export default class NotFound extends Vue {}
</script>

<style scoped></style>
