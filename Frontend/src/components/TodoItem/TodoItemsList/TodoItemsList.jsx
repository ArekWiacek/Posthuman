import React, { useState } from 'react';
import { Table, TableContainer, TableBody, Paper, Box, Fab, Slider } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import TodoItemsListHeader from './TodoItemsListHeader';
import TodoItemsListItem from './TodoItemsListItem';
import TodoItemsListOptions from './TodoItemsListOptions';
import CreateTodoItemInline from '../Forms/CreateTodoItemInline';
import { LogT, LogI } from '../../../Utilities/Utilities';
import { Scrollbar } from "react-scrollbars-custom";
import useDisplayOptions from '../../../Hooks/useDisplayOptions';

const TodoItemsList = ({ todoItems, onTodoItemDelete, onTodoItemEdit, onTodoItemDone, onAddSubtask, onTodoItemVisibleOnOff, onOpenCreateTodoModal }) => {
    const [parentId, setParentId] = useState(0);

    const handleTodoItemDelete = todoItemId => onTodoItemDelete(todoItemId);
    const handleTodoItemEdit = todoItem => onTodoItemEdit(todoItem);
    const handleTodoItemDone = todoItem => onTodoItemDone(todoItem);
    const handleAddSubtask = todoItem => setParentId(todoItem.id);
    const handleCancelSubtask = () => setParentId(0);
    const handleCreateSubtask = newSubtask => onAddSubtask(newSubtask);
    const handleTodoItemVisibleOnOff = todoItem => onTodoItemVisibleOnOff(todoItem);

    const [displayOptions, setDisplayOptions] = useDisplayOptions();
    const handleDisplayOptionsChanged = (option, value) => setDisplayOptions({ ...displayOptions, [option]: value });

    const renderCreateSubtaskInlineComponent = (todoItem) => {
        if (todoItem.id === parentId) {
            return <CreateTodoItemInline
                parentTodoItem={todoItem}
                onCreate={handleCreateSubtask}
                onCancel={handleCancelSubtask} />;
        }
    };

    const isTodoItemVisible = todoItem => {
        // Don't show finished and task is completed - skip
        if (!displayOptions.showFinishedTasks && todoItem.isCompleted)
            return false;

        // Don't show hidden and task is hidden - skip
        else if (!displayOptions.showHiddenTasks && !todoItem.isVisible)
            return false;

        return true;
    };

    return (
        <React.Fragment>
            <Scrollbar style={{ height: 450 }} id='todoListScrollbar'>
                <TableContainer component={Paper} sx={{}}>
                    <Table sx={{}} size={displayOptions.bigItems ? '' : 'small'} stickyHeader>

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

                                                collapseActionButtons={displayOptions.collapsedMenu}
                                            />

                                            {renderCreateSubtaskInlineComponent(todoItem)}
                                        </React.Fragment>
                                    );
                                }
                            })}
                        </TableBody>
                    </Table>
                </TableContainer>
            </Scrollbar>

            <Box sx={{ display: 'flex', flexDirection: 'row', justifyContent: 'space-between', alignItems: '', p: 2 }}>
                <TodoItemsListOptions
                    listDisplayOptions={displayOptions}
                    onDisplayOptionsChanged={handleDisplayOptionsChanged} />

                <Fab color="primary" aria-label="add" onClick={onOpenCreateTodoModal}>
                    <AddIcon />
                </Fab>
            </Box>
        </React.Fragment>
    );
}

export default TodoItemsList;