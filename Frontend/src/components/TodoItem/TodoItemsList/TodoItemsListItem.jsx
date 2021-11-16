import * as React from 'react';
import { useState } from 'react';
import { TableRow, TableCell, Typography, Button, Anchor, } from '@mui/material';
import Deadline from '../../Common/Deadline';
import TodoItemListItemActionButtons from './TodoItemsListItemActionButtons';
import TodoItemsListItemMenu from './TodoItemsListItemMenu';
import { LogI } from '../../../Utilities/Utilities';

const TodoItemListItem = ({ todoItem, onDeleteClicked, onEditClicked, onDoneClicked, onAddSubtaskClicked, onVisibleOnOffClicked }) => {
    const [showActionButtons, setShowActionButtons] = useState(false);

    const handleDeleteClicked = todoItem => onDeleteClicked(todoItem.id);
    const handleEditClicked = todoItem => onEditClicked(todoItem);
    const handleDoneClicked = todoItem => onDoneClicked(todoItem);
    const handleAddSubtaskClicked = todoItem => onAddSubtaskClicked(todoItem);
    const handleVisibleOnOffClicked = todoItem => onVisibleOnOffClicked(todoItem);

    const createProgressText = todoItem => {
        var progressText = "";

        if (todoItem.hasSubtasks) {
            var progressPercentage = Math.round((todoItem.finishedSubtasksCount / todoItem.subtasksCount) * 100);
            progressText = todoItem.finishedSubtasksCount + " / " + todoItem.subtasksCount + " (" + progressPercentage + "%)";
        }
        else 
            todoItem.isCompleted ? progressText = "Done!" : progressText = "Not completed";

        return progressText;
    };

    const getFontSizeForTodoItemTitle = nestingLevel => {
        var titleSizes = { 0: '1.2rem', 1: '1.1rem', 2: '1rem' };
        if (nestingLevel < 2) {
            return titleSizes[nestingLevel];
        } else {
            return '0.9rem';
        }
    };

    const getDeadlineComponent = todoItem => {
        if (!todoItem.isCompleted)
            return <Deadline when={todoItem.deadline} />;
        else
            return '';
    };

    return (
        <TableRow hover sx={{ opacity: todoItem.isVisible ? '100%' : '30%' }}
            onMouseEnter={() => setShowActionButtons(true)}
            onMouseLeave={() => setShowActionButtons(false)}>

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

            <TableCell align="left" sx={{ width: '120px' }}>
                {getDeadlineComponent(todoItem)}
            </TableCell>

            <TableCell align="right" sx={{ width: '140px', color: todoItem.isCompleted ? 'success.main' : '' }}>
                {createProgressText(todoItem)}
            </TableCell>

            <TableCell align="right" sx={{ width: '250px' }}>
                {/* <TodoItemsListItemMenu /> */}
                <TodoItemListItemActionButtons 
                    todoItem={todoItem}
                    onDeleteClicked={handleDeleteClicked} 
                    onEditClicked={handleEditClicked}
                    onDoneClicked={handleDoneClicked}
                    onAddSubtaskClicked={handleAddSubtaskClicked} 
                    onVisibleOnOffClicked={handleVisibleOnOffClicked}
                    isVisible={showActionButtons && !todoItem.isCompleted} />
            </TableCell>
        </TableRow>
    );
}

export default TodoItemListItem;