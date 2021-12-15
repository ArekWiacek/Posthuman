import React, { } from 'react';
import { Grid, Typography, Divider } from '@mui/material';
import CardWithImage from '../components/Common/CardWithImage';

const InfoPage = () => {
    const cardSizes = {
        xs: 12,
        md: 6,
        lg: 4,
        xl: 3
    };

    return (
        <Grid container spacing={3} sx={{ textAlign: 'left' }}>

            <Grid item xs={cardSizes.xs} md={cardSizes.md} lg={cardSizes.lg} xl={cardSizes.xl}>
                <CardWithImage
                    title='Posthuman.Core'
                    subtitle='Model Layer'
                    imgSrc={"/Assets/Images/InfoPage/software_architecture.jpg"}>
                    <ul>
                        <li>Models definitions</li>
                        <li>Entities, DTOs, Enums</li>
                        <li>Repositories interfaces</li>
                        <li>Services interfaces</li>
                        <li>Unit of work interface</li>
                    </ul>
                </CardWithImage>
            </Grid>

            <Grid item xs={cardSizes.xs} md={cardSizes.md} lg={cardSizes.lg} xl={cardSizes.xl}>
                <CardWithImage
                    title='Posthuman.Data'
                    subtitle='Data Layer'
                    imgSrc={"/Assets/Images/InfoPage/software_architecture.jpg"}>
                    <ul>
                        <li>Data context</li>
                        <li>Data tables configuration</li>
                        <li>Migrations</li>
                        <li>Entity Framework integration</li>
                        <li>Repositories implementation</li>
                        <li>Unit of work implementation</li>
                    </ul>
                </CardWithImage>
            </Grid>

            <Grid item xs={cardSizes.xs} md={cardSizes.md} lg={cardSizes.lg} xl={cardSizes.xl}>
                <CardWithImage
                    title='Posthuman.Services'
                    subtitle='Service Layer'
                    imgSrc={"/Assets/Images/InfoPage/software_architecture.jpg"}>
                    <ul>
                        <li>Actually it is 'game logic' layer</li>
                        <li>Implementation of services encapsulating all game logic</li>
                        <li>Implementation of game logic helpers, for example randomization system</li>
                    </ul>
                </CardWithImage>
            </Grid>

            <Grid item xs={cardSizes.xs} md={cardSizes.md} lg={cardSizes.lg} xl={cardSizes.xl}>
                <CardWithImage
                    title='Posthuman.WebApi'
                    subtitle='Web API Layer'
                    imgSrc={"/Assets/Images/InfoPage/software_architecture.jpg"}>
                    <ul>
                        <li>ASP.NET Core Web API integration</li>
                        <li>Web Api Controllers implementation</li>
                        <li>Mappings done by Auto Mapper</li>
                        <li>Logging implementation by log4net</li>
                        <li>Environment type handling</li>
                    </ul>
                </CardWithImage>
            </Grid>

            <Grid item xs={cardSizes.xs} md={cardSizes.md} lg={cardSizes.lg} xl={cardSizes.xl}>
                <CardWithImage
                    title='Posthuman.RealTime'
                    subtitle='Real Time Communication Layer'
                    imgSrc={"/Assets/Images/InfoPage/software_architecture.jpg"}>
                    <ul>
                        <li>SignalR integration</li>
                        <li>Implementation of real-time notifications</li>
                    </ul>
                </CardWithImage>
            </Grid>

            <Grid item xs={cardSizes.xs} md={cardSizes.md} lg={cardSizes.lg} xl={cardSizes.xl}>
                <CardWithImage
                    title='Posthuman.Frontend'
                    subtitle='Frontend React App'
                    imgSrc={"/Assets/Images/InfoPage/software_architecture.jpg"}>
                    <ul>
                        <li>MaterialUI integrated</li>
                    </ul>
                </CardWithImage>
            </Grid>

            <Grid item xs={cardSizes.xs} md={cardSizes.md} lg={cardSizes.lg} xl={cardSizes.xl}>
                <CardWithImage
                    title='Technologies'
                    imgSrc={"/Assets/Images/InfoPage/software_architecture.jpg"}>
                    <Typography variant='h6'>Backend</Typography>
                    <Typography variant='body'>
                        <ul>
                            <li>C#</li>
                            <li>ASP.NET Core 3.1</li>
                            <li>ASP.NET WebAPI</li>
                            <li>Entity Framework</li>
                            <li>SignalR</li>
                        </ul>
                    </Typography>

                    <Typography variant='h6'>Frontend</Typography>
                    <Typography variant='body'>
                        <ul>
                            <li>JavaScript</li>
                            <li>React</li>
                            <li>MaterialUI</li>
                        </ul>
                    </Typography>

                    <Typography variant='h6'>Database</Typography>
                    <Typography variant='body'>
                        <ul>
                            <li>Microsoft SQL 2018</li>
                        </ul>
                    </Typography>
                </CardWithImage>
            </Grid>

            {/* 
            <Grid item xs={12} md={6} lg={4}>
                <Card sx={{ maxWidth: 600 }}>
                    <CardMedia
                        component="img"
                        height="250"
                        image={'/Assets/Images/BlogPosts/cyborg.png'}
                        alt="Cyborg" />

                    <CardContent>
                        <Typography variant='h5'>Technologies</Typography>
                        <Divider />
                        <Typography variant='h6'>Backend</Typography>
                        <Typography variant='body'>
                            <ul>
                                <li>C#</li>
                                <li>ASP.NET Core 3.1</li>
                                <li>ASP.NET WebAPI</li>
                                <li>Entity Framework</li>
                                <li>SignalR</li>
                            </ul>
                        </Typography>

                        <Typography variant='h6'>Frontend</Typography>
                        <Typography variant='body'>
                            <ul>
                                <li>JavaScript</li>
                                <li>React</li>
                                <li>MaterialUI</li>
                            </ul>
                        </Typography>

                        <Typography variant='h6'>Database</Typography>
                        <Typography variant='body'>
                            <ul>
                                <li>Microsoft SQL 2018</li>
                            </ul>
                        </Typography>
                    </CardContent>
                </Card>
            </Grid> */}
        </Grid >
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