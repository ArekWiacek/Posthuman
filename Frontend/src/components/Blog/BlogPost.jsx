import * as React from 'react';
import { Card, CardMedia, CardContent, Typography } from '@mui/material';

const BlogPost = ({ post }) => {
    return (
        <Card sx={{ maxWidth: 600 }}>
            <CardMedia
                component="img"
                height="250"
                image={post.imageUrl}
                alt="Cyborg" />
            <CardContent>
                <Typography gutterBottom variant="h5" component="div">
                    {post.title}
                </Typography>
                <Typography gutterBottom variant="h6">
                    {post.subtitle}
                </Typography>
                <Typography variant="h5" color="text.secondary">
                    Features
                </Typography>

                <React.Fragment>
                    {
                        post.sections.map(section => {
                            return (
                                <React.Fragment>
                                    <Typography variant="h6" sx={{ textAlign: 'left' }}>
                                        {section.title}
                                    </Typography>
                                    <Typography component="span" sx={{ textAlign: 'left' }}>
                                        <ul>
                                            {
                                                section.features.map(feature => {
                                                    return (
                                                        <li>{feature}</li>
                                                    );
                                                })
                                            }
                                        </ul> 
                                    </Typography>
                                </React.Fragment>
                            );
                        })
                    }
                </React.Fragment>
            </CardContent>
        </Card>
    );
}

export default BlogPost;