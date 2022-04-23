using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using Posthuman.Core;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Services;
using Posthuman.RealTime.Notifications;
using Posthuman.Services.Helpers;
using System;
using Posthuman.Core.Models.Enums;
using Posthuman.Core.Exceptions;
using Posthuman.Core.Models.DTO.Avatar;

namespace Posthuman.Services
{
    public class AvatarsService : IAvatarsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IEventItemsService eventItemsService;
        private readonly INotificationsService notificationsService;
        private readonly ITechnologyCardsService rewardCardsService;
        private readonly IExperienceHelper experienceHelper;

        public AvatarsService(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            IEventItemsService eventItemsService,
            INotificationsService notificationsService,
            ITechnologyCardsService rewardCardsService,
            IExperienceHelper experienceHelper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.eventItemsService = eventItemsService;
            this.notificationsService = notificationsService;
            this.rewardCardsService = rewardCardsService;
            this.experienceHelper = experienceHelper;
        }

        public async Task<AvatarDTO> GetAvatarByUserId(int userId)
        {
            var avatar = await unitOfWork.Avatars.GetAvatarForUserAsync(userId);

            if (avatar == null)
               throw new ArgumentNullException("avatar");

            return mapper.Map<AvatarDTO>(avatar);

            //notificationsService.AddNotification(NotificationsHelper.CreateNotification(
            //    avatar,
            //    $"{avatar.Name}",
            //    "",//$"[{activeAvatar.Name}]: systems ready",
            //    $"[{avatar.Name}]: XP: {avatar.Exp}   Level: [{avatar.Level}]",
            //    "Some more bullshit",
            //    "Activated")); ;

            //await notificationsService.SendAllNotifications();
        }
        
        public async Task<IEnumerable<AvatarDTO>> GetAllAvatars()
        {
            var allAvatars = await unitOfWork.Avatars.GetAllAsync();
            var allMapped = mapper.Map<IEnumerable<Avatar>, IEnumerable<AvatarDTO>>(allAvatars);
            return allMapped.ToList();
        }

        public async Task<AvatarDTO> GetAvatarById(int id)
        {
            var avatar = await unitOfWork.Avatars.GetByIdAsync(id);

            if (avatar == null)
                throw new ArgumentException($"Failed to obtain avatar for user of ID: {id}");

            return mapper.Map<AvatarDTO>(avatar);
        }

        public async Task<Avatar> CreateNewAvatar(int userId, string name)
        {
            var avatar = new Avatar();
            avatar.CreationDate = DateTime.Now;
            avatar.UserId = userId;
            avatar.Name = name;
            avatar.Bio = "Write your own story bro";

            SetExperienceNeededForLevel(avatar, 1);

            return avatar;

            //await unitOfWork.Avatars.AddAsync(newAvatar);
            // notificationsService.AddNotification(NotificationsHelper.CreateNotification(newAvatar, $"Avatar of name {newAvatar.Name} created."));
        }

        private async Task<bool> VerifyIfOwner(int userId, int avatarId)
        {
            var user = await unitOfWork.Users.GetByIdAsync(userId);
            var avatar = await unitOfWork.Avatars.GetByIdAsync(avatarId);

            if (user == null)
                throw new ArgumentNullException("userId", $"Could not obtain user of id: {userId}");

            if (avatar == null)
                throw new ArgumentNullException("avatarId", $"Could not obtain avatar of id: {avatarId} for user of id: {userId}");

            if (user.Id != avatar.UserId)
                throw new BadRequestException($"Provided user (ID: {userId}) is not owner of avatar of ID: {avatarId}");

            return true;
        }

        public async Task<AvatarDTO> UpdateAvatar(UpdateAvatarDTO updateAvatarDTO)
        {
            var user = await unitOfWork.Users.GetByIdAsync(updateAvatarDTO.UserId);
            var avatar = await unitOfWork.Avatars.GetByIdAsync(updateAvatarDTO.Id);

            await unitOfWork.CommitAsync();

            return mapper.Map<AvatarDTO>(avatar);
        }

        public async Task UpdateAvatarGainedExp(Avatar avatar, int exp)
        {
            // Add event of completion
            var experienceGainedEvent = await eventItemsService.AddNewEventItem(avatar.UserId, EventType.AvatarExpGained, EntityType.Avatar, avatar.Id, exp);

            avatar.Exp += exp;

            if (avatar.Exp >= avatar.ExpToNewLevel)
            {
                await UpdateAvatarGainedLevel(avatar);
            }

        }

        public async Task UpdateAvatarGainedLevel(Avatar avatar)
        {
            avatar.Level++;
            
            SetExperienceNeededForLevel(avatar, avatar.Level);

            var avatarLevelGainedEvent = await eventItemsService.AddNewEventItem(avatar.UserId, EventType.AvatarLevelGained, EntityType.Avatar, avatar.Id);

            notificationsService.AddNotification(NotificationsHelper.CreateNotification(avatar, avatarLevelGainedEvent));

            if(await HasAvatarDiscoveredNewCard(avatar))
            {
                var avatarCardDiscoveredEvent = await eventItemsService.AddNewEventItem(avatar.Id, EventType.CardDiscovered, null, null);

                notificationsService.AddNotification(NotificationsHelper.CreateNotification(avatar, avatarCardDiscoveredEvent));
            }
        }

        public async Task<bool> HasAvatarDiscoveredNewCard(Avatar avatar)
        {
            var cardsEnabledToCurrentLevel = await rewardCardsService.GetTechnologyCardsForAvatar(avatar.Id);
            var cardEnabledByCurrentLevel = cardsEnabledToCurrentLevel.Where(card => card.RequiredLevel == avatar.Level);
            return cardEnabledByCurrentLevel != null;
        }


        private void SetExperienceNeededForLevel(Avatar avatar, int level)
        {
            var expRangeForLevel = experienceHelper.GetXpRangeForLevel(level);
            avatar.ExpToCurrentLevel = expRangeForLevel.StartXp;
            avatar.ExpToNewLevel = expRangeForLevel.EndXp;
        }
    }
}
