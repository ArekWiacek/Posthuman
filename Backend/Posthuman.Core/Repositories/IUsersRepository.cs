using Posthuman.Core.Models.Entities;
using System.Threading.Tasks;

namespace Posthuman.Core.Repositories
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email);
        new Task<User> GetByIdAsync(int userId);
    }
}