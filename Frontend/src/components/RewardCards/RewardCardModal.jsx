import * as React from 'react';
import ModalWindow from '../Modal/ModalWindow';
import RewardCard from './RewardCard';

const RewardCardModal = ({ isOpen, card, onClose }) => {
    return (
        <ModalWindow isOpen={isOpen} sx={{maxWidth: 900}} onClose={onClose}>
            <RewardCard card={card} displayMode='full' />
        </ModalWindow>
    );
}

export default RewardCardModal;