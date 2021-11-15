import * as React from 'react';
import { Box, Modal, Backdrop } from '@mui/material';

const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4, 
    backgroundImage: 'url("/Assets/Eyecandies/eyecandy1.svg")',    
    backgroundRepeat: 'no-repeat',
    backgroundPosition: 'right top'
};

// Custom modal window encapsulating application sci-fi style adding eyecandies 
// into background and manages general modal behaviour
const ModalWindow = (props) => {
    const handleCloseModal = () => {
        if(props.onClose)
            props.onClose();
    };

    return (
        <Modal
            open={props.isOpen}
            onClose={handleCloseModal}
            aria-labelledby="modal-modal-title"
            aria-describedby="modal-modal-description"
            closeAfterTransition
            BackdropComponent={Backdrop}
            BackdropProps={{ timeout: 500 }}>
            <Box sx={{...style, ...props.sx }}>
                {props.children}
            </Box>
        </Modal>
    );
}

export default ModalWindow;