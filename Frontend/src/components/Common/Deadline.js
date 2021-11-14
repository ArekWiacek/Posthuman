import * as React from 'react';
import { useRef } from 'react';
import { Typography } from '@mui/material';
import moment from 'moment';

const Deadline = ({ when }) => {
    const noDeadlineDefaultText = '-';
    const defaultDateFormat = 'DD.MM.YYYY';
    const isOverdue = useRef(false);

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
            isOverdue.current = true;

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
                    deadlineText = dateText; // + ' (' + daysToDeadline + ' days left)';

                break;
        }

        return deadlineText;
    };

    return (
        <Typography color={isOverdue.current ? "error" : ""}>
            {getDeadlineText(when)}
        </Typography>
    );
}

export default Deadline;