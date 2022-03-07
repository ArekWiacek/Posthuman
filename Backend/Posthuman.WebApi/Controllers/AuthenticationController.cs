using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Posthuman.Core.Models.DTO;
using Posthuman.RealTime.Notifications;
using Posthuman.WebApi.Extensions;

namespace PosthumanWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly Posthuman.Core.Services.IAuthenticationService authenticationService;
        
        public AuthenticationController(
            Posthuman.Core.Services.IAuthenticationService usersService)
        {
            this.authenticationService = usersService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterUserDTO registerUserDTO)
        {
            var userDto = await authenticationService.RegisterUser(registerUserDTO);
            var jwtToken = await authenticationService.GenerateJwtToken(
                userDto.Id);
            userDto.Token = jwtToken;

            return Ok(userDto);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginUserDTO loginUserDTO)
        {
            var token = await authenticationService.ValidateUser(loginUserDTO);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = tokenHandler.WriteToken(token);

            var userDto = new UserDTO
            {
                Id = 666,
                Email = loginUserDTO.Email,
                Token = tokenContent
            };

            return Ok(userDto);
        }

        [Authorize]
        [HttpGet("CurrentUser")]
        public IActionResult GetUser()
        {
            try
            {
                var id = this.GetCurrentUserId();
                return Ok(id);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok("logged out");
        }
    }
}
