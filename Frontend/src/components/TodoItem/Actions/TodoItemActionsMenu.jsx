import * as React from 'react';
import { useState } from 'react';
import { Menu, IconButton } from '@mui/material';
import MenuItem from '@mui/material/MenuItem';
import ListItemIcon from '@mui/material/ListItemIcon';
import MoreVertIcon from '@mui/icons-material/MoreVert';

const TodoItemActionsMenu = ({ todoItem, actions }) => {
  const [triggerElementAnchor, setTriggerElementAnchor] = useState(null);
  const isOpen = Boolean(triggerElementAnchor);

  const handleClick = (event) => {
    setTriggerElementAnchor(event.currentTarget);
  };

  const handleClose = () => {
    setTriggerElementAnchor(null);
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
        onClose={handleClose}>
        {
          actions.map((action) => {
            if (!action.isDisabled) {
              return (
                <MenuItem
                  key={action.id} 
                  onClick={() => action.onClick(todoItem)}>
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