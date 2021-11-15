import * as React from 'react';
import { useContext } from 'react'
import Grid from '@mui/material/Grid';
import AvatarView from '../components/Avatar/AvatarView';
import SelectAvatar from '../components/Avatar/SelectAvatar';
import { AvatarContext } from '../App';

const AvatarPage = () => {
    const { activeAvatar } = useContext(AvatarContext);

    return (
        <React.Fragment>
            <Grid container spacing={3}>
                <Grid item xs={12} md={12} lg={4}>
                    <AvatarView avatar={activeAvatar} />
                </Grid>
                <Grid item xs={12} md={12} lg={4}>
                    <SelectAvatar />
                </Grid>
            </Grid>
        </React.Fragment>
    );
}

export default AvatarPage;