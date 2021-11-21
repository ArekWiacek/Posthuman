import React from 'react';
import { Typography, Box, Divider } from '@mui/material';
import moment from 'moment';

const NotificationItem = ({ notification }) => {
    const { title, subtitle, text, secondText, occured, reward } = notification;

    const defaultDateFormat = 'DD.MM.YYYY HH:mm';

    const getOccuredFormatted = (when) => {
        let timeText = moment(when).format(defaultDateFormat);
        return timeText;
    }

    return (
        <Box sx={{ mb: 1, p: '4px', textAlign: 'left' }}>
            <Typography variant="caption" sx={{ textAlign: 'left' }} component="div">
                {getOccuredFormatted(occured)}
            </Typography>
            <Box sx={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between' }}>
                <Typography variant="h5" component="div">
                    {title}
                </Typography>
                <Typography variant="h5" sx={{ mb: 1, color: 'success.main' }}>
                    {reward}
                </Typography>
            </Box>
            <Typography variant="h6" sx={{ textAlign: 'left' }} >
                    {subtitle}
                </Typography>
            <Typography variant="body2" sx={{ textAlign: 'left' }}>
                {text}
            </Typography>
            <Divider />
            {/* <Typography variant="body2" sx={{  }}>
                {secondText ? secondText : ''}
            </Typography> */}
        </Box>
    );
};

export default NotificationItem;