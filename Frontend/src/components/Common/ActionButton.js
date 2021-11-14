import * as React from 'react';
import { IconButton, Tooltip } from '@mui/material';

const ActionButton = ({ tooltip, ariaLabel, isDisabled, icon, onClick}) => {
    return (
        <Tooltip title={tooltip}>
            <span>
                <IconButton
                    aria-label={ariaLabel}
                    onClick={() => onClick()}
                    disabled={isDisabled}>
                    {icon}
                </IconButton>
            </span>
        </Tooltip>
    );
}

export default ActionButton;