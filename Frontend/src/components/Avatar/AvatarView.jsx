import * as React from 'react';
import { useContext } from 'react';
import {
    Card, CardContent, CardMedia, Typography,
    Divider, Stack, LinearProgress, Box
} from '@mui/material';
import { AvatarContext } from "../../App";

const AvatarView = ({ avatar, viewMode }) => {
    const { activeAvatar } = useContext(AvatarContext);

    const calculateNewLevelProgress = () => {
        var levelProgress = ((activeAvatar.exp - activeAvatar.expToCurrentLevel) / (activeAvatar.expToNewLevel - activeAvatar.expToCurrentLevel)) * 100;
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
        <Card sx={{ maxWidth: 400, mb: 3  }}>
            <CardMedia
                component="img"
                height="140"
                image="/Assets/Images/cyborg_brain.jpg"
                alt="Cyborg brain" />
            <CardContent>
                <Typography gutterBottom variant="h4" component="div">
                    {avatar.name}
                </Typography>
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