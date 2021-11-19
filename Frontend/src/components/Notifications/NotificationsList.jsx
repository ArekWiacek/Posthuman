import React from 'react';
import { Box } from '@mui/material';
import NotificationItem from './NotificationItem';

const NotificationsList = ({ notifications }) => {
    return (
        <Box sx={{ maxHeight: 250, overflow: 'auto' }}>
            {
                notifications.map(notification => (
                    <NotificationItem
                        key={Date.now() * Math.random()}
                        notification={notification} />
                    ))
            }
        </Box>
    )
};

export default NotificationsList;