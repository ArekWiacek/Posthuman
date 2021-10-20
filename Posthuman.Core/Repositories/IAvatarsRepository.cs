using Posthuman.Core.Models.Entities;

namespace Posthuman.Core.Repositories
{
    public interface IAvatarsRepository : IRepository<Avatar>
    {
        Task<Avatar> GetActiveAvatarAsync();
    }
}