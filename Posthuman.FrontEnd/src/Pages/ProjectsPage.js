import * as React from 'react';
import { Paper, Grid } from '@mui/material';


import CreateProject from '../components/Project/CreateProject';
import EditProject from '../components/Project/EditProject';
import ProjectView from '../components/Project/ProjectView';
import ProjectsList from '../components/Project/ProjectsList';
import SelectAvatar from '../components/Avatar/SelectAvatar';

import { AvatarContext } from "../App";
import { LogT, LogI, LogE } from '../Utilities/Utilities';
import { CreateDummyProject, CreateDummyProjects } from '../Utilities/DummyObjects';

import { ApiGet, ApiPost, ApiPut, ApiDelete } from '../Utilities/ApiRepository';

const ProjectsPage = () => {
  const { activeAvatar } = React.useContext(AvatarContext);
  const [projects, setProjects] = React.useState(CreateDummyProjects(4));
  const [isProjectInEditMode, setIsProjectInEditMode] = React.useState(false);
  const [currentProject, setCurrentProject] = React.useState(CreateDummyProject());

  const handleProjectCreated = (newProject) => {
    ApiPost("Projects", newProject, data => {
      const projectsListWithNewProject = [...projects, data];
      setProjects(projectsListWithNewProject);
    });
  }

  const handleProjectDeleted = (projectId) => {
    ApiDelete("Projects", projectId, () => {
      const projectsList = projects.filter((project) => project.id !== projectId);
      setProjects(projectsList);
    });
  }

  const handleProjectSaveChanges = (editedProjectId, editedProject) => {
    ApiPut("Projects", editedProjectId, editedProject, () => {
      const updatedProjectsList = projects.map((project) =>
        project.id === editedProjectId ? editedProject : project
      );

      setProjects(updatedProjectsList);
      setIsProjectInEditMode(false);
      setCurrentProject(CreateDummyProject());
    })
  }

  const handleProjectEdited = (projectToEdit) => {
    setIsProjectInEditMode(true);
    setCurrentProject(projectToEdit);
  }

  const handleProjectCancelEdit = () => {
    setIsProjectInEditMode(false);
    setCurrentProject(CreateDummyProject());
  }

  React.useEffect(() => {
    ApiGet("Projects", data => setProjects(data));
  }, [activeAvatar])

  return (
    <Grid container spacing={3}>
      {/* PROJECTS LIST */}
      <Grid item xs={12}>
        <ProjectsList
          projects={projects}
          onProjectDeleted={handleProjectDeleted}
          onProjectEdited={handleProjectEdited} />
      </Grid>

      {/* PROJECT CREATE / EDIT */}
      <Grid item xs={3} >
        <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
          {
            isProjectInEditMode ?
              (<EditProject
                onSaveChanges={handleProjectSaveChanges}
                onCancelEdit={handleProjectCancelEdit}
                currentProject={currentProject}
                key={currentProject.id} />) : (
                <CreateProject onCreateProject={handleProjectCreated} />)
          }
        </Paper>
      </Grid>

      {/* PROJECTS LIST */}
      <Grid item xs={12} md={12} lg={12}>
        {
          projects.map((project) => (
            <ProjectView key={project.id} project={project} />
          ))
        }
      </Grid>

    </Grid>
  );
}

export default ProjectsPage;