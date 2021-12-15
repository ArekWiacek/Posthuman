import React, { useContext } from 'react';
import { Box, FormControlLabel, FormControl, FormGroup, Switch, ToggleButtonGroup, ToggleButton, Typography } from '@mui/material';
import { useTheme } from '@mui/material/styles';
import AccountTreeIcon from '@mui/icons-material/AccountTree';
import ListIcon from '@mui/icons-material/List';
import TodayIcon from '@mui/icons-material/Today';
import { LogI } from '../../../Utilities/Utilities';
import { ColorModeContext } from '../../Layout/LayoutWrapper';
import DateSelector from '../../Common/DateSelector';

const TodoItemsListOptions = ({ listDisplayOptions, onDisplayOptionsChanged }) => {
    const theme = useTheme();
    const colorMode = useContext(ColorModeContext);

    const handleToggleColorMode = e => {
        let isDarkModeValue = e.target.checked;
        colorMode.toggleColorMode();
        onDisplayOptionsChanged('isDarkMode', isDarkModeValue);
    };

    const handleOptionChange = e => {
        let optionName = e.target.name;
        let optionValue = e.target.checked;
        onDisplayOptionsChanged(optionName, optionValue);
    };

    const handleDisplayModeChange = (e, value) => {
        onDisplayOptionsChanged('displayMode', value);
    };

    const handleDateChange = newDate => {
        onDisplayOptionsChanged('selectedDate', newDate.format('DD.MM.YYYY'));
    };

    return (
        <Box sx={{ paddingTop: 1, paddingBottom: 1, display: 'flex', flexDirection: 'row', justifyContent: 'space-between' }}>
            <FormControl sx={{ m: 1, flexDirection: 'column' }} component="fieldset" variant="standard">
                <FormGroup>
                    <FormControlLabel
                        control={<Switch checked={listDisplayOptions.showHiddenTasks} onChange={handleOptionChange} name='showHiddenTasks' />}
                        label='Show hidden tasks' />
                    <FormControlLabel
                        control={<Switch checked={listDisplayOptions.showFinishedTasks} onChange={handleOptionChange} name='showFinishedTasks' />}
                        label='Show finished tasks' />
                    <FormControlLabel
                        control={<Switch checked={listDisplayOptions.bigItems} onChange={handleOptionChange} name='bigItems' />}
                        label='Make list bigger' />
                </FormGroup>
            </FormControl>

            <Box sx={{ flexDirection: 'column' }}>
                <FormGroup sx={{ m: 1 }}>
                    <FormControlLabel
                        control={<Switch checked={listDisplayOptions.collapsedMenu} onChange={handleOptionChange} name='collapsedMenu' />}
                        label='Collapse menu' />
                </FormGroup>

                <FormGroup sx={{ m: 1 }}>
                    <FormControlLabel
                        control={<Switch checked={theme.palette.mode === 'dark'} onChange={handleToggleColorMode} name='isDarkMode' />}
                        label='Dark mode' />
                </FormGroup>
            </Box>

            <Box>
                <ToggleButtonGroup
                    value={listDisplayOptions.displayMode}
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
            </Box>

            <Box sx={{ display: listDisplayOptions.displayMode !== 'dayByDay' ? 'none' : '' }}>
                <DateSelector initialValue={listDisplayOptions.selectedDate} onDateChanged={handleDateChange} />
            </Box>
        </Box>
    );
}

export default TodoItemsListOptions;