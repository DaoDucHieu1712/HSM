import logo from './logo.svg';
import './App.css';
import { EventType } from "@azure/msal-browser";
import { MsalProvider, useMsal,AuthenticatedTemplate, UnauthenticatedTemplate, } from "@azure/msal-react";
import { PageLayout } from "./ui-components/PageLayout";
import { Routes, Route, useNavigate } from "react-router-dom";
import Grid from "@mui/material/Grid";
import { Home } from "./pages/Home";
import { useState, useEffect } from "react";
import { loginRequest } from './AuthConfig';

// const WrappedView = () =>{
//   const {instance} = useMsal();
//   const activeAccount = instance.getActiveAccount();
//   const handleRedirect = () => {
//     instance 
//     .loginRedirect({
//       ...loginRequest,
//       prompt: 'create'
//     })
//   }


//   return (
//     <div className='App'>
//        <AuthenticatedTemplate>
//         {activeAccount ? ( <p>LOGIN_SUCCESS</p> ): null}
//           </AuthenticatedTemplate>

//           <UnauthenticatedTemplate>
            
//               <center>Please sign-in to see your profile information.</center>
//               <button onClick={handleRedirect}> sign up </button>
           
//           </UnauthenticatedTemplate>
//     </div>
//     // <MsalProvider instance={instance}>
//     //     <PageLayout>
//     //       <Grid container justifyContent="center">
//     //         {/* <Pages /> */}
//     //       </Grid>
//     //     </PageLayout>
//     //   </MsalProvider>

//   );
// }
const App = ({instance}) => {
  return (
    <MsalProvider instance={instance}>
            <PageLayout>
            <Grid container justifyContent="center">
            {/* <Pages /> */}
           </Grid>
        </PageLayout>
    </MsalProvider>
  )
};
//const App = ({instance}) => {
//   return (
//     <MsalProvider instance={instance}>
//       <WrappedView></WrappedView>
//     </MsalProvider>
//   )
// };


// function Pages() {
//   const { instance } = useMsal();
//   const [status, setStatus] = useState(null);


//   return (
//       <Routes>
//           {/* <Route path="/profile" element={<Profile />} />
//           <Route path="/logout" element={<Logout />} /> */}
//           <Route path="/" element={<Home status={status} />} />
//       </Routes>
//   );
// }

export default App;
{/* <MsalProvider instance={pca}>
        <PageLayout>
          <Grid container justifyContent="center">
            <Pages />
          </Grid>
        </PageLayout>
      </MsalProvider> */}