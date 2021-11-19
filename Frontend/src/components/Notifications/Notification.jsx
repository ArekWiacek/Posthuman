import React from 'react';
import { Typography, Box } from '@mui/material';

const Notification = ({ notification }) => {
    const { title, subtitle, text, secondText } = notification;

    return (
        <Box sx={{ mb: 3 }}>
            <Typography variant="h6" component="div">
                {title}
            </Typography>
            <Typography sx={{ mb: 1 }} color="text.secondary">
                {subtitle}
            </Typography>
            <Typography variant="body2">
                {text}
            </Typography>
            <Typography variant="body2" sx={{ color: 'success.main' }}>
                {secondText ? secondText : ''}
            </Typography>
        </Box>
    );
};

export default Notification;