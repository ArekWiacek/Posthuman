import * as React from 'react';
import { TableRow, TableCell, TableHead } from '@mui/material';
 
const TodoItemsListHeader = () => {
    const todoItemsListColumns = [
        { displayText: "Title" },
        { displayText: "Deadline" },
        { displayText: "Progress", align: "right" },
        { displayText: "Actions", align: "right" }
    ];

    return (
        <TableHead>
            <TableRow>
                {todoItemsListColumns.map((column) => (
                    <TableCell 
                        align={column.align} 
                        key={column.displayText}
                        sx={{ fontSize: '1.2rem', padding: 2 }}
                    >
                        {column.displayText}
                    </TableCell>
                ))}
            </TableRow>
        </TableHead>
    );
}

export default TodoItemsListHeader;