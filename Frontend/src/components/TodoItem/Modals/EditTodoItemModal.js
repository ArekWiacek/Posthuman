import * as React from 'react';
import ModalWindow from '../../Modal/ModalWindow';
import EditTodoItemForm from '../Forms/EditTodoItemForm';

const EditTodoItemModal = ({ isOpen, currentTodoItem, todoItems, projects, onSaveEdit, onCancelEdit, onClose }) => {
    return (
        <ModalWindow 
            isOpen={isOpen}
            onClose={onClose}
            sx={{ width: '400px' }}>
            <EditTodoItemForm 
                currentTodoItem={currentTodoItem}
                todoItems={todoItems}
                projects={projects}
                onSaveEdit={onSaveEdit}
                onCancelEdit={onCancelEdit} />
        </ModalWindow>
    );
}

export default EditTodoItemModal;