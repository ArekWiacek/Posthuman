import * as React from 'react';
import { TableRow, TableCell, Typography, IconButton } from '@mui/material';
import { makeStyles } from '@mui/styles';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import AddIcon from '@mui/icons-material/Add';
import moment from 'moment';

// import Moment from 'react-moment';

// const renderCreateSubtaskInlineComponent = (date) => {
//     if (date != null && date != undefined) {
//         return <Moment format="DD.MM.YYYY">{date}</Moment>;
//     } else {
//         return <div>'guuuuuuuuuuuuuuffj'</div>;
//     }
// };


const TodoItemListItem = ({ todoItem, onDeleteClicked, onEditClicked, onDoneClicked, onAddSubtaskClicked }) => {
    const handleDeleteClicked = todoItemId => onDeleteClicked(todoItemId);
    const handleEditClicked = todoItem => onEditClicked(todoItem);
    const handleDoneClicked = todoItem => onDoneClicked(todoItem);
    const handleAddSubtaskClicked = todoItem => onAddSubtaskClicked(todoItem);

    const createRewardDisplayText = () => { return "+20XP"; }

    const createParentProjectDisplayText = (todoItem) => {
        return todoItem.projectId ? ("ID: " + todoItem.projectId) : "-";
    }

    const createProgressText = (todoItem) => {
        var progressText = "";

        if (todoItem.hasSubtasks) {
            var progressPercentage = Math.round((todoItem.finishedSubtasksCount / todoItem.subtasksCount) * 100);
            progressText = todoItem.finishedSubtasksCount + " / " + todoItem.subtasksCount + " (" + progressPercentage + "%)";
        }
        else {
            todoItem.isCompleted ? progressText = "Done!" : progressText = "Not completed";
        }

        return progressText;
    }

    
    function getFontSizeForTodoItemTitle(nestingLevel) { 
         var titleSizes = { 0: '1.3rem', 1: '1.2rem', 2: '1.1rem' };
         if(nestingLevel < 2) {
            return titleSizes[nestingLevel];
        } else {
            return '1rem';
        }
    }
    
    return (
        <TableRow sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
            <TableCell component="th" scope="row" >
                <Typography
                    component="span"
                    sx={{
                        textDecoration: todoItem.isCompleted ? 'line-through' : 'none',
                        paddingLeft: todoItem.nestingLevel * 2,
                        fontSize: getFontSizeForTodoItemTitle(todoItem.nestingLevel)
                    }}>
                    {todoItem.title}
                </Typography>
            </TableCell>
            <TableCell align="left">
                {/* <Moment format="DD.MM.YYYY">{todoItem.deadline}</Moment> */}
            </TableCell>
            {/* <TableCell align="left">
                <Moment format="dddd">{todoItem.deadline}</Moment>
            </TableCell>
            <TableCell align="right">
                {createRewardDisplayText(todoItem)}
            </TableCell>
            <TableCell align="right">
                {createParentProjectDisplayText(todoItem)}
            </TableCell>
            <TableCell align="right">
                {todoItem.parentId != null ? todoItem.parentId : "-"}
            </TableCell> */}
            <TableCell align="right">
                {createProgressText(todoItem)}
            </TableCell>
            <TableCell align="right">
                <IconButton
                    aria-label="add-subtask"
                    onClick={() => handleAddSubtaskClicked(todoItem)}
                    disabled={todoItem.isCompleted}>
                    <AddIcon />
                </IconButton>
                <IconButton
                    aria-label="delete-todoitem"
                    onClick={() => handleDeleteClicked(todoItem.id)}
                    disabled={todoItem.isCompleted}>
                    <DeleteIcon />
                </IconButton>
                <IconButton
                    aria-label="edit-subtask"
                    onClick={() => handleEditClicked(todoItem)}
                    disabled={todoItem.isCompleted}>
                    <EditIcon />
                </IconButton>
                <IconButton
                    aria-label="todoitem-done"
                    onClick={() => handleDoneClicked(todoItem)}
                    disabled={todoItem.isCompleted || todoItem.hasUnfinishedSubtasks}>
                    <CheckCircleIcon />
                </IconButton>
            </TableCell>
        </TableRow>
    );
}

export default TodoItemListItem;