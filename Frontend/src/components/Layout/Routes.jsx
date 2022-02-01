import * as React from "react";

import AccessibilityIcon from '@mui/icons-material/Accessibility';
import EventNoteIcon from '@mui/icons-material/EventNote';
import AccountTreeIcon from '@mui/icons-material/AccountTree';
import CheckIcon from '@mui/icons-material/Check';
import DashboardIcon from '@mui/icons-material/Dashboard';
import AnnouncementOutlinedIcon from '@mui/icons-material/AnnouncementOutlined';
import InfoOutlinedIcon from '@mui/icons-material/InfoOutlined';
import StarOutlineIcon from '@mui/icons-material/StarOutline';
import VpnKeyIcon from '@mui/icons-material/VpnKey';

import LoginPage from '../../Pages/LoginPage';
import InfoPage from '../../Pages/InfoPage';
import BlogPage from '../../Pages/BlogPage';
import DashboardPage from '../../Pages/DashboardPage';
import AvatarPage from '../../Pages/AvatarPage';
import ProjectsPage from '../../Pages/ProjectsPage';
import TodoPage from '../../Pages/TodoPage';
import HistoryPage from '../../Pages/HistoryPage';
import TechnologyCardsPage from '../../Pages/TechnologyCardsPage';

const Routes = [
    {
        path: '/login',
        sidebarTitle: 'Login',
        sidebarIcon: () => <VpnKeyIcon />,
        destinationPage: () => <LoginPage />,
        isPrivate: false
    },
    {
        path: '/todo',
        sidebarTitle: 'Todo',
        sidebarIcon: () => <CheckIcon />,
        destinationPage: () => <TodoPage />,
        isPrivate: true
    },
    {
        path: '/avatar',
        sidebarTitle: 'Avatar',
        sidebarIcon: () => <AccessibilityIcon />,
        destinationPage: () => <AvatarPage />,
        isPrivate: true
    },
    {
        path: '/technologies',
        sidebarTitle: 'Technologies',
        sidebarIcon: () => <StarOutlineIcon />,
        destinationPage: () => <TechnologyCardsPage />,
        isPrivate: true
    },
    {
        path: '/info',
        sidebarTitle: 'Info',
        sidebarIcon: () => <InfoOutlinedIcon />,
        destinationPage: () => <InfoPage />,
        isPrivate: false
    },
    {
        path: '/blog',
        sidebarTitle: 'Blog',
        sidebarIcon: () => <AnnouncementOutlinedIcon />,
        destinationPage: () => <BlogPage />,
        isPrivate: false
    }
    // {
    //   path: '/dashboard',
    //   sidebarTitle: 'Dashboard',
    //   sidebarIcon: () => <DashboardIcon />,
    //   destinationPage: () => <DashboardPage />
    // },
    // {
    //   path: '/projects',
    //   sidebarTitle: 'Projects',
    //   sidebarIcon: () => <AccountTreeIcon />,
    //   destinationPage: () => <ProjectsPage />
    // },
    // {
    //   path: '/history',
    //   sidebarTitle: 'History',
    //   sidebarIcon: () => <EventNoteIcon />,
    //   destinationPage: () => <HistoryPage />
    // }
];

export default Routes;