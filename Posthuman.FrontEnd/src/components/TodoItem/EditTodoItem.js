import * as React from 'react';
import { Box, TextField, Button, MenuItem, Typography } from '@mui/material';
import DesktopDatePicker from '@mui/lab/DesktopDatePicker';
import AddTaskIcon from '@mui/icons-material/AddTask';
import CancelIcon from '@mui/icons-material/Cancel';

import { LogW } from '../../Utilities/Utilities';

const defaultFormValues = {
    title: '',
    description: '',
    deadline: new Date(),
    projectId: 0,
    avatarId: 0
}

const EditTodoItem = ({ currentTodoItem, onSaveChanges, onCancelEdit, projects }) => {
    const [todoItem, setTodoItem] = React.useState({ ...currentTodoItem });

    const handleTitleChange = event => setTodoItem({ ...todoItem, title: event.target.value });
    const handleDescriptionChange = event => setTodoItem({ ...todoItem, description: event.target.value });
    const handleDeadlineChange = newValue => setTodoItem({ ...todoItem, deadline: newValue });
    const handleProjectIdChange = event => setTodoItem({ ...todoItem, projectId: event.target.value });
    const handleCancelEdit = () => onCancelEdit();

    const handleSubmit = (e) => {
        e.preventDefault();

        if (todoItem.title === "" || todoItem.deadline === null) {
            LogW("Cannot edit TodoItem - title or deadline not provided");
            return;
        }

        if (todoItem.avatarId == null || todoItem.avatarId == 0) {
            LogW("Cannot edit TodoItem - active avatar unknown");
            return;
        }

        // "Select" form control uses 0 as "not-selected"
        // Backend model uses "null", so quick convert: 
        if (todoItem.projectId == 0) {
            todoItem.projectId = null;
        }

        onSaveChanges(todoItem.id, todoItem);
        setTodoItem({ ...defaultFormValues });
    }

    return (
        <Box component="form" sx={{ '& .MuiTextField-root': { m: 1, width: '30ch' } }}
            noValidate autoComplete="off" onSubmit={e => handleSubmit(e)}>
            <Typography variant="h5">Edit task</Typography>
            <TextField
                id="title"
                label="Title"
                variant="outlined"
                value={todoItem.title}
                onChange={handleTitleChange}
                required />
            <TextField
                id="description"
                label="Description"
                multiline
                rows={3}
                value={todoItem.description}
                onChange={handleDescriptionChange} />
            <DesktopDatePicker
                label="Deadline"
                inputFormat="DD.MM.YYYY"
                mask="__.__.____"
                value={todoItem.deadline}
                onChange={handleDeadlineChange}
                renderInput={(params) => <TextField {...params} />} />
            <TextField
                select
                fullWidth
                label="Project"
                value={todoItem.projectId != null ? todoItem.projectId : 0}
                onChange={handleProjectIdChange}>
                <MenuItem key={0} value={0}>
                    Select project...
                </MenuItem>
                {projects.map((project) => (
                    <MenuItem key={project.id} value={project.id}>
                        {project.title}
                    </MenuItem>
                ))}
            </TextField>
            <Box
                sx={{
                    display: 'flex',
                    justifyContent: 'center',
                    p: 1,
                    m: 1
                }}>
                <Button
                    variant="contained"
                    startIcon={<CancelIcon />}
                    onClick={handleCancelEdit}
                    sx={{ mr: 2}}>
                    Cancel
                </Button>
                <Button
                    variant="contained"
                    startIcon={<AddTaskIcon />}
                    type="submit">
                    Save
                </Button>
            </Box>
        </Box >
    );
}

export default EditTodoItem;