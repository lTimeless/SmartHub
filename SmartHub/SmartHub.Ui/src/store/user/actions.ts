import {ActionTree} from "vuex";
import {UserState} from "@/store/user/index";
import {RootState} from "@/store";
import {LoginRequest, LoginResponse, ServiceResponse} from "@/types/types";
import Vue from "vue";
import {LOGIN_USER} from "@/store/user/mutations";


// ActionType keys
export const LOGIN = "LOGIN";

export const actions: ActionTree<UserState, RootState> = {
    async [LOGIN]({ commit, state }, payload: LoginRequest) {
        await Vue.axios.post<ServiceResponse<LoginResponse>>("api/Identity/login", payload)
            .then((response) => {
                localStorage.setItem("loginResponse", JSON.stringify(response.data.data));
                commit(LOGIN_USER, response.data);
            })
            .catch(err => {
                console.log(err);
            });

        // async [FETCH_OWN_PROFILE](state) {
        //     state.commit(LOADING_INCREMENT);
        //     const email = getEmailFromLocalStorage();
        //     Vue.axios
        //         .get(`api/Profiles/${email}?pictureSize=150`)
        //         .then(profile => {
        //             state.commit(SET_OWN_PROFILE_M, profile.data);
        //             const projectFavorites = profile.data.projectFavorites;
        //             if (projectFavorites) {
        //                 state.commit(
        //                     SET_PROJECTS_M,
        //                     projectFavorites
        //                         .filter((p: { isLiked: boolean }) => p.isLiked)
        //                         .map((projectLink: { project: Project }) => projectLink.project)
        //                 );
        //             }
        //         })
        //         .finally(() => {
        //             state.commit(LOADING_DECREMENT);
        //         });
        // }
    }
}
