import { Module } from 'vuex';
import { mutations } from "@/store/user/mutations";
import { RootState } from "@/store";
import { getters } from "@/store/user/getters";
import { actions } from "@/store/user/actions";
import { LoginResponse, RegistrationResponse, ServiceResponse, User} from "@/types/types";

export const state: UserState = {
    registrationResponse: {} as ServiceResponse<RegistrationResponse>,
    loginResponse: {} as ServiceResponse<LoginResponse>,
    user: {} as User
};

export const user: Module<UserState, RootState> = {
    state,
    getters,
    actions,
    mutations,
};

export interface UserState {
    user: User;
    loginResponse: ServiceResponse<LoginResponse>;
    registrationResponse: ServiceResponse<RegistrationResponse>;
}
