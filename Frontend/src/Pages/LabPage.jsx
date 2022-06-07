import * as React from 'react';
import { useState, useEffect } from 'react';
import { Grid } from '@mui/material';
import CreateHabitForm from '../components/Habit/Forms/CreateHabitForm';
import HabitsList from '../components/Habit/HabitsList';
import Api from '../Utilities/ApiHelper';
import { LogI, LogE } from '../Utilities/Utilities';
import { CreateDummyHabits } from '../Utilities/DummyObjects';

import { FindItemById } from './../Utilities/ArrayUtils';


import useAuth from '../Hooks/useAuth';

const LabPage = () => {
    let item = FindItemById([], 3);
    console.log('asdasd');

    const habitsEndpointName = "Habits";
    const { user } = useAuth();
    const [habits, setHabits] = useState(CreateDummyHabits(1));

    const handleCreateHabit = habitToCreate => {
        Api.Post(habitsEndpointName, habitToCreate, createdHabit => {
            console.log(createdHabit);
        }, (error) => {
            console.error(error);
        });
    };

    const handleHabitCompleted = completedHabit => {
        Api.Put(habitsEndpointName + "/Complete", completedHabit.id, completedHabit, () => {
            getHabits();
        });
    };

    const getHabits = () => {
        Api.Get(habitsEndpointName, habits => {
            LogI('Habits downloaded: ');
            LogI(habits);
            setHabits(habits);
        });
    };

    useEffect(() => {
        getHabits();
    }, [user]);

    return (
        <Grid container spacing={3}>
            <Grid item xs={12} md={6} lg={3}>
                <CreateHabitForm onCreateHabit={handleCreateHabit}></CreateHabitForm>
            </Grid>

            <Grid item xs={12} md={6} lg={3}>
                <HabitsList 
                    title="Your habits for today" 
                    habits={habits} 
                    onHabitCompleted={handleHabitCompleted} />
            </Grid>

            <Grid item xs={12} md={6} lg={3}>
            <HabitsList 
                    title="Your habits for today" 
                    habits={habits} 
                    onHabitCompleted={handleHabitCompleted} />
            </Grid>

            <Grid item xs={12} md={6} lg={3}>
            </Grid>
        </Grid>
    );
}

export default LabPage;