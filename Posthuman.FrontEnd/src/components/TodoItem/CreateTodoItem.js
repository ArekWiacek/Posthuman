import * as React from 'react';
import { Box, TextField, Button, MenuItem, Typography } from '@mui/material';
import AddTaskIcon from '@mui/icons-material/AddTask';
import DesktopDatePicker from '@mui/lab/DesktopDatePicker';

import { AvatarContext } from "../../App";
import { LogW } from '../../Utilities/Utilities';

const CreateTodoItem = ({ onCreateTodoItem, projects }) => {
    const { activeAvatar } = React.useContext(AvatarContext);
    const [title, setTitle] = React.useState("");
    const [description, setDescription] = React.useState("");
    const [deadline, setDeadline] = React.useState(new Date());
    const [projectId, setProjectId] = React.useState(0);

    const handleTitleChange = event => setTitle(event.target.value);
    const handleDescriptionChange = event => setDescription(event.target.value);
    const handleDeadlineChange = newValue => setDeadline(newValue.toDate());
    const handleProjectIdChange = event => setProjectId(event.target.value);

    const handleSubmit = (e) => {
        e.preventDefault();

        if (title === "" || deadline == null) {
            LogW("Cannot create TodoItem - title or deadline not provided");
            return;
        }

        if (activeAvatar == null || activeAvatar.id == 0) {
            LogW("Cannot create TodoItem - active avatar unknown");
            return;
        }

        const newTodoItem = {
            title: title,
            description: description,
            deadline: deadline,
            projectId: projectId != 0 ? projectId : null,
            avatarId: activeAvatar.id
        };

        onCreateTodoItem(newTodoItem);

        setTitle("");
        setDescription("");
        setDeadline(new Date());
        // setProjectId(0);         // Resetting project id turned off to easier add subtasks one by one to project 
    }

    return (
        <Box component="form" sx={{ '& .MuiTextField-root': { m: 1, width: '30ch' } }}
            noValidate autoComplete="off" onSubmit={e => handleSubmit(e)}>
            <Typography variant="h5">Create task</Typography>
            <TextField
                id="title"
                label="Title"
                variant="outlined"
                value={title}
                onChange={handleTitleChange}
                required />
            <TextField
                id="description"
                label="Description"
                multiline
                rows={3}
                value={description}
                onChange={handleDescriptionChange} />

            <DesktopDatePicker
                label="Deadline"
                inputFormat="DD.MM.YYYY"
                mask="__.__.____"
                value={deadline}
                onChange={handleDeadlineChange}
                renderInput={(params) => <TextField {...params} />} />

            <TextField
                select
                fullWidth
                label="Project"
                value={projectId}
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
            <Button
                sx={{ m: 1, width: '30ch' }}
                variant="contained"
                startIcon={<AddTaskIcon />}
                type="submit">
                Create
            </Button>
        </Box>
    );
}

export default CreateTodoItem;