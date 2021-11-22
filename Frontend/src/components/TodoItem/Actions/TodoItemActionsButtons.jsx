import * as React from 'react';
import { Box } from '@mui/material';
import ActionButton from '../../Common/ActionButton';

const TodoItemActionsButtons = (props) => {
    
    const { todoItem, actions, isVisible } = props;

    return (
        <Box sx={{ visibility: isVisible ? '' : 'hidden', display: 'flex', flexDirection: 'row' }}>
            {
                actions.map((actionButton) => (
                    <ActionButton
                        key={actionButton.id}
                        tooltip={actionButton.title}
                        ariaLabel={actionButton.ariaLabel}
                        onClick={() => actionButton.onClick(todoItem)}
                        isDisabled={actionButton.isDisabled}
                        icon={actionButton.icon} />
                    ))
            }
        </Box>
    );
}

export default TodoItemActionsButtons;