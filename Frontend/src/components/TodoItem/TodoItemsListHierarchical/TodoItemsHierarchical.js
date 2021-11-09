import * as React from 'react';
import { Button, Table, TableRow, TableCell, TableHead, TableContainer, TableBody, Paper } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import AddTaskIcon from '@mui/icons-material/AddTask';
import AddIcon from '@mui/icons-material/Add';
import moment from 'moment';
// import Moment from 'react-moment';

const TodoItemsHierarchical = ({ todoItems, onTodoItemDeleted, onTodoItemEdited, onTodoItemDone, onAddSubtask }) => {
    const handleTodoItemDeleted = todoItemId => onTodoItemDeleted(todoItemId);
    const handleTodoItemEdit = todoItem => onTodoItemEdited(todoItem);
    const handleTodoItemDone = todoItem => onTodoItemDone(todoItem);
    const handleAddSubtask = todoItem => onAddSubtask(todoItem);
    const createRewardDisplayText = () => { "+20XP" }

    const createParentProjectDisplayText = (todoItem) => {
        return todoItem.projectId ? ("ID: " + todoItem.projectId) : "-";
    }

    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 700 }} size="small" aria-label="TodoItems list">
                <TableHead>
                    <TableRow>
                        <TableCell>ID</TableCell>
                        <TableCell>Title</TableCell>
                        <TableCell align="left">Deadline</TableCell>
                        <TableCell align="right"></TableCell>
                        <TableCell align="right">Reward</TableCell>
                        <TableCell align="right">Project</TableCell>
                        <TableCell align="right">Parent task</TableCell>
                        <TableCell align="right">Actions</TableCell>
                    </TableRow>
                </TableHead>

                <TableBody>
                    {todoItems.map((todoItem) => (
                        <TodoItemsHierarchicalRow todoItem={todoItem} />
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}


const TodoItemsHierarchicalRow = ({ todoItem, onTodoItemDeleted, onTodoItemEdited, onTodoItemDone, onAddSubtask }) => {
    const handleTodoItemDeleted = todoItemId => onTodoItemDeleted(todoItemId);
    const handleTodoItemEdit = todoItem => onTodoItemEdited(todoItem);
    const handleTodoItemDone = todoItem => onTodoItemDone(todoItem);
    const handleAddSubtask = todoItem => onAddSubtask(todoItem);
    const createRewardDisplayText = () => { "+20XP" }

    const createParentProjectDisplayText = (todoItem) => {
        return todoItem.projectId ? ("ID: " + todoItem.projectId) : "-";
    }

    return (
        <TableRow key={todoItem.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
            <TableCell component="th" scope="row" >
                {todoItem.id}
            </TableCell>
            <TableCell component="th" scope="row" >
                {todoItem.title}
            </TableCell>
            <TableCell align="left">
                {/* <Moment format="DD.MM.YYYY">{todoItem.deadline}</Moment> */}
            </TableCell>
            <TableCell align="left">
                {/* <Moment format="dddd">{todoItem.deadline}</Moment> */}
            </TableCell>
            <TableCell align="right">
                {createRewardDisplayText(todoItem)}
            </TableCell>
            <TableCell align="right">
                {createParentProjectDisplayText(todoItem)}
            </TableCell>
            <TableCell align="right">
                {todoItem.parentId != null ? todoItem.parentId : "nie ma"}
            </TableCell>
            <TableCell align="right">
                <Button variant="outlined"
                    sx={{ mr: 2 }}
                    startIcon={<AddIcon />}
                    onClick={(e) => handleAddSubtask(todoItem)}
                    disabled={todoItem.isCompleted}>
                    Subtask
                </Button>
                <Button variant="outlined"
                    startIcon={<DeleteIcon />}
                    onClick={(e) => handleTodoItemDeleted(todoItem.id)}
                    disabled={todoItem.isCompleted}>
                    Delete
                </Button>
                <Button variant="outlined"
                    sx={{ mr: 2, ml: 2 }}
                    startIcon={<EditIcon />}
                    onClick={(e) => handleTodoItemEdit(todoItem)}
                    disabled={todoItem.isCompleted}>
                    Edit
                </Button>
                <Button variant="outlined"
                    startIcon={<CheckCircleIcon />}
                    onClick={(e) => handleTodoItemDone(todoItem)}
                    disabled={todoItem.isCompleted}>
                    Done
                </Button>
            </TableCell>
        </TableRow>
    );
}


export { TodoItemsHierarchicalRow, TodoItemsHierarchical } 
