import {GetterTree} from "vuex";
import {UserState} from "@/store/user/index";
import {RootState} from "@/store";
import {LoginResponse, ServiceResponse, User} from "@/types/types";

export const getters: GetterTree<UserState, RootState> = {
    getUser(state): User{
        return state.user;
    },
    getRole(state): string[] {
        return state.loginResponse.data.roles;
    },
    getLogin(state): LoginResponse {
        return state.loginResponse.data;
    },
    getLoginResponse(state): ServiceResponse<LoginResponse> {
        return state.loginResponse;
    }
}
