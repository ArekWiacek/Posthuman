import * as React from 'react';
import ModalWindow from '../Modal/ModalWindow';
import Wizard from '../Wizard/Wizard';

const ConfirmTodoItemDoneModal = ({ isOpen, onFinished, onCanceled, todoItem, xpGained }) => {
//     const handleFinished = () => onFinished();
//     const handleCanceled = () => onCanceled();

    return (
        <ModalWindow isOpen={isOpen}>
            <Wizard 
                todoItem={todoItem} 
                xpGained={xpGained}
                onWizardFinished={onFinished} 
                onWizardCanceled={onCanceled} />
        </ModalWindow>
    );
}

export default ConfirmTodoItemDoneModal;