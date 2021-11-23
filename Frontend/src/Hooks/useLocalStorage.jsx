import { useState, useEffect } from 'react';
import { LogI } from '../Utilities/Utilities';

const getSavedValue = (key, initialValue) => {
    const savedValue = JSON.parse(localStorage.getItem(key));
    if(savedValue)
        return savedValue;
    
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
        LogI("useLocalStorage effect called - dependent on it's value prop");
        localStorage.setItem(key, JSON.stringify(value));
    }, [value]);

    return [value, setValue];
};

export default useLocalStorage;