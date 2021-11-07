import * as React from 'react';
import {
    Card, CardContent, CardMedia, CardActions, Button,
    Typography, Divider, Stack, LinearProgress, Box
} from '@mui/material';
import { AvatarContext } from "../../App";


const AvatarView = ({ avatar }) => {
    const { activeAvatar } = React.useContext(AvatarContext);
    const calculateNewLevelProgress = () => { return 67; };

    return (
        <Card sx={{ maxWidth: 400 }}>
            <CardMedia
                component="img"
                height="140"
                image="cyborg_brain.jpg"
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
                    <Typography>1000 XP</Typography>
                </Stack>
                <Box sx={{ width: '100%' }}>
                    <LinearProgress variant="determinate" value={calculateNewLevelProgress()} />
                </Box>
            </CardContent>

            <Divider />

            <CardContent>
                <Typography variant="body2" color="text.secondary">XXX XP gained this week</Typography>
                <Typography variant="body2" color="text.secondary">YYY XP gained last week</Typography>
            </CardContent>

            <Divider />

            <CardContent>
                <Typography variant="h5" component="div">
                    Cyborg
                </Typography>
                <Typography variant="body2" color="text.secondary">
                    {avatar.bio ? avatar.bio : ''}
                    Cyborg is a futuristic fusion of cybernetic and organism-like being,
                    with both organic and biomechatronic body parts.
                </Typography>
            </CardContent>
        </Card>
    );
}

export default AvatarView;