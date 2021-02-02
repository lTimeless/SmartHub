import { HubConnectionBuilder, LogLevel, HubConnection } from '@microsoft/signalr';
import { reactive, ToRefs, toRefs, UnwrapRef } from 'vue';

interface SignalrState<T> {
  connectionEstablished: boolean;
  connection: HubConnection;
  data: T | null;
  error: null;
}

export function useSignalRHub<T>(hubRoute: string, listenToFunctionName: string): ToRefs {
  const state = reactive<SignalrState<T>>({
    connectionEstablished: false,
    connection: {} as HubConnection,
    data: null,
    error: null
  });

  state.connection = new HubConnectionBuilder()
    .withUrl(`/api/hub/${hubRoute}`)
    .configureLogging(LogLevel.Information)
    .build();

  state.connection.onclose(() => {
    state.connectionEstablished = false;
    state.data = null;
  });

  state.connection.on(listenToFunctionName, (data: UnwrapRef<T>) => {
    state.data = data;
  });

  state.connection
    .start()
    .then(() => {
      state.connectionEstablished = true;
    })
    .catch((err) => {
      state.error = err;
      console.error(err);
    });

  return {
    ...toRefs(state)
  };
}
