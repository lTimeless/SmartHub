<template>
  <div
    class="h-12 md:flex hidden hover:bg-primaryBlueHover items-center cursor-pointer"
    :class="[onlyIcon ? 'w-12 justify-center' : ' w-52 justify-start pl-4']"
    @click="handleIconClick"
  >
    <ChevronDoubleRightIcon v-if="chevronRight" class="text-primaryBlue h-6 w-6" />
    <ChevronDoubleLeftIcon v-if="!chevronRight" class="text-primaryBlue h-6 w-6" />
    <div v-if="!onlyIcon">
      <div class="tracking-wide text-lg leading-loose text-primaryBlue" :class="[onlyIcon ? ' ' : ' pl-2']">
        {{ onlyIcon ? ' ' : 'Hide' }}
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { defineComponent, ref, toRefs } from 'vue';
import { useStore } from 'vuex';
import { AppActionTypes } from '@/store/app/actions';
import { ChevronDoubleRightIcon, ChevronDoubleLeftIcon } from '@heroicons/vue/outline';

export default defineComponent({
  name: 'MiniToggle',
  components: {
    ChevronDoubleRightIcon,
    ChevronDoubleLeftIcon
  },
  props: {
    onlyIcon: {
      type: Boolean,
      required: false,
      default: true
    }
  },
  setup(props) {
    const store = useStore();
    const chevronRight = ref(true);

    const handleIconClick = () => {
      if (chevronRight.value) {
        chevronRight.value = false;
        store.dispatch(AppActionTypes.SET_MINI_SIDEBAR, false);
      } else if (!chevronRight.value) {
        chevronRight.value = true;
        store.dispatch(AppActionTypes.SET_MINI_SIDEBAR, true);
      }
    };

    return {
      chevronRight,
      handleIconClick,
      ...toRefs(props)
    };
  }
});
</script>
