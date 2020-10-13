import { HubConnectionBuilder, LogLevel, HubConnection } from '@microsoft/signalr';
import { reactive, toRefs } from 'vue';
import { useStore } from 'vuex';

export function useSignalRHub(hubRoute: string, listenToFunctionName: string) {
    const state = reactive({
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

    state.connection
        .start()
        .then(() => {
            state.connectionEstablished = true;
            state.connection.on(listenToFunctionName, (data: any) => {
                // if hubRoute === 'home' dann store.dispatch(HomeUpdate, data)
                console.log('sig', data);
                
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
