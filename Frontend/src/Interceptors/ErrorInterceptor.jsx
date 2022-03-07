import axios from 'axios';
import { accountService } from '../Services/accountService';

function handleRequestErrors(error) {
    if (error.response) {
      // The request was made and the server responded with a status code
      // that falls out of the range of 2xx
      console.log(error.response.data);
      console.log(error.response.status);
      console.log(error.response.headers);
    } else if (error.request) {
      // The request was made but no response was received
      // `error.request` is an instance of XMLHttpRequest in the browser and an instance of
      // http.ClientRequest in node.js
      console.log(error.request);
    } else {
      // Something happened in setting up the request that triggered an Error
      console.log('Error', error.message);
    }
    console.log(error.config);
  }

const errorInterceptor = () => {
    //axios.interceptors.response.use(null, (error) => handleRequestErrors(error));
    // {
    //     const { response } = error;

    //     // if(!response) {
    //     //     console.error(error);
    //     //     return;
    //     // }

    //     // if([401, 403].includes(response.status) && accountService.accountValue) {
    //     //     // Auto logout if 401 or 403 response returned from api
    //     //     //accountService.logout();
    //     // }

    //     //const errorMessage = response.data?.message || response.statusText;
    //     //console.error('ERROR: ', errorMessage);
    // });
};

export default errorInterceptor;