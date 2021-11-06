import * as React from 'react';

import Grid from '@mui/material/Grid';
import { Paper } from '@mui/material';

import axios from 'axios';

import CreateTodoItem from '../components/TodoItem/CreateTodoItem';
import EditTodoItem from '../components/TodoItem/EditTodoItem';
import TodoItemList from '../components/TodoItem/TodoItemsList/TodoItemsList';

import CreateProject from '../components/Project/CreateProject';
import EditProject from '../components/Project/EditProject';
import ProjectsList from '../components/Project/ProjectsList';

import SelectAvatar from '../components/Avatar/SelectAvatar';

import { AvatarContext } from "../App";
import { ApiUrl, LogT, LogI, LogE } from '../Utilities/Utilities';
import { ApiRepository } from '../Utilities/ApiRepository';

function todoItemFormInitialValues() {
  return {
    title: "",
    description: "",
    isCompleted: false,
    deadline: new Date(),
    projectId: null,
    avatarId: null
  }
}

function createEmptyProject() {
  return {
    title: "",
    description: "",
    isFinished: false,
    startDate: new Date(),
    avatarId: 0
  }
}

const DashboardPage = () => {
  LogT("DashboardPage.Constructor");

  const { activeAvatar } = React.useContext(AvatarContext);

  const [projects, setProjects] = React.useState([
    {
      id: 1,
      title: 'Sample project 1',
      description: 'Sample project 2 description',
      isFinished: false,
      startDate: new Date()
    }, {
      id: 2,
      title: 'Sample project 2',
      description: 'Sample project 2 description',
      isFinished: false,
      startDate: new Date()
    }
  ]);

  const [todoItems, setTodoItems] = React.useState([
    {
      id: 0,
      title: 'TodoItem 0',
      description: 'jakis opis 0',
      isCompleted: false,
      deadline: new Date()
    }, {
      id: 1,
      title: 'TodoItem 1',
      description: 'jakis opis 1',
      isCompleted: true,
      deadline: new Date()
    }, {
      id: 2,
      title: 'TodoItem 2',
      description: 'jakis opis 2',
      isCompleted: false,
      deadline: new Date()
    }
  ]);

  const [isTaskInEditMode, setIsTaskInEditMode] = React.useState(false);
  const [isProjectInEditMode, setIsProjectInEditMode] = React.useState(false);
  const [currentTodoItem, setCurrentTodoItem] = React.useState(todoItemFormInitialValues());
  const [currentProject, setCurrentProject] = React.useState(createEmptyProject());

  const handleTodoItemCreated = (newTodoItem) => {
    axios
      .post(ApiUrl + "TodoItems", newTodoItem)
      .then(response => {
        const todoItemListWithNewTodoItem = [...todoItems, response.data];
        setTodoItems(todoItemListWithNewTodoItem);
      })
      .catch(error => LogE("Error occured when creating new TodoItem: ", error))
      .then(() => { });
  }

  const handleProjectCreated = (newProject) => {
    axios.post(ApiUrl + "Projects", newProject)
      .then(response => {
        const projectsListWithNewProject = [...projects, response.data];
        setProjects(projectsListWithNewProject);
      })
      .catch(error => LogE("Error occured when creating new Project: ", error));
  }

  const handleTodoItemDeleted = (todoItemId) => {
    axios
      .delete(ApiUrl + "TodoItems" + "/" + todoItemId)
      .then(response => {
        const todoItemsList = todoItems.filter((todoItem) => todoItem.id !== todoItemId);
        setTodoItems(todoItemsList);
      })
      .catch(error => LogE("Error occured when deleting TodoItem: ", error));
  }

  const handleProjectDeleted = (projectId) => {
    axios
      .delete(ApiUrl + "Projects" + "/" + projectId)
      .then(response => {
        const projectsList = projects.filter((project) => project.id !== projectId);
        setProjects(projectsList);
      })
      .catch(error => LogE("Error occured when deleting Project: ", error));
  }

  const handleTodoItemSaveChanges = (editedTodoItemId, editedTodoItem) => {
    axios
      .put(ApiUrl + "TodoItems" + "/" + editedTodoItemId, editedTodoItem)
      .then(response => {
        const updatedTodoItemList = todoItems.map((todoItem) =>
          todoItem.id === editedTodoItemId ? editedTodoItem : todoItem
        );

        setTodoItems(updatedTodoItemList);
        setIsTaskInEditMode(false);
        setCurrentTodoItem(todoItemFormInitialValues());
      })
      .catch(error => LogE("Error occured when saving changes into TodoItem: ", error));
  }

  const handleProjectSaveChanges = (editedProjectId, editedProject) => {
    axios
      .put(ApiUrl + "Projects" + "/" + editedProjectId, editedProject)
      .then(response => {
        const updatedProjectsList = projects.map((project) =>
          project.id === editedProjectId ? editedProject : project
        );

        setProjects(updatedProjectsList);
        setIsProjectInEditMode(false);
        setCurrentProject(createEmptyProject());
      })
      .catch(error => LogE("Error occured when saving changes into Project: ", error));
  }

  const handleTodoItemDone = (completedTodoItem) => {
    completedTodoItem.isCompleted = true;

    axios
      .put(ApiUrl + "TodoItems" + "/" + completedTodoItem.id, completedTodoItem)
      .then(response => {
        const updatedTodoItemList = todoItems.map((todoItem) =>
          todoItem.id === completedTodoItem ? completedTodoItem : todoItem
        );

        setTodoItems(updatedTodoItemList);
      })
      .catch(error => LogE("Error occured when saving changes into TodoItem: ", error));
  }

  const handleTodoItemEdited = (todoItemToEdit) => {
    setIsTaskInEditMode(true);
    setCurrentTodoItem(todoItemToEdit);
  }

  const handleTodoItemCancelEdit = () => {
    setIsTaskInEditMode(false);
    setCurrentTodoItem(todoItemFormInitialValues());
  }

  const handleProjectEdited = (projectToEdit) => {
    setIsProjectInEditMode(true);
    setCurrentProject(projectToEdit);
  }

  const handleProjectCancelEdit = () => {
    setIsProjectInEditMode(false);
    setCurrentProject(createEmptyProject());
  }

  React.useEffect(() => {
    LogT("DashboardPage.useEffect");

    LogI("Getting Projects...")
    axios
      .get(ApiUrl + "Projects")
      .then(response => {
        setProjects(response.data);
      })
      .catch(error => {
        LogE("Error occured when obtaining Projects: ", error);
      });

    LogI("Getting TodoItems...");
    axios
      .get(ApiUrl + "TodoItems")
      .then(response => {
        setTodoItems(response.data);
      })
      .catch(error => {
        LogE("Error occured when obtaining TodoItems: ", error);
      });
  }, [activeAvatar]);

  return (
      <Grid container spacing={3}>
        {/* PROJECT CREATE */}
        <Grid item xs={12} md={4} lg={3}>
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

        {/* TODO ITEM CREATE / EDIT */}
        <Grid item xs={12} md={4} lg={3}>
          <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
            {
              isTaskInEditMode ?
                (<EditTodoItem
                  onSaveChanges={handleTodoItemSaveChanges}
                  onCancelEdit={handleTodoItemCancelEdit}
                  currentTodoItem={currentTodoItem}
                  key={currentTodoItem.id}
                  projects={projects} />) : (
                  <CreateTodoItem onCreateTodoItem={handleTodoItemCreated} projects={projects} />)
            }
          </Paper>
        </Grid>

        {/* SELECT AVATAR */}
        <Grid item xs={12} md={4} lg={3}>
            <SelectAvatar />
        </Grid>

        {/* PROJECTS LIST */}
        <Grid item xs={12} md={12} lg={12}>
          {/* <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}> */}
            <ProjectsList
              projects={projects}
              onProjectDeleted={handleProjectDeleted}
              onProjectEdited={handleProjectEdited} />
          {/* </Paper> */}
        </Grid>

        {/* TODO ITEMS LIST */}
        <Grid item xs={12} md={12} lg={12}>
          {/* <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}> */}
            <TodoItemList
              todoItems={todoItems}
              onTodoItemDeleted={handleTodoItemDeleted}
              onTodoItemEdited={handleTodoItemEdited}
              onTodoItemDone={handleTodoItemDone} />
          {/* </Paper> */}
        </Grid>
      </Grid>
  );
}

export default DashboardPage;