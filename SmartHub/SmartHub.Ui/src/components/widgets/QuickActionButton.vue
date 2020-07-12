<template>
  <v-card :width="width" :height="height" @click="onClick" outlined>
    <v-card-title class="justify-center pa-2">
      {{ title }}
    </v-card-title>
  </v-card>
</template>

<script lang="ts">
import Vue from 'vue';
import Component from 'vue-class-component';
import { Prop } from 'vue-property-decorator';

@Component
export default class QuickActionButton extends Vue {
  @Prop({ type: String, required: true })
  private title!: string;

  @Prop({ type: String, required: false, default: '270' })
  private width?: string;

  @Prop({ type: String, required: false, default: '50' })
  private height?: string;

  onClick() {
    console.log('action click', this.title);
  }
}
</script>

<style lang="scss" scoped></style>
