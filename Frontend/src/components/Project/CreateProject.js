import * as React from 'react';
import { Box, TextField, Button, Typography } from '@mui/material';
import Stack from '@mui/material/Stack';
import DesktopDatePicker from '@mui/lab/DesktopDatePicker';
import AddIcon from '@mui/icons-material/Add';

import { AvatarContext } from "../../App";
import { LogW } from '../../Utilities/Utilities';


const CreateProject = ({ onCreateProject }) => {
    const { activeAvatar } = React.useContext(AvatarContext);
    const [title, setTitle] = React.useState("");
    const [description, setDescription] = React.useState("");
    const [startDate, setStartDate] = React.useState(new Date());

    const handleTitleChange = event => setTitle(event.target.value);
    const handleDescriptionChange = event => setDescription(event.target.value);
    const handleStartDateChange = newValue => setStartDate(newValue);

    const handleSubmit = (e) => {
        e.preventDefault();

        if (title === "") {
            LogW("Cannot create project - title not provided");
            return;
        }

        if (activeAvatar == null || activeAvatar.id == 0) {
            LogW("Cannot create Project - active avatar unknown");
            return;
        }

        const newProject = {
            title: title,
            description: description,
            startDate: startDate,
            avatarId: activeAvatar.id
        };

        onCreateProject(newProject);

        setTitle("");
        setDescription("");
        setStartDate(new Date());
    }

    return (
        <Box
            component="form" noValidate autoComplete="off" onSubmit={e => handleSubmit(e)}>
            <Stack
                direction="column"
                justifyContent="center"
                alignItems="stretch"
                spacing={1}>
                <Typography variant="h5">Create project</Typography>
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
                    label="Start date"
                    inputFormat="DD.MM.YYYY"
                    mask="__.__.____"
                    value={startDate}
                    required
                    onChange={handleStartDateChange}
                    renderInput={(params) => <TextField {...params} />} />
                <Button
                    sx={{ m: 1, width: '30ch' }}
                    variant="contained"
                    startIcon={<AddIcon />}
                    type="submit">
                    Create project
                </Button>
            </Stack>
        </Box>
    );
}

export default CreateProject;