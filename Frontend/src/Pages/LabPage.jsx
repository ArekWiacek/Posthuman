import * as React from 'react';
import { useState } from 'react';
import { Grid } from '@mui/material';
import CreateHabitForm from '../components/Habit/Forms/CreateHabitForm';
import WeekdaysSelector from '../components/Common/WeekdaysSelector';

import { FindItemById } from './../Utilities/ArrayUtils';


const LabPage = () => {
    let item = FindItemById([], 3);
    console.log('asdasd');

    const handleCreateHabit = (habitToCreate) => {
        console.log('Habit to create: ');
        console.log(habitToCreate);
    }

    const [weekdays1, setWeekdays1] = useState(['monday']);
    const [weekdays2, setWeekdays2] = useState();

    const handleWeekdaysChanged1 = (selectedWeekdays) => {
        console.log("Selected weekdays in 1: ");
        console.log(selectedWeekdays);
        setWeekdays1(selectedWeekdays);
    };

    const handleWeekdaysChanged2 = (selectedWeekdays) => {
        console.log("Selected weekdays in 2: ");
        console.log(selectedWeekdays);
        setWeekdays2(selectedWeekdays);
    };

    return (
        <Grid container spacing={3}>
            <Grid item xs={12} md={6} lg={3}>
                <CreateHabitForm onCreateHabit={handleCreateHabit}></CreateHabitForm>
            </Grid>

            <Grid item xs={12} md={6} lg={3} disabled>
                <WeekdaysSelector 
                    initialWeekdays={weekdays1} 
                    allowMultipleSelection={false} 
                    onWeekdaysChanged={handleWeekdaysChanged1} 
                    disabled />
            </Grid>

            <Grid item xs={12} md={6} lg={3}>
                <WeekdaysSelector 
                    initialWeekdays={weekdays2} 
                    onWeekdaysChanged={handleWeekdaysChanged2} />
            </Grid>

            <Grid item xs={12} md={6} lg={3}>
                Values from Labpage: 1 - [ {weekdays1} ], 2 - [ {weekdays2} ] 
            </Grid>
        </Grid>
    );
}

export default LabPage;