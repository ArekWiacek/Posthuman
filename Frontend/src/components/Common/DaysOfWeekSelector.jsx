import * as React from 'react';
import { useState } from 'react';
import { ToggleButton, ToggleButtonGroup } from '@mui/material';

// Selector for choosing days of the week
//      initialDays             - string or array of strings with initial days of week selected
//      allowMultipleSelection  - ability to choose more than one option (default true)
//      disabled                - disable component on/off
//      onDaysChanged           - triggered when day is checked/unchecked

const DaysOfWeekSelector = ({ initialDays, allowMultipleSelection, disabled, onDaysChanged }) => {
    const daysOfWeek = [
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

    const [days, setDays] = useState(initialDays);

    const handleDaysChanged = (event, newDays) => {
        setDays(newDays);

        if (onDaysChanged)
            onDaysChanged(newDays);
    };

    return (
        <ToggleButtonGroup
            value={days}
            onChange={handleDaysChanged}
            exclusive={!allowMultipleSelection}
            disabled={disabled}
            aria-label="weekdays selected"
        >
            {daysOfWeek.map((day) => {
                return (
                    <ToggleButton
                        key={day.value}
                        value={day.value}
                        aria-label={day.label}
                    >
                        {day.shortLabel}
                    </ToggleButton>
                );
            })}
        </ToggleButtonGroup>
    );
}

DaysOfWeekSelector.defaultProps = {
    allowMultipleSelection: true,
    initialDays: ['mon', 'fri']
};

export default DaysOfWeekSelector;