import * as React from 'react';
import { useState } from 'react';
import { ToggleButton, ToggleButtonGroup } from '@mui/material';

// Selector for choosing days of the week
//      initialWeekdays         - string or array of strings with initial weekdays selected
//      allowMultipleSelection  - ability to choose more than one option (default true)
//      disabled                - disable component on/off
//      onWeekdaysChanged       - triggered when toggle button is clicked

const WeekdaysSelector = ({ initialWeekdays, allowMultipleSelection, disabled, onWeekdaysChanged }) => {
    const allWeekdays = [
        {
            label: "Monday",
            shortLabel: "Mon",
            value: "mon"
        },
        {
            label: "Tuesday",
            shortLabel: "Tue",
            value: "tue"
        },
        {
            label: "Wednesday",
            shortLabel: "Wed",
            value: "wed"
        },
        {
            label: "Thursday",
            shortLabel: "Thu",
            value: "thu"
        },
        {
            label: "Friday",
            shortLabel: "Fri",
            value: "fri"
        },
        {
            label: "Saturday",
            shortLabel: "Sat",
            value: "sat"
        },
        {
            label: "Sunday",
            shortLabel: "Sun",
            value: "sun"
        }
    ];

    const [weekdays, setWeekdays] = useState(initialWeekdays);


    const handleWeekdaysChanged = (event, newWeekdays) => {
        setWeekdays(newWeekdays);

        if (onWeekdaysChanged)
            onWeekdaysChanged(newWeekdays);
    };

    return (
        <ToggleButtonGroup
            value={weekdays}
            onChange={handleWeekdaysChanged}
            exclusive={!allowMultipleSelection}
            disabled={disabled}
            aria-label="weekdays selected"
        >
            {allWeekdays.map((weekday) => {
                return (
                    <ToggleButton
                        key={weekday.value}
                        value={weekday.value}
                        aria-label={weekday.label}
                    >
                        {weekday.shortLabel}
                    </ToggleButton>
                );
            })}
        </ToggleButtonGroup>
    );
}

WeekdaysSelector.defaultProps = {
    allowMultipleSelection: true,
    initialWeekdays: ['mon', 'fri']
};

export default WeekdaysSelector;