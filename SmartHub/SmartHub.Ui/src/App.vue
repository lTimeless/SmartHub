<template>
  <div id="app">
    <router-view></router-view>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'App'
});
</script>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
}

/*Scrollbar*/
/* width */
::-webkit-scrollbar {
  width: 12px;
}
/* Track */
::-webkit-scrollbar-track {
  border-radius: 100vh;
  @apply bg-gray-100 bg-opacity-30 border rounded shadow-lg;
  backdrop-filter: blur(10px);
  //background: var(--color-sidebar);
}
/* Handle */
::-webkit-scrollbar-thumb {
  //background: var(--color-border);
  @apply bg-gray-100 bg-opacity-30 border rounded shadow-lg;
  backdrop-filter: blur(10px);
  //border-radius: 100vh;
  //border: 3px solid #edf2f7;
}
/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  @apply bg-gray-100 bg-opacity-30 border rounded shadow-lg;
  backdrop-filter: blur(10px);
  //background: #c1cacb;
}

* {
  transition-property: color, background-color, border-color;
  transition-duration: 200ms;
  transition-timing-function: ease-in-out;
}

table {
  @apply text-left mb-6;

  th {
    @apply py-3 px-4 bg-gray-400 border-white uppercase text-sm text-gray-600 font-bold tracking-wider;
  }

  td {
    @apply px-4 py-2 border-white border-b text-sm text-gray-600 font-semibold tracking-wider;
  }

  tr {
    @apply border-b border-border;
    &:nth-child(even) {
      @apply bg-gray-200;
    }
    &:nth-child(odd) {
      @apply bg-white;
    }
  }
}
</style>
