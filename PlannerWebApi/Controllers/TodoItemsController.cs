using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosthumanWebApi.Models;
using PosthumanWebApi.Models.DTO;
using PosthumanWebApi.Models.Entities;

namespace PosthumanWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ILogger<TodoItemsController> _logger;
        private readonly PosthumanContext _context;

        public TodoItemsController(
            ILogger<TodoItemsController> logger,
            PosthumanContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            //var todoItems = await _context.TodoItems
            //    .Select(todoItem => TodoItemToDTO(todoItem))
            //    .ToListAsync();

            //var withProjects = await _context.TodoItems
            //    .Include(todoItem => todoItem.Project)
            //    .Select(todoItem => TodoItemToDTO(todoItem))
            //    .ToListAsync();

            return await _context.TodoItems
                .Include(todoItem => todoItem.Project)
                .Select(todoItem => TodoItemToDTO(todoItem))
                .ToListAsync();
        }

        // GET: api/TodoItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
                return NotFound();

            return TodoItemToDTO(todoItem);
        }

        // POST: api/TodoItem
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem(
                0,                                  // ID generated automatically
                todoItemDTO.Title,
                todoItemDTO.Description,
                false,                              // IsCompleted always false at the beginning
                todoItemDTO.Deadline,
                todoItemDTO.ProjectId);

            todoItem.CreationDate = DateTime.Now;

            // TodoItem has parent - it need to be updated
            if(todoItem.ProjectId != null && todoItem.ProjectId.Value != 0)
            {
                var project = await _context.Projects.FindAsync(todoItem.ProjectId.Value);

                if (project != null)
                {
                    project.TotalSubtasks++;
                }
            }

            _context.TodoItems.Add(todoItem);

            var taskCreatedEvent = new EventItem(2, EventType.TodoItemCreated, DateTime.Now);
            _context.EventItems.Add(taskCreatedEvent);

            var avatar = await _context.Avatars.FindAsync(2);
            if(avatar != null)
                avatar.Exp += taskCreatedEvent.ExpGained;

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                TodoItemToDTO(todoItem));
        }

        // PUT: api/TodoItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, TodoItemDTO updatedTodoItemDTO)
        {
            if (id != updatedTodoItemDTO.Id)
                return BadRequest();

            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
                return NotFound();

            todoItem.Title = updatedTodoItemDTO.Title;
            todoItem.Description = updatedTodoItemDTO.Description;
            todoItem.Deadline = updatedTodoItemDTO.Deadline;

            // parent project was changed - both old and new parent need to be updated
            if(todoItem.ProjectId != updatedTodoItemDTO.ProjectId)
            {
                var oldParent = await _context.Projects.FindAsync(todoItem.ProjectId.Value);
                var newParent = await _context.Projects.FindAsync(updatedTodoItemDTO.ProjectId.Value);

                oldParent.TotalSubtasks--;
                newParent.TotalSubtasks++;

                todoItem.ProjectId = updatedTodoItemDTO.ProjectId;
            }

            // Add event of type "TodoItemModified"
            var todoItemModifiedEvent = new EventItem(2, EventType.TodoItemModified, DateTime.Now);
            _context.EventItems.Add(todoItemModifiedEvent);

            // TodoItem was just completed
            if (todoItem.IsCompleted == false && updatedTodoItemDTO.IsCompleted == true)
            {
                todoItem.CompletionDate = DateTime.Now;
                todoItem.IsCompleted = updatedTodoItemDTO.IsCompleted;

                // If TodoItem is subtask of a project, update CompletedSubtasks property
                if (todoItem.ProjectId != null)
                {
                    var parentProject = await _context.Projects.FindAsync(todoItem.ProjectId.Value);
                    
                    if(parentProject != null)
                        parentProject.CompletedSubtasks++;
                }

                // Add event of completion
                var todoItemCompletedEvent = new EventItem(2, EventType.TodoItemCompleted, DateTime.Now);
                _context.EventItems.Add(todoItemCompletedEvent);

                // Update Avatar Exp points
                var avatar = await _context.Avatars.FindAsync(2);
                if(avatar != null)
                    avatar.Exp += todoItemCompletedEvent.ExpGained;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/TodoItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
                return NotFound();

            _context.TodoItems.Remove(todoItem);

            // TodoItem was subtask of parent Project - it need to be updated as well
            if (todoItem.ProjectId != null)
            {
                var parentProject = await _context.Projects.FindAsync(todoItem.ProjectId.Value);

                if (parentProject != null)
                    parentProject.TotalSubtasks--;
            }

            var todoItemDeletedEvent = new EventItem(2, EventType.TodoItemDeleted, DateTime.Now);
            _context.EventItems.Add(todoItemDeletedEvent);

            var avatar = await _context.Avatars.FindAsync(2);
            if(avatar != null)
                avatar.Exp += todoItemDeletedEvent.ExpGained;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(int id) => 
            _context.TodoItems.Any(todoItem => todoItem.Id == id);

        private static TodoItemDTO TodoItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO(
                todoItem.Id,
                todoItem.Title,
                todoItem.Description,
                todoItem.IsCompleted,
                todoItem.Deadline,
                todoItem.ProjectId);
    }
}