<template>
  <div class="flex flex-wrap bg-white rounded">
    <LineChart></LineChart>
    <BarChart></BarChart>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import BarChart from '@/components/charts/BarChart.vue';
import LineChart from '@/components/charts/LineChart.vue';

export default defineComponent({
  name: 'Statistics',
  components: {
    BarChart,
    LineChart
  }
});
</script>
