import * as React from 'react';
import { Box, Modal, Backdrop } from '@mui/material';

const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    //width: 600,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4, 
    //backgroundImage: 'url("/Assets/Graphics/Eyecandies/eyecandy1.svg")',    
    backgroundRepeat: 'no-repeat'
    // backgroundPosition: 'right top'
};

// Custom modal window encapsulating application sci-fi style adding eyecandies 
// into background and manages general modal behaviour
const ModalWindow = (props) => {
    const handleCloseModal = () => {
        if(props.onClose)
            props.onClose();
    }

    const eyecandyUrl = 'url("/Assets/Graphics/Eyecandies/' + props.eyecandy + '.svg")';

    return (
        <Modal
            open={props.isOpen}
            onClose={handleCloseModal}
            aria-labelledby="modal-modal-title"
            aria-describedby="modal-modal-description"
            closeAfterTransition
            BackdropComponent={Backdrop}
            BackdropProps={{ timeout: 500 }}>
            <Box sx={{...style, backgroundImage: eyecandyUrl, ...props.background }}>
                {props.children}
            </Box>
        </Modal>
    );
}

export default ModalWindow;