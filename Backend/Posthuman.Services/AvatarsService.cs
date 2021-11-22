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

namespace Posthuman.Services
{
    public class AvatarsService : IAvatarsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly INotificationsService notificationsService;

        public AvatarsService(IUnitOfWork unitOfWork, IMapper mapper,
            INotificationsService notificationsService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.notificationsService = notificationsService;
        }

        public async Task<AvatarDTO> GetActiveAvatar()
        {
            var activeAvatars = unitOfWork
                .Avatars
                .Find(a => a.IsActive);

            var activeAvatar = await unitOfWork.Avatars.GetActiveAvatarAsync();

            if (activeAvatar == null)
               throw new ArgumentNullException("avatar");

            var mapped = mapper.Map<AvatarDTO>(activeAvatar);

            notificationsService.AddNotification(NotificationsHelper.CreateNotification(
                activeAvatar,
                $"{activeAvatar.Name}",
                "",//$"[{activeAvatar.Name}]: systems ready",
                $"[{activeAvatar.Name}]: XP: {activeAvatar.Exp}   Level: [{activeAvatar.Level}]",
                "Some more bullshit",
                "Activated")); ;

            await notificationsService.SendAllNotifications();

            return mapped;
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

            if (avatar != null)
                return mapper.Map<AvatarDTO>(avatar);

            return null;
        }

        public async Task DeactivateAllAvatars()
        {
            var activeAvatars = unitOfWork
                .Avatars
                .Find(a => a.IsActive);

            foreach (var activeAvatar in activeAvatars)
                activeAvatar.IsActive = false;

            await unitOfWork.CommitAsync();
        }

        public async Task SetActiveAvatar(int id)
        {
            var avatarToActivate = await unitOfWork.Avatars.GetByIdAsync(id);

            if (avatarToActivate != null && !avatarToActivate.IsActive)
            {
                await DeactivateAllAvatars();

                avatarToActivate.IsActive = true;

                await unitOfWork.CommitAsync();
            }
        }

        public async Task UpdateAvatar(AvatarDTO avatarDTO)
        {
            var avatar = await unitOfWork.Avatars.GetByIdAsync(avatarDTO.Id);

            if (avatar == null)
                return;

            if(avatar.IsActive != avatarDTO.IsActive)
            {
                var allAvatars = await unitOfWork.Avatars.GetAllAsync();

                foreach(var av in allAvatars)
                {
                    if(av.IsActive == true)
                    {
                        av.IsActive = false;
                    }
                }    

                avatar.IsActive = avatarDTO.IsActive;
            }

            await unitOfWork.CommitAsync();
        }
    }
}
