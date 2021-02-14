import { ref, Ref } from 'vue';

type DateTimeState = {
  currentTime: Ref<string>;
  timeOfDay: Ref<string>;
};

export const useDateTime = (): DateTimeState => {
  const timeOfDay = ref('');
  const currentTime = ref('00:00');
  const now = new Date();
  const hour = now.getHours();

  const time = () => {
    const d = new Date();
    const m = d.getMinutes();
    const h = d.getHours();
    currentTime.value = `${h < 10 ? `0${h}` : h}:${m < 10 ? `0${m}` : m}`;
  };
  setInterval(time, 1000);

  const morning = hour >= 4 && hour <= 11,
    afternoon = hour >= 12 && hour <= 16,
    evening = hour >= 17 && hour <= 21,
    night = hour >= 22 || hour <= 3;

  if (morning) {
    timeOfDay.value = 'morning';
  } else if (afternoon) {
    timeOfDay.value = 'afternoon';
  } else if (evening) {
    timeOfDay.value = 'evening';
  } else if (night) {
    timeOfDay.value = 'night';
  }
  return {
    currentTime,
    timeOfDay
  };
};
