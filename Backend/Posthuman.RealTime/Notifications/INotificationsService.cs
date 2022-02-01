using System.Threading.Tasks;
using Posthuman.Core.Models.DTO.Avatar;

namespace Posthuman.RealTime.Notifications
{
    public interface INotificationsService
    {
        Task UpdateAvatar(AvatarDTO avatarDTO);
        void AddNotification(NotificationMessage notification);
        Task SendAllNotifications();
    }
}
