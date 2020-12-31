<template>
  <div class="flex flex-row h-screen bg-gray-100">
    <AppSidebar />
    <div class="flex flex-row flex-auto bg-gray-200 min-h-screen">
      <div class="flex flex-col w-full p-4 container mx-auto w-full h-full">
        <router-view v-slot="{ Component }">
          <transition
            mode="out-in"
            enter-active-class="transition duration-200 ease-out-in"
            leave-active-class="transition duration-200 ease-out-in"
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
  </div>
  <!--  <div>-->
  <!--    <div class="absolute mx-auto w-full">-->
  <!--      <TopDoubleWaves />-->
  <!--    </div>-->
  <!--    <main class="bg-gray-200">-->
  <!--      <aside>-->
  <!--        <Sidebar />-->
  <!--      </aside>-->
  <!--      <div class="relative md:ml-64 min-h-screen">-->
  <!--        <div class="container mx-auto w-full h-full py-3">-->
  <!--          <Navbar />-->
  <!--          <router-view v-slot="{ Component }">-->
  <!--            <transition-->
  <!--              mode="out-in"-->
  <!--              enter-active-class="transition duration-200 ease-out-in"-->
  <!--              leave-active-class="transition duration-200 ease-out-in"-->
  <!--              enter-from-class="opacity-0 transform translate-y-3"-->
  <!--              enter-to-class="opacity-100 translate-y-0"-->
  <!--              leave-from-class="opacity-100 translate-y-0"-->
  <!--              leave-to-class="opacity-0 translate-y-3"-->
  <!--            >-->
  <!--              <component :is="Component" />-->
  <!--            </transition>-->
  <!--          </router-view>-->
  <!--        </div>-->
  <!--      </div>-->
  <!--    </main>-->
  <!--  </div>-->
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import Navbar from '@/components/ui/layout/Navbar.vue';
import TopDoubleWaves from '@/components/ui/svgs/TopDoubleWaves.vue';
import AppSidebar from '@/components/ui/layout/AppSidebar/AppSidebar.vue';

export default defineComponent({
  name: 'Layout',
  components: {
    AppSidebar
    // Navbar,
    // TopDoubleWaves
  },
  setup() {
    return {};
  }
});
</script>
<style lang="scss" scoped></style>
