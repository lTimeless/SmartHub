import { HubConnectionBuilder, LogLevel, HubConnection } from '@microsoft/signalr';
import { reactive, toRefs } from 'vue';

export function useSignalRHub<T>(hubRoute: string, listenToFunctionName: string) {
    const state = reactive({
        connectionEstablished: false,
        connection: {} as HubConnection,
        data: {} as T,
        error: null
    });

    state.connection = new HubConnectionBuilder()
        .withUrl(`/api/hub/${hubRoute}`)
        .configureLogging(LogLevel.Information)
        .build();

    state.connection.onclose(() => {
        state.connectionEstablished = false;
        state.data =  Object.assign({});
    });

    state.connection
        .start()
        .then(() => {
            state.connectionEstablished = true;
            state.connection.on(listenToFunctionName, (data: any) => {
                state.data = data;
            });
        })
        .catch((err) => {
            state.error = err;
            return console.error(err);
        });

    return {
        ...toRefs(state)
    }
}
