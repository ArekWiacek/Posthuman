import React, { useState, useEffect } from 'react';
import { ImageList, ImageListItem, Typography, ListSubheader } from '@mui/material';
import TechnologyCard from './TechnologyCard';
import Api from '../../Utilities/ApiHelper';
import TechnologyCardModal from './TechnologyCardModal';
import * as ArrayHelper from '../../Utilities/ArrayHelper';
import useAvatar from '../../Hooks/useAvatar';

const TechnologyCardsList = ({ category, title, showHiddenCard }) => {
    const technologyCardsEndpointName = "TechnologyCards";
    const { avatar } = useAvatar();
    const [technologyCards, setTechnologyCards] = useState([]);
    const [clickedCard, setClickedCard] = useState({});
    const [cardModalOpen, setCardModalOpen] = useState(false);

    const hiddenCard = {
        imageUrl: '/Assets/Images/TechnologyCards/next.jpg',
        title: 'Hidden technology',
        subtitle: 'Reach level 15 to discover new tech',
        requiredLevel: 15,
        isHidden: true
    };

    const handleCardClicked = (card) => {
        setClickedCard({ ...card });
        setCardModalOpen(true);
    };

    const handleCloseCardModal = () => {
        setCardModalOpen(false);
    };

    useEffect(() => {
        if (avatar != null && avatar != undefined) {
            Api.Get(technologyCardsEndpointName + '/' + avatar.id + '/' + category, technologyCards => {
                var technologies = showHiddenCard ? ArrayHelper.InsertObjectAtIndex(technologyCards, hiddenCard, 0) : technologyCards;
                setTechnologyCards(technologies);
            });
        }
    }, [avatar]);

    return (
        <React.Fragment>
            <ImageList sx={{ mt: 0 }}>
                <ImageListItem key="CardsListHeader" cols={2}>
                    <ListSubheader component="div">
                        <Typography variant='h5'>{title}</Typography>
                    </ListSubheader>
                </ImageListItem>
                {technologyCards.map(technologyCard => (
                    <TechnologyCard key={technologyCard.imageUrl} card={technologyCard} onCardClicked={handleCardClicked} displayMode='mini' />
                ))}
            </ImageList>

            <TechnologyCardModal isOpen={cardModalOpen} card={clickedCard} onClose={handleCloseCardModal} />
        </React.Fragment>
    );
}

export default TechnologyCardsList;