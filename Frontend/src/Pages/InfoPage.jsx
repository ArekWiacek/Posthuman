import React, { } from 'react';
import { Grid, Typography, Divider, Box, Link } from '@mui/material';
import CardWithImage from '../components/Common/CardWithImage';

const InfoPage = () => {
    const sections = [
        {
            title: 'Posthuman.Core',
            subtitle: 'Model Layer',
            image: '/Assets/Images/Backgrounds/background3.jpg',
            content: `
            <ul>
                <li>Models definitions</li>
                <li>Entities, DTOs, Enums</li>
                <li>Repositories interfaces</li>
                <li>Services interfaces</li>
                <li>Unit of work interface</li>
            </ul>`
        }, {
            title: 'Posthuman.Data',
            subtitle: 'Data Layer',
            image: '/Assets/Images/Backgrounds/background1.jpg',
            content: `
            <ul>
                <li>Data context</li>
                <li>Data tables configuration</li>
                <li>Migrations</li>
                <li>Entity Framework integration</li>
                <li>Repositories implementation</li>
                <li>Unit of work implementation</li>
            </ul>`
        }, {
            title: 'Posthuman.Service',
            subtitle: 'Service Layer',
            image: '/Assets/Images/Backgrounds/background0.jpg',
            content:
                `<ul>
                <li>Actually it is 'game logic' layer</li>
                <li>Implementation of services encapsulating all game logic</li>
                <li>Implementation of game logic helpers, for example randomization system</li>
            </ul>`
        }, {
            title: 'Posthuman.WebApi',
            subtitle: 'Web API Layer',
            image: '/Assets/Images/Backgrounds/background2.jpg',
            content: `
            <ul>
                <li>ASP.NET Core Web API integration</li>
                <li>Web Api Controllers implementation</li>
                <li>Mappings done by Auto Mapper</li>
                <li>Logging implementation by log4net</li>
                <li>Environment type handling</li>
            </ul>`
        }, {
            title: 'Backend',
            image: '/Assets/Images/Backgrounds/background5.png',
            content:
                `<Typography variant='body'>
                <ul>
                    <li>C#</li>
                    <li>ASP.NET Core 3.1</li>
                    <li>ASP.NET WebAPI</li>
                    <li>Entity Framework</li>
                    <li>SignalR</li>
                    <li>AutoMapper</li>
                    <li>Microsoft SQL Server 2018</li>
                    <li>MS Visual Studio 2022</li>
                </ul>
            </Typography>`
        }, {
            title: 'Frontend',
            image: '/Assets/Images/Backgrounds/background4.png',
            content:
                `<Typography variant='body'>
                <ul>
                    <li>React</li>
                    <li>JavaScript</li>
                    <li>MaterialUI</li>
                    <li>MS Visual Studio Code</li>
                </ul>
            </Typography>`
        }];

    const cardSizes = {
        xs: 12,
        md: 6,
        lg: 4,
        xl: 3
    };

    return (
        <React.Fragment>
            <Grid container spacing={3} sx={{ textAlign: 'left' }}>
                <Grid item xs={12} lg={6}>
                    <Box sx={{ display: 'flex', flexDirection: 'column' }}>
                        <Typography gutterBottom variant='h4'>Technical information</Typography>
                        <Typography gutterBottom variant="body">
                            Below you will find technical information about this project - used architecture, design patterns, libraries and technologies.
                            This is useful mostly for developers.
                        </Typography>
                        <Typography gutterBottom variant="h5">Architecture</Typography>
                        <Typography gutterBottom variant="body">
                            Backend solution is divided into different layers to separate responsibility of different parts of app. Layers are :
                            Core (models), Data (here is entity framework implementation), Services (containing "game" logic) and Web API
                            (endpoint for outer world to communicate with frontend). Additionally there is additional layer implementing SignalR
                            library to communicate with frontend in real time - called... "RealTime". Below you have more details about each layer.
                            <br /><br />
                            Frontend application is written in React with MaterialUI.
                        </Typography>
                    </Box>
                </Grid>

                <Grid item xs={12} lg={6}>
                    <Typography gutterBottom variant="h5">Demo</Typography>
                    <Link href="https://posthuman.pl">https://posthuman.pl</Link><br />
                    <Link href="http://posthumanae-001-site1.itempurl.com">http://posthumanae-001-site1.itempurl.com</Link>
                    <br /><br />
                    <Typography gutterBottom variant="h5">Github</Typography>
                    <Link href="https://github.com/ArekWiacek/Posthuman">https://github.com/ArekWiacek/Posthuman</Link>
                </Grid>

                {
                    sections.map(section => (
                        <Grid item xs={cardSizes.xs} md={cardSizes.md} lg={cardSizes.lg} xl={cardSizes.xl}>
                            <CardWithImage
                                title={section.title}
                                subtitle={section.subtitle}
                                imgSrc={section.image}>
                                <Box dangerouslySetInnerHTML={{ __html: section.content }}></Box>
                            </CardWithImage>
                        </Grid>
                    ))
                }
            </Grid >
        </React.Fragment >
    );
}

export default InfoPage;

{
    /* <Grid item xs={12} lg={6}>
        <Typography variant='h4'>Frontend</Typography>
 
        <Divider />
        <Typography variant='h5'>Frontend React App</Typography>
        <Typography variant='h6'>Pages</Typography>
        <ul>
            <li>AvatarPage</li>
            <li>BlogPage</li>
            <li>InfoPage</li>
            <li>TechnologyCardsPage (in progress)</li>
            <li>TodoPage</li>
            <li>MaterialUI integrated</li>
        </ul>
        <Typography variant='h6'>Components</Typography>
        <ul>
            <li>Avatar: AvatarView, SelectAvatar</li>
            <li>Blog: BlogPost</li>
            <li>Common: ActionButton, DateSelector, Deadline</li>
            <li>Dialog: DialogWindow</li>
            <li>Layout: CustomAppBar, CustomDrawer, CustomRouter, LayoutWrapper, NavigationList, Routes</li>
            <li>Modal: ModalWindow</li>
            <li>Notifications: NotificationItem, NotificationsList, NotificationsPanel</li>
            <li>TechnologyCards: TechnologyCard, TechnologyCardModal, TechnologyCardsList</li>
 
            <li>
                TodoItem:
                <li>Actions: TodoItemAction, TodoItemActionsButtons, TodoItemActionsMenu</li>
                <li>Forms: CreateTodoItemForm, CreateTodoItemInline, EditTodoItemForm, TodoItemCycleForm</li>
                <li>Modals: ConfirmTodoItemDoneModal, CreateTodoItemModal, DeleteTodoItemModal, EditTodoItemModal</li>
                <li>TodoItemsList: TodoItemsList, TodoItemsListHeader, TodoITemsListItem, TodoItemsListOptions</li>
            </li>
        </ul>
 
        <Typography variant='h6'>Hooks</Typography>
        <ul>
            <li>useArray</li>
            <li>useAsyncState</li>
            <li>useDisplayOptions</li>
            <li>useForm</li>
            <li>useLocalStorage</li>
            <li>useToggle</li>
            <li>useUpdateLogger</li>
            <li>useWindowDimensions</li>
        </ul>
 
        <Typography variant='h6'>Utilities</Typography>
        <ul>
            <li>ApiHelper</li>
            <li>ArrayHeloer</li>
            <li>Defaults</li>
            <li>DummyObjects</li>
            <li>SignalApiHelper</li>
            <li>TodoItemHelper</li>
            <li>Utilities</li>
        </ul>
 
        <Divider />
 
        <Typography variant='h6'>Design patterns</Typography>
        <Typography variant='body'>
            <ul>
                <li>Repository</li>
                <li>Service</li>
                <li>Unit of Work</li>
                <li>Layered architecture (Model - Data Access - Business logic - WebApi)</li>
            </ul>
        </Typography>
</Grid> */
}