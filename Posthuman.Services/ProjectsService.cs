using Posthuman.Core;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posthuman.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IUnitOfWork unitOfWork;
        public ProjectsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Project> CreateProject(Project newProject)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProject(Project project)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProject(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetProjectByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetProjectWithSubtasks(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProject(Project projectToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
