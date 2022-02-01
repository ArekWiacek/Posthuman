import React from 'react';
import { useForm } from 'react-hook-form';
import * as Yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { Typography, Button, Paper } from '@mui/material';
import { LogI, LogE } from '../../Utilities/Utilities';
import FormInputText from '../Common/Forms/FormInputText';
import customStyles from '../Common/CustomStyles';
import useAuth from '../../Hooks/useAuth';

const defaultValues = {
    name: 'Harki',
    email: 'harki@gmail.com',
    password: '123456',
    confirmPassword: '123456'
};

const validationSchema = Yup.object().shape({
    name: Yup.string().required('Name is required'),
    email: Yup.string().required('Email is required').email('Provide valid email'),
    password: Yup.string().required('Password is required').min(6, 'Password must be at least 6 characters'),
    confirmPassword: Yup.string().required('Confirm Password is required').oneOf([Yup.ref('password')], 'Passwords must match')
});

const RegisterForm = ({ onUserRegistered }) => {
    const classes = customStyles.formStyles();
    const formOptions = { resolver: yupResolver(validationSchema), defaultValues: defaultValues };
    const { handleSubmit, reset, control } = useForm(formOptions);
    const { register } = useAuth();

    const onSubmit = (formData) => {
        register(formData);
    };

    return (
        <Paper component='form' className={classes.root} onSubmit={handleSubmit(onSubmit)} noValidate>
            <Typography variant='h6'>Start new journey</Typography>
            <FormInputText name='name' control={control} label='Name' additional={{ autoFocus: true }} />
            <FormInputText name='email' control={control} label='Email' />
            <FormInputText name='password' control={control} label='Password' additional={{ type: 'password' }} />
            <FormInputText name='confirmPassword' control={control} label='Confirm password' additional={{ type: 'password' }} />
            <Button variant='contained' type='submit'>Register</Button>
        </Paper>
    );
}

export default RegisterForm;