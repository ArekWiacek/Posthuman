import * as React from 'react';
import { useState } from 'react';
import { Menu, IconButton } from '@mui/material';
import MenuItem from '@mui/material/MenuItem';
import ListItemIcon from '@mui/material/ListItemIcon';
import MoreVertIcon from '@mui/icons-material/MoreVert';

const TodoItemActionsMenu = ({ todoItem, actions, onClick }) => {
  const [triggerElementAnchor, setTriggerElementAnchor] = useState(null);
  const isOpen = Boolean(triggerElementAnchor);

  const handleClick = event => {
    setTriggerElementAnchor(event.currentTarget);
  };

  const handleClose = event => {
    setTriggerElementAnchor(null);
  };

  const handleMenuItemClicked = (action, todoItem) => {
    setTriggerElementAnchor(null);
    action.onClick(todoItem);
  };

  return (
    <div>
      <IconButton
        id="show-actions-button"
        onClick={handleClick}>
        <MoreVertIcon />
      </IconButton>
      <Menu
        id="more-actions-menu"
        anchorEl={triggerElementAnchor}
        open={isOpen}
        onClose={handleClose}
        disableRestoreFocus={true}>
        {
          actions.map((action) => {
            if (!action.isDisabled) {
              return (
                <MenuItem
                  key={action.id} 
                  onClick={ () => handleMenuItemClicked(action, todoItem) }>
                  <ListItemIcon>
                    {action.icon}
                  </ListItemIcon>
                  {action.title}
                </MenuItem>)
            }
          })
        }
      </Menu>
    </div >
  );
}

export default TodoItemActionsMenu;