<template>
  <div class="home">
    <router-link to="/">Home</router-link> |
    <router-link to="/about">About</router-link> |
    <router-link to="/settings">Settings</router-link>
    <router-view></router-view>
  </div>
</template>

<script>
export default {
  name: "Home",
  components: {}
};
</script>
