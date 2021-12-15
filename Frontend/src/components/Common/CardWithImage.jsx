import * as React from 'react';
import { Card, CardMedia, CardContent, Typography } from '@mui/material';

const CardWithImage = (props) => {
    const { imgHeight, imgSrc, imgAlt, title, subtitle } = props;

    const renderTitle = (title) => {
        if (title)
            return <Typography variant='h5'>{title}</Typography>;
        else
            return;
    };

    const renderSubtitle = (subtitle) => {
        if (subtitle)
            return <Typography variant='h6'>{subtitle}</Typography>;
        else
            return;
    };

    return (
        <Card>
            <CardMedia
                component="img"
                height={imgHeight}
                image={imgSrc}
                alt={imgAlt} />
            <CardContent>
                {renderTitle(title)}
                {renderSubtitle(subtitle)}
                {props.children}
            </CardContent>
        </Card>
    );
}

export default CardWithImage;

CardWithImage.defaultProps = {
    title: "",
    subtitle: "",
    imgHeight: 250,
    imgAlt: ""    
};