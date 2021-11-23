import { LogI } from '../Utilities/Utilities';
import useLocalStorage from './useLocalStorage';
import { useEffect } from 'react';

const defaultDisplayOptions = {
    showHiddenTasks: false,
    showFinishedTasks: false,
    bigItems: false,                // big / small table rows
    displayMode: 'hierarchical',    // hierarchical vs flat
    collapsedMenu: false,           // buttons spreaded or collapsed to menu 
    isDarkMode: false
};

const useDisplayOptions = () => {
    const displayOptionsKey = 'displayOptions';
    const [displayOptions, setDisplayOptions] = useLocalStorage(displayOptionsKey, defaultDisplayOptions);

    useEffect(() => {
        LogI("useDisplayOptions effect called - dependent on it's displayOptions");
        // nie no to zjebane
        //setDisplayOptions();
    }, [displayOptions]);

    return [displayOptions, setDisplayOptions];
}

export default useDisplayOptions;