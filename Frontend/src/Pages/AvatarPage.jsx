import * as React from 'react';
import { useContext } from 'react'
import { Grid } from '@mui/material';
import AvatarView from '../components/Avatar/AvatarView';
import SelectAvatar from '../components/Avatar/SelectAvatar';
import { AvatarContext } from '../App';
import TechnologyCardsList from '../components/TechnologyCards/TechnologyCardsList'

const AvatarPage = () => {
    const { activeAvatar } = useContext(AvatarContext);

    return (
        <React.Fragment>
            <Grid container spacing={3}>
                <Grid item xs={12} md={12} lg={6} xl={4}>
                    <AvatarView avatar={activeAvatar} viewMode='full'/>
                </Grid>
                <Grid item xs={12} md={12} lg={6} xl={4}>
                    <TechnologyCardsList category='1' title='Technologies' showHiddenCard={1} />
                </Grid>
                <Grid item xs={12} md={12} lg={6} xl={4}>
                    <TechnologyCardsList category='2' title='Team' showHiddenCard={0} />
                    <SelectAvatar />
                </Grid>
            </Grid>
        </React.Fragment>
    );
}

export default AvatarPage;