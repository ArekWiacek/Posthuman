import React from 'react';
import {
    Card, CardContent, CardMedia, Typography,
    Divider, Stack, LinearProgress, Box, Button
} from '@mui/material';
import useAvatar from '../../Hooks/useAvatar';

const AvatarView = ({ viewMode }) => {
    const { avatar, setAvatar } = useAvatar();

    const calculateNewLevelProgress = () => {
        var levelProgress = ((avatar.exp - avatar.expToCurrentLevel) / (avatar.expToNewLevel - avatar.expToCurrentLevel)) * 100;
        return Math.round(levelProgress);
    };

    const detailInfo = () => {
        if(viewMode === 'full')
        return (
            <React.Fragment>
                <Divider />

                <CardContent>
                    <Typography variant="body2" color="text.secondary">325 XP gained this week</Typography>
                    <Typography variant="body2" color="text.secondary">120 XP gained last week</Typography>
                </CardContent>

                <Divider />

                <CardContent>
                    <Typography variant="h5" component="div">
                        Bio
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {avatar.bio ? avatar.bio : ''}
                    </Typography>
                </CardContent>

                <CardContent>
                    <Typography variant="h5" component="div">
                        Cyborg
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        Cyborg is a futuristic fusion of cybernetic and organism-like being,
                        with both organic and biomechatronic body parts.
                    </Typography>
                </CardContent>
            </React.Fragment>);
    };

    return (
        <Card sx={{ mb: 3  }}>
            <CardMedia
                component="img"
                height="140"
                image="/Assets/Images/cyborg_brain.jpg"
                alt="Cyborg brain" />
            <CardContent>
                <Typography variant="h4" component="div">
                    {avatar.name ? avatar.name : '[Name]'}
                </Typography>
                <Divider />
                <Typography variant="h3" component="div">
                    {avatar.level} lvl
                </Typography>

                <Stack
                    direction="row"
                    justifyContent="space-between"
                    alignItems="flex-start"
                    spacing={2}>
                    <Typography>{avatar.exp} XP</Typography>
                    <Typography>{avatar.expToNewLevel} XP</Typography>
                </Stack>
                <Box sx={{ width: '100%' }}>
                    <LinearProgress variant="determinate" value={calculateNewLevelProgress()} />
                </Box>
            </CardContent>

            {detailInfo()}

        </Card>
    );
}

export default AvatarView;