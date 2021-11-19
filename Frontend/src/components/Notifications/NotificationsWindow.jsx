import React from 'react';
import { Paper, Typography, Box } from '@mui/material'
import Notification from './Notification';

const NotificationsWindow = ({ notifications }) => {
    const notificationsList = notifications
        .map(notification => <Notification 
            key={Date.now() * Math.random()}
            notification={notification} />);

    return (
        <React.Fragment>
            <Paper>
                <Typography variant='h5'>Notifications</Typography>
                <Box sx={{ maxHeight: 250, overflow: 'hidden' }}>
                    {notificationsList}
                </Box>
            </Paper>
        </React.Fragment>
    )
}; 

export default NotificationsWindow;