import * as React from 'react';
import { Button, Box, Typography, Container, Paper, Stack, IconButton,  } from '@mui/material';
import { makeStyles } from '@mui/styles';

import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';

const useStyles = makeStyles(theme => ({
    rowItem: {
        textAlign: 'left',
        maxWidth: '100%'
        // padding: theme.spacing(3)
    }
}))

const TodoItemRow = ({ todoItem }) => {
    const classes = useStyles();

    const handleAddSubtaskClicked = () => {}
    const handleDeleteTodoItemClicked = () => {}
    const handleEditTodoItemClicked = () => {}
    const handleTodoItemDoneClicked = () => {}

    return (
        <Container key={todoItem.id} className={classes.rowItem} sx={{}}>
            <Typography variant="h6">{todoItem.title}</Typography>

            <Box
                sx={{
                    display: 'flex',
                    justifyContent: 'flex-end',
                    p: 1,
                    m: 1,
                    bgcolor: 'background.paper',
                }}>
                <Button variant="outlined" startIcon={<AddIcon />}
                    onClick={() => handleAddSubtaskClicked()} />

                <Button variant="outlined" startIcon={<DeleteIcon />}
                    onClick={() => handleDeleteTodoItemClicked()} />

                <Button variant="outlined" startIcon={<EditIcon />}
                    onClick={() => handleEditTodoItemClicked()} />
                {/* 
                <Button variant="outlined" startIcon={<CheckCircleIcon />}
                    onClick={() => handleTodoItemDoneClicked()} /> */}
            </Box>

            <Stack direction="row" spacing={1}>
                <IconButton aria-label="delete">
                    <AddIcon />
                </IconButton>
                <IconButton aria-label="delete" disabled color="primary">
                    <DeleteIcon />
                </IconButton>
                <IconButton color="secondary" aria-label="add an alarm">
                    <EditIcon />
                </IconButton>
                <IconButton color="primary" aria-label="add to shopping cart">
                    <CheckCircleIcon />
                </IconButton>
                </Stack>
        </Container>
    )
}

export default TodoItemRow;