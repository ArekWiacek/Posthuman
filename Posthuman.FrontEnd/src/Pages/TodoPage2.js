import * as React from 'react';
import { Paper, Grid } from '@mui/material';
import { makeStyles } from '@mui/styles';

import { AvatarContext } from "../App";
import { CreateDummyTodoItems, CreateDummyProjects } from '../Utilities/DummyObjects';

import TodoItemList from './../components/TodoItem/TodoItemsList';
import EditTodoItem from '../components/TodoItem/EditTodoItem';
import CreateTodoItem from '../components/TodoItem/CreateTodoItem';
import TodoItemRow from './../components/TodoItem/TodoItemRow';

import { ApiGet, ApiPost, ApiPut, ApiDelete } from '../Utilities/ApiRepository';

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

const TodoPage2 = () => {
    const { activeAvatar } = React.useContext(AvatarContext);
    const [todoItems, setTodoItems] = React.useState(CreateDummyTodoItems(3));
    const [projects, setProjects] = React.useState(CreateDummyProjects(2));
    const [currentTodoItem, setCurrentTodoItem] = React.useState(todoItemFormInitialValues());
    const [isTodoItemInEditMode, setIsTodoItemInEditMode] = React.useState(false);
    const [parentId, setParentId] = React.useState(0);

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
        completedTodoItem.isCompleted = true;

        ApiPut("TodoItems", completedTodoItem.id, completedTodoItem, (data) => {
            const updatedTodoItemList = todoItems.map((todoItem) =>
                    todoItem.id === completedTodoItem ? completedTodoItem : todoItem
            );
            
            setTodoItems(updatedTodoItemList);
        });
    }

    const handleTodoItemCancelEdit = () => {
        setIsTodoItemInEditMode(false);
        setCurrentTodoItem(todoItemFormInitialValues());
    }

    const handleAddSubtask = task => setParentId(task.id);

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
        ApiGet("TodoItems", todoItems => setTodoItems(todoItems));
    }, [activeAvatar]);

    const useStyles = makeStyles(theme => ({
        rowContent: {
            // margin: theme.spacing(5),
            padding: theme.spacing()        
        }
    }))

    const classes = useStyles();

    return (
        <Grid container spacing={3}>
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
        </Grid>
    );
}

export default TodoPage;