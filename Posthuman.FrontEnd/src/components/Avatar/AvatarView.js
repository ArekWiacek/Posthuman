import * as React from 'react';
import { Card, CardContent, CardMedia, CardActions, Button, Typography, Grid } from '@mui/material';

const AvatarView = ({ avatar }) => {
    return (
        // <Grid container spacing={2}>
        //     <Grid item xs={4}>
                    <Card sx={{ maxWidth: 345 }}>
                        <CardMedia
                            component="img"
                            height="140"
                            image="/static/images/cards/contemplative-reptile.jpg"
                            alt="green iguana"
                        />
                        <CardContent>
                            <Typography gutterBottom variant="h5" component="div">
                                Lizard
                            </Typography>
                            <Typography variant="body2" color="text.secondary">
                                Lizards are a widespread group of squamate reptiles, with over 6,000
                                species, ranging across all continents except Antarctica
                            </Typography>
                        </CardContent>
                        <CardActions>
                            <Button size="small">Share</Button>
                            <Button size="small">Learn More</Button>
                        </CardActions>
                    

                    <CardContent>
                        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
                            Your Avatar
                        </Typography>
                        <Typography variant="h5" component="div">
                            {avatar.name}
                        </Typography>
                        <Typography sx={{ mb: 1.5 }} color="text.secondary">
                            CYBORG
                            {/* TODO - change to some "range" - Neuron ArtificalInteligence etc */}
                        </Typography>
                        <Typography variant="body2">
                            {avatar.bio}
                        </Typography>
                    </CardContent>

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
    );
}

export default AvatarView;