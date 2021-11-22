import * as React from 'react';
import { useState, useEffect, createContext, useMemo } from 'react';
import { HubConnectionBuilder, LogLevel, HttpTransportType } from '@microsoft/signalr';
import LayoutWrapper from "./components/Layout/LayoutWrapper";
import { LogT, LogI, LogW, LogE } from './Utilities/Utilities';
import Api from './Utilities/ApiHelper';
import SignalApi from './Utilities/SignalApiHelper';
import * as ArrayHelper from './Utilities/ArrayHelper';

import './App.css';

export const AvatarContext = createContext({
    activeAvatar: {},
    setActiveAvatar: () => { }
});

export const ConnectionContext = createContext({
    connection: {},
    setConnection: () => {}
});

const App = () => {
    LogT("App.Constructor");

    const [activeAvatar, setActiveAvatar] = useState({
        id: 0,
        name: "[No active user]"
    });

    const activeAvatarValue = useMemo(
        () => ({ activeAvatar, setActiveAvatar }),
        [activeAvatar]
    );

    const [connection, setConnection] = useState(null);

    const value = useMemo(
        () => ( { connection, setConnection }),
        [connection]
    );

    useEffect(() => {
        LogT("App.useEffect");
        LogI("App.ApiCall.Avatars.GetActiveAvatar");

        Api.Get("Avatars/GetActiveAvatar", data => {
            if (data !== undefined && data.id !== undefined && data.id !== 0) {
                var avatar = data;
                LogW("Calling 'setActiveAvatar' with Avatar of ID: " + avatar.id);
                setActiveAvatar(avatar);
            }
        });
    }, []);


    useEffect(() => {
        LogI("App.useEffect 2");
        LogI("HubConnectionBuilder");

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
        LogI("App.useEffect 2");
        LogI("Connection changed");
        LogI(connection);

        if (connection) {
            connection.start()
                .then(result => {
                    LogI("Connected to hub!");

                    connection.on('ReceiveAvatarUpdate', avatarUpdate => {
                        LogI('AVATAR UPDATE!');
                        LogI(avatarUpdate);
                        const newAv = { ...avatarUpdate };
                        setActiveAvatar(newAv);
                    });
                })
                .catch(e => LogE('Connection failed: ', e));
        }
    }, [connection]);

    return (
        <AvatarContext.Provider value={activeAvatarValue}>
            <ConnectionContext.Provider value={value}>
                <div className="App">
                    <LayoutWrapper />
                </div>
            </ConnectionContext.Provider>
        </AvatarContext.Provider>
    );
}

export default App;
