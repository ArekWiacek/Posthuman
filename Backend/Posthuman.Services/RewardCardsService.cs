using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using Posthuman.Core;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Services;
using Posthuman.RealTime.Notifications;

namespace Posthuman.Services
{
    public class RewardCardsService : IRewardCardsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly INotificationsService notificationsService;

        public RewardCardsService(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            INotificationsService notificationsService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.notificationsService = notificationsService;
        }

        public async Task<IEnumerable<RewardCardDTO>> GetRewardCardsForAvatar(int avatarId)
        {
            var avatar = await unitOfWork.Avatars.GetByIdAsync(avatarId);

            if (avatar == null)
                throw new ArgumentNullException("avatarId", $"Could not obtain avatar of ID: {avatarId}");

            var cards = unitOfWork.RewardCards.Find(card => card.LevelExpected <= avatar.Level);

            return mapper.Map<IEnumerable<RewardCard>, IEnumerable<RewardCardDTO>>(cards);
        }

        public async Task<IEnumerable<RewardCardDTO>> GetRewardCardsForAvatar(AvatarDTO avatar)
        {
            return await GetRewardCardsForAvatar(avatar.Id);
        }
    }
}
