<template>
  <div>
    {{ test }}
  </div>
</template>

<script>
import { defineComponent, ref } from 'vue';

export default defineComponent({
  name: 'MyUser',
  components: {},
  setup() {
    const test = ref('MyUser');
    return {
      test
    };
  }
});
</script>

<style scoped></style>
