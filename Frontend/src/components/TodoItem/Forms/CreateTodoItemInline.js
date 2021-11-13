import * as React from 'react';
import { useState, useContext } from 'react';
import { TextField, IconButton, TableRow, TableCell } from '@mui/material';
import ControlPointIcon from '@mui/icons-material/ControlPoint';
import CancelIcon from '@mui/icons-material/Cancel';
import { AvatarContext } from "../../../App";

const CreateTodoItemInline = ({ parentTodoItem, onCreate, onCancel }) => {
    const { activeAvatar } = useContext(AvatarContext);
    const [title, setTitle] = useState('');
    const paddingLeftPx = (parentTodoItem.nestingLevel + 1) * 2;

    const handleTitleChange = event => setTitle(event.target.value);
    const handleCreateClicked = parentTask => createSubtask(parentTask);
    const handleCancelClicked = () => onCancel();

    const createSubtask = (parent) => {
        if (!title || !activeAvatar || !activeAvatar.id) 
            return;

        const newSubtask = {
            title: title,
            parentId: parent.id,
            
            deadline: parent.deadline,
            avatarId: activeAvatar.id,
            projectId: parent.projectId,
            nestingLevel: parent.nestingLevel + 1
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
                <TextField
                    variant="standard" margin="dense" size="small" 
                    placeholder="Type subtask title" value={title}
                    onChange={handleTitleChange} required autoFocus fullWidth
                    sx={{ minWidth: '600px', paddingRight: '80px', paddingLeft: paddingLeftPx }}
                    onKeyDown={(e) => handleKeyDown(e, parentTodoItem)}
                    InputProps={{
                        endAdornment: (
                            <React.Fragment>
                                <IconButton
                                    aria-label="cancel-subtask-creation"
                                    onClick={() => handleCancelClicked()}>
                                    <CancelIcon />
                                </IconButton>
                                <IconButton
                                    aria-label="create-subtask"
                                    onClick={() => handleCreateClicked(parentTodoItem)}>
                                    <ControlPointIcon />
                                </IconButton>
                            </React.Fragment>        
                        )}} 
                /> 
            </TableCell>
        </TableRow>
    );
}

export default CreateTodoItemInline;