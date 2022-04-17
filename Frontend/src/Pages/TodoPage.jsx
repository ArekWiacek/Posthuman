import * as React from 'react';
import { useState, useEffect, useContext } from 'react';
import {  Grid, Typography } from '@mui/material';
import moment from 'moment';
import { CreateDummyTodoItems, CreateDummyProjects } from '../Utilities/DummyObjects';
import TodoItemList from '../components/TodoItem/TodoItemsList/TodoItemsList';
import ConfirmTodoItemDoneModal from '../components/TodoItem/Modals/ConfirmTodoItemDoneModal';
import CreateTodoItemModal from '../components/TodoItem/Modals/CreateTodoItemModal';
import EditTodoItemModal from '../components/TodoItem/Modals/EditTodoItemModal';
import Api from '../Utilities/ApiHelper';
import * as ArrayHelper from '../Utilities/ArrayHelper';
import { LogI, LogE } from '../Utilities/Utilities';
import DeleteTodoItemModal from '../components/TodoItem/Modals/DeleteTodoItemModal';
import AvatarView from '../components/Avatar/AvatarView';
// import SelectAvatar from '../components/Avatar/SelectAvatar';
import NotificationsPanel from '../components/Notifications/NotificationsPanel';
import useDisplayOptions from '../Hooks/useDisplayOptions';
import useAuth from '../Hooks/useAuth';

moment.updateLocale("pl", {
    week: {
        dow: 1
    }
});

function todoItemFormInitialValues() {
    return {
        title: "",
        description: "",
        isCompleted: false,
        deadline: new Date(),
        projectId: null,
        avatarId: null,
        parentId: null,
        isCyclic: false,
        startDate: new Date(),
        endDate: null
    }
}

const defaultUser = { 
    name: 'Some example player', 
    level: 3, 
    bio: 'Some example bio', 
    exp: 345, 
    expToNewLevel: 400, 
    cybertribeName: 'Transhumanist'
};

const TodoPage = () => {
    const todoItemsEndpointName = "TodoItems";
    const [displayOptions] = useDisplayOptions();
    const [avatar, setAvatar] = useState(defaultUser);
    const { user } = useAuth();
    
    useEffect(() => {
        console.log("TodoPage-useEffect");

        Api.Get("Avatars/GetAvatarForLoggedUser", data => {
            if (data !== undefined && data.id !== undefined && data.id !== 0) {
                setAvatar(data);
            }
        }, error => 
            LogE("zjebao sie", error.toJSON()));
    }, [user]);


    const [todoItems, setTodoItems] = useState(CreateDummyTodoItems(5));
    const [projects, setProjects] = useState(CreateDummyProjects(2));
    const [todoItemToEdit, setTodoItemToEdit] = useState(todoItemFormInitialValues());
    const [todoItemToDelete, setTodoItemToDelete] = useState(todoItemFormInitialValues());
    const [todoItemToBeCompleted, setTodoItemToBeCompleted] = useState(todoItemFormInitialValues());

    const [modalsVisibility, setModalsVisibility] = useState({
        create: false,
        edit: false,
        confirmComplete: false,
        delete: false
    });

    // CREATE TODO ITEM
    const handleCreateTodoItem = (todoItemToCreate) => {
        Api.Post(todoItemsEndpointName, todoItemToCreate, (createdTodoItem) => {
            let newArray = [];
            if (!createdTodoItem.parentId) {
                if (todoItems && todoItems.length > 0)
                    newArray = ArrayHelper.InsertObject(todoItems, createdTodoItem);
                else
                    newArray.push(createdTodoItem);
            }
            else {
                let parentIndex = ArrayHelper.FindObjectIndexByProperty(todoItems, "id", createdTodoItem.parentId);
                newArray = ArrayHelper.InsertObjectAtIndex(todoItems, createdTodoItem, parentIndex + 1);
            }
            setTodoItems(newArray);
        }, (error) => {
            console.error(error);
        });

        setModalVisible('create', false);
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

    // Open delete confirmation modal
    const handleTodoItemDelete = (todoItemToDeleteId) => {
        let itemToDelete = ArrayHelper.FindObjectByProperty(todoItems, "id", todoItemToDeleteId);
        setTodoItemToDelete({ ...itemToDelete });
        setModalVisible('delete', true);
    }

    // DELETE TODO ITEM (with subtasks)
    const handleTodoItemDeleteConfirmed = (todoItemToDelete) => {
        Api.Delete(todoItemsEndpointName, todoItemToDelete.id, () => {
            // TODO - remove by hand
            refreshTodoItemsCollection();
            //setTodoItems(deleteWithSubtasks(todoItemToDelete));
        });
        setModalVisible('delete', false);
    }

    // UPDATE TODO ITEM (after edition)
    const handleTodoItemSaveChanges = (editedTodoItemId, editedTodoItem) => {
        Api.Put(todoItemsEndpointName, editedTodoItemId, editedTodoItem, () => {
            // Todo - handle updating by hand (moving in hierarchy is not working)
            refreshTodoItemsCollection();
            // setTodoItems(ArrayHelper.ReplaceObjectInArray(todoItems, editedTodoItem, "id", editedTodoItemId));
            // setTodoItemToEdit(todoItemFormInitialValues());
        });

        closeEditTodoItemModal();
    }

    // Make todo item visible / invisible, and propagate it to all it's children
    const handleTodoItemVisibleOnOff = todoItem => {
        // TODO in upcoming version
        // let isVisible = !todoItem.isVisible;
        // let todoItemsTree = [];

        // let clickedTodoItemCopy = ArrayHelper.CopyObject(todoItem);
        // todoItemsTree = ArrayHelper.GetAllChildren(todoItems, clickedTodoItemCopy);

        // let stateCopy = ArrayHelper.CopyArray(todoItems);

        // todoItemsTree.map(item => {
        //     item.isVisible = isVisible;
        //     stateCopy = ArrayHelper.ReplaceObjectInArray(stateCopy, item, 'id', item.id);
        // });

        // setTodoItems(stateCopy);

        let updatedTodoItem = { ...todoItem, isVisible: !todoItem.isVisible };
        Api.Put(todoItemsEndpointName, updatedTodoItem.id, updatedTodoItem, () => {
            // TODO - change to optimistic update 
            refreshTodoItemsCollection();
        });
    };

    // EDIT TODO ITEM CLICKED - turn on edit mode 
    const handleTodoItemEdit = (todoItemToEdit) => {
        setTodoItemToEdit(todoItemToEdit);
        openEditTodoItemModal();
    };

    // Complete task window opened
    const handleTodoItemCompleted = (todoItemToBeCompleted) => {
        const todoItem = { ...todoItemToBeCompleted };
        setTodoItemToBeCompleted(todoItem);
        setModalVisible('confirmComplete', true);
    };

    // Complete task canceled
    const handleTodoItemCompleteDiscarded = () => {
        setModalVisible('confirmComplete', false);
    };

    // Complete task confirmed
    const handleTodoItemCompleteConfirmed = () => {
        var completedTodoItem = ArrayHelper.UpdateObjectProperty(todoItemToBeCompleted, "isCompleted", true);
        Api.Put(todoItemsEndpointName + "/Complete", completedTodoItem.id, completedTodoItem, () => {
            setTodoItems(ArrayHelper.ReplaceObjectInArray(todoItems, completedTodoItem, "id", completedTodoItem.id));

            // TODO - remove
            refreshTodoItemsCollection();
        });

        setModalVisible('confirmComplete', false);
    };

    const setModalVisible = (modal, isVisible) => setModalsVisibility({ ...modalsVisibility, [modal]: isVisible });
    const openCreateTodoItemModal = () => setModalVisible('create', true);
    const closeCreateTodoItemModal = () => setModalVisible('create', false);
    const openEditTodoItemModal = () => setModalVisible('edit', true);
    const closeEditTodoItemModal = () => setModalVisible('edit', false);
    const closeDeleteTodoItemModal = () => setModalVisible('delete', false);

    // useEffect(() => {
    //     Api.Get("Projects", projects => setProjects(projects));
    // }, [currentUser]);

    useEffect(() => {
        getTodoItems();
    }, [user, displayOptions]);

    const getTodoItems = () => {
        let endpointName = '';

        if(displayOptions.displayMode === 'flat')
            endpointName = todoItemsEndpointName;
        else if(displayOptions.displayMode === 'hierarchical')
            endpointName = todoItemsEndpointName + '/Hierarchical';
        else if(displayOptions.displayMode === 'dayByDay')
            endpointName = todoItemsEndpointName + '/ByDeadline/2022-04-06';
            

        //let endpointName = displayOptions.displayMode === 'flat' ? todoItemsEndpointName : todoItemsEndpointName + "/Hierarchical";
        LogI(`TodoPage: Getting todo items list! Endpoint: ${endpointName}`);

        Api.Get(endpointName, todoItems => {
            setTodoItems(todoItems);
        });
    };

    // Temporary lame method for refreshing whole todo items collection when changes occured
    const refreshTodoItemsCollection = () => {
        getTodoItems(displayOptions.displayMode);
    };

    return (
        <React.Fragment>            
            <Grid container spacing={3}>
                {/* LIST VIEW */}
                <Grid item xs={12} md={8} lg={9}>
                    <TodoItemList
                        todoItems={todoItems}
                        onTodoItemDelete={handleTodoItemDelete}
                        onTodoItemEdit={handleTodoItemEdit}
                        onTodoItemDone={handleTodoItemCompleted}
                        onAddSubtask={handleSubtaskCreate}
                        onTodoItemVisibleOnOff={handleTodoItemVisibleOnOff}
                        onOpenCreateTodoModal={openCreateTodoItemModal} />
                </Grid>
                <Grid item xs={12} md={4} lg={3}>
                    <AvatarView avatar={avatar} viewMode='minimal'/>
                    <NotificationsPanel />
                    {/* />
                    <SelectAvatar isMini /> */}
                </Grid>
            </Grid>

            {/* TODOITEM MODALS  */}
            <React.Fragment>
                <CreateTodoItemModal
                    isOpen={modalsVisibility.create}
                    todoItems={todoItems}
                    projects={projects}
                    onClose={closeCreateTodoItemModal}
                    onCreateTodoItem={handleCreateTodoItem}
                />

                <EditTodoItemModal
                    isOpen={modalsVisibility.edit}
                    todoItemToEdit={todoItemToEdit}
                    todoItems={todoItems}
                    projects={projects}
                    onClose={closeEditTodoItemModal}
                    onCancelEdit={closeEditTodoItemModal}
                    onSaveEdit={handleTodoItemSaveChanges}
                />

                <DeleteTodoItemModal
                    isOpen={modalsVisibility.delete}
                    todoItemToDelete={todoItemToDelete}
                    onClose={closeDeleteTodoItemModal}
                    onDelete={handleTodoItemDeleteConfirmed}
                    onCancelDelete={closeDeleteTodoItemModal}
                />

                <ConfirmTodoItemDoneModal
                    isOpen={modalsVisibility.confirmComplete}
                    onFinished={handleTodoItemCompleteConfirmed}
                    onCanceled={handleTodoItemCompleteDiscarded}
                    todoItem={todoItemToBeCompleted}
                    xpGained={25} />
            </React.Fragment>
        </React.Fragment>
    );
}

export default TodoPage;