import * as React from 'react';
import { useState, useEffect } from 'react';
import { Table, TableContainer, TableBody, Paper, Box, Fab } from '@mui/material';
import TodoItemsListHeader from './TodoItemsListHeader';
import TodoItemsListItem from './TodoItemsListItem';
import TodoItemsListOptions from './TodoItemsListOptions';
import CreateTodoItemInline from '../Forms/CreateTodoItemInline';
import AddIcon from '@mui/icons-material/Add';

const TodoItemsList = ({ todoItems, onTodoItemDelete, onTodoItemEdit, onTodoItemDone, onAddSubtask, onTodoItemVisibleOnOff, onOpenCreateTodoModal }) => {
    const [displayOptions, setDisplayOptions] = useState({
        isDensePadding: true,
        showFinished: false,
        showHidden: true,
        showSmallMenu: false, 
        isDarkMode: false
    });

    const [parentId, setParentId] = useState(0);

    const handleTodoItemDelete = todoItemId => onTodoItemDelete(todoItemId);
    const handleTodoItemEdit = todoItem => onTodoItemEdit(todoItem);
    const handleTodoItemDone = todoItem => onTodoItemDone(todoItem);
    const handleAddSubtask = todoItem => setParentId(todoItem.id);
    const handleCancelSubtask = () => setParentId(0);
    const handleCreateSubtask = newSubtask => onAddSubtask(newSubtask);
    const handleTodoItemVisibleOnOff = todoItem => onTodoItemVisibleOnOff(todoItem);

    
    const setDisplayOption = (option, value) => {
        setDisplayOptions({ ...displayOptions, [option]: value });

        localStorage.setItem(option, value);
    };

    const handleDensePaddingChecked = isChecked => setDisplayOption('isDensePadding', isChecked);
    const handleShowFinishedChecked = isChecked => setDisplayOption('showFinished', isChecked);
    const handleShowHiddenChecked = isChecked => setDisplayOption('showHidden', isChecked);
    const handleShowSmallMenuChecked = isChecked => setDisplayOption('showSmallMenu', isChecked);

    const renderCreateSubtaskInlineComponent = (todoItem) => {
        if (todoItem.id === parentId) {
            return <CreateTodoItemInline parentTodoItem={todoItem}
                onCreate={handleCreateSubtask} onCancel={handleCancelSubtask} />;
        }
    };

    const isTodoItemVisible = todoItem => {
        // Don't show finished and task is completed - skip
        if (!displayOptions.showFinished && todoItem.isCompleted)
            return false;

        // Don't show hidden and task is hidden - skip
        else if (!displayOptions.showHidden && !todoItem.isVisible)
            return false;

        return true;
    }

    useEffect(() => {
        const isDensePadding = localStorage.getItem('isDensePadding') === "true";
        const showFinished = localStorage.getItem('showFinished') === "true";
        const showHidden = localStorage.getItem('showHidden') === "true";
        const showSmallMenu = localStorage.getItem('smallMenu') === "true";

        setDisplayOptions({ 
            ...displayOptions, 
            ['isDensePadding']: isDensePadding, 
            ['showFinished']: showFinished, 
            ['showHidden']: showHidden,
            ['showSmallMenu']: showSmallMenu 
        });
    }, []);

    return (
        <>
            <TableContainer component={Paper} sx={{ maxHeight: '500px' }}>
                <Table sx={{ }} size={displayOptions.isDensePadding ? "small" : ""} stickyHeader>

                    <TodoItemsListHeader />

                    <TableBody>
                        {todoItems.map((todoItem) => {
                            if (isTodoItemVisible(todoItem)) {
                                return (
                                    <React.Fragment key={'todoItem_' + todoItem.id}>
                                        <TodoItemsListItem
                                            todoItem={todoItem}
                                            onAddSubtaskClicked={handleAddSubtask}
                                            onDeleteClicked={handleTodoItemDelete}
                                            onDoneClicked={handleTodoItemDone}
                                            onEditClicked={handleTodoItemEdit}
                                            onVisibleOnOffClicked={handleTodoItemVisibleOnOff}
                                            
                                            showSmallMenu={displayOptions.showSmallMenu}
                                        />

                                        {renderCreateSubtaskInlineComponent(todoItem)}
                                    </React.Fragment>
                                );
                            }
                        })}
                    </TableBody>
                </Table>
            </TableContainer>

            <Box sx={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', alignItems: '', p: 2 }}>
                <TodoItemsListOptions
                    isDensePadding={displayOptions.isDensePadding}
                    onDensePaddingChecked={handleDensePaddingChecked}
                    showFinished={displayOptions.showFinished}
                    onShowFinishedChecked={handleShowFinishedChecked}
                    showHidden={displayOptions.showHidden}
                    onShowHiddenChecked={handleShowHiddenChecked} 
                    showSmallMenu={displayOptions.showSmallMenu}
                    onSmallMenuChecked={handleShowSmallMenuChecked} />

                <Fab color="primary" aria-label="add" onClick={onOpenCreateTodoModal}>
                    <AddIcon />
                </Fab>
            </Box>
        </>
    );
}

export default TodoItemsList;