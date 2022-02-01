import { makeStyles } from '@mui/styles';

const formStyles = makeStyles(theme => ({
    root: {
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'center',
        padding: theme.spacing(2),

        '& .MuiTextField-root': {
            margin: theme.spacing(1),
            width: '100%'
        },

        '& .MuiButtonBase-root': {
            margin: theme.spacing(2),
            width: '100%'
        }
    }
}));

const todoListStyles = makeStyles(theme => ({
    container: {
        height: '100%'
    },

    table: {
        '& .MuiTableRow-root:last-child .MuiTableCell-root': {
            borderBottom: 'none'
        }
    },

    footer: {
        display: 'flex', 
        flexDirection: 'row', 
        justifyContent: 'space-between', 
        paddingTop: theme.spacing(2), 
        paddingBottom: theme.spacing(2) 
    }
}));

const loginPageStyles = makeStyles(() => ({
    eyecandy: {
        height: '100%',
        width: '100%',
        display: { xs: 'none', sm: 'none', md: 'initial' }
    }
}));

const customStyles = {
    formStyles,
    todoListStyles,
    loginPageStyles
};

export default customStyles;