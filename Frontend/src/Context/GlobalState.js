import React, { createContext, useReducer } from 'react';
import AppReducer from './AppReducer';

const initialState = {
    currentAvatar: {}
}

export const GlobalContext = createContext(initialState);

export const GlobalProvider = ({children}) => {
    const [state, dispatch] = useReducer(AppReducer, initialState);

    function setCurrentAvatar(newAvatar) {
        dispatch({
            type: 'SET_AVATAR',
            payload: newAvatar
        });
    }

    return (
        <GlobalContext.Provider value = {
            {currentAvatar : state.currentAvatar, setCurrentAvatar}}
        >
            {children}
        </GlobalContext.Provider>
    )
}