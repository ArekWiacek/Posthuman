import React, { useState, useEffect, useContext } from 'react';
import { Grid } from '@mui/material';
import RewardCard from '../components/RewardCards/RewardCard';
import Api from '../Utilities/ApiHelper';
import { AvatarContext } from './../App';

const RewardCardsPage = () => {
    const rewardCardsEndpointName = "RewardCards";
    const { activeAvatar } = useContext(AvatarContext);
    const [rewardCards, setRewardCards] = useState([]); 

    const hiddenCard = {
        imageUrl: '/Assets/Images/RewardCards/next.jpg',
        title: 'Card hidden',
        subtitle: 'Card hidden...',
        description: 'Reach 15 level to unlock the card',
        levelExpected: 15,
        isHidden: true
    };

    useEffect(() => {
        Api.Get(rewardCardsEndpointName + '/' + activeAvatar.id , rewardCards => {
            setRewardCards(rewardCards);
        });
    }, [activeAvatar]);

    return (
        <Grid container spacing={3}>
            <Grid item xs={12} md={6} lg={4}>
                <RewardCard rewardCard={hiddenCard} />
            </Grid>
            {
                rewardCards.map(rewardCard => (
                    <Grid item xs={12} md={6} lg={4} key={rewardCard.id}>
                        <RewardCard rewardCard={rewardCard} />
                    </Grid>
                ))
            }
        </Grid>
    );
}

export default RewardCardsPage;