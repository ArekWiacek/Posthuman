import * as React from 'react';
import {useContext} from 'react'
import Grid from '@mui/material/Grid';
import AvatarView from '../components/Avatar/AvatarView';
import { AvatarContext } from '../App';
import { LogT } from '../Utilities/Utilities';

const AvatarPage = () => {
    LogT("AvatarPage.Constructor");

    const { activeAvatar, setActiveAvatar } = useContext(AvatarContext);
    const avatar = activeAvatar;

    return (
        <Grid container spacing={3}>
            <Grid item xs={12} md={12} lg={12}>
                <AvatarView avatar={avatar} />
            </Grid>
        </Grid>
    );
}

export default AvatarPage;