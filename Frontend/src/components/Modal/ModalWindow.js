import * as React from 'react';
import { Box, Modal, Backdrop } from '@mui/material';
import { AvatarContext } from "../../App";

const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 600,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4,
};

const ModalWindow = (props) => {
    const handleCloseModal = () => props.onClose();

    return (
        <Modal
            open={props.isOpen}
            // onClose={handleCloseModal}
            aria-labelledby="modal-modal-title"
            aria-describedby="modal-modal-description"
            closeAfterTransition
            BackdropComponent={Backdrop}
            BackdropProps={{
                timeout: 500,
            }}
        >
            <Box sx={style}>
                {props.children}
                {/* <Wizard onWizardFinished={handleCloseModal} /> */}
            </Box> 
        </Modal>

        // <Modal
        //     open={open}
        //     onClose={handleOnCancelClicked}
        //     aria-labelledby="modal-modal-title"
        //     aria-describedby="modal-modal-description"
        // >
        //     <Box sx={style}>
        //         <Typography id="modal-modal-title" variant="h6" component="h2">
        //             Text in a modal
        //         </Typography>
        //         <Typography id="modal-modal-description" sx={{ mt: 2 }}>
        //             Duis mollis, est non commodo luctus, nisi erat porttitor ligula.
        //         </Typography>
        //     </Box>
        // </Modal>
    );
}

export default ModalWindow;