import * as React from 'react';
import { useState, useEffect, useContext } from 'react';
import { Paper, Grid, Button } from '@mui/material';

import { AvatarContext } from "../App";
import { CreateDummyTodoItems, CreateDummyProjects } from '../Utilities/DummyObjects';

import TodoItemList from '../components/TodoItem/TodoItemsList/TodoItemsList';
import EditTodoItemForm from '../components/TodoItem/Forms/EditTodoItemForm';
import CreateTodoItemForm from '../components/TodoItem/Forms/CreateTodoItemForm';
import ConfirmTodoItemDoneModal from '../components/TodoItem/Modals/ConfirmTodoItemDoneModal';
import CreateTodoItemModal from '../components/TodoItem/Modals/CreateTodoItemModal';
import EditTodoItemModal from '../components/TodoItem/Modals/EditTodoItemModal';

import Api from '../Utilities/ApiHelper';
import * as ArrayHelper from '../Utilities/ArrayHelper';

function todoItemFormInitialValues() {
    return {
        title: "",
        description: "",
        isCompleted: false,
        deadline: new Date(),
        projectId: null,
        avatarId: null,
        parentId: null
    }
}

const TodoPage = () => {
    const todoItemsEndpointName = "TodoItems";

    const { activeAvatar } = useContext(AvatarContext);
    const [todoItems, setTodoItems] = useState(CreateDummyTodoItems(3));
    const [projects, setProjects] = useState(CreateDummyProjects(2));
    const [currentTodoItem, setCurrentTodoItem] = useState(todoItemFormInitialValues());
    const [isTodoItemInEditMode, setIsTodoItemInEditMode] = useState(false);
    const [parentId, setParentId] = useState(0);

    const [todoItemToBeCompleted, setTodoItemToBeCompleted] = useState(todoItemFormInitialValues())
    const [todoCompleteConfirmationModalVisible, setTodoCompleteConfirmationModalVisible] = useState(false);

    const [createTodoItemModalVisible, setCreateTodoItemModalVisible] = useState(false);
    const [editTodoItemModalVisible, setEditTodoItemModalVisible] = useState(false);
    
    // CREATE TODO ITEM
    const handleCreateTodoItem = (todoItemToCreate) => {
        Api.Post(todoItemsEndpointName, todoItemToCreate, (createdTodoItem) => {
            var newArray = ArrayHelper.InsertObject(todoItems, createdTodoItem);
            setTodoItems(newArray);
        });

        setCreateTodoItemModalVisible(false);
    }

    // CREATE SUBTASK
    const handleSubtaskCreate = (subtaskToCreate) => {
        Api.Post(todoItemsEndpointName, subtaskToCreate, (createdSubtask) => {
            var parentTodoItemIndex = ArrayHelper.FindObjectIndexByProperty(todoItems, "id", subtaskToCreate.parentId);
            // Todo - find out why not calculating correctly
            createdSubtask.nestingLevel = subtaskToCreate.nestingLevel;
            var newArray = ArrayHelper.InsertObjectAtIndex(todoItems, createdSubtask, parentTodoItemIndex + 1);
            setTodoItems(newArray);
        });
    }

    // DELETE TODO ITEM (with subtasks)
    const handleTodoItemDelete = (todoItemToDeleteId) => {
        Api.Delete(todoItemsEndpointName, todoItemToDeleteId, () => {
            setTodoItems(ArrayHelper.RemoveObjectFromArray(todoItems, "id", todoItemToDeleteId));
        });
    }

    // UPDATE TODO ITEM (after edition)
    const handleTodoItemSaveChanges = (editedTodoItemId, editedTodoItem) => {
        Api.Put(todoItemsEndpointName, editedTodoItemId, editedTodoItem, () => {
            setTodoItems(ArrayHelper.ReplaceObjectInArray(todoItems, editedTodoItem, "id", editedTodoItemId));
            setIsTodoItemInEditMode(false);
            setCurrentTodoItem(todoItemFormInitialValues());
        });

        closeEditTodoItemModal();
    }

    // EDIT TODO ITEM CLICKED - turn on edit mode 
    const handleTodoItemEdit = (todoItemToEdit) => {
        setIsTodoItemInEditMode(true);
        setCurrentTodoItem(todoItemToEdit);
        openEditTodoItemModal();
    }

    // EDITING CANCELLED
    const handleTodoItemCancelEdit = () => {
        setIsTodoItemInEditMode(false);
        setCurrentTodoItem(todoItemFormInitialValues());
    }

    // Complete task window opened
    const handleTodoItemCompleted = (todoItemToBeCompleted) => {
        const todoItem = { ...todoItemToBeCompleted };
        setTodoItemToBeCompleted(todoItem);
        setTodoCompleteConfirmationModalVisible(true);
    }
    
    // Complete task window discarded
    const handleTodoItemCompleteDiscarded = () => {
        setTodoCompleteConfirmationModalVisible(false);
    }

    // Complete task window confirmation
    const handleTodoItemCompleteConfirmed = () => {
        var completedTodoItem = ArrayHelper.UpdateObjectProperty(todoItemToBeCompleted, "isCompleted", true);
        Api.Put(todoItemsEndpointName, completedTodoItem.id, completedTodoItem, () => {
            setTodoItems(ArrayHelper.ReplaceObjectInArray(todoItems, completedTodoItem, "id", completedTodoItem.id));
        });

        setTodoCompleteConfirmationModalVisible(false);
    }

    const openCreateTodoItemModal = () => setCreateTodoItemModalVisible(true);
    const closeCreateTodoItemModal = () => setCreateTodoItemModalVisible(false);

    const openEditTodoItemModal = () => setEditTodoItemModalVisible(true);
    const closeEditTodoItemModal = () => setEditTodoItemModalVisible(false);

    useEffect(() => {
        Api.Get("Projects", projects => setProjects(projects));
    }, [activeAvatar]);

    useEffect(() => {
        Api.Get(todoItemsEndpointName + "/Hierarchical", todoItems => { 
            console.log(todoItems);
            setTodoItems(todoItems);
        });
    }, [activeAvatar]);

    return (
      <Grid container spacing={3}>
            {/* LIST VIEW */}
            <Grid item xs={12}>
                <TodoItemList
                    todoItems={todoItems}
                    onTodoItemDelete={handleTodoItemDelete}
                    onTodoItemEdit={handleTodoItemEdit}
                    onTodoItemDone={handleTodoItemCompleted}
                    onAddSubtask={handleSubtaskCreate}
                />
            </Grid>

            {/* CREATE / EDIT */}
            <Grid item xs={3}>
                <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
                    {
                        isTodoItemInEditMode ? (
                            <EditTodoItemForm
                                onSaveEdit={handleTodoItemSaveChanges}
                                onCancelEdit={handleTodoItemCancelEdit}
                                currentTodoItem={currentTodoItem}
                                projects={projects}
                                todoItems={todoItems} />
                        ) : (
                            <CreateTodoItemForm
                                onCreateTodoItem={handleCreateTodoItem}
                                projects={projects}
                                todoItems={todoItems}
                                parentTaskId={parentId} />)
                    }
                </Paper>
            </Grid>

            <Button onClick={openCreateTodoItemModal}>Otfjeraj</Button>

            {/* TodoItem done confirmation modal */}
            <ConfirmTodoItemDoneModal
                isOpen={todoCompleteConfirmationModalVisible}
                onFinished={handleTodoItemCompleteConfirmed}
                onCanceled={handleTodoItemCompleteDiscarded}
                todoItem={todoItemToBeCompleted}
                xpGained={25} />

            {/* Create TodoItem modal  */}
            <CreateTodoItemModal 
                isOpen={createTodoItemModalVisible}
                todoItems={todoItems}
                projects={projects}
                parentTaskId={parentId}
                onClose={closeCreateTodoItemModal}
                onCreateTodoItem={handleCreateTodoItem}
            />

            {/* Edit TodoItem modal */}
            <EditTodoItemModal
                isOpen={editTodoItemModalVisible}
                currentTodoItem={currentTodoItem}
                todoItems={todoItems}
                projects={projects}
                onClose={closeEditTodoItemModal}
                onCancelEdit={closeEditTodoItemModal}
                onSaveEdit={handleTodoItemSaveChanges}
            />
        </Grid>
    );
}

export default TodoPage;