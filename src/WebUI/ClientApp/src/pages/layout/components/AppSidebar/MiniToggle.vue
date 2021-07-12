<template>
  <div
    class="h-12 md:flex hidden hover:bg-primaryBlueHover items-center cursor-pointer"
    :class="[onlyIcon ? 'w-12 justify-center' : ' w-52 justify-start pl-4']"
    @click="handleIconClick"
  >
    <AppIcon :icon-name="iconName" icon-color="text-primaryBlue" />
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
import AppIcon from '@/components/icons/AppIcon.vue';

export default defineComponent({
  name: 'AppUserDropdown',
  components: {
    AppIcon
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
    const iconName = ref('ChevronDoubleRight');

    const handleIconClick = () => {
      if (iconName.value === 'ChevronDoubleRight') {
        iconName.value = 'ChevronDoubleLeft';
        store.dispatch(AppActionTypes.SET_MINI_SIDEBAR, false);
      } else if (iconName.value === 'ChevronDoubleLeft') {
        iconName.value = 'ChevronDoubleRight';
        store.dispatch(AppActionTypes.SET_MINI_SIDEBAR, true);
      }
    };

    return {
      iconName,
      handleIconClick,
      ...toRefs(props)
    };
  }
});
</script>
