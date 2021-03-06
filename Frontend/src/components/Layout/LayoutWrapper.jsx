import React, { useState, useMemo, createContext } from 'react';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { CssBaseline, Box, Toolbar, Container } from '@mui/material';
import { Scrollbar } from "react-scrollbars-custom";

import AdapterMoment from '@mui/lab/AdapterMoment';
import LocalizationProvider from '@mui/lab/LocalizationProvider';

import CustomRouter from './Navigation/CustomRouter';
import CustomAppBar from './CustomAppBar';
import CustomDrawer from './CustomDrawer';

import useDisplayOptions from '../../Hooks/useDisplayOptions';

export const ColorModeContext = createContext({
    toggleColorMode: () => { }
});

const LayoutWrapper = () => {
    const [open, setOpen] = useState(false);
    const toggleDrawer = () => setOpen(!open);
    const [displayOptions, setDisplayOptions] = useDisplayOptions();
    const [mode, setMode] = useState(() => (displayOptions.isDarkMode ? 'dark' : 'light'));

    const colorMode = useMemo(
        () => ({
            toggleColorMode: () => {
                setMode((prevMode) => {
                    return prevMode === 'light' ? 'dark' : 'light';
                });
            },
        }),
        []
    );

    // Re-create theme evey time "mode" changes
    const theme = useMemo(
        () => createTheme({
            palette: {
                mode
            }
        }),
        [mode]
    );

    return (
        <ColorModeContext.Provider value={colorMode}>
            <ThemeProvider theme={theme}>
                <Box sx={{ display: "flex" }}>
                    <CssBaseline />

                    <CustomAppBar
                        title="Posthuman"
                        open={open}
                        onToggleDrawerClicked={toggleDrawer} />

                    <CustomDrawer
                        open={open}
                        onToggleDrawerClicked={toggleDrawer} />

                    <Box component="main" sx={{
                        backgroundColor: (theme) =>
                            theme.palette.mode === "light"
                                ? theme.palette.grey[100]
                                : theme.palette.grey[900],
                        flexGrow: 1,
                        height: "100vh",
                        overflow: "hidden"
                    }}>

                        <Toolbar />

                        <Scrollbar id='contentScrollbar' style={{ width: '100%' }}>
                            <Container maxWidth="false" sx={{ mt: 4, mb: 4 }}>
                                <LocalizationProvider dateAdapter={AdapterMoment}>
                                    <CustomRouter />
                                </LocalizationProvider>
                            </Container>
                        </Scrollbar>
                    </Box>
                </Box>
            </ThemeProvider>
        </ColorModeContext.Provider>
    );
}

export default LayoutWrapper;