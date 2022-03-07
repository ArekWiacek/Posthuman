import React, { useState } from 'react';

const useForm = (initialFormState) => {
    const [formState, setFormState] = useState(initialFormState);

    const handleChange = (propertyName, newValue) => {
        setFormState({ ...formState, [propertyName]: newValue });
    };
    
    const handleInputChange = e => {
        const { name, value } = e.target;
        handleChange(name, value);
    };

    const handleToggleChange = e => {
        const name = e.target.name;
        const value = e.target.checked;
        handleChange(name, value);
    };

    const handleDateChange = (propertyName, newValue) => {
        handleChange(propertyName, newValue ? newValue.toDate() : null);
    };

    return {
        formState,
        setFormState, 
        handleInputChange,
        handleToggleChange,
        handleDateChange
    };
};

export default useForm;