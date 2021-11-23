using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posthuman.Core.Services
{
    public interface IRewardCardsService
    {
        Task<IEnumerable<RewardCardDTO>> GetRewardCardsForAvatar(int avatarId);
        Task<IEnumerable<RewardCardDTO>> GetRewardCardsForAvatar(AvatarDTO avatar);
    }
}
