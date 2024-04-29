import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { PublicClientApplication,EventType } from '@azure/msal-browser';
import { msalConfig } from './AuthConfig';
import { BrowserRouter as Router } from "react-router-dom";
const msalInstance = new PublicClientApplication(msalConfig)

msalInstance.initialize().then(() => {
  // Default to using the first account if no account is active on page load
  if (!msalInstance.getActiveAccount() && msalInstance.getAllAccounts().length > 0) {
    // Account selection logic is app dependent. Adjust as needed for different use cases.
    msalInstance.setActiveAccount(msalInstance.getAllAccounts()[0]);
  }
});

msalInstance.addEventCallback((event) => {
  if(event.eventType === EventType.LOGIN_SUCCESS && event.payload.account ){
    const account = event.payload.account;
    msalInstance.setActiveAccount(account);
  }
});

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <Router>
    <App instance={msalInstance}/>
    </Router>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
