import { ConnectionTypes, PluginTypes } from '@/types/enums';
import { reactive, toRefs } from 'vue';

export function useEnumTypes() {
    const state = reactive({
        pluginTypesValues: [] as number[],
        pluginNames: [] as string[],
        pluginTypes: PluginTypes,
        connectionNames: [] as string[],
        connectionTypes: ConnectionTypes,
        connectionTypesValues: [] as number[]
    });
    state.pluginNames = Object.keys(PluginTypes).filter((e) => isNaN(+e));
    state.pluginTypesValues = Object.keys(PluginTypes).filter((e) => !isNaN(+e)).map(num => parseInt(num));
    state.connectionNames = Object.keys(ConnectionTypes).filter((e) => isNaN(+e));
    state.connectionTypesValues = Object.keys(ConnectionTypes).filter((e) => !isNaN(+e)).map(num => parseInt(num));

    return {
        ...toRefs(state)
    }
}
