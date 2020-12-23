<template>
  <div id="app">
    <router-view></router-view>
<!--      <transition name="route" mode="out-in">-->
<!--        <component :is="Component" />-->
<!--      </transition>-->

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
/* Router view transitions */
.route-enter-from,
.route-leave-to {
  opacity: 0;
  transform: translateY(-30px);
}

.route-enter-active,
.route-leave-active {
  transition: all 0.2s ease;
}

.route-enter-to,
.route-leave-from {
  opacity: 1;
  transform: translateY(0);
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

.sidebar {
  overflow: auto;
  @apply fixed bg-background px-4 inset-x-0 bottom-0 w-full border-r border-border transition-all z-40;
  transform: translateX(-100%);

  &.open {
    transform: translateX(0);
  }

  @screen md {
    @apply w-1/4 px-0 bg-transparent top-0 bottom-auto inset-x-auto sticky z-0;
    transform: translateX(0);
  }
}
</style>
