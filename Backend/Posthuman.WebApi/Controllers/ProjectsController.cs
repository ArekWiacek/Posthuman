using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Posthuman.Core.Services;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.DTO;

namespace PosthumanWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ILogger<ProjectsController> logger;
        private readonly IProjectsService projectsService;

        public ProjectsController(
            ILogger<ProjectsController> logger,
            IProjectsService projectsService)
        {
            this.logger = logger;
            this.projectsService = projectsService;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
        {
            try 
            { 
                var allProjects = await projectsService.GetAllProjectsForActiveAvatar();
                return Ok(allProjects);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return Ok(null);
            }
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetProject(int id)
        {
            var project = await projectsService.GetProjectById(id);

            if (project == null)
                return NotFound();

            return project;
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(ProjectDTO projectDTO)
        {
            var createdProjectDTO = await projectsService.CreateProject(projectDTO);

            return CreatedAtAction(
                nameof(GetProject),
                new { id = createdProjectDTO.Id },
                createdProjectDTO);
        }


        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectDTO updatedProjectDTO)
        {
            if (id != updatedProjectDTO.Id)
                return BadRequest();

            await projectsService.UpdateProject(updatedProjectDTO);

            return NoContent();
        }


        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await projectsService.DeleteProject(id);
            return NoContent();
        }
    }
}
