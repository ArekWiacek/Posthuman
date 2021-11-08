using Posthuman.Core.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posthuman.Core.Services
{
    public interface IAvatarsService
    {
        Task<AvatarDTO> GetAvatarById(int id);
        Task<IEnumerable<AvatarDTO>> GetAllAvatars();

        Task<AvatarDTO> GetActiveAvatar();
        Task SetActiveAvatar(int id);

        Task DeactivateAllAvatars();

        Task UpdateAvatar(AvatarDTO avatarDTO);
    }
}
