using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Posthuman.Core.Services;
using Posthuman.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Posthuman.WebApi.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;
        private readonly AuthenticationSettings authenticationSettings;

        public JwtMiddleware(
            RequestDelegate next,
            AuthenticationSettings authenticationSettings)
        {
            this.next = next;
            this.authenticationSettings = authenticationSettings;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, token);

            await next(context);
        }

        private void attachUserToContext(HttpContext context, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(authenticationSettings.JwtKey);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            var securityValidatedToken = (JwtSecurityToken)validatedToken;

            var id = securityValidatedToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            context.Items["UserId"] = id;
            //context.Items["User"] = authenticationService.GetUserById(int.Parse(id));
        }
    }
}
