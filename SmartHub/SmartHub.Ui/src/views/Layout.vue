<template>
  <!-- TODO add here firstTime Login welcome modal -->
  <!--      <ConfirmationModalAsync-->
  <!--        v-if="doneInit"-->
  <!--        title="SmartHub initialization success"-->
  <!--        button-title="Go to Registration"-->
  <!--        :callback="modalCallback"-->
  <!--      >-->
  <!--        <div class="text-gray-600 mb-8">-->
  <!--          Thank you for using SmartHub.-->
  <!--          <br />If you encounter any problems or have any suggestions, please visit-->
  <!--          <a class="text-primary" :href="githubUrl">github</a>-->
  <!--          and create an issue. 🔥👌🚀❤-->
  <!--        </div>-->
  <!--      </ConfirmationModalAsync>-->
  <div>
    <div class="absolute mx-auto w-full">
      <TopDoubleWaves />
    </div>
    <main class="bg-gray-200">
      <aside>
        <Sidebar />
      </aside>
      <div class="relative md:ml-64 min-h-screen">
        <div class="container mx-auto w-full h-full py-3">
          <Navbar />
          <router-view v-slot="{ Component }">
            <transition
              mode="out-in"
              enter-active-class="transition duration-300 ease-out-in"
              leave-active-class="transition duration-300 ease-out-in"
              enter-from-class="opacity-0 transform translate-y-3"
              enter-to-class="opacity-100 translate-y-0"
              leave-from-class="opacity-100 translate-y-0"
              leave-to-class="opacity-0 translate-y-3"
            >
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
import Sidebar from '@/components/ui/layout/AppSidebar/Sidebar.vue';
import Navbar from '@/components/ui/layout/Navbar.vue';
import TopDoubleWaves from '@/components/ui/svgs/TopDoubleWaves.vue';
import { AppActionTypes } from '@/store/app/actions';

export default defineComponent({
  name: 'Layout',
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
<style lang="scss" scoped></style>
