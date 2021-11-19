import React from 'react';
import { Typography, Box } from '@mui/material';

const NotificationItem = ({ notification }) => {
    const { title, subtitle, text, secondText } = notification;

    return (
        <Box sx={{ mb: 3, p: '4px' }}>
            <Box sx={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between' }}>
                <Typography variant="h6" component="div">
                    {title}
                </Typography>
                <Typography variant="h5" sx={{ mb: 1, color: 'success.main' }}>
                    {subtitle}
                </Typography>
            </Box>
            <Typography variant="body2" sx={{ textAlign: 'left' }}>
                {text}
            </Typography>
            {/* <Typography variant="body2" sx={{  }}>
                {secondText ? secondText : ''}
            </Typography> */}
        </Box>
    );
};

export default NotificationItem;