import * as React from 'react';
import { Typography, Box, Button,  } from '@mui/material';
import ModalWindow from '../../Modal/ModalWindow';
import DeleteIcon from '@mui/icons-material/Delete';
import CancelIcon from '@mui/icons-material/Cancel';

const DeleteTodoItemModal = ({ isOpen, todoItemToDelete, onDelete, onCancelDelete, onClose }) => {
    const getUnfinishedSubtasksCount = (todoItem) => {
        return todoItem.subtasksCount - todoItem.finishedSubtasksCount;;
    };

    const getSubtasksInfoText = () => {
        let todo = todoItemToDelete;
        if(!todo.hasSubtasks)
            return '';

        let text = `Task has ${todoItemToDelete.subtasksCount} subtasks`;
        let unfinishedCount = getUnfinishedSubtasksCount(todo);
        if(unfinishedCount > 0) {
            text += ` (${unfinishedCount} unfinished)`;
        }

        return text;
    };

    return (
        <ModalWindow
            isOpen={isOpen}
            onClose={onClose}
            sx={{ width: '500px', textAlign: 'center' }}
            background={{ backgroundPosition: 'right top' }}
            eyecandy='eyecandy2'>
            <Typography variant="h5">Confirm deletion</Typography>
            <Typography variant="body" sx={{ pb: 1 }}>Are you sure you want to delete task: '{todoItemToDelete.title}'?</Typography>

            <Typography variant="body2" sx={{ pb: 1 }}>{getSubtasksInfoText()}</Typography>

            <Box sx={{ display: 'flex', justifyContent: 'center', m: 1, width: '100%' }}>
                <Button
                    variant="contained"
                    startIcon={<CancelIcon />}
                    onClick={onCancelDelete}
                    sx={{ mr: 2, width: '100%' }}>
                    No
                </Button>
                <Button
                    variant="contained"
                    startIcon={<DeleteIcon />}
                    sx={{ width: '100%' }}
                    onClick={() => onDelete(todoItemToDelete)}>
                    Yes
                </Button>
            </Box>
        </ModalWindow>
    );
}

export default DeleteTodoItemModal;