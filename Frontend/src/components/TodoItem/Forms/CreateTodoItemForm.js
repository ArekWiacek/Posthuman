import * as React from 'react';
import { useState, useEffect } from 'react';
import { Box, TextField, Button, MenuItem, Typography } from '@mui/material';
import DesktopDatePicker from '@mui/lab/DesktopDatePicker';
import AddTaskIcon from '@mui/icons-material/AddTask';
import { LogI, LogW } from '../../../Utilities/Utilities';

const CreateTodoItemForm = ({ onCreateTodoItem, todoItems, projects, parentTaskId, parentProjectId }) => {
    const [formState, setFormState] = useState({
        title: '', description: '', deadline: null,
        parentId: parentTaskId ? parentTaskId : '',
        projectId: parentProjectId ? parentProjectId: ''
    });

    useEffect(() => {
        setFormState({ ...formState, parentId: parentTaskId });
    }, [parentTaskId]);

    const handleInputChange = e => {
        const { name, value } = e.target;
        setFormState({ ...formState, [name]: value });
    };

    const handleDeadlineChange = newValue => {
        setFormState({ ...formState, deadline: (newValue ? newValue.toDate() : null) });
    };

    const handleSubmit = e => {
        e.preventDefault();

        if (!formState.title) {
            LogW("Cannot create TodoItem - title not provided");
            return;
        }

        var todoItem = {
            title: formState.title,
            description: formState.description,
            deadline: formState.deadline,
            parentId: formState.parentId ? formState.parentId : null,
            projectId: formState.projectId ? formState.projectId : null
        };

        onCreateTodoItem(todoItem);
        setFormState({...formState, title: '', description: ''});
    };

    return (
        <Box component="form" sx={{ 
            '& .MuiTextField-root': { m: 1, width: '100%' },
            display: 'flex', flexDirection: 'column', alignItems: 'center' }}
            noValidate autoComplete="off" onSubmit={e => handleSubmit(e)}>

            <Typography variant="h5">Create task</Typography>

            <TextField
                label="Title" name="title" variant="outlined" required
                value={formState.title} onChange={handleInputChange} />

            <TextField
                label="Description" name="description" value={formState.description}
                onChange={handleInputChange} multiline rows={3} />

            <DesktopDatePicker
                label="Deadline" name="deadline" inputFormat="DD.MM.YYYY" mask="__.__.____"
                value={formState.deadline} onChange={handleDeadlineChange}
                renderInput={(params) => <TextField {...params} />} />

            <TextField
                label="Project" name="projectId" select 
                disabled={!projects || projects.length === 0} disabled
                value={formState.projectId} onChange={handleInputChange}>

                <MenuItem key={0} value={0}>Select project</MenuItem>

                {projects.map((project) => (
                    <MenuItem key={project.id} value={project.id}>
                        {project.title}
                    </MenuItem>
                ))} 
            </TextField>

            <TextField
                label="Parent task" name="parentId" select 
                disabled={!todoItems || todoItems.length === 0}
                value={formState.parentId} onChange={handleInputChange}>

                <MenuItem key={0} value={0}>Select parent task</MenuItem>

                {todoItems.map((todoItem) => (
                    <MenuItem key={todoItem.id} value={todoItem.id}>
                        {todoItem.title}
                    </MenuItem>
                ))}
            </TextField>

            <Button
                sx={{ m: 1, width: '30ch' }}
                variant="contained" type="submit"
                startIcon={<AddTaskIcon />}>
                Create
            </Button>
        </Box>
    );
}

CreateTodoItemForm.defaultProps = {
    todoItems: [], 
    projects: [],
    parentId: '',
    projectId: '' 
};

export default CreateTodoItemForm;