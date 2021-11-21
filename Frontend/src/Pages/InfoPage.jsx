import React, { } from 'react';
import { Grid, Typography, Paper, Divider } from '@mui/material';

const InfoPage = () => {
    return (
        <Grid container spacing={3}>
            
            <Grid item xs={4}>
                <Paper sx={{ textAlign: 'left', p: 3 }}>
                    <Typography variant='h5'>About Posthuman</Typography>
                    <Divider />
                    <Typography variant='body'>Blablabla about lorem ipsum czipsum gipsum cyganipsum</Typography>
                </Paper>
            </Grid>

            <Grid item xs={4}>
                <Paper sx={{ textAlign: 'left', p: 3 }}>
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
                            <li>Microsoft SQL</li>
                        </ul>
                    </Typography>                
                </Paper>
            </Grid>

            <Grid item xs={4}>
                <Paper sx={{ textAlign: 'left', p: 3 }}>
                    <Typography variant='h5'>Architecture</Typography>
                    
                    <Divider />

                    <Typography variant='h6'>Backend</Typography>
                    <Typography variant='body'>
                        <ul>
                            <li>Posthuman.Core</li>
                            <li>Posthuman.Data</li>
                            <li>Posthuman.Services</li>
                            <li>Posthuman.RealTimeCommunication</li>
                            <li>Posthuman.Shared</li>
                            <li>Posthuman.WebApi</li>
                        </ul>
                    </Typography>

                    <Typography variant='h6'>Frontend</Typography>
                    <Typography variant='body'>
                        <ul>
                            <li>React app</li>
                        </ul>
                    </Typography>

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
                </Paper>
            </Grid>
        </Grid>
    );
}

export default InfoPage;