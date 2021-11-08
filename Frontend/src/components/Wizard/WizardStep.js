import React from 'react';
import Button from '@mui/material/Button';
import Box from '@mui/material/Box';

const style = {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center'
};

const buttonsStyle = {
    display: 'flex',
    flexDirection: 'row',
    alignItems: 'center'
};

const WizardStep = (props) => {
    const handleOnConfirm = () => props.onConfirm();
    const handleOnCancel = () => props.onCancel();

    return (
        <Box sx={style}>
            {props.children}

            <Box sx={buttonsStyle}>
                {(props.cancelText && props.onCancel) ? (
                    <Button
                        sx={{ m: 1, width: '30ch' }}
                        variant="contained"
                        onClick={handleOnCancel}
                        type="submit">
                        {props.cancelText}
                    </Button>
                ) : ''}

                {(props.confirmText && props.onConfirm) ? (
                    <Button
                        sx={{ m: 1, width: '30ch' }}
                        variant="contained"
                        onClick={handleOnConfirm}
                        type="submit">
                        {props.confirmText}
                    </Button>
                ) : ''}
            </Box>
        </Box>
    );
};

export default WizardStep;