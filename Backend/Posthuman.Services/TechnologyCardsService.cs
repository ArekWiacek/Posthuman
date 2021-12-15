using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using Posthuman.Core;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Services;
using Posthuman.RealTime.Notifications;
using Posthuman.Core.Models.Enums;

namespace Posthuman.Services
{
    public class TechnologyCardsService : ITechnologyCardsService
    {
        private readonly IEventItemsService eventItemsService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TechnologyCardsService(
            IEventItemsService eventItemsService,
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            this.eventItemsService = eventItemsService;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<TechnologyCardDiscoveryDTO> DiscoverTechnologyCardForAvatar(int avatarId, int technologyCardId)
        {
            var avatar = await unitOfWork.Avatars.GetByIdAsync(avatarId);
            var technologyCard = await unitOfWork.TechnologyCards.GetByIdAsync(technologyCardId);

            if (avatar == null)
                throw new ArgumentNullException("avatar");

            if (technologyCard == null)
                throw new ArgumentNullException("technlogyCard");

            if(AvatarMeetsRequirements(avatar, technologyCard))
            {
                var discovery = CreateTechnologyDiscovery(avatar, technologyCard);
                await unitOfWork.TechnologyCardsDiscoveries.AddAsync(discovery);
                await unitOfWork.CommitAsync();
                return mapper.Map<TechnologyCardDiscovery, TechnologyCardDiscoveryDTO>(discovery);
            }

            return null;
        }

        private TechnologyCardDiscovery CreateTechnologyDiscovery(Avatar avatar, TechnologyCard technologyCard)
        {
            var discovery = new TechnologyCardDiscovery
            {
                AvatarId = avatar.Id,
                HasBeenSeen = false,
                Avatar = avatar,
                DiscoveryDate = DateTime.Now,
                RewardCardId = technologyCard.Id,
                RewardCard = technologyCard
            };

            return discovery;
        }

        private bool AvatarMeetsRequirements(Avatar avatar, TechnologyCard technologyCard)
        {
            return true;
        }

        public async Task<IEnumerable<TechnologyCardDTO>> GetTechnologyCardsForAvatar(int avatarId)
        {
            var avatar = await unitOfWork.Avatars.GetByIdAsync(avatarId);

            if (avatar == null)
                throw new ArgumentNullException("avatarId", $"Could not obtain avatar of ID: {avatarId}");

            var cards = unitOfWork.TechnologyCards.Find(card => card.RequiredLevel <= avatar.Level);

            return mapper.Map<IEnumerable<TechnologyCard>, IEnumerable<TechnologyCardDTO>>(cards);
        }

        public async Task<IEnumerable<TechnologyCardDTO>> GetTechnologyCardsForAvatar(AvatarDTO avatar)
        {
            return await GetTechnologyCardsForAvatar(avatar.Id);
        }

        public async Task<IEnumerable<TechnologyCardDTO>> GetTechnologyCardsForCategory(int avatarId, CardCategory category)
        {
            var avatar = await unitOfWork.Avatars.GetByIdAsync(avatarId);

            if (avatar == null)
                throw new ArgumentNullException("avatarId", $"Could not obtain avatar of ID: {avatarId}");

            var cards = unitOfWork.TechnologyCards.Find(card => card.RequiredLevel <= avatar.Level && card.Categories.HasFlag(category));

            return mapper.Map<IEnumerable<TechnologyCard>, IEnumerable<TechnologyCardDTO>>(cards);

        }

        public Task<IEnumerable<TechnologyCardDTO>> GetTechnologyCardsForCategory(AvatarDTO avatar, CardCategory category)
        {
            throw new NotImplementedException();
        }
    }
}
