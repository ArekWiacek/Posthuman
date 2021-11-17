import * as React from 'react';
import {
    Box, FormControlLabel, FormControl, FormGroup, Checkbox, Switch, FormLabel,
    ToggleButtonGroup, ToggleButton
} from '@mui/material';
import AccountTreeIcon from '@mui/icons-material/AccountTree';
import ListIcon from '@mui/icons-material/List';
import { LogI } from '../../../Utilities/Utilities';

const TodoItemsListOptions = ({ isDensePadding, showFinished, showHidden, isSmallMenu,
    onDensePaddingChecked, onShowFinishedChecked, onShowHiddenChecked, onSmallMenuChecked }) => {
    var displayMode = "hierarchical";

    const handleChangeDenseChecked = e => {
        localStorage.setItem('isDensePadding', isDensePadding);
        onDensePaddingChecked(!isDensePadding)
    };

    const handleShowFinishedChecked = e => onShowFinishedChecked(e.target.checked);
    const handleShowHiddenChecked = e => onShowHiddenChecked(e.target.checked);
    const handleIsSmallMenuChecked = e => onSmallMenuChecked(e.target.checked);

    const handleDisplayModeSelected = e => { LogI(e); };

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

            <Box sx={{ flexDirection: 'column' }}>
                <ToggleButtonGroup
                    value={displayMode}
                    sx={{ m: 1, display: 'flex', flexDirection: 'row', alignItems: 'flex-start' }}
                    onChange={handleDisplayModeSelected}
                    exclusive disabled>
                    <ToggleButton value="hierarchical" key="hierarchical">
                        <AccountTreeIcon />
                    </ToggleButton>,
                    <ToggleButton value="flat" key="flat">
                        <ListIcon />
                    </ToggleButton>
                </ToggleButtonGroup>

                <FormGroup sx={{ m: 1 }}>
                    <FormControlLabel
                        control={<Switch checked={isSmallMenu} onChange={handleIsSmallMenuChecked} name="small-menu" />}
                        label="Small menu" />
                </FormGroup>
            </Box>
        </Box>
    );
}

export default TodoItemsListOptions;