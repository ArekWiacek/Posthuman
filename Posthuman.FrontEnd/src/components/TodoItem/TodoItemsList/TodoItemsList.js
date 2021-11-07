import * as React from 'react';
import { Table, TableContainer, TableBody, Paper } from '@mui/material';
import TodoItemsListHeader from './TodoItemsListHeader';
import TodoItemsListItem from './TodoItemsListItem';
import TodoItemsListFooter from './TodoItemsListFooter';
import CreateTodoItemInline from './CreateTodoItemInline';

const TodoItemsList = ({ todoItems, onTodoItemDeleted, onTodoItemEdited, onTodoItemDone, onAddSubtask }) => {
    const [isDensePadding, setIsDensePadding] = React.useState(true);
    const [showFinished, setShowFinished] = React.useState(false);

    const [parentId, setParentId] = React.useState(0);
    
    const handleTodoItemDeleted = todoItemId => onTodoItemDeleted(todoItemId);
    const handleTodoItemEdit = todoItem => onTodoItemEdited(todoItem);
    const handleTodoItemDone = todoItem => onTodoItemDone(todoItem);

    const handleAddSubtask = todoItem => setParentId(todoItem.id); //todoItem => onAddSubtask(todoItem);

    const handleDiscardSubtask = () => {
        setParentId(0);
    };

    const handleCreateSubtask = newSubtask => {
        onAddSubtask(newSubtask);
        setParentId(0);
    };

    const handleDensePaddingChecked = (isChecked) => {
        setIsDensePadding(isChecked);
    };

    const handleShowFinishedChecked = (isChecked) => {
        setShowFinished(isChecked);
    };

    const renderCreateSubtaskInline = (todoItem) => {
        if (todoItem.id == parentId) {
            return <CreateTodoItemInline key={'createSubtaskFor_' + todoItem.id} parentTask={todoItem} onCreate={handleCreateSubtask} onDiscard={handleDiscardSubtask} />;
        }
    };

    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 700 }} size={isDensePadding ? "small" : ""} aria-label="TodoItems list">
                <TodoItemsListHeader />

                <TableBody>
                    {todoItems.map((todoItem) => {
                        if (!showFinished && todoItem.isCompleted) {

                        }
                        else {
                            return (
                                <React.Fragment key={'todoItem_' + todoItem.id}>
                                    <TodoItemsListItem
                                        todoItem={todoItem}
                                        onAddSubtaskClicked={handleAddSubtask}
                                        onDeleteClicked={handleTodoItemDeleted}
                                        onDoneClicked={handleTodoItemDone}
                                        onEditClicked={handleTodoItemEdit}
                                    />

                                    {renderCreateSubtaskInline(todoItem)}
                                </React.Fragment>
                            )
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