import { reactive, ToRefs, toRefs } from 'vue';
import { checkHome, checkUsers } from '@/services/apis/init';

type State = {
  data: boolean | undefined;
  error: null | unknown;
  loading: boolean;
};

//TODO: here trigger notification popup for Errors and/or messages

export function useCheckHome(): ToRefs<State> {
  const state = reactive<State>({
    data: false,
    error: null,
    loading: false
  });

  state.loading = true;
  checkHome()
    .then((res) => {
      if (res.success) {
        state.data = res.data;
        state.loading = false;
      } else {
        state.loading = false;
        state.error = res.message;
      }
    })
    .catch((err) => Promise.reject(err));

  return toRefs(state);
}

export function useCheckUsers(): ToRefs<State> {
  const state = reactive<State>({
    data: false,
    error: null,
    loading: false
  });

  state.loading = true;
  checkUsers()
    .then((res) => {
      if (res.success) {
        state.data = res.data;
        state.loading = false;
      } else {
        state.loading = false;
        state.error = res.message;
      }
    })
    .catch((err) => Promise.reject(err));

  return toRefs(state);
}
