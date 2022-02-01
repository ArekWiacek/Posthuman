using System.Collections.Generic;
using System.Threading.Tasks;
using Posthuman.RealTime.Notifications;
using Microsoft.AspNetCore.SignalR;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.DTO.Avatar;

namespace Posthuman.Services
{
    public class NotificationsService : INotificationsService
    {
        private IHubContext<NotificationsHub, INotificationsClient> NotificationsContext;
        private List<NotificationMessage> notifications = new List<NotificationMessage>();

        public NotificationsService(
            IHubContext<NotificationsHub, INotificationsClient> notificationsContext)
        {
            NotificationsContext = notificationsContext;
        }

        public void AddNotification(NotificationMessage notification)
        {
            notifications.Add(notification);
        }

        public async Task SendAllNotifications()
        {
            if (notifications.Count == 0)
                return;

            foreach (var notification in notifications)
            {
                await NotificationsContext.Clients.All.ReceiveNotification(notification);
            }
        }

        public async Task UpdateAvatar(AvatarDTO avatarDTO)
        {
            await NotificationsContext.Clients.All.ReceiveAvatarUpdate(avatarDTO);
        }
    }
}
