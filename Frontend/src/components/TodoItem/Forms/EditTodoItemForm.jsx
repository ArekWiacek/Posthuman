import * as React from 'react';
import { useState } from 'react';
import { Box, TextField, Button, MenuItem, Typography } from '@mui/material';
import DesktopDatePicker from '@mui/lab/DesktopDatePicker';
import AddTaskIcon from '@mui/icons-material/AddTask';
import CancelIcon from '@mui/icons-material/Cancel';
import moment from 'moment';
import { LogW } from '../../../Utilities/Utilities';

const EditTodoItemForm = ({ todoItemToEdit, todoItems, projects, onSaveEdit, onCancelEdit }) => {
    const [todoItem, setTodoItem] = useState({ ...todoItemToEdit });

    const handleInputChange = e => {
        const { name, value } = e.target;
        setTodoItem({ ...todoItem, [name]: value });
    };

    const handleDeadlineChange = newValue => {
        setTodoItem({ ...todoItem, deadline: (newValue ? newValue.toDate() : null)})
    };

    const handleCancelEdit = () => onCancelEdit();

    const handleSubmit = (e) => {
        e.preventDefault();

        if (!todoItem || !todoItem.title || !todoItem.avatarId) { 
            LogW("Cannot save changes in TodoItem");    
            return;
        }

        // "Select" form control uses 0 as "not-selected"
        // Backend model uses "null", so needs to be converted: 
        if (todoItem.projectId === 0) 
            todoItem.projectId = null;
        if (todoItem.parentId === 0)
            todoItem.parentId = null;

        onSaveEdit(todoItem.id, todoItem);
    }

    return (
        <Box component="form" sx={{ '& .MuiTextField-root': { m: 1, width: '100%' }, 
            display: 'flex', flexDirection: 'column', alignItems: 'center' }}
            noValidate autoComplete="off" onSubmit={e => handleSubmit(e)}>

            <Typography variant="h5">Edit task</Typography>

            <TextField
                label="Title" name="title" variant="outlined" required autoFocus
                value={todoItem.title} onChange={handleInputChange} />

            <TextField
                label="Description" name="description" multiline rows={3}
                value={todoItem.description} onChange={handleInputChange} />

            <DesktopDatePicker
                label="Deadline" name="deadline" inputFormat="DD.MM.YYYY" mask="__.__.____"
                value={todoItem.deadline} onChange={handleDeadlineChange} minDate={moment()}
                renderInput={(params) => <TextField {...params} />} />

            <TextField
                label="Project" name="projectId" select
                disabled={!projects || projects.length === 0} disabled
                value={todoItem.projectId ? todoItem.projectId : ''} 
                onChange={handleInputChange}>
                
                <MenuItem key={0} value={0}>Select project...</MenuItem>
                
                {projects.map((project) => (
                    <MenuItem key={project.id} value={project.id}>
                        {project.title}
                    </MenuItem>
                ))}
            </TextField>

            <TextField
                label="Parent task" name="parentId" select
                disabled={!todoItems || todoItems.length === 0}
                value={todoItem.parentId ? todoItem.parentId : ''} 
                onChange={handleInputChange}>
                
                <MenuItem key={0} value={0}>Select parent task...</MenuItem>
                
                {todoItems.map((todoItem) =>  {
                    if (!todoItem.isCompleted && todoItem.isVisible) {
                        return (
                            <MenuItem key={todoItem.id} value={todoItem.id} sx={{ pl: (todoItem.nestingLevel + 1) * 2}}>
                                {todoItem.title}
                            </MenuItem>
                        )
                    }
                })}
            </TextField>

            <Box
                sx={{ display: 'flex', justifyContent: 'center', m: 1, width: '100%' }}>
                <Button
                    variant="contained"
                    startIcon={<CancelIcon />}
                    onClick={handleCancelEdit}
                    sx={{ mr: 2, width: '100%' }}>
                    Cancel
                </Button>
                <Button
                    variant="contained"
                    startIcon={<AddTaskIcon />}
                    sx={{ width: '100%' }}
                    type="submit">
                    Save
                </Button>
            </Box>
        </Box >
    );
}

export default EditTodoItemForm;