import React from 'react';
import { List, ListSubheader } from '@mui/material';
import HabitsListItem from './HabitsListItem';
import customStyles from '../Common/CustomStyles';

const HabitsList = ({ title, habits, onHabitCompleted }) => {
    const classes = customStyles.habitListStyles();

    return (
        <List 
            className={classes.listRoot} 
            subheader={<ListSubheader>{title}</ListSubheader>}
        >
            {habits.map(habit => {
                return (
                    <HabitsListItem key={habit.id} habit={habit} onHabitCompleted={onHabitCompleted} />
                );
            })}
        </List>
    );
}

export default HabitsList;