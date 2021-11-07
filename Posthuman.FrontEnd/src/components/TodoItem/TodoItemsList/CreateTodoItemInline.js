import * as React from 'react';
import { Box, TextField, Button, MenuItem, Typography, IconButton, TableRow, TableCell } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import DeleteIcon from '@mui/icons-material/Delete';

import DesktopDatePicker from '@mui/lab/DesktopDatePicker';

import { AvatarContext } from "../../../App";
import { LogI, LogW } from '../../../Utilities/Utilities';

const CreateTodoItemInline = ({ parentTask, onCreate, onDiscard }) => {
    const { activeAvatar } = React.useContext(AvatarContext);
    const [title, setTitle] = React.useState("");

    const handleTitleChange = event => setTitle(event.target.value);
    
    const handleCreateClicked = (parent) => {
        if (title === "") {
            LogW("Cannot create TodoItem - title not provided");
            return;
        }

        if (activeAvatar == null || activeAvatar.id == 0) {
            LogW("Cannot create TodoItem - active avatar unknown");
            return;
        }

        const newSubtask = {
            title: title,
            description: '',
            deadline: parent.deadline,
            projectId: parent.projectId != 0 ? parent.projectId : null,
            parentId: parent.id,
            avatarId: activeAvatar.id,
            nestingLevel: parent.nestingLevel + 1
        }

        onCreate(newSubtask);
    };

    const handleDiscardClicked = () => {
        onDiscard();
    };

    return (
        <TableRow key={"subtask_of_" + parentTask.id}>
            <TableCell component="th" scope="row">
                {/* Parent: {parentTask.id} */}
            </TableCell>

            <TableCell component="th" scope="row" colSpan={7}>
                <TextField
                    id="title"
                    label="Title"
                    variant="outlined"
                    value={title}
                    onChange={handleTitleChange}
                    sx={{width: '100%'}}
                    required 
                    autoFocus />
            </TableCell>

            <TableCell align="right">
                <IconButton
                    aria-label="add-subtask"
                    onClick={() => handleCreateClicked(parentTask)}>
                    <AddIcon />
                </IconButton>
                <IconButton
                    aria-label="discard-subtask"
                    onClick={() => handleDiscardClicked()}>
                    <DeleteIcon />
                </IconButton>
            </TableCell>
        </TableRow>

    );
}

export default CreateTodoItemInline;