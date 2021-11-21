import React, { useState, useEffect, useRef } from 'react';
import { TextField, IconButton, TableRow, TableCell, ClickAwayListener } from '@mui/material';
import ControlPointIcon from '@mui/icons-material/ControlPoint';
import CancelIcon from '@mui/icons-material/Cancel';
import { LogI } from '../../../Utilities/Utilities';

const CreateTodoItemInline = ({ parentTodoItem, onCreate, onCancel }) => {
    const [title, setTitle] = useState('');
    const titleInputRef = useRef(null);

    const paddingLeftPx = (parentTodoItem.nestingLevel + 1) * 2;

    const handleTitleChange = event => setTitle(event.target.value);
    
    const handleCreateClicked = parentTask => {
        createSubtask(parentTask);
        titleInputRef.current.focus();
    };

    const handleCancelClicked = () => onCancel();

    useEffect(() => {
        if (titleInputRef && titleInputRef.current) {
            titleInputRef.current.focus();
        }
    }, []);

    const handleLostFocus = e => {
    };

    const handleGotFocus = e => {
    };

    const handleClickAway = e => {
        onCancel();
    }

    const createSubtask = (parent) => {
        if (!title)
            return;

        const newSubtask = {
            title: title,
            parentId: parent.id,

            deadline: parent.deadline,
            avatarId: parent.avatarId,
            projectId: parent.projectId,
            nestingLevel: parent.nestingLevel + 1,
            isVisible: parent.isVisible
        };

        onCreate(newSubtask);
        setTitle('');
    };

    const handleKeyDown = (event, parentTodoItem) => {
        switch (event.key) {
            case 'Enter':
                createSubtask(parentTodoItem);
                break;

            case 'Escape':
                onCancel();
                break;

            default:
                break;
        }
    };

    return (
        <TableRow>
            <TableCell component="th" scope="row" colSpan={4}>
                <ClickAwayListener onClickAway={handleClickAway}>
                    <TextField
                        variant="standard" margin="dense" size="small"
                        placeholder="Type subtask title" value={title} inputRef={titleInputRef}
                        onChange={handleTitleChange} required fullWidth
                        sx={{ minWidth: '600px', paddingRight: '80px', paddingLeft: paddingLeftPx }}
                        onKeyDown={(e) => handleKeyDown(e, parentTodoItem)}
                        onBlur={(e) => handleLostFocus(e)} onFocus={(e) => handleGotFocus(e)}
                        InputProps={{
                            endAdornment: (
                                <React.Fragment>
                                    <IconButton onClick={() => handleCancelClicked()}>
                                        <CancelIcon />
                                    </IconButton>
                                    <IconButton onClick={() => handleCreateClicked(parentTodoItem)}>
                                        <ControlPointIcon />
                                    </IconButton>
                                </React.Fragment>
                            )
                        }}
                    />
                </ClickAwayListener>
            </TableCell>
        </TableRow>
    );
}

export default CreateTodoItemInline;