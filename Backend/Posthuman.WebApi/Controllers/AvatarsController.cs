using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Services;

namespace PosthumanWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvatarsController : ControllerBase
    {
        private readonly ILogger<AvatarsController> logger;
        private readonly IAvatarsService avatarsService;

        public AvatarsController(
            ILogger<AvatarsController> logger,
            IAvatarsService avatarsService)
        {
            this.logger = logger;
            this.avatarsService = avatarsService;
        }

        // GET: api/Avatars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvatarDTO>>> GetAvatars()
        {
            var allAvatars = await avatarsService.GetAllAvatars();
            return Ok(allAvatars);
        }

        // GET: api/Avatars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AvatarDTO>> GetAvatar(int id)
        {
            var avatar = await avatarsService.GetAvatarById(id);
            if (avatar == null)
                throw new ArgumentNullException("Avatar");
                //return NotFound();

            return avatar;
        }

        // GET: api/Avatar/GetActiveAvatar
        [HttpGet("GetActiveAvatar")]
        public async Task<ActionResult<TodoItemDTO>> GetActiveAvatar()
        {
            var avatar = await avatarsService.GetActiveAvatar();
            if (avatar == null)
                return NotFound();

            return Ok(avatar);
        }

        // PUT: api/Avatars/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAvatar(int id, AvatarDTO updatedAvatarDTO)
        {
            if (id != updatedAvatarDTO.Id)
                return BadRequest();

            await avatarsService.UpdateAvatar(updatedAvatarDTO);

            return NoContent();
        }

        // PUT: api/Avatar/SetActiveAvatar/5
        [HttpPut("SetActiveAvatar/{id}")]
        public async Task<IActionResult> SetActiveAvatar(int id)
        {
            await avatarsService.SetActiveAvatar(id);
            var avatar = await avatarsService.GetActiveAvatar();
            return Ok(avatar);
        }
    }
}
