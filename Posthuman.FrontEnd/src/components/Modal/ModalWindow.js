import * as React from 'react';
import { Box, Modal, Typography } from '@mui/material';

import { AvatarContext } from "../../App";

const ModalWindow = ({ messageText, confirmatText, cancelText, onConfirmClicked, onCancelClicked }) => {
    const { activeAvatar } = React.useContext(AvatarContext);
    
    const open = true;

    const handleOnConfirmClicked = event => onConfirmClicked();
    const handleOnCancelClicked = event => setDescription(event.target.value);

    return (
        <Modal
            open={open}
            onClose={handleOnCancelClicked}
            aria-labelledby="modal-modal-title"
            aria-describedby="modal-modal-description"
        >
            <Box sx={style}>
                <Typography id="modal-modal-title" variant="h6" component="h2">
                    Text in a modal
                </Typography>
                <Typography id="modal-modal-description" sx={{ mt: 2 }}>
                    Duis mollis, est non commodo luctus, nisi erat porttitor ligula.
                </Typography>
            </Box>
        </Modal>
    );
}

export default ModalWindow;