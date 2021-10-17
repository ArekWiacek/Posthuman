using Posthuman.Core.Models.Entities;

namespace Posthuman.Core.Services
{
    public interface IProjectsService
    {
        Task<Project> GetProjectByID(int id);
        Task<Project> GetProjectWithSubtasks(int projectId);

        Task<Project> CreateProject(Project newProject);

        Task UpdateProject(Project projectToUpdate);

        Task DeleteProject(Project project);
        Task DeleteProject(int id);
    }
}

