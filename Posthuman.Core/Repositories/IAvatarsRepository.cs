using Posthuman.Core.Models.Entities;
using System.Threading.Tasks;

namespace Posthuman.Core.Repositories
{
    public interface IAvatarsRepository : IRepository<Avatar>
    {
        Task<Avatar> GetActiveAvatarAsync();
    }
}