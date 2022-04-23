using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Posthuman.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Posthuman.WebApi.Extensions;
using Posthuman.Core.Models.DTO.Habit;

namespace PosthumanWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HabitsController : ControllerBase
    {
        private readonly ILogger<TodoItemsController> logger;
        private readonly IHabitsService habitsService;

        public HabitsController(
            ILogger<TodoItemsController> logger,
            IHabitsService habitsService) 
        {
            this.logger = logger;
            this.habitsService = habitsService;
        }

        /// GET: api/Habits
        /// Returns list of Habits for current user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HabitDTO>>> GetHabits()
        {
            return Ok(await habitsService.GetAllByUserId(this.GetCurrentUserId()));
        }

        /// GET: api/Habits/5
        /// Returns Habit of given ID
        [HttpGet("{id}")]
        public async Task<ActionResult<HabitDTO>> GetHabit(int id)
        {
            var habit = await habitsService.GetByUserId(id, this.GetCurrentUserId());

            if (habit == null)
                return NotFound();

            return Ok(habit);
        }

        /// POST: api/Habit
        /// Creates new Habit with all backend logic
        [HttpPost]
        public async Task<ActionResult<HabitDTO>> CreateHabit(CreateHabitDTO createHabitDTO)
        {
            var habitDTO = await habitsService.CreateHabit(this.GetCurrentUserId(), createHabitDTO);
            return CreatedAtAction(nameof(GetHabit), new { id = habitDTO.Id }, habitDTO);
        }

        /// PUT: api/Habit/5
        /// Updates Habit with all backend logic
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHabit(int id, CreateHabitDTO habitDTO)
        {
            if (id != habitDTO.Id)
                return BadRequest();

            await habitsService.UpdateHabit(habitDTO);

            return NoContent();
        }

        [HttpPut("Complete/{id}")]
        public async Task<IActionResult> CompleteHabitInstance(int id)
        {
            //if (id != habitDTO.Id)
            //    return BadRequest();

            await habitsService.CompleteHabitInstance(id, this.GetCurrentUserId());

            return NoContent();
        }

        /// DELETE: api/Habit/5
        /// Deletes Habit
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabit(int id)
        {
            await habitsService.DeleteHabit(id, this.GetCurrentUserId());

            return NoContent();
        }
    }
}
