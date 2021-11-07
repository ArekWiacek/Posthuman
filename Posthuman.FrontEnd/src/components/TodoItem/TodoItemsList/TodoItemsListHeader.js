import * as React from 'react';
import { TableRow, TableCell, TableHead } from '@mui/material';
 
const TodoItemsListHeader = () => {
    const todoItemsListColumns = [
        // { displayText: "ID" },
        { displayText: "Title" },
        { displayText: "Deadline" },
        // { displayText: "" },
        // { displayText: "Reward", align: "right" },
        // { displayText: "Project", align: "right" },
        // { displayText: "Parent task", align: "right" },
        { displayText: "Progress", align: "right" },
        { displayText: "Actions", align: "right" }
    ];

    return (
        <TableHead>
            <TableRow>
                {/* <TableCell>
                    <IconButton
                        aria-label='expand'
                        size='small'
                        onClick={() => setOpen(!open)}>
                            {open ? <ArrowDropUpIcon /> : <ArrowDropDownIcon />}
                    </IconButton>
                </TableCell> */}
                
                {todoItemsListColumns.map((column) => (
                    <TableCell align={column.align} key={column.displayText}>
                        {column.displayText}
                    </TableCell>
                ))}
            </TableRow>
        </TableHead>
    );
}

export default TodoItemsListHeader;