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

import Slide from '@mui/material/Slide';

import { LogI } from '../Utilities/Utilities';
import { FindObjectIndexByProperty, CreateArrayCopy, InsertObjectAtIndex, ReplaceObjectInArray, RemoveObjectFromArray } from '../Utilities/ArrayHelper';

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

const Transition = React.forwardRef(function Transition(props, ref) {
    return <Slide direction="up" ref={ref} {...props} />;
});

const TodoPage = () => {
    const { activeAvatar } = React.useContext(AvatarContext);
    const [todoItems, setTodoItems] = React.useState(CreateDummyTodoItems(3));
    const [projects, setProjects] = React.useState(CreateDummyProjects(2));
    const [currentTodoItem, setCurrentTodoItem] = React.useState(todoItemFormInitialValues());
    const [isTodoItemInEditMode, setIsTodoItemInEditMode] = React.useState(false);
    const [parentId, setParentId] = React.useState(0);


    const [taskToComplete, setTaskToComplete] = React.useState(todoItemFormInitialValues())
    const [showDoneConfirmModal, setShowDoneConfirmModal] = React.useState(false);

    const handleTodoItemCreated = (newTodoItem) => {
        ApiPost("TodoItems", newTodoItem, (data) => {
            const todoItemListWithNewTodoItem = [...todoItems, data];
            setTodoItems(todoItemListWithNewTodoItem);
        });
    }

    const handleTodoItemEdited = (todoItemToEdit) => {
        setIsTodoItemInEditMode(true);
        setCurrentTodoItem(todoItemToEdit);
    }

    const handleTodoItemDeleted = (todoItemId) => {
        ApiDelete("TodoItems", todoItemId, (data) => {
            setTodoItems(RemoveObjectFromArray(todoItems, "id", todoItemId));
        });
    }

    const handleTodoItemDone = (todoItemToComplete) => {
        const task = {...todoItemToComplete};
        setTaskToComplete(task);
        setShowDoneConfirmModal(true);
    }

    const handleTodoItemCancelEdit = () => {
        setIsTodoItemInEditMode(false);
        setCurrentTodoItem(todoItemFormInitialValues());
    }


    const handleAddSubtask = (newSubtask) => {
        ApiPost("TodoItems", newSubtask, (createdSubtask) => {
            var parentTodoItemIndex = FindObjectIndexByProperty(todoItems, "id", newSubtask.parentId);
            createdSubtask.nestingLevel = newSubtask.nestingLevel;                                          // Todo - find out why not calculating correctly
            setTodoItems(InsertObjectAtIndex(todoItems, createdSubtask, parentTodoItemIndex));
        });
    };

    const handleTodoItemSaveChanges = (editedTodoItemId, editedTodoItem) => {
        ApiPut("TodoItems", editedTodoItemId, editedTodoItem, () => {
            setTodoItems(ReplaceObjectInArray(todoItems, editedTodoItem, "id", editedTodoItemId));
            setIsTodoItemInEditMode(false);
            setCurrentTodoItem(todoItemFormInitialValues());
        });
    }

    React.useEffect(() => {
        ApiGet("Projects", projects => setProjects(projects));
    }, [activeAvatar]);

    React.useEffect(() => {
        ApiGet("TodoItems/Hierarchical", todoItems => setTodoItems(todoItems));
    }, [activeAvatar]);

    const useStyles = makeStyles(theme => ({
        rowContent: {
            // margin: theme.spacing(5),
            padding: theme.spacing()
        }
    }));

    const classes = useStyles();

    const handleFinishedModal = () => {
        setShowDoneConfirmModal(false);

        // Update

        const completedTodoItem = {...taskToComplete};
        completedTodoItem.isCompleted = true;

        ApiPut("TodoItems", completedTodoItem.id, completedTodoItem, (data) => {
            const updatedTodoItemList = todoItems.map((todoItem) => 
                todoItem.id === completedTodoItem.id ? completedTodoItem : todoItem
            );

            setTodoItems(updatedTodoItemList);
        });
    };

    const handleCloseModal = () => {
        setShowDoneConfirmModal(false);
    };

    return (
        <Grid container spacing={3}>

            <Grid item xs={6}>
                <ConfirmTodoItemDoneModal 
                    isOpen={showDoneConfirmModal}
                    onFinished={handleFinishedModal}
                    onCanceled={handleCloseModal} 
                    todoItem={taskToComplete}
                    xpGained={25} />
            </Grid>

            {/* HIERARCHICAL VIEW */}
            {/* <Grid item xs={12}>
                <TodoItemsHierarchical todoItems></TodoItemsHierarchical>
            </Grid> */}

            {/* HIERARCHICAL VIEW */}
            {/* <Grid item xs={12} >
                
                <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
                    {todoItems.map(todoItem => (
                        <TodoItemRow 
                            className={classes.rowContent}
                            todoItem={todoItem}></TodoItemRow>
                    ))}
                </Paper>
            </Grid> */}

            {/* LIST VIEW */}
            <Grid item xs={12}>
                <TodoItemList
                    todoItems={todoItems}
                    onTodoItemDeleted={handleTodoItemDeleted}
                    onTodoItemEdited={handleTodoItemEdited}
                    onTodoItemDone={handleTodoItemDone}
                    onAddSubtask={handleAddSubtask}
                />
            </Grid>

            {/* CREATE / EDIT */}
            <Grid item xs={3}>
                <Paper className={classes.pageContent} sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
                    {
                        isTodoItemInEditMode ?
                            (
                                <EditTodoItem
                                    onSaveChanges={handleTodoItemSaveChanges}
                                    onCancelEdit={handleTodoItemCancelEdit}
                                    currentTodoItem={currentTodoItem}
                                    key={currentTodoItem.id}
                                    projects={projects}
                                    todoItems={todoItems} />
                            ) :
                            (
                                <CreateTodoItem
                                    onCreateTodoItem={handleTodoItemCreated}
                                    projects={projects}
                                    todoItems={todoItems}
                                    parentTaskId={parentId} />
                            )
                    }
                </Paper>
            </Grid>

            {/* <Grid item xs={6}>
                <StepperWizard />
            </Grid> */}
        </Grid>
    );
}

export default TodoPage;