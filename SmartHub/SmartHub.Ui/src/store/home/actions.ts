import { ActionContext, ActionTree } from 'vuex';
import { RootState, HomeState, AuthState } from '@/store/index.types';
import { DeviceCreateRequest, Group, GroupCreateRequest, GroupUpdateRequest, HomeCreateRequest, HomeUpdateRequest } from '@/types/types';
import { HomeMutations, HomeMutationTypes } from '@/store/home/mutations';
import { getHome, postHome, putHome } from '@/services/apis/home.service';
import { getByIdGroup, postGroup, putByIdGroup } from '@/services/apis/group.service';
import { postDevice } from '@/services/apis/device.service';

// Keys
export enum HomeActionTypes {
  CREATE_HOME = 'CREATE_HOME',
  UPDATE_HOME = 'UPDATE_HOME',
  // Group
  CREATE_GROUP = 'CREATE_GROUP',
  FETCH_BY_GROUP_ID = 'FETCH_BY_GROUP_ID',
  UPDATE_GROUP = 'UPDATE_GROUP',
  // Device
  CREATE_DEVICE = 'CREATE_DEVICE',
  UPDATE_DEVICE = 'UPDATE_DEVICE'
}

// actions context type
type ActionAugments = Omit<ActionContext<AuthState, RootState>, 'commit'> & {
  commit<K extends keyof HomeMutations>(key: K, payload: Parameters<HomeMutations[K]>[1]): ReturnType<HomeMutations[K]>;
};

// Action Interface
export type HomeActions = {
  [HomeActionTypes.CREATE_HOME]({ commit }: ActionAugments, payload: HomeCreateRequest): Promise<void>;
  [HomeActionTypes.UPDATE_HOME]({ commit }: ActionAugments, payload: HomeUpdateRequest): Promise<void>;
  // Group
  [HomeActionTypes.CREATE_GROUP]({}: ActionAugments, payload: GroupCreateRequest): Promise<void>;
  [HomeActionTypes.FETCH_BY_GROUP_ID]({}: ActionAugments, payload: string): Promise<Group>;
  [HomeActionTypes.UPDATE_GROUP]({}: ActionAugments, payload: GroupUpdateRequest): Promise<void>;
  // DEvice
  [HomeActionTypes.CREATE_DEVICE]({}: ActionAugments, payload: DeviceCreateRequest): Promise<void>;
};

export const actions: ActionTree<HomeState, RootState> = {
  async [HomeActionTypes.CREATE_HOME]({ commit }, payload): Promise<void> {
    await postHome(payload)
      .then((response) => {
        if (!response.success) {
          return Promise.reject(response.message);
        }
        commit(HomeActionTypes.UPDATE_HOME, response.data);
        return Promise.resolve(response.data);
      })
      .catch((error) => Promise.reject(error));
  },
  async [HomeActionTypes.UPDATE_HOME]({ commit }, payload): Promise<void> {
    await putHome(payload)
      .then((res) => {
        commit(HomeActionTypes.UPDATE_HOME, res.data);
      })
      .catch((err) => {
        console.log(err);
      });
  },
  // Group
  async [HomeActionTypes.CREATE_GROUP]({ dispatch }, payload): Promise<void> {
    await postGroup(payload)
      .then((response) => {
        if (!response.success) {
          return Promise.reject(response.message);
        }
        return Promise.resolve();
      })
      .catch((error) => Promise.reject(error));
  },
  async [HomeActionTypes.FETCH_BY_GROUP_ID]({}, payload): Promise<Group> {
    return await getByIdGroup(payload)
      .then((response) => {
        if (!response.success) {
          return Promise.reject(response.message);
        }
        return Promise.resolve(response.data as Group);
      })
      .catch((error) => Promise.reject(error));
  },
  async [HomeActionTypes.UPDATE_GROUP]({ dispatch }: ActionAugments, payload: GroupUpdateRequest): Promise<void> {
    await putByIdGroup(payload)
      .then((response) => {
        if (!response.success) {
          return Promise.reject(response.message);
        }
        return Promise.resolve();
      })
      .catch((error) => Promise.reject(error));
  },
  // Device
  async [HomeActionTypes.CREATE_DEVICE]({ dispatch }, payload): Promise<void> {
    await postDevice(payload)
      .then((response) => {
        if (!response.success) {
          return Promise.reject(response.message);
        }
        return Promise.resolve();
      })
      .catch((error) => Promise.reject(error));
  }
};
