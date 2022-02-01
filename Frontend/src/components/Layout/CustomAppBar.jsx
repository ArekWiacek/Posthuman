
import * as React from "react";
import { Toolbar, Button, IconButton, Typography } from '@mui/material';
import MenuIcon from "@mui/icons-material/Menu";
import MuiAppBar from "@mui/material/AppBar";
import LoggedUser from '../Accounts/LoggedUser';

// import Toolbar from "@mui/material/Toolbar";
// import IconButton from "@mui/material/IconButton";
// import Typography from "@mui/material/Typography";

import { styled } from "@mui/material/styles";

const drawerWidth = 240;

const AppBar = styled(MuiAppBar, {
    shouldForwardProp: (prop) => prop !== "open"
})(({ theme, open }) => {
    return ({
        zIndex: theme.zIndex.drawer + 1,

        transition: theme.transitions.create(["width", "margin"], {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.leavingScreen
        }),

        ...(open && {
            marginLeft: drawerWidth,
            width: `calc(100% - ${drawerWidth}px)`,
            transition: theme.transitions.create(["width", "margin"], {
                easing: theme.transitions.easing.sharp,
                duration: theme.transitions.duration.enteringScreen
            })
        })
    });
});

const CustomAppBar = ({ title, open, onToggleDrawerClicked }) => {

    const handleToggleDrawerClicked = () => {
        onToggleDrawerClicked();
    };

    return (
        <AppBar position="absolute" open={open}>
            <Toolbar
                sx={{
                    pr: "24px" // keep right padding when drawer closed
                }}
            >
                <IconButton
                    edge="start"
                    color="inherit"
                    aria-label="open drawer"
                    onClick={handleToggleDrawerClicked}
                    sx={{
                        marginRight: "36px",
                        ...(open && { display: "none" })
                    }}
                >
                    <MenuIcon />
                </IconButton>

                <Typography
                    component="h1"
                    variant="h6"
                    color="inherit"
                    noWrap
                    sx={{ flexGrow: 1 }}
                >
                    {title}
                </Typography>

                <LoggedUser />
            </Toolbar>
        </AppBar>
    );
}

export default CustomAppBar;