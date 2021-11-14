import * as React from 'react';
import { useState } from 'react';
import { Table, TableContainer, TableBody, Paper } from '@mui/material';
import TodoItemsListHeader from './TodoItemsListHeader';
import TodoItemsListItem from './TodoItemsListItem';
import TodoItemsListFooter from './TodoItemsListFooter';
import CreateTodoItemInline from '../Forms/CreateTodoItemInline';

const TodoItemsList = ({ todoItems, onTodoItemDelete, onTodoItemEdit, onTodoItemDone, onAddSubtask, onTodoItemVisibleOnOff }) => {
    const [displayOptions, setDisplayOptions] = useState({
        isDensePadding: true,
        showFinished: false,
        showHidden: true
    });

    const setDisplayOption = (option, value) => setDisplayOptions({ ...displayOptions, [option]: value });
    
    //const [isDensePadding, setIsDensePadding] = useState(true);
    //const [showFinished, setShowFinished] = useState(false);
    //const [showHidden, setShowHidden] = useState(true);
    const [parentId, setParentId] = useState(0);

    const handleTodoItemDelete = todoItemId => onTodoItemDelete(todoItemId);
    const handleTodoItemEdit = todoItem => onTodoItemEdit(todoItem);
    const handleTodoItemDone = todoItem => onTodoItemDone(todoItem);
    const handleAddSubtask = todoItem => setParentId(todoItem.id);
    const handleCancelSubtask = () => setParentId(0);
    const handleCreateSubtask = newSubtask => onAddSubtask(newSubtask);
    const handleTodoItemVisibleOnOff = todoItem => onTodoItemVisibleOnOff(todoItem);

    const handleDensePaddingChecked = isChecked => setDisplayOption('isDensePadding', isChecked);
    const handleShowFinishedChecked = isChecked => setDisplayOption('showFinished', isChecked);
    const handleShowHiddenChecked = isChecked => setDisplayOption('showHidden', isChecked);

    const renderCreateSubtaskInlineComponent = (todoItem) => {
        if (todoItem.id === parentId) {
            return <CreateTodoItemInline parentTodoItem={todoItem} 
                onCreate={handleCreateSubtask} onCancel={handleCancelSubtask} />;
        }
    };

    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 700 }} size={displayOptions.isDensePadding ? "small" : ""} aria-label="TodoItems list">
                <TodoItemsListHeader />

                <TableBody>
                    {todoItems.map((todoItem) => {
                        // Don't show finished and task is completed - skip
                        if (!displayOptions.showFinished && todoItem.isCompleted) {
                        }
                        // Don't show hidden and task is hidden - skip
                        else if (!displayOptions.showHidden && !todoItem.isVisible) {
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
                                        onVisibleOnOffClicked={handleTodoItemVisibleOnOff}
                                    />

                                    {renderCreateSubtaskInlineComponent(todoItem)}
                                </React.Fragment>
                            )
                        }
                    })}
                </TableBody>
            </Table>
            <TodoItemsListFooter
                isDensePadding={displayOptions.isDensePadding}
                onDensePaddingChecked={handleDensePaddingChecked}
                showFinished={displayOptions.showFinished}
                onShowFinishedChecked={handleShowFinishedChecked}
                showHidden={displayOptions.showHidden}
                onShowHiddenChecked={handleShowHiddenChecked} />
        </TableContainer>
    );
}

export default TodoItemsList;