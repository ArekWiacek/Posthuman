import React, { useState, useRef, useEffect } from 'react';
import { HubConnectionBuilder, LogLevel, HttpTransportType } from '@microsoft/signalr';
import { LogE, LogI } from '../../Utilities/Utilities';
import NotificationsWindow from './NotificationsWindow';

const Notifications = () => {
    const [connection, setConnection] = useState(null);
    const [notifications, setNotifications] = useState([]);
    const latestNotifications = useRef(null);

    latestNotifications.current = notifications;

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .configureLogging(LogLevel.Debug)
            .withUrl('https://localhost:7201/notifications', {
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets
            })
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);
    }, []);

    useEffect(() => {
        if(connection) {
            connection.start()
            .then(result => {
                LogI("Connected to hub!");

                connection.on('ReceiveNotification', notification => {
                    const updatedNotifications = [...latestNotifications.current];
                    updatedNotifications.push(notification);
                    setNotifications(updatedNotifications);
                });
            })
            .catch(e => LogE('Connection failed: ', e));
        }
    }, [connection]);

    return (
        <React.Fragment>
            <NotificationsWindow notifications={notifications} />
        </React.Fragment>
    );
}; 

export default Notifications;