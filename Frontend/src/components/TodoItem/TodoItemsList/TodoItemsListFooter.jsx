import * as React from 'react';
import { Box, FormControlLabel, Switch } from '@mui/material';

const TodoItemsListFooter = ({ isDensePadding, showFinished, showHidden, 
    onDensePaddingChecked, onShowFinishedChecked, onShowHiddenChecked }) => {
    
    const handleChangeDenseChecked = e => onDensePaddingChecked(e.target.checked);
    const handleShowFinishedChecked = e => onShowFinishedChecked(e.target.checked);
    const handleShowHiddenChecked = e => onShowHiddenChecked(e.target.checked);

    return (
        <Box sx={{ paddingTop: 1, paddingBottom: 1 }}>
            <FormControlLabel
                control={<Switch checked={isDensePadding} 
                onChange={handleChangeDenseChecked} />}
                label="Dense padding?" />
            <FormControlLabel
                control={<Switch checked={showFinished} 
                onChange={handleShowFinishedChecked} />}
                label="Display finished?" />
            <FormControlLabel
                control={<Switch checked={showHidden} 
                onChange={handleShowHiddenChecked} />}
                label="Display hidden?" />
        </Box>
    );
}

export default TodoItemsListFooter;