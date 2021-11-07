import * as React from 'react';
import { Card, CardContent, Typography, Grid } from '@mui/material';

const AvatarView = ({ avatar }) => {
    return (
        <Grid container spacing={2}>
            <Grid item xs={4}>
                <Card >
                    <CardContent>
                        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
                            Your Avatar
                        </Typography>
                        <Typography variant="h5" component="div">
                            {avatar.name}
                        </Typography>
                        <Typography sx={{ mb: 1.5 }} color="text.secondary">
                            Rookie 
                            {/* TODO - change to some "range" - Neuron ArtificalInteligence etc */}
                        </Typography>
                        <Typography variant="body2">
                            {avatar.bio}
                        </Typography>
                    </CardContent>
                    {/* <CardActions>
                        <Button size="small">Edit details</Button>
                    </CardActions> */}
                </Card>
            </Grid>
            <Grid item xs={4}>
                <Card >
                    <CardContent>
                        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
                            Level
                        </Typography>
                        <Typography variant="h5" component="div">
                            {avatar.level}
                        </Typography>
                        <Typography sx={{ mb: 1.5 }} color="text.secondary">
                            (sth about level)
                        </Typography>
                        <Typography variant="body2">
                            Gain exp to reach new level
                        </Typography>
                    </CardContent>
                </Card>
            </Grid>
            <Grid item xs={4}>
                <Card >
                    <CardContent>
                        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
                            Experience
                        </Typography>
                        <Typography variant="h5" component="div">
                            {avatar.exp} XP
                        </Typography>
                        <Typography sx={{ mb: 1.5 }} color="text.secondary">
                            Next level
                        </Typography>
                        <Typography variant="body2">
                            {avatar.exp} / 1000
                            {/* TODO - add calculating xp needed to reach new level */}
                        </Typography>
                    </CardContent>
                </Card>
            </Grid>
        </Grid>
    );
}

export default AvatarView;