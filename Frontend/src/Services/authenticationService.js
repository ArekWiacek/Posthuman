import Api from '../Utilities/ApiHelper';
import { useLocalStorage } from '../Hooks/useLocalStorage';

const userInfoStorageKey = 'userInfo';

export const authenticationService = {
    isAuthenticated,
    login,
    logout,
    register,
    currentUser: {},
    getLoggedUserInfo,
    getLoggedUserToken,
    getLoggedUserId
};

function getLoggedUserId() {
    Api.Get('Authentication/CurrentUser', userId => {
        console.log('Current user id: ' + userId);

        return userId;
    }, error => {
        console.log('Error getting current user');
    })
}

// MAIN
function register(userToRegister, successCallback, errorCallback) {
    Api.Post('Authentication/Register', userToRegister, registeredUser => {
        saveLoggedUserInfo(registeredUser);

        if(successCallback)
            successCallback(registeredUser);

    }, errorCallback);
}

function login(email, password, successCallback, errorCallback) {
    Api.Post('Authentication/Login', { email, password }, loggedUser => {
        saveLoggedUserInfo(loggedUser)
        if(successCallback)
            successCallback(loggedUser);
    }, error => {
        if(errorCallback)
            errorCallback(error);
    });
}

function logout() {
    removeLoggedUserInfo();
}

function isAuthenticated() {
    let token = getLoggedUserToken();
    return token != null && token != undefined && token != '';
}

function getLoggedUserToken() {
    let token = null;
    var userInfo = getLoggedUserInfo();
    if(userInfo != null && userInfo.token != null && userInfo.token != '') 
        token = userInfo.token;
    return token;
}

// HELPERS
function saveLoggedUserInfo(userInfo) {
    localStorage.setItem(userInfoStorageKey, JSON.stringify(userInfo));
}

function removeLoggedUserInfo() {
    localStorage.removeItem(userInfoStorageKey);
}

function getLoggedUserInfo() {
    var userInfo = JSON.parse(localStorage.getItem(userInfoStorageKey));
    return userInfo;
}

