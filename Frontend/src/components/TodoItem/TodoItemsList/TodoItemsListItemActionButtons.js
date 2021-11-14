import * as React from 'react';
import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff';
import VisibilityIcon from '@mui/icons-material/Visibility';
import ActionButton from '../../Common/ActionButton';

const TodoItemListItemActionButtons = ({ todoItem, onDeleteClicked, onEditClicked, onDoneClicked, onAddSubtaskClicked, onVisibleOnOffClicked }) => {
    const handleDeleteClicked = todoItem => onDeleteClicked(todoItem);
    const handleEditClicked = todoItem => onEditClicked(todoItem);
    const handleDoneClicked = todoItem => onDoneClicked(todoItem);
    const handleAddSubtaskClicked = todoItem => onAddSubtaskClicked(todoItem);
    const handleVisibleOnOffClicked = todoItem => onVisibleOnOffClicked(todoItem);
    const getVisibilityIcon = isVisible => {
        if (isVisible)
            return <VisibilityIcon />;
        else
            return <VisibilityOffIcon />;
    };

    const actionButtons = [{
        id: 1,
        tooltip: 'Add subtask',
        ariaLabel: 'add-subtask',
        onClick: handleAddSubtaskClicked,
        icon: <AddIcon />,
        isDisabled: todoItem.isCompleted
    }, {
        id: 2,
        tooltip: 'Visible on/off',
        ariaLabel: 'visible-onoff',
        onClick: handleVisibleOnOffClicked,
        icon: getVisibilityIcon(todoItem.isVisible)
    }, {
        id: 3,
        tooltip: 'Delete',
        ariaLabel: 'delete-todo',
        onClick: handleDeleteClicked,
        icon: <DeleteIcon />,
        isDisabled: todoItem.isCompleted
    }, {
        id: 4,
        tooltip: 'Edit',
        ariaLabel: 'edit-todo',
        onClick: handleEditClicked,
        icon: <EditIcon />,
        isDisabled: todoItem.isCompleted
    }, {
        id: 5,
        tooltip: 'Done',
        ariaLabel: 'done-todo',
        onClick: handleDoneClicked,
        icon: <CheckCircleIcon />,
        isDisabled: todoItem.isCompleted || todoItem.hasUnfinishedSubtasks
    }];

    return (
        <React.Fragment>
            {
                actionButtons.map((actionButton) => (
                    <ActionButton
                        key={actionButton.id}
                        tooltip={actionButton.tooltip}
                        ariaLabel={actionButton.ariaLabel}
                        onClick={() => actionButton.onClick(todoItem)}
                        isDisabled={actionButton.isDisabled}
                        icon={actionButton.icon} />
                    ))
            }
        </React.Fragment>
    );
}

export default TodoItemListItemActionButtons;