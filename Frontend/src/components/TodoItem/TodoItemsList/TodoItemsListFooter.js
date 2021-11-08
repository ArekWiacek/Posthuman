import * as React from 'react';
import { Box, FormControlLabel, Switch } from '@mui/material';

const TodoItemsListFooter = ({ isDensePadding, onDensePaddingChecked, showFinished, onShowFinishedChecked }) => {

    const handleChangeDenseChecked = (event) => {
        onDensePaddingChecked(event.target.checked);
    };

    const handleShowFinishedChecked = (event) => {
        onShowFinishedChecked(event.target.checked);
    };

    return (
        <Box>
            <FormControlLabel
                control={<Switch checked={isDensePadding} onChange={handleChangeDenseChecked} />}
                label="Dense padding?" />
            <FormControlLabel
                control={<Switch checked={showFinished} onChange={handleShowFinishedChecked} />}
                label="Display finished?" />
        </Box>
    );
}

export default TodoItemsListFooter;