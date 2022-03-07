import * as React from 'react';
import ModalWindow from '../Modal/ModalWindow';
import TechnologyCard from './TechnologyCard';

const TechnologyCardModal = ({ isOpen, card, onClose }) => {
    return (
        <ModalWindow isOpen={isOpen} sx={{maxWidth: 900}} onClose={onClose}>
            <TechnologyCard card={card} displayMode='full' />
        </ModalWindow>
    );
}

export default TechnologyCardModal;