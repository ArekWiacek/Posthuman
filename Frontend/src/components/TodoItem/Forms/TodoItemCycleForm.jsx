import * as React from 'react';
import { useState } from 'react';
import { Box, TextField, MenuItem, Typography } from '@mui/material';
import DesktopDatePicker from '@mui/lab/DesktopDatePicker';
import moment from 'moment';

const TodoItemCycleForm = ({ onCycleDefinitionChanged }) => {    
    const initialValues = {
        repetitionPeriod: 2, 
        startDate: new Date(),
        endDate: new Date()
    };

    const repetitionPeriods = [{
        id: 1,
        label: 'Daily'
    }, {
        id: 2,
        label: 'Weekly'
    }, {
        id: 3,
        label: 'Monthly'
    }];

    const [state, setState] = useState(initialValues);

    const handleRepetitionPeriodChange = e => {
        const { name, value } = e.target;
        setState({ ...state, [name]: value });
        onCycleDefinitionChanged({ ...state, [name]: value });
    };

    const handleStartDateChange = newValue => {
        setState({ ...state, ['startDate']: newValue ? newValue.toDate() : null });
        onCycleDefinitionChanged({ ...state, ['startDate']: newValue ? newValue.toDate() : null });
    };

    const handleEndDateChange = newValue => {
        setState({ ...state, ['endDate']: newValue ? newValue.toDate() : null });
        onCycleDefinitionChanged({ ...state, ['endDate']: newValue ? newValue.toDate() : null });
    };

    return (
        <Box sx={{
            display: 'flex', flexDirection: 'column', alignItems: 'center',
            '& .MuiTextField-root': { m: 1, width: '100%' } }}
            noValidate autoComplete="off">
            <TextField
                label="Repeat" name="repetitionPeriod" select
                value={state.repetitionPeriod} onChange={handleRepetitionPeriodChange}>
                {repetitionPeriods.map((repetitionPeriod) => (
                    <MenuItem key={repetitionPeriod.id} value={repetitionPeriod.id}>
                        {repetitionPeriod.label}
                    </MenuItem>
                ))}
            </TextField>

            <DesktopDatePicker
                label="Start" name="startDate" inputFormat="DD.MM.YYYY" mask="__.__.____"
                value={state.startDate} onChange={handleStartDateChange} minDate={moment()}
                renderInput={(params) => <TextField {...params} />} />

            <DesktopDatePicker
                label="End" name="endDate" inputFormat="DD.MM.YYYY" mask="__.__.____"
                value={state.endDate} onChange={handleEndDateChange} minDate={moment()}
                renderInput={(params) => <TextField {...params} />} />
        </Box>
    );
}

export default TodoItemCycleForm;