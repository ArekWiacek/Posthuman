using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posthuman.Core.Services
{
    public interface ITechnologyCardsService
    {
        Task<IEnumerable<TechnologyCardDTO>> GetTechnologyCardsForAvatar(int avatarId);
        Task<IEnumerable<TechnologyCardDTO>> GetTechnologyCardsForAvatar(AvatarDTO avatar);
        Task<IEnumerable<TechnologyCardDTO>> GetTechnologyCardsForCategory(int avatarId, CardCategory category);
        Task<IEnumerable<TechnologyCardDTO>> GetTechnologyCardsForCategory(AvatarDTO avatar, CardCategory category);
        Task<TechnologyCardDiscoveryDTO> DiscoverTechnologyCardForAvatar(int avatarId, int technologyCardId);

    }
}
