using Posthuman.Core.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posthuman.Core.Services
{
    public interface IProjectsService
    {
        Task<ProjectDTO> GetProjectById(int id);
        Task<IEnumerable<ProjectDTO>> GetAllProjectsForActiveAvatar();
        
        Task<IEnumerable<ProjectDTO>> GetAllProjects();
        
        Task<ProjectDTO> GetProjectWithSubtasks(int projectId);

        Task<ProjectDTO> CreateProject(ProjectDTO newProjectDTO);

        Task UpdateProject(ProjectDTO projectToUpdateDTO);

        //Task DeleteProject(ProjectDTO project);
        Task DeleteProject(int id);
    }
}

