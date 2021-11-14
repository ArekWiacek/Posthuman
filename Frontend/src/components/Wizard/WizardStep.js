import React from 'react';
import Button from '@mui/material/Button';
import Box from '@mui/material/Box';

const WizardStep = (props) => {
    const style = {
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center'
    };
    
    const handleOnConfirm = () => props.onConfirm();
    const handleOnCancel = () => props.onCancel();

    const buttonsStyle = { ...style, flexDirection: 'row' }
    const displayCancelButton = props.cancelText && props.onCancel;
    const displayConfirmButton = props.confirmText && props.onConfirm;

    return (
        <Box sx={style}>
            {props.children}
            
            <Box sx={buttonsStyle}>
                { displayCancelButton ? ( 
                <Button
                        sx={{ m: 1, width: '30ch' }}
                        variant="contained" type="submit"
                        onClick={handleOnCancel} >
                        {props.cancelText}
                </Button> ) : ''}

                { displayConfirmButton ? (
                <Button
                        sx={{ m: 1, width: '30ch' }}
                        variant="contained" type="submit"
                        onClick={handleOnConfirm}>
                        {props.confirmText}
                </Button> ) : ''}
            </Box>
        </Box>
    );
};

export default WizardStep;