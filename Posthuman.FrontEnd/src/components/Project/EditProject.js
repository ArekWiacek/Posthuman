import * as React from 'react';
import { Box, TextField, Button, Stack, Typography } from '@mui/material';
import DesktopDatePicker from '@mui/lab/DesktopDatePicker';
import CheckIcon from '@mui/icons-material/Check';
import CancelIcon from '@mui/icons-material/Cancel';
import { LogW } from '../../Utilities/Utilities';

const EditProject = ({ currentProject, onSaveChanges, onCancelEdit }) => {
    const [project, setProject] = React.useState({ ...currentProject });

    const handleTitleChange = event => setProject({ ...project, title: event.target.value });
    const handleDescriptionChange = event => setProject({ ...project, description: event.target.value });
    const handleStartDateChange = newValue => setProject({ ...project, startDate: newValue });
    const handleCancelEdit = (e) => onCancelEdit();

    const handleSubmit = (e) => {
        e.preventDefault();

        if (project.title === "") {
            LogW("Cannot save Project - Title not provided");
            return;
        }

        if (project.avatarId == null || project.avatarId == 0) {
            LogW("Cannot save Project - active Avatar unknown");
            return;
        }

        onSaveChanges(project.id, project);

        setProject({ ...project });
    }


    return (
        <Box
            component="form" noValidate autoComplete="off"
            onSubmit={e => handleSubmit(e)}>
            <Stack
                direction="column"
                justifyContent="center"
                alignItems="stretch"
                spacing={1}>
                <Typography variant="h5">Edit project</Typography>
                <TextField
                    id="title"
                    label="Title"
                    variant="outlined"
                    value={project.title}
                    onChange={handleTitleChange}
                    required />
                <TextField
                    id="description"
                    label="Description"
                    multiline
                    rows={3}
                    value={project.description}
                    onChange={handleDescriptionChange} />
                <DesktopDatePicker
                    label="Start date"
                    inputFormat="DD.MM.YYYY"
                    mask="__.__.____"
                    value={project.startDate}
                    required
                    onChange={handleStartDateChange}
                    renderInput={(params) => <TextField {...params} />} />
                <Box
                    sx={{
                        display: 'flex',
                        justifyContent: 'flex-end',
                        p: 1,
                        m: 1
                    }}>
                    <Button
                        sx={{ mr: 1 }}
                        variant="contained"
                        startIcon={<CancelIcon />}
                        onClick={handleCancelEdit}>
                        Cancel
                    </Button>
                    <Button
                        variant="contained"
                        startIcon={<CheckIcon />}
                        type="submit">
                        Save changes
                    </Button>
                </Box>
            </Stack>
        </Box>
    );
}

export default EditProject;