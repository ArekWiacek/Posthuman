import axios from 'axsios';
import { accountService } from '../Services';


const jwtInterceptor = () => {
    axios.interceptors.request.use(request => {
        // Add auth header with jwt if account is logged in and request is to the api url
        const account = accountService.accountValue;
        const isLoggedIn = account?.token;
        const isApiUrl = request.url.startsWith(process.env.REACT_APP_API_URL);
        
        if(isLoggedIn && isApiUrl) {
            request.headers.common.Authorization = `Bearer ${account.token}`;
        }

        return request;
    });
};

export default jwtInterceptor;