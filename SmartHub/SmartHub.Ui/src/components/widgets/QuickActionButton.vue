<template>
  <v-btn> {{ title }}</v-btn>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { Prop } from "vue-property-decorator";

@Component
export default class QuickActionButton extends Vue {
  @Prop({ type: String, required: true })
  private title!: string;
}
</script>

<style scoped></style>
