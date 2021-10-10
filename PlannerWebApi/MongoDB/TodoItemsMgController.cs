/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosthumanWebApi.Models;
using PosthumanWebApi.Models.DTO;
using PosthumanWebApi.Models.Entities;
using PosthumanWebApi.Services;

namespace PosthumanWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsMgController : ControllerBase
    {
        private readonly ILogger<TodoItemsMgController> _logger;
        private readonly PosthumanService _service;

        public TodoItemsMgController(
            ILogger<TodoItemsMgController> logger,
            PosthumanService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<TodoItemMg>> Get() =>
            _service.Get();

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<TodoItemMg> Get(string id)
        {
            var todoItem = _service.Get(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost]
        public ActionResult<TodoItemMg> Create(TodoItemMg todoItem)
        {
            _service.Create(todoItem);

            return CreatedAtRoute("GetBook", new { id = todoItem.Id.ToString() }, todoItem);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, TodoItemMg todoItemIn)
        {
            var todoItem = _service.Get(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _service.Update(id, todoItemIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var todoItem = _service.Get(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _service.Remove(todoItem.Id);

            return NoContent();
        }
    }
}*/