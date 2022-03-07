import * as React from 'react';
import { useState } from 'react';
import { Menu, Button, FormGroup, FormControlLabel, Switch } from '@mui/material';
import MenuItem from '@mui/material/MenuItem';
import SettingsIcon from '@mui/icons-material/Settings';

const TodoListDisplayOptionsMenu = ({ options, onOptionChanged }) => {
    const [triggerElementAnchor, setTriggerElementAnchor] = useState(null);
    const isOpen = Boolean(triggerElementAnchor);

    const handleClick = event => {
        setTriggerElementAnchor(event.currentTarget);
    };

    const handleClose = event => {
        setTriggerElementAnchor(null);
    };

    const handleOptionChange = e => {
        onOptionChanged(e);
    };

    return (
        <div>
            <Button
                variant='outlined'
                startIcon={<SettingsIcon />}
                onClick={handleClick}
                size='large'
                sx={{ height: 48 }}>
                Display Options
            </Button>

            <Menu
                id='display-options-menu'
                anchorEl={triggerElementAnchor}
                open={isOpen}
                onClose={handleClose}
                disableRestoreFocus={true}>
                {
                    options.map((option) => {
                        return (
                            <MenuItem key={option.id}>
                                <FormGroup>
                                    <FormControlLabel
                                        label={option.title}
                                        control={
                                            <Switch
                                                checked={option.isChecked}
                                                name={option.name}
                                                onChange={handleOptionChange} />} 
                                    />
                                </FormGroup>
                            </MenuItem>)
                    })
                }
            </Menu>
        </div >
    );
}

export default TodoListDisplayOptionsMenu;