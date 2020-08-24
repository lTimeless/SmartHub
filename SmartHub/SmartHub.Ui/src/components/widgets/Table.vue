<template>
  <div class="tableContainer">
    <table class="border-collapse table-auto w-full whitespace-no-wrap bg-white table-striped relative">
      <thead>
        <tr class="text-left">
          <th
            v-for="heading in headers"
            :key="heading.value"
            class="sticky top-0 border-b py-2"
            v-text="heading.text"
            :class="{ [heading.value]: true }"
          ></th>
        </tr>
      </thead>
      <tbody>
        <slot></slot>
      </tbody>
    </table>
  </div>
</template>

<script>
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'Table',
  props: {
    headers: {
      type: Array,
      required: true
    }
  },
  setup(props) {
    return {
      ...props
    };
  }
});
</script>

<style scoped></style>
