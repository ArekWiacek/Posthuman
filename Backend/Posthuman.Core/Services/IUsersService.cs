using Posthuman.Core.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posthuman.Core.Services
{
    public interface IUsersService
    {
        Task<UserDTO> GetUserById(int id);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> RegisterUser(UserDTO userDTO);
        Task<UserDTO> LoginUser(UserDTO userDTO);
    }
}
