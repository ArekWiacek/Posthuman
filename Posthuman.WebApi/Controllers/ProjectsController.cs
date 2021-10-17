using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosthumanWebApi.Models;
using PosthumanWebApi.Models.Entities;
using PosthumanWebApi.Models.Enums;

namespace PosthumanWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly PosthumanContext _context;

        
        public ProjectsController(PosthumanContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
        {
            var projs = _context.Projects.Include(p => p.TodoItems).ToList();

            var projects = _context.Projects.ToList();

            return await _context
                .Projects
                .Select(project => ProjectToDTO(project))
                .ToListAsync();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
                return NotFound();

            return ProjectToDTO(project);
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(ProjectDTO projectDTO)
        {
            var project = new Project(
                0,
                projectDTO.Title,
                projectDTO.Description,
                false,
                projectDTO.StartDate);

            project.CreationDate = DateTime.Now;
            project.TotalSubtasks = 0;
            project.CompletedSubtasks = 0;

            _context.Projects.Add(project);

            await _context.SaveChangesAsync(true);

            var projectCreatedEvent = new EventItem(2, EventType.ProjectCreated, DateTime.Now, EntityType.Project, project.Id);
            _context.EventItems.Add(projectCreatedEvent);

            var avatar = await _context.Avatars.FindAsync(2);
            if (avatar != null)
                avatar.Exp += projectCreatedEvent.ExpGained;

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjects", new { id = project.Id }, project);
        }


        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectDTO updatedProjectDTO)
        {
            if (id != updatedProjectDTO.Id)
                return BadRequest();

            var project = await _context.Projects.FindAsync(id);

            if (project == null)
                return NotFound();

            project.Title = updatedProjectDTO.Title;
            project.Description = updatedProjectDTO.Description;
            project.StartDate = updatedProjectDTO.StartDate;

            var projectModifiedEvent = new EventItem(2, EventType.ProjectModified, DateTime.Now, EntityType.Project, project.Id);
            _context.EventItems.Add(projectModifiedEvent);

            // Project was just finished
            if (project.IsFinished == false && updatedProjectDTO.IsFinished == true)
            {
                project.FinishDate = DateTime.Now;
                project.IsFinished = updatedProjectDTO.IsFinished;

                // Add event of type "ProjectFinished"
                var projectFinishedEvent = new EventItem(2, EventType.ProjectFinished, DateTime.Now, EntityType.Project, project.Id);
                _context.EventItems.Add(projectFinishedEvent);

                // Update Avatar Exp points
                var avatar = await _context.Avatars.FindAsync(2);
                if (avatar != null)
                    avatar.Exp += projectFinishedEvent.ExpGained;
            }
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        
        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);

            var projectDeletedEvent = new EventItem(2, EventType.ProjectDeleted, DateTime.Now, EntityType.Project, project.Id);
            _context.EventItems.Add(projectDeletedEvent);

            var avatar = await _context.Avatars.FindAsync(2);
            if (avatar != null)
                avatar.Exp += projectDeletedEvent.ExpGained;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id) =>
            _context.Projects.Any(e => e.Id == id);

        private static ProjectDTO ProjectToDTO(Project project) =>
            new ProjectDTO(
                project.Id,
                project.Title,
                project.Description,
                project.IsFinished,
                project.StartDate,
                project.CreationDate,
                project.FinishDate,
                project.TotalSubtasks,
                project.CompletedSubtasks);
    }
}
