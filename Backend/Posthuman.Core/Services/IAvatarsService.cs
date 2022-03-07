using System.Threading.Tasks;
using System.Collections.Generic;
using Posthuman.Core.Models.DTO.Avatar;
using Posthuman.Core.Models.Entities;

namespace Posthuman.Core.Services
{
    public interface IAvatarsService
    {
        Task<AvatarDTO> GetAvatarByUserId(int userId);
        Task<AvatarDTO> GetAvatarById(int id);
        Task<IEnumerable<AvatarDTO>> GetAllAvatars();

        Task<Avatar> CreateNewAvatar(int userId, string name);
        Task<AvatarDTO> UpdateAvatar(UpdateAvatarDTO avatarDTO);


        Task UpdateAvatarGainedExp(Avatar avatar, int exp);
        Task<bool> HasAvatarDiscoveredNewCard(Avatar avatar);
        
    }
}
