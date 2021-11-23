import React, { useState, useEffect, useContext } from 'react';
import { ImageList, ImageListItem, Typography, ListSubheader } from '@mui/material';
import RewardCard from './RewardCard';
import Api from '../../Utilities/ApiHelper';
import { AvatarContext } from './../../App';
import RewardCardModal from './RewardCardModal';

const RewardCardsList = () => {
    const rewardCardsEndpointName = "RewardCards";
    const { activeAvatar } = useContext(AvatarContext);
    const [rewardCards, setRewardCards] = useState([]);
    const [clickedCard, setClickedCard] = useState({}); 
    const [cardModalOpen, setCardModalOpen] = useState(false);

    const hiddenCard = {
        imageUrl: '/Assets/Images/RewardCards/next.jpg',
        title: 'Card hidden',
        subtitle: 'Card hidden...',
        description: 'Reach 15 level to unlock the card',
        levelExpected: 15,
        isHidden: true
    };

    const handleCardClicked = (card) => {
        setClickedCard({ ...card});
        setCardModalOpen(true);
    };

    const handleCloseCardModal = () => {
        setCardModalOpen(false);
    };

    useEffect(() => {
        Api.Get(rewardCardsEndpointName + '/' + activeAvatar.id, rewardCards => {
            setRewardCards(rewardCards);
        });
    }, [activeAvatar]);

    return (
        <React.Fragment>
            <ImageList sx={{ mt: 0 }}>
                <ImageListItem key="CardsListHeader" cols={2}>
                    <ListSubheader component="div">
                        <Typography variant='h5'>Cards collection</Typography>
                    </ListSubheader>
                </ImageListItem>
                {rewardCards.map(rewardCard => (
                    <RewardCard key={rewardCard.imageUrl} card={rewardCard} onCardClicked={handleCardClicked} displayMode='mini' />
                ))}
            </ImageList>

            <RewardCardModal isOpen={cardModalOpen} card={clickedCard} onClose={handleCloseCardModal}/>

        </React.Fragment>
    );
}

export default RewardCardsList;