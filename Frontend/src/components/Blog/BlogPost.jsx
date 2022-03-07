import * as React from 'react';
import { Box, Card, CardMedia, CardContent, Typography } from '@mui/material';
import moment from 'moment';
import CardWithImage from '../Common/CardWithImage';

const BlogPost = (props) => {
    const { post } = props;
    const defaultDateFormat = 'DD.MM.YYYY';
    const date = moment(post.publishDate).format(defaultDateFormat);

    return (
        <React.Fragment>
            <CardWithImage sx={{ opacity: 60 }} imgSrc={post.imageUrl}>
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
            </CardWithImage>

            {/* <Card sx={{ maxWidth: 600 }}>
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
            </Card> */}
            </React.Fragment>
    );
}

export default BlogPost;