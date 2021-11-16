import * as React from 'react';
import Divider from '@mui/material/Divider';
import { Paper, Button, Menu } from '@mui/material';
import MenuList from '@mui/material/MenuList';
import MenuItem from '@mui/material/MenuItem';
import ListItemText from '@mui/material/ListItemText';
import ListItemIcon from '@mui/material/ListItemIcon';
import Typography from '@mui/material/Typography';
import ContentCut from '@mui/icons-material/ContentCut';
import ContentCopy from '@mui/icons-material/ContentCopy';
import ContentPaste from '@mui/icons-material/ContentPaste';
import Cloud from '@mui/icons-material/Cloud';

export default function IconMenu() {
    
    const [anchorEl, setAnchorEl] = React.useState(null);
    const open = Boolean(anchorEl);
    const handleClick = (event) => {
      setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
      setAnchorEl(null);
    };
    

  return (
//     <div>
//     <Button
//       id="basic-button"
//       aria-controls="basic-menu"
//       aria-haspopup="true"
//       aria-expanded={open ? 'true' : undefined}
//       onClick={handleClick}
//     >
//       Dashboard
//     </Button>
//     <Menu
//       id="basic-menu"
//       anchorEl={anchorEl}
//       open={open}
//       onClose={handleClose}
//       MenuListProps={{
//         'aria-labelledby': 'basic-button',
//       }}
//     >
//       <MenuItem onClick={handleClose}>Profile</MenuItem>
//       <MenuItem onClick={handleClose}>My account</MenuItem>
//       <MenuItem onClick={handleClose}>Logout</MenuItem>
//     </Menu>
//   </div>

    <Paper sx={{ width: 320, maxWidth: '100%' }}>
        <Button
      id="basic-button"
      aria-controls="basic-menu"
      aria-haspopup="true"
      aria-expanded={open ? 'true' : undefined}
      onClick={handleClick}
    >
      Dashboard
    </Button>
    <Menu
      id="basic-menu"
      anchorEl={anchorEl}
      open={open}
      onClose={handleClose}
      MenuListProps={{
        'aria-labelledby': 'basic-button',
      }}>

      </Menu>

    //   <MenuList>
    //     <MenuItem>
    //       <ListItemIcon>
    //         <ContentCut fontSize="small" />
    //       </ListItemIcon>
    //       <ListItemText>Cut</ListItemText>
    //       <Typography variant="body2" color="text.secondary">
    //         ⌘X
    //       </Typography>
    //     </MenuItem>
    //     <MenuItem>
    //       <ListItemIcon>
    //         <ContentCopy fontSize="small" />
    //       </ListItemIcon>
    //       <ListItemText>Copy</ListItemText>
    //       <Typography variant="body2" color="text.secondary">
    //         ⌘C
    //       </Typography>
    //     </MenuItem>
    //     <MenuItem>
    //       <ListItemIcon>
    //         <ContentPaste fontSize="small" />
    //       </ListItemIcon>
    //       <ListItemText>Paste</ListItemText>
    //       <Typography variant="body2" color="text.secondary">
    //         ⌘V
    //       </Typography>
    //     </MenuItem>
    //     <Divider />
    //     <MenuItem>
    //       <ListItemIcon>
    //         <Cloud fontSize="small" />
    //       </ListItemIcon>
    //       <ListItemText>Web Clipboard</ListItemText>
    //     </MenuItem>
    //   </MenuList>
    </Paper>
  );
}