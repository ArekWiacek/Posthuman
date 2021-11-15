using AutoMapper;
using Posthuman.Core;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Enums;
using Posthuman.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Posthuman.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProjectsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ProjectDTO> CreateProject(ProjectDTO newProjectDTO)
        {
            var newProject = mapper.Map<Project>(newProjectDTO);
            newProject.IsFinished = false;
            newProject.CreationDate = DateTime.Now;
            newProject.TotalSubtasks = 0;
            newProject.CompletedSubtasks = 0;

            // Set owner Avatar
            var ownerAvatar = await
                unitOfWork
                .Avatars
                .GetByIdAsync(newProjectDTO.AvatarId);

            if (ownerAvatar != null)
                newProject.Avatar = ownerAvatar;

            await unitOfWork.Projects.AddAsync(newProject);

            // Todo - how to have project id before save
            await unitOfWork.CommitAsync();
            
            var projectCreatedEvent = new EventItem(
                ownerAvatar.Id, 
                EventType.ProjectCreated, 
                DateTime.Now, 
                EntityType.Project, 
                newProject.Id);

            await unitOfWork.EventItems.AddAsync(projectCreatedEvent);

            ownerAvatar.Exp += projectCreatedEvent.ExpGained;

            await unitOfWork.CommitAsync();

            return mapper.Map<ProjectDTO>(newProject); // ProjectToDTO(newProject);
        }

        public async Task DeleteProject(int id)
        {
            // TODO: obsluzyc sytuacje kiedy usuwany projekt ma subtaski
            var project = await unitOfWork.Projects.GetByIdAsync(id);

            if (project == null)
                return; 

            unitOfWork.Projects.Remove(project);

            var ownerAvatar = await unitOfWork.Avatars.GetByIdAsync(project.AvatarId);

            var projectDeletedEvent = new EventItem(
                ownerAvatar.Id, 
                EventType.ProjectDeleted, 
                DateTime.Now, 
                EntityType.Project, 
                project.Id);

            await unitOfWork.EventItems.AddAsync(projectDeletedEvent);

            if (ownerAvatar != null)
                ownerAvatar.Exp += projectDeletedEvent.ExpGained;

            await unitOfWork.CommitAsync();

        }

        public async Task<IEnumerable<ProjectDTO>> GetAllProjectsForActiveAvatar()
        {
            var activeAvatar = await unitOfWork.Avatars.GetActiveAvatarAsync();

            var allProjects = await 
                unitOfWork
                .Projects
                .GetAllByAvatarIdAsync(activeAvatar.Id);

            var allMapped =
                mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDTO>>(allProjects);

            return allMapped.ToList();

            //return allProjects
            //    .Select(project => ProjectToDTO(project))
            //    .ToList();
        }

        public async Task<IEnumerable<ProjectDTO>> GetAllProjects()
        {
            var allProjects = await unitOfWork.Projects.GetAllAsync();

            var allMapped = 
                mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDTO>>(allProjects);

            return allMapped.ToList();

            //return allProjects
            //    .Select(project => ProjectToDTO(project))
            //    .ToList();
        }

        public async Task<ProjectDTO> GetProjectById(int id)
        {
            var project = await unitOfWork.Projects.GetByIdAsync(id);

            if (project != null)
                return mapper.Map<ProjectDTO>(project); 

            return null;
        }

        public async Task<ProjectDTO> GetProjectWithSubtasks(int projectId)
        {
            var projectWithSubtasks = await unitOfWork.Projects.GetByIdAsync(projectId);

            // todo: include subtasks
            return mapper.Map<ProjectDTO>(projectWithSubtasks); 

        }

        public async Task UpdateProject(ProjectDTO projectDTO)
        {
            var project = await unitOfWork.Projects.GetByIdAsync(projectDTO.Id);
            var ownerAvatar = await unitOfWork.Avatars.GetByIdAsync(projectDTO.AvatarId);

            if (project == null)
                return;// NotFound();

            project.Title = projectDTO.Title;
            project.Description = projectDTO.Description;
            project.StartDate = projectDTO.StartDate;

            var projectModifiedEvent = new EventItem(
                ownerAvatar.Id, 
                EventType.ProjectModified, 
                DateTime.Now, 
                EntityType.Project, 
                project.Id);

            await unitOfWork.EventItems.AddAsync(projectModifiedEvent);

            // Project was just finished
            if (project.IsFinished == false && projectDTO.IsFinished == true)
            {
                project.FinishDate = DateTime.Now;
                project.IsFinished = projectDTO.IsFinished;

                // Add event of type "ProjectFinished"
                var projectFinishedEvent = new EventItem(
                    ownerAvatar.Id, 
                    EventType.ProjectFinished, 
                    DateTime.Now, 
                    EntityType.Project, 
                    project.Id);
                await unitOfWork.EventItems.AddAsync(projectFinishedEvent);

                // Update Avatar Exp points
                ownerAvatar.Exp += projectFinishedEvent.ExpGained;
            }

            await unitOfWork.CommitAsync();
        }
    }
}
