import * as React from 'react';
import { Table, TableContainer, TableBody, Paper } from '@mui/material';
import TodoItemsListHeader from './TodoItemsListHeader';
import TodoItemsListItem from './TodoItemsListItem';
import TodoItemsListFooter from './TodoItemsListFooter';

const TodoItemsList = ({ todoItems, onTodoItemDeleted, onTodoItemEdited, onTodoItemDone, onAddSubtask }) => {
    const [isDensePadding, setIsDensePadding] = React.useState(true);
    const [showFinished, setShowFinished] = React.useState(false);

    const handleTodoItemDeleted = todoItemId => onTodoItemDeleted(todoItemId);
    const handleTodoItemEdit = todoItem => onTodoItemEdited(todoItem);
    const handleTodoItemDone = todoItem => onTodoItemDone(todoItem);
    const handleAddSubtask = todoItem => onAddSubtask(todoItem);

    const handleDensePaddingChecked = (isChecked) => {
        setIsDensePadding(isChecked);
    };

    const handleShowFinishedChecked = (isChecked) => {
        setShowFinished(isChecked);
    };

    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 700 }} size={isDensePadding ? "small" : ""} aria-label="TodoItems list">
                <TodoItemsListHeader />

                <TableBody>
                    {todoItems.map((todoItem) => {
                        if(!showFinished && todoItem.isCompleted) {

                        }
                        else {                        
                            return (<TodoItemsListItem
                                key={todoItem.id}
                                todoItem={todoItem}
                                onAddSubtaskClicked={handleAddSubtask}
                                onDeleteClicked={handleTodoItemDeleted}
                                onDoneClicked={handleTodoItemDone}
                                onEditClicked={handleTodoItemEdit}
                            />)
                        }
                    })}
                </TableBody>
            </Table>
            <TodoItemsListFooter 
                    isDensePadding={isDensePadding} 
                    onDensePaddingChecked={handleDensePaddingChecked}
                    showFinished={showFinished}
                    onShowFinishedChecked={handleShowFinishedChecked} />
        </TableContainer>
    );
}

export default TodoItemsList;