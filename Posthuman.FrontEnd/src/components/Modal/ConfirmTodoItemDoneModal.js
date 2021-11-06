import * as React from 'react';
import ModalWindow from '../Modal/ModalWindow';
import Wizard from '../Wizard/Wizard';

const ConfirmTodoItemDoneModal = ({ isOpen, onFinished, onCanceled, todoItem }) => {
    const handleFinished = () => onFinished();
    const handleCanceled = () => onCanceled();

    return (
        <ModalWindow
            isOpen={isOpen}
            onClose={handleCanceled}>

            <Wizard 
                todoItem={todoItem} 
                onWizardFinished={handleFinished} 
                onWizardCanceled={handleCanceled} />
        
        </ModalWindow>
    );
}

export default ConfirmTodoItemDoneModal;