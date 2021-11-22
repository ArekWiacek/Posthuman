import * as React from 'react';
import { useRef } from 'react';
import { Typography } from '@mui/material';
import moment from 'moment';

const Deadline = ({ when }) => {
    const noDeadlineDefaultText = '-';
    const defaultDateFormat = 'DD.MM.YYYY';
    const textColor = useRef('');

    const calculateDaysToDeadline = (deadlineDate) => {
        let now = moment().endOf('day');
        let deadline = moment(deadlineDate).endOf('day');
        let daysToDeadline = deadline.diff(now, 'days');
        return daysToDeadline;
    };

    const getDeadlineText = (when) => {
        if(!when) 
            return noDeadlineDefaultText;

        let deadlineText = '';
        let daysToDeadline = calculateDaysToDeadline(when);

        if(daysToDeadline < 0) 
            textColor.current = 'error.main';
        else if(daysToDeadline == 0)
            textColor.current = 'warning.main';
            
        switch (daysToDeadline) {
            case -1:
                deadlineText = 'Yesterday';
                break;

            case 0:
                deadlineText = 'Today';
                break;

            case 1:
                deadlineText = 'Tomorrow';
                break;

            default:
                let dateText = moment(when).format(defaultDateFormat);

                if(daysToDeadline < -1)
                    deadlineText = dateText + ' (' + Math.abs(daysToDeadline) + ' days ago)';  
                else
                    deadlineText = dateText; 

                break;
        }

        return deadlineText;
    };

    return (
        <Typography color={textColor.current}>
            {getDeadlineText(when)}
        </Typography>
    );
}

export default Deadline;