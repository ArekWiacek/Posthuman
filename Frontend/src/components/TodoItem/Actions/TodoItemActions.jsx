import * as React from 'react';
import TodoItemActionsMenu from './TodoItemActionsMenu';
import TodoItemActionsButtons from './TodoItemActionsButtons';

import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff';
import VisibilityIcon from '@mui/icons-material/Visibility';

const TodoItemActions = (props) => {
    const {
        todoItem, isHover, showCollapsed,
        onDeleteClicked, onEditClicked, onDoneClicked,
        onAddSubtaskClicked, onVisibleOnOffClicked } = props;

    const handleDeleteClicked = todoItem => onDeleteClicked(todoItem);
    const handleEditClicked = todoItem => onEditClicked(todoItem);
    const handleDoneClicked = todoItem => onDoneClicked(todoItem);
    const handleAddSubtaskClicked = todoItem => onAddSubtaskClicked(todoItem);
    const handleVisibleOnOffClicked = todoItem => onVisibleOnOffClicked(todoItem);

    const getVisibilityIcon = isVisible => {
        if (isVisible)
            return <VisibilityOffIcon />;
        else
            return <VisibilityIcon />;
    };

    const getUniqueActionId = (todoItem, action) => {
        return `action_${todoItem.id}_${action.id}`;
    };

    const actions = [{
        id: getUniqueActionId,
        title: 'Add subtask',
        onClick: handleAddSubtaskClicked,
        icon: <AddIcon />,
        isDisabled: todoItem.isCompleted
    }, {
        id: 2,
        title: todoItem.isVisible ? 'Visible off' : 'Visible on',
        onClick: handleVisibleOnOffClicked,
        icon: getVisibilityIcon(todoItem.isVisible)
    }, {
        id: 3,
        title: 'Delete',
        onClick: handleDeleteClicked,
        icon: <DeleteIcon />,
        isDisabled: todoItem.isCompleted
    }, {
        id: 4,
        title: 'Edit',
        onClick: handleEditClicked,
        icon: <EditIcon fontSize='small' />,
        isDisabled: todoItem.isCompleted
    }, {
        id: 5,
        title: 'Done',
        onClick: handleDoneClicked,
        icon: <CheckCircleIcon />,
        isDisabled: todoItem.isCompleted || todoItem.hasUnfinishedSubtasks
    }];

    const renderActions = (showCollapsed) => {
        if (showCollapsed === true) {
            return (
                <TodoItemActionsMenu
                    {...props}
                    actions={actions} />);
        } else if (showCollapsed === false)
            return (
                <TodoItemActionsButtons
                    {...props}
                    actions={actions}
                    isVisible={isHover && !todoItem.isCompleted} />);
    }

    return (
        <React.Fragment>
            {renderActions(showCollapsed)}
        </React.Fragment>
    );
}

export default TodoItemActions;