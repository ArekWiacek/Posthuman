import * as React from 'react';
import ModalWindow from '../../Modal/ModalWindow';
import EditTodoItemForm from '../Forms/EditTodoItemForm';

const EditTodoItemModal = ({ isOpen, todoItemToEdit, todoItems, projects, onSaveEdit, onCancelEdit, onClose }) => {
    return (
        <ModalWindow 
            isOpen={isOpen}
            onClose={onClose}
            sx={{ width: '400px' }}>
            <EditTodoItemForm 
                todoItemToEdit={todoItemToEdit}
                todoItems={todoItems}
                projects={projects}
                onSaveEdit={onSaveEdit}
                onCancelEdit={onCancelEdit} />
        </ModalWindow>
    );
}

export default EditTodoItemModal;