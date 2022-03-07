using Posthuman.Core.Models.DTO;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Posthuman.Core.Services
{
    public interface IAuthenticationService
    {
        Task<UserDTO> GetUserById(int id);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> RegisterUser(RegisterUserDTO userDTO);

        Task<string> GenerateJwtToken(int userId);
        Task<JwtSecurityToken> ValidateUser(LoginUserDTO userDTO);
        
        JwtSecurityToken Verify(string jwtToken);
        Task<UserDTO> GetUser(LoginUserDTO loginUserDTO);
    }
}
