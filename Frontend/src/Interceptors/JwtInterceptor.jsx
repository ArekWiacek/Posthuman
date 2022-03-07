import axios from 'axios';

const getJwtToken = () => {
    let token = null;
    let authData = localStorage.getItem('user');
    if (authData != null) {
        let authDataJson = JSON.parse(authData);
        if(authDataJson.token != null && authDataJson.token != '')
            token = authDataJson.token;
    }
    return token;
};

// This middleware is executed before making any request to API
// It adds authentication jwt token to every request we make (ofc when there is user logged in)
const jwtInterceptor = () => {
    axios.interceptors.request.use(request => {
        let token = getJwtToken();
        const isLoggedIn = token != null; 
        const isApiUrl = request.url.startsWith('https://localhost') || request.url.startsWith('https://posthuman.pl');

        if (isLoggedIn && isApiUrl) {
            request.headers.common.Authorization = `Bearer ${token}`;
        }

        return request;
    });
};

export default jwtInterceptor;