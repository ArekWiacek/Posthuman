import * as React from 'react';
import { Paper } from '@mui/material';
import Grid from '@mui/material/Grid';
import { makeStyles } from '@mui/styles';
import axios from 'axios';
import SelectAvatar from '../components/Avatar/SelectAvatar';

import { AvatarContext } from "../App";
import { ApiUrl, LogT, LogI, LogE } from '../Utilities/Utilities';
import { CreateDummyTodoItems } from '../Utilities/DummyObjects';

import TodoItemList from './../components/TodoItem/TodoItemsList';
import EditTodoItem from '../components/TodoItem/EditTodoItem';
import CreateTodoItem from '../components/TodoItem/CreateTodoItem';

function todoItemFormInitialValues() {
    return {
        title: "",
        description: "",
        isCompleted: false,
        deadline: new Date(),
        projectId: null,
        avatarId: null
    }
}

const TodoPage = () => {
    LogT("TodoPage.Constructor");

    const { activeAvatar } = React.useContext(AvatarContext);
    const [todoItems, setTodoItems] = React.useState(CreateDummyTodoItems(3));
    const [currentTodoItem, setCurrentTodoItem] = React.useState(todoItemFormInitialValues());
    const [isTodoItemInEditMode, setIsTodoItemInEditMode] = React.useState(false);

    const [projects, setProjects] = React.useState([
        {
            id: 1,
            title: 'Sample project 1',
            description: 'Sample project 2 description',
            isFinished: false,
            startDate: new Date()
        }, {
            id: 2,
            title: 'Sample project 2',
            description: 'Sample project 2 description',
            isFinished: false,
            startDate: new Date()
        }
    ]);
    const handleTodoItemDeleted = (todoItemId) => {
        axios
            .delete(ApiUrl + "TodoItems/" + todoItemId)
            .then(response => {
                const todoItemsList = todoItems.filter((todoItem) => todoItem.id !== todoItemId);
                setTodoItems(todoItemsList);
            })
            .catch(error => LogE("Error occured when deleting TodoItem: ", error));
    }

    const handleTodoItemDone = (completedTodoItem) => {
        completedTodoItem.isCompleted = true;

        axios
            .put(ApiUrl + "TodoItems/" + completedTodoItem.id, completedTodoItem)
            .then(response => {
                const updatedTodoItemList = todoItems.map((todoItem) =>
                    todoItem.id === completedTodoItem ? completedTodoItem : todoItem
                );

                setTodoItems(updatedTodoItemList);
            })
            .catch(error => LogE("Error occured when saving changes into TodoItem: ", error));
    }

    const handleTodoItemCreated = (newTodoItem) => {
        axios
            .post(ApiUrl + "TodoItems", newTodoItem)
            .then(response => {
                const todoItemListWithNewTodoItem = [...todoItems, response.data];
                setTodoItems(todoItemListWithNewTodoItem);
            })
            .catch(error => LogE("Error occured when creating new TodoItem: ", error));
    }

    const handleTodoItemEdited = (todoItemToEdit) => {
        setIsTodoItemInEditMode(true);
        setCurrentTodoItem(todoItemToEdit);
    }

    const handleTodoItemCancelEdit = () => {
        setIsTodoItemInEditMode(false);
        setCurrentTodoItem(todoItemFormInitialValues());
    }


    const handleTodoItemSaveChanges = (editedTodoItemId, editedTodoItem) => {
        axios
            .put(ApiUrl + "TodoItems" + "/" + editedTodoItemId, editedTodoItem)
            .then(response => {
                const updatedTodoItemList = todoItems.map((todoItem) =>
                    todoItem.id === editedTodoItemId ? editedTodoItem : todoItem
                );

                setTodoItems(updatedTodoItemList);
                setIsTodoItemInEditMode(false);
                setCurrentTodoItem(todoItemFormInitialValues());
            })
            .catch(error => LogE("Error occured when saving changes into TodoItem: ", error));
    }

    React.useEffect(() => {
        LogT("TodoPage.useEffect1");
        LogI("TodoPage.ApiCall.Projects.Get")
        axios
            .get(ApiUrl + "Projects")
            .then(response => {
                setProjects(response.data);
            })
            .catch(error => {
                LogE("Error occured when obtaining Projects: ", error);
            });
    }, [activeAvatar]);

    React.useEffect(() => {
        LogT("TodoPage.useEffect2");
        LogI("TodoPage.ApiCall.TodoItems.Get");

        axios
            .get(ApiUrl + "TodoItems")
            .then(response => {
                setTodoItems(response.data);
            })
            .catch(error => {
                LogE("Error occured when obtaining TodoItems: ", error);
            });
    }, [activeAvatar]);

    const useStyles = makeStyles(theme => ({
        pageContent: {
            // margin: theme.spacing(5),
            // padding: theme.spacing(3)
        }
    }))

    const classes = useStyles();

    return (
        <div>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <TodoItemList
                        todoItems={todoItems}
                        onTodoItemDeleted={handleTodoItemDeleted}
                        onTodoItemEdited={handleTodoItemEdited}
                        onTodoItemDone={handleTodoItemDone} 
                    />
                </Grid>

                <Grid item xs={3}>
                    <Paper className={classes.pageContent} sx={{p: 2, display: 'flex', flexDirection: 'column' }}> 
                    {
                        isTodoItemInEditMode ?
                        (<EditTodoItem
                            onSaveChanges={handleTodoItemSaveChanges}
                            onCancelEdit={handleTodoItemCancelEdit}
                            currentTodoItem={currentTodoItem}
                            key={currentTodoItem.id}
                            projects={projects} />) : (
                            <CreateTodoItem onCreateTodoItem={handleTodoItemCreated}
                                projects={projects} />
                        )
                    }
                    </Paper>
                </Grid>
            </Grid>
        </div>
    );
}




// <Grid container spacing={3}>
//     <Grid item xs={12} md={12} lg={12}>
//         <Paper sx={{p: 2, display: 'flex', flexDirection: 'column' }}>
//             <TodoItemList
//                 todoItems={todoItems}
//                 onTodoItemDeleted={handleTodoItemDeleted}
//                 onTodoItemEdited={handleTodoItemEdited}
//                 onTodoItemDone={handleTodoItemDone}
//             />
//         </Paper>
//     </Grid>

//     {/* SELECT AVATAR */}
//     <Grid item xs={12} md={4} lg={4}>
//         <SelectAvatar />
//     </Grid>
// </Grid>

export default TodoPage;