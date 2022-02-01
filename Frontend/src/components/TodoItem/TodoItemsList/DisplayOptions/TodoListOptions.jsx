import React, { useContext } from 'react';
import { Box } from '@mui/material';
import { useTheme } from '@mui/material/styles';
import { ColorModeContext } from '../../../Layout/LayoutWrapper';
import DateSelector from '../../../Common/DateSelector';
import TodoListDisplayOptions from './TodoListDisplayOptions';
import TodoListDisplayMode from './TodoListDisplayMode';

const TodoListOptions = ({ listDisplayOptions, onDisplayOptionsChanged }) => {
    const theme = useTheme();
    const colorMode = useContext(ColorModeContext);

    const handleOptionChange = e => {
        let optionName = e.target.name;
        let optionValue = e.target.checked;

        if(optionName == 'isDarkMode') {
            onDisplayOptionsChanged('isDarkMode', optionValue);
            //colorMode.toggleColorMode();  // TODO
        } else {
            onDisplayOptionsChanged(optionName, optionValue);
        }
    };

    const handleDisplayModeChange = (e, value) => {
        onDisplayOptionsChanged('displayMode', value);
    };

    const handleDateChange = newDate => {
        onDisplayOptionsChanged('selectedDate', newDate.format('DD.MM.YYYY'));
    };

    return (
        <Box sx={{ 
            display: 'flex', flexDirection: 'row', 
            justifyContent: 'space-between', alignItems: 'center' }}
        >
            <TodoListDisplayOptions 
                displayOptions={listDisplayOptions}
                onOptionChanged={handleOptionChange} />

            <TodoListDisplayMode
                displayOptions={listDisplayOptions}
                onDisplayModeChanged={handleDisplayModeChange} />

            <Box sx={{ display: listDisplayOptions.displayMode !== 'dayByDay' ? 'none' : '' }}>
                <DateSelector initialValue={listDisplayOptions.selectedDate} onDateChanged={handleDateChange} />
            </Box>
        </Box>
    );
}

export default TodoListOptions;