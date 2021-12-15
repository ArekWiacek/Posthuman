import React from 'react';

import { Route, Redirect } from 'react-router-dom';
import { accountService } from '../../Services/AccountService';

const PrivateRoute = ({component: Component, ...rest }) => {
    return (
        <Route { ...rest } render={ props => {
            const account = accountService.accountValue;
            
            if(!account) {
                // Not logged in - redirect to login page with the return url
                return <Redirect to={{ pathname: '/login', state: { from: props.location }}} />
            }

            return <Component { ...props } />;
        }} />
    );
};

export default PrivateRoute;