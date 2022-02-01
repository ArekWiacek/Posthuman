import React, { createContext, useEffect, useContext, useMemo, useState } from 'react';
import { useHistory, useLocation } from 'react-router-dom';
import useLocalStorage from './useLocalStorage';
import Api from '../Utilities/ApiHelper';

const initialAuthContext = {};

const AuthContext = createContext(initialAuthContext);

export const AuthProvider = ({ children }) => {
    const userStorageKey = 'user';
    const [user, setUser] = useState();
    const [error, setError] = useState();
    const [loading, setLoading] = useState(false);
    const [loadingInitial, setLoadingInitial] = useState(true);
    const [userStorage, setUserStorage] = useLocalStorage(userStorageKey, {});
    const history = useHistory();
    const location = useLocation();
    
    // reset error after changing page
    useEffect(() => {
        if(error) setError(null);
    }, [location.pathname]);

    // get user from storage at first execution
    useEffect(() => {
        let userInfo = getAuthenticatedUserInfo();
        if(userInfo)
            setUser(userInfo);
        setLoadingInitial(false);
    }, []);

    const getAuthenticatedUserInfo = () => {
        return userStorage;
    };

    const login = (email, password) => {
        setLoading(true);

        Api.Post('Authentication/Login', { email, password }, loggedUser => {
            setUser(loggedUser);
            setUserStorage(loggedUser);
            setLoading(false);
            history.push('/todo');
        }, error => {
            setError(error);
            setLoading(false);
        });
    };
    
    const register = (userToRegister, successCallback) => {
        setLoading(true);

        Api.Post('Authentication/Register', userToRegister, registeredUser => {
            setUser(registeredUser);
            setUserStorage(registeredUser);
            setLoading(false);
            history.push('/todo');
        }, error => {
            setError(error);
            setLoading(false);
        });
    };

    const logout = () => {
        setUserStorage({});
        setUser(null);
        history.push('/login');
    };

    const isLogged = () => {
        return (user != null && user != undefined && user.token != undefined && user.token != null && user.token != '');
    };

    const memoedValue = useMemo(
        () => ({
            user, 
            loading,
            error,
            login, 
            register, 
            logout,
            isLogged
        }),
        [user, loading, error]
    );

    return (
        <AuthContext.Provider value={memoedValue}>
            {!loadingInitial && children}
        </AuthContext.Provider>
    );
};

const useAuth = () => {
    return useContext(AuthContext);
};

export default useAuth;