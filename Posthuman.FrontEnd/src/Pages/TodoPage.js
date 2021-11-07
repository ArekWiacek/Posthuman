import * as React from 'react';
import { Paper, Grid } from '@mui/material';
import { makeStyles } from '@mui/styles';

import { AvatarContext } from "../App";
import { CreateDummyTodoItems, CreateDummyProjects } from '../Utilities/DummyObjects';

import TodoItemList from '../components/TodoItem/TodoItemsList/TodoItemsList';
import EditTodoItem from '../components/TodoItem/EditTodoItem';
import CreateTodoItem from '../components/TodoItem/CreateTodoItem';
import ConfirmTodoItemDoneModal from '../components/Modal/ConfirmTodoItemDoneModal';

import { ApiGet, ApiPost, ApiPut, ApiDelete } from '../Utilities/ApiRepository';
import { LogI } from '../Utilities/Utilities';
import {
    FindObjectIndexByProperty, InsertObjectAtIndex, InsertObject, 
    ReplaceObjectInArray, RemoveObjectFromArray, UpdateObjectProperty
} from '../Utilities/ArrayHelper';

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

    const { activeAvatar } = React.useContext(AvatarContext);
    const [todoItems, setTodoItems] = React.useState(CreateDummyTodoItems(3));
    const [projects, setProjects] = React.useState(CreateDummyProjects(2));
    const [currentTodoItem, setCurrentTodoItem] = React.useState(todoItemFormInitialValues());
    const [isTodoItemInEditMode, setIsTodoItemInEditMode] = React.useState(false);
    const [parentId, setParentId] = React.useState(0);

    const [todoItemToBeCompleted, setTodoItemToBeCompleted] = React.useState(todoItemFormInitialValues())
    const [todoCompleteConfirmationModalVisible, setTodoCompleteConfirmationModalVisible] = React.useState(false);

    // CREATE TODO ITEM
    const handleTodoItemCreate = (todoItemToCreate) => {
        ApiPost(todoItemsEndpointName, todoItemToCreate, (createdTodoItem) => {
            var newArray = InsertObject(todoItems, createdTodoItem);
            setTodoItems(newArray);
        });
    }

    // CREATE SUBTASK
    const handleSubtaskCreate = (subtaskToCreate) => {
        ApiPost(todoItemsEndpointName, subtaskToCreate, (createdSubtask) => {
            var parentTodoItemIndex = FindObjectIndexByProperty(todoItems, "id", subtaskToCreate.parentId);
            // Todo - find out why not calculating correctly
            createdSubtask.nestingLevel = subtaskToCreate.nestingLevel;
            var newArray = InsertObjectAtIndex(todoItems, createdSubtask, parentTodoItemIndex + 1);
            setTodoItems(newArray);
        });
    }

    // DELETE
    const handleTodoItemDelete = (todoItemToDeleteId) => {
        ApiDelete(todoItemsEndpointName, todoItemToDeleteId, () => {
            setTodoItems(RemoveObjectFromArray(todoItems, "id", todoItemToDeleteId));
        });
    }

    // UPDATE
    const handleTodoItemSaveChanges = (editedTodoItemId, editedTodoItem) => {
        ApiPut(todoItemsEndpointName, editedTodoItemId, editedTodoItem, () => {
            setTodoItems(ReplaceObjectInArray(todoItems, editedTodoItem, "id", editedTodoItemId));
            setIsTodoItemInEditMode(false);
            setCurrentTodoItem(todoItemFormInitialValues());
        });
    }

    // EDITING CHOOSEN
    const handleTodoItemEdit = (todoItemToEdit) => {
        setIsTodoItemInEditMode(true);
        setCurrentTodoItem(todoItemToEdit);
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
        var completedTodoItem = UpdateObjectProperty(todoItemToBeCompleted, "isCompleted", true);
        ApiPut(todoItemsEndpointName, completedTodoItem.id, completedTodoItem, () => {
            setTodoItems(ReplaceObjectInArray(todoItems, completedTodoItem, "id", completedTodoItem.id));
        });

        setTodoCompleteConfirmationModalVisible(false);
    }

    React.useEffect(() => {
        ApiGet("Projects", projects => setProjects(projects));
    }, [activeAvatar]);

    React.useEffect(() => {
        ApiGet(todoItemsEndpointName + "/Hierarchical", todoItems => setTodoItems(todoItems));
    }, [activeAvatar]);

    return (
        <Grid container spacing={3}>
            {/* LIST VIEW */}
            <Grid item xs={12}>
                <TodoItemList
                    todoItems={todoItems}
                    onTodoItemDeleted={handleTodoItemDelete}
                    onTodoItemEdited={handleTodoItemEdit}
                    onTodoItemDone={handleTodoItemCompleted}
                    onAddSubtask={handleSubtaskCreate}
                />
            </Grid>

            {/* CREATE / EDIT */}
            <Grid item xs={3}>
                <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
                    {
                        isTodoItemInEditMode ? (
                            <EditTodoItem
                                onSaveChanges={handleTodoItemSaveChanges}
                                onCancelEdit={handleTodoItemCancelEdit}
                                currentTodoItem={currentTodoItem}
                                key={currentTodoItem.id}
                                projects={projects}
                                todoItems={todoItems} />
                        ) : (
                            <CreateTodoItem
                                onCreateTodoItem={handleTodoItemCreate}
                                projects={projects}
                                todoItems={todoItems}
                                parentTaskId={parentId} />)
                    }
                </Paper>
            </Grid>

            {/* TodoItem done confirmation modal */}
            <ConfirmTodoItemDoneModal
                isOpen={todoCompleteConfirmationModalVisible}
                onFinished={handleTodoItemCompleteConfirmed}
                onCanceled={handleTodoItemCompleteDiscarded}
                todoItem={todoItemToBeCompleted}
                xpGained={25} />
        </Grid>
    );
}

export default TodoPage;