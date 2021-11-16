import * as React from 'react';
import { Box, FormControlLabel, FormControl, FormGroup, Checkbox, Switch, FormLabel,
    ToggleButtonGroup, ToggleButton, Typography } from '@mui/material';
import AccountTreeIcon from '@mui/icons-material/AccountTree';
import ListIcon from '@mui/icons-material/List';
import { LogI } from '../../../Utilities/Utilities';

const TodoItemsListOptions = ({ isDensePadding, showFinished, showHidden, onDensePaddingChecked, onShowFinishedChecked, onShowHiddenChecked }) => {
    var displayMode = "hierarchical";
    
    const handleChangeDenseChecked = e => {
        localStorage.setItem('isDensePadding', isDensePadding);
        onDensePaddingChecked(!isDensePadding)
    };
    
    const handleShowFinishedChecked = e => onShowFinishedChecked(e.target.checked);
    const handleShowHiddenChecked = e => onShowHiddenChecked(e.target.checked);

    const handleDisplayModeSelected = e => { LogI(e); };
    // const [listOptions, setListOptions] = { //useState({
    //     show: {
    //         hidden: true,
    //         finished: true,
    //     },
    //     display: {
    //         hierarchical: true,
    //         flat: false
    //     },
    //     longList: {
    //         scroll: true,
    //         pagination: false
    //     },
    //     keyboardEnabled: true
    // };

    return (
        <Box sx={{ paddingTop: 1, paddingBottom: 1, display: 'flex', flexDirection: 'row', justifyContent: 'space-between' }}>
            <FormControl 
                sx={{ m: 1, flexDirection: 'column' }} 
                component="fieldset" variant="standard">
                {/* <FormLabel component="legend">Display</FormLabel> */}
                <FormGroup>
                    <FormControlLabel
                        control={<Checkbox checked={showHidden} onChange={handleShowHiddenChecked} name="hidden" />}
                        label="Hidden tasks" />
                    <FormControlLabel
                        control={<Checkbox checked={showFinished} onChange={handleShowFinishedChecked} name="finished" />}
                        label="Finished tasks" />
                    <FormControlLabel
                        control={<Checkbox checked={!isDensePadding} onChange={handleChangeDenseChecked} name="dense" />}
                        label="Big items" />
                </FormGroup>
            </FormControl>

            <ToggleButtonGroup
                value={displayMode}
                exclusive sx={{ m: 1, flexDirection: 'row', alignItems: 'flex-start' }}
                onChange={handleDisplayModeSelected}
                aria-label="text alignment"
                disabled>
                <ToggleButton value="hierarchical" key="hierarchical">
                    <AccountTreeIcon />
                </ToggleButton>,
                <ToggleButton value="flat" key="flat">
                    <ListIcon />
                </ToggleButton>
            </ToggleButtonGroup>
        </Box>
    );
}

export default TodoItemsListOptions;