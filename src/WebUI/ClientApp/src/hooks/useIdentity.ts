import {AppActionTypes} from '@/store/app/actions';
import {Roles, Routes} from '@/types/enums';
import router from '@/router';
import {store} from '@/store';
import {ref} from 'vue';
import {Maybe} from '@/graphql/graphql.types';
import {client} from '@/graphql-client';
import {
    RefreshTokensDocument,
    RefreshTokensMutation,
    RefreshTokensMutationVariables
} from '@/graphql/mutations/identity/refreshTokens.generated';
import {LogoutDocument, LogoutQuery, LogoutQueryVariables} from '@/graphql/mutations/identity/logout.generated';

const userId = ref<Maybe<string>>(undefined);
const userName = ref<Maybe<string>>(undefined);
const userRoles = ref<Maybe<string[]>>(undefined);
const authenticated = ref<boolean>(false);
const firstLogin = ref<boolean>(false);
const autoLoginAlreadyDone = ref<boolean>(false);

export const useIdentity = () => {
    const isAuthenticated = () => authenticated.value !== false;
    const setIdentity = (
        roles: Maybe<string[]>,
        id: Maybe<string>,
        isAuthenticated: Maybe<boolean>,
        uName: Maybe<string>,
        fLogin: Maybe<boolean>
    ) => {
        userRoles.value = roles ?? userRoles.value;
        userId.value = id ?? userId.value;
        authenticated.value = isAuthenticated ?? authenticated.value;
        userName.value = uName;
        firstLogin.value = fLogin ?? false;
    };

    const clearIdentity = () => {
        userRoles.value = undefined;
        authenticated.value = false;
    userId.value = undefined;
    firstLogin.value = false;
        userName.value = undefined;
        autoLoginAlreadyDone.value = false;
  };

  const logout = async () => {
      client
          .query<LogoutQuery, LogoutQueryVariables>(LogoutDocument)
          .toPromise()
          .then(async ({data}) => {
              if (data && data.logout) {
                  clearIdentity();
                  await store.dispatch(AppActionTypes.SET_USER_DROPDOWN, false);
                  await store.dispatch(AppActionTypes.RESET_SIDEBAR);
                  await router.push(Routes.Login);
              } else {
                  throw new Error(`${data?.logout.message}`);
              }
          })
          .catch((error) => console.log(error));
  };

    const refreshTokens = async () => {
        return client
            .mutation<RefreshTokensMutation, RefreshTokensMutationVariables>(RefreshTokensDocument)
            .toPromise()
            .then(({data}) => {
                if (data && data.refreshTokens.errors) {
                    setIdentity(null, null, data.refreshTokens.isAuthenticated, null, null);
                }
                if (data && data.refreshTokens.isAuthenticated) {
                    const {id, userName, roles, isFirstLogin} = data.refreshTokens.user || {};
                    setIdentity(roles, id, data.refreshTokens.isAuthenticated, userName, isFirstLogin);
                }
                autoLoginAlreadyDone.value = true;
                return data;
            });
    };

  const isRole = () => {
    if (!userRoles.value) {
      return Roles.None;
    } else if (userRoles.value.includes('Admin')) {
      return Roles.Admin;
    } else if (userRoles.value.includes('User')) {
      return Roles.User;
    } else if (userRoles.value.includes('Guest')) {
      return Roles.Guest;
    } else {
      return Roles.None;
    }
  };

  return {
      roles: userRoles,
      authenticated,
      userId,
      firstLogin,
      userName,
      autoLoginAlreadyDone,
      setIdentity,
      refreshTokens,
      clearIdentity,
      isAuthenticated,
      isRole,
      logout
  };
};
