import React, { useState, useEffect, useContext } from 'react';
import { ImageList, ImageListItem, Typography, ListSubheader } from '@mui/material';
import TechnologyCard from './TechnologyCard';
import Api from '../../Utilities/ApiHelper';
import { AvatarContext } from '../../App';
import TechnologyCardModal from './TechnologyCardModal';
import * as ArrayHelper from '../../Utilities/ArrayHelper';

const TechnologyCardsList = ({ category, title, showHiddenCard }) => {
    const technologyCardsEndpointName = "TechnologyCards";
    const { activeAvatar } = useContext(AvatarContext);
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
        setClickedCard({ ...card});
        setCardModalOpen(true);
    };

    const handleCloseCardModal = () => {
        setCardModalOpen(false);
    };

    useEffect(() => {
        Api.Get(technologyCardsEndpointName + '/' + activeAvatar.id + '/' + category, technologyCards => {
            var technologies = showHiddenCard ? ArrayHelper.InsertObjectAtIndex(technologyCards, hiddenCard, 0) : technologyCards;
            setTechnologyCards(technologies);
        });
    }, [activeAvatar]);

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

            <TechnologyCardModal isOpen={cardModalOpen} card={clickedCard} onClose={handleCloseCardModal}/>
        </React.Fragment>
    );
}

export default TechnologyCardsList;