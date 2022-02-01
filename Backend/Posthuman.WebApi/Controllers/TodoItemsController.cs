using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Posthuman.Core.Services;
using Posthuman.Core.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Posthuman.WebApi.Extensions;

namespace PosthumanWebApi.Controllers
{
    [Authorize]
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

        /// GET: api/TodoItems
        /// Returns list of TodoItems for current user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var userTodoItems = await
                todoItemsService
                .GetAllTodoItems(this.GetCurrentUserId());

            return Ok(userTodoItems);
        }

        /// GET: api/TodoItems/Hierarchical
        /// Returns flattened list of TodoItems for current user
        /// List is sorted by hierarchy (top-level TodoItems first, then recursively it's subtasks)
        [HttpGet("Hierarchical")]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItemsHierarchical()
        {
            var allTodoItems = await
                todoItemsService
                .GetAllTodoItemsHierarchical(this.GetCurrentUserId());

            return Ok(allTodoItems);
        }

        /// GET: api/TodoItem/5
        /// Returns TodoItem of given ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(int id)
        {
            var todoItem = await todoItemsService.GetTodoItemById(id);

            if (todoItem == null)
                return NotFound();

            return Ok(todoItem);
        }

        /// POST: api/TodoItem
        /// Creates new TodoItem with all backend logic
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(CreateTodoItemDTO createTodoItemDTO)
        {
            var userId = this.GetCurrentUserId();
            var todoItemDTO = await todoItemsService.CreateTodoItem(userId, createTodoItemDTO);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItemDTO.Id }, todoItemDTO);
        }

        /// PUT: api/TodoItem/5
        /// Updates TodoItem with all backend logic
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
                return BadRequest();

            await todoItemsService.UpdateTodoItem(todoItemDTO);

            return NoContent();
        }

        [HttpPut("Complete/{id}")]
        public async Task<IActionResult> CompleteTodoItem(int id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
                return BadRequest();

            //var userId = this.GetCurrentUserId();

            await todoItemsService.CompleteTodoItem(todoItemDTO);

            return NoContent();
        }

        /// DELETE: api/TodoItem/5
        /// Deletes TodoItem with all backend logic
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            await todoItemsService.DeleteTodoItem(id);

            return NoContent();
        }
    }
}
