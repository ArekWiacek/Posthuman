import * as React from 'react';
import { TableRow, TableCell, Typography } from '@mui/material';
import Deadline from '../../Common/Deadline';
import TodoItemListItemActionButtons from './TodoItemsListItemActionButtons';

const TodoItemListItem = ({ todoItem, onDeleteClicked, onEditClicked, onDoneClicked, onAddSubtaskClicked, onVisibleOnOffClicked }) => {
    const handleDeleteClicked = todoItem => onDeleteClicked(todoItem.id);
    const handleEditClicked = todoItem => onEditClicked(todoItem);
    const handleDoneClicked = todoItem => onDoneClicked(todoItem);
    const handleAddSubtaskClicked = todoItem => onAddSubtaskClicked(todoItem);
    const handleVisibleOnOffClicked = todoItem => onVisibleOnOffClicked(todoItem);

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
    };

    function getFontSizeForTodoItemTitle(nestingLevel) {
        var titleSizes = { 0: '1.2rem', 1: '1.1rem', 2: '1rem' };
        if (nestingLevel < 2) {
            return titleSizes[nestingLevel];
        } else {
            return '0.9rem';
        }
    };

    return (
        <TableRow sx={{ opacity: todoItem.isVisible ? '100%' : '30%' }}>
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

            <TableCell align="left" sx={{ width: '250px' }}>
                <Deadline when={todoItem.deadline} />
            </TableCell>

            <TableCell align="right" sx={{ width: '150px' }}>
                {createProgressText(todoItem)}
            </TableCell>

            <TableCell align="right" sx={{ width: '250px' }}>
                <TodoItemListItemActionButtons 
                    todoItem={todoItem}
                    onDeleteClicked={handleDeleteClicked} 
                    onEditClicked={handleEditClicked}
                    onDoneClicked={handleDoneClicked}
                    onAddSubtaskClicked={handleAddSubtaskClicked} 
                    onVisibleOnOffClicked={handleVisibleOnOffClicked} />
            </TableCell>
        </TableRow>
    );
}

export default TodoItemListItem;