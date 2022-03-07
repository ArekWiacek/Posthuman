import { LogI } from '../Utilities/Utilities';
import useLocalStorage from './useLocalStorage';
import { useEffect } from 'react';
import moment from 'moment';
import DefaultDateFormat from '../Utilities/Defaults';

const defaultDisplayOptions = {
    showHiddenTasks: false,
    showFinishedTasks: false,
    bigItems: false,                // big / small table rows
    displayMode: 'hierarchical',    // hierarchical vs flat
    collapsedMenu: false,           // buttons spreaded or collapsed to menu 
    isDarkMode: true,
    selectedDate: moment().format(DefaultDateFormat)
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