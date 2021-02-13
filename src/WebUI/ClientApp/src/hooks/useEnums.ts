import { ConnectionTypes, PluginTypes } from '@/types/enums';
import { reactive, ToRefs, toRefs } from 'vue';

type State = {
  pluginTypesValues: number[];
  pluginNames: string[];
  pluginTypes: PluginTypes;
  connectionNames: string[];
  connectionTypes: ConnectionTypes;
  connectionTypesValues: number[];
};

export const useEnumTypes = (): ToRefs<State> => {
  const state = reactive<State>({
    pluginTypesValues: [] as number[],
    pluginNames: [] as string[],
    pluginTypes: PluginTypes.None,
    connectionNames: [] as string[],
    connectionTypes: ConnectionTypes.None,
    connectionTypesValues: [] as number[]
  });
  state.pluginNames = Object.keys(PluginTypes).filter((e) => isNaN(+e));
  state.pluginTypesValues = Object.keys(PluginTypes)
    .filter((e) => !isNaN(+e))
    .map((num) => parseInt(num));
  state.connectionNames = Object.keys(ConnectionTypes).filter((e) => isNaN(+e));
  state.connectionTypesValues = Object.keys(ConnectionTypes)
    .filter((e) => !isNaN(+e))
    .map((num) => parseInt(num));

  return {
    ...toRefs(state)
  };
};
