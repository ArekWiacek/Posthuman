import React from 'react';
import { useForm } from 'react-hook-form';
import * as Yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { Typography, Button, Paper } from '@mui/material';
import { LogI, LogE } from '../../Utilities/Utilities';
import { authenticationService } from '../../Services/authenticationService';
import FormInputText from '../Common/Forms/FormInputText';
import customStyles from '../Common/CustomStyles';
import useAuth from '../../Hooks/useAuth';

const defaultValues = {
    email: 'harki@gmail.com',
    password: '123456'
};

const validationSchema = Yup.object().shape({
    email: Yup.string().required('Email is required').email('Provide valid email'),
    password: Yup.string().required('Password is required')
});

const LoginForm = ({ onUserLogged }) => {
    const classes = customStyles.formStyles();
    const formOptions = { resolver: yupResolver(validationSchema), defaultValues: defaultValues };
    const { handleSubmit, reset, control } = useForm(formOptions);
    const { login, loading, error } = useAuth();
    const onSubmit = (formData) => {
        login(formData.email, formData.password);
        // authenticationService.login(formData.email, formData.password, loggedUser => {
        //     LogI(`Logging successfull for user with email: '${loggedUser.email}'`);
        //     reset({ defaultValues }); 
        //     if(onUserLogged)
        //         onUserLogged(loggedUser);   
        // }, error => {
        //     LogE(`Failed to login user with email: '${formData.name}'`, error);
        // });
    };

    return (
        <Paper component='form' className={classes.root} onSubmit={handleSubmit(onSubmit)} noValidate>
            <Typography variant='h6'>Continue adventure</Typography>
            <FormInputText name='email' control={control} label='Email' />
            <FormInputText name='password' control={control} label='Password' additional={{ type: 'password' }} />
            <Button variant='contained' type='submit'>Login</Button>
        </Paper>
    );
}

export default LoginForm;