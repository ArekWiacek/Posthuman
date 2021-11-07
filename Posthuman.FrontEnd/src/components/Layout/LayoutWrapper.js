import * as React from "react";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import CssBaseline from "@mui/material/CssBaseline";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import Container from "@mui/material/Container";

import CustomRouter from "./CustomRouter";
import CustomAppBar from './CustomAppBar';
import CustomDrawer from './CustomDrawer';

import AdapterMoment from '@mui/lab/AdapterMoment';
import LocalizationProvider from '@mui/lab/LocalizationProvider';

const mdTheme = createTheme({
  palette: {
     // mode: 'dark',
  },
});

const LayoutWrapper = () => {
  const [open, setOpen] = React.useState(false);
  const toggleDrawer = () => {
    setOpen(!open);
  };

  return (
    <ThemeProvider theme={mdTheme}>
      <Box sx={{ display: "flex" }}>
        <CssBaseline />

        <CustomAppBar
          title="Posthuman"
          open={open}
          onToggleDrawerClicked={toggleDrawer}
        />

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
          overflow: "auto"
        }}>

          <Toolbar />

          <Container maxWidth="false" sx={{ mt: 4, mb: 4 }}>
            <LocalizationProvider dateAdapter={AdapterMoment}>
              <CustomRouter />
            </LocalizationProvider>
          </Container>
        </Box>
      </Box>
    </ThemeProvider>
  );
}

export default LayoutWrapper;
