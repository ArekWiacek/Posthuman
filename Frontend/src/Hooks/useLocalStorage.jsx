import { useState, useEffect } from 'react';
import { LogI } from '../Utilities/Utilities';

const getSavedValue = (key, initialValue) => {
    const savedValue = localStorage.getItem(key);
    if(savedValue != 'undefined' && savedValue != null)
        return JSON.parse(savedValue);

    // if(savedValue)
    //     return savedValue;
    
    if(initialValue instanceof Function)
        return initialValue();
    else
        return initialValue;
};    

const useLocalStorage = (key, initialValue) => {
    const [value, setValue] = useState(() => {
        return getSavedValue(key, initialValue);
    });

    useEffect(() => {
        localStorage.setItem(key, JSON.stringify(value));
    }, [value]);

    return [value, setValue];
};

export default useLocalStorage;