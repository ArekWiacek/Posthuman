import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import useAuth from '../../../Hooks/useAuth';

const PrivateRoute = ({component: Component, ...rest }) => {
    const { isLogged } = useAuth();

    return (
        <Route { ...rest } render={ props => {
            // Not logged in - redirect to login page with the return url
            if(!isLogged()) 
                return <Redirect to={{ pathname: '/login', state: { from: props.location }}} />
            else
                return <Component { ...props } />;
        }} />
    );
};

export default PrivateRoute;