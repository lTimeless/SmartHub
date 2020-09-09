<template>
  <div>
    {{ test }}
  </div>
</template>

<script>
import { defineComponent, ref } from 'vue';
import { useRoute } from 'vue-router';

export default defineComponent({
  name: 'MyUser',
  components: {},
  setup() {
    const route = useRoute();
    const test = ref('MyUser');
    return {
      test
    };
  }
});
</script>

<style scoped></style>
