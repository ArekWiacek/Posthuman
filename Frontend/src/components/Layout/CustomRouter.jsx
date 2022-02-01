import * as React from 'react';
import { Switch, Route, Redirect } from "react-router-dom";
import Routes from './Routes';
import PrivateRoute from './PrivateRoute';

const createRoute = (route) => {
    if(route.isPrivate)
        return <PrivateRoute key={route.path} path={route.path} component={() => route.destinationPage()} />
    else 
        return <Route key={route.path} path={route.path} render={() => route.destinationPage()} />
};

const CustomRouter = () => {
    return (
        <div className="app">
            <Switch>
                <Route exact path="/">
                    <Redirect to="/login" />
                </Route>
                
                { Routes.map(route => createRoute(route)) }
            </Switch>
        </div>
    );
};

export default CustomRouter;
