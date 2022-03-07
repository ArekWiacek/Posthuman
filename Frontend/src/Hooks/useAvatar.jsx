import React, { createContext, ReactNode, useEffect, useContext, useMemo, useState } from 'react';
import { LogI, LogE } from '../Utilities/Utilities';
import Api from '../Utilities/ApiHelper';
import useAuth from './useAuth';

const initialAvatarContext = {
    name: 'xxx'
};

const defaultUser = { 
    name: 'Major Suchodolski', 
    level: 3, 
    bio: 'Psznie jem', 
    exp: 345, 
    expToNewLevel: 400, 
    cybertribeName: 'Szczury wodne' 
};

const AvatarContext = createContext(initialAvatarContext);

export const AvatarProvider = ({ children }) => {
    const { user, isLogged } = useAuth();
    const [avatar, setAvatar] = useState(defaultUser);
    const [loadingInitial, setLoadingInitial] = useState(true);

    useEffect(() => {
        if (isLogged()) {
            Api.Get("Avatars/GetAvatarForLoggedUser", data => {
                if (data !== undefined && data.id !== undefined && data.id !== 0) {
                    setAvatar(data);
                }
                setLoadingInitial(false);
            });
        } else {
            setLoadingInitial(false);
        }

    }, [user]);

    const memoedValue = useMemo(
        () => ({
            user,
            avatar,
            setAvatar
        }),
        [user, avatar, loadingInitial]
    );

    return (
        <AvatarContext.Provider value={memoedValue}>
            {!loadingInitial && children}
        </AvatarContext.Provider>
    );
};

const useAvatar = () => {
    return useContext(AvatarContext);
};

export default useAvatar;