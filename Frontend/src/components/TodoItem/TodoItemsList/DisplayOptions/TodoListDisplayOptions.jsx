import React, { useContext } from 'react';
import TodoListDisplayOptionsMenu from './TodoListDisplayOptionsMenu';

const TodoListDisplayOptions = ({ displayOptions, onOptionChanged }) => {

    const handleDisplayOptionsChanged = (option, value) => {
        onOptionChanged(option, value);
    };

    const options = [{
        id: `1`,
        title: 'Show hidden tasks',
        name: 'showHiddenTasks',
        isChecked: displayOptions.showHiddenTasks
    }, {
        id: '2',
        title: 'Show finished tasks',
        name: 'showFinishedTasks',
        isChecked: displayOptions.showFinishedTasks
    }, {
        id: `3`,
        title: 'Make list bigger',
        name: 'bigItems',
        isChecked: displayOptions.bigItems
    }, {
        id: `4`,
        title: 'Collapse menu',
        name: 'collapsedMenu',
        isChecked: displayOptions.collapsedMenu
    }, {
        id: `5`,
        title: 'Dark mode',
        name: 'isDarkMode',
        isChecked: displayOptions.isDarkMode //theme.palette.mode === 'dark'
    }];

    const renderActions = () => {
        return (
            <TodoListDisplayOptionsMenu 
                options={options}
                onOptionChanged={handleDisplayOptionsChanged} />
        );
    };

    return (
        <React.Fragment>
            {renderActions()}
        </React.Fragment>
    );
}

export default TodoListDisplayOptions;