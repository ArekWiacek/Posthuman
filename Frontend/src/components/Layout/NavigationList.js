import * as React from "react";
import ListItem from "@mui/material/ListItem";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import { useHistory } from "react-router-dom";
import Routes from './Routes';

const NavigationList = () => {
  const history = useHistory();
  
  return (
    <div>
        {
          Routes.map((route) => (
            <ListItem key={route.path} button onClick={() => history.push(route.path)}>
              <ListItemIcon>
                {route.sidebarIcon()}
              </ListItemIcon>
              <ListItemText primary={route.sidebarTitle} />
            </ListItem>
          ))
        }
    </div>
  )
};

export default NavigationList;
