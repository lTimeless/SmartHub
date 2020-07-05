<template>
  <div>
    <v-container>
      <v-img
        class="mb-8"
        src="../assets/images/undraw_page_not_found_su7k.svg"
      ></v-img>

      <p>
        Zurück zu
        <router-link to="/login">login</router-link>
        oder
        <router-link to="/">home</router-link>
      </p>
    </v-container>
  </div>
</template>

<script>
export default {
  name: "NotFound"
};
</script>

<style scoped></style>
