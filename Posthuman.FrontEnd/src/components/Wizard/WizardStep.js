import React from 'react';
import Button from '@mui/material/Button';

const WizardStep = (props) => { 
    const handleOnConfirm = () => props.onConfirm();
    const handleOnCancel = () => props.onCancel();

    return (
        <div>
            {props.children}

            { (props.cancelText && props.onCancel) ? (
                <Button
                    sx={{ m: 1, width: '30ch' }}
                    variant="contained"
                    onClick={handleOnCancel}
                    type="submit">
                    {props.cancelText}
                </Button>
                ) : ''}

            { (props.confirmText && props.onConfirm) ? (
                <Button
                    sx={{ m: 1, width: '30ch' }}
                    variant="contained"
                    onClick={handleOnConfirm}
                    type="submit">
                    {props.confirmText}
                </Button>
                ) : '' }
        </div>
    );
};

export default WizardStep;