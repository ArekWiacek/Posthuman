import * as React from 'react';
import { Button, Table, TableRow, TableCell, TableHead, TableContainer, TableBody, Paper } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit'

const ProjectsList = ({ projects, onProjectEdited, onProjectDeleted }) => {
    const handleProjectDeleted = projectId => onProjectDeleted(projectId);
    const handleProjectEdit = (project) => onProjectEdited(project);

    const calculateProjectProgress = (project) => {
        if (project.totalSubtasks === 0)
            return 0;

        var projectProgress = (project.completedSubtasks / project.totalSubtasks) * 100;
        projectProgress = Math.round(projectProgress);

        return projectProgress;
    }

    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 600 }} size="small" aria-label="Projects list">
                <TableHead>
                    <TableRow>
                        <TableCell>Title</TableCell>
                        <TableCell align="right">Start date</TableCell>
                        <TableCell align="right">Tasks completed</TableCell>
                        <TableCell align="right">Progress</TableCell>
                        <TableCell align="right">Actions</TableCell>
                    </TableRow>
                </TableHead>

                <TableBody>
                    {projects.map((project) => (
                        <TableRow
                            key={project.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                            <TableCell component="th" scope="row" >
                                {project.title}
                            </TableCell>
                            <TableCell align="right">
                                {/* {project.startDate ? <Moment format="DD.MM.YYYY">{project.startDate}</Moment> : "Start date not set"} */}
                            </TableCell>
                            <TableCell align="right">
                                {project.completedSubtasks} / {project.totalSubtasks}
                            </TableCell>
                            <TableCell align="right">
                                {calculateProjectProgress(project)} %
                            </TableCell>
                            <TableCell align="right">
                                <Button variant="outlined"
                                    sx={{ mr: 2 }}
                                    startIcon={<DeleteIcon />}
                                    onClick={(e) => handleProjectDeleted(project.id)} >
                                    Delete
                                </Button>
                                <Button variant="outlined"
                                    startIcon={<EditIcon />}
                                    onClick={(e) => handleProjectEdit(project)} >
                                    Edit
                                </Button>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default ProjectsList;