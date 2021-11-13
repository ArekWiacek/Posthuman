import * as React from 'react';
import { Box, Paper, TableContainer, Table, TableHead, 
    TableBody, TableRow, TableCell, TableFooter, TablePagination } from '@mui/material';
import Typography from '@mui/material/Typography'
import Grid from '@mui/material/Grid';
import moment from 'moment';
// import Moment from 'react-moment';

import { ApiGet } from '../Utilities/ApiRepository';

const HistoryPage = () => {
    const [eventItems, setEventItems] = React.useState([
        {
            id: 0,
            avatarId: 2,
            type: "Event type",
            occured: new Date()
        }
    ]);

    const getRewardText = (expGained) => {
        var rewardText = null;

        if (expGained > 0) {
            rewardText = (<Typography component="div" variant="body1">
                <Box sx={{ color: 'success.main' }}>+{expGained} XP</Box>
            </Typography>);
        }
        else if (expGained < 0) {
            rewardText = (<Typography component="div" variant="body1">
                <Box sx={{ color: 'error.main' }}>{expGained} XP</Box>
            </Typography>);
        }
        else {
            rewardText = (<Typography component="div" variant="body1">
                <Box sx={{ color: 'text.primary' }}>{expGained} XP</Box>
            </Typography>);
        }

        return rewardText;
    };

    const getEventTypeName = (eventItemType) => {
        switch (eventItemType) {
            case 1:
                return "Todo Item created";

            case 2:
                return "Todo Item modified";

            case 3:
                return "Todo Item deleted";

            case 4:
                return "Todo Item completed";

            case 5:
                return "Project created";

            case 6:
                return "Project modified";

            case 7:
                return "Project deleted";

            case 8:
                return "Projet finished";

            default:
                break;
        }
    };

    React.useEffect(() => {
        ApiGet("EventItems", data => setEventItems(data));
    }, []);

    return (
        <Grid container spacing={3}>
            <Grid item xs={12} md={12} lg={12}>
                <TableContainer component={Paper}>
                    <Typography variant="h5">Historical events</Typography>
                        <Table sx={{ minWidth: 600 }} size="small" aria-label="Historical events">
                            <TableHead>
                                <TableRow>
                                    <TableCell>Event type</TableCell>
                                    <TableCell>Details</TableCell>
                                    <TableCell align="right">Occured</TableCell>
                                    <TableCell align="right">Reward</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {eventItems.map((eventItem) => (
                                    <TableRow
                                        key={eventItem.id}
                                        sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                                        <TableCell component="th" scope="row" >
                                            {getEventTypeName(eventItem.type)}
                                        </TableCell>
                                        <TableCell>
                                            Entity ID: {eventItem.relatedEntityId} of type: {eventItem.relatedEntityType}
                                        </TableCell>
                                        <TableCell align="right">
                                            {/* <Moment format="DD.MM.YYYY">{eventItem.occured}</Moment> */}
                                        </TableCell>
                                        <TableCell align="right">
                                            {getRewardText(eventItem.expGained)}
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                            <TableFooter>
                            <TablePagination rowsPerPageOptions={[10, 50, { value: -1, label: 'All' }]} />
                            </TableFooter>
                        </Table>
                    </TableContainer>
            </Grid>
        </Grid>
    );
}

export default HistoryPage;