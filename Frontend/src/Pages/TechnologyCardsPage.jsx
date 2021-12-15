import React, { useState, useEffect, useContext } from 'react';
import { Grid } from '@mui/material';
import TechnologyCardList from '../components/TechnologyCards/TechnologyCardsList';
import TechnologyCard from '../components/TechnologyCards/TechnologyCard';
import Api from '../Utilities/ApiHelper';
import { AvatarContext } from '../App';

const TechnologyCardsPage = () => {
    const technologyCardsEndpointName = "TechnologyCards";
    const { activeAvatar } = useContext(AvatarContext);
    const [technologyCards, setTechnologyCards] = useState([]); 

    const hiddenCard = {
        imageUrl: '/Assets/Images/RewardCards/next.jpg',
        title: 'Card hidden',
        subtitle: 'Card hidden...',
        description: 'Reach 15 level to unlock the card',
        levelExpected: 15,
        isHidden: true
    };

    useEffect(() => {
        Api.Get(technologyCardsEndpointName + '/' + activeAvatar.id , technologyCards => {
            setTechnologyCards(technologyCards);
        });
    }, [activeAvatar]);

    return (
        <Grid container spacing={3}>
            <Grid item xs={12} md={6} lg={4}>
                <TechnologyCard card={hiddenCard} />
            </Grid>
            {
                technologyCards.map(technologyCard => (
                    <Grid item xs={12} md={6} lg={4} key={technologyCard.id}>
                        <TechnologyCard card={technologyCard} />
                    </Grid>
                ))
            }
        </Grid>
    );
}

export default TechnologyCardsPage;