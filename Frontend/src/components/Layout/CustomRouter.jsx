import * as React from 'react';
import { Switch, Route, Redirect } from "react-router-dom";
import Routes from './Routes';

const CustomRouter = () => {
  return (
    <div className="app">
      <Switch>
        <Route exact path="/">
          <Redirect to="/blog" />
        </Route>

        {
          Routes.map((route) => (
            <Route key={route.path} path={route.path} render={() => route.destinationPage()} />
          ))
        }
      </Switch>
    </div>
  );
};

export default CustomRouter;
