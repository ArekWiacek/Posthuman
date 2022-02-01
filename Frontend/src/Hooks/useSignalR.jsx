import React, { createContext, useEffect, useContext, useMemo, useState } from 'react';
import { HubConnectionBuilder, LogLevel, HttpTransportType } from '@microsoft/signalr';
import { LogI, LogE } from '../Utilities/Utilities';
import SignalApi from '../Utilities/SignalApiHelper';
import useAvatar from './useAvatar';

const initialSignalRContext = {};
const SignalRContext = createContext(initialSignalRContext);

export const SignalRProvider = ({ children }) => {
    const { avatar, setAvatar } = useAvatar();
    const [connection, setConnection] = useState(null);
    const value = useMemo(
        () => ({ connection, setConnection }),
        [connection]
    );

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .configureLogging(LogLevel.Debug)
            .withUrl(SignalApi.GetUrl(), {
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets
            })
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);
    }, []);

    useEffect(() => {
        if (connection) {
            connection.start()
                .then(result => {
                    LogI("Connected to hub!");

                    connection.on('ReceiveAvatarUpdate', avatarUpdate => {
                        LogI('AVATAR UPDATE!');
                        LogI(avatarUpdate);
                        const newAv = { ...avatarUpdate };
                        if (setAvatar != undefined)
                            setAvatar(newAv);
                    });
                })
                .catch(e => LogE('Connection failed: ', e));
        }
    }, [connection]);

    return (
        <SignalRContext.Provider value={value}>
            {children}
        </SignalRContext.Provider>
    );
};

const useSignalR = () => {
    return useContext(SignalRContext);
};

export default useSignalR;