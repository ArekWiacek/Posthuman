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
      todoItem, isHover, showSmallMenu, 
      onDeleteClicked, onEditClicked, onDoneClicked, 
      onAddSubtaskClicked, onVisibleOnOffClicked } = props;

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

    const getUniqueId = (todoItem, action) => {
      return `action_${todoItem.id}_${action.id}`; 
    };

    const actions = [{
      id: getUniqueId,
      title: 'Add subtask',
      onClick: handleAddSubtaskClicked,
      icon: <AddIcon />,
      isDisabled: todoItem.isCompleted
    }, {
      id: 2,
      title: 'Visible on/off',
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
      icon: <EditIcon fontSize='small'/>,
      isDisabled: todoItem.isCompleted
    }, {
      id: 5,
      title: 'Done',
      onClick: handleDoneClicked,
      icon: <CheckCircleIcon />,
      isDisabled: todoItem.isCompleted || todoItem.hasUnfinishedSubtasks
    }];

    const renderActions = (smallMenu) => {
      if(smallMenu === true) {
        return (
          <TodoItemActionsMenu 
            { ...props } 
            actions={actions} />);
      } else if(smallMenu === false)
        return (
          <TodoItemActionsButtons 
            {...props} 
            actions={actions} 
            isVisible={isHover && !todoItem.isCompleted} />);
    }

    return (
      <React.Fragment>
        {renderActions(showSmallMenu)}
      </React.Fragment>
    );
}

export default TodoItemActions;