import React, { useState, useEffect, useContext } from 'react';
import { Grid } from '@mui/material';
import TechnologyCardList from '../components/TechnologyCards/TechnologyCardsList';
import TechnologyCard from '../components/TechnologyCards/TechnologyCard';
import Api from '../Utilities/ApiHelper';
import useAvatar from '../Hooks/useAvatar';

const TechnologyCardsPage = () => {
    const technologyCardsEndpointName = "TechnologyCards";
    const { avatar } = useAvatar();
    const [technologyCards, setTechnologyCards] = useState([]);

    const hiddenCard = {
        imageUrl: '/Assets/Images/TechnologyCards/next.jpg',
        title: 'Card hidden',
        subtitle: 'Card hidden...',
        description: 'Reach 15 level to unlock the card',
        levelExpected: 15,
        isHidden: true
    };

    useEffect(() => {
        console.log("!!! TEH CARDS PAGE EVVECT");
        if (avatar != null && avatar != undefined && avatar.id) {
            console.log("!!! TEH CARDS PAGE Avatar: ");
            console.log(avatar);
            Api.Get(technologyCardsEndpointName + '/' + avatar.id, technologyCards => {
                setTechnologyCards(technologyCards);
            });
        }
    }, [avatar]);

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