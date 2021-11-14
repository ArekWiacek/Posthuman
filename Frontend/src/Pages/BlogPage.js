import * as React from 'react';
import { Card, CardMedia, CardContent, Typography } from '@mui/material';

const BlogPage = () => {
    return (
        <Card sx={{ maxWidth: 600 }}>
            <CardMedia
                component="img"
                height="140"
                image="/static/images/cards/contemplative-reptile.jpg"
                alt="green iguana"
            />
            <CardContent>
                <Typography gutterBottom variant="h5" component="div">
                    Version 0.1 released!
                </Typography>
                <Typography variant="body2" color="text.secondary">
                    We officially go online!

                    Features:
                        - Todo page 
                            - Create, edit, delete or update tasks
                            - Nest task creating tasks hierarchy
                            - Create subtasks inline
                            - Mark tasks as done
                        - Avatar page
                            - See your avatar with level and XP
                            - See your progress
                </Typography>
            </CardContent>
        </Card>
    );
}

export default BlogPage;