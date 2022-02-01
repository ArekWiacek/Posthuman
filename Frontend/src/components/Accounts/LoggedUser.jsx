import React, { useState, useEffect } from 'react';
import { useHistory } from "react-router-dom";
import { Box, Button, Typography } from '@mui/material';
import useAuth from '../../Hooks/useAuth';
import useAvatar from '../../Hooks/useAvatar';
import Api from '../../Utilities/ApiHelper';

const LoggedUser = () => {
    const history = useHistory();

    // User info is taken from global context and local storage, 
    // Avatar is 'game' entity, so it's taken from webapi separately only for scope of this component
    const { user, logout } = useAuth();
    const { avatar } = useAvatar();

    const handleLogoutClick = () => {
        logout();
    };

    const handleLoginClick = () => {
        history.push('/login');
    };

    const renderAuthButton = () => {
        if(user != null && user != undefined) 
            return (
            <>
                <Typography>Logged as: {user.name ? user.name : user.email},&nbsp; 
                Avatar: {avatar != null && avatar != undefined ? (avatar.name ? avatar.name : 'No avatar is set') : 'no avatar at all'}
                </Typography>
                <Button onClick={handleLogoutClick}>Logout</Button>
            </>)
        else 
            return <Button onClick={handleLoginClick}>Login</Button>
    };

    return (
        <Box sx={{ display: 'flex', flexDirection: 'row', alignItems: 'center' }}>
            {renderAuthButton()}
        </Box>
    );
}

export default LoggedUser;