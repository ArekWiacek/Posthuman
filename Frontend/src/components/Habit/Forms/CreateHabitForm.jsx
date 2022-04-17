import * as React from 'react';
import { useState } from 'react';
import {
    Box, TextField, Button, Typography, FormControlLabel,
    FormControl, FormLabel, RadioGroup, Radio, ToggleButton, ToggleButtonGroup
} from '@mui/material';
import AddTaskIcon from '@mui/icons-material/AddTask';
import { LogI, LogW } from '../../../Utilities/Utilities';

import WeekdaysSelector from '../../Common/WeekdaysSelector';

const CreateHabitForm = ({ onCreateHabit }) => {
    const [formState, setFormState] = useState({
        title: '',
        repetitionPeriod: 'daily',
        weekdays: ['mon'],
        dayOfMonth: 1,
    });

    const handleInputChange = e => {
        const { name, value } = e.target;
        handleFormChange(name, value);
    };

    const handleRepetitionPeriodChanged = e => {
        const name = e.target.name;
        const value = e.target.value;
        handleFormChange(name, value);
    };

    const handleFormChange = (formProperty, newValue) => {
        setFormState({ ...formState, [formProperty]: newValue });
    };

    const handleWeekdaysChanged = newWeekdays => {
        setFormState({ ...formState, ['weekdays']: newWeekdays });
    };

    const handleSubmit = e => {
        e.preventDefault();

        if (!formState.title) {
            LogW('Cannot create Habit - title not provided');
            return;
        }

        var habit = {
            title: formState.title,
            repetitionPeriod: formState.repetitionPeriod,
            dayOfMonth: formState.dayOfMonth ? formState.dayOfMonth : null,
            weekDays: formState.weekDays ? formState.weekDays : null,
        };

        onCreateHabit(habit);
        setFormState({ ...formState, title: '', repetitionPeriod: 'daily' });
    };

    return (
        <Box component='form' sx={{
            display: 'flex', flexDirection: 'column', alignItems: 'center',
            '& .MuiTextField-root': { m: 1, width: '100%' }}}
            noValidate autoComplete='off' onSubmit={e => handleSubmit(e)}>

            <Typography variant='h5'>Create habit</Typography>

            <TextField
                label='Title' name='title' variant='outlined' required autoFocus
                value={formState.title} onChange={handleInputChange} />

            <FormControl sx={{ alignSelf: 'start' }}>
                <FormLabel id='repetition-period-label'>Repeat every</FormLabel>
                
                <RadioGroup
                    aria-labelledby='repetition-period-label'
                    name='repetitionPeriod'
                    value={formState.repetitionPeriod}
                    defaultValue={formState.repetitionPeriod}
                    onChange={handleRepetitionPeriodChanged}
                >

                    <FormControlLabel value='daily' control={<Radio />} label='Day' />

                    <FormControlLabel value='weekly' control={<Radio />} label='Week' />
                    <WeekdaysSelector 
                        initialWeekdays={formState.weekdays}
                        allowMultipleSelection={true}  
                        disabled={formState.repetitionPeriod != 'weekly'}
                        onWeekdaysChanged={handleWeekdaysChanged} />

                    <FormControlLabel value='monthly' control={<Radio />} label='Month' />
                    
                </RadioGroup>
            </FormControl>

            {/* <TextField
                label='Description' name='description' value={formState.description}
                onChange={handleInputChange} multiline rows={3} />


            <DesktopDatePicker
                label='Deadline' name='deadline' inputFormat='DD.MM.YYYY' mask='__.__.____'
                value={formState.deadline} onChange={handleDeadlineChange} minDate={moment()}
                renderInput={(params) => <TextField {...params} />} />

            <TextField
                label='Parent task' name='parentId' select
                disabled={!todoItems || todoItems.length === 0}
                value={formState.parentId} onChange={handleInputChange}>
                <MenuItem key={0} value={0}>Select parent task</MenuItem>
                {todoItems.map((todoItem) => {
                    if (!todoItem.isCompleted && todoItem.isVisible) {
                        return (
                            <MenuItem key={todoItem.id} value={todoItem.id} sx={{ pl: (todoItem.nestingLevel + 1) * 2 }}>
                                {todoItem.title}
                            </MenuItem>
                        )
                    }
                })}
            </TextField> */}

            <Button sx={{ m: 1, width: '100%' }}
                variant='contained' type='submit'
                startIcon={<AddTaskIcon />}>
                Create
            </Button>
        </Box>
    );
}

CreateHabitForm.defaultProps = {
    // todoItems: [],
    // projects: [],
    // parentTaskId: '',
    // projectId: ''
};

export default CreateHabitForm;