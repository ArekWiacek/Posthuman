import * as React from 'react';
import moment from 'moment';

const Deadline = (date) => {
    const noDeadlineDefaultText = '-';
    const defaultDateFormat = 'DD.MM.YYYY';

    const calculateDaysToDeadline = (deadlineDate) => {
        let now = moment().endOf('day');
        let deadline = moment(deadlineDate).endOf('day');
        let daysToDeadline = deadline.diff(now, 'days');
        return daysToDeadline;
    };

    const getDeadlineText = ({ when }) => {
        if(!when) 
            return noDeadlineDefaultText;

        let deadlineText = '';
        let daysToDeadline = calculateDaysToDeadline(when);

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
                deadlineText = moment(when).format(defaultDateFormat);
                break;
        }

        return deadlineText;
    };

    return (
        <span> {getDeadlineText(date)}</span>
    );
}

export default Deadline;