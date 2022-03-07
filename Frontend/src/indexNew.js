import React from 'react';
import { StrictMode } from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';

import App from './App';
import { jwtInterceptor, errorInterceptor } from './Interceptors';
import { initFacebookSdk } from './Utilities';

jwtInterceptor();
errorInterceptor();

initFacebookSdk().then(startApp);

const rootElement = document.getElementById("root");

function startApp() {
    ReactDOM.render(
        <StrictMode>
            <BrowserRouter>
                <App />
            </BrowserRouter>
        </StrictMode>,
        rootElement
    );
}