import * as React from "react";
import ListItem from "@mui/material/ListItem";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import { useHistory } from "react-router-dom";
import Routes from './Routes';
import useAuth from '../../../Hooks/useAuth';

const NavigationList = () => {
    const history = useHistory();
    const { isLogged } = useAuth();
    
    // Handling following cases:
    //  Route is private - show only when user logged
    //  Route is public - show for everyone
    //  Extra case: route can be public but hidden when user logged - so we can hide eg. logging route 
    const shouldDisplayMenuItem = route => {
        if(route.isPrivate) {
            return isLogged();
        }

        if(route.hideWhenAuthenticated && isLogged())
            return false;
            
        return true;
    };

    const createMenuItem = (route) => {
        if (shouldDisplayMenuItem(route))
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
