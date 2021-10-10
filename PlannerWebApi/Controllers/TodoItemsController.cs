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
            return await _context.TodoItems
                .Select(todoItem => TodoItemToDTO(todoItem))
                .ToListAsync();
        }

        // GET: api/TodoItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
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
                todoItemDTO.Deadline);

            todoItem.CreationDate = DateTime.Now;

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                TodoItemToDTO(todoItem));
        }

        // PUT: api/TodoItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
                return BadRequest();

            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
                return NotFound();

            todoItem.Title = todoItemDTO.Title;
            todoItem.Description = todoItemDTO.Description;
            todoItem.Deadline = todoItemDTO.Deadline;
            todoItem.IsCompleted = todoItemDTO.IsCompleted;

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
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
                return NotFound();

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id) => 
            _context.TodoItems.Any(todoItem => todoItem.Id == id);

        private static TodoItemDTO TodoItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO(
                todoItem.Id,
                todoItem.Title,
                todoItem.Description,
                todoItem.IsCompleted,
                todoItem.Deadline);
    }
}