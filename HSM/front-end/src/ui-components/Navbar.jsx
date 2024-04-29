import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import Link from "@mui/material/Link";
import Typography from "@mui/material/Typography";
import SignInSignOutButton from "./SignInSignOutButton";
import { Link as RouterLink } from "react-router-dom";

const Navbar = () => {
    return (
        <div sx={{ flexGrow: 1}}>
            <AppBar position="static">
            <Toolbar>
                <Typography sx={{ flexGrow: 1 }}>
                    <Link component={RouterLink} to="/" color="inherit" variant="h6">Microsoft identity platform</Link>
                </Typography>
              
                <SignInSignOutButton />
            </Toolbar>
            </AppBar>
        </div>
    );
};

export default Navbar;