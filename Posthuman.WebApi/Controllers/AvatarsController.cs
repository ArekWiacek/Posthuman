using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosthumanWebApi.Models;
using PosthumanWebApi.Models.DTO;
using PosthumanWebApi.Models.Entities;

namespace PosthumanWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvatarsController : ControllerBase
    {
        private readonly PosthumanContext _context;
        public AvatarsController(PosthumanContext context)
        {
            _context = context;
        }

        // GET: api/Avatars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvatarDTO>>> GetAvatars()
        {
            //var projs = _context.Avatars.Include(p => p.TodoItems).ToList();

            var avatars = _context.Avatars.ToList();

            return await _context
                .Avatars
                .Select(avatar => AvatarToDTO(avatar))
                .ToListAsync();
        }

        // GET: api/Avatars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AvatarDTO>> GetProject(int id)
        {
            var avatar = await _context.Avatars.FindAsync(id);

            if (avatar == null)
            {
                return NotFound();
            }

            return AvatarToDTO(avatar);
        }

        // PUT: api/Avatars/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, AvatarDTO updatedAvatarDTO)
        {
            if (id != updatedAvatarDTO.Id)
                return BadRequest();

            var avatar = await _context.Avatars.FindAsync(id);

            if (avatar == null)
                return NotFound();

            avatar.Name = updatedAvatarDTO.Name;
            avatar.Bio = updatedAvatarDTO.Bio;
            
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

        // POST: api/Avatars
        [HttpPost]
        public async Task<ActionResult<AvatarDTO>> CreateProject(AvatarDTO avatarDTO)
        {
            var avatar = new Avatar(
                0,
                avatarDTO.Name,
                avatarDTO.Bio,
                DateTime.Now, 
                1, 
                0);

            _context.Avatars.Add(avatar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAvatars", new { id = avatar.Id }, avatar);
        }

        // DELETE: api/Avatars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var avatar = await _context.Avatars.FindAsync(id);
            if (avatar == null)
            {
                return NotFound();
            }

            _context.Avatars.Remove(avatar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id) =>
            _context.Avatars.Any(e => e.Id == id);

        private static AvatarDTO AvatarToDTO(Avatar avatar) =>
            new AvatarDTO(
                avatar.Id,
                avatar.Name,
                avatar.Bio,
                avatar.Level,
                avatar.Exp);
    }
}
