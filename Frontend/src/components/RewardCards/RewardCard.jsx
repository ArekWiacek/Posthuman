import * as React from 'react';
import {
    Box, Card, CardMedia, CardContent, Divider, Typography,
    ImageListItem, ImageListItemBar, IconButton
} from '@mui/material';
import InfoIcon from '@mui/icons-material/Info';
import { LogI } from '../../Utilities/Utilities';

const RewardCard = ({ card, displayMode, onCardClicked }) => {

    const isMini = () => {
        return displayMode === 'mini';
    };

    const isFullSize = () => {
        return displayMode === 'fullSize';
    };

    const handleCardClicked = () => {
        LogI(`Card of title: ${card.cliked}`);
        onCardClicked(card);
    };

    return (
        <React.Fragment>{
            isMini() ?
                (<ImageListItem key={card.imageUrl}>
                    <img
                        src={card.imageUrl}
                        srcSet={`${card.imageUrl}?w=248&fit=crop&auto=format&dpr=2 2x`}
                        loading="lazy" />
                    <ImageListItemBar
                        title={card.title}
                        subtitle={card.subtitle}
                        actionIcon={
                            <IconButton
                                sx={{ color: 'rgba(255, 255, 255, 0.54)' }}
                                onClick={handleCardClicked}>
                                <InfoIcon />
                            </IconButton>
                        }
                    />
                </ImageListItem>) :
                (<Card sx={{ opacity: card.isHidden ? '40%' : '100%' }}>
                    <CardMedia
                        component='img'
                        height={'250'}
                        image={card.imageUrl} />

                    <CardContent>
                        <Typography gutterBottom variant='h5' component='div'>
                            {card.title}
                        </Typography>
                        <Typography gutterBottom variant='h6'>
                            {card.subtitle}
                        </Typography>
                        <Box sx={{ textAlign: 'left' }}>
                            <Typography variant='body' dangerouslySetInnerHTML={{ __html: card.description }} />
                            <Divider />
                            <Typography variant='h6'>Available at level: {card.levelExpected}</Typography>
                        </Box>
                    </CardContent>
                </Card >)
        }</React.Fragment>
    );
}

export default RewardCard;