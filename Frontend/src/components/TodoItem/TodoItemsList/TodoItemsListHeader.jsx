import * as React from 'react';
import { TableRow, TableCell, TableHead } from '@mui/material';
import useWindowDimensions from '../../../Hooks/useWindowDimensions';

const TodoItemsListHeader = () => {    
    const { isXs } = useWindowDimensions();

    const todoItemsListColumns = [{ 
        displayText: "Title" 
    }, 
    { 
        displayText: "Deadline", 
        display: {
            xs: 'none',
            md: 'table-cell'
        } 
    },
    { 
        displayText: "Progress", 
        align: "right",
        display: {
            xs: 'none',
            lg: 'table-cell'
        } 
    },

    { 
        displayText: "Actions", 
        align: "right" 
    }];

    return (
        <TableHead>
            <TableRow>
                {todoItemsListColumns.map((column) => (
                    <TableCell 
                        align={column.align} 
                        key={column.displayText}
                        sx={{ fontSize: '1.2rem', padding: 2,
                        display: column.display }} >
                        {column.displayText}
                    </TableCell>
                ))}
            </TableRow>
        </TableHead>
    );
}

export default TodoItemsListHeader;