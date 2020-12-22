<template>
  <div>
    <div class="absolute mx-auto w-full">
      <TopDoubleWaves />
    </div>
    <main class="bg-gray-200">
      <aside>
        <Sidebar />
      </aside>
      <div class="relative md:ml-64 min-h-screen z-20">
        <div class="md:pt-28 pt-12 px-4 md:px-10 mx-auto w-full z-10">
          <Navbar />
          <router-view v-slot="{ Component }">
            <transition name="route" mode="out-in">
              <component :is="Component" />
            </transition>
          </router-view>
        </div>
      </div>
    </main>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useStore } from 'vuex';
import Sidebar from '@/components/layout/Sidebar.vue';
import Navbar from '@/components/layout/Navbar.vue';
import TopDoubleWaves from '@/components/shared/svgs/TopDoubleWaves.vue';
import { AppActionTypes } from '@/store/app/actions';

export default defineComponent({
  name: 'Home',
  components: {
    Navbar,
    Sidebar,
    TopDoubleWaves
  },
  setup() {
    const store = useStore();
    store.dispatch(AppActionTypes.GET_APP);

    return {};
  }
});
</script>
