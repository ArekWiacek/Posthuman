import * as React from 'react';
import { useState } from 'react';
import { Box, TextField, IconButton } from '@mui/material';
import DesktopDatePicker from '@mui/lab/DesktopDatePicker';
import ArrowBackIosNewIcon from '@mui/icons-material/ArrowBackIosNew';
import ArrowForwardIosIcon from '@mui/icons-material/ArrowForwardIos';
import moment from 'moment';
import DefaultDateFormat from '../../Utilities/Defaults';

const DateSelector = ({ initialDate, onDateChanged }) => {
    const [selectedDate, setSelectedDate] = useState(initialDate ? moment(initialDate, DefaultDateFormat) : moment());
    const [selectedDateText, setSelectedDateText] = useState();

    const handleDateChange = newValue => {
        setSelectedDate(newValue);
        setSelectedDateText(newValue.format(DefaultDateFormat));

        if(onDateChanged)
            onDateChanged(newValue);
    };

    const handlePreviousDateClick = () => {
        let previousDate = selectedDate.clone().add(-1, 'days');
        handleDateChange(previousDate);
    };

    const handleNextDateClick = () => {
        let nextDate = selectedDate.clone().add(1, 'days');
        handleDateChange(nextDate);
    };

    return (
        <Box sx={{ display: 'flex', flexDirection: 'row', alignItems: 'center' }}>
            <IconButton onClick={handlePreviousDateClick}>
                <ArrowBackIosNewIcon />
            </IconButton>

            <DesktopDatePicker
                label="Selected date" name="selectedDate" inputFormat="DD.MM.YYYY" mask="__.__.____"
                value={selectedDate} onChange={handleDateChange} 
                renderInput={(params) => <TextField {...params} />} />

            <IconButton onClick={handleNextDateClick}>
                <ArrowForwardIosIcon />
            </IconButton>
        </Box>
    );
}

export default DateSelector;