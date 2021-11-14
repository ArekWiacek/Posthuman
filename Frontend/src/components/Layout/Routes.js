import * as React from "react";

import AccessibilityIcon from '@mui/icons-material/Accessibility';
import EventNoteIcon from '@mui/icons-material/EventNote';
import AccountTreeIcon from '@mui/icons-material/AccountTree';
import CheckIcon from '@mui/icons-material/Check';
import DashboardIcon from '@mui/icons-material/Dashboard';
import AnnouncementIcon from '@mui/icons-material/Announcement';

import BlogPage from '../../Pages/BlogPage';
import DashboardPage from '../../Pages/DashboardPage';
import AvatarPage from '../../Pages/AvatarPage';
import ProjectsPage from '../../Pages/ProjectsPage';
import TodoPage from '../../Pages/TodoPage';
import HistoryPage from '../../Pages/HistoryPage';

const Routes = [
  {
    path: '/blog',
    sidebarTitle: 'Blog',
    sidebarIcon: () => { return <AnnouncementIcon />; },
    destinationPage: () => { return <BlogPage />; }
  },
  // {
  //   path: '/dashboard',
  //   sidebarTitle: 'Dashboard',
  //   sidebarIcon: () => { return <DashboardIcon />; },
  //   destinationPage: () => { return <DashboardPage />; }
  // },
  {
    path: '/avatar',
    sidebarTitle: 'Avatar',
    sidebarIcon: () => { return <AccessibilityIcon />; },
    destinationPage: () => { return <AvatarPage />; }
  },
  {
    path: '/todo',
    sidebarTitle: 'Todo',
    sidebarIcon: () => { return <CheckIcon />; },
    destinationPage: () => { return <TodoPage />; }
  },
  {
    path: '/projects',
    sidebarTitle: 'Projects',
    sidebarIcon: () => { return <AccountTreeIcon />; },
    destinationPage: () => { return <ProjectsPage />; }
  },
  {
    path: '/history',
    sidebarTitle: 'History',
    sidebarIcon: () => { return <EventNoteIcon />; },
    destinationPage: () => { return <HistoryPage />; }
  }
];

export default Routes;