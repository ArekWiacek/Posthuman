
using Microsoft.AspNetCore.Mvc;
using Posthuman.Core.Services;
using Posthuman.Core.Models.DTO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

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

        /// GET: api/TodoItems
        /// <summary>
        ///     Returns list of TodoItems for active Avatar
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var allTodoItems = await
                todoItemsService
                .GetAllTodoItemsForActiveAvatar();

            return Ok(allTodoItems);
        }

        /// GET: api/TodoItems/Hierarchical
        /// <summary>
        ///     Returns flat list of TodoItems for active Avatar
        ///     List is sorted by hierarchy (top-level TodoItems first, then recursively it's subtasks)
        /// </summary>
        [HttpGet("Hierarchical")]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItemsHierarchical()
        {
            var allTodoItems = await
                todoItemsService
                .GetTodoItemsHierarchical();

            return Ok(allTodoItems);
        }

        /// GET: api/TodoItem/5
        /// <summary>
        ///     Returns TodoItem of given ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(int id)
        {
            var todoItem = await todoItemsService.GetTodoItemById(id);

            if (todoItem == null)
                return NotFound();

            return Ok(todoItem);
        }

        /// POST: api/TodoItem
        /// <summary>
        ///     Creates new TodoItem with all backend logic
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var createdTodoItemDTO = await todoItemsService.CreateTodoItem(todoItemDTO);

            return CreatedAtAction(nameof(GetTodoItem), new { id = createdTodoItemDTO.Id }, createdTodoItemDTO);
        }

        /// PUT: api/TodoItem/5
        /// <summary>
        ///     Updates TodoItem with all backend logic
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
                return BadRequest();

            await todoItemsService.UpdateTodoItem(todoItemDTO);

            return NoContent();
        }

        /// DELETE: api/TodoItem/5
        /// <summary>
        ///     Deletes TodoItem with all backend logic
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            await todoItemsService.DeleteTodoItem(id);

            return NoContent();
        }
    }
}
