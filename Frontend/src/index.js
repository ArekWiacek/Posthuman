import React from 'react';
import { StrictMode } from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import jwtInterceptor from './Interceptors/JwtInterceptor';
// import errorInterceptor from './Interceptors/errorInterceptor';

// TODO: enable in future and handle errors in elegant way
// errorInterceptor();
jwtInterceptor();

const rootElement = document.getElementById("root");

ReactDOM.render(
    <StrictMode>
        <BrowserRouter>
            <App />
        </BrowserRouter>
    </StrictMode>,
    rootElement
);
