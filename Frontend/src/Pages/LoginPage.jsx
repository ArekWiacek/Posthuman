import React from 'react';
import { Box, Paper, Container, Grid, Typography } from '@mui/material';
import LoginForm from '../components/Accounts/LoginForm';
import RegisterForm from '../components/Accounts/RegisterForm';
import customStyles from '../components/Common/CustomStyles';

const LoginPage = () => {
    const classes = customStyles.loginPageStyles();

    return (
        <Container maxWidth='md'>
            <Grid container spacing={4}>
                <Grid item xs={12}>
                    <Paper>
                        <Grid container>
                            <Grid item sm={12} md={5}>
                                <Box
                                    className={classes.eyecandy}
                                    component="img" 
                                    src='/Assets/Images/Backgrounds/background3.jpg'
                                />
                            </Grid>
                            <Grid item sm={12} md={7} sx={{ padding: 2 }}>
                                <Typography variant='h6' gutterBottom>
                                    Welcome to Posthuman
                                </Typography>
                                <Typography gutterBottom>
                                    Reach the new peaks of your life by taking part in an awesome futuristic adventure
                                </Typography>
                                <Typography>
                                    Below you can create new account or sign into existing one
                                </Typography>
                            </Grid>
                        </Grid>
                    </Paper>
                </Grid>

                <Grid item xs={12} sm={12} md={6}>
                    <RegisterForm />
                </Grid>

                <Grid item xs={12} sm={12} md={6}>
                    <LoginForm />
                </Grid>
            </Grid>
        </Container>
    );
};

export default LoginPage;