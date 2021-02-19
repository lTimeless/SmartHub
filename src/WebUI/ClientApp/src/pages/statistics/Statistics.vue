<template>
  <div class="flex flex-wrap bg-white rounded">
    <LineChart />
    <BarChart />
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import BarChart from '@/pages/statistics/components/BarChart.vue';
import LineChart from '@/pages/statistics/components/LineChart.vue';

export default defineComponent({
  name: 'Statistics',
  components: {
    BarChart,
    LineChart
  }
});
</script>
