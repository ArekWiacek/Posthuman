import React from 'react';
import { Box, ToggleButtonGroup, ToggleButton, Typography } from '@mui/material';
import AccountTreeIcon from '@mui/icons-material/AccountTree';
import ListIcon from '@mui/icons-material/List';
import TodayIcon from '@mui/icons-material/Today';

const TodoListDisplayMode = ({ displayOptions, onDisplayModeChanged }) => {
    
    const handleDisplayModeChange = (e, value) => {
        onDisplayModeChanged('displayMode', value);
    };

    return (
        <Box>
            <ToggleButtonGroup
                value={displayOptions.displayMode}
                sx={{ m: 1, display: 'flex', flexDirection: 'row', alignItems: 'flex-start' }}
                onChange={(e, option) => handleDisplayModeChange(e, option)} exclusive>
                <ToggleButton value='hierarchical' key='hierarchical'>
                    <AccountTreeIcon />
                </ToggleButton>
                <ToggleButton value='flat' key='flat'>
                    <ListIcon />
                </ToggleButton>
                <ToggleButton value='dayByDay' key='dayByDay'>
                    <TodayIcon />
                </ToggleButton>
            </ToggleButtonGroup>
        </Box >
    );
}

export default TodoListDisplayMode;