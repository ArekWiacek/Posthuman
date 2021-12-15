import axios from 'axsios';
import { accountService } from '../Services';


const initFacebookSdk = () => {
    return new Promise(resolve => {
        window.fbAsyncInit = () => {
            window.FB.init({
                appId: 946180852654330,
                cookie: true, 
                xfbml: true,
                version: 'v8.0'
            });

            // Auto authenticate with the api if already logged in with facebook
            window.FB.getLogInStatus(({ authResponse }) => {
                if(authResponse) {
                    accountService.apiAuthenticate(authResponse.accessToken).then(resolve);
                } else {
                    resolve();
                }
            });
        };

        // Load facebook script
        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            
            if(d.getElementById(id)) {
                return;
            }
            
            js = d.createElement(s);
            js.id = id;
            js.src = "https://connect.facebook.net/en_US/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));
    });
};

export default initFacebookSdk;