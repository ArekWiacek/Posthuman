import * as React from 'react';
import { Card, CardMedia, CardContent, Typography } from '@mui/material';

const BlogPage = () => {
    return (
        <Card sx={{ maxWidth: 600 }}>
            <CardMedia
                component="img"
                height="250"
                image="/Assets/Images/cyborg.png"
                alt="Cyborg"
            />
            <CardContent>
                <Typography gutterBottom variant="h5" component="div">
                    Version 0.1 released
                </Typography>
                <Typography gutterBottom variant="h6">
                    We are officially going online, bitches! Whoop whoop!
                </Typography>
                <Typography variant="h5" color="text.secondary">
                    Features
                </Typography>
                <Typography variant="h6" sx={{ textAlign: 'left' }}>
                    Todo page
                </Typography>
                <Typography component="span" sx={{ textAlign: 'left' }}>
                    <ul>
                        <li>Use todo page to keep track of your everyday plans</li>
                        <li>Create, edit or delete tasks</li>
                        <li>Create inline subtasks fast and easy way (Enter and Escape buttons are your friends)</li> 
                        <li>Nest tasks inside each other to create hierarchy of more complex goals</li>
                        <li>See the progress of your activities</li>
                        <li>Change display options to fit your needs</li>
                    </ul>
                </Typography>
                
                <Typography variant="h6" sx={{ textAlign: 'left' }}>
                    Avatar page
                </Typography>
                <Typography component="span" sx={{ textAlign: 'left' }}>
                    <ul>
                        <li>See your Avatar - virtual representation of yourself</li>
                        <li>Gain experience points by completing tasks</li>
                        <li>Reach new levels both in game and life</li>    
                    </ul>
                </Typography>
            </CardContent>
        </Card>
    );
}

export default BlogPage;