import * as React from 'react';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import LinearProgress from '@mui/material/LinearProgress';


const ProjectView = ({ project }) => {    
    const calculateProjectProgress = (project) => {
        if(project.totalSubtasks === 0)
            return 0;

        var projectProgress = (project.completedSubtasks / project.totalSubtasks) * 100;
        projectProgress = Math.round(projectProgress);

        return projectProgress;
    }

    return (
        <Card>
            <CardContent>
                <Typography variant="h5" component="div">
                    {project.title}
                </Typography>
                <Typography sx={{ mb: 1.5 }} color="text.secondary">
                        What is all about?
                </Typography>
                <Typography variant="body2">
                    {project.description}
                </Typography>
                <LinearProgress variant="determinate" value={calculateProjectProgress(project)} />
            </CardContent>
            <CardActions>
                <Button size="small">Edit details</Button>
            </CardActions>
        </Card>
    );
}

export default ProjectView;