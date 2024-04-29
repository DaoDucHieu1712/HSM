import {logLevel} from "@azure/msal-browser"

export const msalConfig ={
    auth:{
        clientId : '23ccaa38-0de4-46d6-9172-d9a3950456a8',
        authority : 'https://login.microsoftonline.com/thuantd2001hbgmail.onmicrosoft.com',
        redirectUrl: '/',
        postLogoutRedirectUrl: '/',
        navigateToLoginRequestUrl: false
    },
    cache:{
        cacheLocation: 'sessionStorage',
        storeAuthStateInCookie: false,
    }
   
}
export const loginRequest = {
    scopes : ['user.read'],
}