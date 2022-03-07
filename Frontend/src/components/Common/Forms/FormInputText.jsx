import React from 'react';
import { Controller } from 'react-hook-form';
import { TextField } from '@mui/material';

const FormInputText = ({ name, control, label, rules, additional }) => {
    return (
        <Controller 
            name={name}
            control={control}
            rules={rules}
            render={({ 
                field: { onChange, value },
                fieldState: { error },
                formState }) => (
                    <TextField
                        helperText={ error ? error.message : null }
                        error={!!error}
                        onChange={onChange}
                        // defaultValue={defaultValue}
                        value={value}
                        label={label}
                        variant='outlined'
                        autoComplete='none'
                        { ...additional } />
            )}
        />
    );
};

export default FormInputText;