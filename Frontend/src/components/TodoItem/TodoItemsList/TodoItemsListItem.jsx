import * as React from 'react';
import { useState } from 'react';
import { TableRow, TableCell, Typography, Button, Anchor, } from '@mui/material';
import Deadline from '../../Common/Deadline';
import TodoItemActions from '../Actions/TodoItemActions';
import { LogI } from '../../../Utilities/Utilities';
import useWindowDimensions from '../../../Hooks/useWindowDimensions';

const TodoItemListItem = ({ todoItem, showSmallMenu, onDeleteClicked, onEditClicked, onDoneClicked, onAddSubtaskClicked, onVisibleOnOffClicked }) => {
    const [isHover, setIsHover] = useState(false);

    const { height, width, isXs, isSm, isMd, isLg, isXl } = useWindowDimensions();

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
            onMouseEnter={() => setIsHover(true)}
            onMouseLeave={() => setIsHover(false)}>

            <TableCell component="th" scope="row" sx={{ width: '100%' }}>
                <Typography
                    component="span"
                    sx={{
                        textDecoration: todoItem.isCompleted ? 'line-through' : 'none',
                        paddingLeft: todoItem.nestingLevel * 2,
                        fontSize: getFontSizeForTodoItemTitle(todoItem.nestingLevel)
                    }}>
                    {todoItem.title}
                    
                    {/* Dimensions - h: {height}, w: {width}, 
                    isXs: {isXs() ? 'yes' : 'no'}, 
                    isSm: {isSm() ? 'yes' : 'no'},
                    isMd: {isMd() ? 'yes' : 'no'},
                    isLg: {isLg() ? 'yes' : 'no'},
                    isXl: {isXl() ? 'yes' : 'no'} */}
                </Typography>
            </TableCell>

            <TableCell align="left" sx={{ 
                display: { xs: 'none', md: 'table-cell' },
                minWidth: '120px'
            }}>
                {getDeadlineComponent(todoItem)}
            </TableCell>

            <TableCell align="right" sx={{ 
                display: { xs: 'none', lg: 'table-cell' }, 
                minWidth: '140px', 
                color: todoItem.isCompleted ? 'success.main' : '' }}>
                {createProgressText(todoItem)}
            </TableCell>

            <TableCell align="right">
                <TodoItemActions 
                    isHover={isHover}
                    showSmallMenu={isMd() || showSmallMenu}
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