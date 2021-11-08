import * as React from 'react';
import { TextField, IconButton, TableRow, TableCell } from '@mui/material';
import ControlPointIcon from '@mui/icons-material/ControlPoint';
import CancelIcon from '@mui/icons-material/Cancel';
import { AvatarContext } from "../../App";

const CreateTodoItemInline = ({ parentTodoItem, onCreate, onDiscard }) => {
    const { activeAvatar } = React.useContext(AvatarContext);
    const [title, setTitle] = React.useState('');

    const handleDiscardClicked = () => onDiscard();
    const handleTitleChange = event => setTitle(event.target.value);
    const paddingLeftPx = (parentTodoItem.nestingLevel + 1) * 2;

    const createSubtask = (parent) => {
        if (title === '' || activeAvatar === null || activeAvatar.id === 0) {
            return;
        }

        const newSubtask = {
            title: title,
            description: '',
            deadline: parent.deadline,
            projectId: parent.projectId !== 0 ? parent.projectId : null,
            parentId: parent.id,
            avatarId: activeAvatar.id,
            nestingLevel: parent.nestingLevel + 1
        }

        onCreate(newSubtask);
        setTitle('');
    }

    const handleCreateClicked = (parent) => {
        createSubtask(parent);
    }

    const handleKeyDown = (event, parentTodoItem) => {
        switch (event.key) {
            case 'Enter':
                createSubtask(parentTodoItem);
                break;

            case 'Escape':
                onDiscard();
                break;

            default:
                break;
        }
    }

    return (
        <TableRow>
            <TableCell component="th" scope="row" colSpan={4}>
                <TextField
                    variant="standard"
                    margin="dense" size="small"
                    placeholder="Type subtask title"
                    value={title}
                    onChange={handleTitleChange}
                    required autoFocus fullWidth
                    sx={{ minWidth: '600px', paddingRight: '80px', paddingLeft: paddingLeftPx }}
                    onKeyDown={(e) => handleKeyDown(e, parentTodoItem)}
                    InputProps={{
                        endAdornment:
                            <React.Fragment>
                                <IconButton
                                    aria-label="exit-subtask-creation"
                                    onClick={() => handleDiscardClicked()}>
                                    <CancelIcon />
                                </IconButton>
                                <IconButton
                                    aria-label="subtask-create"
                                    onClick={() => handleCreateClicked(parentTodoItem)}>
                                    <ControlPointIcon />
                                </IconButton>
                            </React.Fragment>
                    }} />
            </TableCell>
        </TableRow>
    );
}

export default CreateTodoItemInline;