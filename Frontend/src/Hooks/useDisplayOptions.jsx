import useLocalStorage from './useLocalStorage';

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

    return [displayOptions, setDisplayOptions];
}

export default useDisplayOptions;