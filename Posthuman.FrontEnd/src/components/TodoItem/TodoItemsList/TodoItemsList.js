import * as React from 'react';
import { Table, TableContainer, TableBody, Paper } from '@mui/material';
import TodoItemsListHeader from './TodoItemsListHeader';
import TodoItemsListItem from './TodoItemsListItem';
import TodoItemsListFooter from './TodoItemsListFooter';
import CreateTodoItemInline from '../CreateTodoItemInline';

// import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
// import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';

const TodoItemsList = ({ todoItems, onTodoItemDelete, onTodoItemEdit, onTodoItemDone, onAddSubtask }) => {
    const [isDensePadding, setIsDensePadding] = React.useState(true);
    const [showFinished, setShowFinished] = React.useState(false);
    const [parentId, setParentId] = React.useState(0);
    
    const handleTodoItemDelete = todoItemId => onTodoItemDelete(todoItemId);
    const handleTodoItemEdit = todoItem => onTodoItemEdit(todoItem);
    const handleTodoItemDone = todoItem => onTodoItemDone(todoItem);
    const handleAddSubtask = todoItem => setParentId(todoItem.id);
    const handleDiscardSubtask = () => setParentId(0);
    const handleCreateSubtask = newSubtask => onAddSubtask(newSubtask);

    const handleDensePaddingChecked = (isChecked) => setIsDensePadding(isChecked);
    const handleShowFinishedChecked = (isChecked) => setShowFinished(isChecked);
    
    const renderCreateSubtaskInlineComponent = (todoItem) => {
        if (todoItem.id == parentId) {
            return <CreateTodoItemInline parentTodoItem={todoItem} onCreate={handleCreateSubtask} onDiscard={handleDiscardSubtask} />;
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
                                        onDeleteClicked={handleTodoItemDelete}
                                        onDoneClicked={handleTodoItemDone}
                                        onEditClicked={handleTodoItemEdit}
                                    />

                                    {renderCreateSubtaskInlineComponent(todoItem)}
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