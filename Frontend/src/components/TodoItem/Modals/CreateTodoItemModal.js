import * as React from 'react';
import ModalWindow from '../../Modal/ModalWindow';
import CreateTodoItemForm from '../Forms/CreateTodoItemForm';

const CreateTodoItemModal = ({ isOpen, todoItems, projects, parentTaskId, parentProjectId, onCreateTodoItem, onClose }) => {
    return (
        <ModalWindow 
            isOpen={isOpen}
            onClose={onClose}
            sx={{ width: '400px' }}>
            <CreateTodoItemForm 
                todoItems={todoItems}
                projects={projects}
                parentTaskId={parentTaskId}
                parentProjectId={parentProjectId}
                onCreateTodoItem={onCreateTodoItem} />
        </ModalWindow>
    );
}

export default CreateTodoItemModal;