import * as React from "react";
import ListItem from "@mui/material/ListItem";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import { useHistory } from "react-router-dom";
import Routes from './Routes';
import useAuth from '../../Hooks/useAuth';

const NavigationList = () => {
    const history = useHistory();
    const { isLogged } = useAuth();

    const createMenuItem = (route) => {
        if (!route.isPrivate || route.isPrivate && isLogged())
             return (
                <ListItem key={route.path} button onClick={() => history.push(route.path)}>
                    <ListItemIcon>
                        {route.sidebarIcon()}
                    </ListItemIcon>
                    <ListItemText primary={route.sidebarTitle} />
                </ListItem>   
            )
        else
            return '';
    };

    return (
        <div>
            {
                Routes.map((route) => (
                    createMenuItem(route)
                ))
            }
        </div>
    )
};

export default NavigationList;
