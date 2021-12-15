import * as React from 'react';
import { Switch, Route, Redirect } from "react-router-dom";
import Routes from './Routes';
import PrivateRoute from './PrivateRoute';

const CustomRouter = () => {
    const createRoute = (route) => {
        if(route.isPrivate)
            return <PrivateRoute path={route.path} component={() => route.destinationPage()} />
        else 
            return <Route key={route.path} path={route.path} render={() => route.destinationPage()} />
    };
    
    return (
        <div className="app">
            <Switch>
                <Route exact path="/">
                    <Redirect to="/todo" />
                </Route>

                {
                    Routes.map((route) => (
                        //createRoute(route);
                        <Route key={route.path} path={route.path} render={() => route.destinationPage()} />
                    ))
                }
            </Switch>
        </div>
    );
};

export default CustomRouter;
