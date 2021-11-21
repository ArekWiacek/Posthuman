import React, { useState, useRef, useEffect } from 'react';
import { Paper, Typography, Divider } from '@mui/material';
import { HubConnectionBuilder, LogLevel, HttpTransportType } from '@microsoft/signalr';
import { LogE, LogI } from '../../Utilities/Utilities';
import NotificationsList from './NotificationsList';
import * as ArrayHelper from '../../Utilities/ArrayHelper';
import { Scrollbar } from "react-scrollbars-custom";
import SignalApi from '../../Utilities/SignalApiHelper';

const NotificationsPanel = () => {
    const [connection, setConnection] = useState(null);
    const [notifications, setNotifications] = useState([]);
    const latestNotifications = useRef(null);

    latestNotifications.current = notifications;

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

                    connection.on('ReceiveNotification', notification => {
                        const updatedNotifications = ArrayHelper.InsertObjectAtIndex(latestNotifications.current, notification, 0);
                        setNotifications(updatedNotifications);
                    });
                })
                .catch(e => LogE('Connection failed: ', e));
        }
    }, [connection]);

    return (
     
        <Paper>
            <Typography variant='h5' sx={{ textAlign: 'left', p: 1 }}>Notifications</Typography>
            <Divider />
            
            <Scrollbar id='notificationsScrollbar' style={{ width: '100%', height: 200 }}>
                <NotificationsList notifications={notifications} />
            </Scrollbar>
        </Paper>
    );
};

export default NotificationsPanel;