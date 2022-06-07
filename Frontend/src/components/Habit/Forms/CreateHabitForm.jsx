import * as React from 'react';
import { useState } from 'react';
import {
    Box, TextField, Button, Typography, FormControlLabel,
    FormControl, FormLabel, RadioGroup, Radio, ToggleButton, ToggleButtonGroup
} from '@mui/material';
import AddTaskIcon from '@mui/icons-material/AddTask';
import { LogI, LogW } from '../../../Utilities/Utilities';
import DaysOfWeekSelector from '../../Common/DaysOfWeekSelector';

const defaultValues = {
    title: 'Type your habit title',
    description: 'Describe your habit',
    repetitionPeriod: 'weekly',
    daysOfWeek: ['mon'],
    dayOfMonth: 1,
};

const CreateHabitForm = ({ onCreateHabit }) => {
    const [formState, setFormState] = useState(defaultValues);

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

    const handleDaysOfWeekChanged = newDaysOfWeek => {
        setFormState({ ...formState, ['daysOfWeek']: newDaysOfWeek });
    };

    const handleSubmit = e => {
        e.preventDefault();

        if (!formState.title) {
            LogW('Cannot create Habit - title not provided');
            return;
        }

        if (formState.repetitionPeriod == 'monthly') {
            if (formState.dayOfMonth < 1 || formState.dayOfMonth > 31) {
                LogW('If you want to repeat your habit each month, select number from 1 to 31 to specify on which day of month you want to accomplish it.');
                return;
            }
        }

        var habit = {
            title: formState.title,
            description: formState.description,
            repetitionPeriod: formState.repetitionPeriod,
            dayOfMonth: formState.dayOfMonth ? formState.dayOfMonth : null,
            daysOfWeek: formState.daysOfWeek ? formState.daysOfWeek : null,
        };

        onCreateHabit(habit);
        setFormState({ ...formState, title: 'Another habit', repetitionPeriod: 'weekly' });
    };

    return (
        <Box component='form' sx={{
            display: 'flex', flexDirection: 'column', alignItems: 'center',
            '& .MuiTextField-root': { m: 1, width: '100%' }
        }} noValidate autoComplete='off' onSubmit={e => handleSubmit(e)}>

            <Typography variant='h5'>Create habit</Typography>

            <TextField
                label='Title' name='title' variant='outlined' required autoFocus
                value={formState.title} onChange={handleInputChange} />

            <TextField
                label='Description' name='description' variant='outlined' multiline rows={3}
                value={formState.description} onChange={handleInputChange} />

            <FormControl sx={{ alignItems: 'center', width: '100%' }}>
                <FormLabel id='repetition-period-label'>Repeat every</FormLabel>

                <RadioGroup
                    aria-labelledby='repetition-period-label'
                    name='repetitionPeriod'
                    value={formState.repetitionPeriod}
                    defaultValue={formState.repetitionPeriod}
                    onChange={handleRepetitionPeriodChanged}
                >
                    <FormControlLabel value='weekly' control={<Radio />} label='Week' />

                    <DaysOfWeekSelector
                        initialDaysOfWeek={formState.daysOfWeek}
                        allowMultipleSelection={true}
                        disabled={formState.repetitionPeriod != 'weekly'}
                        onDaysChanged={handleDaysOfWeekChanged} />

                    Days of week: {formState.daysOfWeek}

                    <FormControlLabel value='monthly' control={<Radio />} label='Month' />

                    <TextField
                        label='Day of month' name='dayOfMonth' variant='outlined'
                        disabled={formState.repetitionPeriod != 'monthly'}
                        value={formState.dayOfMonth} onChange={handleInputChange} inputProps={{ min: 1, max: 31 }} />

                </RadioGroup>
            </FormControl>

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