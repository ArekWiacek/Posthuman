
using Microsoft.AspNetCore.Mvc;
using Posthuman.Core.Services;

using Posthuman.Core.Models.DTO;

namespace PosthumanWebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ILogger<TodoItemsController> logger;
        private readonly ITodoItemsService todoItemsService;

        public TodoItemsController(
            ILogger<TodoItemsController> logger,
            ITodoItemsService todoItemsService)
        {
            this.logger = logger;
            this.todoItemsService = todoItemsService;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var allTodoItems = await
                todoItemsService
                .GetAllTodoItemsForActiveAvatar();

            return Ok(allTodoItems);

            //var todoItems = await _context.TodoItems
            //    .Select(todoItem => TodoItemToDTO(todoItem))
            //    .ToListAsync();

            //var withProjects = await _context.TodoItems
            //    .Include(todoItem => todoItem.Project)
            //    .Select(todoItem => TodoItemToDTO(todoItem))
            //    .ToListAsync();

            //return await _context.TodoItems
            //    .Include(todoItem => todoItem.Project)
            //    .Select(todoItem => TodoItemToDTO(todoItem))
            //    .ToListAsync();
        }

        // GET: api/TodoItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(int id)
        {
            var todoItem = await todoItemsService.GetTodoItemById(id);

            if (todoItem == null)
                return NotFound();

            return Ok(todoItem);
        }

        // POST: api/TodoItem
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var createdTodoItemDTO = await todoItemsService.CreateTodoItem(todoItemDTO);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = createdTodoItemDTO.Id },
                createdTodoItemDTO);
        }

        // PUT: api/TodoItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
                return BadRequest();

            await todoItemsService.UpdateTodoItem(todoItemDTO);

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!TodoItemExists(id))
            //        return NotFound();
            //    else
            //        throw;
            //}
            //*/
            return NoContent();
        }

        // DELETE: api/TodoItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            await todoItemsService.DeleteTodoItem(id);

            //var todoItem = await _context.TodoItems.FindAsync(id);

            //if (todoItem == null)
            //    return NotFound();

            //_context.TodoItems.Remove(todoItem);

            // TodoItem was subtask of parent Project - it need to be updated as well
            /*if (todoItem.ProjectId != null)
            {
                var parentProject = await _context.Projects.FindAsync(todoItem.ProjectId.Value);

                if (parentProject != null)
                    parentProject.TotalSubtasks--;
            }

            var todoItemDeletedEvent = new EventItem(2, EventType.TodoItemDeleted, DateTime.Now, EntityType.TodoItem, todoItem.Id);
            _context.EventItems.Add(todoItemDeletedEvent);

            var avatar = await _context.Avatars.FindAsync(2);
            if(avatar != null)
                avatar.Exp += todoItemDeletedEvent.ExpGained;

            await _context.SaveChangesAsync();
            */
            return NoContent();
        }
    }
}
