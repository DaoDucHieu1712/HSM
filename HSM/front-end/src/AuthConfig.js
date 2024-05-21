import {logLevel} from "@azure/msal-browser"
import { PublicClientApplication } from "@azure/msal-browser";
export const msalConfig ={
    auth:{
        clientId : '23ccaa38-0de4-46d6-9172-d9a3950456a8',
        authority: 'https://login.microsoftonline.com/6edea0d9-ef08-4280-b76d-00150fb4e5f4/',
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
    scopes : ['api://23ccaa38-0de4-46d6-9172-d9a3950456a8/userCheck'],
}
const msalInstance = new PublicClientApplication(msalConfig);
await msalInstance.initialize();