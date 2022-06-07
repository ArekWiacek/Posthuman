using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Posthuman.Core.Models.DTO.Avatar;
using Posthuman.Core.Services;
using Posthuman.WebApi.Extensions;

namespace PosthumanWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AvatarsController : ControllerBase
    {
        private readonly IAvatarsService avatarsService;

        public AvatarsController(IAvatarsService avatarsService)
        {
            this.avatarsService = avatarsService;
        }

        [HttpGet("GetAvatarForLoggedUser")]
        public async Task<ActionResult<AvatarDTO>> GetAvatarForLoggedUser()
        {
            var avatar = await 
                avatarsService
                .GetAvatarByUserId(this.GetCurrentUserId());

            if (avatar == null)
                throw new ArgumentNullException("Avatar");
            
            return Ok(avatar);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AvatarDTO>> GetAvatar(int id)
        {
            var avatar = await avatarsService.GetAvatarById(id);

            if (avatar == null)
                throw new ArgumentNullException("Avatar");
            
            return Ok(avatar);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvatarDTO>>> GetAvatars()
        {
            var allAvatars = await avatarsService.GetAllAvatars();
            return Ok(allAvatars);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAvatar(int id, UpdateAvatarDTO updatedAvatarDTO)
        {
            if (id != updatedAvatarDTO.Id)
                return BadRequest();

            await avatarsService.UpdateAvatar(updatedAvatarDTO);

            return NoContent();
        }
    }
}
