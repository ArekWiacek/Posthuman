import * as React from 'react';
import { Box, Card, CardMedia, CardContent, Typography } from '@mui/material';
import moment from 'moment';

const BlogPost = (props) => {
    const { post } = props;
    const defaultDateFormat = 'DD.MM.YYYY';
    const date = moment(post.publishDate).format(defaultDateFormat);

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

                <Box sx={{ textAlign: 'left' }} dangerouslySetInnerHTML={{ __html: post.content }} />
                
                <Typography variant="h6">
                    Author: {post.author} @ {date}
                </Typography>
            </CardContent>
        </Card>
    );
}

export default BlogPost;