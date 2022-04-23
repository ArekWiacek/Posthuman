import * as React from 'react';
import { Paper, ListItem, ListItemButton, ListItemIcon, ListItemText, Checkbox, IconButton } from '@mui/material';
import { LogI } from '../../Utilities/Utilities';
import AddIcon from '@mui/icons-material/Add';

const HabitsListItem = ({ habit, onHabitCompleted }) => {

    const handleCompletedClicked = habit => () => {
        LogI('handling completed clicked: ');
        LogI(habit);

        if (onHabitCompleted)
            onHabitCompleted(habit);
    };

    const handleToggle = (value) => () => {
        LogI('handling toggle clicked: ');
        LogI(value);
      };

    return (
        <Paper>
            <ListItem
                key={habit.id}
                // secondaryAction={
                //     <IconButton edge="end" aria-label="comments">
                //         <AddIcon />
                //     </IconButton>
                // }
                
                disablePadding
                disabled={!habit.isActive}
            >
                <ListItemButton onClick={handleCompletedClicked(habit)} dense>
                    <ListItemIcon>
                        <Checkbox
                            edge="start"
                            checked={false}
                            disabled={!habit.isActive}
                            tabIndex={-1}
                            disableRipple
                        />
                    </ListItemIcon>
                    <ListItemText primary={habit.title} />
                </ListItemButton>
            </ListItem>
        </Paper>
    );
}

export default HabitsListItem;