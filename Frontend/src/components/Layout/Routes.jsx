import * as React from "react";

import AccessibilityIcon from '@mui/icons-material/Accessibility';
import EventNoteIcon from '@mui/icons-material/EventNote';
import AccountTreeIcon from '@mui/icons-material/AccountTree';
import CheckIcon from '@mui/icons-material/Check';
import DashboardIcon from '@mui/icons-material/Dashboard';
import AnnouncementOutlinedIcon from '@mui/icons-material/AnnouncementOutlined';
import InfoOutlinedIcon from '@mui/icons-material/InfoOutlined';

import HomePage from '../../Pages/HomePage';
import BlogPage from '../../Pages/BlogPage';
import DashboardPage from '../../Pages/DashboardPage';
import AvatarPage from '../../Pages/AvatarPage';
import ProjectsPage from '../../Pages/ProjectsPage';
import TodoPage from '../../Pages/TodoPage';
import HistoryPage from '../../Pages/HistoryPage';

const Routes = [
    {
        path: '/info',
        sidebarIcon: 'Info',
        sidebarIcon: () => <InfoOutlinedIcon />,
        destinationPage: () => <HomePage />
    },
    {
        path: '/blog',
        sidebarTitle: 'Blog',
        sidebarIcon: () => <AnnouncementOutlinedIcon />,
        destinationPage: () => <BlogPage />
    },
    // {
    //   path: '/dashboard',
    //   sidebarTitle: 'Dashboard',
    //   sidebarIcon: () => <DashboardIcon />,
    //   destinationPage: () => <DashboardPage />
    // },
    {
        path: '/avatar',
        sidebarTitle: 'Avatar',
        sidebarIcon: () => <AccessibilityIcon />,
        destinationPage: () => <AvatarPage />
    },
    {
        path: '/todo',
        sidebarTitle: 'Todo',
        sidebarIcon: () => <CheckIcon />,
        destinationPage: () => <TodoPage />
    }
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