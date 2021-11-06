import * as React from 'react';
import { Paper, Grid } from '@mui/material';
import { makeStyles } from '@mui/styles';

import { AvatarContext } from "../App";
import { CreateDummyTodoItems, CreateDummyProjects } from '../Utilities/DummyObjects';

import TodoItemList from '../components/TodoItem/TodoItemsList/TodoItemsList';
import EditTodoItem from '../components/TodoItem/EditTodoItem';
import CreateTodoItem from '../components/TodoItem/CreateTodoItem';

import ConfirmTodoItemDoneModal from '../components/Modal/ConfirmTodoItemDoneModal';
// import ModalWindow from '../components/Modal/ModalWindow';
// import Wizard from '../components/Wizard/Wizard';
// import StepperWizard from '../components/StepperWizard/StepperWizard';

import { ApiGet, ApiPost, ApiPut, ApiDelete } from '../Utilities/ApiRepository';

import Slide from '@mui/material/Slide';

import { Modal, Typography, Box } from '@mui/material';
import { LogI } from '../Utilities/Utilities';

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
            const todoItemsList = todoItems.filter((todoItem) => todoItem.id !== todoItemId);
            setTodoItems(todoItemsList);
        });
    }

    const handleTodoItemDone = (completedTodoItem) => {
        const task = {...completedTodoItem};
        setTaskToComplete(task);
        setShowDoneConfirmModal(true);
    }

    const handleTodoItemCancelEdit = () => {
        setIsTodoItemInEditMode(false);
        setCurrentTodoItem(todoItemFormInitialValues());
    }

    const handleAddSubtask = (task) => {
        setParentId(task.id);
        // ToDo - set also parent project
    }

    const handleTodoItemSaveChanges = (editedTodoItemId, editedTodoItem) => {
        ApiPut("TodoItems", editedTodoItemId, editedTodoItem, (data) => {
            const updatedTodoItemList = todoItems.map((todoItem) =>
                todoItem.id === editedTodoItemId ? editedTodoItem : todoItem
            );

            setTodoItems(updatedTodoItemList);
            setIsTodoItemInEditMode(false);
            setCurrentTodoItem(todoItemFormInitialValues());
        });
    }

    React.useEffect(() => {
        ApiGet("Projects", projects => setProjects(projects));
    }, [activeAvatar]);

    React.useEffect(() => {
        //ApiGet("TodoItems", todoItems => setTodoItems(todoItems));

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
        LogI("Modal closed");
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