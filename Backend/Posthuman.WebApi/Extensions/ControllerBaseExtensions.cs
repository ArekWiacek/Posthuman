using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System;

namespace Posthuman.WebApi.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static int GetCurrentUserId(this ControllerBase controller)
        {
            var user = controller.HttpContext.User;
            if (user == null || user.Claims == null || !user.Claims.Any())
                throw new Exception("Could not obtain user object from HttpContext.");

            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new Exception("Could not obtain user NameIdentifier claim.");

            return int.Parse(userIdClaim.Value);
        }
    }
}
