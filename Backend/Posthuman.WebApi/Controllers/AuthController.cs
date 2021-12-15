using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Services;

namespace PosthumanWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly IUsersService usersService;

        public AuthController(
            ILogger<AuthController> logger,
            IUsersService usersService)
        {
            this.logger = logger;
            this.usersService = usersService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            var registeredUser = await usersService.RegisterUser(userDTO);

            return Created("success", registeredUser);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserDTO userDTO)
        {
            var user = await usersService.LoginUser(userDTO);

            return Ok(user);
        }


    }
}
