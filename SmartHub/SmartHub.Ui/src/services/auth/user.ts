import {LoginResponse} from "@/types/types";

type AuthType = {isAdmin: boolean; isUser: boolean; isGuest: boolean};

export const userAuth = ():AuthType  => {
    if (localStorage.getItem('loginResponse') == null){
        return {isAdmin: false, isUser: false, isGuest: false};
    }
    const loginResponse = JSON.parse(localStorage.getItem("loginResponse")!) as LoginResponse;
    if (loginResponse.roles.includes("Admin")) {
        return {isAdmin: true, isUser: false, isGuest: false};
    } else if(loginResponse.roles.includes("User")) {
        return {isAdmin: false, isUser: true, isGuest: false};
    } else if(loginResponse.roles.includes("Guest")) {
        return {isAdmin: false, isUser: false, isGuest: true};
    } else {
        return {isAdmin: false, isUser: false, isGuest: false};
    }
}
