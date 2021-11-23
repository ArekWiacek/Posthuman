using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Enums;
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


        Task UpdateAvatarGainedExp(Avatar avatar, int exp);
        Task<bool> HasAvatarDiscoveredNewCard(Avatar avatar);
    }
}
